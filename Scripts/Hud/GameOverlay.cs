using Godot;

public partial class GameOverlay : Control
{
    Player Player;

    ProgressBar FuelMeter;
    ProgressBar HealthMeter;
    ProgressBar CooldownMeter;
    VBoxContainer EventContainer;
    Label ScoreLabel;

    public override void _Ready()
    {
        Player = GetTree().Root.GetNode<Player>("Main/Playground/Player");

        FuelMeter = GetNode<ProgressBar>("FuelMeter");
        HealthMeter = GetNode<ProgressBar>("HealthMeter");
        CooldownMeter = GetNode<ProgressBar>("CooldownMeter");
        EventContainer = GetNode<VBoxContainer>("EventContainer");

        ScoreLabel = GetNode<Label>("ScoreLabel");

        Player.ScoreChanged += () => ScoreLabel.Text = Player.Score.ToString();
        Player.HealthChanged += () => HealthMeter.Value = Player.Health;
        Player.ShotsFired += StartCooldown;
        Player.Pickuped += AddEventHappened;
    }
    public override void _Process(double delta)
    {
        FuelMeter.Value = Player.Fuel;
    }
    private void StartCooldown()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(CooldownMeter, ProgressBar.PropertyName.Value.ToString(), 100, Player.CooldownTimer.WaitTime).From(0).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Quad);
    }
    private void AddEventHappened(int Type)
    {
        InfoLabel Label = ResourceBag.InfoLabelScene.Instantiate<InfoLabel>();

        Label.Text = $"Picked up {(Pickable.PickableType)Type}";
        EventContainer.AddChild(Label);
    }
}