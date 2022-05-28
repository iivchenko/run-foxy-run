using Godot;
using System;

public class MainMenu : Node2D
{
    private Button _start;
    private Button _exit;

    public override void _Ready()
    {
        _start = GetNode<Button>("Ui/StartButton");
        _exit = GetNode<Button>("Ui/ExitButton");

        _start.Connect("pressed", this, nameof(StartButtonPressed));
        _exit.Connect("pressed", this, nameof(ExitButtonPressed));
    }

    private void StartButtonPressed()
    {
        GetTree().ChangeScene("res://levels/level01.tscn");
    }
    
    private void ExitButtonPressed()
    {
        GetTree().Quit();
    }
}
