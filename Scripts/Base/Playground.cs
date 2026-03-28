using System;
using Godot;

public partial class Playground : Node2D
{
    Node2D Slider;
    Node2D LevelContainer;
    Node2D EnemyContainer;
    PackedScene NextMapPackedComponent;

    Random Random = new();
    public static float SliderSpeed = 300.0f;

    public override void _Ready()
    {
        Slider = GetNode<Node2D>("InGameSpawnedObjects");
        LevelContainer = GetNode<Node2D>("InGameSpawnedObjects/LevelContainer");
        EnemyContainer = GetNode<Node2D>("InGameSpawnedObjects/Enemies");

        GetNode<Timer>("Timers/JetSpawnTimer").Timeout += SpawnEnemyJets;

        NextMapPackedComponent = BaseMapDefaults.ModularLevelScenes[0];
    }
    public override void _Process(double delta)
    {
        Slider.GlobalPosition += Vector2.Down * SliderSpeed * (float) delta;
    }
    public void SpawnModularLevelComponent(Vector2 SacrificedPosition)
    {
        BaseMapComponent MapComponent = NextMapPackedComponent.Instantiate<BaseMapComponent>();
        
        MapComponent.Position = new Vector2(0, SacrificedPosition.Y - 720 * 3);
        LevelContainer.AddChild(MapComponent);

        Enemies EnemyShip = ResourceBag.EnemyScenes["Ship"].Instantiate<Enemies>();
        Vector2 RefPosition = (Vector2)MapComponent.GetNode<Node2D>("SpawnPositions/Ship").GetChild(0).Get(Node2D.PropertyName.GlobalPosition);

        int RandomInt = Random.Next(0,10);
        if (RandomInt < 3)
        EnemyShip.SpawnPickableOnFree = (Pickable.PickableType) RandomInt; 
        
        EnemyContainer.AddChild(EnemyShip);
        EnemyShip.GlobalPosition = RefPosition;

        BaseMapDefaults.ModularLevelNamesEnum NextMapEnum = MapComponent.NextModularLevels.PickRandom();
        NextMapPackedComponent =  BaseMapDefaults.ModularLevelScenes[(int)NextMapEnum];
    }
    private void SpawnEnemyJets()
    {
        int PositionX = 320 * Random.Next(1, 4) - 160;

        for (int i=2; i>0; i--)
        {
            Enemies EnemyJet = ResourceBag.EnemyScenes["Jet"].Instantiate<Enemies>();
            EnemyContainer.AddChild(EnemyJet);
            EnemyJet.GlobalPosition = new Vector2(PositionX, -20);
            PositionX += 320;
        }
    }
}