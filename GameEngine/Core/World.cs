namespace GameEngine.Core;

public class World
{
    public HashSet<GameObject> Entities { get; } = new();
    
    private Camera? _camera;
    private readonly Graphics _graphics;
    
    private readonly Queue<MeshRenderer> _renderQueue = new();

    public World(Graphics graphics)
    {
        _graphics = graphics;
    }

    public void AddEntity(GameObject gameObject)
    {
        Entities.Add(gameObject);
    }

    public void RenderWorld()
    {
        foreach (GameObject entity in Entities)
        {
            if (entity.HasComponent(out Camera camera))
            {
                _camera = camera;
            }
            
            if(entity.HasComponent(out MeshRenderer meshRenderer))
            {
                _renderQueue.Enqueue(meshRenderer);
            }
        }

        if (_camera == null)
        {
            throw new Exception("No camera found");
        }
        
        foreach (MeshRenderer meshRenderer in _renderQueue)
        {
            _graphics.Render(meshRenderer.Mesh, meshRenderer.Material);
        }
    }
}

public class Camera : Component
{
}