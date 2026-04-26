using System;
using System.Runtime.InteropServices;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// <see cref="SafeHandle"/> for OpenGL Rendering Context.
    /// </summary>
    public sealed class GLRenderContextHandle : SafeHandle
    {
        /// <summary>
        /// True if handle value is zero, false otherwise.
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;

        /// <summary>
        /// </summary>
        /// <param name="hGlRc">The handle of the OpenGL rendeting context.</param>
        public GLRenderContextHandle(IntPtr hGlRc)
            : base(hGlRc, true)
        {
        }

        /// <summary>
        /// Release OpenGL rendering context.
        /// </summary>
        /// <returns>
        /// <para>If the method succeeds, the return value is true.</para>
        /// <para>If the method fails, the return value is false.</para>
        /// </returns>
        protected override bool ReleaseHandle()
        {
            return WGL.SafeNativeMethods.WglDeleteContext(handle);
        }
    }
}
