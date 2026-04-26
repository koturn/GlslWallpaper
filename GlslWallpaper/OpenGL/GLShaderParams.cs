namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Possible values for second argument of <see cref="GL.GetShaderiv(GLShader, GLShaderParams, nint)"/>.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetShader.xhtml"><c>glGetShader</c></seealso>
    /// </remarks>
    public enum GLShaderParams : uint
    {
        /// <summary>
        /// Params returns GL_TRUE if the last compile operation on shader was successful, and GL_FALSE otherwise.
        /// </summary>
        CompileStatus = 0x8b81,
        /// <summary>
        /// Params returns the number of characters in the information log for shader or program including the null termination character
        /// (i.e., the size of the character buffer required to store the information log).
        /// If shader or program has no information log, a value of 0 is returned.
        /// </summary>
        InfoLogLength = 0x8b84
    }
}
