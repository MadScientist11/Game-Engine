using System.Drawing;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace GameEngine.Core;

public class Graphics
{
    private readonly IWindow _window;
    private GL _gl;
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
        _gl = _window.CreateOpenGL();
    }

    public void SetClearColor(Color color) => _gl.ClearColor(color);

    public void ClearColor(ClearBufferMask mask) => _gl.Clear(mask);

    public unsafe void Render()
    {
        _gl.BindVertexArray(_currentVAO);
        _gl.UseProgram(_currentShaders);
        _gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, (void*) 0);
    }

    public void SetMaterial(Material material)
    {
        uint vertexShader = _gl.CreateShader(ShaderType.VertexShader);
        _gl.ShaderSource(vertexShader, material.VertexShader);

        CompileShader(vertexShader);

        uint fragmentShader = _gl.CreateShader(ShaderType.FragmentShader);
        _gl.ShaderSource(fragmentShader, material.FragmentShader);

        CompileShader(fragmentShader);

        _currentShaders = _gl.CreateProgram();
        _gl.AttachShader(_currentShaders, vertexShader);
        _gl.AttachShader(_currentShaders, fragmentShader);

        _gl.LinkProgram(_currentShaders);

        _gl.GetProgram(_currentShaders, ProgramPropertyARB.LinkStatus, out int pStatus);
        if (pStatus != (int)GLEnum.True)
            throw new Exception("Program failed to link: " + _gl.GetProgramInfoLog(_currentShaders));

        _gl.DetachShader(_currentShaders, vertexShader);
        _gl.DetachShader(_currentShaders, fragmentShader);
        _gl.DeleteShader(vertexShader);
        _gl.DeleteShader(fragmentShader);
    }

    public unsafe void SetMesh(Mesh mesh)
    {
        _currentVAO = _gl.GenVertexArray();
        _gl.BindVertexArray(_currentVAO);

        _currentVBO = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _currentVBO);

        fixed (float* buf = mesh.Vertices)
            _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(mesh.Vertices.Length * sizeof(float)), buf,
                BufferUsageARB.StaticDraw);

        _currentEBO = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _currentEBO);

        fixed (uint* buf = mesh.Triangles)
            _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(mesh.Triangles.Length * sizeof(uint)), buf,
                BufferUsageARB.StaticDraw);


        const uint positionLoc = 0;
        _gl.EnableVertexAttribArray(positionLoc);
        _gl.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);

        _gl.BindVertexArray(0);
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
    }

    private void CompileShader(uint shader)
    {
        _gl.CompileShader(shader);

        _gl.GetShader(shader, ShaderParameterName.CompileStatus, out int status);
        if (status != (int)GLEnum.True)
            throw new Exception($"Shader {shader} failed to compile: {_gl.GetShaderInfoLog(shader)}");
    }
}