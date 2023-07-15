using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace GameEngine.Core;

public abstract class GameWindowBase
{
    protected Graphics Graphics => _graphics;
    
    private IWindow _window;
    private Graphics _graphics;

    public void Initialize()
    {
        WindowOptions options = WindowOptions.Default;
        _window = Window.Create(options);

        OnCreateWindow(ref options);

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;
    }
    
    public void Run() => _window.Run();

    protected virtual void OnCreateWindow(ref WindowOptions windowOptions)
    {
       
    }

    protected virtual void OnLoad()
    {
        _graphics = new Graphics(_window);
        _graphics.Initialize();
    }

    protected virtual void OnUpdate(double deltaTime)
    {
    }

    protected virtual void OnRender(double deltaTime)
    {
        Graphics.Render();
    }
}