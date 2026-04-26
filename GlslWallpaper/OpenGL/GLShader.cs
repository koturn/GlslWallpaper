using System;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// OpenGL shader class.
    /// </summary>
    public abstract class GLShader : IDisposable
    {
        /// <summary>
        /// The handle of the shader object.
        /// </summary>
        public uint Handle { get; private set; }

        /// <summary>
        /// Create OpenGL shader object.
        /// </summary>
        /// <param name="type">Shader type.</param>
        /// <exception cref="GLException">Thrown when failed to create shader object.</exception>
        public GLShader(uint type)
        {
            var handle = GL.CreateShader(type);
            if (handle == 0 || handle == GLConst.GL_INVALID_ENUM)
            {
                throw new GLException($"Failed to create shader object. [{handle}]");
            }
            Handle = handle;
        }

        /// <summary>
        /// Delete shader object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Delete the shader object.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (Handle == 0)
            {
                return;
            }

            if (disposing)
            {
                GL.DeleteShader(Handle);
                Handle = 0;
            }
        }
    }
}
