using Godot;
using System;

public partial class PauseMenu : Panel
{
    Button ResumeButton;
    Button OptionsButton;
    Button MainMenuButton;

    public override void _Ready()
    {
        ResumeButton = GetNode<Button>("ButtonContainer/ResumeButton");
        OptionsButton = GetNode<Button>("ButtonContainer/OptionsButton");
        MainMenuButton = GetNode<Button>("ButtonContainer/HBoxContainer/MainMenuButton");

        ResumeButton.Pressed += ToggleGameState;
    }
    public void ToggleGameState()
    {
        GetTree().Paused = !GetTree().Paused;
        Visible = !Visible;
        GetNode<TextureButton>("../MenuSlide/ColorRect/PauseButton").SetPressedNoSignal(false);
    }
}