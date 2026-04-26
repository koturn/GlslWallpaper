using System;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Mask bits for <see cref="GL.Clear(GLClearMaskBits)"/>
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glClear.xhtml"><c>glClear</c></seealso>
    /// </remarks>
    [Flags]
    public enum GLClearMaskBits : uint
    {
        /// <summary>
        /// Indicates the depth buffer.
        /// </summary>
        DepthBufferBit = 0x00000100,
        /// <summary>
        /// Indicates the stencil buffer.
        /// </summary>
        StencilBufferBit = 0x00000400,
        /// <summary>
        /// Indicates the buffers currently enabled for color writing.
        /// </summary>
        ColorBufferBit = 0x00004000
    }
}
