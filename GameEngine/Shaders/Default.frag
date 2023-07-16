#version 330 core

in vec3 ourColor;
in vec2 texCoord;

out vec4 out_color;



void main()
{
    out_color = vec4(ourColor,1.0);
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