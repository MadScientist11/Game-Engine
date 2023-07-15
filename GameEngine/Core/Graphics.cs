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

    public unsafe void Render()
    {
        Gl.BindVertexArray(_currentVAO);
        Gl.UseProgram(_currentShaders);
        Gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, (void*) 0);
    }
    
    public unsafe void Render(Shader shader)
    {
        Gl.BindVertexArray(_currentVAO);
        Gl.UseProgram(shader.Id);
        Gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, (void*) 0);
    }
    
    public unsafe void Render(Mesh mesh, Shader shader)
    {
        SetMesh(mesh);
        Gl.BindVertexArray(_currentVAO);
        Gl.UseProgram(shader.Id);
        Gl.DrawElements(PrimitiveType.Triangles, (uint)mesh.Triangles.Length, DrawElementsType.UnsignedInt, (void*) 0);
    }


    public void SetMaterial(Material material)
    {
       
    }

    public unsafe void SetMesh(Mesh mesh)
    {
        _currentVAO = Gl.GenVertexArray();
        Gl.BindVertexArray(_currentVAO);

        _currentVBO = Gl.GenBuffer();
        Gl.BindBuffer(BufferTargetARB.ArrayBuffer, _currentVBO);

        fixed (float* buf = mesh.Vertices)
            Gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(mesh.Vertices.Length * sizeof(float)), buf,
                BufferUsageARB.StaticDraw);

        _currentEBO = Gl.GenBuffer();
        Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _currentEBO);

        fixed (uint* buf = mesh.Triangles)
            Gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(mesh.Triangles.Length * sizeof(uint)), buf,
                BufferUsageARB.StaticDraw);


        const uint positionLoc = 0;
        Gl.EnableVertexAttribArray(positionLoc);
        Gl.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);

        Gl.BindVertexArray(0);
        Gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
    }

  
}