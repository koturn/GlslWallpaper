#if NET7_0_OR_GREATER
#    define SUPPORT_LIBRARY_IMPORT
#endif  // NET7_0_OR_GREATER
// #define DISABLE_FUNCTION_POINTER
using System;
using System.Runtime.InteropServices;
using System.Security;
using GlslWallpaper.Internals;
using GlslWallpaper.Win32;


namespace GlslWallpaper.OpenGL
{
#if SUPPORT_LIBRARY_IMPORT
    /// <summary>
    /// Provides WGL utility functions.
    /// </summary>
    internal static unsafe partial class WGL
#else
    internal static unsafe class WGL
#endif  // SUPPORT_LIBRARY_IMPORT
    {
#if DISABLE_FUNCTION_POINTER
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate nint WGLCreateContextAttribsARBFunc(nint hdc, nint share, int* attribs);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool WGLSwapIntervalEXTFunc(int interval);
#endif  // DISABLE_FUNCTION_POINTER

        private static nint hOpengl32;
#if DISABLE_FUNCTION_POINTER
        private static WGLCreateContextAttribsARBFunc? _wglCreateContextAttribsARB;
        private static WGLSwapIntervalEXTFunc? _wglSwapIntervalEXT;
#else
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL/extensions/ARB/WGL_ARB_create_context.txt"><c>wglCreateContextAttribsARB</c></see>.
        /// </summary>
        /// <remarks>
        /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglcreatecontext"/>
        /// </remarks>
        private static delegate* unmanaged[Cdecl]<nint, nint, int*, nint> _wglCreateContextAttribsARB;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL/extensions/EXT/WGL_EXT_swap_control.txt"><c>wglSwapIntervalEXT</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<int, bool> _wglSwapIntervalEXT;
#endif  // DISABLE_FUNCTION_POINTER

        /// <summary>
        /// Creates a new OpenGL rendering context, which is suitable for drawing on the device referenced by hdc.
        /// </summary>
        /// <param name="dcHandle">Device context.</param>
        /// <returns>A new OpenGL rendering context</returns>
        public static GLRenderContextHandle CreateContext(DeviceContextHandle dcHandle)
        {
            var glrcHandle = SafeNativeMethods.WglCreateContext(dcHandle);
            if (glrcHandle == 0)
            {
                ThrowHelper.ThrowLastWin32Exception("wglCreateContext() failed");
            }
            return new GLRenderContextHandle(glrcHandle);
        }

        public static GLRenderContextHandle CreateContextAttribsARB(DeviceContextHandle dcHandle, nint share, int[] attribs)
        {
            if (_wglCreateContextAttribsARB == null)
            {
#if DISABLE_FUNCTION_POINTER
                _wglCreateContextAttribsARB = LoadFunctionAsDelegate<WGLCreateContextAttribsARBFunc>("wglCreateContextAttribsARB");
#else
                _wglCreateContextAttribsARB = (delegate* unmanaged[Cdecl]<nint, nint, int*, nint>)LoadFunction("wglCreateContextAttribsARB");
#endif  // DISABLE_FUNCTION_POINTER
            }
            unsafe
            {
                fixed (int* pAttribs = &attribs[0])
                {
                    var hGlRc = _wglCreateContextAttribsARB(dcHandle.DangerousGetHandle(), share, pAttribs);
                    if (hGlRc == 0)
                    {
                        ThrowHelper.ThrowLastWin32Exception("wglCreateContextAttribsARB() failed");
                    }
                    return new GLRenderContextHandle(hGlRc);
                }
            }
        }

        public static void SwapInterval(int frameCount)
        {
            if (_wglSwapIntervalEXT == null)
            {
#if DISABLE_FUNCTION_POINTER
                _wglSwapIntervalEXT = LoadFunctionAsDelegate<WGLSwapIntervalEXTFunc>("wglSwapIntervalEXT");
#else
                _wglSwapIntervalEXT = (delegate* unmanaged[Cdecl]<int, bool>)LoadFunction("wglSwapIntervalEXT");
#endif  // DISABLE_FUNCTION_POINTER
            }
            if (!_wglSwapIntervalEXT(frameCount))
            {
                ThrowHelper.ThrowLastWin32Exception("wglSwapIntervalEXT() failed");
            }
        }

        /// <summary>
        /// Deletes a specified OpenGL rendering context.
        /// </summary>
        /// <param name="hGlRc">OpenGL rendering context.</param>
        public static void DeleteContext(nint hGlRc)
        {
            var result = SafeNativeMethods.WglDeleteContext(hGlRc);
            if (!result)
            {
                ThrowHelper.ThrowLastWin32Exception("wglDeleteContext() failed");
            }
        }

        /// <summary>
        /// makes a specified OpenGL rendering context the calling thread's current rendering context.
        /// </summary>
        /// <param name="dcHandle">Device context.</param>
        /// <param name="glrcHandle">OpenGL rendering context</param>
        /// <exception cref="System.ComponentModel.Win32Exception">Thrown when wglMakeCurrent function is failed.</exception>
        public static void MakeCurrent(DeviceContextHandle dcHandle, GLRenderContextHandle glrcHandle)
        {
            var result = SafeNativeMethods.WglMakeCurrent(dcHandle.DangerousGetHandle(), glrcHandle.DangerousGetHandle());
            if (!result)
            {
                ThrowHelper.ThrowLastWin32Exception("wglMakeCurrent() failed");
            }
        }

        public static void ResetCurrent(bool ignoreError = false)
        {
            var result = SafeNativeMethods.WglMakeCurrent(0, 0);
            if (!result && !ignoreError)
            {
                ThrowHelper.ThrowLastWin32Exception("wglMakeCurrent() failed");
            }
        }

        public static void ShareLists(GLRenderContextHandle glrcHandleFrom, GLRenderContextHandle glrcHandleTo)
        {
            var result = SafeNativeMethods.WglShareLists(glrcHandleFrom, glrcHandleTo);
            if (!result)
            {
                ThrowHelper.ThrowLastWin32Exception("wglShareLists() failed");
            }
        }

        /// <summary>
        /// Load OpenGL function as a pointer.
        /// </summary>
        /// <param name="funcName">Function name.</param>
        /// <returns>Pointer to the OpenGL function.</returns>
        /// <exception cref="DllNotFoundException">Thrown when opengl32.dll is not found.</exception>
        /// <exception cref="EntryPointNotFoundException">Thrown when specified function is not found.</exception>
        internal static nint LoadFunction(string funcName)
        {
            var ptr = SafeNativeMethods.WglGetProcAddress(funcName);
            if (ptr >= -1 && ptr <= 3)
            {
                if (hOpengl32 == 0)
                {
#if NETCOREAPP3_0_OR_GREATER
                    hOpengl32 = NativeLibrary.Load("opengl32.dll");
#else
                    hOpengl32 = SafeNativeMethods.GetModuleHandle("opengl32");
                    if (hOpengl32 == 0)
                    {
                        throw new DllNotFoundException("Dll not found: opengl32.dll");
                    }
#endif  // NETCOREAPP3_0_OR_GREATER
                }
#if NETCOREAPP3_0_OR_GREATER
                ptr = NativeLibrary.GetExport(hOpengl32, funcName);
#else
                ptr = SafeNativeMethods.GetProcAddress(hOpengl32, funcName);
                if (ptr == 0)
                {
                    throw new EntryPointNotFoundException($"GL function not found: {funcName}");
                }
#endif  // NETCOREAPP3_0_OR_GREATER
            }
            return ptr;
        }

#if DISABLE_FUNCTION_POINTER
        /// <summary>
        /// Load OpenGL function as delegate.
        /// </summary>
        /// <typeparam name="T">Delegate type.</typeparam>
        /// <param name="funcName">Function name.</param>
        /// <returns>Delegate of OpenGL function.</returns>
        internal static T LoadFunctionAsDelegate<T>(string funcName)
            where T : Delegate
        {
            return Marshal.GetDelegateForFunctionPointer<T>(LoadFunction(funcName));
        }
#endif  // DISABLE_FUNCTION_POINTER

        /// <summary>
        /// Provides native methods.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
#if SUPPORT_LIBRARY_IMPORT
        internal static partial class SafeNativeMethods
#else
        internal static class SafeNativeMethods
#endif  // SUPPORT_LIBRARY_IMPORT
        {
#if !NETCOREAPP3_0_OR_GREATER
            [DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = nameof(GetProcAddress), ExactSpelling = true, SetLastError = true)]
            public static extern nint GetProcAddress(nint hModule, string procName);

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = nameof(GetModuleHandle) + "W", ExactSpelling = true, SetLastError = true)]
            public static extern nint GetModuleHandle(string lpModuleName);
#endif  // !NETCOREAPP3_0_OR_GREATER

            /// <summary>
            /// The <see cref="WglCreateContext"/> function creates a new OpenGL rendering context, which is suitable for drawing on the device referenced by hdc.
            /// The rendering context has the same pixel format as the device context.
            /// </summary>
            /// <param name="dcHandle">Typically named handleToDeviceContext.
            /// Handle to a device context for which the function creates a suitable OpenGL rendering context.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is a valid handle to an OpenGL rendering context.</para>
            /// <para>If the function fails, the return value is ZERO.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglcreatecontext"/></para>
            /// <para>A rendering context is not the same as a device context.
            /// Set the pixel format of the device context before creating a rendering context.
            /// For more information on setting the device context's pixel format, see the <see cref="Gdi32.SetPixelFormat"/> function.</para>
            /// <para>To use OpenGL, you create a rendering context, select it as a thread's current rendering context, and then call OpenGL functions.
            /// When you are finished with the rendering context, you dispose of it by calling the <see cref="WglDeleteContext"/> function.</para>
            /// </remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("opengl32.dll", EntryPoint = "wglCreateContext", SetLastError = true)]
            public static partial nint WglCreateContext(DeviceContextHandle dcHandle);
#else
            [DllImport("opengl32.dll", EntryPoint = "wglCreateContext", ExactSpelling = true, SetLastError = true)]
            public static extern nint WglCreateContext(DeviceContextHandle dcHandle);
#endif  // SUPPORT_LIBRARY_IMPORT

            /// <summary>
            /// The <see cref="WglDeleteContext"/> function deletes a specified OpenGL rendering context.
            /// </summary>
            /// <param name="glrcHandle">Handle to an OpenGL rendering context that the function will delete.</param>
            /// <returns>
            /// <para>If the function succeeds, the return value is true.</para>
            /// <para>If the function fails, the return value is false. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wgldeletecontext"/></para>
            /// <para>It is an error to delete an OpenGL rendering context that is the current context of another thread.
            /// However, if a rendering context is the calling thread's current context,
            /// the <see cref="WglDeleteContext"/> function changes the rendering context to being not current before deleting it.</para>
            /// <para>The wglDeleteContext function does not delete the device context associated with the OpenGL rendering context when you call the wglMakeCurrent function.
            /// After calling <see cref="WglDeleteContext"/>, you must call <see href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-deletedc">DeleteDC</see>
            /// to delete the associated device context.</para>
            /// </remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("opengl32.dll", EntryPoint = "wglDeleteContext", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool WglDeleteContext(nint glrcHandle);
#else
            [DllImport("opengl32.dll", EntryPoint = "wglDeleteContext", ExactSpelling = true, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool WglDeleteContext(nint glrcHandle);
#endif  // SUPPORT_LIBRARY_IMPORT

            /// <summary>
            /// The <see cref="WglMakeCurrent"/> function makes a specified OpenGL rendering context the calling thread's current rendering context.
            /// All subsequent OpenGL calls made by the thread are drawn on the device identified by <paramref name="hDc"/>.
            /// You can also use <see cref="WglMakeCurrent"/> to change the calling thread's current rendering context so it's no longer current.
            /// </summary>
            /// <param name="hDc">Handle to a device context. Subsequent OpenGL calls made by the calling thread are drawn on the device identified by <paramref name="hDc"/>.</param>
            /// <param name="hGlRc">
            /// <para>Handle to an OpenGL rendering context that the function sets as the calling thread's rendering context.</para>
            /// <para>If <paramref name="hGlRc"/> is ZERO, the function makes the calling thread's current rendering context no longer current,
            /// and releases the device context that is used by the rendering context.
            /// In this case, <paramref name="hDc"/> is ignored.</para>
            /// </param>
            /// <returns>When the wglMakeCurrent function succeeds, the return value is true; otherwise the return value is false.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglmakecurrent"/></para>
            /// <para>The <paramref name="hDc"/> parameter must refer to a drawing surface supported by OpenGL.
            /// It need not be the same <paramref name="hDc"/> that was passed to <see cref="WglCreateContext(DeviceContextHandle)"/> when <paramref name="hGlRc"/> was created,
            /// but it must be on the same device and have the same pixel format.
            /// GDI transformation and clipping in <paramref name="hDc"/> are not supported by the rendering context.
            /// The current rendering context uses the <paramref name="hDc"/> device context until the rendering context is no longer current.</para>
            /// <para>Before switching to the new rendering context, OpenGL flushes any previous rendering context that was current to the calling thread.</para>
            /// <para>A thread can have one current rendering context.
            /// A process can have multiple rendering contexts by means of multithreading.
            /// A thread must set a current rendering context before calling any OpenGL functions.
            /// Otherwise, all OpenGL calls are ignored.</para>
            /// <para>A rendering context can be current to only one thread at a time.
            /// You cannot make a rendering context current to multiple threads.</para>
            /// <para>An application can perform multithread drawing by making different rendering contexts current to different threads,
            /// supplying each thread with its own rendering context and device context.</para>
            /// <para>If an error occurs, the <see cref="WglMakeCurrent"/> function makes the thread's current rendering context not current before returning.</para>
            /// </remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("opengl32.dll", EntryPoint = "wglMakeCurrent", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool WglMakeCurrent(nint hDc, nint hGlRc);
#else
            [DllImport("opengl32.dll", EntryPoint = "wglMakeCurrent", ExactSpelling = true, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool WglMakeCurrent(nint hDc, nint hGlRc);
#endif  // SUPPORT_LIBRARY_IMPORT

            /// <summary>
            /// The <see cref="WglShareLists"/> function enables multiple OpenGL rendering contexts to share a single display-list space.
            /// </summary>
            /// <param name="glrcHandleFrom">Specifies the OpenGL rendering context with which to share display lists.</param>
            /// <param name="glrcHandleTo">Specifies the OpenGL rendering context to share display lists with <paramref name="glrcHandleFrom"/>.
            /// The <paramref name="glrcHandleTo"/> parameter should not contain any existing display lists when <see cref="WglShareLists(GLRenderContextHandle, GLRenderContextHandle)"/> is called.</param>
            /// <returns>
            /// <para>When the function succeeds, the return value is true.</para>
            /// <para>When the function fails, the return value is false and the display lists are not shared.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglsharelists"/></para>
            /// <para>When you create an OpenGL rendering context, it has its own display-list space.
            /// The <see cref="WglShareLists(GLRenderContextHandle, GLRenderContextHandle)"/> function enables a rendering context to share the display-list space of another rendering context;
            /// any number of rendering contexts can share a single display-list space.
            /// Once a rendering context shares a display-list space, the rendering context always uses the display-list space until the rendering context is deleted.
            /// When the last rendering context of a shared display-list space is deleted, the shared display-list space is deleted.
            /// All the indexes and definitions of display lists in a shared display-list space are shared.</para>
            /// <para>You can only share display lists with rendering contexts within the same process.
            /// However, not all rendering contexts in a process can share display lists.
            /// Rendering contexts can share display lists only if they use the same implementation of OpenGL functions.
            /// All client rendering contexts of a given pixel format can always share display lists.</para>
            /// <para>All rendering contexts of a shared display list must use an identical pixel format.
            /// Otherwise the results depend on the implementation of OpenGL used.</para>
            /// </remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("opengl32.dll", EntryPoint = "wglShareLists", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static partial bool WglShareLists(GLRenderContextHandle glrcHandleFrom, GLRenderContextHandle glrcHandleTo);
#else
            [DllImport("opengl32.dll", EntryPoint = "wglShareLists", ExactSpelling = true, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool WglShareLists(GLRenderContextHandle glrcHandleFrom, GLRenderContextHandle glrcHandleTo);
#endif  // SUPPORT_LIBRARY_IMPORT

            /// <summary>
            /// The <see cref="WglGetProcAddress(string)"/> function returns the address of an OpenGL extension function
            /// for use with the current OpenGL rendering context.
            /// </summary>
            /// <param name="name">The name of the extension function.
            /// The name of the extension function must be identical to a corresponding function implemented by OpenGL.</param>
            /// <returns>
            /// <para>When the function succeeds, the return value is the address of the extension function.</para>
            /// <para>When no current rendering context exists or the function fails, the return value is ZERO.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// </returns>
            /// <remarks>
            /// <para><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglgetprocaddress"/></para>
            /// <para>The OpenGL library supports multiple implementations of its functions.
            /// Extension functions supported in one rendering context are not necessarily available in a separate rendering context.
            /// Thus, for a given rendering context in an application, use the function addresses returned by the <see cref="WglGetProcAddress(string)"/> function only.</para>
            /// <para>The spelling and the case of the extension function pointed to by <paramref name="name"/> must be identical to that of a function supported and implemented by OpenGL.
            /// Because extension functions are not exported by OpenGL, you must use <see cref="WglGetProcAddress(string)"/> to get the addresses of vendor-specific extension functions.</para>
            /// <para>The extension function addresses are unique for each pixel format.
            /// All rendering contexts of a given pixel format share the same extension function addresses.</para>
            /// </remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("opengl32.dll", EntryPoint = "wglGetProcAddress", StringMarshalling = StringMarshalling.Utf8, SetLastError = true)]
            public static partial nint WglGetProcAddress(string name);
#else
            [DllImport("opengl32.dll", EntryPoint = "wglGetProcAddress", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint WglGetProcAddress(string name);
#endif  // SUPPORT_LIBRARY_IMPORT
        }
    }
}
