using System.Runtime.InteropServices;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// <see cref="SafeHandle"/> for device context.
    /// </summary>
    public sealed class DeviceContextHandle : SafeHandle
    {
        /// <summary>
        /// True if handle value is zero, false otherwise.
        /// </summary>
        public override bool IsInvalid => handle == default;
        /// <summary>
        /// Window handle associated with device contest handle.
        /// </summary>
        private readonly NativeWindowHandle _windowHandle;

        /// <summary>
        /// Create instance with specified device context.
        /// </summary>
        /// <param name="windowHandle">Window handle associated with device contest handle.</param>
        /// <param name="hDc">Device context handle.</param>
        public DeviceContextHandle(NativeWindowHandle windowHandle, nint hDc)
            : base(hDc, true)
        {
            _windowHandle = windowHandle;
        }

        /// <summary>
        /// Release device context.
        /// </summary>
        /// <returns>
        /// <para>If the method succeeds, the return value is true.</para>
        /// <para>If the method fails, the return value is false.</para>
        /// </returns>
        protected override bool ReleaseHandle()
        {
            return NativeWindow.SafeNativeMethods.ReleaseDC(_windowHandle.DangerousGetHandle(), handle);
        }
    }
}
