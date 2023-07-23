namespace GameEngine.Core;

public class MeshRenderer : Component
{
    public Mesh Mesh { get; }
    public DefaultMaterial DefaultMaterial { get; }

    public MeshRenderer(Mesh mesh, DefaultMaterial defaultMaterial)
    {
        Mesh = mesh;
        DefaultMaterial = defaultMaterial;
    }
    

    
}