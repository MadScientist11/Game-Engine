using System.Drawing;
using System.Numerics;
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
    private GameObject _gameObject;
    private World _world;

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
        
        _world = new World(Graphics);

        Mesh mesh = CreateQuadMesh();

        _texture = new Texture2D(@"D:\UnityProjects\GameEngine\GameEngine\Textures\wood.png");
        _texture2 = new Texture2D(@"D:\UnityProjects\GameEngine\GameEngine\Textures\awesomeface.png");
        _shader = Shader.Create(@"D:\UnityProjects\GameEngine\GameEngine\Shaders\Default.vert",
            @"D:\UnityProjects\GameEngine\GameEngine\Shaders\Default.frag");

        Material material = new Material(_shader, _texture);

        _gameObject = new GameObject();
        _gameObject.AddComponent(new MeshRenderer(mesh, material));
        _world.AddEntity(_gameObject);
    }

    protected override void OnRender(double obj)
    {
        Graphics.ClearColor(ClearBufferMask.ColorBufferBit);

        _world.RenderWorld();
    }

    private Mesh CreateQuadMesh()
    {
        Mesh mesh = new Mesh();
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

        float[] colors =
        {
            1.0f, 1.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 1.0f,
            1.0f, 1.0f, 0.0f,
        };

        float[] textureCoords =
        {
            1, 1,
            1, 0,
            0, 0,
            0, 1,
        };


        mesh.SetVertices(vertices)
            .SetTriangles(indices)
            .SetColors(colors)
            .SetTextureCoords(textureCoords);

        return mesh;
    }
}