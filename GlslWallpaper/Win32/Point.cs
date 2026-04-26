using System.Runtime.InteropServices;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// The POINT structure defines the x- and y-coordinates of a point.
    /// </summary>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/en-us/windows/win32/api/windef/ns-windef-point"/></para>
    /// <para>The POINT structure is identical to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/windef/ns-windef-pointl">POINTL</see> structure.</para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        /// <summary>
        /// Specifies the x-coordinate of the point.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Specifies the y-coordinate of the point.
        /// </summary>
        public int Y { get; set; }
    }
}
