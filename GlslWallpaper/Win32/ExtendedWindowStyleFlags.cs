using System;


#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1069 // Enums values should not be duplicated
#pragma warning restore IDE0079 // Remove unnecessary suppression


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// Flag bits for the first argument of <see cref="NativeWindow.SafeNativeMethods.CreateWindowEx(ExtendedWindowStyleFlags, nint, string, WindowStyleFlags, int, int, int, int, nint, nint, nint, nint)"/>.
    /// </summary>
    /// <remarks>
    /// <seelso href="https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles"/>
    /// </remarks>
    [Flags]
    internal enum ExtendedWindowStyleFlags
    {
        /// <summary>
        /// The window has generic left-aligned properties.
        /// This is the default.
        /// </summary>
        Left = 0x00000000,
        /// <summary>
        /// The window text is displayed using left-to-right reading-order properties.
        /// This is the default.
        /// </summary>
        LeftReading = 0x00000000,
        /// <summary>
        /// The vertical scroll bar (if present) is to the right of the client area.
        /// This is the default.
        /// </summary>
        RightScrollBar = 0x00000000,
        /// <summary>
        /// The window has a double border; the window can, optionally, be created with a title bar by specifying the <see cref="WindowStyleFlags.Caption"/> style in the dwStyle parameter.
        /// </summary>
        DialogModalFrame = 0x00000001,
        /// <summary>
        /// The child window created with this style does not send
        /// the <see href="https://learn.microsoft.com/en-us/previous-versions/windows/desktop/inputmsg/wm-parentnotify">WM_PARENTNOTIFY</see> message
        /// to its parent window when it is created or destroyed.
        /// </summary>
        NoParentNotify = 0x00000004,
        /// <summary>
        /// The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated.
        /// To add or remove this style, use the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos">SetWindowPos</see> function.
        /// </summary>
        TopMost = 0x00000008,
        /// <summary>
        /// The window accepts drag-drop files.
        /// </summary>
        AcceptFiles = 0x00000010,
        /// <summary>
        /// <para>The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
        /// The window appears transparent because the bits of underlying sibling windows have already been painted.</para>
        /// <para>To achieve transparency without these restrictions,
        /// use the <see href="https://learn.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-setwindowrgn">SetWindowRgn</see> function.</para>
        /// </summary>
        Transparent = 0x00000020,
        /// <summary>
        /// The window is a MDI child window.
        /// </summary>
        MDIChild = 0x00000040,
        /// <summary>
        /// The window is intended to be used as a floating toolbar.
        /// A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font.
        /// A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB.
        /// If a tool window has a system menu, its icon is not displayed on the title bar.
        /// However, you can display the system menu by right-clicking or by typing ALT+SPACE.
        /// </summary>
        ToolWindow = 0x00000080,
        /// <summary>
        /// The window has a border with a raised edge.
        /// </summary>
        WindowEdge = 0x00000100,
        /// <summary>
        /// The window is palette window, which is a modeless dialog box that presents an array of commands.
        /// </summary>
        PaletteWindow = (WindowEdge | ToolWindow | TopMost),
        /// <summary>
        /// The window has a border with a sunken edge.
        /// </summary>
        ClientEdge = 0x00000200,
        /// <summary>
        /// The window is an overlapped window.
        /// </summary>
        OverlappedWindow = (WindowEdge | ClientEdge),
        /// <summary>
        /// <para>The title bar of the window includes a question mark.
        /// When the user clicks the question mark, the cursor changes to a question mark with a pointer.
        /// If the user then clicks a child window, the child receives a <see href="https://learn.microsoft.com/en-us/windows/win32/shell/wm-help">WM_HELP</see> message.
        /// The child window should pass the message to the parent window procedure,
        /// which should call the <see href="https://learn.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-winhelpa">WinHelp</see> function using the HELP_WM_HELP command.
        /// The Help application displays a pop-up window that typically contains help for the child window.</para>
        /// <para><see cref="ContextHelp"/> cannot be used with the <see cref="WindowStyleFlags.MaximizeBox"/> or <see cref="WindowStyleFlags.MinimizeBox"/> styles.</para>
        /// </summary>
        ContextHelp = 0x00000400,
        /// <summary>
        /// <para>The window has generic "right-aligned" properties.
        /// This depends on the window class.
        /// This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.</para>
        /// <para>Using the <see cref="Right"/> style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively.
        /// Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.</para>
        /// </summary>
        Right = 0x00001000,
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties.
        /// For other languages, the style is ignored.
        /// </summary>
        RightLeading = 0x00002000,
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area.
        /// For other languages, the style is ignored.
        /// </summary>
        LeftScrollBar = 0x00004000,
        /// <summary>
        /// The window itself contains child windows that should take part in dialog box navigation.
        /// If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key,
        /// an arrow key, or a keyboard mnemonic.
        /// </summary>
        ControlParent = 0x00010000,
        /// <summary>
        /// The window has a three-dimensional border style intended to be used for items that do not accept user input.
        /// </summary>
        StaticEdge = 0x00020000,
        /// <summary>
        /// Forces a top-level window onto the taskbar when the window is visible.
        /// </summary>
        AppWindow = 0x00040000,
        /// <summary>
        /// <para>The window is a <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/window-features">layered window</see>.
        /// This style cannot be used if the window has a <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/about-window-classes">class style</see>
        /// of either <see cref="WindowClassStyleFlags.OwnDC"/> or <see cref="WindowClassStyleFlags.ClassDC"/>.</para>
        /// <para>Windows 8: The <see cref="Layered"/> style is supported for top-level windows and child windows.
        /// Previous Windows versions support <see cref="Layered"/> only for top-level windows.</para>
        /// </summary>
        Layered = 0x00080000,
        /// <summary>
        /// The window does not pass its window layout to its child windows.
        /// </summary>
        NoInheritLayout = 0x00100000,
        /// <summary>
        /// The window does not render to a redirection surface.
        /// This is for windows that do not have visible content or that use mechanisms other than surfaces to provide their visual.
        /// </summary>
        NoRedirectionBitmap = 0x00200000,
        /// <summary>
        /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the horizontal origin of the window is on the right edge.
        /// Increasing horizontal values advance to the left.
        /// </summary>
        LayoutRight = 0x00400000,
        /// <summary>
        /// <para>Paints all descendants of a window in bottom-to-top painting order using double-buffering.
        /// Bottom-to-top painting order allows a descendent window to have translucency (alpha) and transparency (color-key) effects,
        /// but only if the descendent window also has the <see cref="Transparent"/> bit set.
        /// Double-buffering allows the window and its descendents to be painted without flicker.
        /// This cannot be used if the window has a <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/about-window-classes">class style</see>
        /// of CS_OWNDC, CS_CLASSDC, or CS_PARENTDC.</para>
        /// <para>Windows 2000: This style is not supported.</para>
        /// </summary>
        Composited = 0x02000000,
        /// <summary>
        /// <para>A top-level window created with this style does not become the foreground window when the user clicks it.
        /// The system does not bring this window to the foreground when the user minimizes or closes the foreground window.</para>
        /// <para>The window should not be activated through programmatic access or via keyboard navigation by accessible technology, such as Narrator.</para>
        /// <para>To activate the window, use the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setactivewindow">SetActiveWindow</see>
        /// or <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setforegroundwindow">SetForegroundWindow</see> function.</para>
        /// <para>The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the <see cref="AppWindow"/> style.</para>
        /// </summary>
        NoActivate = 0x08000000,
    }
}
