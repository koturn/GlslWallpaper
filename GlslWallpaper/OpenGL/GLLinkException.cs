using System;
using System.Runtime.Serialization;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Represents GLSL link errors.
    /// </summary>
    [Serializable]
    public class GLLinkException
        : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GLLinkException"/> class.
        /// </summary>
        public GLLinkException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLLinkException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public GLLinkException(string? message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLLinkException"/> class with
        /// a specified error message and a reference to the inner exception that
        /// is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference,
        /// the current exception is raised in a catch block that handles the inner exception.</param>
        public GLLinkException(string? message, Exception? inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLLinkException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
#if NET8_0_OR_GREATER
        [Obsolete("This ctor is only for .NET Framework", DiagnosticId = "SYSLIB0051")]
#endif  // NET8_0_OR_GREATER
        protected GLLinkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }  // class GLLinkException
}
