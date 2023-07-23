using System.Diagnostics;
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
    private GameObject _object;
    private World _world;

    private Vector2D<int> Size = new Vector2D<int>(800, 600);
    private Transform _objectTransform;
    private Camera _cameraComp;
    private Transform _camTransform;

    protected override void OnCreateWindow(ref WindowOptions windowOptions)
    {
        base.OnCreateWindow(ref windowOptions);
        windowOptions.Size = Size;
        windowOptions.Title = "Game";
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        Graphics.SetClearColor(Color.CornflowerBlue);

        _world = new World(Graphics);

        Mesh mesh = CreateCubeMesh();

        _texture = new Texture2D(@"D:\UnityProjects\GameEngine\GameEngine\Textures\wood.png");
        _texture2 = new Texture2D(@"D:\UnityProjects\GameEngine\GameEngine\Textures\awesomeface.png");
        _shader = Shader.Create(@"D:\UnityProjects\GameEngine\GameEngine\Shaders\Default.vert",
            @"D:\UnityProjects\GameEngine\GameEngine\Shaders\Default.frag");

        DefaultMaterial defaultMaterial = new DefaultMaterial(_shader, _texture);

        _object = new GameObject();
        _object.AddComponent(new MeshRenderer(mesh, defaultMaterial));
        _object.HasComponent(out Transform transform);
        _objectTransform = transform;
        transform.Position = new Vector3(0, 0, 0);

        _world.AddEntity(_object);
        GameObject camera = new GameObject();
        _cameraComp = new Camera();
        _cameraComp.Size = Size;
        camera.AddComponent(_cameraComp);
        camera.HasComponent(out Transform camTransform);
        _camTransform = camTransform;
        _camTransform.Position = new Vector3(0, 0, -10);
        //camTransform.Rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.ToRadians(-45));
        _world.AddEntity(camera);
        
        Graphics.Enable(EnableCap.DepthTest);
    }

    private float angle = 0;

    protected override void OnRender(double deltaTime)
    {
        Graphics.ClearColor(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        angle += 20 * (float)deltaTime;
        _objectTransform.Rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.ToRadians(angle));
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

        _world.RenderWorld();
    }

    private Mesh CreateCubeMesh()
    {
        Mesh mesh = new Mesh();



        float[] vertices =
        {
            -0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,
            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f,  0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
            -0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f,  0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,
        };



        float[] colors =
        {
            0.0f, 0.0f, 0.0f,
            1.0f, 0.0f, 0.0f,
            1.0f, 1.0f, 0.0f,
            1.0f, 1.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f,
            1.0f, 0.0f, 1.0f,
            1.0f, 1.0f, 1.0f,
            1.0f, 1.0f, 1.0f,
            0.0f, 1.0f, 1.0f,
            0.0f, 0.0f, 1.0f,
            0.0f, 1.0f, 1.0f,
            0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f,
            0.0f, 1.0f, 1.0f,
            1.0f, 1.0f, 1.0f,
            1.0f, 0.0f, 1.0f,
            1.0f, 0.0f, 0.0f,
            1.0f, 0.0f, 0.0f,
            1.0f, 0.0f, 1.0f,
            1.0f, 1.0f, 1.0f,
            0.0f, 0.0f, 0.0f,
            1.0f, 0.0f, 0.0f,
            1.0f, 0.0f, 1.0f,
            1.0f, 0.0f, 1.0f,
            0.0f, 0.0f, 1.0f,
            0.0f, 1.0f, 0.0f,
            1.0f, 1.0f, 0.0f,
            1.0f, 1.0f, 0.0f,
        };

        float[] textureCoords =
        {
            0.0f, 0.0f,

            1.0f, 0.0f,
            1.0f, 1.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,
            0.0f, 0.0f,
            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,
            0.0f, 0.0f,
            0.0f, 1.0f,
            0.0f, 1.0f,
            0.0f, 0.0f,
            0.0f, 0.0f,
            0.0f, 0.0f,
            0.0f, 1.0f,
            1.0f, 1.0f,
            1.0f, 1.0f,
            1.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,
            1.0f, 1.0f,
            1.0f, 0.0f,
            1.0f, 0.0f,
            0.0f, 0.0f,
            0.0f, 1.0f,
            1.0f, 1.0f,
        };


        mesh.SetVertices(vertices)
            .SetColors(colors)
            .SetTextureCoords(textureCoords);

        return mesh;
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