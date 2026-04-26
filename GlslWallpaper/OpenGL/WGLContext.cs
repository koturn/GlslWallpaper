namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Provides some WGL context constants.
    /// </summary>
    internal static class WGLContext
    {
        /// <summary>
        /// request an OpenGL context supporting the specified major version of the API.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://registry.khronos.org/OpenGL/extensions/ARB/WGL_ARB_create_context.txt">WGL_ARB_create_context</seealso>
        /// </remarks>
        public const int MajorVersionARB = 0x2091;
        /// <summary>
        /// request an OpenGL context supporting the specified minor version of the API.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://registry.khronos.org/OpenGL/extensions/ARB/WGL_ARB_create_context.txt">WGL_ARB_create_context</seealso>
        /// </remarks>
        public const int MinorVersionARB = 0x2092;
        /// <summary>
        /// specifies a set of flag bits affecting the rendering context.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://registry.khronos.org/OpenGL/extensions/ARB/WGL_ARB_create_context.txt">WGL_ARB_create_context</seealso>
        /// </remarks>
        public const int FlagsARB = 0x2094;
        /// <summary>
        /// equests an OpenGL context supporting a specific &lt;profile&gt; of the API.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://registry.khronos.org/OpenGL/extensions/ARB/WGL_ARB_create_context.txt">WGL_ARB_create_context</seealso>
        /// </remarks>
        public const int ProfileMaskARB = 0x9126;
        /// <summary>
        /// Request to return a context implementing the &lt;core&gt; profile of OpenGL.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://registry.khronos.org/OpenGL/extensions/ARB/WGL_ARB_create_context.txt">WGL_ARB_create_context</seealso>
        /// </remarks>
        public const int CoreProfileBitARB = 0x00000001;
        /// <summary>
        /// Request to return a context implementing the &lt;compatibility&gt; profile of OpenGL.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://registry.khronos.org/OpenGL/extensions/ARB/WGL_ARB_create_context.txt">WGL_ARB_create_context</seealso>
        /// </remarks>
        public const int CompatibilityProfileBitARB = 0x00000002;
    }
}
