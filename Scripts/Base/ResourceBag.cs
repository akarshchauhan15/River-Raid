using Godot;
using System;

public partial class ResourceBag : Node
{
    public static PackedScene BulletScene;

    public override void _Ready()
    {
        BulletScene = ResourceLoader.Load<PackedScene>("res://Scenes/Misc/Bullet.tscn");
    }
}
