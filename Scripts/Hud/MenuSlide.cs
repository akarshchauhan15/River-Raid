using Godot;
using System;

public partial class MenuSlide : Slide
{
    TextureButton FullscreenButton;
    TextureButton PauseButton;

    public override void _Ready()
    {
        base._Ready();

        FullscreenButton = GetNode<TextureButton>("ColorRect/FullscreenButton");
        PauseButton = GetNode<TextureButton>("ColorRect/PauseButton");

        FullscreenButton.Toggled += OnFullscreenButtonToggled;
        PauseButton.Toggled += OnPauseButtonToggled;
    }
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Pause"))
            OnPauseButtonToggled(true);
    }
    private void OnFullscreenButtonToggled(bool Fullscreen)
    {
        DisplayServer.WindowSetMode(Fullscreen ? DisplayServer.WindowMode.Fullscreen : DisplayServer.WindowMode.Windowed);
    }
    private void OnPauseButtonToggled(bool Paused)
    {
        Control P = GetNode<PauseMenu>("../PauseMenu");
        GetTree().Paused = !GetTree().Paused;
        P.Visible = !P.Visible;
    }
}
