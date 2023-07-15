#version 330 core

uniform float t;
in vec3 ourColor;
out vec4 out_color;

in vec2 texCoord;
uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
    out_color = mix(texture(texture1, texCoord), texture(texture2, texCoord), t);
}

//void main()
//{
//    out_color = texture(ourTexture, texCoord) * vec4(ourColor, 1.0);
//
//}


//void main()
//{
//    out_color = vec4(ourColor,1.0);
//}