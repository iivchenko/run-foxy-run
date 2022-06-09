using Godot;
using System;

public class MainMenu : Node2D
{
    private CanvasLayer _ui;

    public override void _Ready()
    {
        _ui = GetNode<CanvasLayer>("Ui");

        LoadMainMenuDialog();
    }

    private void MainMenuDialogButtonPressed(MainMenuDialogButton button)
    {
        switch(button)
        {
            case MainMenuDialogButton.Start:

                RemoveCurrentDialog();

                var dialog = GD.Load<PackedScene>("res://scenes/main_menu/level_selector_dialog/level_selector_dialog.tscn").Instance() as LevelSelectorDialog;
                dialog.Connect(nameof(LevelSelectorDialog.BackButtonPressed), this, nameof(OnSubDialogBackButtonPressed));

                _ui.AddChild(dialog);
                break;

            case MainMenuDialogButton.Settings:

                RemoveCurrentDialog();

                var settignsDialog = GD.Load<PackedScene>("res://ui/settings_dialog/settings_dialog.tscn").Instance() as SettingsDialog;
                settignsDialog.Connect(nameof(SettingsDialog.BackButtonPressed), this, nameof(OnSubDialogBackButtonPressed));

                _ui.AddChild(settignsDialog);
                break;

            case MainMenuDialogButton.Exit:
                GetTree().Quit();
                break;
        }
    }

    public void OnSubDialogBackButtonPressed()
    {
        RemoveCurrentDialog();
        LoadMainMenuDialog();
    }

    private void RemoveCurrentDialog()
    {
        foreach(Node child in _ui.GetChildren())
        {
            child.QueueFree();
        }
    }

    private void LoadMainMenuDialog()
    {
        var dialog = GD.Load<PackedScene>("res://scenes/main_menu/main_menu_dialog/main_menu_dialog.tscn").Instance() as MainMenuDialog;
        _ui.AddChild(dialog);

        dialog.Connect("ButtonPressed", this, nameof(MainMenuDialogButtonPressed));
    }
}
