namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Vertex shader class.
    /// </summary>
    public sealed class GLVertexShader : GLShader
    {
        /// <summary>
        /// Create vertex shader resource.
        /// </summary>
        public GLVertexShader()
            : base(GLConst.GL_VERTEX_SHADER)
        {
        }
    }
}
