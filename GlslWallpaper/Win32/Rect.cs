using System.Runtime.InteropServices;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// The <see cref="Rect"/> structure defines a rectangle by the coordinates of its upper-left and lower-right corners.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect"/>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        /// <summary>
        /// Specifies the x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int Left { get; set; }
        /// <summary>
        /// Specifies the y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// Specifies the x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int Right { get; set; }
        /// <summary>
        /// Specifies the y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int Bottom { get; set; }

        /// <summary>
        /// Width of the <see cref="Rect"/>.
        /// </summary>
        public readonly int Width => Right - Left;
        /// <summary>
        /// Height of the <see cref="Rect"/>.
        /// </summary>
        public readonly int Height => Bottom - Top;
    }
}
