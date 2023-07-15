#version 330 core

uniform float t;
in vec3 ourColor;
out vec4 out_color;

void main()
{
    out_color = vec4(ourColor,1.0);
}