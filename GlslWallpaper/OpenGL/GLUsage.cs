namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// OpenGL buffer usage values.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glBufferData.xhtml"><c>glBufferData</c></seealso>
    /// </remarks>
    public enum GLUsage : uint
    {
        /// <summary>
        /// The data store contents will be modified once and used at most a few times,
        /// and the data store contents are modified by the application, and used as the source for GL drawing and image specification commands.
        /// </summary>
        StreamDraw = 0x88e0,
        /// <summary>
        /// The data store contents will be modified once and used at most a few times,
        /// and the data store contents are modified by reading data from the GL, and used to return that data when queried by the application.
        /// </summary>
        StreamRead = 0x88e1,
        /// <summary>
        /// The data store contents will be modified once and used at most a few times,
        /// and the data store contents are modified by reading data from the GL, and used as the source for GL drawing and image specification commands.
        /// </summary>
        StreamCopy = 0x88e2,
        /// <summary>
        /// The data store contents will be modified once and used many times,
        /// and the data store contents are modified by the application, and used as the source for GL drawing and image specification commands.
        /// </summary>
        StaticDraw = 0x88e4,
        /// <summary>
        /// The data store contents will be modified once and used many times,
        /// and the data store contents are modified by reading data from the GL, and used to return that data when queried by the application.
        /// </summary>
        StaticRead = 0x88e5,
        /// <summary>
        /// The data store contents will be modified once and used many times,
        /// and the data store contents are modified by reading data from the GL, and used as the source for GL drawing and image specification commands.
        /// </summary>
        StaticCopy = 0x88e6,
        /// <summary>
        /// The data store contents will be modified repeatedly and used many times.
        /// and the data store contents are modified by the application, and used as the source for GL drawing and image specification commands.
        /// </summary>
        DynamicDraw = 0x88e8,
        /// <summary>
        /// The data store contents will be modified repeatedly and used many times.
        /// and the data store contents are modified by reading data from the GL, and used to return that data when queried by the application.
        /// </summary>
        DynamicRead = 0x88e9,
        /// <summary>
        /// The data store contents will be modified repeatedly and used many times.
        /// and the data store contents are modified by reading data from the GL, and used as the source for GL drawing and image specification commands.
        /// </summary>
        DynamicCopy = 0x88ea
    }
}
