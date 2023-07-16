using System.Numerics;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using static GameEngine.Core.Graphics;

namespace GameEngine.Core;

public class Shader
{
    public uint Id { get; private set; }
    public string VertexShader { get; init; }
    public string FragmentShader { get; init; }


    private Shader(string vertexShader, string fragmentShader)
    {
        VertexShader = vertexShader;
        FragmentShader = fragmentShader;
    }

    public static Shader Create(string vsPath, string fsPath)
    {
        Shader shader = new Shader(File.ReadAllText(vsPath), File.ReadAllText(fsPath));
        uint vertexShader = Gl.CreateShader(ShaderType.VertexShader);
        Gl.ShaderSource(vertexShader, shader.VertexShader);
        CompileShader(vertexShader);

        uint fragmentShader = Gl.CreateShader(ShaderType.FragmentShader);
        Gl.ShaderSource(fragmentShader, shader.FragmentShader);
        CompileShader(fragmentShader);

        shader.Id = Gl.CreateProgram();

        Gl.AttachShader(shader.Id, vertexShader);
        Gl.AttachShader(shader.Id, fragmentShader);

        Gl.LinkProgram(shader.Id);

        Gl.GetProgram(shader.Id, ProgramPropertyARB.LinkStatus, out int pStatus);
        if (pStatus != (int)GLEnum.True)
            throw new Exception("Program failed to link: " + Gl.GetProgramInfoLog(shader.Id));
        
        Gl.DetachShader(shader.Id, vertexShader);
        Gl.DetachShader(shader.Id, fragmentShader);
        Gl.DeleteShader(vertexShader);
        Gl.DeleteShader(fragmentShader);
        return shader;
    }

    public static void SetFloat(uint shaderId, string name, float value)
    {
        int location = Gl.GetUniformLocation(shaderId, name);
        Gl.Uniform1(location, value);
    }
    
    public static void SetInt(uint shaderId, string name, int value)
    {
        int location = Gl.GetUniformLocation(shaderId, name);
        Gl.Uniform1(location, value);
    }

    public static unsafe void SetMatrix(uint shaderId, string name, Matrix4x4 value, bool transpose = true)
    {
        int location = Gl.GetUniformLocation(shaderId, name);
        Gl.UniformMatrix4(location, 1, transpose, (float*) &value);
    }

    public void Use()
    {
        Gl.UseProgram(Id);
    }

    public static void CompileShader(uint shader)
    {
        Gl.CompileShader(shader);

        Gl.GetShader(shader, ShaderParameterName.CompileStatus, out int status);
        if (status != (int)GLEnum.True)
            throw new Exception($"Shader {shader} failed to compile: {Gl.GetShaderInfoLog(shader)}");
    }
}