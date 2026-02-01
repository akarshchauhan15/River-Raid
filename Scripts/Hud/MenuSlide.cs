using Godot;
using System;

public partial class MenuSlide : Slide
{
    TextureButton PauseButton;
    public override void _Ready()
    {
        base._Ready();

        PauseButton = GetNode<TextureButton>("ColorRect/PauseButton");

        PauseButton.Toggled += OnPauseButtonToggled;
    }
    private void OnPauseButtonToggled(bool Paused)
    {
        Control P = GetNode<PauseMenu>("../PauseMenu");
        GetTree().Paused = !GetTree().Paused;
        P.Visible = !P.Visible;
    }
}
