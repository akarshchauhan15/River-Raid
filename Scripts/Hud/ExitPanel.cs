using Godot;
using System;

public partial class ExitPanel : Panel
{
    Button YesButton;
    Button NoButton;

    public override void _Ready()
    {
        YesButton = GetNode<Button>("HBoxContainer/YesButton");
        NoButton = GetNode<Button>("HBoxContainer/NoButton");

        NoButton.Pressed += Hide;
        YesButton.Pressed += QueueFree;
    }
}
