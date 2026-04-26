using System;


namespace GlslWallpaper.Win32
{
    /// <summary>
    /// Available flags bits for <see cref="PixelFormatDescriptor.Flags"/>.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-pixelformatdescriptor"/>
    /// </remarks>
    [Flags]
    public enum PixelFormatDescriptorFlags : uint
    {
        /// <summary>
        /// The buffer is double-buffered.
        /// This flag and <see cref="SupportGdi"/> are mutually exclusive in the current generic implementation. 
        /// </summary>
        DoubleBuffer = 0x00000001,
        /// <summary>
        /// The buffer is stereoscopic.
        /// This flag is not supported in the current generic implementation.
        /// </summary>
        Stereo = 0x00000002,
        /// <summary>
        /// The buffer can draw to a window or device surface.
        /// </summary>
        DrawToWindow = 0x00000004,
        /// <summary>
        /// The buffer can draw to a memory bitmap.
        /// </summary>
        DrawToBitmap = 0x00000008,
        /// <summary>
        /// The buffer supports GDI drawing.
        /// This flag and <see cref="DoubleBuffer"/> are mutually exclusive in the current generic implementation.
        /// </summary>
        SupportGdi = 0x00000010,
        /// <summary>
        /// The buffer supports OpenGL drawing.
        /// </summary>
        SupportOpenGL = 0x00000020,
        /// <summary>
        /// The pixel format is supported by the GDI software implementation, which is also known as the generic implementation.
        /// If this bit is clear, the pixel format is supported by a device driver or hardware.
        /// </summary>
        GenericFormat = 0x00000040,
        /// <summary>
        /// The buffer uses RGBA pixels on a palette-managed device.
        /// A logical palette is required to achieve the best results for this pixel type.
        /// Colors in the palette should be specified according to the values of the cRedBits, cRedShift, cGreenBits, cGreenShift, cBluebits, and cBlueShift members.
        /// The palette should be created and realized in the device context before calling wglMakeCurrent.
        /// </summary>
        NeedPalette = 0x00000080,
        /// <summary>
        /// <para>Defined in the pixel format descriptors of hardware that supports one hardware palette in 256-color mode only.
        /// For such systems to use hardware acceleration, the hardware palette must be in a fixed order (for example, 3-3-2)
        /// when in RGBA mode or must match the logical palette when in color-index mode.
        /// When this flag is set, you must call SetSystemPaletteUse in your program to force a one-to-one mapping of the logical palette and the system palette.
        /// If your OpenGL hardware supports multiple hardware palettes and the device driver can allocate spare hardware palettes for OpenGL, this flag is typically clear.</para>
        /// <para>This flag is not set in the generic pixel formats.</para>
        /// </summary>
        NeedSystemPalette = 0x00000100,
        /// <summary>
        /// Specifies the content of the back buffer in the double-buffered main color plane following a buffer swap.
        /// Swapping the color buffers causes the exchange of the back buffer's content with the front buffer's content.
        /// Following the swap, the back buffer's content contains the front buffer's content before the swap.
        /// <see cref="SwapExchange"/> is a hint only and might not be provided by a driver. 
        /// </summary>
        /// <remarks>
        /// With the glAddSwapHintRectWIN extension function, this flag is included for the <see cref="PixelFormatDescriptor"/> pixel format structure.
        /// </remarks>
        SwapExchange = 0x00000200,
        /// <summary>
        /// Specifies the content of the back buffer in the double-buffered main color plane following a buffer swap.
        /// Swapping the color buffers causes the content of the back buffer to be copied to the front buffer.
        /// The content of the back buffer is not affected by the swap.
        /// <see cref="SwapCopy"/> is a hint only and might not be provided by a driver.
        /// </summary>
        /// <remarks>
        /// With the glAddSwapHintRectWIN extension function, this flag is included for the <see cref="PixelFormatDescriptor"/> pixel format structure.
        /// </remarks>
        SwapCopy = 0x00000400,
        /// <summary>
        /// Indicates whether a device can swap individual layer planes with pixel formats that include double-buffered overlay or underlay planes.
        /// Otherwise all layer planes are swapped together as a group.
        /// When this flag is set, wglSwapLayerBuffers is supported.
        /// </summary>
        SwapLayerBuffers = 0x00000800,
        /// <summary>
        /// The pixel format is supported by a device driver that accelerates the generic implementation.
        /// If this flag is clear and the PFD_GENERIC_FORMAT flag is set, the pixel format is supported by the generic implementation only.
        /// </summary>
        GenericAccelerated = 0x00001000,
        /// <summary>
        /// The requested pixel format can either have or not have a depth buffer.
        /// To select a pixel format without a depth buffer, you must specify this flag.
        /// The requested pixel format can be with or without a depth buffer.
        /// Otherwise, only pixel formats with a depth buffer are considered.
        /// </summary>
        /// <remarks>
        /// You can specify this bit flags when calling <see cref="Gdi32.ChoosePixelFormat"/>.
        /// </remarks>
        DepthDontcare = 0x20000000,
        /// <summary>
        /// The requested pixel format can be either single- or double-buffered.
        /// </summary>
        /// <remarks>
        /// You can specify this bit flags when calling <see cref="Gdi32.ChoosePixelFormat"/>.
        /// </remarks>
        DoubleBufferDontCare = 0x40000000,
        /// <summary>
        /// The requested pixel format can be either monoscopic or stereoscopic.
        /// </summary>
        /// <remarks>
        /// You can specify this bit flags when calling <see cref="Gdi32.ChoosePixelFormat"/>.
        /// </remarks>
        StereoDontcare = 0x80000000
    }
}
