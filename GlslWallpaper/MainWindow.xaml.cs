using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GlslWallpaper.OpenGL;
using GlslWallpaper.Resources;
using GlslWallpaper.Win32;
using GlslWallpaper.Internals;
using Microsoft.Win32;
#if !NET8_0_OR_GREATER
using Microsoft.WindowsAPICodePack.Dialogs;
#endif  // !NET8_0_OR_GREATER


namespace GlslWallpaper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Vertex coordinates.
        /// </summary>
        private static readonly float[] _vertices = [
            -1.0f, 1.0f, 0.0f,
            1.0f, 1.0f, 0.0f,
            -1.0f, -1.0f, 0.0f,
            1.0f, -1.0f, 0.0f
        ];
        /// <summary>
        /// Vertex indices.
        /// </summary>
        private static readonly byte[] _triangles = [
            0, 2, 1,
            1, 2, 3
        ];
        /// <summary>
        /// File extensions for OpenGL shader files that are supported.
        /// </summary>
        private static readonly string[] _shaderSuffixes = [".glsl", ".frag", ".geeker", ".geekest", ".geeker300es", ".geekest300es", ".shadertoy"];
        /// <summary>
        /// Class Atom.
        /// </summary>
        private ClassAtom? _atom;
        /// <summary>
        /// Monitor window list.
        /// </summary>
        private readonly List<MonitorWindowInfo> _monitorWindowInfoList = [];
        /// <summary>
        /// Shader path list.
        /// </summary>
        private readonly List<string> _shaderPathList = [];
        /// <summary>
        /// Cache dictionary of shader paths and built shader programs.
        /// </summary>
        private readonly Dictionary<string, ProgramSet> _shaderPathProgDict = [];
        /// <summary>
        /// Animation rendering thread.
        /// This thread can stopped with <see cref="_animationMre"/>.
        /// </summary>
        private Thread? _animationRenderThread;
        /// <summary>
        /// <see cref="ManualResetEvent"/> to stop <see cref="_animationRenderThread"/>.
        /// </summary>
        private ManualResetEvent? _animationMre;
        /// <summary>
        /// Slide show thread.
        /// This thread can stopped with <see cref="_slideShowMre"/>.
        /// </summary>
        private Thread? _slideShowThread;
        /// <summary>
        /// <see cref="ManualResetEvent"/> to stop <see cref="_slideShowThread"/>.
        /// </summary>
        private ManualResetEvent? _slideShowMre;
        /// <summary>
        /// <see cref="Stopwatch"/> for total elapased time.
        /// </summary>
        private readonly Stopwatch _totalSw = new();
        /// <summary>
        /// Total frame count.
        /// </summary>
        private int _frameCount;
        /// <summary>
        /// Last elapsed time.
        /// </summary>
        private long _prevRenderElapsedMs;
        /// <summary>
        /// Animation loop interval.
        /// </summary>
        private int _loopInterval = 66;
        /// <summary>
        /// Slide show interval.
        /// </summary>
        private int _slideShowInterval = 10 * 1000;
        /// <summary>
        /// Lock object for <see cref="Render(long)"/>.
        /// </summary>
#if NET9_0_OR_GREATER
        private readonly Lock _renderLock = new();
#else
        private readonly object _renderLock = new();
#endif  // NET9_0_OR_GREATER


        /// <summary>
        /// Initialize components.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Setup();
            _shaderPathList.AddRange(GatherShaderPath("."));
        }

        /// <summary>
        /// Setup render target window for each monitor and setup OpenGL rendering context.
        /// </summary>
        private void Setup()
        {
            try
            {
                var hWorkerW = NativeWindow.GetWorkerW();

                var monitorInfoList = NativeWindow.GetAllMonitorInfo();
                _atom = NativeWindow.RegisterClass("GlslWallpaper");

                //
                // Create windows for each monitors.
                //
                var monitorWindowInfoList = _monitorWindowInfoList;
                monitorWindowInfoList.Clear();

                var rowIndex = 0;
                var checkBoxSlideBinding = new Binding(nameof(_checkBoxSlide.IsChecked))
                {
                    Source = _checkBoxSlide,
                    Converter = new InverseBooleanConverter()
                };
                foreach (var mi in monitorInfoList)
                {
                    var windowHandle = NativeWindow.CreateMonitorWindow(_atom, mi.MonitorRect, hWorkerW);

                    var grid = _gridShaderSource;
                    grid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = GridLength.Auto
                    });

                    var label = new Label()
                    {
                        Content = "Monitor " + (rowIndex + 1),
                        Margin = new Thickness(3.0, 3.0, 3.0, 3.0)
                    };
                    Grid.SetRow(label, rowIndex);
                    Grid.SetColumn(label, 0);
                    grid.Children.Add(label);

                    var textAreaShaderSource = new TextBox()
                    {
                        AllowDrop = true,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(3.0, 3.0, 3.0, 3.0)
                    };
                    textAreaShaderSource.SetBinding(IsEnabledProperty, checkBoxSlideBinding);
                    Grid.SetRow(textAreaShaderSource, rowIndex);
                    Grid.SetColumn(textAreaShaderSource, 1);
                    grid.Children.Add(textAreaShaderSource);
                    textAreaShaderSource.PreviewDragOver += (sender, e) =>
                    {
                        var data = e.Data;
                        if (data.GetDataPresent(DataFormats.FileDrop))
                        {
                            foreach (var path in (string[])data.GetData(DataFormats.FileDrop))
                            {
                                if (File.Exists(path))
                                {
                                    e.Effects = DragDropEffects.All;
                                    e.Handled = true;
                                    return;
                                }
                            }
                        }

                        e.Effects = DragDropEffects.None;
                        e.Handled = true;
                    };
                    textAreaShaderSource.Drop += (sender, e) =>
                    {
                        var data = e.Data;
                        if (data.GetDataPresent(DataFormats.FileDrop))
                        {
                            foreach (var path in (string[])data.GetData(DataFormats.FileDrop))
                            {
                                if (File.Exists(path))
                                {
                                    ((TextBox)sender).Text = path;
                                    break;
                                }
                            }
                        }
                    };

                    var button = new Button()
                    {
                        Content = " ... ",
                        Margin = new Thickness(3.0, 6.0, 3.0, 6.0)
                    };
                    button.SetBinding(IsEnabledProperty, checkBoxSlideBinding);
                    button.Click += (sender, e) =>
                    {
                        var dialog = new OpenFileDialog()
                        {
                            InitialDirectory = Path.GetDirectoryName(textAreaShaderSource.Text),
                            Filter = "GLSL (*.glsl)|*.glsl"
                                + "|GLSL (*.frag)|*.frag"
                                + "|Twigl Geeker (*.geeker)|*.geeker"
                                + "|Twigl Geekest (*.geekest)|*.geekest"
                                + "|Twigl Geeker 300es (*.geeker300es)|*.geeker300es"
                                + "|Twigl Geekest 300es (*.geekest300es)|*.geekest300es"
                                + "|ShaderToy (*.shadertoy)|*.shadertoy"
                                + "|All Files (*.*)|*.*"
                        };
                        if (dialog.ShowDialog().GetValueOrDefault())
                        {
                            textAreaShaderSource.Text = dialog.FileName;
                        }
                    };
                    Grid.SetRow(button, rowIndex);
                    Grid.SetColumn(button, 2);
                    grid.Children.Add(button);

                    rowIndex++;

                    var mwi = new MonitorWindowInfo(windowHandle, mi.MonitorRect, mi.DeviceName, textAreaShaderSource);
                    if (monitorWindowInfoList.Count == 0)
                    {
                        WGL.MakeCurrent(mwi.DeviceContextHandle, mwi.RenderContextHandle);
                    }
                    else
                    {
                        WGL.ShareLists(monitorWindowInfoList[0].RenderContextHandle, mwi.RenderContextHandle);
                    }
                    monitorWindowInfoList.Add(mwi);

                    textAreaShaderSource.TextChanged += (sender, e) =>
                    {
                        var textBox = (TextBox)sender;
                        if (!textBox.IsEnabled)
                        {
                            return;
                        }
                        var filePath = textBox.Text;
                        if (!File.Exists(filePath))
                        {
                            return;
                        }
                        Task.Run(() =>
                        {
                            try
                            {
                                SetShaderSource(mwi, filePath);
                            }
                            catch (Exception ex)
                            {
                                Console.Error.WriteLine(ex);
                            }
                        });
                    };
                }

                //
                // Load OpenGL functions.
                //
                WGL.MakeCurrent(monitorWindowInfoList[0].DeviceContextHandle, monitorWindowInfoList[0].RenderContextHandle);
                GL.GetVersion(out var major, out var minor);
                Console.WriteLine($"OpenGL version: {major}.{minor}");
                Console.WriteLine($"Language Version: {GL.GetString(GLStringParams.ShadingLanguageVersion)}");
                Console.WriteLine($"Vendor: {GL.GetString(GLStringParams.Vendor)}");
                Console.WriteLine($"Renderer: {GL.GetString(GLStringParams.Renderer)}");
                Console.WriteLine("Extensions:");
                foreach (var extension in GL.GetExtensions())
                {
                    Console.WriteLine($"  {extension}");
                }
                GL.Initialize();

                //
                // Setup Vertex Array Object, Vertex Buffer Object and Index Buffer Object.
                //
                var vbo = GL.CreateVertexBufferObject(_vertices);
                var ibo = GL.CreateIndexBufferObject(_triangles);
                foreach (var mwi in monitorWindowInfoList)
                {
                    WGL.MakeCurrent(mwi.DeviceContextHandle, mwi.RenderContextHandle);
                    mwi.VertexArray = new GLVertexArray(1);  // VAO is necessary on Core Profile.
                    mwi.VertexBuffer = vbo;
                    mwi.IndexBuffer = ibo;
                }

                //
                // Setup program.
                //
                var programSet = BuildProgram(AppResource.GetText("frag300es.glsl"));
                foreach (var mwi in _monitorWindowInfoList)
                {
                    mwi.ProgramSet = programSet;
                }

                WGL.ResetCurrent();

                _totalSw.Restart();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                MessageBox.Show(ex.ToString(), ex.GetType().Name);
            }
        }

        /// <summary>
        /// Try to set shader source.
        /// </summary>
        /// <param name="mwi">Monitor window information.</param>
        /// <param name="filePath">Shader file path.</param>
        private void SetShaderSource(MonitorWindowInfo mwi, string filePath)
        {
            var shaderPath = Path.GetFullPath(filePath);

            lock (_renderLock)
            {
                try
                {
                    WGL.MakeCurrent(mwi.DeviceContextHandle, mwi.RenderContextHandle);

                    if (_shaderPathProgDict.TryGetValue(shaderPath, out var cachedProgramSet))
                    {
                        mwi.ProgramSet = cachedProgramSet;
                        Console.WriteLine($"Set shader: {mwi.DeviceName}: {shaderPath} (Use cache)");
                        Render(_totalSw.ElapsedMilliseconds);
                        return;
                    }

                    try
                    {
                        var sw = Stopwatch.StartNew();
                        Console.WriteLine($"Compile {shaderPath} ...");
                        var programSet = BuildProgramFromFile(shaderPath);
                        Console.WriteLine($"Compile {shaderPath} ... Done; Elapsed {sw.Elapsed.TotalMilliseconds} ms");

                        mwi.ProgramSet = programSet;
                        mwi.ShaderSourcePath = shaderPath;
                        Console.WriteLine($"Set shader: {mwi.DeviceName}: {shaderPath}");
                        _shaderPathProgDict.Add(shaderPath, programSet);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex);
                    }

                    Render(_totalSw.ElapsedMilliseconds);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    WGL.ResetCurrent(true);
                }
            }
        }

        /// <summary>
        /// Render one frame.
        /// </summary>
        /// <param name="elapsedMs">Elapsed time in milliseconds.</param>
        private void Render(long elapsedMs)
        {
            _prevRenderElapsedMs = elapsedMs;
            var time = elapsedMs * 0.001f;
            var timeDelta = (elapsedMs - _prevRenderElapsedMs) * 0.001f;
            var dt = DateTime.Now;
            foreach (var mwi in _monitorWindowInfoList)
            {
                WGL.MakeCurrent(mwi.DeviceContextHandle, mwi.RenderContextHandle);

                GL.Viewport(0, 0, mwi.Rect.Width, mwi.Rect.Height);
                GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
                GL.Clear(GLClearMaskBits.ColorBufferBit);
                // GL.ClearDepth(1.0f);
                // GL.ClearStencil(0);

                var ps = mwi.ProgramSet!;
                ps.Use();
                GL.BindVertexArray(mwi.VertexArray![0]);  // VAO is necessary on Core Profile.
                GL.BindBuffer(GLTargets.ArrayBuffer, mwi.VertexBuffer![0]);
                GL.BindBuffer(GLTargets.ElementArrayBuffer, mwi.IndexBuffer![0]);

                foreach (var aPosition in ps.AttributePositionList)
                {
                    GL.EnableVertexAttribArray((uint)aPosition);
                    GL.VertexAttribPointer((uint)aPosition, 3, GLValueTypes.Float, false, 0, IntPtr.Zero);
                }

                var rect = mwi.Rect;
                ps.SetUniformResolution((float)rect.Width, (float)rect.Height);

                var mousePoint = NativeWindow.GetCursorPos();
                mousePoint.X = Math.Max(0, Math.Min(mousePoint.X - rect.Left, rect.Width - 1));
                mousePoint.Y = Math.Max(0, Math.Min(mousePoint.Y - rect.Top, rect.Height - 1));
                ps.SetUniformMouse((float)mousePoint.X / (float)(rect.Width - 1), 1.0f - (float)mousePoint.Y / (float)(rect.Height - 1));
                ps.SetUniformTime(time);
                ps.SetUniformTimeDelta(timeDelta);
                ps.SetUniformFrameRate(1.0f / timeDelta);
                ps.SetUniformFrameCount(_frameCount);
                ps.SetUniformDate(dt);

                GL.DrawElements(GLDrawModes.Triangles, _triangles.Length, GLValueTypes.UnsignedByte);

                Gdi32.SwapBuffers(mwi.DeviceContextHandle);
            }
            WGL.ResetCurrent();
            _frameCount++;
        }

        /// <summary>
        /// Start flip thread.
        /// </summary>
        /// <param name="mre"><see cref="ManualResetEvent"/> instance to stop created thread.</param>
        /// <returns>Created thead.</returns>
        private Thread StartSlideShowThread(ManualResetEvent mre)
        {
            var th = new Thread(() =>
            {
                try
                {
                    var rnd = new Random();
                    var totalSw = _totalSw;
                    while (true)
                    {
                        foreach (var mwi in _monitorWindowInfoList)
                        {
                            while (_shaderPathList.Count > 0)
                            {
                                var index = rnd.Next(0, _shaderPathList.Count);
                                var shaderPath = _shaderPathList[index];
                                try
                                {
                                    SetShaderSource(mwi, shaderPath);
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.Error.WriteLine(ex);
                                    _shaderPathList.RemoveAt(index);
                                }
                            }
                        }
                        if (mre.WaitOne(_slideShowInterval))
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
            })
            {
                IsBackground = true
            };
            th.Start();
            return th;
        }

        /// <summary>
        /// Stop <see cref="_animationRenderThread"/> using <see cref="_animationMre"/>
        /// </summary>
        private void StopAnimationThread()
        {
            var mre = Interlocked.Exchange(ref _animationMre, null);
            var thread = Interlocked.Exchange(ref _animationRenderThread, null);
            StopThread(thread, mre, 3000);
        }

        /// <summary>
        /// Start animation thread.
        /// </summary>
        /// <param name="mre"><see cref="ManualResetEvent"/> instance to stop created thread.</param>
        /// <returns>Created thead.</returns>
        private Thread StartAnimationRenderThread(ManualResetEvent mre)
        {
            var th = new Thread(() =>
            {
                try
                {
                    var totalSw = _totalSw;
                    var sleepSw = new Stopwatch();
                    while (true)
                    {
                        lock (_renderLock)
                        {
                            try
                            {
                                sleepSw.Restart();
                                Render(totalSw.ElapsedMilliseconds);
                            }
                            finally
                            {
                                WGL.ResetCurrent(true);
                            }
                        }

                        var sleepTime = (int)Math.Max(0, _loopInterval - sleepSw.ElapsedMilliseconds);
                        if (mre.WaitOne(sleepTime))
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
            })
            {
                IsBackground = true
            };
            th.Start();
            return th;
        }

        /// <summary>
        /// Stop <see cref="_slideShowThread"/> using <see cref="_slideShowMre"/>
        /// </summary>
        private void StopSlideShowThread()
        {
            var mre = Interlocked.Exchange(ref _slideShowMre, null);
            var thread = Interlocked.Exchange(ref _slideShowThread, null);
            StopThread(thread, mre, 3000);
        }

        /// <summary>
        /// <para>Occurs when the element is laid out, rendered, and ready for interaction.</para>
        /// <para>Stop auto sizing form.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached. (this)</param>
        /// <param name="e">The event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SizeToContent = SizeToContent.Manual;
        }

        /// <summary>
        /// <para>Occurs when the element is removed from within an element tree of loaded elements.</para>
        /// <para>Stop all running thread and dispose all resources.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached. (this)</param>
        /// <param name="e">The event data.</param>
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            StopAnimationThread();
            StopSlideShowThread();
            foreach (var programSet in _shaderPathProgDict.Values)
            {
                programSet.Dispose();
            }
            foreach (var mwi in _monitorWindowInfoList)
            {
                mwi.Dispose();
            }
            _atom?.Dispose();
        }

        /// <summary>
        /// <para>Occurs when a <see cref="System.Windows.Controls.CheckBox"/> is checked.</para>
        /// <para>Start flip thread.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void CheckBoxSlideShow_Checked(object sender, RoutedEventArgs e)
        {
            _slideShowMre = new ManualResetEvent(false);
            _slideShowThread = StartSlideShowThread(_slideShowMre);
        }

        /// <summary>
        /// <para>Occurs when a <see cref="System.Windows.Controls.CheckBox"/> is unchecked.</para>
        /// <para>Stop flip thread.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void CheckBoxSlideShow_Unchecked(object sender, RoutedEventArgs e)
        {
            StopSlideShowThread();
        }

        /// <summary>
        /// Occurs when content changes in the text box.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void TextBoxSlideShowSource_TextChanged(object sender, TextChangedEventArgs e)
        {
            var dirPath = ((TextBox)sender).Text;
            if (!Directory.Exists(dirPath))
            {
                return;
            }
            Task.Run(() =>
            {
                lock (_renderLock)
                {
                    _shaderPathList.Clear();
                    _shaderPathList.AddRange(GatherShaderPath(dirPath));
                }
            });
        }

        /// <summary>
        /// <para>Occurs when the input system reports an underlying drag event with this element as the potential drop target.</para>
        /// <para>Check dragging items has directory or not.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void TextBoxSlideShowSource_PreviewDragOver(object sender, DragEventArgs e)
        {
            var data = e.Data;
            if (data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (var path in (string[])data.GetData(DataFormats.FileDrop))
                {
                    if (Directory.Exists(path))
                    {
                        e.Effects = DragDropEffects.All;
                        e.Handled = true;
                        return;
                    }
                }
            }

            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        /// <summary>
        /// <para>Occurs when an object is dropped within the bounds of an element that is acting as a drop target.</para>
        /// <para></para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void TextBoxSlideShowSource_Drop(object sender, DragEventArgs e)
        {
            var data = e.Data;
            if (data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (var path in (string[])data.GetData(DataFormats.FileDrop))
                {
                    if (Directory.Exists(path))
                    {
                        ((TextBox)sender).Text = path;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// <para>Occurs when a Button is clicked.</para>
        /// <para>Set slide show source directory to the text box.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void ButtonSetSlideShowSource_Click(object sender, RoutedEventArgs e)
        {
#if NET8_0_OR_GREATER
            var dialog = new OpenFolderDialog()
            {
                Title = "Select shader directory",
                Multiselect = false
            };
            if (!dialog.ShowDialog().GetValueOrDefault())
            {
                return;
            }

            var dirPath = dialog.FolderName;
            if (!Directory.Exists(dirPath))
            {
                return;
            }
            _textBoxSlideShowSource.Text = dirPath;
#else
            using (var dialog = new CommonOpenFileDialog()
            {
                Title = "Select shader directory",
                IsFolderPicker = true
            })
            {
                if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
                {
                    return;
                }

                var dirPath = dialog.FileName;
                if (!Directory.Exists(dirPath))
                {
                    return;
                }
                _textBoxSlideShowSource.Text = dirPath;
            }
#endif  // NET8_0_OR_GREATER
        }

        /// <summary>
        /// <para>Occurs when the range value changes.</para>
        /// <para>Change flip interval.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">Provides data about a change in value to a dependency property as reported by particular routed events,
        /// including the previous and current value of the property that changed.</param>
        private void SliderSlideShowInterval_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _slideShowInterval = (int)(e.NewValue * 1000.0);
        }

        /// <summary>
        /// <para>Occurs when a <see cref="System.Windows.Controls.CheckBox"/> is checked.</para>
        /// <para>Start animation thread.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void AnimationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _animationMre = new ManualResetEvent(false);
            _animationRenderThread = StartAnimationRenderThread(_animationMre);
        }

        /// <summary>
        /// <para>Occurs when a <see cref="System.Windows.Controls.CheckBox"/> is unchecked.</para>
        /// <para>Stop animation thread.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void AnimationCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            StopAnimationThread();
        }

        /// <summary>
        /// <para>Occurs when the range value changes.</para>
        /// <para>Change animation loop interval.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">Provides data about a change in value to a dependency property as reported by particular routed events,
        /// including the previous and current value of the property that changed.</param>
        private void FpsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _loopInterval = (int)(1000.0 / e.NewValue);
        }

        /// <summary>
        /// <para>Occurs when a <see cref="System.Windows.Controls.CheckBox"/> is checked.</para>
        /// <para>Enable VSync.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void VSyncCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            lock (_renderLock)
            {
                try
                {
                    var mwi = _monitorWindowInfoList[_monitorWindowInfoList.Count - 1];
                    WGL.MakeCurrent(mwi.DeviceContextHandle, mwi.RenderContextHandle);
                    WGL.SwapInterval(1);
                    _loopInterval = 1;
                }
                finally
                {
                    WGL.ResetCurrent();
                }
            }
        }

        /// <summary>
        /// <para>Occurs when a <see cref="System.Windows.Controls.CheckBox"/> is unchecked.</para>
        /// <para>Disable VSync.</para>
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void VSyncCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            lock (_renderLock)
            {
                try
                {
                    var mwi = _monitorWindowInfoList[_monitorWindowInfoList.Count - 1];
                    WGL.MakeCurrent(mwi.DeviceContextHandle, mwi.RenderContextHandle);
                    WGL.SwapInterval(0);
                    _loopInterval = (int)(1000.0 / _fpsSlide.Value);
                }
                finally
                {
                    WGL.ResetCurrent();
                }
            }
        }


        /// <summary>
        /// Gather OpenGL shader source files under specified directory.
        /// </summary>
        /// <param name="dirPath">Target directory.</param>
        /// <returns>OpenGL shader source paths.</returns>
        private static IEnumerable<string> GatherShaderPath(string dirPath)
        {
            foreach (var path in Directory.EnumerateFiles(dirPath, "*", SearchOption.AllDirectories))
            {
                foreach (var suffix in _shaderSuffixes)
                {
                    if (path.EndsWith(suffix))
                    {
                        yield return path;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Build OpenGL program from source file.
        /// </summary>
        /// <param name="shaderPath">Fragment shader source path.</param>
        /// <returns>Program set.</returns>
        private static ProgramSet BuildProgramFromFile(string shaderPath)
        {
            var source = File.ReadAllText(shaderPath);
            switch (Path.GetExtension(shaderPath))
            {
                case ".geeker":
                    source = Twigl.ConvertGeeker(source);
                    break;
                case ".geekest":
                    source = Twigl.ConvertGeekest(source);
                    break;
                case ".geeker300es":
                    source = Twigl.ConvertGeeker300es(source);
                    break;
                case ".geekest300es":
                    source = Twigl.ConvertGeekest300es(source);
                    break;
                case ".shadertoy":
                    source = ShaderToy.Convert(source);
                    break;
                default:
                    break;
            }
            return BuildProgram(source);
        }

        /// <summary>
        /// Compile and link OpenGL program.
        /// </summary>
        /// <param name="source">Fragment shader source.</param>
        /// <returns>Program set.</returns>
        private static ProgramSet BuildProgram(string source)
        {
            using (var vs = GL.CompileVertexShader(AppResource.GetText("vert100.glsl"), out var warnMessage))
            {
                if (warnMessage != null)
                {
                    Console.Error.WriteLine(warnMessage);
                }
                using (var fs = GL.CompileFragmentShader(source, out warnMessage))
                {
                    if (warnMessage != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Error.WriteLine(warnMessage);
                        Console.ResetColor();
                    }

                    var programSet = new ProgramSet(GL.LinkShaders(vs, fs, out warnMessage));
                    if (warnMessage != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Error.WriteLine(warnMessage);
                        Console.ResetColor();
                    }

                    programSet.AddAttributePosition("position");
                    programSet.AddUniformResolution("u_resolution");
                    programSet.AddUniformResolution("resolution");
                    programSet.AddUniformResolution("r");
                    programSet.AddUniformResolution3("iResolution");
                    programSet.AddUniformTime("u_time");
                    programSet.AddUniformTime("iTime");
                    programSet.AddUniformTime("t");
                    programSet.AddUniformTimeDelta("iTimeDelta");
                    programSet.AddUniformFrameRate("iFrameRate");
                    programSet.AddUniformFrameCount("iFrame");
                    programSet.AddUniformMouse("m");
                    programSet.AddUniformMouse("mouse");
                    programSet.AddUniformMouse("u_mouse");
                    programSet.AddUniformDate("iDate");

                    return programSet;
                }
            }
        }

        /// <summary>
        /// Attempt to stop specified thread.
        /// </summary>
        /// <param name="thread">The <see cref="Thread"/> to stop.</param>
        /// <param name="mre"><see cref="ManualResetEvent"/> to stop the thread.</param>
        /// <param name="timeout">Timeout in milliseconds.</param>
        private static void StopThread(Thread? thread, ManualResetEvent? mre, int timeout)
        {
            if (thread != null)
            {
                mre?.Set();
                if (!thread.Join(timeout))
                {
                    thread.Interrupt();
                    Thread.Sleep(1000);
                }
            }

            mre?.Dispose();
        }
    }
}
