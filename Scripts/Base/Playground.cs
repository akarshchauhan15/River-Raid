using Godot;

public partial class Playground : Node2D
{
    Node2D Slider;
    Node2D LevelContainer;
    Node2D EnemyContainer;
    PackedScene NextMapPackedComponent;

    public static float SliderSpeed = 300.0f;

    public override void _Ready()
    {
        Slider = GetNode<Node2D>("InGameSpawnedObjects");
        LevelContainer = GetNode<Node2D>("InGameSpawnedObjects/LevelContainer");
        EnemyContainer = GetNode<Node2D>("InGameSpawnedObjects/Enemies");

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

        Enemies EnemyShip = ResourceBag.EnemyShipScene.Instantiate<Enemies>();
        Vector2 RefPosition = (Vector2)MapComponent.GetNode<Node2D>("SpawnPositions/Ship").GetChild(0).Get(Node2D.PropertyName.GlobalPosition);
        
        EnemyContainer.AddChild(EnemyShip);
        EnemyShip.GlobalPosition = RefPosition;

        BaseMapDefaults.ModularLevelNamesEnum NextMapEnum = MapComponent.NextModularLevels.PickRandom();
        NextMapPackedComponent =  BaseMapDefaults.ModularLevelScenes[(int)NextMapEnum];
    }
}