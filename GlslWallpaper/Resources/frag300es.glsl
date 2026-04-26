#version 300 es
precision highp float;
uniform vec2 u_resolution;
uniform float u_time;
out vec4 outColor;
void main()
{
    vec2 r = u_resolution;
    vec2 p = (gl_FragCoord.xy * 2. - r) / min(r.x, r.y);
    outColor = vec4(p.xy, mod(u_time, 1.0), 1.0);
}
