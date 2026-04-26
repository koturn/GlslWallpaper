using System;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// Flags bits for 5th argument of <see cref="NativeWindow.SafeNativeMethods.SendMessageTimeout(nint, int, nint, nint, SendMessageTimeoutFlags, uint, out nint)"/>.
    /// </summary>
    [Flags]
    internal enum SendMessageTimeoutFlags : uint
    {
        /// <summary>
        /// The calling thread is not prevented from processing other requests while waiting for the function to return.
        /// </summary>
        Normal = 0x0000,
        /// <summary>
        /// Prevents the calling thread from processing any other requests until the function returns.
        /// </summary>
        Block = 0x0001,
        /// <summary>
        /// The function returns without waiting for the time-out period to elapse if the receiving thread appears to not respond or "hangs."
        /// </summary>
        AbortIfHung = 0x0002,
        /// <summary>
        /// The function does not enforce the time-out period as long as the receiving thread is processing messages.
        /// </summary>
        NoTimeoutIfNotHung = 0x0008,
        /// <summary>
        /// The function should return 0 if the receiving window is destroyed or its owning thread dies while the message is being processed.
        /// </summary>
        ErrorOnExit = 0x0020
    }
}
