using System.Drawing;
using GameEngine.Core;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Shader = GameEngine.Core.Shader;

namespace Game;

public class GameWindow : GameWindowBase
{
    private Mesh _mesh;
    private Mesh _mesh1;
    private Shader _shader;
    private Texture2D _texture;
    private Texture2D _texture2;

    protected override void OnCreateWindow(ref WindowOptions windowOptions)
    {
        base.OnCreateWindow(ref windowOptions);
        windowOptions.Size = new Vector2D<int>(800, 600);
        windowOptions.Title = "Game";
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        Graphics.SetClearColor(Color.CornflowerBlue);
        Graphics.SetMaterial(Material.Default());

        _mesh = new Mesh();
        float[] vertices =
        {
            0.5f, 0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f, 0.5f, 0.0f,
        };


        uint[] indices =
        {
            0u, 1u, 3u,
            1u, 2u, 3u
        };
        _mesh.SetColors(new[]
        {
            0.0f, 0.0f, 1.0f,
            0.0f, 0.0f, 1.0f,
            0.0f, 0.0f, 1.0f,
            0.0f, 0.0f, 1.0f,
        });
        
    

        _mesh.SetVertices(vertices);
        _mesh.SetTriangles(indices);

        _mesh1 = new Mesh();

        float[] vertices1 =
        {
            0.75f, 0.75f, 0.0f,
            0.75f, -0.75f, 0.0f,
            -0.75f, -0.75f, 0.0f,
        };

        uint[] indices1 =
        {
            0u, 1u, 2u,
        };

        _mesh1.SetColors(new[]
        {
            1.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 1.0f
        });
        
        _mesh1.SetTextureCoords(new[]
        {
            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
        });
        _mesh1.SetVertices(vertices1);
        _mesh1.SetTriangles(indices1);

        _shader = Shader.Create(@"D:\UnityProjects\GameEngine\GameEngine\Shaders\Default.vert",
            @"D:\UnityProjects\GameEngine\GameEngine\Shaders\Default.frag");

        _texture = new Texture2D(@"D:\UnityProjects\GameEngine\GameEngine\Textures\wood.png");
        _texture2 = new Texture2D(@"D:\UnityProjects\GameEngine\GameEngine\Textures\awesomeface.png");
        

    }

    protected override void OnRender(double obj)
    {
        Graphics.ClearColor(ClearBufferMask.ColorBufferBit);
        Shader.SetFloat(_shader.Id, "t", (float)Time % 1);
        Texture2D.Bind(_texture.Id, 0);
        Texture2D.Bind(_texture2.Id, 1);
        
        Shader.SetInt(_shader.Id, "texture1", 0);

        Shader.SetInt(_shader.Id, "texture2", 1);



        Graphics.Render(_mesh, _shader);
        Graphics.Render(_mesh1, _shader);
    }
}