namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Fragment shader class.
    /// </summary>
    public sealed class GLFragmentShader : GLShader
    {
        /// <summary>
        /// Create fragment shader resource.
        /// </summary>
        public GLFragmentShader()
            : base(GLConst.GL_FRAGMENT_SHADER)
        {
        }
    }
}
