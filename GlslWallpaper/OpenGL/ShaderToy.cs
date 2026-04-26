namespace GlslWallpaper.OpenGL
{
    /// <summary>
    /// ShaderToy style source converter.
    /// </summary>
    public static class ShaderToy
    {
        /// <summary>
        /// Convert geeker style source to notmal GLSL fragment shader source.
        /// </summary>
        /// <param name="source">geeker style source.</param>
        /// <returns>Normal GLSL fragment shader source.</returns>
        public static string Convert(string source)
        {
            return @"#version 300 es
precision highp float;
uniform vec3  iResolution;  // viewport resolution (in pixels)
uniform float iTime;        // shader playback time (in seconds)
uniform float iTimeDelta;   // render time (in seconds)
uniform float iFrameRate;   // shader frame rate
uniform int   iFrame;       // shader playback frame
uniform vec4  iMouse;       // mouse pixel coords. xy: current (if MLB down), zw: click
uniform vec4  iDate;        // (year, month, day, time in seconds)
out vec4 FragColor;
void mainImage(out vec4 fragColor, vec2 fragCoord);
void main(void)
{
    mainImage(/* out */ FragColor, gl_FragCoord.xy);
}
#line 1
" + source;
        }
    }
}
