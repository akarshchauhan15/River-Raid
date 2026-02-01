using System;
using Godot;

public partial class Player : CharacterBody2D
{
    Camera2D Camera;
    Timer CooldownTimer;
    Marker2D BulletSpawnLocation;

    public static float Fuel = 100f;

    Vector2 MaxVelocity = new(700, 500);
    float MinVelocityY = 250;
    Vector2 Acceleration = new(2500, 400);
    Vector2 Friction = new(1600, 2400);

    public override void _Ready()
    {
        Camera = GetNode<Camera2D>("%Camera");
        CooldownTimer = GetNode<Timer>("CooldownTimer");
        BulletSpawnLocation = GetNode<Marker2D>("BulletSpawnLocation");
    }
    public override void _Process(double delta)
    {
        CheckMovement(delta);
        AlignCamera();
        UpdateStats(delta);
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("Shoot") && CooldownTimer.TimeLeft == 0)
            Shoot();
    }
    public void OnHit()
    {

    }
    private void CheckMovement(double delta)
    {
        Vector2 Direction;

        Direction = Input.GetVector("Left", "Right", "Up", "Down");
        
        float HorizontalVelocityModifier = (Playground.SliderSpeed + 700) / 1200;

        if (Direction.X != 0)
            Velocity = Velocity.MoveToward(new Vector2(Direction.X  * MaxVelocity.X * HorizontalVelocityModifier, Velocity.Y),Acceleration.X *  (float) delta);
        else
            Velocity = Velocity.MoveToward(new Vector2(0, Velocity.Y), Friction.X * (float) delta);

        if (Direction.Y < 0)
            Playground.SliderSpeed = Mathf.MoveToward(Playground.SliderSpeed, MaxVelocity.Y, Mathf.Abs(Direction.Y) * Acceleration.Y * (float)delta);
        else if (Direction.Y > 0)
            Playground.SliderSpeed = Mathf.MoveToward(Playground.SliderSpeed, MinVelocityY, Mathf.Abs(Direction.Y) * Acceleration.Y * (float)delta);

        MoveAndSlide();
    }
    private void AlignCamera()
    {
        float NewPositionX = 640 + (GlobalPosition.X - 640) * 0.1f;
        Camera.GlobalPosition = new(NewPositionX, 360 - GlobalPosition.Y / 10);
    }
    private void Shoot()
    {
        CooldownTimer.Start();

        Bullet NewBullet = ResourceBag.BulletScene.Instantiate<Bullet>();
        NewBullet.GlobalPosition = BulletSpawnLocation.GlobalPosition;
        NewBullet.Direction = Vector2.Up;
        NewBullet.Speed += Mathf.Abs(Velocity.Y);
        NewBullet.SetCollisionMaskValue(1, false);
        GetNode<Node2D>("%InGameSpawnedObjects/Projectiles").AddChild(NewBullet);
    }
    private void UpdateStats(double delta)
    {
        float HorizontalVelocityModifier = (Playground.SliderSpeed + 700) / 1200;
        Fuel -= 2 * HorizontalVelocityModifier * (float) delta;
    }
}