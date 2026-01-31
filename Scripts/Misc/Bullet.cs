using Godot;
using System;

public partial class Bullet : Area2D
{
    public float Speed = 400.0f;
    public Vector2 Direction = Vector2.Down;

    public override void _Ready()
    {
        GetNode<Timer>("Timer").Timeout += QueueFree;
        BodyEntered += OnCollision;
    }
    public override void _Process(double delta)
    {
        Position += Direction * Speed * (float) delta;
        Speed -= 100 * (float) delta;
    }
    private void OnCollision(Node2D Body)
    {
        QueueFree();
        if (Body.HasMethod("OnHit")) Body.Call("OnHit");
    }
}
