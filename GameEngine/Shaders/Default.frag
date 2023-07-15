#version 330 core

uniform float t;

out vec4 out_color;

void main()
{
    out_color = vec4(1.0, t, 0.2, 1.0);
}