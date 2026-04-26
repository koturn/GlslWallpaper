#if NET7_0_OR_GREATER
#    define SUPPORT_LIBRARY_IMPORT
#endif  // NET7_0_OR_GREATER

using System.Runtime.InteropServices;
using System.Security;
using GlslWallpaper.Internals;


namespace GlslWallpaper.Win32
{
#if SUPPORT_LIBRARY_IMPORT
    internal static partial class Gdi32
#else
    internal static class Gdi32
#endif  // SUPPORT_LIBRARY_IMPORT
    {
        public static int SetupPixelFormat(DeviceContextHandle dcHandle)
        {
            var pfd = new PixelFormatDescriptor
            {
                Size = (ushort)Marshal.SizeOf<PixelFormatDescriptor>(),
                Version = 1,
                Flags = PixelFormatDescriptorFlags.DoubleBuffer
                    | PixelFormatDescriptorFlags.DrawToWindow
                    | PixelFormatDescriptorFlags.SupportOpenGL,
                PixelType = PixelFormatDescriptorPixelType.RGBA,
                ColorBits = 32,
                DepthBits = 24,
                StencilBits = 8,
            };
            var formatIndex = ChoosePixelFormat(dcHandle, ref pfd);
            SetPixelFormat(dcHandle, formatIndex, ref pfd);
            return formatIndex;
        }

        public static int ChoosePixelFormat(DeviceContextHandle dcHandle, ref PixelFormatDescriptor pfd)
        {
            int formatIndex = SafeNativeMethods.ChoosePixelFormat(dcHandle.DangerousGetHandle(), ref pfd);
            if (formatIndex == 0)
            {
                ThrowHelper.ThrowLastWin32Exception(nameof(SafeNativeMethods.ChoosePixelFormat) + "() failed");
            }
            return formatIndex;
        }

        public static void SetPixelFormat(DeviceContextHandle dcHandle, int format, ref PixelFormatDescriptor pfd)
        {
            bool result = SafeNativeMethods.SetPixelFormat(dcHandle.DangerousGetHandle(), format, ref pfd);
            if (!result)
            {
                ThrowHelper.ThrowLastWin32Exception(nameof(SafeNativeMethods.SetPixelFormat) + "() failed");
            }
        }

        public static void SwapBuffers(DeviceContextHandle hdc)
        {
            if (!SafeNativeMethods.SwapBuffers(hdc.DangerousGetHandle()))
            {
                ThrowHelper.ThrowLastWin32Exception(nameof(SafeNativeMethods.SwapBuffers) + "() failed");
            }
        }

        /// <summary>
        /// Provides native methods.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
#if SUPPORT_LIBRARY_IMPORT
        private static partial class SafeNativeMethods
#else
        private static class SafeNativeMethods
#endif  // SUPPORT_LIBRARY_IMPORT
        {
            /// <summary>
            /// The <see cref="Gdi32.ChoosePixelFormat"/> function attempts to match an appropriate pixel format supported by a device context to a given pixel format specification.
            /// </summary>
            /// <param name="hdc">Specifies the device context that the function examines to determine the best match for the pixel format descriptor pointed to by <paramref name="pfd"/>.</param>
            /// <param name="pfd">Reference of a <see cref="PixelFormatDescriptor"/> structure that specifies the requested pixel format.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is a pixel format index (one-based) that is the closest match to the given pixel format descriptor.</para>
            /// <para>If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-choosepixelformat"/>
            /// <para>You must ensure that the pixel format matched by the <see cref="ChoosePixelFormat"/> function satisfies your requirements.
            /// For example, if you request a pixel format with a 24-bit RGB color buffer but the device context offers only 8-bit RGB color buffers,
            /// the function returns a pixel format with an 8-bit RGB color buffer.</para>
            /// </remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("gdi32.dll", EntryPoint = nameof(ChoosePixelFormat), SetLastError = true)]
            public static partial int ChoosePixelFormat(nint hdc, ref PixelFormatDescriptor pfd);
#else
            [DllImport("gdi32.dll", EntryPoint = nameof(ChoosePixelFormat), ExactSpelling = true, SetLastError = true)]
            public static extern int ChoosePixelFormat(nint hdc, ref PixelFormatDescriptor pfd);
#endif  // SUPPORT_LIBRARY_IMPORT

            /// <summary>
            /// The <see cref="SetPixelFormat"/> function sets the pixel format of the specified device context to the format specified by the iPixelFormat index.
            /// </summary>
            /// <param name="hdc">Specifies the device context whose pixel format the function attempts to set.</param>
            /// <param name="format">Index that identifies the pixel format to set.
            /// The various pixel formats supported by a device context are identified by one-based indexes.</param>
            /// <param name="pfd">Pointer to a <see cref="PixelFormatDescriptor"/> structure that contains the logical pixel format specification.
            /// The system's metafile component uses this structure to record the logical pixel format specification.
            /// The structure has no other effect upon the behavior of the <see cref="SetPixelFormat"/> function.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is true.</para>
            /// <para>If the function fails, the return value is false. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-setpixelformat"/></para>
            /// <para>If <paramref name="hdc"/> references a window, calling the <see cref="SetPixelFormat"/> function also changes the pixel format of the window.
            /// Setting the pixel format of a window more than once can lead to significant complications for the Window Manager and for multithread applications, so it is not allowed.
            /// An application can only set the pixel format of a window one time.
            /// Once a window's pixel format is set, it cannot be changed.</para>
            /// <para>You should select a pixel format in the device context before calling the <see cref="OpenGL.WGL.CreateContext"/> function.
            /// The <see cref="OpenGL.WGL.CreateContext"/> function creates a rendering context for drawing on the device in the selected pixel format of the device context.</para>
            /// <para>An OpenGL window has its own pixel format.
            /// Because of this, only device contexts retrieved for the client area of an OpenGL window are allowed to draw into the window.
            /// As a result, an OpenGL window should be created with the WS_CLIPCHILDREN and WS_CLIPSIBLINGS styles.
            /// Additionally, the window class attribute should not include the CS_PARENTDC style.</para>
            /// </remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("gdi32.dll", EntryPoint = nameof(SetPixelFormat), SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool SetPixelFormat(nint hdc, int format, ref PixelFormatDescriptor pfd);
#else
            [DllImport("gdi32.dll", EntryPoint = nameof(SetPixelFormat), ExactSpelling = true, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetPixelFormat(nint hdc, int format, ref PixelFormatDescriptor pfd);
#endif  // SUPPORT_LIBRARY_IMPORT

            /// <summary>
            /// The SwapBuffers function exchanges the front and back buffers if the current pixel format
            /// for the window referenced by the specified device context includes a back buffer.
            /// </summary>
            /// <param name="hdc">Specifies a device context.
            /// If the current pixel format for the window referenced by this device context includes a back buffer, the function exchanges the front and back buffers.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is true.</para>
            /// <para>If the function fails, the return value is false. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-swapbuffers"/></para>
            /// <para>If the current pixel format for the window referenced by the device context does not include a back buffer,
            /// this call has no effect and the content of the back buffer is undefined when the function returns.</para>
            /// <para>With multithread applications, flush the drawing commands in any other threads drawing to the same window before calling <see cref="SwapBuffers"/>.</para>
            /// </remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("gdi32.dll", EntryPoint = nameof(SwapBuffers), SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool SwapBuffers(nint hdc);
#else
            [DllImport("gdi32.dll", EntryPoint = nameof(SwapBuffers), ExactSpelling = true, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SwapBuffers(nint hdc);
#endif  // SUPPORT_LIBRARY_IMPORT
        }
    }
}
