using System;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// Flag bits for <see cref="MonitorInfo.Flags"/>
    /// </summary>
    [Flags]
    internal enum MonitorInfoFlags : uint
    {
        /// <summary>
        /// This is the primary display monitor.
        /// </summary>
        Primary = 0x00000001
    }
}
