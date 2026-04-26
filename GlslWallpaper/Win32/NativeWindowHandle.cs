using System;
using System.Runtime.InteropServices;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// <see cref="SafeHandle"/> for window handle.
    /// </summary>
    public class NativeWindowHandle : SafeHandle
    {
        /// <summary>
        /// True if handle value is zero, false otherwise.
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;

        /// <summary>
        /// Create <see cref="NativeWindowHandle"/> instance.
        /// </summary>
        /// <param name="hWnd"></param>
        public NativeWindowHandle(IntPtr hWnd)
            : base(IntPtr.Zero, true)
        {
            handle = hWnd;
        }

        /// <summary>
        /// Destroy native window.
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            return NativeWindow.SafeNativeMethods.DestroyWindow(handle);
        }
    }
}
