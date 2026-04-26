using System;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// OpenGL vertex array handle class.
    /// </summary>
    public sealed class GLVertexArray : IDisposable
    {
        /// <summary>
        /// Vertex array handles.
        /// </summary>
        private uint[] _vertexArray;
        /// <summary>
        /// Access N-th vertex array.
        /// </summary>
        /// <param name="index">Index of the vertex array.</param>
        /// <returns>N-th vertex array.</returns>
        public uint this[int index] => _vertexArray[index];

        /// <summary>
        /// Create OpenGL vertex arrays.
        /// </summary>
        /// <param name="count">The number of vertex array object names to generate.</param>
        public GLVertexArray(int count)
        {
            var vertexArray = new uint[count];
            GL.GenVertexArrays(vertexArray);
            _vertexArray = vertexArray;
        }

        /// <summary>
        /// Delete the vertex array.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Delete the vertex array.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (_vertexArray.Length == 0)
            {
                return;
            }

            if (disposing)
            {
                GL.DeleteVertexArrays(_vertexArray);
            }

            _vertexArray = [];
        }
    }
}
