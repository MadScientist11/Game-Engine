using System.Numerics;
using Silk.NET.Maths;

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

            if (entity.HasComponent(out MeshRenderer meshRenderer))
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
            _camera.HasComponent(out Transform camTransform);

            if (meshRenderer.HasComponent(out Transform transform))
            {
                _camera.GenerateViewMatrix();
                meshRenderer.DefaultMaterial.SetMatrix4x4("view", _camera.ViewMatrix);
                meshRenderer.DefaultMaterial.SetMatrix4x4("projection", _camera.Projection);
                meshRenderer.DefaultMaterial.SetMatrix4x4("model", transform.TRS);

                _graphics.Render(transform, meshRenderer.Mesh, meshRenderer.DefaultMaterial);
            }
        }
    }
}

public class Camera : Component
{
    public Matrix4x4 Projection { get; private set; }

    private Vector2D<int> _size;
    private readonly float _near;
    private readonly float _far;
    public Matrix4x4 ViewMatrix { get; set; }

    public Vector2D<int> Size
    {
        get => _size;
        set
        {
            if (_size != value)
            {
                _size = value;
                Projection = GenerateProjectionMatrix();
            }
        }
    }


    public float FOV { get; set; }

    public Camera()
    {
        FOV = MathF.PI / 4;
        _near = 0.1f;
        _far = 1000f;
    }

    public void GenerateViewMatrix()
    {
        HasComponent(out Transform transform);

        Vector3 Forward = Vector3.Transform(Vector3.UnitZ, transform.Rotation);
        Vector3 Up = Vector3.Transform(Vector3.UnitY, transform.Rotation);
        Vector3 Right = Vector3.Transform(Vector3.UnitX, transform.Rotation);
        
        
        ViewMatrix = Matrix4x4.CreateLookAt(transform.Position, transform.Position + Forward, Up);
    }


    private Matrix4x4 GenerateProjectionMatrix()
    {
        return Matrix4x4.CreatePerspectiveFieldOfView(FOV, _size.X / (float)_size.Y, _near, _far);
    }
}