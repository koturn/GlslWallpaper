namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Possible values for second argument of <see cref="GL.GetShaderiv(GLShader, GLShaderParams, nint)"/>.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetShader.xhtml"><c>glGetShader</c></seealso>
    /// </remarks>
    public enum GLShaderParams : uint
    {
        /// <summary>
        /// Params returns <see cref="GLShaderTypes.VertexShader"/> if shader is a vertex shader object,
        /// <see cref="GLShaderTypes.GeometryShader"/> if shader is a geometry shader object,
        /// and <see cref="GLShaderTypes.FragmentShader"/> if shader is a fragment shader object.
        /// </summary>
        ShaderType = 0x8b4f,
        /// <summary>
        /// Params returns <see cref="GLBool.True"/> if shader is currently flagged for deletion, and <see cref="GLBool.False"/> otherwise.
        /// </summary>
        DeleteStatus = 0x8b80,
        /// <summary>
        /// Params returns GL_TRUE if the last compile operation on shader was successful, and GL_FALSE otherwise.
        /// </summary>
        CompileStatus = 0x8b81,
        /// <summary>
        /// Params returns the number of characters in the information log for shader or program including the null termination character
        /// (i.e., the size of the character buffer required to store the information log).
        /// If shader or program has no information log, a value of 0 is returned.
        /// </summary>
        InfoLogLength = 0x8b84,
        /// <summary>
        /// Params returns the length of the concatenation of the source strings that make up the shader source for the shader, including the null termination character.
        /// (i.e., the size of the character buffer required to store the shader source).
        /// If no source code exists, 0 is returned.
        /// </summary>
        ShaderSourceLength = 0x8b88
    }
}
