using Godot;
using System;

public partial class Player : CharacterBody2D
{
    float MaxVelocity = 700;
    float Acceleration = 2500;
    float Friction = 1600;

    public override void _Ready()
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        CheckMovement(delta);
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventAction Action){

            if (Action.IsAction("Shoot"))
            Shoot();
        }
    }

    private void CheckMovement(double delta)
    {
        Vector2 Direction;

        Direction = Input.GetVector("Left", "Right", "Up", "Down");

        if (Direction.X != 0)
        Velocity = Velocity.MoveToward(new Vector2(Direction.X, Velocity.Y) * MaxVelocity, Acceleration *  (float) delta);

        else
        Velocity = Velocity.MoveToward(new Vector2(0, Velocity.Y), Friction * (float) delta);

        MoveAndSlide();
    }
    private void Shoot()
    {
    }
}