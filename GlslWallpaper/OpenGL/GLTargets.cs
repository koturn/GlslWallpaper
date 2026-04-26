namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// OpenGL target buffer types.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindBuffer.xhtml"><c>glBindBuffer</c></seealso>
    /// </remarks>
    public enum GLTargets : uint
    {
        /// <summary>
        /// Vertex attributes.
        /// </summary>
        ArrayBuffer = 0x8892,
        /// <summary>
        /// Atomic counter storage.
        /// </summary>
        AtomicCounterBuffer = 0x92c0,
        /// <summary>
        /// Buffer copy source.
        /// </summary>
        CopyReadBuffer = 0x8f36,
        /// <summary>
        /// Buffer copy destination.
        /// </summary>
        CopyWriteBuffer = 0x8f37,
        /// <summary>
        /// Indirect compute dispatch commands.
        /// </summary>
        DispatchIndirectBuffer = 0x90ee,
        /// <summary>
        /// Indirect command arguments
        /// </summary>
        DrawIndirectBuffer = 0x8f3f,
        /// <summary>
        /// Vertex array indices.
        /// </summary>
        ElementArrayBuffer = 0x8893,
        /// <summary>
        /// Pixel read target.
        /// </summary>
        PixelPackBuffer = 0x88eb,
        /// <summary>
        /// Texture data source
        /// </summary>
        PixelUnpackBuffer = 0x88ec,
        /// <summary>
        /// Query result buffer
        /// </summary>
        QueryBuffer = 0x9192,
        /// <summary>
        /// Read-write storage for shaders
        /// </summary>
        ShaderStorageBuffer = 0x90d2,
        /// <summary>
        /// Texture data buffer
        /// </summary>
        TextureBuffer = 0x8c2a,
        /// <summary>
        ///	Transform feedback buffer
        /// </summary>
        TransformFeedbackBuffer = 0x8c8e,
        /// <summary>
        /// Uniform block storage
        /// </summary>
        UniformBuffer = 0x8a11
    }
}
