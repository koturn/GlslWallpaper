namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Draw mode of <see cref="GL.DrawElements(GLDrawModes, int, byte[])"/>, <see cref="GL.DrawElements(GLDrawModes, int, ushort[])"/>
    /// and <see cref="GL.DrawElements(GLDrawModes, int, uint[])"/>.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://registry.khronos.org/OpenGL-Refpages/gl4/html/glDrawElements.xhtml"><c>glDrawElements</c></seealso>
    /// </remarks>
    public enum GLDrawModes : uint
    {
        /// <summary>
        /// Treats each vertex as a single point. Vertex n defines point n. N points are drawn.
        /// </summary>
        Points = 0x0000,
        /// <summary>
        /// Treats each pair of vertices as an independent line segment.
        /// Vertices 2n - 1 and 2n define line n.
        /// N/2 lines are drawn.
        /// </summary>
        Lines = 0x0001,
        /// <summary>
        /// Draws a connected group of line segments from the first vertex to the last, then back to the first.
        /// Vertices n and n + 1 define line n.
        /// The last line, however, is defined by vertices N and 1.
        /// N lines are drawn.
        /// </summary>
        LineLoop = 0x0002,
        /// <summary>
        /// Draws a connected group of line segments from the first vertex to the last.
        /// Vertices n and n+1 define line n.
        /// N - 1 lines are drawn.
        /// </summary>
        LineStrip = 0x0003,
        /// <summary>
        /// Treats each triplet of vertices as an independent triangle.
        /// Vertices 3n−2, 3n−1, and 3n define triangle n.
        /// N/3 triangles are drawn.
        /// </summary>
        Triangles = 0x0004,
        /// <summary>
        /// Draws a connected group of triangles.
        /// One triangle is defined for each vertex presented after the first two vertices.
        /// For odd n, vertices n, n + 1, and n + 2 define triangle n.
        /// For even n, vertices n + 1, n, and n + 2 define triangle n.
        /// N - 2 triangles are drawn.
        /// </summary>
        TriangleStrip = 0x0005,
        /// <summary>
        /// Draws a connected group of triangles.
        /// One triangle is defined for each vertex presented after the first two vertices.
        /// Vertices 1, n + 1, n + 2 define triangle n.
        /// N - 2 triangles are drawn.
        /// </summary>
        TriangleFan = 0x0006,
        /// <summary>
        /// Treats each group of four vertices as an independent quadrilateral.
        /// Vertices 4n - 3, 4n - 2, 4n - 1, and 4n define quadrilateral n.
        /// N/4 quadrilaterals are drawn.
        /// </summary>
        Quads = 0x0007,
        /// <summary>
        /// Draws a connected group of quadrilaterals.
        /// One quadrilateral is defined for each pair of vertices presented after the first pair.
        /// Vertices 2n - 1, 2n, 2n + 2, and 2n + 1 define quadrilateral n.
        /// N/2 - 1 quadrilaterals are drawn.
        /// Note that the order in which vertices are used to construct a quadrilateral from strip data is different from that used with independent data.
        /// </summary>
        QuadStrip = 0x0008,
        /// <summary>
        /// Draws a single, convex polygon.
        /// Vertices 1 through N define this polygon.
        /// </summary>
        Polygon = 0x0009,
        /// <summary>
        /// <para>Draws individual line segments with adjacency information.
        /// Each group of four vertices defines one line segment.
        /// The second and third vertices define the line segment, while the first and fourth vertices provide adjacency information for the two endpoints.
        /// The number of line segments drawn is N/4.</para>
        /// <para>Available only if the GL version is 3.2 or greater.</para>
        /// </summary>
        LinesAdjacency = 0x000a,
        /// <summary>
        /// <para>Draws a connected group of line segments with adjacency information.
        /// A line segment is defined for each vertex after the first three vertices.
        /// For line segment n, vertices n + 1 and n + 2 define the line segment, while vertices n and n + 3 provide adjacency information for the endpoints.
        /// The number of line segments drawn is N - 3.</para>
        /// <para>Available only if the GL version is 3.2 or greater.</para>
        /// </summary>
        LineStripAdjacency = 0x000B,
        /// <summary>
        /// <para>Draws individual triangles with adjacency information.
        /// Each group of six vertices defines one triangle.
        /// The first, third, and fifth vertices define the triangle, while the second, fourth, and sixth vertices provide adjacency information for the three edges.
        /// The number of triangles drawn is N/6.</para>
        /// <para>Available only if the GL version is 3.2 or greater.</para>
        /// </summary>
        TrianglesAdjacency = 0x000c,
        /// <summary>
        /// <para>Draws a connected group of triangles with adjacency information.
        /// One triangle is defined for every two vertices presented after the first four vertices.
        /// For triangle n, vertices 2n - 1, 2n + 1, and 2n + 3 define the triangle, while vertices 2n and 2n + 2 provide adjacency information for the triangle edges.
        /// The number of triangles drawn is N/2 - 2.</para>
        /// <para>Available only if the GL version is 3.2 or greater.</para>
        /// </summary>
        TriangleStripAdjacency = 0x000d,
        /// <summary>
        /// Draws a sequence of independent patches.
        /// Each group of N vertices defines a separate patch, where N is specified by glPatchParameteri with GL_PATCH_VERTICES.
        /// The number of patches drawn is N/V, where N is the number of vertices specified and V is the number of vertices per patch.
        /// </summary>
        Patches = 0x000e
    }
}
