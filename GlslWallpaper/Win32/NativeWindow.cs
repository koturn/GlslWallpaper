#if NET7_0_OR_GREATER
#    define SUPPORT_LIBRARY_IMPORT
#endif  // NET7_0_OR_GREATER

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using GlslWallpaper.Internals;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// Provides wrapper method about native Win32 windows.
    /// </summary>
#if NET7_0_OR_GREATER
    internal static partial class NativeWindow
#else
    internal static class NativeWindow
#endif  // NET7_0_OR_GREATER
    {
        /// <summary>
        /// Default window procedure.
        /// </summary>
        private readonly static WndProcFunc _wndProcDefault = WndProcDefault;

        /// <summary>
        /// Get the window handle of "WorkerW".
        /// </summary>
        /// <returns>the window handle of "WorkerW".</returns>
        public static nint GetWorkerW()
        {
            var progman = SafeNativeMethods.FindWindow("Progman", null);
            SafeNativeMethods.SendMessageTimeout(progman, 0x052C, default, default, SendMessageTimeoutFlags.AbortIfHung, 1000, out _);

            var hWorkerW = default(nint);
            unsafe
            {
                SafeNativeMethods.EnumWindows((top, phWorkerW) =>
                {
                    var shell = SafeNativeMethods.FindWindowEx(top, default, "SHELLDLL_DefView", null);
                    if (shell != default)
                    {
                        *(nint*)phWorkerW = SafeNativeMethods.FindWindowEx(default, top, "WorkerW", null);
                    }
                    return true;
                }, (nint)(&hWorkerW));
            }

            return hWorkerW;
        }

        public static List<MonitorInfo> GetAllMonitorInfo()
        {
            var monitorInfoList = new List<MonitorInfo>();

            var ret = SafeNativeMethods.EnumDisplayMonitors(default, default, (hMon, hdc, ref rect, p) => {
                var mi = MonitorInfo.Create();
                if (!SafeNativeMethods.GetMonitorInfo(hMon, ref mi))
                {
                    return false;
                }

                monitorInfoList.Add(mi);

                return true;
            }, default);

            return monitorInfoList;
        }

        public static DeviceContextHandle GetDC(NativeWindowHandle windowHandle)
        {
            return new DeviceContextHandle(windowHandle, GetDC(windowHandle.DangerousGetHandle()));
        }

        public static nint GetDC(nint hWnd)
        {
            var hDc = SafeNativeMethods.GetDC(hWnd);
            if (hDc == default)
            {
                ThrowHelper.ThrowLastWin32Exception(nameof(SafeNativeMethods.GetDC) + "() failed");
            }
            return hDc;
        }

        public static void ReleaseDC(nint hWnd, nint hDc)
        {
            var result = SafeNativeMethods.ReleaseDC(hWnd, hDc);
            if (!result)
            {
                ThrowHelper.ThrowLastWin32Exception(nameof(SafeNativeMethods.ReleaseDC) + "() failed");
            }
        }

        public static ClassAtom RegisterClass(string className)
        {
            return RegisterClass(className, _wndProcDefault);
        }

        public static ClassAtom RegisterClass(string className, WndProcFunc wndProc)
        {
            var wc = new WndClassEx
            {
                Size = (uint)Marshal.SizeOf<WndClassEx>(),
                Style = WindowClassStyleFlags.VRedraw | WindowClassStyleFlags.HRedraw,
                WndProcPointer = Marshal.GetFunctionPointerForDelegate(wndProc),
                ClsExtra = 0,
                WndExtra = 0,
                InstanceHandle = default,
                CusorHandle = default,
                BackgroudBrushHandle = default,
                lpszClassName = className
            };

            var atom = SafeNativeMethods.RegisterClassEx(ref wc);
            if (atom == 0)
            {
                ThrowHelper.ThrowLastWin32Exception(nameof(SafeNativeMethods.RegisterClassEx) + "() failed");
            }

            return new ClassAtom(atom, wc.InstanceHandle);
        }

        public static NativeWindowHandle CreateMonitorWindow(string name, Rect rect, nint parent = default)
        {
            unsafe
            {
                fixed (char* pName = name)
                {
                    var hWnd = SafeNativeMethods.CreateWindowEx(
                        ExtendedWindowStyleFlags.Left,
                        (nint)pName,
                        "",
                        WindowStyleFlags.Child | WindowStyleFlags.Visible,
                        rect.Left,
                        rect.Top,
                        rect.Width,
                        rect.Height,
                        parent,
                        default,
                        default,
                        default);
                    if (hWnd == default)
                    {
                        ThrowHelper.ThrowLastWin32Exception(nameof(SafeNativeMethods.CreateWindowEx) + "() failed");
                    }
                    return new NativeWindowHandle(hWnd);
                }
            }
        }

        public static NativeWindowHandle CreateMonitorWindow(ClassAtom atom, Rect rect, nint parent = default)
        {
            var hWnd = SafeNativeMethods.CreateWindowEx(
                ExtendedWindowStyleFlags.Left,
                atom.Handle,
                "",
                WindowStyleFlags.Child | WindowStyleFlags.Visible,
                rect.Left,
                rect.Top,
                rect.Width,
                rect.Height,
                parent,
                default,
                default,
                default);
            if (hWnd == default)
            {
                ThrowHelper.ThrowLastWin32Exception(nameof(SafeNativeMethods.CreateWindowEx) + "() failed");
            }
            return new NativeWindowHandle(hWnd);
        }



        public static void DestroyWindow(nint hWnd)
        {
            var result = SafeNativeMethods.DestroyWindow(hWnd);
            if (!result)
            {
                ThrowHelper.ThrowLastWin32Exception(nameof(SafeNativeMethods.DestroyWindow) + "() failed");
            }
        }

        public static Point GetCursorPos()
        {
            var result = SafeNativeMethods.GetCursorPos(out var point);
            if (!result)
            {
                ThrowHelper.ThrowLastWin32Exception(nameof(SafeNativeMethods.GetCursorPos) + "() failed");
            }
            return point;
        }

        private static nint WndProcDefault(nint hWnd, uint msg, nint wParam, nint lParam)
        {
            switch (msg)
            {
                case 0x0002: // WM_DESTROY
                    Environment.Exit(0);
                    break;
            }
            return SafeNativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
        }


        /// <summary>
        /// An application-defined callback function used with the <see cref="SafeNativeMethods.EnumDisplayMonitors(nint, nint, MonitorEnumFunc, nint)"/> function.
        /// The MONITORENUMPROC type defines a pointer to this callback function.
        /// MonitorEnumProc is a placeholder for the application-defined function name.
        /// </summary>
        /// <param name="hMonitor">A handle to the display monitor. This value will always be non-ZERO. This parameter is typically named hMonitor.</param>
        /// <param name="hdcMonitor">
        /// <para>A handle to a device context. This parameter is typically named hdcMonitor.</para>
        /// <para>The device context has color attributes that are appropriate for the display monitor identified by hMonitor.
        /// The clipping area of the device context is set to the intersection of the visible region of the device context identified
        /// by the hdc parameter of <see cref="SafeNativeMethods.EnumDisplayMonitors(nint, nint, MonitorEnumFunc, nint)"/>,
        /// the rectangle pointed to by the lprcClip parameter of <see cref="SafeNativeMethods.EnumDisplayMonitors(nint, nint, MonitorEnumFunc, nint)"/>, and the display monitor rectangle.</para>
        /// <para>This value is ZERO if the hdc parameter of <see cref="SafeNativeMethods.EnumDisplayMonitors(nint, nint, MonitorEnumFunc, nint)"/> was ZERO.</para>
        /// </param>
        /// <param name="lprcMonitor">
        /// <para>A pointer to a <see cref="Rect"/> structure. This parameter is typically named lprcMonitor.</para>
        /// <para>If <paramref name="hdcMonitor"/> is non-ZERO, this rectangle is the intersection of the clipping area of the device context identified
        /// by <paramref name="hdcMonitor"/> and the display monitor rectangle.
        /// The rectangle coordinates are device-context coordinates.</para>
        /// <para>If <paramref name="hdcMonitor"/> is null, this rectangle is the display monitor rectangle.
        /// The rectangle coordinates are virtual-screen coordinates.</para>
        /// </param>
        /// <param name="dwData">Application-defined data that <see cref="SafeNativeMethods.EnumDisplayMonitors(nint, nint, MonitorEnumFunc, nint)"/> passes directly to the enumeration function.
        /// This parameter is typically named dwData.</param>
        /// <returns>
        /// <para>To continue the enumeration, return true.</para>
        /// <para>To stop the enumeration, return false.</para>
        /// </returns>
        /// <remarks>
        /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-monitorenumproc"/></para>
        /// <para>You can use the <see cref="SafeNativeMethods.EnumDisplayMonitors(nint, nint, MonitorEnumFunc, nint)"/> function
        /// to enumerate the set of display monitors that intersect the visible region of a specified device context and, optionally, a clipping rectangle.
        /// To do this, set the hdc parameter to a non-ZERO value, and set the lprcClip parameter as needed.</para>
        /// <para>You can also use the <see cref="SafeNativeMethods.EnumDisplayMonitors(nint, nint, MonitorEnumFunc, nint)"/> function
        /// to enumerate one or more of the display monitors on the desktop, without supplying a device context.
        /// To do this, set the hdc parameter of <see cref="SafeNativeMethods.EnumDisplayMonitors(nint, nint, MonitorEnumFunc, nint)"/> to ZERO and set the lprcClip parameter as needed.</para>
        /// <para>In all cases, <see cref="SafeNativeMethods.EnumDisplayMonitors(nint, nint, MonitorEnumFunc, nint)"/> calls a specified <see cref="MonitorEnumFunc"/> function once
        /// for each display monitor in the calculated enumeration set.
        /// The <see cref="MonitorEnumFunc"/> function always receives a handle to the display monitor.</para>
        /// <para>If the hdc parameter of <see cref="SafeNativeMethods.EnumDisplayMonitors(nint, nint, MonitorEnumFunc, nint)"/> is non-ZERO,
        /// the <see cref="MonitorEnumFunc"/> function also receives a handle to a device context whose color format is appropriate for the display monitor.
        /// You can then paint into the device context in a manner that is optimal for the display monitor.</para>
        /// </remarks>
        internal delegate bool MonitorEnumFunc(nint hMonitor, nint hdcMonitor, ref Rect lprcMonitor, nint dwData);

        /// <summary>
        /// An application-defined callback function used with the <see cref="SafeNativeMethods.EnumWindows(EnumWindowsFunc, nint)"/>
        /// or <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdesktopwindows">EnumDesktopWindows</see> function.
        /// It receives top-level window handles.
        /// The WNDENUMPROC type defines a pointer to this callback function.
        /// EnumWindowsProc is a placeholder for the application-defined function name.
        /// </summary>
        /// <param name="hWnd">A handle to a top-level window.</param>
        /// <param name="lParam">The application-defined value given in <see cref="SafeNativeMethods.EnumWindows(EnumWindowsFunc, nint)"/>
        /// or <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdesktopwindows">EnumDesktopWindows</see>.</param>
        /// <returns>
        /// To continue enumeration, the callback function must return true; to stop enumeration, it must return false.
        /// </returns>
        /// <remarks>
        /// <para><seealso href="https://learn.microsoft.com/ja-jp/previous-versions/windows/desktop/legacy/ms633498(v=vs.85)"/></para>
        /// <para>An application must register this callback function by passing its address to <see cref="SafeNativeMethods.EnumWindows(EnumWindowsFunc, nint)"/>
        /// or <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdesktopwindows">EnumDesktopWindows</see>.</para>
        /// </remarks>
        internal delegate bool EnumWindowsFunc(nint hWnd, nint lParam);


        /// <summary>
        /// Provides native methods.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
#if NET7_0_OR_GREATER
        internal static partial class SafeNativeMethods
#else
        internal static class SafeNativeMethods
#endif  // NET7_0_OR_GREATER
        {
            /// <summary>
            /// Registers a window class for subsequent use in calls to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-createwindoww">CreateWindow</see>
            /// or <see cref="CreateWindowEx(ExtendedWindowStyleFlags, nint, string, WindowStyleFlags, int, int, int, int, nint, nint, nint, nint)"/> function.
            /// </summary>
            /// <param name="wndClass">A reference of a <see cref="WndClassEx"/> structure.
            /// You must fill the structure with the appropriate class attributes before passing it to the function.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is a class atom that uniquely identifies the class being registered.
            /// This atom can only be used by the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-createwindoww">CreateWindow</see>,
            /// <see cref="CreateWindowEx(ExtendedWindowStyleFlags, nint, string, WindowStyleFlags, int, int, int, int, nint, nint, nint, nint)"/>,
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclassinfow">GetClassInfo</see>,
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclassinfoexw">GetClassInfoEx</see>,
            /// <see cref="FindWindow(string?, string?)"/>,
            /// <see cref="FindWindowEx(nint, nint, string?, string?)"/>,
            /// and <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unregisterclassw">UnregisterClass</see> functions
            /// and the IActiveIMMap::FilterClientWindows method.</para>
            /// <para>If the function fails, the return value is zero.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassexw"/></para>
            /// <para>If you register the window class by using RegisterClassExA,
            /// the application tells the system that the windows of the created class expect messages with text or character parameters
            /// to use the ANSI character set; if you register it by using RegisterClassExW, the application requests that the system pass text parameters of messages as Unicode.
            /// The <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindowunicode">IsWindowUnicode</see> function enables applications
            /// to query the nature of each window.
            /// For more information on ANSI and Unicode functions,
            /// see <see href="https://learn.microsoft.com/en-us/windows/win32/intl/conventions-for-function-prototypes">Conventions for Function Prototypes</see>.</para>
            /// <para>All window classes that an application registers are unregistered when it terminates.</para>
            /// <para>No window classes registered by a DLL are unregistered when the DLL is unloaded.
            /// A DLL must explicitly unregister its classes when it is unloaded.</para>
            /// </remarks>
            [DllImport("user32.dll", EntryPoint = nameof(RegisterClassEx) + "W", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern ushort RegisterClassEx(ref WndClassEx wndClass);

            /// <summary>
            /// Unregisters a window class, freeing the memory required for the class.
            /// </summary>
            /// <param name="atomOrClassNamePtr">A null-terminated string or a class atom.
            /// If lpClassName is a string, it specifies the window class name.
            /// This class name must have been registered by a previous call
            /// to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassw">RegisterClass</see> or <see cref="RegisterClassEx(ref WndClassEx)"/> function.
            /// System classes, such as dialog box controls, cannot be unregistered.
            /// If this parameter is an atom, it must be a class atom created by a previous call
            /// to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassw">RegisterClass</see> or <see cref="RegisterClassEx(ref WndClassEx)"/> function.
            /// The atom must be in the low-order word of <paramref name="atomOrClassNamePtr"/>; the high-order word must be zero.</param>
            /// <param name="hInstance">A handle to the instance of the module that created the class.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is true.</para>
            /// <para>If the class could not be found or if a window still exists that was created with the class, the return value is false.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unregisterclassw"/></para>
            /// <para>Before calling this function, an application must destroy all windows created with the specified class.</para>
            /// <para>All window classes that an application registers are unregistered when it terminates.</para>
            /// <para>Class atoms are special atoms returned only by <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassw">RegisterClass</see>
            /// and <see cref="RegisterClassEx(ref WndClassEx)"/>.</para>
            /// <para>No window classes registered by a DLL are unregistered when the .dll is unloaded.</para>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(UnregisterClass) + "W", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool UnregisterClass(nint atomOrClassNamePtr, nint hInstance);
#else
            [DllImport("user32.dll", EntryPoint = nameof(UnregisterClass) + "W", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnregisterClass(nint atomOrClassNamePtr, nint hInstance);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// <para>Calls the default window procedure to provide default processing for any window messages that an application does not process.
            /// This function ensures that every message is processed.</para>
            /// <para><see cref="DefWindowProc(nint, uint, nint, nint)"/> is called with the same parameters received by the window procedure.</para>
            /// </summary>
            /// <param name="hWnd">A handle to the window procedure that received the message.</param>
            /// <param name="uMsg">The message.</param>
            /// <param name="wParam">Additional message information.
            /// The content of this parameter depends on the value of the <paramref name="uMsg"/></param>
            /// <param name="lParam">Additional message information.
            /// The content of this parameter depends on the value of the <paramref name="uMsg"/> parameter.</param>
            /// <returns>The return value is the result of the message processing and depends on the message.</returns>
            /// <remarks>
            /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-defwindowprocw"/>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(DefWindowProc) + "W", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
            public static partial nint DefWindowProc(nint hWnd, uint uMsg, nint wParam, nint lParam);
#else
            [DllImport("user32.dll", EntryPoint = nameof(DefWindowProc) + "W", ExactSpelling = true , SetLastError = true)]
            public static extern nint DefWindowProc(nint hWnd, uint uMsg, nint wParam, nint lParam);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// Creates an overlapped, pop-up, or child window with an extended window style; otherwise,
            /// this function is identical to the <see href="https://learn.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-createwindoww">CreateWindow</see> function.
            /// For more information about creating a window and for full descriptions of the other parameters
            /// of <see cref="CreateWindowEx(ExtendedWindowStyleFlags, nint, string, WindowStyleFlags, int, int, int, int, nint, nint, nint, nint)"/>, see CreateWindow.
            /// </summary>
            /// <param name="exStyle">The extended window style of the window being created.
            /// For a list of possible values, see <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles">Extended Window Styles</see>.</param>
            /// <param name="atomOrClassNamePtr">
            /// <para>A null-terminated string or a class atom.</para>
            /// <para>If a null-terminated string, it specifies the window class name.
            /// The class name can be any name registered with the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassw">RegisterClass</see>
            /// or <see cref="RegisterClassEx(ref WndClassEx)"/> function, provided that the module that registers the class is also the module that creates the window.
            /// The class name can also be any of the predefined <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/about-window-classes">system class</see> names.</para>
            /// <para>If a class atom created by a previous call to RegisterClass or RegisterClassEx,
            /// it must be converted using the macro <see href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-makeintatom">MAKEINTATOM</see>.
            /// (The atom must be in the low-order word of lpClassName; the high-order word must be zero.)</para>
            /// </param>
            /// <param name="windowName">The window name.
            /// If the window style specifies a title bar, the window title pointed to by <paramref name="windowName"/> is displayed in the title bar.
            /// When using <see cref="CreateWindowEx(ExtendedWindowStyleFlags, nint, string, WindowStyleFlags, int, int, int, int, nint, nint, nint, nint)"/> to create controls,
            /// such as buttons, check boxes, and static controls, use <paramref name="windowName"/> to specify the text of the control.
            /// When creating a static control with the SS_ICON style, use <paramref name="windowName"/> to specify the icon name or identifier.
            /// To specify an identifier, use the syntax "#num".</param>
            /// <param name="style">The style of the window being created.
            /// This parameter can be a combination of the <see cref="WindowStyleFlags"/> values, plus the control styles indicated in the Remarks section.</param>
            /// <param name="x">The initial horizontal position of the window.
            /// For an overlapped or pop-up window, the x parameter is the initial x-coordinate of the window's upper-left corner, in screen coordinates.
            /// For a child window, x is the x-coordinate of the upper-left corner of the window relative to the upper-left corner of the parent window's client area.
            /// If this parameter is set to CW_USEDEFAULT, the system selects the default position for the window's upper-left corner and ignores the y parameter.
            /// CW_USEDEFAULT is valid only for overlapped windows; if it is specified for a pop-up or child window, the x and y parameters are set to zero.</param>
            /// <param name="y">
            /// <para>The initial vertical position of the window.
            /// For an overlapped or pop-up window, the y parameter is the initial y-coordinate of the window's upper-left corner, in screen coordinates.
            /// For a child window, y is the initial y-coordinate of the upper-left corner of the child window relative to the upper-left corner of the parent window's client area.
            /// For a list box, y is the initial y-coordinate of the upper-left corner of the list box's client area relative to the upper-left corner of the parent window's client area.</para>
            /// <para>If an overlapped window is created with the <see cref="WindowStyleFlags.Visible"/> style bit set and the x parameter is set to CW_USEDEFAULT,
            /// then the y parameter determines how the window is shown.
            /// If the y parameter is CW_USEDEFAULT, then the window manager calls <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow">ShowWindow</see>
            /// with the SW_SHOW flag after the window has been created.
            /// If the y parameter is some other value, then the window manager calls ShowWindow with that value as the nCmdShow parameter.</para>
            /// </param>
            /// <param name="width">The width, in device units, of the window.
            /// For overlapped windows, nWidth is either the window's width, in screen coordinates, or CW_USEDEFAULT.
            /// If nWidth is CW_USEDEFAULT, the system selects a default width and height for the window;
            /// the default width extends from the initial x-coordinate to the right edge of the screen,
            /// and the default height extends from the initial y-coordinate to the top of the icon area.
            /// CW_USEDEFAULT is valid only for overlapped windows; if CW_USEDEFAULT is specified for a pop-up or child window, nWidth and nHeight are set to zero.</param>
            /// <param name="height">The height, in device units, of the window.
            /// For overlapped windows, nHeight is the window's height, in screen coordinates.
            /// If nWidth is set to CW_USEDEFAULT, the system ignores nHeight.</param>
            /// <param name="hWndParent">
            /// <para>A handle to the parent or owner window of the window being created.
            /// To create a child window or an owned window, supply a valid window handle.
            /// This parameter is optional for pop-up windows.</para>
            /// <para>To create a <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/window-features">message-only window</see>,
            /// supply HWND_MESSAGE or a handle to an existing message-only window.</para>
            /// </param>
            /// <param name="hMenu">A handle to a menu, or specifies a child-window identifier depending on the window style.
            /// For an overlapped or pop-up window, hMenu identifies the menu to be used with the window; it can be ZERO if the class menu is to be used.
            /// For a child window, hMenu specifies the child-window identifier, an integer value used by a dialog box control to notify its parent about events.
            /// The application determines the child-window identifier; it must be unique for all child windows with the same parent window.</param>
            /// <param name="hInstance">A handle to the instance of the module to be associated with the window.</param>
            /// <param name="lpParam">
            /// <para>A pointer to a value to be passed to the window through the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-createstructw">CREATESTRUCT</see> structure
            /// (lpCreateParams member) pointed to by the lParam param of the <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/wm-create">WM_CREATE</see> message.
            /// This message is sent to the created window by this function before it returns.</para>
            /// <para>If an application calls CreateWindow to create a MDI client window,
            /// <paramref name="lpParam"/> should point to a <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-clientcreatestruct">CLIENTCREATESTRUCT</see> structure.
            /// If an MDI client window calls CreateWindow to create an MDI child window,
            /// <paramref name="lpParam"/> should point to a <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mdicreatestructw">MDICREATESTRUCT</see> structure.
            /// <paramref name="lpParam"/> may be ZERO if no additional data is needed.</para>
            /// </param>
            /// <returns>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-createwindowexw"/></para>
            /// <para>If the function succeeds, the return value is a handle to the new window.</para>
            /// <para>If the function fails, the return value is ZERO.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// <para>This function typically fails for one of the following reasons:</para>
            /// <para>
            /// <list type="bullet">
            ///   <item>an invalid parameter value</item>
            ///   <item>the system class was registered by a different module</item>
            ///   <item>The WH_CBT hook is installed and returns a failure code</item>
            ///   <item>if one of the controls in the dialog template is not registered,
            ///   or its window window procedure fails <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/wm-create">WM_CREATE</see>
            ///   or <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/wm-nccreate">WM_NCCREATE</see></item>
            /// </list>
            /// </para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-createwindowexw"/></para>
            /// <para>Before returning, <see cref="CreateWindowEx(ExtendedWindowStyleFlags, nint, string, WindowStyleFlags, int, int, int, int, nint, nint, nint, nint)"/> sends
            /// a <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/wm-create">WM_CREATE</see> message to the window procedure.
            /// For overlapped, pop-up, and child windows, <see cref="CreateWindowEx(ExtendedWindowStyleFlags, nint, string, WindowStyleFlags, int, int, int, int, nint, nint, nint, nint)"/> sends WM_CREATE,
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/wm-getminmaxinfo">WM_GETMINMAXINFO</see>,
            /// and <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/wm-nccreate">WM_NCCREATE</see> messages to the window.
            /// The lParam parameter of the WM_CREATE message contains a pointer
            /// to a <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-createstructw">CREATESTRUCT</see> structure.
            /// If the <see cref="WindowStyleFlags.Visible"/> style is specified,
            /// <see cref="CreateWindowEx(ExtendedWindowStyleFlags, nint, string, WindowStyleFlags, int, int, int, int, nint, nint, nint, nint)"/> sends the window
            /// all the messages required to activate and show the window.</para>
            /// <para>If the created window is a child window, its default position is at the bottom of the Z-order.
            /// If the created window is a top-level window, its default position is at the top of the Z-order (but beneath all topmost windows unless the created window is itself topmost).</para>
            /// <para>For information on controlling whether the Taskbar displays a button for the created window,
            /// see <see href="https://learn.microsoft.com/en-us/windows/win32/shell/taskbar">Managing Taskbar Buttons</see>.</para>
            /// <para>For information on removing a window, see the <see cref="DestroyWindow(nint)"/> function.</para>
            /// <para>The following predefined system classes can be specified in the <paramref name="atomOrClassNamePtr"/> parameter.
            /// Note the corresponding control styles you can use in the <paramref name="style"/> parameter.</para>
            /// <para>
            /// <list type="table">
            ///   <listheader>
            ///     <term>BUTTON</term>
            ///     <description>
            ///       <para>Designates a small rectangular child window that represents a button the user can click to turn it on or off.
            ///       Button controls can be used alone or in groups, and they can either be labeled or appear without text.
            ///       Button controls typically change appearance when the user clicks them.
            ///       For more information, see <see href="https://learn.microsoft.com/en-us/windows/win32/controls/buttons">Buttons</see>.</para>
            ///       <para>For a table of the button styles you can specify in the dwStyle parameter,
            ///       see <see href="https://learn.microsoft.com/en-us/windows/win32/controls/button-styles">Button Styles</see>.</para>
            ///     </description>
            ///   </listheader>
            ///   <listheader>
            ///     <term>COMBOBOX</term>
            ///     <description>
            ///       <para>Designates a control consisting of a list box and a selection field similar to an edit control.
            ///       When using this style, an application should either display the list box at all times or enable a drop-down list box.
            ///       If the list box is visible, typing characters into the selection field highlights the first list box entry that matches the characters typed.
            ///       Conversely, selecting an item in the list box displays the selected text in the selection field.
            ///       For more information, see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/combo-boxes">Combo Boxes</see>.</para>
            ///       <para>For a table of the combo box styles you can specify in the dwStyle parameter,
            ///       see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/combo-box-styles">Combo Box Styles</see>.</para>
            ///     </description>
            ///   </listheader>
            ///   <listheader>
            ///     <term>EDIT</term>
            ///     <description>
            ///       <para>Designates a rectangular child window into which the user can type text from the keyboard.
            ///       The user selects the control and gives it the keyboard focus by clicking it or moving to it by pressing the TAB key.
            ///       The user can type text when the edit control displays a flashing caret; use the mouse to move the cursor, select characters to be replaced,
            ///       or position the cursor for inserting characters; or use the key to delete characters.
            ///       For more information, see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/edit-controls">Edit Controls</see>.</para>
            ///       <para>For a table of the edit control styles you can specify in the dwStyle parameter,
            ///       see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/edit-control-styles">Edit Control Styles</see>.</para>
            ///     </description>
            ///   </listheader>
            ///   <listheader>
            ///     <term>LISTBOX</term>
            ///     <description>
            ///       <para>Designates a list of character strings.
            ///       Specify this control whenever an application must present a list of names, such as filenames, from which the user can choose.
            ///       The user can select a string by clicking it.
            ///       A selected string is highlighted, and a notification message is passed to the parent window.
            ///       For more information, see <see href="https://learn.microsoft.com/en-us/windows/desktop/uxguide/ctrl-list-boxes">List Boxes</see>.</para>
            ///       <para>For a table of the list box styles you can specify in the dwStyle parameter,
            ///       see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/list-box-styles">List Box Styles</see>.</para>
            ///     </description>
            ///   </listheader>
            ///   <listheader>
            ///     <term>MDICLIENT</term>
            ///     <description>Designates an MDI client window.
            ///     This window receives messages that control the MDI application's child windows.
            ///     The recommended style bits are <see cref="WindowStyleFlags.ClipChildren"/> and <see cref="WindowStyleFlags.Child"/>.
            ///     Specify the <see cref="WindowStyleFlags.HScroll"/> and <see cref="WindowStyleFlags.VScroll"/> styles
            ///     to create an MDI client window that allows the user to scroll MDI child windows into view.
            ///     For more information, see <see href="https://learn.microsoft.com/en-us/windows/desktop/winmsg/multiple-document-interface">Multiple Document Interface</see>.
            ///     </description>
            ///   </listheader>
            ///   <listheader>
            ///     <term>RichEdit</term>
            ///     <description>
            ///       <para>Designates a Microsoft Rich Edit 1.
            ///       0 control.
            ///       This window lets the user view and edit text with character and paragraph formatting, and can include embedded Component Object Model (COM) objects.
            ///       For more information, see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/rich-edit-controls">Rich Edit Controls</see>.</para>
            ///       <para>For a table of the rich edit control styles you can specify in the dwStyle parameter,
            ///       see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/rich-edit-control-styles">Rich Edit Control Styles</see>.</para>
            ///     </description>
            ///   </listheader>
            ///   <listheader>
            ///     <term>RICHEDIT_CLASS</term>
            ///     <description>
            ///       <para>Designates a Microsoft Rich Edit 2.
            ///       0 control.
            ///       This controls let the user view and edit text with character and paragraph formatting, and can include embedded COM objects.
            ///       For more information, see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/rich-edit-controls">Rich Edit Controls</see>.</para>
            ///       <para>For a table of the rich edit control styles you can specify in the dwStyle parameter,
            ///       see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/rich-edit-control-styles">Rich Edit Control Styles</see>.</para>
            ///     </description>
            ///   </listheader>
            ///   <listheader>
            ///     <term>SCROLLBAR</term>
            ///     <description>
            ///       <para>Designates a rectangle that contains a scroll box and has direction arrows at both ends.
            ///       The scroll bar sends a notification message to its parent window whenever the user clicks the control.
            ///       The parent window is responsible for updating the position of the scroll box, if necessary.
            ///       For more information, see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/scroll-bars">Scroll Bars</see>.</para>
            ///       <para>For a table of the scroll bar control styles you can specify in the dwStyle parameter,
            ///       see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/scroll-bar-control-styles">Scroll Bar Control Styles</see>.</para>
            ///     </description>
            ///   </listheader>
            ///   <listheader>
            ///     <term>STATIC</term>
            ///     <description>
            ///       <para>Designates a simple text field,
            ///       box, or rectangle used to label, box, or separate other controls.
            ///       Static controls take no input and provide no output.
            ///       For more information, see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/static-controls">Static Controls</see>.</para>
            ///       <para>For a table of the static control styles you can specify in the dwStyle parameter,
            ///       see <see href="https://learn.microsoft.com/en-us/windows/desktop/Controls/static-control-styles">Static Control Styles</see>.</para>
            ///     </description>
            ///   </listheader>
            /// </list>
            /// </para>
            /// <para>The <see cref="ExtendedWindowStyleFlags.NoActivate"/> value for dwExStyle prevents foreground activation by the system.
            /// To prevent queue activation when the user clicks on the window,
            /// you must process the <see href="https://learn.microsoft.com/en-us/windows/win32/inputdev/wm-mouseactivate">WM_MOUSEACTIVATE</see> message appropriately.
            /// To bring the window to the foreground or to activate it programmatically,
            /// use <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setforegroundwindow">SetForegroundWindow</see>
            /// or <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setactivewindow">SetActiveWindow</see>.
            /// Returning FALSE to <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/wm-ncactivate">WM_NCACTIVATE</see> prevents the window from losing queue activation.
            /// However, the return value is ignored at activation time.</para>
            /// <para>With <see cref="ExtendedWindowStyleFlags.Composited"/> set, all descendants of a window get bottom-to-top painting order using double-buffering.
            /// Bottom-to-top painting order allows a descendent window to have translucency (alpha) and transparency (color-key) effects,
            /// but only if the descendent window also has the <see cref="ExtendedWindowStyleFlags.Transparent"/> bit set.
            /// Double-buffering allows the window and its descendents to be painted without flicker.</para>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(CreateWindowEx) + "W", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
            public static partial nint CreateWindowEx(ExtendedWindowStyleFlags exStyle, nint atomOrClassNamePtr, string windowName, WindowStyleFlags style, int x, int y, int width, int height, nint hWndParent, nint hMenu, nint hInstance, nint lpParam);
#else
            [DllImport("user32.dll", EntryPoint = "CreateWindowExW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern nint CreateWindowEx(ExtendedWindowStyleFlags exStyle, nint atomOrClassNamePtr, string windowName, WindowStyleFlags style, int x, int y, int width, int height, nint hWndParent, nint hMenu, nint hInstance, nint lpParam);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// <para>Destroys the specified window.
            /// The function sends <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/wm-destroy">WM_DESTROY</see>
            /// and <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/wm-ncdestroy">WM_NCDESTROY</see> messages
            /// to the window to deactivate it and remove the keyboard focus from it.
            /// The function also destroys the window's menu, destroys timers, removes clipboard ownership, and breaks the clipboard viewer chain
            /// (if the window is at the top of the viewer chain).</para>
            /// <para>If the specified window is a parent or owner window, <see cref="DestroyWindow(nint)"/> automatically destroys the associated child or owned windows
            /// when it destroys the parent or owner window.
            /// The function first destroys child or owned windows, and then it destroys the parent or owner window.</para>
            /// <para>DestroyWindow also destroys modeless dialog boxes created
            /// by the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-createdialogw">CreateDialog</see> function.</para>
            /// </summary>
            /// <param name="hWnd">A handle to the window to be destroyed.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is true.</para>
            /// <para>If the function fails, the return value is false.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-destroywindow"/></para>
            /// <para>A thread cannot use <see cref="DestroyWindow(nint)"/> to destroy a window created by a different thread.</para>
            /// <para>If the window being destroyed is a child window that does not have the <see cref="ExtendedWindowStyleFlags.NoParentNotify"/> style,
            /// a <see href="https://learn.microsoft.com/en-us/windows/win32/inputmsg/wm-parentnotify">WM_PARENTNOTIFY</see> message is sent to the parent.</para>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(DestroyWindow), SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool DestroyWindow(nint hWnd);
#else
            [DllImport("user32.dll", EntryPoint = nameof(DestroyWindow), ExactSpelling = true, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DestroyWindow(nint hWnd);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// <para>Retrieves a handle to the top-level window whose class name and window name match the specified strings.
            /// This function does not search child windows.
            /// This function does not perform a case-sensitive search.</para>
            /// <para>To search child windows, beginning with a specified child window, use the <see cref="FindWindowEx(nint, nint, string, string?)"/> function.</para>
            /// </summary>
            /// <param name="className">
            /// <para>The class name used for a previous call
            /// to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassw">RegisterClass</see>
            /// or <see cref="RegisterClassEx(ref WndClassEx)"/> function.</para>
            /// <para>The class name can be any name registered with <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassw">RegisterClass</see>
            /// or <see cref="RegisterClassEx(ref WndClassEx)"/>, or any of the predefined control-class names.</para>
            /// <para>If <paramref name="className"/> is null, it finds any window whose title matches the lpWindowName parameter.</para>
            /// </param>
            /// <param name="windowName">The window name (the window's title).
            /// If this parameter is null, all window names match.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is a handle to the window that has the specified class name and window name.</para>
            /// <para>If the function fails, the return value is ZERO. This function does not modify the last error value.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-findwindoww"/></para>
            /// <para>If the lpWindowName parameter is not null, FindWindow calls
            /// the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextw">GetWindowText</see> function to retrieve the window name for comparison.
            /// For a description of a potential problem that can arise, see the Remarks for GetWindowText.</para>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(FindWindow) + "W", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
            public static partial nint FindWindow(string? className, string? windowName);
#else
            [DllImport("user32.dll", EntryPoint = nameof(FindWindow) + "W", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern nint FindWindow(string? className, string? windowName);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// Retrieves a handle to a window whose class name and window name match the specified strings.
            /// The function searches child windows, beginning with the one following the specified child window.
            /// This function does not perform a case-sensitive search.
            /// </summary>
            /// <param name="hWndParent">
            /// <para>A handle to the parent window whose child windows are to be searched.</para>
            /// <para>If <paramref name="hWndParent"/> is ZERO, the function uses the desktop window as the parent window.
            /// The function searches among windows that are child windows of the desktop.</para>
            /// <para>If hwndParent is HWND_MESSAGE, the function searches all <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/window-features">message-only windows</see>.</para>
            /// </param>
            /// <param name="hWndChildAfter">
            /// <para>A handle to a child window.
            /// The search begins with the next child window in the Z order.
            /// The child window must be a direct child window of hwndParent, not just a descendant window.</para>
            /// <para>If <paramref name="hWndChildAfter"/> is ZERO, the search begins with the first child window of <paramref name="hWndParent"/>.</para>
            /// <para>Note that if both <paramref name="hWndParent"/> and <paramref name="hWndChildAfter"/> are ZERO, the function searches all top-level and message-only windows.</para>
            /// </param>
            /// <param name="className">
            /// <para>The class name used for a previous call
            /// to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassw">RegisterClass</see>
            /// or <see cref="RegisterClassEx(ref WndClassEx)"/> function.</para>
            /// <para>The class name can be any name registered with <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassw">RegisterClass</see>
            /// or <see cref="RegisterClassEx(ref WndClassEx)"/>, or any of the predefined control-class names, or it can be MAKEINTATOM(0x8000).
            /// In this latter case, 0x8000 is the atom for a menu class.
            /// For more information, see the Remarks section of this topic.</para>
            /// </param>
            /// <param name="windowName">The window name (the window's title).
            /// If this parameter is ZERO, all window names match.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is a handle to the window that has the specified class and window names.</para>
            /// <para>If the function fails, the return value is ZERO. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindowexw"/></para>
            /// <para>The <see cref="FindWindowEx(nint, nint, string?, string?)"/> function searches only direct child windows.
            /// It does not search other descendants.</para>
            /// <para>If the <paramref name="windowName"/> parameter is not null, <see cref="FindWindowEx(nint, nint, string?, string?)"/> calls
            /// the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextw">GetWindowText</see> function to retrieve the window name for comparison.
            /// For a description of a potential problem that can arise, see the Remarks section of GetWindowText.</para>
            /// <para>An application can call this function in the following way.</para>
            /// <para><c>FindWindowEx(default, default, 0x8000, null);</c></para>
            /// <para>Note that 0x8000 is the atom for a menu class.
            /// When an application calls this function, the function checks whether a context menu is being displayed that the application created.</para>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(FindWindowEx) + "W", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
            public static partial nint FindWindowEx(nint hWndParent, nint hWndChildAfter, string? className, string? windowName);
#else
            [DllImport("user32.dll", EntryPoint = nameof(FindWindowEx) + "W", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern nint FindWindowEx(nint hWndParent, nint hWndChildAfter, string? className, string? windowName);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// Enumerates all top-level windows on the screen by passing the handle to each window, in turn, to an application-defined callback function.
            /// <see cref="EnumWindows(EnumWindowsFunc, nint)"/> continues until the last top-level window is enumerated or the callback function returns false.
            /// </summary>
            /// <param name="enumFunc">A pointer to an application-defined callback function.
            /// For more information, see <see href="https://learn.microsoft.com/ja-jp/previous-versions/windows/desktop/legacy/ms633498(v=vs.85)">EnumWindowsProc</see>.</param>
            /// <param name="lParam">An application-defined value to be passed to the callback function.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is true.</para>
            /// <para>If the function fails, the return value is false.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// <para>If EnumWindowsProc returns false, the return value is also false.
            /// In this case, the callback function should call <see href="https://learn.microsoft.com/en-us/windows/win32/api/errhandlingapi/nf-errhandlingapi-setlasterror">SetLastError</see>
            /// to obtain a meaningful error code to be returned to the caller of <see cref="EnumWindows(EnumWindowsFunc, nint)"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para>https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumwindows</para>
            /// <para>The EnumWindows function does not enumerate child windows,
            /// with the exception of a few top-level windows owned by the system that have the <see cref="WindowStyleFlags.Child"/> style.</para>
            /// <para>This function is more reliable than calling the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindow">GetWindow</see> function in a loop.
            /// An application that calls GetWindow to perform this task risks being caught in an infinite loop or referencing a handle to a window that has been destroyed.</para>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(EnumWindows), SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool EnumWindows(EnumWindowsFunc enumFunc, nint lParam);
#else
            [DllImport("user32.dll", EntryPoint = nameof(EnumWindows), ExactSpelling = true, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EnumWindows(EnumWindowsFunc enumFunc, nint lParam);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// Sends the specified message to one or more windows.
            /// </summary>
            /// <param name="hWnd">
            /// <para>A handle to the window whose window procedure will receive the message.</para>
            /// <para>If this parameter is HWND_BROADCAST ((nint)0xffff), the message is sent to all top-level windows in the system, including disabled or invisible unowned windows.
            /// The function does not return until each window has timed out.
            /// Therefore, the total wait time can be up to the value of uTimeout multiplied by the number of top-level windows.</para>
            /// </param>
            /// <param name="uMsg">
            /// <para>The message to be sent.</para>
            /// <para>For lists of the system-provided messages,
            /// see <see href="https://learn.microsoft.com/en-us/windows/desktop/winmsg/about-messages-and-message-queues">System-Defined Messages</see>.</para>
            /// </param>
            /// <param name="wParam">Any additional message-specific information.</param>
            /// <param name="lParam">Any additional message-specific information.</param>
            /// <param name="flags">The behavior of this function. This parameter can be one or more of the <see cref="SendMessageTimeoutFlags"/> values.</param>
            /// <param name="timeout">The duration of the time-out period, in milliseconds.
            /// If the message is a broadcast message, each window can use the full time-out period.
            /// For example, if you specify a five second time-out period and there are three top-level windows that fail to process the message,
            /// you could have up to a 15 second delay.</param>
            /// <param name="result">The result of the message processing.
            /// The value of this parameter depends on the message that is specified.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is nonzero.
            /// <see cref="SendMessageTimeout(nint, int, nint, nint, SendMessageTimeoutFlags, uint, out nint)"/> does not provide information
            /// about individual windows timing out if HWND_BROADCAST is used.</para>
            /// <para>If the function fails or times out, the return value is 0.
            /// Note that the function does not always call <see href="https://learn.microsoft.com/en-us/windows/desktop/api/errhandlingapi/nf-errhandlingapi-setlasterror">SetLastError</see> on failure.
            /// If the reason for failure is important to you, call SetLastError(ERROR_SUCCESS) before calling <see cref="SendMessageTimeout(nint, int, nint, nint, SendMessageTimeoutFlags, uint, out nint)"/>.
            /// If the function returns 0, and <see cref="Marshal.GetLastWin32Error"/> returns ERROR_SUCCESS, then treat it as a generic failure.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagetimeoutw"/></para>
            /// <para>The function calls the window procedure for the specified window and, if the specified window belongs to a different thread,
            /// does not return until the window procedure has processed the message or the specified time-out period has elapsed.
            /// If the window receiving the message belongs to the same queue as the current thread, the window procedure is called directly—the time-out value is ignored.</para>
            /// <para>This function considers that a thread is not responding if it has not called <see href="https://learn.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getmessage">GetMessage</see>
            /// or a similar function within five seconds.</para>
            /// <para>The system only does marshalling for system messages (those in the range 0 to (<see href="https://learn.microsoft.com/en-us/windows/desktop/winmsg/wm-user">WM_USER</see>-1)).
            /// To send other messages (those >= WM_USER) to another process, you must do custom marshalling.</para>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(SendMessageTimeout) + "W", SetLastError = true)]
            public static partial nint SendMessageTimeout(nint hWnd, int uMsg, nint wParam, nint lParam, SendMessageTimeoutFlags flags, uint timeout, out nint result);
#else
            [DllImport("user32.dll", EntryPoint = nameof(SendMessageTimeout) + "W", ExactSpelling = true, SetLastError = true)]
            public static extern nint SendMessageTimeout(nint hWnd, int uMsg, nint wParam, nint lParam, SendMessageTimeoutFlags flags, uint timeout, out nint result);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// <para>The <see cref="GetDC(nint)"/> function retrieves a handle to a device context (DC) for the client area of a specified window or for the entire screen.
            /// You can use the returned handle in subsequent GDI functions to draw in the DC.
            /// The device context is an opaque data structure, whose values are used internally by GDI.</para>
            /// <para>The <see href="https://learn.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getdcex">GetDCEx</see> function is an extension to <see cref="GetDC(nint)"/>,
            /// which gives an application more control over how and whether clipping occurs in the client area.</para>
            /// </summary>
            /// <param name="hWnd">A handle to the window whose DC is to be retrieved.
            /// If this value is ZERO, <see cref="GetDC(nint)"/> retrieves the DC for the entire screen.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is a handle to the DC for the specified window's client area.</para>
            /// <para>If the function fails, the return value is ZERO.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getdc"/></para>
            /// <para>The <see cref="GetDC(nint)"/> function retrieves a common, class, or private DC depending on the class style of the specified window.
            /// For class and private DCs, <see cref="GetDC(nint)"/> leaves the previously assigned attributes unchanged.
            /// However, for common DCs, <see cref="GetDC(nint)"/> assigns default attributes to the DC each time it is retrieved.
            /// For example, the default font is System, which is a bitmap font.
            /// Because of this, the handle to a common DC returned by <see cref="GetDC(nint)"/> does not tell you what font, color, or brush was used when the window was drawn.
            /// To determine the font, call <see href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-gettextfacew">GetTextFace</see>.</para>
            /// <para>Note that the handle to the DC can only be used by a single thread at any one time.</para>
            /// <para>After painting with a common DC, the <see cref="ReleaseDC(nint, nint)"/> function must be called to release the DC.
            /// Class and private DCs do not have to be released.
            /// <see cref="ReleaseDC(nint, nint)"/> must be called from the same thread that called <see cref="GetDC(nint)"/>.
            /// The number of DCs is limited only by available memory.</para>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(GetDC), SetLastError = true)]
            public static partial nint GetDC(nint hWnd);
#else
            [DllImport("user32.dll", EntryPoint = nameof(GetDC), ExactSpelling = true, SetLastError = true)]
            public static extern nint GetDC(nint hWnd);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// The <see cref="ReleaseDC(nint, nint)"/> function releases a device context (DC), freeing it for use by other applications.
            /// The effect of the <see cref="ReleaseDC(nint, nint)"/> function depends on the type of DC.
            /// It frees only common and window DCs.
            /// It has no effect on class or private DCs.
            /// </summary>
            /// <param name="hWnd">A handle to the window whose DC is to be released.</param>
            /// <param name="hDc">A handle to the DC to be released.</param>
            /// <returns>
            /// <para>The return value indicates whether the DC was released.
            /// If the DC was released, the return value is true.</para>
            /// <para>If the DC was not released, the return value is false.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-releasedc"/></para>
            /// <para>The application must call the <see cref="ReleaseDC(nint, nint)"/> function for each call
            /// to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowdc">GetWindowDC</see>
            /// function and for each call to the <see cref="GetDC(nint)"/> function that retrieves a common DC.</para>
            /// <para>An application cannot use the <see cref="ReleaseDC(nint, nint)"/> function to release a DC
            /// that was created by calling the <see href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-createdcw">CreateDC</see> function;
            /// instead, it must use the <see href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-deletedc">DeleteDC</see> function.
            /// <see cref="ReleaseDC(nint, nint)"/> must be called from the same thread that called <see cref="GetDC(nint)"/>.</para>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(ReleaseDC), SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool ReleaseDC(nint hWnd, nint hDc);
#else
            [DllImport("user32.dll", EntryPoint = nameof(ReleaseDC), ExactSpelling = true, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ReleaseDC(nint hWnd, nint hDc);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// The <see cref="EnumDisplayMonitors"/> function enumerates display monitors (including invisible pseudo-monitors associated with the mirroring drivers)
            /// that intersect a region formed by the intersection of a specified clipping rectangle and the visible region of a device context.
            /// <see cref="EnumDisplayMonitors"/> calls an application-defined <see cref="MonitorEnumFunc"/> callback delegate once for each monitor that is enumerated.
            /// Note that <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics">GetSystemMetrics</see> (SM_CMONITORS) counts only the display monitors.
            /// </summary>
            /// <param name="hdc">
            /// <para>A handle to a display device context that defines the visible region of interest.</para>
            /// <para>If this parameter is 0, the hdcMonitor parameter passed to the callback delegate will be 0,
            /// and the visible region of interest is the virtual screen that encompasses all the displays on the desktop.</para>
            /// </param>
            /// <param name="lprcClip">
            /// <para>A pointer to a <see cref="Rect"/> structure that specifies a clipping rectangle.
            /// The region of interest is the intersection of the clipping rectangle with the visible region specified by <paramref name="hdc"/>.</para>
            /// <para>If <paramref name="hdc"/> is non-0, the coordinates of the clipping rectangle are relative to the origin of the <paramref name="hdc"/>.
            /// If <paramref name="hdc"/> is 0, the coordinates are virtual-screen coordinates.</para>
            /// <para>This parameter can be 0 if you don't want to clip the region specified by hdc.</para>
            /// </param>
            /// <param name="lpfnEnum">An application-defined <see cref="MonitorEnumFunc"/> delegate.</param>
            /// <param name="dwData">Application-defined data that <see cref="EnumDisplayMonitors"/> passes directly to the <see cref="MonitorEnumFunc"/> callback delegate.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is true.</para>
            /// <para>If the function fails, the return value is false.</para>
            /// </returns>
            /// <remarks>
            /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaymonitors"/>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(EnumDisplayMonitors), SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool EnumDisplayMonitors(nint hdc, nint lprcClip, MonitorEnumFunc lpfnEnum, nint dwData);
#else
            [DllImport("user32.dll", EntryPoint = nameof(EnumDisplayMonitors), ExactSpelling = true, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EnumDisplayMonitors(nint hdc, nint lprcClip, MonitorEnumFunc lpfnEnum, nint dwData);
#endif  // NET7_0_OR_GREATER

            /// <summary>
            /// The <see cref="GetMonitorInfo"/> function retrieves information about a display monitor.
            /// </summary>
            /// <param name="hMonitor">A handle to the display monitor of interest.</param>
            /// <param name="lpmi">
            /// <para>A reference to a <see cref="MonitorInfo"/> structure that receives information about the specified display monitor.</para>
            /// <para>You must set the <see cref="MonitorInfo._size"/> member of the structure to <c>Marshal.SizeOf&lt;MONITORINFO&gt;()</c> before calling the <see cref="GetMonitorInfo"/> function.
            /// Doing so lets the function determine the type of structure you are passing to it.</para>
            /// </param>
            /// <returns>
            /// <para>If the function succeeds, the return value is true.</para>
            /// <para>If the function fails, the return value is false.</para>
            /// </returns>
            /// <remarks>
            /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmonitorinfow"/>
            /// </remarks>
            [DllImport("user32.dll", EntryPoint = nameof(GetMonitorInfo) + "W", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetMonitorInfo(nint hMonitor, ref MonitorInfo lpmi);

            /// <summary>
            /// Retrieves the position of the mouse cursor, in screen coordinates.
            /// </summary>
            /// <param name="point">A reference of a <see cref="Point"/> structure that receives the screen coordinates of the cursor.</param>
            /// <returns>Returns nonzero if successful or zero otherwise.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getcursorpos"/></para>
            /// <para>The cursor position is always specified in screen coordinates and is not affected by the mapping mode of the window that contains the cursor.</para>
            /// <para>The calling process must have WINSTA_READATTRIBUTES access to the window station.</para>
            /// <para>The input desktop must be the current desktop when you call <see cref="GetCursorPos(out Point)"/>.
            /// Call <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-openinputdesktop">OpenInputDesktop</see>
            /// to determine whether the current desktop is the input desktop.
            /// If it is not, call <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setthreaddesktop">SetThreadDesktop</see>
            /// with the HDESK returned by OpenInputDesktop to switch to that desktop.</para>
            /// </remarks>
#if NET7_0_OR_GREATER
            [LibraryImport("user32.dll", EntryPoint = nameof(GetCursorPos), SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool GetCursorPos(out Point point);
#else
            [DllImport("user32.dll", EntryPoint = nameof(GetCursorPos), ExactSpelling = true, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetCursorPos(out Point point);
#endif  // NET7_0_OR_GREATER
        }
    }
}
