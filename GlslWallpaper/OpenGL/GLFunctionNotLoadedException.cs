using System;
using System.Runtime.Serialization;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Represents that OpenGL functions not loaded.
    /// </summary>
    [Serializable]
    public class GLFunctionNotLoadedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GLFunctionNotLoadedException"/> class.
        /// </summary>
        public GLFunctionNotLoadedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLFunctionNotLoadedException"/> class with a specified error message.
        /// </summary>
        /// <param name="funcName">OpenGL function name.</param>
        public GLFunctionNotLoadedException(string? funcName)
            : base(funcName + "() is not loaded.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLFunctionNotLoadedException"/> class with
        /// a specified error message and a reference to the inner exception that
        /// is the cause of this exception.
        /// </summary>
        /// <param name="funcName">OpenGL function name.</param>
        /// <param name="inner">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference,
        /// the current exception is raised in a catch block that handles the inner exception.</param>
        public GLFunctionNotLoadedException(string? funcName, Exception? inner)
            : base(funcName + "() is not loaded.", inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLFunctionNotLoadedException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
#if NET8_0_OR_GREATER
        [Obsolete("This ctor is only for .NET Framework", DiagnosticId = "SYSLIB0051")]
#endif  // NET8_0_OR_GREATER
        protected GLFunctionNotLoadedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
