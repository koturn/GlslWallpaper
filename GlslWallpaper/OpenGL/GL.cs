#if NET7_0_OR_GREATER
#    define SUPPORT_LIBRARY_IMPORT
#endif  // NET7_0_OR_GREATER
// #define DISABLE_FUNCTION_POINTER
// #define ENABLE_ERROR_CHECK

using System.Diagnostics.CodeAnalysis;
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
    internal static partial class GL
#else
    internal static class GL
#endif  // SUPPORT_LIBRARY_IMPORT
    {
#if DISABLE_FUNCTION_POINTER
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGet.xhtml"><c>glGetIntegerv</c></see>.
        /// </summary>
        /// <param name="pname">The parameter value to be returned for non-indexed versions of glGet.</param>
        /// <param name="data">The integer value of a selected parameter.</param>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glgetintegerv"/>
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLGetIntegervAction(GLIntegralParams pname, out int data);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetString.xhtml"><c>glGetString</c></see>.
        /// </summary>
        /// <param name="pname">Specifies a symbolic constant, one of <see cref="GLStringParams.Vendor"/>, <see cref="GLStringParams.Renderer"/>,
        /// <see cref="GLStringParams.Version"/>, or <see cref="GLStringParams.ShadingLanguageVersion"/>.</param>
        /// <returns>A string describing the current GL connection.</returns>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glgetstring"/>
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate sbyte* GLGetStringFunc(GLStringParams pname);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetString.xhtml"><c>glGetStringi</c></see>.
        /// </summary>
        /// <param name="pname">Specifies a symbolic constant, one of <see cref="GLStringParams.Vendor"/>, <see cref="GLStringParams.Renderer"/>,
        /// <see cref="GLStringParams.Version"/>, or <see cref="GLStringParams.ShadingLanguageVersion"/>.</param>
        /// <returns>A string describing the current GL connection.</returns>
        /// <param name="index">Specifies the index of the string to return.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate sbyte* GLGetStringiFunc(GLStringParams pname, uint index);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glClear.xhtml"><c>glClear</c></see>.
        /// </summary>
        /// <param name="mask">Bitwise OR of masks that indicate the buffers to be cleared.
        /// The three masks are <see cref="GLClearMaskBits.ColorBufferBit"/>, <see cref="GLClearMaskBits.DepthBufferBit"/>, and <see cref="GLClearMaskBits.StencilBufferBit"/>.</param>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glclear"/>
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLClearAction(GLClearMaskBits mask);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glClearColor.xhtml"><c>glClearColor</c></see>.
        /// </summary>
        /// <param name="r">The red value used when the color buffers are cleared. The initial value is 0.</param>
        /// <param name="g">The green value used when the color buffers are cleared. The initial value is 0.</param>
        /// <param name="b">The blue value used when the color buffers are cleared. The initial value is 0.</param>
        /// <param name="a">The alpha value used when the color buffers are cleared. The initial value is 0.</param>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glclearcolor"/>
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLClearColorAction(float r, float g, float b, float a);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glViewport.xhtml"><c>glViewport</c></see>.
        /// </summary>
        /// <param name="x">Specify the x-coordinate of the lower left corner of the viewport rectangle, in pixels. The initial value is 0.</param>
        /// <param name="y">Specify the y-coordinate of the lower left corner of the viewport rectangle, in pixels. The initial value is 0.</param>
        /// <param name="w">Specify the width of the viewport. When a GL context is first attached to a window, width is set to the dimensions of that window.</param>
        /// <param name="h">Specify the height of the viewport. When a GL context is first attached to a window, height is set to the dimensions of that window.</param>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glviewport"/>
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLViewportAction(int x, int y, int w, int h);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glFlush.xhtml"><c>glFlush</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glflush"/>
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLFlushAction();
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetError.xhtml"><c>glGetError</c></see>.
        /// </summary>
        /// <returns>The value of the error flag.</returns>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glgeterror"/>
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint GLGetErrorFunc();
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCreateShader.xhtml"><c>glCreateShader</c></see>.
        /// </summary>
        /// <param name="shaderType">the type of shader to be created.
        /// Must be one of <see cref="GLShaderTypes.ComputeShader"/>, <see cref="GLShaderTypes.VertexShader"/>, <see cref="GLShaderTypes.TessControlShader"/>,
        /// <see cref="GLShaderTypes.TessEvaluationShader"/>, <see cref="GLShaderTypes.GeometryShader"/>, or <see cref="GLShaderTypes.FragmentShader"/>.</param>
        /// <returns>The handle of the created shader object.</returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint GLCreateShaderFunc(GLShaderTypes shaderType);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteShader.xhtml"><c>glDeleteShader</c></see>.
        /// </summary>
        /// <param name="shader">The handle of the shader object to be deleted.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLDeleteShaderAction(uint shader);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glShaderSource.xhtml"><c>glShaderSource</c></see>.
        /// </summary>
        /// <param name="shader">Specifies the handle of the shader object whose source code is to be replaced.</param>
        /// <param name="count">Specifies the number of elements in the <paramref name="pSourceArray"/> and <paramref name="pLengthArray"/> arrays.</param>
        /// <param name="pSourceArray">Specifies an array of pointers to strings containing the source code to be loaded into the shader.</param>
        /// <param name="pLengthArray">Specifies an array of string lengths.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate void GLShaderSourceAction(uint shader, int count, byte** pSourceArray, int* pLengthArray);
        /// <summary>
        /// Delegate <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCompileShader.xhtml"><c>glCompileShader</c></see>.
        /// </summary>
        /// <param name="shader">The shader object to be compiled.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLCompileShaderAction(uint shader);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetShader.xhtml"><c>glGetShaderiv</c></see>.
        /// </summary>
        /// <param name="shader">Specifies the shader object to be queried.</param>
        /// <param name="pname">The object parameter.
        /// Accepted symbolic names are GL_SHADER_TYPE, GL_DELETE_STATUS, <see cref="GLShaderParams.CompileStatus"/>,
        /// <see cref="GLShaderParams.InfoLogLength"/>, GL_SHADER_SOURCE_LENGTH.</param>
        /// <param name="pParam">Returns the requested object parameter.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLGetShaderivAction(uint shader, GLShaderParams pname, nint pParam);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetShaderInfoLog.xhtml"><c>glGetShaderInfoLog</c></see>.
        /// </summary>
        /// <param name="shader">Specifies the shader object whose information log is to be queried.</param>
        /// <param name="maxLength">Specifies the size of the character buffer for storing the returned information log.</param>
        /// <param name="length">Returns the length of the string returned in infoLog (excluding the null terminator).</param>
        /// <param name="pInfoLog">Specifies an array of characters that is used to return the information log.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate void GLGetShaderInfoLogAction(uint shader, int maxLength, out int length, sbyte* pInfoLog);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCreateProgram.xhtml"><c>glCreateProgram</c></see>.
        /// </summary>
        /// <returns>The handle of the program object.</returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint GLCreateProgramFunc();
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteProgram.xhtml"><c>glDeleteProgram</c></see>.
        /// </summary>
        /// <param name="program">Specifies the program object to be deleted.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLDeleteProgramAction(uint program);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glAttachShader.xhtml"><c>glAttachShader</c></see>.
        /// </summary>
        /// <param name="program">Specifies the program object to which a shader object will be attached.</param>
        /// <param name="shader">Specifies the shader object that is to be attached.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLAttachShaderAction(uint program, uint shader);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glLinkProgram.xhtml"><c>glLinkProgram</c></see>.
        /// </summary>
        /// <param name="program">Specifies the handle of the program object to be linked.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLLinkProgramAction(uint program);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgram.xhtml"><c>glGetProgramiv</c></see>.
        /// </summary>
        /// <param name="program">The program object to be queried.</param>
        /// <param name="pname">Specifies the object parameter.</param>
        /// <param name="pParam">The destination of the requested object parameter.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLGetProgramivAction(uint program, GLProgramParams pname, nint pParam);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgramInfoLog.xhtml"><c>glGetProgramInfoLog</c></see>.
        /// </summary>
        /// <param name="program">Specifies the program object whose information log is to be queried.</param>
        /// <param name="maxLength">Specifies the size of the character buffer for storing the returned information log.</param>
        /// <param name="length">Returns the length of the string returned in infoLog (excluding the null terminator).</param>
        /// <param name="pInfoLog">Specifies an array of characters that is used to return the information log.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate void GLGetProgramInfoLogAction(uint program, int maxLength, out int length, sbyte *pInfoLog);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUseProgram.xhtml"><c>glUseProgram</c></see>.
        /// </summary>
        /// <param name="program">Specifies the handle of the program object whose executables are to be used as part of current rendering state.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUseProgramAction(uint program);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGenBuffers.xhtml"><c>glGenBuffers</c></see>.
        /// </summary>
        /// <param name="n">Specifies the number of buffer object names to be generated.</param>
        /// <param name="pBuffers">Specifies an array in which the generated buffer object names are stored.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate void GLGenBuffersAction(int n, uint *pBuffers);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteBuffers.xhtml"><c>glDeleteBuffers</c></see>.
        /// </summary>
        /// <param name="n">Specifies the number of buffer objects to be deleted.</param>
        /// <param name="pBuffers">Specifies an array of buffer objects to be deleted.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate void GLDeleteBuffersAction(int n, uint *pBuffers);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindBuffer.xhtml"><c>glBindBuffer</c></see>.
        /// </summary>
        /// <param name="target">Specifies the target to which the buffer object is bound.</param>
        /// <param name="buffer">Specifies the name of a buffer object.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLBindBufferAction(GLTargets target, uint buffer);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBufferData.xhtml"><c>glBufferData</c></see>.
        /// </summary>
        /// <param name="target">Specifies the target to which the buffer object is bound for glBufferData.</param>
        /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization, or NULL if no data is to be copied.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLBufferDataAction(GLTargets target, nint size, nint data, GLUsage usage);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGenVertexArrays.xhtml"><c>glGenVertexArrays</c></see>.
        /// </summary>
        /// <param name="n">Specifies the number of vertex array object names to generate.</param>
        /// <param name="pArrays">Specifies an array in which the generated vertex array object names are stored.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate void GLGenVertexArraysAction(int n, uint *pArrays);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteVertexArrays.xhtml"><c>_glDeleteVertexArrays</c></see>.
        /// </summary>
        /// <param name="n">Specifies the number of vertex array objects to be deleted.</param>
        /// <param name="pArrays">Specifies the address of an array containing the <paramref name="n"/> names of the objects to be deleted.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate void GLDeleteVertexArraysAction(int n, uint *pArrays);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindVertexArray.xhtml"><c>glBindVertexArray</c></see>.
        /// </summary>
        /// <param name="array">Specifies the name of the vertex array to bind.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLBindVertexArrayAction(uint array);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glEnableVertexAttribArray.xhtml"><c>glEnableVertexAttribArray</c></see>.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be enabled or disabled.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLEnableVertexAttribArrayAction(uint index);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glVertexAttribPointer.xhtml"><c>glVertexAttribPointer</c></see>.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="size">Specifies the number of components per generic vertex attribute.
        /// Must be 1, 2, 3, 4.
        /// Additionally, the symbolic constant GL_BGRA is accepted by glVertexAttribPointer.
        /// The initial value is 4.</param>
        /// <param name="type">Specifies the data type of each component in the array.</param>
        /// <param name="normalized">Specifies whether fixed-point data values should be normalized (<see cref="GLBool.True"/>) or converted directly
        /// as fixed-point values (<see cref="GLBool.False"/>) when they are accessed.</param>
        /// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes.
        /// If stride is 0, the generic vertex attributes are understood to be tightly packed in the array.
        /// The initial value is 0.</param>
        /// <param name="ptr">Specifies the offset of the first component of the first generic vertex attribute
        /// in the array in the data store of the buffer currently bound to the <see cref="GLTargets.ArrayBuffer"/> target.
        /// The initial value is 0.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLVertexAttribPointerAction(uint index, int size, GLValueTypes type, byte normalized, int stride, nint ptr);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDrawElements.xhtml"><c>glDrawElements</c></see>.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices.
        /// Must be one of <see cref="GLValueTypes.UnsignedByte"/>, <see cref="GLValueTypes.UnsignedShort"/>, or <see cref="GLValueTypes.UnsignedInt"/>.</param>
        /// <param name="indices"></param>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/gldrawelements"/>
        /// </remarks>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLDrawElementsAction(GLDrawModes mode, int count, GLValueTypes type, nint indices);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetAttribLocation.xhtml"><c>glGetAttribLocation</c></see>.
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="pName">Points to a null terminated string containing the name of the attribute variable whose location is to be queried.</param>
        /// <returns>The location of an attribute variable</returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GLGetAttribLocationFunc(uint program, nint pName);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetUniformLocation.xhtml"><c>glGetUniformLocation</c></see>.
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="pName">Points to a null terminated string containing the name of the uniform variable whose location is to be queried.</param>
        /// <returns>The location of a uniform variable.</returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GLGetUniformLocationFunc(uint program, nint pName);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform1f</c></see>.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="v0">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUniform1fAction(int location, float v0);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform2f</c></see>.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="v0">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="v1">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUniform2fAction(int location, float v0, float v1);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform3f</c></see>.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="v0">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="v1">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="v2">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUniform3fAction(int location, float v0, float v1, float v2);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform4f</c></see>.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="v0">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="v1">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="v2">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        /// <param name="v3">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUniform4fAction(int location, float v0, float v1, float v2, float v3);
        /// <summary>
        /// Delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform1i</c></see>.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="v0">For the scalar commands, specifies the new values to be used for the specified uniform variable.</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GLUniform1iAction(int location, int v0);
#endif  // DISABLE_FUNCTION_POINTER

#if DISABLE_FUNCTION_POINTER
        /// <summary>
        /// The instance of the <see cref="GLGetIntegervAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGet.xhtml"><c>glGetIntegerv</c></see>.
        /// </summary>
        private static GLGetIntegervAction? _glGetIntegerv;
        /// <summary>
        /// The instance of the <see cref="GLGetStringFunc"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetString.xhtml"><c>glGetString</c></see>.
        /// </summary>
        private static GLGetStringFunc? _glGetString;
        /// <summary>
        /// The instance of the <see cref="GLGetStringiFunc"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetString.xhtml"><c>glGetString</c></see>.
        /// </summary>
        private static GLGetStringiFunc? _glGetStringi;
        /// <summary>
        /// The instance of the <see cref="GLClearAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glClear.xhtml"><c>glClear</c></see>.
        /// </summary>
        private static GLClearAction _glClear = (_) => throw new GLFunctionNotLoadedException("glClear");
        /// <summary>
        /// The instance of the <see cref="GLClearColorAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glClearColor.xhtml"><c>glClearColor</c></see>.
        /// </summary>
        private static GLClearColorAction _glClearColor = (_, _, _, _) => throw new GLFunctionNotLoadedException("glClearColor");
        /// <summary>
        /// The instance of the <see cref="GLViewportAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glViewport.xhtml"><c>glViewport</c></see>.
        /// </summary>
        private static GLViewportAction _glViewport = (_, _, _, _) => throw new GLFunctionNotLoadedException("glViewport");
        /// <summary>
        /// The instance of the <see cref="GLFlushAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glFlush.xhtml"><c>glFlush</c></see>.
        /// </summary>
        private static GLFlushAction _glFlush = () => throw new GLFunctionNotLoadedException("glFlush");
        /// <summary>
        /// The instance of the <see cref="GLGetErrorFunc"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetError.xhtml"><c>glGetError</c></see>.
        /// </summary>
        private static GLGetErrorFunc _glGetError = () => throw new GLFunctionNotLoadedException("glGetError");
        /// <summary>
        /// The instance of the <see cref="GLCreateShaderFunc"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCreateShader.xhtml"><c>glCreateShader</c></see>.
        /// </summary>
        private static GLCreateShaderFunc _glCreateShader = (_) => throw new GLCompilationException("glCreateShader");
        /// <summary>
        /// The instance of the <see cref="GLDeleteShaderAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteShader.xhtml"><c>glDeleteShader</c></see>.
        /// </summary>
        private static GLDeleteShaderAction _glDeleteShader = (_) => throw new GLFunctionNotLoadedException("glDeleteShader");
        /// <summary>
        /// The instance of the <see cref="GLShaderSourceAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glShaderSource.xhtml"><c>glShaderSource</c></see>.
        /// </summary>
        private static unsafe GLShaderSourceAction _glShaderSource = (_, _, _, _) => throw new GLFunctionNotLoadedException("glShaderSource");
        /// <summary>
        /// The instance of the <see cref="GLCompileShaderAction"/>
        /// which is the delegate <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCompileShader.xhtml"><c>glCompileShader</c></see>.
        /// </summary>
        private static GLCompileShaderAction _glCompileShader = (_) => throw new GLFunctionNotLoadedException("glCompileShader");
        /// <summary>
        /// The instance of the <see cref="GLGetShaderivAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetShader.xhtml"><c>glGetShaderiv</c></see>.
        /// </summary>
        private static GLGetShaderivAction _glGetShaderiv = (_, _, _) => throw new GLFunctionNotLoadedException("glGetShaderiv");
        /// <summary>
        /// The instance of the <see cref="GLGetShaderInfoLogAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetShaderInfoLog.xhtml"><c>glGetShaderInfoLog</c></see>.
        /// </summary>
        private static unsafe GLGetShaderInfoLogAction _glGetShaderInfoLog = (_, _, out _, _) => throw new GLFunctionNotLoadedException("glGetShaderInfoLog");
        /// <summary>
        /// The instance of the <see cref="GLCreateProgramFunc"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCreateProgram.xhtml"><c>glCreateProgram</c></see>.
        /// </summary>
        private static GLCreateProgramFunc _glCreateProgram = () => throw new GLFunctionNotLoadedException("glCreateProgram");
        /// <summary>
        /// The instance of the <see cref="GLDeleteProgramAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteProgram.xhtml"><c>glDeleteProgram</c></see>.
        /// </summary>
        private static GLDeleteProgramAction _glDeleteProgram = (_) => throw new GLFunctionNotLoadedException("glDeleteProgram");
        /// <summary>
        /// The instance of the <see cref="GLAttachShaderAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glAttachShader.xhtml"><c>glAttachShader</c></see>.
        /// </summary>
        private static GLAttachShaderAction _glAttachShader = (_, _) => throw new GLFunctionNotLoadedException("glAttachShader");
        /// <summary>
        /// The instance of the <see cref="GLLinkProgramAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glLinkProgram.xhtml"><c>glLinkProgram</c></see>.
        /// </summary>
        private static GLLinkProgramAction _glLinkProgram = (_) => throw new GLFunctionNotLoadedException("glLinkProgram");
        /// <summary>
        /// The instance of the <see cref="GLGetProgramivAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgram.xhtml"><c>glGetProgramiv</c></see>.
        /// </summary>
        private static GLGetProgramivAction _glGetProgramiv = (_, _, _) => throw new GLFunctionNotLoadedException("glGetProgramiv");
        /// <summary>
        /// The instance of the <see cref="GLGetProgramInfoLogAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgramInfoLog.xhtml"><c>glGetProgramInfoLog</c></see>.
        /// </summary>
        private static unsafe GLGetProgramInfoLogAction _glGetProgramInfoLog = (_, _, out _, _) => throw new GLFunctionNotLoadedException("glGetProgramInfoLog");
        /// <summary>
        /// The instance of the <see cref="GLUseProgramAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUseProgram.xhtml"><c>glUseProgram</c></see>.
        /// </summary>
        private static GLUseProgramAction _glUseProgram = (_) => throw new GLFunctionNotLoadedException("glUseProgram");
        /// <summary>
        /// The instance of the <see cref="GLGenBuffersAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGenBuffers.xhtml"><c>glGenBuffers</c></see>.
        /// </summary>
        private static unsafe GLGenBuffersAction _glGenBuffers = (_, _) => throw new GLFunctionNotLoadedException("glGenBuffers");
        /// <summary>
        /// The instance of the <see cref="GLDeleteBuffersAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteBuffers.xhtml"><c>glDeleteBuffers</c></see>.
        /// </summary>
        private static unsafe GLDeleteBuffersAction _glDeleteBuffers = (_, _) => throw new GLFunctionNotLoadedException("glDeleteBuffers");
        /// <summary>
        /// The instance of the <see cref="GLBindBufferAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindBuffer.xhtml"><c>glBindBuffer</c></see>.
        /// </summary>
        private static GLBindBufferAction _glBindBuffer = (_, _) => throw new GLFunctionNotLoadedException("glBindBuffer");
        /// <summary>
        /// The instance of the <see cref="GLBufferDataAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBufferData.xhtml"><c>glBufferData</c></see>.
        /// </summary>
        private static GLBufferDataAction _glBufferData = (_, _, _, _) => throw new GLFunctionNotLoadedException("glBufferData");
        /// <summary>
        /// The instance of the <see cref="GLGenVertexArraysAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGenVertexArrays.xhtml"><c>glGenVertexArrays</c></see>.
        /// </summary>
        private static unsafe GLGenVertexArraysAction _glGenVertexArrays = (_, _) => throw new GLFunctionNotLoadedException("glGenVertexArrays");
        /// <summary>
        /// The instance of the <see cref="GLDeleteVertexArraysAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteVertexArrays.xhtml"><c>_glDeleteVertexArrays</c></see>.
        /// </summary>
        private static unsafe GLDeleteVertexArraysAction _glDeleteVertexArrays = (_, _) => throw new GLFunctionNotLoadedException("glDeleteVertexArrays");
        /// <summary>
        /// The instance of the <see cref="GLBindVertexArrayAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindVertexArray.xhtml"><c>glBindVertexArray</c></see>.
        /// </summary>
        private static GLBindVertexArrayAction _glBindVertexArray = (_) => throw new GLFunctionNotLoadedException("glBindVertexArray");
        /// <summary>
        /// The instance of the <see cref="GLEnableVertexAttribArrayAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glEnableVertexAttribArray.xhtml"><c>glEnableVertexAttribArray</c></see>.
        /// </summary>
        private static GLEnableVertexAttribArrayAction _glEnableVertexAttribArray = (_) => throw new GLFunctionNotLoadedException("glEnableVertexAttribArray");
        /// <summary>
        /// The instance of the <see cref="GLVertexAttribPointerAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glVertexAttribPointer.xhtml"><c>glVertexAttribPointer</c></see>.
        /// </summary>
        private static GLVertexAttribPointerAction _glVertexAttribPointer = (_, _, _, _, _, _) => throw new GLFunctionNotLoadedException("glVertexAttribPointer");
        /// <summary>
        /// The instance of the <see cref="GLDrawElementsAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDrawElements.xhtml"><c>glDrawElements</c></see>.
        /// </summary>
        private static GLDrawElementsAction _glDrawElements = (_, _, _, _) => throw new GLFunctionNotLoadedException("glDrawElements");
        /// <summary>
        /// The instance of the <see cref="GLGetAttribLocationFunc"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetAttribLocation.xhtml"><c>glGetAttribLocation</c></see>.
        /// </summary>
        private static GLGetAttribLocationFunc _glGetAttribLocation = (_, _) => throw new GLFunctionNotLoadedException("glGetAttribLocation");
        /// <summary>
        /// The instance of the <see cref="GLGetUniformLocationFunc"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetUniformLocation.xhtml"><c>glGetUniformLocation</c></see>.
        /// </summary>
        private static GLGetUniformLocationFunc _glGetUniformLocation = (_, _) => throw new GLFunctionNotLoadedException("glGetUniformLocation");
        /// <summary>
        /// The instance of the <see cref="GLUniform1fAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform1f</c></see>.
        /// </summary>
        private static GLUniform1fAction _glUniform1f = (_, _) => throw new GLFunctionNotLoadedException("glUniform1f");
        /// <summary>
        /// The instance of the <see cref="GLUniform2fAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform2f</c></see>.
        /// </summary>
        private static GLUniform2fAction _glUniform2f = (_, _, _) => throw new GLFunctionNotLoadedException("glUniform2f");
        /// <summary>
        /// The instance of the <see cref="GLUniform3fAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform3f</c></see>.
        /// </summary>
        private static GLUniform3fAction _glUniform3f = (_, _, _, _) => throw new GLFunctionNotLoadedException("glUniform3f");
        /// <summary>
        /// The instance of the <see cref="GLUniform4fAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform4f</c></see>.
        /// </summary>
        private static GLUniform4fAction _glUniform4f = (_, _, _, _, _) => throw new GLFunctionNotLoadedException("glUniform4f");
        /// <summary>
        /// The instance of the <see cref="GLUniform1iAction"/>
        /// which is the delegate for <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform1i</c></see>.
        /// </summary>
        private static GLUniform1iAction _glUniform1i = (_, _) => throw new GLFunctionNotLoadedException("glUniform1i");
#else
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGet.xhtml"><c>glGetIntegerv</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glgetintegerv"/>
        /// </remarks>
        private static unsafe delegate* unmanaged[Cdecl]<GLNumericParams, out int, void> _glGetIntegerv;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetString.xhtml"><c>glGetString</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glgetstring"/>
        /// </remarks>
        private static unsafe delegate* unmanaged[Cdecl]<GLStringParams, sbyte*> _glGetString;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetString.xhtml"><c>glGetStringi</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<GLStringParams, uint, sbyte*> _glGetStringi;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glClear.xhtml"><c>glClear</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glclear"/>
        /// </remarks>
        private static unsafe delegate* unmanaged[Cdecl]<GLClearMaskBits, void> _glClear;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glClearColor.xhtml"><c>glClearColor</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glclearcolor"/>
        /// </remarks>
        private static unsafe delegate* unmanaged[Cdecl]<float, float, float, float, void> _glClearColor;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glViewport.xhtml"><c>glViewport</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glviewport"/>
        /// </remarks>
        private static unsafe delegate* unmanaged[Cdecl]<int, int, int, int, void> _glViewport;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glFlush.xhtml"><c>glFlush</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glflush"/>
        /// </remarks>
        private static unsafe delegate* unmanaged[Cdecl]<void> _glFlush;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetError.xhtml"><c>glGetError</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/glgeterror"/>
        /// </remarks>
        private static unsafe delegate* unmanaged[Cdecl]<uint> _glGetError;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCreateShader.xhtml"><c>glCreateShader</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<GLShaderTypes, uint> _glCreateShader;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteShader.xhtml"><c>glDeleteShader</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, void> _glDeleteShader;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glShaderSource.xhtml"><c>glShaderSource</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, int, byte**, int*, void> _glShaderSource;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCompileShader.xhtml"><c>glCompileShader</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, void> _glCompileShader;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetShader.xhtml"><c>glGetShaderiv</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, GLShaderParams, nint, void> _glGetShaderiv;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetShaderInfoLog.xhtml"><c>glGetShaderInfoLog</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, int, out int, sbyte*, void> _glGetShaderInfoLog;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glCreateProgram.xhtml"><c>glCreateProgram</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint> _glCreateProgram;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteProgram.xhtml"><c>glDeleteProgram</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, void> _glDeleteProgram;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glAttachShader.xhtml"><c>glAttachShader</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, uint, void> _glAttachShader;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glLinkProgram.xhtml"><c>glLinkProgram</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, void> _glLinkProgram;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgram.xhtml"><c>glGetProgramiv</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, GLProgramParams, nint, void> _glGetProgramiv;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgramInfoLog.xhtml"><c>glGetProgramInfoLog</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, int, out int, sbyte*, void> _glGetProgramInfoLog;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUseProgram.xhtml"><c>glUseProgram</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, void> _glUseProgram;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGenBuffers.xhtml"><c>glGenBuffers</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<int, uint*, void> _glGenBuffers;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteBuffers.xhtml"><c>glDeleteBuffers</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<int, uint*, void> _glDeleteBuffers;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindBuffer.xhtml"><c>glBindBuffer</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<GLTargets, uint, void> _glBindBuffer;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBufferData.xhtml"><c>glBufferData</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<GLTargets, nint, nint, GLUsage, void> _glBufferData;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGenVertexArrays.xhtml"><c>glGenVertexArrays</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<int, uint*, void> _glGenVertexArrays;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDeleteVertexArrays.xhtml"><c>_glDeleteVertexArrays</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<int, uint*, void> _glDeleteVertexArrays;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBindVertexArray.xhtml"><c>glBindVertexArray</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, void> _glBindVertexArray;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glEnableVertexAttribArray.xhtml"><c>glEnableVertexAttribArray</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, void> _glEnableVertexAttribArray;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glVertexAttribPointer.xhtml"><c>glVertexAttribPointer</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, int, GLValueTypes, byte, int, nint, void> _glVertexAttribPointer;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDrawElements.xhtml"><c>glDrawElements</c></see>.
        /// </summary>
        /// <remarks>
        /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/opengl/gldrawelements"/>
        /// </remarks>
        private static unsafe delegate* unmanaged[Cdecl]<GLDrawModes, int, GLValueTypes, nint, void> _glDrawElements;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetAttribLocation.xhtml"><c>glGetAttribLocation</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, nint, int> _glGetAttribLocation;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetUniformLocation.xhtml"><c>glGetUniformLocation</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<uint, nint, int> _glGetUniformLocation;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform1f</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<int, float, void> _glUniform1f;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform2f</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<int, float, float, void> _glUniform2f;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform3f</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<int, float, float, float, void> _glUniform3f;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform4f</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<int, float, float, float, float, void> _glUniform4f;
        /// <summary>
        /// Function pointer to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glUniform.xhtml"><c>glUniform1i</c></see>.
        /// </summary>
        private static unsafe delegate* unmanaged[Cdecl]<int, int, void> _glUniform1i;
#endif  // DISABLE_FUNCTION_POINTER


        /// <summary>
        /// Stack buffer size for string.
        /// </summary>
        private const int StringStackBufferSize = 8192;


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
            unsafe
            {
                _glClear = (delegate* unmanaged[Cdecl]<GLClearMaskBits, void>)WGL.LoadFunction("glClear");
                _glClearColor = (delegate* unmanaged[Cdecl]<float, float, float, float, void>)WGL.LoadFunction("glClearColor");
                _glViewport = (delegate* unmanaged[Cdecl]<int, int, int, int, void>)WGL.LoadFunction("glViewport");
                _glFlush = (delegate* unmanaged[Cdecl]<void>)WGL.LoadFunction("glFlush");
                _glGetError = (delegate* unmanaged[Cdecl]<uint>)WGL.LoadFunction("glGetError");
                _glCreateShader = (delegate* unmanaged[Cdecl]<GLShaderTypes, uint>)WGL.LoadFunction("glCreateShader");
                _glDeleteShader = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glDeleteShader");
                _glShaderSource = (delegate* unmanaged[Cdecl]<uint, int, byte**, int*, void>)WGL.LoadFunction("glShaderSource");
                _glCompileShader = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glCompileShader");
                _glGetShaderiv = (delegate* unmanaged[Cdecl]<uint, GLShaderParams, nint, void>)WGL.LoadFunction("glGetShaderiv");
                _glGetShaderInfoLog = (delegate* unmanaged[Cdecl]<uint, int, out int, sbyte*, void>)WGL.LoadFunction("glGetShaderInfoLog");
                _glCreateProgram = (delegate* unmanaged[Cdecl]<uint>)WGL.LoadFunction("glCreateProgram");
                _glDeleteProgram = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glDeleteProgram");
                _glAttachShader = (delegate* unmanaged[Cdecl]<uint, uint, void>)WGL.LoadFunction("glAttachShader");
                _glLinkProgram = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glLinkProgram");
                _glGetProgramiv = (delegate* unmanaged[Cdecl]<uint, GLProgramParams, nint, void>)WGL.LoadFunction("glGetProgramiv");
                _glGetProgramInfoLog = (delegate* unmanaged[Cdecl]<uint, int, out int, sbyte*, void>)WGL.LoadFunction("glGetProgramInfoLog");
                _glUseProgram = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glUseProgram");
                _glGenBuffers = (delegate* unmanaged[Cdecl]<int, uint*, void>)WGL.LoadFunction("glGenBuffers");
                _glDeleteBuffers = (delegate* unmanaged[Cdecl]<int, uint*, void>)WGL.LoadFunction("glDeleteBuffers");
                _glBindBuffer = (delegate* unmanaged[Cdecl]<GLTargets, uint, void>)WGL.LoadFunction("glBindBuffer");
                _glBufferData = (delegate* unmanaged[Cdecl]<GLTargets, nint, nint, GLUsage, void>)WGL.LoadFunction("glBufferData");
                _glGenVertexArrays = (delegate* unmanaged[Cdecl]<int, uint*, void>)WGL.LoadFunction("glGenVertexArrays");
                _glDeleteVertexArrays = (delegate* unmanaged[Cdecl]<int, uint*, void>)WGL.LoadFunction("glDeleteVertexArrays");
                _glBindVertexArray = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glBindVertexArray");
                _glEnableVertexAttribArray = (delegate* unmanaged[Cdecl]<uint, void>)WGL.LoadFunction("glEnableVertexAttribArray");
                _glVertexAttribPointer = (delegate* unmanaged[Cdecl]<uint, int, GLValueTypes, byte, int, nint, void>)WGL.LoadFunction("glVertexAttribPointer");
                _glDrawElements = (delegate* unmanaged[Cdecl]<GLDrawModes, int, GLValueTypes, nint, void>)WGL.LoadFunction("glDrawElements");
                _glGetAttribLocation = (delegate* unmanaged[Cdecl]<uint, nint, int>)WGL.LoadFunction("glGetAttribLocation");
                _glGetUniformLocation = (delegate* unmanaged[Cdecl]<uint, nint, int>)WGL.LoadFunction("glGetUniformLocation");
                _glUniform1f = (delegate* unmanaged[Cdecl]<int, float, void>)WGL.LoadFunction("glUniform1f");
                _glUniform2f = (delegate* unmanaged[Cdecl]<int, float, float, void>)WGL.LoadFunction("glUniform2f");
                _glUniform3f = (delegate* unmanaged[Cdecl]<int, float, float, float, void>)WGL.LoadFunction("glUniform3f");
                _glUniform4f = (delegate* unmanaged[Cdecl]<int, float, float, float, float, void>)WGL.LoadFunction("glUniform4f");
                _glUniform1i = (delegate* unmanaged[Cdecl]<int, int, void>)WGL.LoadFunction("glUniform1i");
            }
#endif  // DISABLE_FUNCTION_POINTER
        }

        /// <summary>
        /// Return the integer value of a selected parameter.
        /// </summary>
        /// <param name="pName">the parameter value to be returned for non-indexed versions of glGet.</param>
        /// <returns>The integer value of a selected parameter.</returns>
        public static int GetIntegerv(GLNumericParams pName)
        {
            unsafe
            {
                if (_glGetIntegerv == null)
                {
#if DISABLE_FUNCTION_POINTER
                    _glGetIntegerv = WGL.LoadFunctionAsDelegate<GLGetIntegervAction>("glGetIntegerv");
#else
                    _glGetIntegerv = (delegate* unmanaged[Cdecl]<GLNumericParams, out int, void>)WGL.LoadFunction("glGetIntegerv");
#endif  // DISABLE_FUNCTION_POINTER
                }
                _glGetIntegerv(pName, out var val);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
                return val;
            }
        }

        /// <summary>
        /// Return a string describing the current GL connection.
        /// </summary>
        /// <param name="pName">Specifies a symbolic constant, one of GL_VENDOR, GL_RENDERER, <see cref="GLStringParams.Version"/>,
        /// or <see cref="GLStringParams.ShadingLanguageVersion"/>.</param>
        /// <returns>A string describing the current GL connection.</returns>
        public static string? GetString(GLStringParams pName)
        {
            unsafe
            {
                if (_glGetString == null)
                {
#if DISABLE_FUNCTION_POINTER
                    _glGetString = WGL.LoadFunctionAsDelegate<GLGetStringFunc>("glGetString");
#else
                    _glGetString = (delegate* unmanaged[Cdecl]<GLStringParams, sbyte*>)WGL.LoadFunction("glGetString");
#endif  // DISABLE_FUNCTION_POINTER
                }
                var pExtension = _glGetString(pName);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
                return pExtension == null ? null : new string(pExtension);
            }
        }

        /// <summary>
        /// Return a string describing the current GL connection.
        /// </summary>
        /// <param name="pName">Specifies a symbolic constant, one of GL_VENDOR, GL_RENDERER, <see cref="GLStringParams.Version"/>,
        /// or <see cref="GLStringParams.ShadingLanguageVersion"/>.</param>
        /// <param name="index">Specifies the index of the string to return.</param>
        /// <returns>A string describing the current GL connection.</returns>
        public static string? GetStringi(GLStringParams pName, uint index)
        {
            unsafe
            {
                if (_glGetStringi == null)
                {
#if DISABLE_FUNCTION_POINTER
                    _glGetStringi = WGL.LoadFunctionAsDelegate<GLGetStringiFunc>("glGetStringi");
#else
                    _glGetStringi = (delegate* unmanaged[Cdecl]<GLStringParams, uint, sbyte*>)WGL.LoadFunction("glGetStringi");
#endif  // DISABLE_FUNCTION_POINTER
                }
                var pExtension = _glGetStringi(pName, index);
                return pExtension == null ? null : new string(pExtension);
            }
        }

        /// <summary>
        /// Clear buffers to preset values.
        /// </summary>
        /// <param name="mask">Bitwise OR of masks that indicate the buffers to be cleared.
        /// The three masks are <see cref="GLClearMaskBits.ColorBufferBit"/>, <see cref="GLClearMaskBits.DepthBufferBit"/>,
        /// and <see cref="GLClearMaskBits.StencilBufferBit"/>.</param>
        public static void Clear(GLClearMaskBits mask)
        {
            unsafe
            {
                _glClear(mask);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
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
            unsafe
            {
                _glClearColor(r, g, b, a);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
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
            unsafe
            {
                _glViewport(x, y, w, h);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Force execution of GL commands in finite time.
        /// </summary>
        public static void Flush()
        {
            unsafe
            {
                _glFlush();
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Return error information.
        /// </summary>
        /// <returns>The value of the error flag.</returns>
        public static GLErrors GetError()
        {
            unsafe
            {
                if (_glGetError == null)
                {
#if DISABLE_FUNCTION_POINTER
                    _glGetError = WGL.LoadFunctionAsDelegate<GLGetErrorFunc>("glGetError");
#else
                    _glGetError = (delegate* unmanaged[Cdecl]<uint>)WGL.LoadFunction("glGetError");
#endif  // DISABLE_FUNCTION_POINTER
                }
                return (GLErrors)_glGetError();
            }
        }

        /// <summary>
        /// Creates a shader object.
        /// </summary>
        /// <param name="shaderType">the type of shader to be created.
        /// Must be one of GL_COMPUTE_SHADER, <see cref="GLShaderTypes.VertexShader"/>, GL_TESS_CONTROL_SHADER, GL_TESS_EVALUATION_SHADER,
        /// GL_GEOMETRY_SHADER, or <see cref="GLShaderTypes.FragmentShader"/>.</param>
        /// <returns>The handle of the created shader object.</returns>
        public static uint CreateShader(GLShaderTypes shaderType)
        {
            unsafe
            {
#if ENABLE_ERROR_CHECK
                var shader = _glCreateShader(shaderType);
                ThrowIfError();
                return shader;
#else
                return _glCreateShader(shaderType);
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Deletes a shader object.
        /// </summary>
        /// <param name="shader">The handle of the shader object to be deleted.</param>
        public static void DeleteShader(uint shader)
        {
            unsafe
            {
                _glDeleteShader(shader);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
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
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
                }
            }
        }

        /// <summary>
        /// Compiles a shader object.
        /// </summary>
        /// <param name="shader">The shader object to be compiled.</param>
        public static void CompileShader(GLShader shader)
        {
            unsafe
            {
                _glCompileShader(shader.Handle);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Returns a parameter from a shader object.
        /// </summary>
        /// <param name="shader">The shader object to be queried.</param>
        /// <param name="pname">The object parameter.
        /// Accepted symbolic names are GL_SHADER_TYPE, GL_DELETE_STATUS, <see cref="GLShaderParams.CompileStatus"/>,
        /// <see cref="GLShaderParams.InfoLogLength"/>, GL_SHADER_SOURCE_LENGTH.</param>
        /// <param name="pParam">The requested object parameter.</param>
        public static void GetShaderiv(GLShader shader, GLShaderParams pname, nint pParam)
        {
            unsafe
            {
                _glGetShaderiv(shader.Handle, pname, pParam);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Get shader information log.
        /// </summary>
        /// <param name="shader">The shader object to be queried.</param>
        /// <returns>The shader information log.</returns>
        public static string? GetShaderInfoLog(GLShader shader)
        {
            int infoLogLength;
            unsafe
            {
                GetShaderiv(shader, GLShaderParams.InfoLogLength, (nint)(&infoLogLength));
            }
            if (infoLogLength == 0)
            {
                return null;
            }

            if (infoLogLength < StringStackBufferSize)
            {
                unsafe
                {
                    var pLogBuffer = stackalloc sbyte[StringStackBufferSize];
                    _glGetShaderInfoLog(shader.Handle, StringStackBufferSize, out var length, pLogBuffer);
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
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
#if ENABLE_ERROR_CHECK
                        ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
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
            unsafe
            {
#if ENABLE_ERROR_CHECK
                var program = _glCreateProgram();
                ThrowIfError();
                return program;
#else
                return _glCreateProgram();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Deletes a program object.
        /// </summary>
        /// <param name="program">The handle of the program object.</param>
        public static void DeleteProgram(uint program)
        {
            unsafe
            {
                _glDeleteProgram(program);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Attaches a shader object to a program object.
        /// </summary>
        /// <param name="program">The program object to which a shader object will be attached.</param>
        /// <param name="shader">The shader object that is to be attached.</param>
        public static void AttachShader(GLProgram program, GLShader shader)
        {
            unsafe
            {
                _glAttachShader(program.Handle, shader.Handle);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Links a program object.
        /// </summary>
        /// <param name="program">The handle of the program object to be linked.</param>
        public static void LinkProgram(GLProgram program)
        {
            unsafe
            {
                _glLinkProgram(program.Handle);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Returns a parameter from a program object.
        /// </summary>
        /// <param name="program">The program object to be queried.</param>
        /// <param name="pName">Specifies the object parameter.</param>
        /// <param name="pParam">The destination of the requested object parameter.</param>
        public static void GetProgramiv(GLProgram program, GLProgramParams pName, nint pParam)
        {
            unsafe
            {
                _glGetProgramiv(program.Handle, pName, pParam);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Get program information log.
        /// </summary>
        /// <param name="program">The program object to be queried.</param>
        /// <returns>The program information log.</returns>
        public static string? GetProgramInfoLog(GLProgram program)
        {
            int infoLogLength;
            unsafe
            {
                GetProgramiv(program, GLProgramParams.InfoLogLength, (nint)(&infoLogLength));
            }
            if (infoLogLength == 0)
            {
                return null;
            }

            if (infoLogLength < StringStackBufferSize)
            {
                unsafe
                {
                    var pLogBuffer = stackalloc sbyte[StringStackBufferSize];
                    _glGetProgramInfoLog(program.Handle, StringStackBufferSize, out var length, pLogBuffer);
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
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
#if ENABLE_ERROR_CHECK
                        ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
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
            unsafe
            {
                _glUseProgram(program.Handle);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
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
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
                }
            }
            foreach (var buffer in buffers)
            {
                if (buffer == 0 || buffer == (uint)GLErrors.InvalidValue)
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
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
                }
            }
        }

        /// <summary>
        /// Bind a named buffer object.
        /// </summary>
        /// <param name="target">The target to which the buffer object is bound.</param>
        /// <param name="buffer">The name of a buffer object.</param>
        public static void BindBuffer(GLTargets target, uint buffer)
        {
            unsafe
            {
                _glBindBuffer(target, buffer);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <param name="target">The target to which the buffer object is bound for <see cref="_glBufferData"/>.</param>
        /// <param name="size">The size in bytes of the buffer object's new data store.</param>
        /// <param name="data">A pointer to data that will be copied into the data store for initialization, or NULL if no data is to be copied.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public static void BufferData(GLTargets target, int size, byte[] data, GLUsage usage)
        {
            unsafe
            {
                fixed (byte* pData = &data[0])
                {
                    _glBufferData(target, (nint)size, (nint)pData, usage);
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
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
        public static void BufferData(GLTargets target, int size, ushort[] data, GLUsage usage)
        {
            unsafe
            {
                fixed (ushort* pData = &data[0])
                {
                    _glBufferData(target, (nint)size, (nint)pData, usage);
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
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
        public static void BufferData(GLTargets target, int size, float[] data, GLUsage usage)
        {
            unsafe
            {
                fixed (float* pData = &data[0])
                {
                    _glBufferData(target, (nint)size, (nint)pData, usage);
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
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
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
                }
            }
            foreach (var va in vertexArray)
            {
                if (va == 0 || va == (uint)GLErrors.InvalidValue)
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
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
                }
            }
        }

        /// <summary>
        /// Bind a vertex array object.
        /// </summary>
        /// <param name="arrayHandle">The name of the vertex array to bind.</param>
        public static void BindVertexArray(uint arrayHandle)
        {
            unsafe
            {
                _glBindVertexArray(arrayHandle);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Enable or disable a generic vertex attribute array.
        /// </summary>
        /// <param name="index">The index of the generic vertex attribute to be enabled.</param>
        public static void EnableVertexAttribArray(uint index)
        {
            unsafe
            {
                _glEnableVertexAttribArray(index);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Define an array of generic vertex attribute data.
        /// </summary>
        /// <param name="index">The index of the generic vertex attribute to be modified.</param>
        /// <param name="size">The number of components per generic vertex attribute.
        /// Must be 1, 2, 3, 4. Additionally, the symbolic constant GL_BGRA is accepted by <see cref="_glVertexAttribPointer"/>.
        /// The initial value is 4.</param>
        /// <param name="type">Specifies the data type of each component in the array.</param>
        /// <param name="normalized">Specifies whether fixed-point data values should be normalized (true) or converted directly
        /// as fixed-point values (false) when they are accessed.</param>
        /// <param name="stride">The byte offset between consecutive generic vertex attributes.
        /// If stride is 0, the generic vertex attributes are understood to be tightly packed in the array.
        /// The initial value is 0.</param>
        /// <param name="ptr">The offset of the first component of the first generic vertex attribute in the array
        /// in the data store of the buffer currently bound to the <see cref="GLTargets.ArrayBuffer"/> target.
        /// The initial value is 0.</param>
        public static void VertexAttribPointer(uint index, int size, GLValueTypes type, bool normalized, int stride, nint ptr)
        {
            unsafe
            {
                _glVertexAttribPointer(index, size, type, normalized ? (byte)GLBool.True : (byte)GLBool.False, stride, ptr);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">What kind of primitives to render.</param>
        /// <param name="count">The number of elements to be rendered.</param>
        /// <param name="type">the type of the values in indices.
        /// Must be one of <see cref="GLValueTypes.UnsignedByte"/>, <see cref="GLValueTypes.UnsignedShort"/>, or <see cref="GLValueTypes.UnsignedInt"/>.</param>
        public static void DrawElements(GLDrawModes mode, int count, GLValueTypes type)
        {
            unsafe
            {
                _glDrawElements(mode, count, type, 0);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">What kind of primitives to render.</param>
        /// <param name="count">The number of elements to be rendered.</param>
        /// <param name="indices">A byte offset (cast to a pointer type) into the buffer bound to <see cref="GLTargets.ElementArrayBuffer"/> to start reading indices from.</param>
        public static void DrawElements(GLDrawModes mode, int count, byte[] indices)
        {
            unsafe
            {
                fixed (byte* pIndices = &indices[0])
                {
                    _glDrawElements(mode, count, GLValueTypes.UnsignedByte, (nint)pIndices);
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
                }
            }
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">What kind of primitives to render.</param>
        /// <param name="count">The number of elements to be rendered.</param>
        /// <param name="indices">A byte offset (cast to a pointer type) into the buffer bound to <see cref="GLTargets.ElementArrayBuffer"/> to start reading indices from.</param>
        public static void DrawElements(GLDrawModes mode, int count, ushort[] indices)
        {
            unsafe
            {
                fixed (ushort* pIndices = &indices[0])
                {
                    _glDrawElements(mode, count, GLValueTypes.UnsignedShort, (nint)pIndices);
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
                }
            }
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">What kind of primitives to render.</param>
        /// <param name="count">The number of elements to be rendered.</param>
        /// <param name="indices">A byte offset (cast to a pointer type) into the buffer bound to <see cref="GLTargets.ElementArrayBuffer"/> to start reading indices from.</param>
        public static void DrawElements(GLDrawModes mode, int count, uint[] indices)
        {
            unsafe
            {
                fixed (uint* pIndices = &indices[0])
                {
                    _glDrawElements(mode, count, GLValueTypes.UnsignedInt, (nint)pIndices);
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
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
#if ENABLE_ERROR_CHECK
                    var location = _glGetAttribLocation(program.Handle, (nint)pName);
                    ThrowIfError();
                    return location;
#else
                    return _glGetAttribLocation(program.Handle, (nint)pName);
#endif  // ENABLE_ERROR_CHECK
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
#if ENABLE_ERROR_CHECK
                    var location = _glGetUniformLocation(program.Handle, (nint)pName);
                    ThrowIfError();
                    return location;
#else
                    return _glGetUniformLocation(program.Handle, (nint)pName);
#endif  // ENABLE_ERROR_CHECK
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
            unsafe
            {
                _glUniform1f(location, v0);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Specify the values of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">The location of the uniform variable to be modified.</param>
        /// <param name="v0">The first new values to be used for the specified uniform variable.</param>
        /// <param name="v1">The second new values to be used for the specified uniform variable.</param>
        public static void Uniform(int location, float v0, float v1)
        {
            unsafe
            {
                _glUniform2f(location, v0, v1);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
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
            unsafe
            {
                _glUniform3f(location, v0, v1, v2);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
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
            unsafe
            {
                _glUniform4f(location, v0, v1, v2, v3);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Specify the values of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">The location of the uniform variable to be modified.</param>
        /// <param name="v0">The new values to be used for the specified uniform variable.</param>
        public static void Uniform(int location, int v0)
        {
            unsafe
            {
                _glUniform1i(location, v0);
#if ENABLE_ERROR_CHECK
                ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
            }
        }

        /// <summary>
        /// Get OpenGL version.
        /// </summary>
        /// <param name="major">Major version.</param>
        /// <param name="minor">Minor version.</param>
        public static void GetVersion(out int major, out int minor)
        {
            try
            {
                major = GetIntegerv(GLNumericParams.MajorVersion);
                minor = GetIntegerv(GLNumericParams.MinorVersion);
            }
            catch
            {
                var version = GetString(GLStringParams.Version);
                if (version == null)
                {
                    major = 0;
                    minor = 0;
                    return;
                }
                var nums = version.Split(' ')[0].Split('.');
                major = int.Parse(nums[0]);
                minor = int.Parse(nums[1]);
            }
        }

        /// <summary>
        /// Get all extension names.
        /// </summary>
        /// <returns>Extension name list.</returns>
        public static string[] GetExtensions()
        {
            unsafe
            {
                var n = GetIntegerv(GLNumericParams.NumExtensions);
                if (n == 0)
                {
                    return [];
                }

                if (_glGetStringi == null)
                {
#if DISABLE_FUNCTION_POINTER
                    _glGetStringi = WGL.LoadFunctionAsDelegate<GLGetStringiFunc>("glGetStringi");
#else
                    _glGetStringi = (delegate* unmanaged[Cdecl]<GLStringParams, uint, sbyte*>)WGL.LoadFunction("glGetStringi");
#endif  // DISABLE_FUNCTION_POINTER
                }

                var extensions = new string[n];
                for (int i = 0; i < extensions.Length; i++)
                {
                    var pExtension = _glGetStringi(GLStringParams.Extensions, (uint)i);
#if ENABLE_ERROR_CHECK
                    ThrowIfError();
#endif  // ENABLE_ERROR_CHECK
                    extensions[i] = new string(pExtension);
                }
                return extensions;
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
            BindBuffer(GLTargets.ArrayBuffer, vbo[0]);
            BufferData(GLTargets.ArrayBuffer, vertices.Length * sizeof(float), vertices, GLUsage.StaticDraw);
            BindBuffer(GLTargets.ArrayBuffer, 0);
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
            BindBuffer(GLTargets.ElementArrayBuffer, ibo[0]);
            BufferData(GLTargets.ElementArrayBuffer, triangles.Length * sizeof(byte), triangles, GLUsage.StaticDraw);
            BindBuffer(GLTargets.ElementArrayBuffer, 0);
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
            BindBuffer(GLTargets.ElementArrayBuffer, ibo[0]);
            BufferData(GLTargets.ElementArrayBuffer, triangles.Length * sizeof(ushort), triangles, GLUsage.StaticDraw);
            BindBuffer(GLTargets.ElementArrayBuffer, 0);
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
            unsafe
            {
                GetShaderiv(shader, GLShaderParams.CompileStatus, (nint)(&compileResult));
            }

            var message = GetShaderInfoLog(shader);
            if (compileResult == (int)GLBool.False)
            {
                using (shader)
                {
                    throw new GLCompilationException(message);
                }
            }
            warnMessage = message;

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
            unsafe
            {
                GetShaderiv(shader, GLShaderParams.CompileStatus, (nint)(&compileResult));
            }

            var message = GetShaderInfoLog(shader);
            if (compileResult == (int)GLBool.False)
            {
                using (shader)
                {
                    throw new GLCompilationException(message);
                }
            }
            warnMessage = message;

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
            unsafe
            {
                GetProgramiv(program, GLProgramParams.LinkStatus, (nint)(&linkResult));
            }

            var message = GetProgramInfoLog(program);
            if (linkResult == 0)
            {
                using (program)
                {
                    throw new GLLinkException(message);
                }
            }
            warnMessage = message;

            return program;
        }

        /// <summary>
        /// Throw <see cref="GLException"/> if return value of <see cref="GetError()"/> indicates some errors.
        /// </summary>
        /// <exception cref="GLException">Thrown when any OpenGL error detected.</exception>
        public static void ThrowIfError()
        {
            var err = GetError(); 
            if (err != 0)
            {
                Throw(err);
            }
        }

        /// <summary>
        /// Throw <see cref="GLException"/> according to <paramref name="err"/>.
        /// </summary>
        /// <param name="err">Error code.</param>
        /// <exception cref="GLException">Thrown when any OpenGL error detected.</exception>
        [DoesNotReturn]
        private static void Throw(GLErrors err)
        {
            switch (err)
            {
                case GLErrors.InvalidEnum:
                    throw new GLException($"OepnGL Error: [{err}] (GL_INVALID_ENUM)");
                case GLErrors.InvalidValue:
                    throw new GLException($"OepnGL Error: [{err}] (GL_INVALID_VALUE)");
                case GLErrors.InvalidOperation:
                    throw new GLException($"OepnGL Error: [{err}] (GL_INVALID_OPERATION)");
                case GLErrors.StackOverflow:
                    throw new GLException($"OepnGL Error: [{err}] (GL_STACK_OVERFLOW)");
                case GLErrors.StackUnderflow:
                    throw new GLException($"OepnGL Error: [{err}] (GL_STACK_UNDERFLOW)");
                case GLErrors.OutOfMemory:
                    throw new GLException($"OepnGL Error: [{err}] (GL_OUT_OF_MEMORY)");
                default:
                    throw new GLException($"OepnGL Error: [{err}]");
            }
        }
    }
}
