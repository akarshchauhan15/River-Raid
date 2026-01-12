using Godot;

public partial class Playground : Node2D
{
    Node2D LevelContainer;

    public override void _Ready()
    {
        LevelContainer = GetNode<Node2D>("Levels");
    }
    public void SpawnModularLevelComponent(Vector2 SacrificedPosition)
    {
        GD.Print((int)BaseMapDefaults.ModularLevelNamesEnum.Centre);
        Node2D MapComponent = BaseMapDefaults.ModularLevelScenes[(int)BaseMapDefaults.ModularLevelNamesEnum.Centre].Instantiate<Node2D>();
        
        MapComponent.Position = new Vector2(0, SacrificedPosition.Y - 720 * 3);
        LevelContainer.AddChild(MapComponent);
    }

}