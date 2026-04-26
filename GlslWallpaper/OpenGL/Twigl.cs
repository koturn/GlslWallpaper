using GlslWallpaper.Resources;


namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// Twigl style source converter.
    /// </summary>
    public static class Twigl
    {
        /// <summary>
        /// Convert geeker style source to notmal GLSL fragment shader source.
        /// </summary>
        /// <param name="source">geeker style source.</param>
        /// <returns>Normal GLSL fragment shader source.</returns>
        public static string ConvertGeeker(string source)
        {
            return @"precision highp float;
uniform vec2 r;  // Screen resolution.
uniform vec2 m;  // Mouse position.
uniform float t;  // Elapsed time.
uniform float f;  // Frame count.
uniform float s;  // Sound frequency.
uniform sampler2D b;  // Back buffer.
#line 1
" + source;
        }

        /// <summary>
        /// Convert geekest style source to notmal GLSL fragment shader source.
        /// </summary>
        /// <param name="source">geekest style source.</param>
        /// <returns>Normal GLSL fragment shader source.</returns>
        public static string ConvertGeekest(string source)
        {
            return @$"precision highp float;
uniform vec2 r;  // Screen resolution.
uniform vec2 m;  // Mouse position.
uniform float t;  // Elapsed time.
uniform float f;  // Frame count.
uniform float s;  // Sound frequency.
uniform sampler2D b;  // Back buffer.
#define FC gl_FragCoord
{AppResource.GetText("noise.glsl")}
void main()
{{
#line 1
{source}
}}
";
        }

        /// <summary>
        /// Convert geeker300es style source to notmal GLSL fragment shader source.
        /// </summary>
        /// <param name="source">geeker300es style source.</param>
        /// <returns>Normal GLSL fragment shader source.</returns>
        public static string ConvertGeeker300es(string source)
        {
            return @"#version 300 es
precision highp float;
uniform vec2 r;  // Screen resolution.
uniform vec2 m;  // Mouse position.
uniform float t;  // Elapsed time.
uniform float f;  // Frame count.
uniform float s;  // Sound frequency.
uniform sampler2D b;  // Back buffer.
out vec4 o;  // Output color.
#line 1
" + source;
        }

        /// <summary>
        /// Convert geekest300es style source to notmal GLSL fragment shader source.
        /// </summary>
        /// <param name="source">geekest300es style source.</param>
        /// <returns>Normal GLSL fragment shader source.</returns>
        public static string ConvertGeekest300es(string source)
        {
            return @$"#version 300 es
precision highp float;
uniform vec2 r;  // Screen resolution.
uniform vec2 m;  // Mouse position.
uniform float t;  // Elapsed time.
uniform float f;  // Frame count.
uniform float s;  // Sound frequency.
uniform sampler2D b;  // Back buffer.
out vec4 o;  // Output color.
#define FC gl_FragCoord
{AppResource.GetText("noise.glsl")}
void main()
{{
#line 1
{source}
}}
";
        }
    }
}
