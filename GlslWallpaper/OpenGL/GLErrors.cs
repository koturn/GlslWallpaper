namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// OpenGL error codes.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glGetError.xhtml"><c>glGetError</c></seealso>
    /// </remarks>
    public enum GLErrors : uint
    {
        /// <summary>
        /// No error has been recorded. The value of this symbolic constant is guaranteed to be zero.
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// One of error code obtained from <see cref="GL.GetError"/>;
        /// which means that an unacceptable value is specified for an enumerated argument.
        /// The offending command is ignored and has no other side effect than to set the error flag.
        /// </summary>
        InvalidEnum = 0x0500,
        /// <summary>
        /// One of error code obtained from <see cref="GL.GetError"/>;
        /// which means that a numeric argument is out of range.
        /// The offending command is ignored and has no other side effect than to set the error flag.
        /// </summary>
        InvalidValue = 0x0501,
        /// <summary>
        /// One of error code obtained from <see cref="GL.GetError"/>;
        /// which means that the specified operation is not allowed in the current state.
        /// The offending command is ignored and has no other side effect than to set the error flag.
        /// </summary>
        InvalidOperation = 0x0502,
        /// <summary>
        /// One of error code obtained from <see cref="GL.GetError"/>;
        /// which means that an attempt has been made to perform an operation that would cause an internal stack to overflow.
        /// </summary>
        StackOverflow = 0x0503,
        /// <summary>
        /// One of error code obtained from <see cref="GL.GetError"/>;
        /// which means that an attempt has been made to perform an operation that would cause an internal stack to underflow.
        /// </summary>
        StackUnderflow = 0x0504,
        /// <summary>
        /// One of error code obtained from <see cref="GL.GetError"/>;
        /// which means that there is not enough memory left to execute the command.
        /// The state of the GL is undefined, except for the state of the error flags, after this error is recorded.
        /// </summary>
        OutOfMemory = 0x0505
    }
}
