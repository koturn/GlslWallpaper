using System.Runtime.InteropServices;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// The <see cref="PixelFormatDescriptor"/> structure describes the pixel format of a drawing surface.
    /// </summary>
    /// <remarks>
    /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-pixelformatdescriptor"/>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PixelFormatDescriptor
    {
        /// <summary>
        /// Specifies the size of this data structure.
        /// This value should be set to <c><see cref="Marshal.SizeOf(System.Type)">Marshal.SizeOf</see>(typeof(<see cref="PixelFormatDescriptor"/>))</c>.
        /// </summary>
        public ushort Size { get; set; }
        /// <summary>
        /// Specifies the version of this data structure. This value should be set to 1.
        /// </summary>
        public ushort Version { get; set; }
        /// <summary>
        /// A set of bit flags that specify properties of the pixel buffer.
        /// The properties are generally not mutually exclusive; you can set any combination of bit flags, with the exceptions noted.
        /// </summary>
        public PixelFormatDescriptorFlags Flags { get; set; }
        /// <summary>
        /// Specifies the type of pixel data.
        /// </summary>
        public PixelFormatDescriptorPixelType PixelType { get; set; }
        /// <summary>
        /// Specifies the number of color bitplanes in each color buffer.
        /// For RGBA pixel types, it is the size of the color buffer, excluding the alpha bitplanes.
        /// For color-index pixels, it is the size of the color-index buffer.
        /// </summary>
        public byte ColorBits { get; set; }
        /// <summary>
        /// Specifies the number of red bitplanes in each RGBA color buffer.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _redBits;
        /// <summary>
        /// Specifies the shift count for red bitplanes in each RGBA color buffer.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _redShift;
        /// <summary>
        /// Specifies the number of green bitplanes in each RGBA color buffer.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _greenBits;
        /// <summary>
        /// Specifies the shift count for green bitplanes in each RGBA color buffer.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _greenShift;
        /// <summary>
        /// Specifies the number of blue bitplanes in each RGBA color buffer.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _blueBits;
        /// <summary>
        /// Specifies the shift count for blue bitplanes in each RGBA color buffer.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _blueShift;
        /// <summary>
        /// Specifies the number of alpha bitplanes in each RGBA color buffer.
        /// Alpha bitplanes are not supported.
        /// </summary>
        /// <remarks>
        /// Zero or greater.
        /// </remarks>
        private readonly byte _alphaBits;
        /// <summary>
        /// Specifies the shift count for alpha bitplanes in each RGBA color buffer. Alpha bitplanes are not supported.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _alphaShift;
        /// <summary>
        /// Specifies the total number of bitplanes in the accumulation buffer.
        /// </summary>
        /// <remarks>
        /// Zero or greater.
        /// </remarks>
        public byte AccumBits { get; set; }
        /// <summary>
        /// Specifies the number of red bitplanes in the accumulation buffer.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _accumRedBits;
        /// <summary>
        /// Specifies the number of green bitplanes in the accumulation buffer.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _accumGreenBits;
        /// <summary>
        /// Specifies the number of blue bitplanes in the accumulation buffer.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _accumBlueBits;
        /// <summary>
        /// Specifies the number of alpha bitplanes in the accumulation buffer.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _accumAlphaBits;
        /// <summary>
        /// Specifies the depth of the depth (z-axis) buffer.
        /// </summary>
        /// <remarks>
        /// Zero or greater.
        /// </remarks>
        public byte DepthBits { get; set; }
        /// <summary>
        /// Specifies the depth of the stencil buffer.
        /// </summary>
        /// <remarks>
        /// Zero or greater.
        /// </remarks>
        public byte StencilBits { get; set; }
        /// <summary>
        /// Specifies the number of auxiliary buffers.
        /// Auxiliary buffers are not supported.
        /// </summary>
        /// <remarks>
        /// Zero or greater.
        /// </remarks>
        private readonly byte _auxBuffers;
        /// <summary>
        /// Ignored. Earlier implementations of OpenGL used this member, but it is no longer used.
        /// </summary>
        private readonly sbyte _layerType;
        /// <summary>
        /// Specifies the number of overlay and underlay planes.
        /// Bits 0 through 3 specify up to 15 overlay planes and bits 4 through 7 specify up to 15 underlay planes.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly byte _reserved;
        /// <summary>
        /// Ignored.
        /// Earlier implementations of OpenGL used this member, but it is no longer used.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly uint _layerMask;
        /// <summary>
        /// Specifies the transparent color or index of an underlay plane.
        /// When the pixel type is RGBA, dwVisibleMask is a transparent RGB color value.
        /// When the pixel type is color index, it is a transparent index value.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly uint _visibleMask;
        /// <summary>
        /// Ignored.
        /// Earlier implementations of OpenGL used this member, but it is no longer used.
        /// </summary>
        /// <remarks>
        /// Not used.
        /// </remarks>
        private readonly uint _damageMask;
    }
}
