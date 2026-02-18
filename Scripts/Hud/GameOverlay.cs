using Godot;

public partial class GameOverlay : Control
{
    Player Player;

    ProgressBar FuelMeter;
    Label ScoreLabel;

    public override void _Ready()
    {
        Player = GetTree().Root.GetNode<Player>("Main/Playground/Player");
        FuelMeter = GetNode<ProgressBar>("FuelMeter");
        ScoreLabel = GetNode<Label>("ScoreLabel");

        Player.ScoreChanged += UpdateScore;
    }
    public override void _Process(double delta)
    {
        FuelMeter.Value = Player.Fuel;
    }
    public void UpdateScore()
    {
        ScoreLabel.Text = Player.Score.ToString();
    }
}