using Godot;
using System;

public partial class Jet : Enemies
{
    float Speed = 400;
    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Down * Speed * (float)delta;
    }
}
