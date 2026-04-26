#if !NET7_0_OR_GREATER


namespace GlslWallpaper.Internals
{
    /// <summary>
    /// Win32 error codes used in this library.
    /// </summary>
    /// <remarks>
    /// <see href="https://learn.microsoft.com/en-us/windows/win32/debug/system-error-codes"/>
    /// </remarks>
    internal enum Win32Errors
    {
        /// <summary>
        /// The data area passed to a system call is too small.
        /// </summary>
        InsufficientBuffer = 122
    }
}


#endif  // !NET7_0_OR_GREATER
