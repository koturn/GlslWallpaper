using System;
using GlslWallpaper.OpenGL;
using GlslWallpaper.Win32;


namespace GlslWallpaper
{
    /// <summary>
    /// Class of the monitor window (includes device context and rendering context) and OpenGL objects.
    /// </summary>
    public sealed class MonitorWindowInfo : IDisposable
    {
        /// <summary>
        /// <see cref="NativeWindowHandle"/> of monitor window.
        /// </summary>
        public NativeWindowHandle WindowHandle { get; }
        /// <summary>
        /// <see cref="Rect"/> of the monitor.
        /// </summary>
        public Rect Rect { get; }
        /// <summary>
        /// Monitor device name.
        /// </summary>
        public string DeviceName { get; }
        /// <summary>
        /// Device context handle of <see cref="WindowHandle"/>.
        /// </summary>
        public DeviceContextHandle DeviceContextHandle { get; }
        /// <summary>
        /// Rendering context handle of <see cref="DeviceContextHandle"/>.
        /// </summary>
        public GLRenderContextHandle RenderContextHandle { get; private set; }
        /// <summary>
        /// OpenGL vertex arrays.
        /// This member is set null in ctor.
        /// </summary>
        public GLVertexArray? VertexArray { get; set; }
        /// <summary>
        /// OpenGL vertex buffers.
        /// This member is set null in ctor.
        /// </summary>
        public GLBuffer? VertexBuffer { get; set; }
        /// <summary>
        /// OpenGL index buffers.
        /// This member is set null in ctor.
        /// </summary>
        public GLBuffer? IndexBuffer { get; set; }
        /// <summary>
        /// OpenGL shader program.
        /// This member is set null in ctor.
        /// </summary>
        public ProgramSet? ProgramSet { get; set; }
        /// <summary>
        /// A flag property which indicates this instance is disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }


        /// <summary>
        /// Set the OpenGL rendering context to the detected version and create an <see cref="MonitorWindowInfo"/> instance..
        /// </summary>
        /// <param name="windowHandle"><see cref="NativeWindowHandle"/> of monitor window.</param>
        /// <param name="rect"><see cref="Rect"/> of the monitor.</param>
        /// <param name="deviceName">Monitor device name.</param>
        public MonitorWindowInfo(NativeWindowHandle windowHandle, Rect rect, string deviceName)
        {
            WindowHandle = windowHandle;
            Rect = rect;
            DeviceName = deviceName;
            DeviceContextHandle = NativeWindow.GetDC(windowHandle);
            Gdi32.SetupPixelFormat(DeviceContextHandle);
            RenderContextHandle = WGL.CreateContext(DeviceContextHandle);

            WGL.MakeCurrent(DeviceContextHandle, RenderContextHandle);
            GL.GetVertsion(out var major, out var minor);

            UpdateRenderContext([
                WGLConst.WGL_CONTEXT_MAJOR_VERSION_ARB, major,
                WGLConst.WGL_CONTEXT_MINOR_VERSION_ARB, minor,
                WGLConst.WGL_CONTEXT_PROFILE_MASK_ARB, WGLConst.WGL_CONTEXT_CORE_PROFILE_BIT_ARB,
                0
            ]);
        }

        /// <summary>
        /// Setup OpenGL rendering context with specified attributes and create an <see cref="MonitorWindowInfo"/> instance..
        /// </summary>
        /// <param name="windowHandle"><see cref="NativeWindowHandle"/> of monitor window.</param>
        /// <param name="rect"><see cref="Rect"/> of the monitor.</param>
        /// <param name="deviceName">Monitor device name.</param>
        /// <param name="attribs">Attribute array for <see cref="UpdateRenderContext"/>.</param>
        public MonitorWindowInfo(NativeWindowHandle windowHandle, Rect rect, string deviceName, int[] attribs)
        {
            WindowHandle = windowHandle;
            Rect = rect;
            DeviceName = deviceName;
            DeviceContextHandle = NativeWindow.GetDC(windowHandle);
            Gdi32.SetupPixelFormat(DeviceContextHandle);
            RenderContextHandle = WGL.CreateContext(DeviceContextHandle);

            WGL.MakeCurrent(DeviceContextHandle, RenderContextHandle);

            UpdateRenderContext(attribs);
        }


        /// <summary>
        /// Update rendering context.
        /// </summary>
        /// <param name="attribs">Attribute array for <see cref="WGL.CreateContextAttribsARB(DeviceContextHandle, nint, int[])"/>.</param>
        public void UpdateRenderContext(int[] attribs)
        {
            var oldRenderContextHandle = RenderContextHandle;
            WGL.MakeCurrent(DeviceContextHandle, oldRenderContextHandle);
            RenderContextHandle = WGL.CreateContextAttribsARB(DeviceContextHandle, default, attribs);
            oldRenderContextHandle.Dispose();
        }

        /// <summary>
        /// Release all resources used by the <see cref="MonitorWindowInfo"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Release all resources used by the <see cref="MonitorWindowInfo"/> object.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                IndexBuffer?.Dispose();
                VertexBuffer?.Dispose();
                VertexArray?.Dispose();
                RenderContextHandle.Dispose();
                DeviceContextHandle.Dispose();
                WindowHandle.Dispose();
            }

            IsDisposed = true;
        }
    }
}
