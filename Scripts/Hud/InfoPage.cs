using Godot;
using System;

public partial class InfoPage : Panel
{
    Button BackButton;

    public override void _Ready()
    {
        GetNode<Label>("Version").Text = ProjectSettings.GetSetting("application/config/version").ToString();

        BackButton = GetNode<Button>("BackButton");
        BackButton.Pressed += Hide;
    }
}
