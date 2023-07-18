using System.Drawing;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace GameEngine.Core;

public class Graphics
{
    private readonly IWindow _window;
    public static GL Gl;
    private uint _currentShaders;
    private uint _currentVAO;
    private uint _currentVBO;
    private uint _currentEBO;

    public Graphics(IWindow window)
    {
        _window = window;
    }

    public void Initialize()
    {
        Gl = _window.CreateOpenGL();
    }

    public void SetClearColor(Color color) => Gl.ClearColor(color);

    public void ClearColor(ClearBufferMask mask) => Gl.Clear(mask);


    public unsafe void Render(Mesh mesh, Material material)
    {
        if (material.Albedo != null)
            Texture2D.Bind(material.Albedo.Id, 0);
        mesh.Build();

        Gl.BindVertexArray(mesh.Id);
        Gl.UseProgram(material.ShaderId);

        uint count = (uint)mesh.Triangles.Length;
        Gl.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, (void*)0);
    }
}