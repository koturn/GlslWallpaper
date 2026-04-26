using System;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// OpenGL program class.
    /// </summary>
    public sealed class GLProgram : IDisposable
    {
        /// <summary>
        /// The handle of the program object.
        /// </summary>
        public uint Handle { get; private set; }

        /// <summary>
        /// Create OpenGL shader object.
        /// </summary>
        /// <exception cref="GLException">Thrown when failed to create program object.</exception>
        public GLProgram()
        {
            var handle = GL.CreateProgram();
            if (handle == 0)
            {
                throw new GLException("Failed to create GL program.");
            }
            Handle = handle;
        }

        /// <summary>
        /// Delete the program object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Delete the program object.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (Handle == 0)
            {
                return;
            }

            if (disposing)
            {
                GL.DeleteProgram(Handle);
                Handle = 0;
            }
        }
    }
}
