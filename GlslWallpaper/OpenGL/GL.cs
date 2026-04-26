#if NET7_0_OR_GREATER
#    define SUPPORT_LIBRARY_IMPORT
#endif  // NET7_0_OR_GREATER
// #define DISABLE_FUNCTION_POINTER

using System;
#if DISABLE_FUNCTION_POINTER
using System.Runtime.InteropServices;
#endif  // DISABLE_FUNCTION_POINTER
using System.Text;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Provides OpenGL functions.
    /// </summary>
#if SUPPORT_LIBRARY_IMPORT
    internal static unsafe partial class GL
#else
    internal static unsafe class GL
#endif  // SUPPORT_LIBRARY_IMPORT
    {
#if DISABLE_FUNCTION_POINTER
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLGetIntegervAction(uint pName, out int data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate sbyte* GLGetStringFunc(uint pName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLClearAction(uint mask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLClearColorAction(float r, float g, float b, float a);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLViewportAction(int x, int y, int w, int h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLFlushAction();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint GLGetErrorFunc();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint GLCreateShaderFunc(uint type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLDeleteShaderAction(uint shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLShaderSourceAction(uint s, int c, byte** pSourceArray, int* pLengthArray);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLGetShaderivAction(uint shader, uint pname, nint pParam);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLGetShaderInfoLogAction(uint shader, int maxLength, out int length, sbyte* pInfoLog);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLCompileShaderAction(uint s);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint GLCreateProgramFunc();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLDeleteProgramAction(uint program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLAttachShaderAction(uint p, uint s);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLLinkProgramAction(uint p);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLGetProgramivAction(uint program, uint pname, nint pParam);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLGetProgramInfoLogAction(uint program, int maxLength, out int length, sbyte *pInfoLog);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUseProgramAction(uint p);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLGenBuffersAction(int n, uint *pBuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLDeleteBuffersAction(int n, uint *pBuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLBindBufferAction(uint t, uint b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLBufferDataAction(uint t, nint size, nint data, uint usage);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLGenVertexArraysAction(int n, uint *pArrays);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLDeleteVertexArraysAction(int n, uint *pArrays);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLBindVertexArrayAction(uint a);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLEnableVertexAttribArrayAction(uint i);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLVertexAttribPointerAction(uint i, int s, uint t, int n, int stride, nint ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLDrawElementsAction(uint mode, int count, uint type, nint indices);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GLGetAttribLocationFunc(uint program, nint pName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GLGetUniformLocationFunc(uint program, nint pName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUniform1fAction(int location, float v0);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUniform2fAction(int location, float v0, float v1);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUniform3fAction(int location, float v0, float v1, float v2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUniform4fAction(int location, float v0, float v1, float v2, float v3);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUniform1iAction(int location, int v0);
#endif  // DISABLE_FUNCTION_POINTER

#if DISABLE_FUNCTION_POINTER
        private static GLGetIntegervAction? _glGetIntegerv;
        private static GLGetStringFunc? _glGetString;
        private static GLClearAction _glClear = (_) => throw new GLFunctionNotLoadedException("glClear");
        private static GLClearColorAction _glClearColor = (_, _, _, _) => throw new GLFunctionNotLoadedException("glClearColor");
        private static GLViewportAction _glViewport = (_, _, _, _) => throw new GLFunctionNotLoadedException("glViewport");
        private static GLFlushAction _glFlush = () => throw new GLFunctionNotLoadedException("glFlush");
        private static GLGetErrorFunc _glGetError = () => throw new GLFunctionNotLoadedException("glGetError");
        private static GLCreateShaderFunc _glCreateShader = (_) => throw new GLCompilationException("glCreateShader");
        private static GLDeleteShaderAction _glDeleteShader = (_) => throw new GLFunctionNotLoadedException("glDeleteShader");
        private static GLShaderSourceAction _glShaderSource = (_, _, _, _) => throw new GLFunctionNotLoadedException("glShaderSource");
        private static GLCompileShaderAction _glCompileShader = (_) => throw new GLFunctionNotLoadedException("glCompileShader");
        private static GLGetShaderivAction _glGetShaderiv = (_, _, _) => throw new GLFunctionNotLoadedException("glGetShaderiv");
        private static GLGetShaderInfoLogAction _glGetShaderInfoLog = (_, _, out _, _) => throw new GLFunctionNotLoadedException("glGetShaderInfoLog");
        private static GLCreateProgramFunc _glCreateProgram = () => throw new GLFunctionNotLoadedException("glCreateProgram");
        private static GLDeleteProgramAction _glDeleteProgram = (_) => throw new GLFunctionNotLoadedException("glDeleteProgram");
        private static GLAttachShaderAction _glAttachShader = (_, _) => throw new GLFunctionNotLoadedException("glAttachShader");
        private static GLLinkProgramAction _glLinkProgram = (_) => throw new GLFunctionNotLoadedException("glLinkProgram");
        private static GLGetProgramivAction _glGetProgramiv = (_, _, _) => throw new GLFunctionNotLoadedException("glGetProgramiv");
        private static GLGetProgramInfoLogAction _glGetProgramInfoLog = (_, _, out _, _) => throw new GLFunctionNotLoadedException("glGetProgramInfoLog");
        private static GLUseProgramAction _glUseProgram = (_) => throw new GLFunctionNotLoadedException("glUseProgram");
        private static GLGenBuffersAction _glGenBuffers = (_, _) => throw new GLFunctionNotLoadedException("glGenBuffers");
        private static GLDeleteBuffersAction _glDeleteBuffers = (_, _) => throw new GLFunctionNotLoadedException("glDeleteBuffers");
        private static GLBindBufferAction _glBindBuffer = (_, _) => throw new GLFunctionNotLoadedException("glBindBuffer");
        private static GLBufferDataAction _glBufferData = (_, _, _, _) => throw new GLFunctionNotLoadedException("glBufferData");
        private static GLGenVertexArraysAction _glGenVertexArrays = (_, _) => throw new GLFunctionNotLoadedException("glGenVertexArrays");
        private static GLDeleteVertexArraysAction _glDeleteVertexArrays = (_, _) => throw new GLFunctionNotLoadedException("glDeleteVertexArrays");
        private static GLBindVertexArrayAction _glBindVertexArray = (_) => throw new GLFunctionNotLoadedException("glBindVertexArray");
        private static GLEnableVertexAttribArrayAction _glEnableVertexAttribArray = (_) => throw new GLFunctionNotLoadedException("glEnableVertexAttribArray");
        private static GLVertexAttribPointerAction _glVertexAttribPointer = (_, _, _, _, _, _) => throw new GLFunctionNotLoadedException("glVertexAttribPointer");
        private static GLDrawElementsAction _glDrawElements = (_, _, _, _) => throw new GLFunctionNotLoadedException("glDrawElements");
        private static GLGetAttribLocationFunc _glGetAttribLocation = (_, _) => throw new GLFunctionNotLoadedException("glGetAttribLocation");
        private static GLGetUniformLocationFunc _glGetUniformLocation = (_, _) => throw new GLFunctionNotLoadedException("glGetUniformLocation");
        private static GLUniform1fAction _glUniform1f = (_, _) => throw new GLFunctionNotLoadedException("glUniform1f");
        private static GLUniform2fAction _glUniform2f = (_, _, _) => throw new GLFunctionNotLoadedException("glUniform2f");
        private static GLUniform3fAction _glUniform3f = (_, _, _, _) => throw new GLFunctionNotLoadedException("glUniform3f");
        private static GLUniform4fAction _glUniform4f = (_, _, _, _, _) => throw new GLFunctionNotLoadedException("glUniform4f");
        private static GLUniform1iAction _glUniform1i = (_, _) => throw new GLFunctionNotLoadedException("glUniform1i");
#else
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGet.xhtml"><c>glGetIntegerv</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glgetintegerv"/>
        /// </remarks>
        private static delegate* unmanaged[Cdecl]<uint, out int, void> _glGetIntegerv;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetString.xhtml"><c>glGetString</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/ja-jp/windows/win32/opengl/glgetstring"/>
        /// </remarks>
        private static delegate* unmanaged[Cdecl]<uint, sbyte*> _glGetString;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glClear.xhtml"><c>glClear</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glclear"/>
        /// </remarks>
        private static delegate* unmanaged[Cdecl]<uint, void> _glClear;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glClearColor.xhtml"><c>glClearColor</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glclearcolor"/>
        /// </remarks>
        private static delegate* unmanaged[Cdecl]<float, float, float, float, void> _glClearColor;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glViewport.xhtml"><c>glViewport</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glviewport"/>
        /// </remarks>
        private static delegate* unmanaged[Cdecl]<int, int, int, int, void> _glViewport;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glFlush.xhtml"><c>glFlush</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glflush"/>
        /// </remarks>
        private static delegate* unmanaged[Cdecl]<void> _glFlush;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetError.xhtml"><c>glGetError</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glgeterror"/>
        /// </remarks>
        private static delegate* unmanaged[Cdecl]<uint> _glGetError;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCreateShader.xhtml"><c>glCreateShader</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, uint> _glCreateShader;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteShader.xhtml"><c>glDeleteShader</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, void> _glDeleteShader;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glShaderSource.xhtml"><c>glShaderSource</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, int, byte**, int*, void> _glShaderSource;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCompileShader.xhtml"><c>glCompileShader</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, void> _glCompileShader;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetShader.xhtml"><c>glGetShaderiv</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, uint, nint, void> _glGetShaderiv;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetShaderInfoLog.xhtml"><c>glGetShaderInfoLog</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, int, out int, sbyte*, void> _glGetShaderInfoLog;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCreateProgram.xhtml"><c>glCreateProgram</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint> _glCreateProgram;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteProgram.xhtml"><c>glDeleteProgram</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, void> _glDeleteProgram;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glAttachShader.xhtml"><c>glAttachShader</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, uint, void> _glAttachShader;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glLinkProgram.xhtml"><c>glLinkProgram</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, void> _glLinkProgram;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgram.xhtml"><c>glGetProgramiv</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, uint, nint, void> _glGetProgramiv;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgramInfoLog.xhtml"><c>glGetProgramInfoLog</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, int, out int, sbyte*, void> _glGetProgramInfoLog;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUseProgram.xhtml"><c>glUseProgram</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, void> _glUseProgram;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGenBuffers.xhtml"><c>glGenBuffers</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<int, uint*, void> _glGenBuffers;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteBuffers.xhtml"><c>glDeleteBuffers</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<int, uint*, void> _glDeleteBuffers;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindBuffer.xhtml"><c>glBindBuffer</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, uint, void> _glBindBuffer;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBufferData.xhtml"><c>glBufferData</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, nint, nint, uint, void> _glBufferData;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGenVertexArrays.xhtml"><c>glGenVertexArrays</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<int, uint*, void> _glGenVertexArrays;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteVertexArrays.xhtml"><c>_glDeleteVertexArrays</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<int, uint*, void> _glDeleteVertexArrays;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindVertexArray.xhtml"><c>glBindVertexArray</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, void> _glBindVertexArray;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glEnableVertexAttribArray.xhtml"><c>glEnableVertexAttribArray</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, void> _glEnableVertexAttribArray;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glVertexAttribPointer.xhtml"><c>glVertexAttribPointer</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, int, uint, int, int, nint, void> _glVertexAttribPointer;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDrawElements.xhtml"><c>glDrawElements</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/gldrawelements"/>
        /// </remarks>
        private static delegate* unmanaged[Cdecl]<uint, int, uint, nint, void> _glDrawElements;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetAttribLocation.xhtml"><c>glGetAttribLocation</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, nint, int> _glGetAttribLocation;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetUniformLocation.xhtml"><c>glGetUniformLocation</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<uint, nint, int> _glGetUniformLocation;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform1f</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<int, float, void> _glUniform1f;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform2f</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<int, float, float, void> _glUniform2f;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform3f</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<int, float, float, float, void> _glUniform3f;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform4f</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<int, float, float, float, float, void> _glUniform4f;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform1i</c></see>.
        /// </summary>
        private static delegate* unmanaged[Cdecl]<int, int, void> _glUniform1i;
#endif  // DISABLE_FUNCTION_POINTER

        /// <summary>
        /// Load OpenGL functions.
        /// </summary>
        public static void Initialize()
        {
#if DISABLE_FUNCTION_POINTER
            _glClear = WGL.LoadFunctionAsDelegate<GLClearAction>("glClear");
            _glClearColor = WGL.LoadFunctionAsDelegate<GLClearColorAction>("glClearColor");
            _glViewport = WGL.LoadFunctionAsDelegate<GLViewportAction>("glViewport");
            _glFlush = WGL.LoadFunctionAsDelegate<GLFlushAction>("glFlush");
            _glGetError = WGL.LoadFunctionAsDelegate<GLGetErrorFunc>("glGetError");
            _glCreateShader = WGL.LoadFunctionAsDelegate<GLCreateShaderFunc>("glCreateShader");
            _glDeleteShader = WGL.LoadFunctionAsDelegate<GLDeleteShaderAction>("glDeleteShader");
            _glShaderSource = WGL.LoadFunctionAsDelegate<GLShaderSourceAction>("glShaderSource");
            _glCompileShader = WGL.LoadFunctionAsDelegate<GLCompileShaderAction>("glCompileShader");
            _glGetShaderiv = WGL.LoadFunctionAsDelegate<GLGetShaderivAction>("glGetShaderiv");
            _glGetShaderInfoLog = WGL.LoadFunctionAsDelegate<GLGetShaderInfoLogAction>("glGetShaderInfoLog");
            _glCreateProgram = WGL.LoadFunctionAsDelegate<GLCreateProgramFunc>("glCreateProgram");
            _glDeleteProgram = WGL.LoadFunctionAsDelegate<GLDeleteProgramAction>("glDeleteProgram");
            _glAttachShader = WGL.LoadFunctionAsDelegate<GLAttachShaderAction>("glAttachShader");
            _glLinkProgram = WGL.LoadFunctionAsDelegate<GLLinkProgramAction>("glLinkProgram");
            _glGetProgramiv = WGL.LoadFunctionAsDelegate<GLGetProgramivAction>("glGetProgramiv");
            _glGetProgramInfoLog = WGL.LoadFunctionAsDelegate<GLGetProgramInfoLogAction>("glGetProgramInfoLog");
            _glUseProgram = WGL.LoadFunctionAsDelegate<GLUseProgramAction>("glUseProgram");
            _glGenBuffers = WGL.LoadFunctionAsDelegate<GLGenBuffersAction>("glGenBuffers");
            _glDeleteBuffers = WGL.LoadFunctionAsDelegate<GLDeleteBuffersAction>("glDeleteBuffers");
            _glBindBuffer = WGL.LoadFunctionAsDelegate<GLBindBufferAction>("glBindBuffer");
            _glBufferData = WGL.LoadFunctionAsDelegate<GLBufferDataAction>("glBufferData");
            _glGenVertexArrays = WGL.LoadFunctionAsDelegate<GLGenVertexArraysAction>("glGenVertexArrays");
            _glDeleteVertexArrays = WGL.LoadFunctionAsDelegate<GLDeleteVertexArraysAction>("glDeleteVertexArrays");
            _glBindVertexArray = WGL.LoadFunctionAsDelegate<GLBindVertexArrayAction>("glBindVertexArray");
            _glEnableVertexAttribArray = WGL.LoadFunctionAsDelegate<GLEnableVertexAttribArrayAction>("glEnableVertexAttribArray");
            _glVertexAttribPointer = WGL.LoadFunctionAsDelegate<GLVertexAttribPointerAction>("glVertexAttribPointer");
            _glDrawElements = WGL.LoadFunctionAsDelegate<GLDrawElementsAction>("glDrawElements");
            _glGetAttribLocation = WGL.LoadFunctionAsDelegate<GLGetAttribLocationFunc>("glGetAttribLocation");
            _glGetUniformLocation = WGL.LoadFunctionAsDelegate<GLGetUniformLocationFunc>("glGetUniformLocation");
            _glUniform1f = WGL.LoadFunctionAsDelegate<GLUniform1fAction>("glUniform1f");
            _glUniform2f = WGL.LoadFunctionAsDelegate<GLUniform2fAction>("glUniform2f");
            _glUniform3f = WGL.LoadFunctionAsDelegate<GLUniform3fAction>("glUniform3f");
            _glUniform4f = WGL.LoadFunctionAsDelegate<GLUniform4fAction>("glUniform4f");
            _glUniform1i = WGL.LoadFunctionAsDelegate<GLUniform1iAction>("glUniform1i");
#else
            _glClear = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glClear");
            _glClearColor = (delegate* unmanaged[Cdecl]<float, float, float, float, void>)WGL.LoadFunction("glClearColor");
            _glViewport = (delegate* unmanaged[Cdecl]<int, int, int, int, void>)WGL.LoadFunction("glViewport");
            _glFlush = (delegate* unmanaged[Cdecl]<void>)WGL.LoadFunction("glFlush");
            _glGetError = (delegate* unmanaged[Cdecl]<uint>)WGL.LoadFunction("glGetError");
            _glCreateShader = (delegate* unmanaged[Cdecl]<uint, uint>)WGL.LoadFunction("glCreateShader");
            _glDeleteShader = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glDeleteShader");
            _glShaderSource = (delegate* unmanaged[Cdecl]<uint, int, byte**, int*, void>)WGL.LoadFunction("glShaderSource");
            _glCompileShader = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glCompileShader");
            _glGetShaderiv = (delegate* unmanaged[Cdecl]<uint, uint, nint, void>)WGL.LoadFunction("glGetShaderiv");
            _glGetShaderInfoLog = (delegate* unmanaged[Cdecl]<uint, int, out int, sbyte*, void>)WGL.LoadFunction("glGetShaderInfoLog");
            _glCreateProgram = (delegate* unmanaged[Cdecl]<uint>)WGL.LoadFunction("glCreateProgram");
            _glDeleteProgram = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glDeleteProgram");
            _glAttachShader = (delegate* unmanaged[Cdecl]<uint, uint, void>)WGL.LoadFunction("glAttachShader");
            _glLinkProgram = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glLinkProgram");
            _glGetProgramiv = (delegate* unmanaged[Cdecl]<uint, uint, nint, void>)WGL.LoadFunction("glGetProgramiv");
            _glGetProgramInfoLog = (delegate* unmanaged[Cdecl]<uint, int, out int, sbyte*, void>)WGL.LoadFunction("glGetProgramInfoLog");
            _glUseProgram = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glUseProgram");
            _glGenBuffers = (delegate* unmanaged[Cdecl]<int, uint*, void>)WGL.LoadFunction("glGenBuffers");
            _glDeleteBuffers = (delegate* unmanaged[Cdecl]<int, uint*, void>)WGL.LoadFunction("glDeleteBuffers");
            _glBindBuffer = (delegate* unmanaged[Cdecl]<uint, uint, void>)WGL.LoadFunction("glBindBuffer");
            _glBufferData = (delegate* unmanaged[Cdecl]<uint, nint, nint, uint, void>)WGL.LoadFunction("glBufferData");
            _glGenVertexArrays = (delegate* unmanaged[Cdecl]<int, uint*, void>)WGL.LoadFunction("glGenVertexArrays");
            _glDeleteVertexArrays = (delegate* unmanaged[Cdecl]<int, uint*, void>)WGL.LoadFunction("glDeleteVertexArrays");
            _glBindVertexArray = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glBindVertexArray");
            _glEnableVertexAttribArray = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glEnableVertexAttribArray");
            _glVertexAttribPointer = (delegate* unmanaged[Cdecl]<uint, int, uint, int, int, nint, void>)WGL.LoadFunction("glVertexAttribPointer");
            _glDrawElements = (delegate* unmanaged[Cdecl]<uint, int, uint, nint, void>)WGL.LoadFunction("glDrawElements");
            _glGetAttribLocation = (delegate* unmanaged[Cdecl]<uint, nint, int>)WGL.LoadFunction("glGetAttribLocation");
            _glGetUniformLocation = (delegate* unmanaged[Cdecl]<uint, nint, int>)WGL.LoadFunction("glGetUniformLocation");
            _glUniform1f = (delegate* unmanaged[Cdecl]<int, float, void>)WGL.LoadFunction("glUniform1f");
            _glUniform2f = (delegate* unmanaged[Cdecl]<int, float, float, void>)WGL.LoadFunction("glUniform2f");
            _glUniform3f = (delegate* unmanaged[Cdecl]<int, float, float, float, void>)WGL.LoadFunction("glUniform3f");
            _glUniform4f = (delegate* unmanaged[Cdecl]<int, float, float, float, float, void>)WGL.LoadFunction("glUniform4f");
            _glUniform1i = (delegate* unmanaged[Cdecl]<int, int, void>)WGL.LoadFunction("glUniform1i");
#endif  // DISABLE_FUNCTION_POINTER
        }

        /// <summary>
        /// Return the integer value of a selected parameter.
        /// </summary>
        /// <param name="pName">the parameter value to be returned for non-indexed versions of glGet.</param>
        /// <returns>The integer value of a selected parameter.</returns>
        public static int GetIntegerv(uint pName)
        {
            if (_glGetIntegerv == null)
            {
#if DISABLE_FUNCTION_POINTER
                _glGetIntegerv = WGL.LoadFunctionAsDelegate<GLGetIntegervAction>("glGetIntegerv");
#else
                _glGetIntegerv = (delegate* unmanaged[Cdecl]<uint, out int, void>)WGL.LoadFunction("glGetIntegerv");
#endif  // DISABLE_FUNCTION_POINTER
            }
            _glGetIntegerv(pName, out var val);
            return val;
        }

        /// <summary>
        /// Return a string describing the current GL connection.
        /// </summary>
        /// <param name="pName">Specifies a symbolic constant, one of GL_VENDOR, GL_RENDERER, <see cref="GLConst.GL_VERSION"/>, or GL_SHADING_LANGUAGE_VERSION.</param>
        /// <returns></returns>
        public static string GetString(uint pName)
        {
            if (_glGetString == null)
            {
#if DISABLE_FUNCTION_POINTER
                _glGetString = WGL.LoadFunctionAsDelegate<GLGetStringFunc>("glGetString");
#else
                _glGetString = (delegate* unmanaged[Cdecl]<uint, sbyte*>)WGL.LoadFunction("glGetString");
#endif  // DISABLE_FUNCTION_POINTER
            }
            return new string(_glGetString(pName));
        }

        /// <summary>
        /// Clear buffers to preset values.
        /// </summary>
        /// <param name="mask">Bitwise OR of masks that indicate the buffers to be cleared.
        /// The three masks are <see cref="GLConst.GL_COLOR_BUFFER_BIT"/>, <see cref="GLConst.GL_DEPTH_BUFFER_BIT"/>, and <see cref="GLConst.GL_STENCIL_BUFFER_BIT"/>.</param>
        public static void Clear(uint mask)
        {
            _glClear(mask);
        }

        /// <summary>
        /// Specify clear values for the color buffers.
        /// </summary>
        /// <param name="r">The red value used when the color buffers are cleared. The initial value is 0.</param>
        /// <param name="g">The green value used when the color buffers are cleared. The initial value is 0.</param>
        /// <param name="b">The blue value used when the color buffers are cleared. The initial value is 0.</param>
        /// <param name="a">The alpha value used when the color buffers are cleared. The initial value is 0.</param>
        public static void ClearColor(float r, float g, float b, float a)
        {
            _glClearColor(r, g, b, a);
        }

        /// <summary>
        /// Set the viewport.
        /// </summary>
        /// <param name="x">Specify the x-coordinate of the lower left corner of the viewport rectangle, in pixels. The initial value is 0.</param>
        /// <param name="y">Specify the y-coordinate of the lower left corner of the viewport rectangle, in pixels. The initial value is 0.</param>
        /// <param name="w">Specify the width of the viewport. When a GL context is first attached to a window, width is set to the dimensions of that window.</param>
        /// <param name="h">Specify the height of the viewport. When a GL context is first attached to a window, height is set to the dimensions of that window.</param>
        public static void Viewport(int x, int y, int w, int h)
        {
            _glViewport(x, y, w, h);
        }

        /// <summary>
        /// Force execution of GL commands in finite time.
        /// </summary>
        public static void Flush()
        {
            _glFlush();
        }

        /// <summary>
        /// Return error information.
        /// </summary>
        /// <returns>The value of the error flag.</returns>
        public static uint GetError()
        {
            return _glGetError();
        }

        /// <summary>
        /// Creates a shader object.
        /// </summary>
        /// <param name="shaderType">the type of shader to be created.
        /// Must be one of GL_COMPUTE_SHADER, <see cref="GLConst.GL_VERTEX_SHADER"/>, GL_TESS_CONTROL_SHADER, GL_TESS_EVALUATION_SHADER,
        /// GL_GEOMETRY_SHADER, or <see cref="GLConst.GL_FRAGMENT_SHADER"/>.</param>
        /// <returns>The handle of the created shader object.</returns>
        public static uint CreateShader(uint shaderType)
        {
            return _glCreateShader(shaderType);
        }

        /// <summary>
        /// Deletes a shader object.
        /// </summary>
        /// <param name="shader">The handle of the shader object to be deleted.</param>
        public static void DeleteShader(uint shader)
        {
            _glDeleteShader(shader);
        }

        /// <summary>
        /// Replaces the source code in a shader object.
        /// </summary>
        /// <param name="shader">The handle of the shader object whose source code is to be replaced.</param>
        /// <param name="source">The source code to be loaded into the shader.</param>
        public static void ShaderSource(GLShader shader, string source)
        {
            var sourceUtf8Bytes = Encoding.UTF8.GetBytes(source);
            unsafe
            {
                fixed (byte* pSourceUtf8Bytes = &sourceUtf8Bytes[0])
                {
                    var length = sourceUtf8Bytes.Length;
                    _glShaderSource(shader.Handle, 1, &pSourceUtf8Bytes, &length);
                }
            }
        }

        /// <summary>
        /// Compiles a shader object.
        /// </summary>
        /// <param name="shader">The shader object to be compiled.</param>
        public static void CompileShader(GLShader shader)
        {
            _glCompileShader(shader.Handle);
        }

        /// <summary>
        /// Returns a parameter from a shader object.
        /// </summary>
        /// <param name="shader">The shader object to be queried.</param>
        /// <param name="pname">The object parameter.
        /// Accepted symbolic names are GL_SHADER_TYPE, GL_DELETE_STATUS, <see cref="GLConst.GL_COMPILE_STATUS"/>,
        /// <see cref="GLConst.GL_INFO_LOG_LENGTH"/>, GL_SHADER_SOURCE_LENGTH.</param>
        /// <param name="pParam">The requested object parameter.</param>
        public static void GetShaderiv(GLShader shader, uint pname, nint pParam)
        {
            _glGetShaderiv(shader.Handle, pname, pParam);
        }

        /// <summary>
        /// Get shader information log.
        /// </summary>
        /// <param name="shader">The shader object to be queried.</param>
        /// <returns>The shader information log.</returns>
        public static string? GetShaderInfoLog(GLShader shader)
        {
            int infoLogLength;
            GetShaderiv(shader, GLConst.GL_INFO_LOG_LENGTH, (nint)(&infoLogLength));
            if (infoLogLength == 0)
            {
                return null;
            }

            if (infoLogLength < 8192)
            {
                unsafe
                {
                    var pLogBuffer = stackalloc sbyte[8192];
                    _glGetShaderInfoLog(shader.Handle, 8192, out var length, pLogBuffer);
                    return new string(pLogBuffer);
                }
            }
            else
            {
                var logBuffer = new sbyte[infoLogLength];
                unsafe
                {
                    fixed (sbyte *pLogBuffer = &logBuffer[0])
                    {
                        _glGetShaderInfoLog(shader.Handle, logBuffer.Length, out var length, pLogBuffer);
                        return new string(pLogBuffer);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a program object.
        /// </summary>
        /// <returns>The handle of the program object.</returns>
        public static uint CreateProgram()
        {
            return _glCreateProgram();
        }

        /// <summary>
        /// Deletes a program object.
        /// </summary>
        /// <param name="program">The handle of the program object.</param>
        public static void DeleteProgram(uint program)
        {
            _glDeleteProgram(program);
        }

        /// <summary>
        /// Attaches a shader object to a program object.
        /// </summary>
        /// <param name="program">The program object to which a shader object will be attached.</param>
        /// <param name="shader">The shader object that is to be attached.</param>
        public static void AttachShader(GLProgram program, GLShader shader)
        {
            _glAttachShader(program.Handle, shader.Handle);
        }

        /// <summary>
        /// Links a program object.
        /// </summary>
        /// <param name="program">The handle of the program object to be linked.</param>
        public static void LinkProgram(GLProgram program)
        {
            _glLinkProgram(program.Handle);
        }

        /// <summary>
        /// Returns a parameter from a program object.
        /// </summary>
        /// <param name="program">The program object to be queried.</param>
        /// <param name="pName">Specifies the object parameter.</param>
        /// <param name="pParam">The destination of the requested object parameter.</param>
        public static void GetProgramiv(GLProgram program, uint pName, nint pParam)
        {
            _glGetProgramiv(program.Handle, pName, pParam);
        }

        /// <summary>
        /// Get program information log.
        /// </summary>
        /// <param name="program">The program object to be queried.</param>
        /// <returns>The program information log.</returns>
        public static string? GetProgramInfoLog(GLProgram program)
        {
            int infoLogLength;
            GetProgramiv(program, GLConst.GL_INFO_LOG_LENGTH, (nint)(&infoLogLength));
            if (infoLogLength == 0)
            {
                return null;
            }

            if (infoLogLength < 8192)
            {
                unsafe
                {
                    var pLogBuffer = stackalloc sbyte[8192];
                    _glGetProgramInfoLog(program.Handle, 8192, out var length, pLogBuffer);
                    return new string(pLogBuffer);
                }
            }
            else
            {
                var logBuffer = new sbyte[infoLogLength];
                unsafe
                {
                    fixed (sbyte *pLogBuffer = &logBuffer[0])
                    {
                        _glGetProgramInfoLog(program.Handle, logBuffer.Length, out var length, pLogBuffer);
                        return new string(pLogBuffer);
                    }
                }
            }
        }

        /// <summary>
        /// Installs a program object as part of current rendering state.
        /// </summary>
        /// <param name="program">The handle of the program object whose executables are to be used as part of current rendering state.</param>
        public static void UseProgram(GLProgram program)
        {
            _glUseProgram(program.Handle);
        }

        /// <summary>
        /// Generate buffer object names.
        /// </summary>
        /// <param name="buffers">An array in which the generated buffer object names are stored.</param>
        /// <exception cref="GLException">Thrown when failed to create OpenGL buffers.</exception>
        public static void GenBuffers(uint[] buffers)
        {
            unsafe
            {
                fixed (uint* pBuffers = &buffers[0])
                {
                    _glGenBuffers(buffers.Length, pBuffers);
                }
            }
            foreach (var buffer in buffers)
            {
                if (buffer == 0 || buffer == GLConst.GL_INVALID_VALUE)
                {
                    throw new GLException("Failed to create OpenGL buffers.");
                }
            }
        }

        /// <summary>
        /// Delete named buffer objects.
        /// </summary>
        /// <param name="buffers">An array of buffer objects to be deleted.</param>
        public static void DeleteBuffers(uint[] buffers)
        {
            unsafe
            {
                fixed (uint* pBuffers = &buffers[0])
                {
                    _glDeleteBuffers(buffers.Length, pBuffers);
                }
            }
        }

        /// <summary>
        /// Bind a named buffer object.
        /// </summary>
        /// <param name="target">The target to which the buffer object is bound.</param>
        /// <param name="buffer">The name of a buffer object.</param>
        public static void BindBuffer(uint target, uint buffer)
        {
            _glBindBuffer(target, buffer);
        }

        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <param name="target">The target to which the buffer object is bound for <see cref="_glBufferData"/>.</param>
        /// <param name="size">The size in bytes of the buffer object's new data store.</param>
        /// <param name="data">A pointer to data that will be copied into the data store for initialization, or NULL if no data is to be copied.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public static void BufferData(uint target, int size, byte[] data, uint usage)
        {
            unsafe
            {
                fixed (byte* pData = &data[0])
                {
                    _glBufferData(target, (nint)size, (nint)pData, usage);
                }
            }
        }

        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <param name="target">The target to which the buffer object is bound for <see cref="_glBufferData"/>.</param>
        /// <param name="size">The size in bytes of the buffer object's new data store.</param>
        /// <param name="data">A pointer to data that will be copied into the data store for initialization, or NULL if no data is to be copied.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public static void BufferData(uint target, int size, ushort[] data, uint usage)
        {
            unsafe
            {
                fixed (ushort* pData = &data[0])
                {
                    _glBufferData(target, (nint)size, (nint)pData, usage);
                }
            }
        }

        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <param name="target">The target to which the buffer object is bound for <see cref="_glBufferData"/>.</param>
        /// <param name="size">The size in bytes of the buffer object's new data store.</param>
        /// <param name="data">A pointer to data that will be copied into the data store for initialization, or NULL if no data is to be copied.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public static void BufferData(uint target, int size, float[] data, uint usage)
        {
            unsafe
            {
                fixed (float* pData = &data[0])
                {
                    _glBufferData(target, (nint)size, (nint)pData, usage);
                }
            }
        }

        /// <summary>
        /// Generate vertex array object names.
        /// </summary>
        /// <param name="vertexArray">An array in which the generated vertex array object names are stored.</param>
        /// <exception cref="GLException">Thrown when failed to create vertex array object.</exception>
        public static void GenVertexArrays(uint[] vertexArray)
        {
            unsafe
            {
                fixed (uint* pVertexArray = &vertexArray[0])
                {
                    _glGenVertexArrays(vertexArray.Length, pVertexArray);
                }
            }
            foreach (var va in vertexArray)
            {
                if (va == 0 || va == GLConst.GL_INVALID_VALUE)
                {
                    throw new GLException("Failed to create GL vertex array.");
                }
            }
        }

        /// <summary>
        /// Delete vertex array objects.
        /// </summary>
        /// <param name="vertexArray">An array containing the n names of the objects to be deleted.</param>
        public static void DeleteVertexArrays(uint[] vertexArray)
        {
            unsafe
            {
                fixed (uint* pVertexArray = &vertexArray[0])
                {
                    _glDeleteVertexArrays(vertexArray.Length, pVertexArray);
                }
            }
        }

        /// <summary>
        /// Bind a vertex array object.
        /// </summary>
        /// <param name="arrayHandle">The name of the vertex array to bind.</param>
        public static void BindVertexArray(uint arrayHandle)
        {
            _glBindVertexArray(arrayHandle);
        }

        /// <summary>
        /// Enable or disable a generic vertex attribute array.
        /// </summary>
        /// <param name="index">The index of the generic vertex attribute to be enabled.</param>
        public static void EnableVertexAttribArray(uint index)
        {
            _glEnableVertexAttribArray(index);
        }

        /// <summary>
        /// Define an array of generic vertex attribute data.
        /// </summary>
        /// <param name="index">The index of the generic vertex attribute to be modified.</param>
        /// <param name="size">The number of components per generic vertex attribute.
        /// Must be 1, 2, 3, 4. Additionally, the symbolic constant GL_BGRA is accepted by <see cref="_glVertexAttribPointer"/>.
        /// The initial value is 4.</param>
        /// <param name="type">Specifies the data type of each component in the array.</param>
        /// <param name="normalized">Specifies whether fixed-point data values should be normalized (<see cref="GLConst.GL_TRUE"/>) or converted directly
        /// as fixed-point values (<see cref="GLConst.GL_FALSE"/>) when they are accessed.</param>
        /// <param name="stride">The byte offset between consecutive generic vertex attributes.
        /// If stride is 0, the generic vertex attributes are understood to be tightly packed in the array.
        /// The initial value is 0.</param>
        /// <param name="ptr">The offset of the first component of the first generic vertex attribute in the array
        /// in the data store of the buffer currently bound to the <see cref="GLConst.GL_ARRAY_BUFFER"/> target.
        /// The initial value is 0.</param>
        public static void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, nint ptr)
        {
            _glVertexAttribPointer(index, size, type, normalized ? 1 : 0, stride, ptr);
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">What kind of primitives to render.</param>
        /// <param name="count">The number of elements to be rendered.</param>
        /// <param name="type">the type of the values in indices.
        /// Must be one of <see cref="GLConst.GL_UNSIGNED_BYTE"/>, <see cref="GLConst.GL_UNSIGNED_SHORT"/>, or <see cref="GLConst.GL_UNSIGNED_INT"/>.</param>
        public static void DrawElements(uint mode, int count, uint type)
        {
            _glDrawElements(mode, count, type, 0);
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">What kind of primitives to render.</param>
        /// <param name="count">The number of elements to be rendered.</param>
        /// <param name="indices">A byte offset (cast to a pointer type) into the buffer bound to <see cref="GLConst.GL_ELEMENT_ARRAY_BUFFER"/>to start reading indices from.</param>
        public static void DrawElements(uint mode, int count, byte[] indices)
        {
            unsafe
            {
                fixed (byte* pIndices = &indices[0])
                {
                    _glDrawElements(mode, count, GLConst.GL_UNSIGNED_BYTE, (nint)pIndices);
                }
            }
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">What kind of primitives to render.</param>
        /// <param name="count">The number of elements to be rendered.</param>
        /// <param name="indices">A byte offset (cast to a pointer type) into the buffer bound to <see cref="GLConst.GL_ELEMENT_ARRAY_BUFFER"/>to start reading indices from.</param>
        public static void DrawElements(uint mode, int count, ushort[] indices)
        {
            unsafe
            {
                fixed (ushort* pIndices = &indices[0])
                {
                    _glDrawElements(mode, count, GLConst.GL_UNSIGNED_SHORT, (nint)pIndices);
                }
            }
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">What kind of primitives to render.</param>
        /// <param name="count">The number of elements to be rendered.</param>
        /// <param name="indices">A byte offset (cast to a pointer type) into the buffer bound to <see cref="GLConst.GL_ELEMENT_ARRAY_BUFFER"/>to start reading indices from.</param>
        public static void DrawElements(uint mode, int count, uint[] indices)
        {
            unsafe
            {
                fixed (uint* pIndices = &indices[0])
                {
                    _glDrawElements(mode, count, GLConst.GL_UNSIGNED_INT, (nint)pIndices);
                }
            }
        }

        /// <summary>
        /// Returns the location of an attribute variable.
        /// </summary>
        /// <param name="program">The program object to be queried.</param>
        /// <param name="name">Points to a null terminated string containing the name of the attribute variable whose location is to be queried.</param>
        /// <returns>The location of an attribute variable.</returns>
        public static int GetAttribLocation(GLProgram program, string name)
        {
            unsafe
            {
                fixed (byte* pName = &Encoding.ASCII.GetBytes(name)[0])
                {
                    return _glGetAttribLocation(program.Handle, (nint)pName);
                }
            }
        }

        /// <summary>
        /// Returns the location of a uniform variable
        /// </summary>
        /// <param name="program">The program object to be queried.</param>
        /// <param name="name">Points to a null terminated string containing the name of the uniform variable whose location is to be queried.</param>
        /// <returns>The location of a uniform variable.</returns>
        public static int GetUniformLocation(GLProgram program, string name)
        {
            unsafe
            {
                fixed (byte* pName = &Encoding.ASCII.GetBytes(name)[0])
                {
                    return _glGetUniformLocation(program.Handle, (nint)pName);
                }
            }
        }

        /// <summary>
        /// Specify the values of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">The location of the uniform variable to be modified.</param>
        /// <param name="v0">The new values to be used for the specified uniform variable.</param>
        public static void Uniform(int location, float v0)
        {
            _glUniform1f(location, v0);
        }

        /// <summary>
        /// Specify the values of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">The location of the uniform variable to be modified.</param>
        /// <param name="v0">The first new values to be used for the specified uniform variable.</param>
        /// <param name="v1">The second new values to be used for the specified uniform variable.</param>
        public static void Uniform(int location, float v0, float v1)
        {
            _glUniform2f(location, v0, v1);
        }

        /// <summary>
        /// Specify the values of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">The location of the uniform variable to be modified.</param>
        /// <param name="v0">The first new values to be used for the specified uniform variable.</param>
        /// <param name="v1">The second new values to be used for the specified uniform variable.</param>
        /// <param name="v2">The third new values to be used for the specified uniform variable.</param>
        public static void Uniform(int location, float v0, float v1, float v2)
        {
            _glUniform3f(location, v0, v1, v2);
        }

        /// <summary>
        /// Specify the values of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">The location of the uniform variable to be modified.</param>
        /// <param name="v0">The first new values to be used for the specified uniform variable.</param>
        /// <param name="v1">The second new values to be used for the specified uniform variable.</param>
        /// <param name="v2">The third new values to be used for the specified uniform variable.</param>
        /// <param name="v3">The fourth new values to be used for the specified uniform variable.</param>
        public static void Uniform(int location, float v0, float v1, float v2, float v3)
        {
            _glUniform4f(location, v0, v1, v2, v3);
        }

        /// <summary>
        /// Specify the values of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">The location of the uniform variable to be modified.</param>
        /// <param name="v0">The new values to be used for the specified uniform variable.</param>
        public static void Uniform(int location, int v0)
        {
            _glUniform1i(location, v0);
        }

        /// <summary>
        /// Get OpenGL version.
        /// </summary>
        /// <param name="major">Major version.</param>
        /// <param name="minor">Minor version.</param>
        public static void GetVertsion(out int major, out int minor)
        {
            try
            {
                major = GetIntegerv(GLConst.GL_MAJOR_VERSION);
                minor = GetIntegerv(GLConst.GL_MINOR_VERSION);
            }
            catch
            {
                var nums = GetString(GLConst.GL_VERSION).Split(' ')[0].Split('.');
                major = int.Parse(nums[0]);
                minor = int.Parse(nums[1]);
            }
        }

        /// <summary>
        /// Create <see cref="GLBuffer"/> instance and transfer specified vertex data.
        /// </summary>
        /// <param name="vertices">Vertex positions.</param>
        /// <returns>Created <see cref="GLBuffer"/> instance.</returns>
        public static GLBuffer CreateVertexBufferObject(float[] vertices)
        {
            var vbo = new GLBuffer(1);
            BindBuffer(GLConst.GL_ARRAY_BUFFER, vbo[0]);
            BufferData(GLConst.GL_ARRAY_BUFFER, vertices.Length * sizeof(float), vertices, GLConst.GL_STATIC_DRAW);
            BindBuffer(GLConst.GL_ARRAY_BUFFER, 0);
            return vbo;
        }

        /// <summary>
        /// Create <see cref="GLBuffer"/> instance and transfer specified index data.
        /// </summary>
        /// <param name="triangles">Polygon index data.</param>
        /// <returns>Created <see cref="GLBuffer"/> instance.</returns>
        public static GLBuffer CreateIndexBufferObject(byte[] triangles)
        {
            var ibo = new GLBuffer(1);
            BindBuffer(GLConst.GL_ELEMENT_ARRAY_BUFFER, ibo[0]);
            BufferData(GLConst.GL_ELEMENT_ARRAY_BUFFER, triangles.Length * sizeof(byte), triangles, GLConst.GL_STATIC_DRAW);
            BindBuffer(GLConst.GL_ELEMENT_ARRAY_BUFFER, 0);
            return ibo;
        }

        /// <summary>
        /// Create <see cref="GLBuffer"/> instance and transfer specified index data.
        /// </summary>
        /// <param name="triangles">Polygon index data.</param>
        /// <returns>Created <see cref="GLBuffer"/> instance.</returns>
        public static GLBuffer CreateIndexBufferObject(ushort[] triangles)
        {
            var ibo = new GLBuffer(1);
            BindBuffer(GLConst.GL_ELEMENT_ARRAY_BUFFER, ibo[0]);
            BufferData(GLConst.GL_ELEMENT_ARRAY_BUFFER, triangles.Length * sizeof(ushort), triangles, GLConst.GL_STATIC_DRAW);
            BindBuffer(GLConst.GL_ELEMENT_ARRAY_BUFFER, 0);
            return ibo;
        }

        /// <summary>
        /// Compile specified source code as vertex shader.
        /// </summary>
        /// <param name="source">Vertex shader source.</param>
        /// <param name="warnMessage">Warning message.</param>
        /// <returns>Created <see cref="GLVertexShader"/> instance.</returns>
        /// <exception cref="GLCompilationException">Thrown when compilation error occured.</exception>
        public static GLVertexShader CompileVertexShader(string source, out string? warnMessage)
        {
            var shader = new GLVertexShader();
            ShaderSource(shader, source);
            CompileShader(shader);

            int compileResult;
            GetShaderiv(shader, GLConst.GL_COMPILE_STATUS, (nint)(&compileResult));

            if (compileResult == 0)
            {
                using (shader)
                {
                    throw new GLCompilationException(GetShaderInfoLog(shader));
                }
            }

            warnMessage = GetShaderInfoLog(shader);

            return shader;
        }

        /// <summary>
        /// Compile specified source code as fragment shader.
        /// </summary>
        /// <param name="source">Fragment shader source.</param>
        /// <param name="warnMessage">Warning message.</param>
        /// <returns>Created <see cref="GLVertexShader"/> instance.</returns>
        /// <exception cref="GLCompilationException">Thrown when compilation error occured.</exception>
        public static GLFragmentShader CompileFragmentShader(string source, out string? warnMessage)
        {
            var shader = new GLFragmentShader();
            ShaderSource(shader, source);
            CompileShader(shader);

            int compileResult;
            GetShaderiv(shader, GLConst.GL_COMPILE_STATUS, (nint)(&compileResult));

            if (compileResult == 0)
            {
                using (shader)
                {
                    throw new GLCompilationException(GetShaderInfoLog(shader));
                }
            }

            warnMessage = GetShaderInfoLog(shader);

            return shader;
        }

        /// <summary>
        /// Create <see cref="GLProgram"/> and link vertex shader and fragment shader.
        /// </summary>
        /// <param name="vertexShader">Vertex shader.</param>
        /// <param name="fragmentShader">Fragment shader.</param>
        /// <param name="warnMessage">Warning message.</param>
        /// <returns>Linked program.</returns>
        /// <exception cref="GLLinkException">Thrown then link error occured.</exception>
        public static GLProgram LinkShaders(GLVertexShader vertexShader, GLFragmentShader fragmentShader, out string? warnMessage)
        {
            var program = new GLProgram();
            AttachShader(program, vertexShader);
            AttachShader(program, fragmentShader);
            LinkProgram(program);

            int linkResult;
            GetProgramiv(program, GLConst.GL_LINK_STATUS, (nint)(&linkResult));

            if (linkResult == 0)
            {
                using (program)
                {
                    throw new GLLinkException(GetProgramInfoLog(program));
                }
            }

            warnMessage = GetProgramInfoLog(program);

            return program;
        }

        /// <summary>
        /// Throw <see cref="GLException"/> if return value of <see cref="GetError()"/> indicates some errors.
        /// </summary>
        /// <exception cref="GLException">Thrown when any OpenGL error detected.</exception>
        public static void ThrowIfError()
        {
            var err = GetError(); 
            if (err == 0)
            {
                return;
            }
            switch (err)
            {
                case GLConst.GL_INVALID_ENUM:
                    throw new GLException($"OepnGL Error: [{err}] (GL_INVALID_ENUM)");
                case GLConst.GL_INVALID_VALUE:
                    throw new GLException($"OepnGL Error: [{err}] (GL_INVALID_VALUE)");
                case GLConst.GL_INVALID_OPERATION:
                    throw new GLException($"OepnGL Error: [{err}] (GL_INVALID_OPERATION)");
                case GLConst.GL_STACK_OVERFLOW:
                    throw new GLException($"OepnGL Error: [{err}] (GL_STACK_OVERFLOW)");
                case GLConst.GL_STACK_UNDERFLOW:
                    throw new GLException($"OepnGL Error: [{err}] (GL_STACK_UNDERFLOW)");
                case GLConst.GL_OUT_OF_MEMORY:
                    throw new GLException($"OepnGL Error: [{err}] (GL_OUT_OF_MEMORY)");
                default:
                    throw new GLException($"OepnGL Error: [{err}]");
            }
        }
    }
}
