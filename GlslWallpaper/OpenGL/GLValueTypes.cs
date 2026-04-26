namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// OpenGL value types.
    /// </summary>
    public enum GLValueTypes : uint
    {
        /// <summary>
        /// Lists is treated as an array of unsigned bytes, each in the range 0 through 255.
        /// </summary>
        UnsignedByte = 0x1401,
        /// <summary>
        /// Lists is treated as an array of unsigned two-byte integers, each in the range 0 through 65535.
        /// </summary>
        UnsignedShort = 0x1403,
        /// <summary>
        /// Lists is treated as an array of unsigned four-byte integers.
        /// </summary>
        UnsignedInt = 0x1405,
        /// <summary>
        /// Lists is treated as an array of four-byte floating-point values.
        /// </summary>
        Float = 0x1406
    }
}
