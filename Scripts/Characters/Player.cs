using System;
using Godot;
using Godot.NativeInterop;

public partial class Player : CharacterBody2D
{
    Camera2D Camera;

    Vector2 MaxVelocity = new(700, 500);
    float MinVelocityY = 250;
    Vector2 Acceleration = new(2500, 400);
    Vector2 Friction = new(1600, 2400);

    public override void _Ready()
    {
        Camera = GetNode<Camera2D>("../Camera");

        Velocity = new(0, 0);
    }
    public override void _Process(double delta)
    {
        CheckMovement(delta);
        AlignCamera();
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

        float HorizontalVelocityModifier = (BaseMapComponent.Speed + 300) / 800;

        if (Direction.X != 0)
            Velocity = Velocity.MoveToward(new Vector2(Direction.X  * MaxVelocity.X * HorizontalVelocityModifier, Velocity.Y),Acceleration.X *  (float) delta);
        else
            Velocity = Velocity.MoveToward(new Vector2(0, Velocity.Y), Friction.X * (float) delta);

        if (Direction.Y < 0)
            BaseMapComponent.Speed = Mathf.MoveToward(BaseMapComponent.Speed, MaxVelocity.Y, Mathf.Abs(Direction.Y) * Acceleration.Y * (float) delta);
        else if (Direction.Y > 0)
            BaseMapComponent.Speed = Mathf.MoveToward(BaseMapComponent.Speed, MinVelocityY, Mathf.Abs(Direction.Y) * Acceleration.Y * (float) delta);

        MoveAndSlide();
    }
    private void AlignCamera()
    {
        float NewPositionX = 640 + (GlobalPosition.X - 640) * 0.1f;
        Camera.GlobalPosition = new(NewPositionX, 360 - BaseMapComponent.Speed / 10);
    }
    private void Shoot()
    {
    }
}