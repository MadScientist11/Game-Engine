using System.Numerics;

namespace GameEngine.Core;

public class Material
{
    public uint ShaderId { get; }
    public Texture2D Albedo { get; }

    public Material(Shader shader, Texture2D albedo)
    {
        ShaderId = shader.Id;
        Albedo = albedo;
    }

    public void SetFloat(string name, float value)
    {
        Shader.SetFloat(ShaderId, name, value);
    }
    
    public void SetInt(string name, int value)
    {
        Shader.SetInt(ShaderId, name, value);
    }
    
    public void SetMatrix4x4(string name, Matrix4x4 value)
    {
        Shader.SetMatrix(ShaderId, name, value);
    }
}