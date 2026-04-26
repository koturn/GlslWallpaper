using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using GlslWallpaper.OpenGL;
using GlslWallpaper.Win32;
using GlslWallpaper.Resources;


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
        /// Flip shader thread.
        /// This thread can stopped with <see cref="_flipMre"/>.
        /// </summary>
        private Thread? _flipThread;
        /// <summary>
        /// <see cref="ManualResetEvent"/> to stop <see cref="_flipThread"/>.
        /// </summary>
        private ManualResetEvent? _flipMre;
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
        /// Flip loop interval.
        /// </summary>
        private int _flipInterval = 10 * 1000;
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
            _shaderPathList = GatherShaderPath(".");
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
                foreach (var mi in monitorInfoList)
                {
                    var windowHandle = NativeWindow.CreateMonitorWindow(_atom, mi.MonitorRect, hWorkerW);

                    var mwi = new MonitorWindowInfo(windowHandle, mi.MonitorRect, mi.DeviceName);
                    if (monitorWindowInfoList.Count == 0)
                    {
                        WGL.MakeCurrent(mwi.DeviceContextHandle, mwi.RenderContextHandle);
                    }
                    else
                    {
                        WGL.ShareLists(monitorWindowInfoList[0].RenderContextHandle, mwi.RenderContextHandle);
                    }
                    monitorWindowInfoList.Add(mwi);
                }

                //
                // Load OpenGL functions.
                //
                WGL.MakeCurrent(monitorWindowInfoList[0].DeviceContextHandle, monitorWindowInfoList[0].RenderContextHandle);
                GL.GetVertsion(out var major, out var minor);
                Console.WriteLine($"OpenGL version: {major}.{minor}");
                Console.WriteLine($"Language Version: {GL.GetString(GLConst.GL_SHADING_LANGUAGE_VERSION)}");
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

                //
                // Start threads.
                //
                _totalSw.Restart();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                MessageBox.Show(ex.ToString(), ex.GetType().Name);
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
                GL.Clear(GLConst.GL_COLOR_BUFFER_BIT);
                // GL.ClearDepth(1.0f);
                // GL.ClearStencil(0);

                var ps = mwi.ProgramSet!;
                ps.Use();
                GL.BindVertexArray(mwi.VertexArray![0]);  // VAO is necessary on Core Profile.
                GL.BindBuffer(GLConst.GL_ARRAY_BUFFER, mwi.VertexBuffer![0]);
                GL.BindBuffer(GLConst.GL_ELEMENT_ARRAY_BUFFER, mwi.IndexBuffer![0]);

                foreach (var aPosition in ps.AttributePositionList)
                {
                    GL.EnableVertexAttribArray((uint)aPosition);
                    GL.VertexAttribPointer((uint)aPosition, 3, GLConst.GL_FLOAT, false, 0, IntPtr.Zero);
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

                GL.DrawElements(GLConst.GL_TRIANGLES, _triangles.Length, GLConst.GL_UNSIGNED_BYTE);

                Gdi32.SwapBuffers(mwi.DeviceContextHandle);
            }
            WGL.ResetCurrent();
            _frameCount++;
        }

        private Thread StartFlipThread(ManualResetEvent mre)
        {
            var th = new Thread(() =>
            {
                try
                {
                    var rnd = new Random();
                    var totalSw = _totalSw;
                    while (true)
                    {
                        lock (_renderLock)
                        {
                            try
                            {
                                foreach (var mwi in _monitorWindowInfoList)
                                {
                                    WGL.MakeCurrent(mwi.DeviceContextHandle, mwi.RenderContextHandle);

                                    while (_shaderPathList.Count > 0)
                                    {
                                        var index = rnd.Next(0, _shaderPathList.Count);
                                        var shaderPath = _shaderPathList[index];
                                        if (_shaderPathProgDict.TryGetValue(shaderPath, out var cachedProgramSet))
                                        {
                                            mwi.ProgramSet = cachedProgramSet;
                                            Console.WriteLine($"Set shader: {mwi.DeviceName}: {shaderPath} (Use cache)");
                                            break;
                                        }

                                        try
                                        {
                                            var sw = Stopwatch.StartNew();
                                            Console.WriteLine($"Compile {shaderPath} ...");
                                            var programSet = BuildProgramFromFile(shaderPath);
                                            Console.WriteLine($"Compile {shaderPath} ... Done; Elapsed {sw.Elapsed.TotalMilliseconds} ms");

                                            mwi.ProgramSet = programSet;
                                            Console.WriteLine($"Set shader: {mwi.DeviceName}: {shaderPath}");
                                            _shaderPathProgDict.Add(shaderPath, programSet);
                                            break;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.Error.WriteLine(ex);
                                            _shaderPathList.RemoveAt(index);
                                        }
                                    }
                                }
                                Render(totalSw.ElapsedMilliseconds);
                            }
                            finally
                            {
                                WGL.ResetCurrent(true);
                            }
                        }

                        if (mre.WaitOne(_flipInterval))
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
        /// Stop <see cref="_flipThread"/> using <see cref="_flipMre"/>
        /// </summary>
        private void StopFlipThread()
        {
            var mre = Interlocked.Exchange(ref _flipMre, null);
            var thread = Interlocked.Exchange(ref _flipThread, null);
            StopThread(thread, mre, 3000);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            StopAnimationThread();
            StopFlipThread();
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

        private void FlipCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _flipMre = new ManualResetEvent(false);
            _flipThread = StartFlipThread(_flipMre);
        }

        private void FlipCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            StopFlipThread();
        }

        private void FlipIntervalSlide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _flipInterval = (int)(e.NewValue * 1000.0);
        }

        private void AnimationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _animationMre = new ManualResetEvent(false);
            _animationRenderThread = StartAnimationRenderThread(_animationMre);
        }

        private void AnimationCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            StopAnimationThread();
        }

        private void FpsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _loopInterval = (int)(1000.0 / e.NewValue);
        }

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


        private static List<string> GatherShaderPath(string dirPath)
        {
            var pathList = new List<string>();
            foreach (var path in Directory.EnumerateFiles(dirPath, "*", SearchOption.AllDirectories))
            {
                foreach (var suffix in _shaderSuffixes)
                {
                    if (path.EndsWith(suffix))
                    {
                        pathList.Add(path);
                        break;
                    }
                }
            }
            return pathList;
        }

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
                        Console.Error.WriteLine(warnMessage);
                    }

                    var programSet = new ProgramSet(GL.LinkShaders(vs, fs, out warnMessage));
                    if (warnMessage != null)
                    {
                        Console.Error.WriteLine(warnMessage);
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

        private static void StopThread(Thread? thread, ManualResetEvent? mre, int timeout)
        {
            if (thread != null)
            {
                mre?.Set();
                if (thread.Join(timeout))
                {
                    thread.Interrupt();
                    Thread.Sleep(1000);
                }
            }

            mre?.Dispose();
        }
    }
}
