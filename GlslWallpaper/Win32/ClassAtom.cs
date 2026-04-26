using System;


namespace GlslWallpaper.Win32
{
    internal class ClassAtom : IDisposable
    {
        public ushort Handle { get; private set; }
        public nint InstanceHandle { get; }

        public ClassAtom(ushort handle)
            : this(handle, 0)
        {
        }

        public ClassAtom(ushort handle, nint hInstance)
        {
            Handle = handle;
            InstanceHandle = hInstance;
        }


        /// <summary>
        /// Release resources.
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
                NativeWindow.SafeNativeMethods.UnregisterClass((nint)Handle, InstanceHandle);
            }

            Handle = 0;
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // /// <summary>
        // /// Release unmanaged resources.
        // /// </summary>
        // ~ClassAtom()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(false);
        // }

        /// <summary>
        /// Release all resources used by the <see cref="ClassAtom"/> object.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(true);
            // TODO: Add following code when finalizer is implemented.
            // GC.SuppressFinalize(this);
        }
    }
}
