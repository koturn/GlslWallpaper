namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Possible values of <see cref="GL.GetString(GLStringParams)"/>
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetString.xhtml"><c>glGetString</c></seealso>
    /// </remarks>
    public enum GLStringParams : uint
    {
        /// <summary>
        /// Returns the company responsible for this GL implementation. This name does not change from release to release.
        /// </summary>
        Vendor = 0x1f00,
        /// <summary>
        /// Returns the name of the renderer.
        /// This name is typically specific to a particular configuration of a hardware platform.
        /// It does not change from release to release.
        /// </summary>
        Renderer = 0x1f01,
        /// <summary>
        /// Returns a version or release number.
        /// </summary>
        Version = 0x1f02,
        /// <summary>
        /// For <c>glGetStringi</c> only, returns the extension string supported by the implementation at index.
        /// </summary>
        Extensions = 0x1f03,
        /// <summary>
        /// Returns a version or release number for the shading language.
        /// </summary>
        ShadingLanguageVersion = 0x8b8c
    }
}
