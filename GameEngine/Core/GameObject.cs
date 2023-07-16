namespace GameEngine.Core;

public class GameObject
{
    public Mesh Mesh { get; }
    public Material Material { get; }

    public GameObject(Mesh mesh, Material material)
    {
        Mesh = mesh;
        Material = material;
    }
}