namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// OpenGL shader types.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCreateShader.xhtml"><c>glCreateShader</c></seealso>
    /// </remarks>
    public enum GLShaderTypes : uint
    {
        /// <summary>
        /// Intended to run on the programmable fragment processor.
        /// </summary>
        FragmentShader = 0x8b30,
        /// <summary>
        /// Intended to run on the programmable vertex processor.
        /// </summary>
        VertexShader = 0x8b31,
        /// <summary>
        /// Intended to run on the programmable tessellation evaluation processor.
        /// </summary>
        GeometryShader = 0x8dd9,
        /// <summary>
        /// Intended to run on the programmable tessellation evaluation processor.
        /// </summary>
        TessEvaluationShader = 0x8e87,
        /// <summary>
        /// Intended to run on the programmable tessellation control processor.
        /// </summary>
        TessControlShader = 0x8e88,
        /// <summary>
        /// Intended to run on the programmable compute processor.
        /// </summary>
        ComputeShader = 0x91b9
    }
}
