namespace GameEngine.Core;

public class Material
{
    public string VertexShader { get; init; }
    public string FragmentShader { get; init; }

    public Material(string vertexShader, string fragmentShader)
    {
        VertexShader = vertexShader;
        FragmentShader = fragmentShader;
    }

    public static Material Default()
    {
        string vs = File.ReadAllText(@"D:\UnityProjects\GameEngine\GameEngine\Shaders\Default.vert");
        string fs = File.ReadAllText(@"D:\UnityProjects\GameEngine\GameEngine\Shaders\Default.frag");
        return new Material(vs, fs);
    }
}