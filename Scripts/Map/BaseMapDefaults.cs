using Godot;
using Godot.Collections;

public partial class BaseMapDefaults : Node
{
    public static readonly string[] ModularLevelNames = [
        "Centre",
        "CentreToRight",
        "CentreToLeft",
        "CentreIsland"
    ];
    public static readonly Array<PackedScene> ModularLevelScenes = [];

    public enum ModularLevelNamesEnum
    {
        Centre,
        CentreToRight,
        CentreToLeft,
        CentreIsland
    }
    public override void _Ready()
    {
        foreach (string Address in ModularLevelNames)
        {
            ModularLevelScenes.Add(ResourceLoader.Load<PackedScene>($"res://Scenes/Map/{Address}.tscn"));
        }
    }
}