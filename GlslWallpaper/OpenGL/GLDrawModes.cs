namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Draw mode of <see cref="GL.DrawElements(GLDrawModes, int, byte[])"/>, <see cref="GL.DrawElements(GLDrawModes, int, ushort[])"/>
    /// and <see cref="GL.DrawElements(GLDrawModes, int, uint[])"/>.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDrawElements.xhtml"><c>glDrawElements</c></seealso>
    /// </remarks>
    public enum GLDrawModes : uint
    {
        /// <summary>
        /// Treats each triplet of vertices as an independent triangle.
        /// Vertices 3n−2, 3n−1, and 3n define triangle n.
        /// N/3 triangles are drawn.
        /// </summary>
        Triangles = 0x0004
    }
}
