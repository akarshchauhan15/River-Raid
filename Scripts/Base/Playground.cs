using Godot;
using Godot.Collections;

public partial class Playground : Node2D
{
    readonly string[] ModularLevelNames = ["Basic1"];
    Array<PackedScene> ModularLevelScenes = [];

    enum ModularLevelNamesEnum
    {
        Basic1
    }

    Node2D LevelContainer;

    public override void _Ready()
    {
        LevelContainer = GetNode<Node2D>("Levels");
        LoadLevels();
    }
    public void SpawnModularLevelComponent(Vector2 SacrificedPosition)
    {
        Node2D MapComponent = ModularLevelScenes[0].Instantiate<Node2D>();
        MapComponent.Position = new Vector2(0, SacrificedPosition.Y - 720 * 3);
        LevelContainer.AddChild(MapComponent);
    }
    private void LoadLevels()
    {
        foreach (string Address in ModularLevelNames){
            ModularLevelScenes.Add(ResourceLoader.Load<PackedScene>($"res://Scenes/Map/{Address}.tscn"));
        }
    }
}