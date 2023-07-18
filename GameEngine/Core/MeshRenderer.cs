namespace GameEngine.Core;

public class MeshRenderer : Component
{
    public Mesh Mesh { get; }
    public Material Material { get; }

    public MeshRenderer(Mesh mesh, Material material)
    {
        Mesh = mesh;
        Material = material;
    }
}