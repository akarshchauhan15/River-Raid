using Godot;
using System;
[Tool]

public partial class Enemies : AnimatableBody2D
{
    Marker2D BulletSpawnLocation;
    Timer CooldownTimer;
    Player Player;

    [Export]
    float DetectionRadius = 200.0f;
    [Export]
    float ShootingTimePeriod = 2.0f;

    public override void _Ready()
    {
        BulletSpawnLocation = GetNode<Marker2D>("BulletSpawnLocation");
        CooldownTimer = GetNode<Timer>("CooldownTimer");
        Player =  GetNode<Player>("%Player");

        CooldownTimer.WaitTime = ShootingTimePeriod;

        CircleShape2D Circle = new CircleShape2D();
        Circle.Radius = DetectionRadius;
        GetNode<CollisionShape2D>("PlayerDetectionArea/CollisionShape2D").Shape = Circle;

        GetNode<Area2D>("PlayerDetectionArea").BodyEntered += (Node2D Body) => {CooldownTimer.Start(); };
        GetNode<Area2D>("PlayerDetectionArea").BodyExited += (Node2D Body) => {CooldownTimer.Stop(); };
        CooldownTimer.Timeout += Shoot;
    }
    public void OnHit()
    {
        QueueFree();
    }
    private void Shoot()
    {
        Bullet NewBullet = ResourceBag.BulletScene.Instantiate<Bullet>();
        NewBullet.GlobalPosition = BulletSpawnLocation.GlobalPosition;
        NewBullet.Direction = GlobalPosition.DirectionTo(Player.GlobalPosition);
        GetNode<Node>("%InGameSpawnedObjects/Bullets").AddChild(NewBullet);
    }
}