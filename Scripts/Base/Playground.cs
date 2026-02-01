using Godot;

public partial class Playground : Node2D
{
    Node2D LevelContainer;
    PackedScene NextMapPackedComponent;

    public static float SliderSpeed = 300.0f;

    public override void _Ready()
    {
        LevelContainer = GetNode<Node2D>("InGameSpawnedObjects/LevelContainer");

        NextMapPackedComponent = BaseMapDefaults.ModularLevelScenes[0];
    }
    public override void _Process(double delta)
    {
        LevelContainer.GlobalPosition += Vector2.Down * SliderSpeed * (float) delta;
    }
    public void SpawnModularLevelComponent(Vector2 SacrificedPosition)
    {
        BaseMapComponent MapComponent = NextMapPackedComponent.Instantiate<BaseMapComponent>();
        
        MapComponent.Position = new Vector2(0, SacrificedPosition.Y - 720 * 3);
        LevelContainer.AddChild(MapComponent);

        BaseMapDefaults.ModularLevelNamesEnum NextMapEnum = MapComponent.NextModularLevels.PickRandom();
        NextMapPackedComponent =  BaseMapDefaults.ModularLevelScenes[(int)NextMapEnum];
    }

}