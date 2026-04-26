using System;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// OpenGL buffers.
    /// </summary>
    public sealed class GLBuffer : IDisposable
    {
        /// <summary>
        /// OpenGL buffers.
        /// </summary>
        private readonly uint[] _buffers;

        /// <summary>
        /// A flag property which indicates this instance is disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }
        /// <summary>
        /// Get buffer handle.
        /// </summary>
        /// <param name="index">Buffer index.</param>
        /// <returns>Buffer handle</returns>
        public uint this[int index] => _buffers[index];


        /// <summary>
        /// Create OpenGL buffers.
        /// </summary>
        /// <param name="count">Number of buffers.</param>
        public GLBuffer(int count)
        {
            var buffers = new uint[count];
            GL.GenBuffers(buffers);
            _buffers = buffers;
        }


        /// <summary>
        /// Release all resources used by the <see cref="GLBuffer"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Release resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                GL.DeleteBuffers(_buffers);
            }

            IsDisposed = true;
        }
    }
}
