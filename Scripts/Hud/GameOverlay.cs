using Godot;

public partial class GameOverlay : Control
{
    Player Player;

    ProgressBar FuelMeter;
    ProgressBar HealthMeter;
    ProgressBar CooldownMeter;
    Label ScoreLabel;

    public override void _Ready()
    {
        Player = GetTree().Root.GetNode<Player>("Main/Playground/Player");

        FuelMeter = GetNode<ProgressBar>("FuelMeter");
        HealthMeter = GetNode<ProgressBar>("HealthMeter");
        CooldownMeter = GetNode<ProgressBar>("CooldownMeter");

        ScoreLabel = GetNode<Label>("ScoreLabel");

        Player.ScoreChanged += UpdateScore;
        Player.ShotsFired += StartCooldown;
    }
    public override void _Process(double delta)
    {
        FuelMeter.Value = Player.Fuel;
    }
    public void UpdateScore()
    {
        ScoreLabel.Text = Player.Score.ToString();
    }
    public void StartCooldown()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(CooldownMeter, ProgressBar.PropertyName.Value.ToString(), 100, Player.CooldownTimer.WaitTime).From(0).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Quad);
    }
}