using Godot;

public partial class BaseMapComponent : Node2D
{
    public VisibleOnScreenNotifier2D Notifier2D;
    public static float Speed = 300;
    
    public override void _Ready()
    {
        Notifier2D = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
        Notifier2D.ScreenExited += OnExitedFromScreen;
    }
    public override void _Process(double delta)
    {
        GlobalPosition += Vector2.Down * Speed * (float) delta;
    }
    private void OnExitedFromScreen()
    {
        GetNode<Playground>("../../").SpawnModularLevelComponent(Position);
        QueueFree();
    }
}
