using System.Runtime.InteropServices;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// <para>The <see cref="MonitorInfo"/> structure contains information about a display monitor.</para>
    /// <para>The <see cref="NativeWindow.SafeNativeMethods.GetMonitorInfo(nint, ref MonitorInfo)"/> function stores information
    /// into a <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfoexw">MONITORINFOEX</see> structure or
    /// a <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfo">MONITORINFO</see> structure.</para>
    /// <para>The <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfoexw">MONITORINFOEX</see> structure
    /// is a superset of the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfo">MONITORINFO</see> structure.
    /// The <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfoexw">MONITORINFOEX</see> structure adds
    /// a string member to contain a name for the display monitor.</para>
    /// </summary>
    /// <remarks>
    /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfo"/></para>
    /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfoexw"/></para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct MonitorInfo
    {
        /// <summary>
        /// <para>The size of the structure, in bytes.</para>
        /// <para>Set this member to <c>sizeof ( MONITORINFO )</c> before calling
        /// the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmonitorinfow">GetMonitorInfo</see> function.
        /// Doing so lets the function determine the type of structure you are passing to it.</para>
        /// </summary>
        private int _size;
        /// <summary>
        /// A <see cref="Rect"/> structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates.
        /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
        /// </summary>
        public Rect MonitorRect { get; }
        /// <summary>
        /// A <see cref="Rect"/> structure that specifies the work area rectangle of the display monitor, expressed in virtual-screen coordinates.
        /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
        /// </summary>
        public Rect WorkRect { get; }
        /// <summary>
        /// A set of flags that represent attributes of the display monitor.
        /// </summary>
        public MonitorInfoFlags Flags { get; }
        /// <summary>
        /// A string that specifies the device name of the monitor being used.
        /// Most applications have no use for a display monitor name, and so can save some bytes
        /// by using a <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfo">MONITORINFO</see> structure.
        /// </summary>
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName { get; }


        /// <summary>
        /// Create <see cref="MonitorInfo"/> instance with initializing <see cref="_size"/>.
        /// </summary>
        /// <returns>Created <see cref="MonitorInfo"/> instance.</returns>
        public static MonitorInfo Create()
        {
            return new MonitorInfo()
            {
                _size = Marshal.SizeOf<MonitorInfo>()
            };
        }
    }
}
