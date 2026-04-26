namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Possible values for second argument of <see cref="GL.GetProgramiv(GLProgram, GLProgramParams, nint)"/>.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgram.xhtml"><c>glGetProgram</c></seealso>
    /// </remarks>
    public enum GLProgramParams
    {
        /// <summary>
        /// Params returns GL_TRUE if the last link operation on program was successful, and GL_FALSE otherwise.
        /// </summary>
        LinkStatus = 0x8b82,
        /// <summary>
        /// Params returns the number of characters in the information log for shader or program including the null termination character
        /// (i.e., the size of the character buffer required to store the information log).
        /// If shader or program has no information log, a value of 0 is returned.
        /// </summary>
        InfoLogLength = 0x8b84
    }
}
