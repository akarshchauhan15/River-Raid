using Godot;

public partial class Playground : Node2D
{
    Node2D LevelContainer;
    PackedScene NextMapPackedComponent;

    public override void _Ready()
    {
        LevelContainer = GetNode<Node2D>("Levels");
        NextMapPackedComponent = BaseMapDefaults.ModularLevelScenes[0];
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