using System.Drawing;
using System.Numerics;
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
    public void Enable(EnableCap option) => Gl.Enable(option);


    public unsafe void Render(Transform transform, Mesh mesh, DefaultMaterial defaultMaterial)
    {
        if (defaultMaterial.Albedo != null)
            Texture2D.Bind(defaultMaterial.Albedo.Id, 0);
        mesh.Build();


        Gl.BindVertexArray(mesh.Id);
        Gl.UseProgram(defaultMaterial.ShaderId);

        defaultMaterial.SetMatrix4x4("TRS", transform.TRS);


        if (mesh.Triangles is null)
        {
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 36);

        }
        else
        {
            uint count = (uint)mesh.Triangles.Length;

            Gl.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, (void*)0);

        }
    }

}