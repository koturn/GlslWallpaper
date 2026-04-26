using System;
using System.Runtime.Serialization;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Represents OpenGL errors.
    /// </summary>
    [Serializable]
    public class GLException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GLException"/> class.
        /// </summary>
        public GLException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public GLException(string? message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLException"/> class with
        /// a specified error message and a reference to the inner exception that
        /// is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference,
        /// the current exception is raised in a catch block that handles the inner exception.</param>
        public GLException(string? message, Exception? inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
#if NET8_0_OR_GREATER
        [Obsolete("This ctor is only for .NET Framework", DiagnosticId = "SYSLIB0051")]
#endif  // NET8_0_OR_GREATER
        protected GLException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
