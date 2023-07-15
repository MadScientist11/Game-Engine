using System.Drawing;
using GameEngine.Core;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Game;

public class GameWindow : GameWindowBase
{
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
    
        Mesh mesh = new Mesh();
        float[] vertices =
        {
            0.5f, 0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f, 0.5f, 0.0f
        };
        mesh.SetVertices(vertices);

        uint[] indices =
        {
            0u, 1u, 3u,
            1u, 2u, 3u
        };

        mesh.SetTriangles(indices);

        Graphics.SetMesh(mesh);
    }

    protected override void OnRender(double obj)
    {
        Graphics.ClearColor(ClearBufferMask.ColorBufferBit);
        base.OnRender(obj);
    }
}