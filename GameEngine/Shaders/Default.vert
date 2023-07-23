﻿#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aColor; 
layout (location = 2) in vec2 aTexCoord;

out vec3 ourColor;
out vec2 texCoord;

uniform mat4 TRS;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = vec4(aPosition,1) * model * view * projection;
    
    ourColor = aColor;
    texCoord = aTexCoord;
}