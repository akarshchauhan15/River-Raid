using Godot;
using System;

public partial class Enemies : Area2D
{
    Marker2D BulletSpawnLocation;
    Timer CooldownTimer;
    Player Player;

    [Export]
    float DetectionRadius = 200.0f;
    [Export]
    float ShootingTimePeriod = 2.0f;
    [Export]
    bool Flying = false;

    public override void _Ready()
    {
        BulletSpawnLocation = GetNode<Marker2D>("BulletSpawnLocation");
        CooldownTimer = GetNode<Timer>("CooldownTimer");
        Player = GetTree().Root.GetNode<Player>("Main/Playground/Player");

        BodyEntered += OnCollisionWithPlayer;
        GetNode<Area2D>("PlayerDetectionArea").BodyEntered += (Node2D Body) => { CooldownTimer.Start(); };
        GetNode<Area2D>("PlayerDetectionArea").BodyExited += (Node2D Body) => { CooldownTimer.Stop(); };
        CooldownTimer.Timeout += Shoot;
        GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D").ScreenExited += QueueFree;

        CooldownTimer.WaitTime = ShootingTimePeriod;

        CircleShape2D Circle = new CircleShape2D();
        Circle.Radius = DetectionRadius;
        GetNode<CollisionShape2D>("PlayerDetectionArea/CollisionShape2D").Shape = Circle;

        //SetCollisionMaskValue(1, Flying);
    }
    public void OnHit()
    {
        QueueFree();
    }
    private void OnCollisionWithPlayer(Node2D Body)
    {
        if (!(Body is Player)) return;
        Player.OnHit();
        OnHit();
    }
    private void Shoot()
    {
        Bullet NewBullet = ResourceBag.BulletScene.Instantiate<Bullet>();
        NewBullet.Direction = GlobalPosition.DirectionTo(Player.GlobalPosition);
        NewBullet.SetCollisionMaskValue(2, false);
        GetNode<Node>("../../../Projectiles").AddChild(NewBullet);
        NewBullet.GlobalPosition = BulletSpawnLocation.GlobalPosition;
    }
}