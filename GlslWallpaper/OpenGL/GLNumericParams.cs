namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Possible arguments for <see cref="GL.GetIntegerv(GLIntegralParams)"/>;
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGet.xhtml"><c>glGet</c></seealso>
    /// </remarks>
    public enum GLIntegralParams : uint
    {
        /// <summary>
        /// Data returns one value, the major version number of the OpenGL API supported by the current context.
        /// </summary>
        MajorVersion = 0x821b,
        /// <summary>
        /// Data returns one value, the minor version number of the OpenGL API supported by the current context.
        /// </summary>
        MinorVersion = 0x821c,
        /// <summary>
        /// data returns one value, the number of extensions supported by the GL implementation for the current context.
        /// See <see cref="GL.GetString(GLStringParams)"/>.
        /// </summary>
        NumExtensions = 0x821d
    }
}
