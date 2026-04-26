namespace GlslWallpaper.Win32
{
    /// <summary>
    /// <para>Delegate for the Window procedure functions.</para>
    /// <para>A callback function, which you define in your application, that processes messages sent to a window.
    /// The WNDPROC type defines a pointer to this callback function.
    /// The WndProc name is a placeholder for the name of the function that you define in your application.</para>
    /// </summary>
    /// <param name="hWnd">A handle to the window. This parameter is typically named <paramref name="hWnd"/>.</param>
    /// <param name="uMsg">
    /// <para>The message. This parameter is typically named <paramref name="uMsg"/>.</para>
    /// <para>For lists of the system-provided messages, see <see href="https://learn.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues#system-defined-messages">System-defined messages</see>.</para>
    /// </param>
    /// <param name="wParam">
    /// <para>Additional message information. This parameter is typically named wParam.</para>
    /// <para>The contents of the <paramref name="wParam"/> parameter depend on the value of the <paramref name="uMsg"/> parameter.</para>
    /// </param>
    /// <param name="lParam">
    /// <para>Additional message information. This parameter is typically named <paramref name="lParam"/>.</para>
    /// <para>The contents of the <paramref name="lParam"/> parameter depend on the value of the <paramref name="uMsg"/> parameter.</para>
    /// </param>
    /// <returns>The return value is the result of the message processing, and depends on the message sent.</returns>
    /// <remarks>
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-wndproc"/>
    /// </remarks>
    public delegate nint WndProcFunc(nint hWnd, uint uMsg, nint wParam, nint lParam);
}
