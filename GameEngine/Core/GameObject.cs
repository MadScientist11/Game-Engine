using System.Numerics;

namespace GameEngine.Core;

public class GameObject
{
    private readonly List<Component> _components = new();

    public GameObject()
    {
        AddComponent(new Transform
        {
            Position = Vector3.One,
            Scale = Vector3.One,
            Rotation = Quaternion.Identity,
        });
    }

    public List<Component> Components => _components;

    public void AddComponent<T>(T component) where T : Component
    {
        component.GameObject = this;
        Components.Add(component);
    }

    public bool HasComponent<T>(out T component) where T : Component
    {
        component = (T)Components.Find(c => c is T)!;
        return component != null;
    }
}