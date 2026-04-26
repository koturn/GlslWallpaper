using System.Runtime.InteropServices;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// <para></para>
    /// <para>The <see cref="WndClassEx"/> structure is similar to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-wndclassw">WNDCLASS</see> structure.
    /// There are two differences.
    /// <see cref="WndClassEx"/> includes the <see cref="Size"/> member, which specifies the size of the structure,
    /// and the <see cref="IconSmallHandle"/> member, which contains a handle to a small icon associated with the window class.</para>
    /// </summary>
    /// <remarks>
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-wndclassexw"/>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WndClassEx
    {
        /// <summary>
        /// The size, in bytes, of this structure.
        /// Set this member to <c><see cref="Marshal.SizeOf(System.Type)">Marshal.SizeOf</see>(typeof(<see cref="WndClassEx"/>))</c>.
        /// Be sure to set this member before calling the <see href="https://learn.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getclassinfoexw">GetClassInfoEx</see> function.
        /// </summary>
        public uint Size { get; set; }
        /// <summary>
        /// The class style(s).
        /// This member can be any combination of the <see cref="WindowClassStyleFlags"/>.
        /// </summary>
        public WindowClassStyleFlags Style { get; set; }
        /// <summary>
        /// A pointer to the window procedure.
        /// You must use the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callwindowprocw">CallWindowProc</see> function to call the window procedure.
        /// For more information, see <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-wndproc">WindowProc</see>.
        /// </summary>
        public nint WndProcPointer { get; set; }
        /// <summary>
        /// The number of extra bytes to allocate following the window-class structure.
        /// The system initializes the bytes to zero.
        /// </summary>
        public int ClsExtra { get; set; }
        /// <summary>
        /// The number of extra bytes to allocate following the window instance.
        /// The system initializes the bytes to zero.
        /// If an application uses <see cref="WndClassEx"/> to register a dialog box created by using the CLASS directive in the resource file,
        /// it must set this member to DLGWINDOWEXTRA.
        /// </summary>
        public int WndExtra { get; set; }
        /// <summary>
        /// A handle to the instance that contains the window procedure for the class.
        /// </summary>
        public nint InstanceHandle { get; set; }
        /// <summary>
        /// A handle to the class icon.
        /// This member must be a handle to an icon resource.
        /// If this member is ZERO, the system provides a default icon.
        /// </summary>
        public nint IconHandle { get; set; }
        /// <summary>
        /// A handle to the class cursor.
        /// This member must be a handle to a cursor resource.
        /// If this member is ZERO, an application must explicitly set the cursor shape whenever the mouse moves into the application's window.
        /// </summary>
        public nint CusorHandle { get; set; }
        /// <summary>
        /// <para>A handle to the class background brush.
        /// This member can be a handle to the brush to be used for painting the background, or it can be a color value.
        /// A color value must be one of the following standard system colors (the value 1 must be added to the chosen color).
        /// If a color value is given, you must convert it to one of the following HBRUSH types:</para>
        /// <para>
        /// <list type="bullet">
        ///   <item>COLOR_ACTIVEBORDER</item>
        ///   <item>COLOR_ACTIVECAPTION</item>
        ///   <item>COLOR_APPWORKSPACE</item>
        ///   <item>COLOR_BACKGROUND</item>
        ///   <item>COLOR_BTNFACE</item>
        ///   <item>COLOR_BTNSHADOW</item>
        ///   <item>COLOR_BTNTEXT</item>
        ///   <item>COLOR_CAPTIONTEXT</item>
        ///   <item>COLOR_GRAYTEXT</item>
        ///   <item>COLOR_HIGHLIGHT</item>
        ///   <item>COLOR_HIGHLIGHTTEXT</item>
        ///   <item>COLOR_INACTIVEBORDER</item>
        ///   <item>COLOR_INACTIVECAPTION</item>
        ///   <item>COLOR_MENU</item>
        ///   <item>COLOR_MENUTEXT</item>
        ///   <item>COLOR_SCROLLBAR</item>
        ///   <item>COLOR_WINDOW</item>
        ///   <item>COLOR_WINDOWFRAME</item>
        ///   <item>COLOR_WINDOWTEXT</item>
        /// </list>
        /// </para>
        /// <para>The system automatically deletes class background brushes when the class is unregistered
        /// by using <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unregisterclassw">UnregisterClass</see>.
        /// An application should not delete these brushes.</para>
        /// <para>When this member is NULL, an application must paint its own background whenever it is requested to paint in its client area.
        /// To determine whether the background must be painted,
        /// an application can either process the <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/wm-erasebkgnd">WM_ERASEBKGND</see> message
        /// or test the fErase member of the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-paintstruct">PAINTSTRUCT</see> structure filled
        /// by the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-beginpaint">BeginPaint</see> function.</para>
        /// </summary>
        public nint BackgroudBrushHandle { get; set; }
        /// <summary>
        /// Pointer to a null-terminated character string that specifies the resource name of the class menu, as the name appears in the resource file.
        /// If you use an integer to identify the menu, use the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-makeintresourcew">MAKEINTRESOURCE</see> macro.
        /// If this member is NULL, windows belonging to this class have no default menu.
        /// </summary>
        public string lpszMenuName { get; set; }
        /// <summary>
        /// <para>A pointer to a null-terminated string or is an atom.
        /// If this parameter is an atom, it must be a class atom created by a previous call
        /// to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassw">RegisterClass</see>
        /// or <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassexw">RegisterClassEx</see> function.
        /// The atom must be in the low-order word of <see cref="lpszClassName"/>; the high-order word must be zero.</para>
        /// <para>If <see cref="lpszClassName"/> is a string, it specifies the window class name.
        /// The class name can be any name registered with <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassw">RegisterClass</see>
        /// or <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassexw">RegisterClassEx</see>, or any of the predefined control-class names.</para>
        /// <para>The maximum length for <see cref="lpszClassName"/> is 256.
        /// If <see cref="lpszClassName"/> is greater than the maximum length, the <see cref="NativeWindow.SafeNativeMethods.RegisterClassEx"/> function will fail.</para>
        /// </summary>
        public string lpszClassName { get; set; }
        /// <summary>
        /// A handle to a small icon that is associated with the window class.
        /// If this member is ZERO, the system searches the icon resource specified by the <see cref="IconHandle"/> member for an icon of the appropriate size to use as the small icon.
        /// </summary>
        public nint IconSmallHandle { get; set; }
    }
}
