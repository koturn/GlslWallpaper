namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Possible values for second argument of <see cref="GL.GetProgramiv(GLProgram, GLProgramParams, nint)"/>.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgram.xhtml"><c>glGetProgram</c></seealso>
    /// </remarks>
    public enum GLProgramParams : uint
    {
        /// <summary>
        /// Params returns an array of three integers containing the local work group size of the compute program as specified by its input layout qualifier(s).
        /// program must be the name of a program object that has been previously linked successfully and contains a binary for the compute shader stage.
        /// </summary>
        ComputeWorkGroupSize = 0x8267,
        /// <summary>
        /// params returns the length of the program binary, in bytes that will be returned by a call to <see href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetProgramBinary.xhtml">glGetProgramBinary.</see>
        /// When a progam's <see cref="LinkStatus"/> is <see cref="GLBool.False"/>, its program binary length is zero.
        /// </summary>
        ProgramBinaryLength = 0x8741,
        /// <summary>
        /// Params returns the maximum number of vertices that the geometry shader in program will output.
        /// </summary>
        GeometryVerticesOut = 0x8916,
        /// <summary>
        /// Params returns a symbolic constant indicating the primitive type accepted as input to the geometry shader contained in program.
        /// </summary>
        GeometryInputType = 0x8917,
        /// <summary>
        /// Params returns a symbolic constant indicating the primitive type that will be output by the geometry shader contained in program.
        /// </summary>
        GeometryOutputType = 0x8918,
        /// <summary>
        /// params returns a symbolic constant indicating the buffer mode used when transform feedback is active. This may be GL_SEPARATE_ATTRIBS or GL_INTERLEAVED_ATTRIBS.
        /// </summary>
        TransformFeedbackBufferMode = 0x8c7f,
        /// <summary>
        /// Params returns <see cref="GLBool.True"/> if shader is currently flagged for deletion, and <see cref="GLBool.False"/> otherwise.
        /// </summary>
        DeleteStatus = GLShaderParams.DeleteStatus,
        /// <summary>
        /// Params returns <see cref="GLBool.True"/> or if the last validation operation on program was successful, and <see cref="GLBool.False"/> otherwise.
        /// </summary>
        ValidateStatus = 0x8b83,
        /// <summary>
        /// Params returns GL_TRUE if the last link operation on program was successful, and GL_FALSE otherwise.
        /// </summary>
        LinkStatus = 0x8b82,
        /// <summary>
        /// Params returns the number of characters in the information log for shader or program including the null termination character
        /// (i.e., the size of the character buffer required to store the information log).
        /// If shader or program has no information log, a value of 0 is returned.
        /// </summary>
        InfoLogLength = GLShaderParams.InfoLogLength,
        /// <summary>
        /// Params returns the number of shader objects attached to program.
        /// </summary>
        AttachedShaders = 0x8b85,
        /// <summary>
        /// Params returns the number of active uniform variables for program.
        /// </summary>
        ActiveUniforms = 0x8b86,
        /// <summary>
        /// params returns the length of the longest active uniform variable name for program, including the null termination character
        /// (i.e., the size of the character buffer required to store the longest uniform variable name).
        /// If no active uniform variables exist, 0 is returned.
        /// </summary>
        ActiveUniformMaxLength = 0x8b87,
        /// <summary>
        /// Params returns the number of active attribute variables for program.
        /// </summary>
        ActiveAttributes = 0x8b89,
        /// <summary>
        /// params returns the length of the longest variable name to be used for transform feedback, including the null-terminator.
        /// </summary>
        TransformFeedbackVaryingMaxLength = 0x8c76,
        /// <summary>
        /// Params returns the number of varying variables to capture in transform feedback mode for the program.
        /// </summary>
        TransformFeedbackVaryings = 0x8c83,
        /// <summary>
        /// Params returns the length of the longest active attribute name for program, including the null termination character
        /// (i.e., the size of the character buffer required to store the longest attribute name).
        /// If no active attributes exist, 0 is returned.
        /// </summary>
        ActiveAttributeMaxLength = 0x8b8a,
        /// <summary>
        /// Params returns the number of active attribute atomic counter buffers used by program.
        /// </summary>
        ActiveAtomicCounterBuffers = 0x92d9
    }
}
