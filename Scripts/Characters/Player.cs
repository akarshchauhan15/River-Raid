using System;
using Godot;

public partial class Player : CharacterBody2D
{
    float MaxVelocity = 700;
    float MinVelocity = 300;
    Vector2 Acceleration = new(2500, 500);
    Vector2 Friction = new(1600, 2500);

    public override void _Ready()
    {
        Velocity = new(0, 0);
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

        //Horizontal Velocity
        if (Direction.X != 0)
            Velocity = Velocity.MoveToward(new Vector2(Direction.X  * MaxVelocity, Velocity.Y), Acceleration.X *  (float) delta);
        else
            Velocity = Velocity.MoveToward(new Vector2(0, Velocity.Y), Friction.X * (float) delta);

        if (Direction.Y < 0)
            BaseMapComponent.Speed = Mathf.MoveToward(BaseMapComponent.Speed, MaxVelocity, Mathf.Abs(Direction.Y) * Acceleration.Y * (float) delta);
        else if (Direction.Y > 0)
            BaseMapComponent.Speed = Mathf.MoveToward(BaseMapComponent.Speed, MinVelocity, Mathf.Abs(Direction.Y) * Acceleration.Y * (float) delta);

        MoveAndSlide();
    }
    private void Shoot()
    {
    }
}