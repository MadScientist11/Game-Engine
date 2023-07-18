namespace GameEngine.Core;

public abstract class Component
{
    public GameObject GameObject { get; set; }


   

    public void AddComponent<T>(T component) where T : Component
    {
        GameObject.Components.Add(component);
    }

    public bool HasComponent<T>(out T component) where T : Component
    {
        component = (T)GameObject.Components.Find(c => c is T)!;
        return component != null;
    }
}