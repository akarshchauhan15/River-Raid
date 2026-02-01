using Godot;

public partial class GameOverlay : Control
{
    Player Player;

    ProgressBar FuelMeter;

    public override void _Ready()
    {
        Player = GetTree().Root.GetNode<Player>("Main/Playground/Player");
        FuelMeter = GetNode<ProgressBar>("FuelMeter");
    }
    public override void _Process(double delta)
    {
        FuelMeter.Value = Player.Fuel;
    }
}