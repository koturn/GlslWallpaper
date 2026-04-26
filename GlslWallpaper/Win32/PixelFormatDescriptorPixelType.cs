namespace GlslWallpaper.Win32
{
    /// <summary>
    /// Available value for <see cref="PixelFormatDescriptor.PixelType"/>.
    /// </summary>
    public enum PixelFormatDescriptorPixelType : byte
    {
        /// <summary>
        /// RGBA pixels. Each pixel has four components in this order: red, green, blue, and alpha.
        /// </summary>
        RGBA = 0,
        /// <summary>
        /// Color-index pixels. Each pixel uses a color
        /// </summary>
        ColorIndex = 1
    }
}
