using System.Numerics;

namespace GameEngine.Core;

public class GameObject
{
    private List<Component> _components;

    public GameObject()
    {
        _components.Add(new Transform
        {
            Position = Vector3.One,
            Scale = Vector3.One,
            Rotation = Quaternion.Identity,
        });
    }

    public void AddComponent<T>(T component) where T : Component
    {
        _components.Add(component);
    }

    public bool HasComponent<T>(out T component) where T : Component
    {
        component = (T)_components.Find(c => c is T)!;
        return component != null;
    }
}

public class Transform : Component
{
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }
    public Quaternion Rotation { get; set; }
}

public abstract class Component
{
}