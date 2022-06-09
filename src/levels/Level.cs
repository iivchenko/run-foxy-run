using Godot;
using System;

public class Level : Node2D
{
    private float _currentTime;

    private Node2D _game;
    private CanvasLayer _dialogs;
    private Label _timePassedLabel; // TODO: for debug only, so remove it from release version
    private Label _scoreLabel;

    private Area2D _finishPoint;
    
    private int _score;

    public override void _Ready()
    {
        _currentTime = 0;
        _score = 0;

        _game = FindNode("Game") as Node2D;
        _dialogs = FindNode("Dialogs") as CanvasLayer;
        _timePassedLabel = FindNode("TimePassedLabel") as Label;
        _scoreLabel = FindNode("ScoreLabel") as Label;
        _finishPoint = FindNode("FinishPoint") as Area2D;

        foreach(Node child in GetNode<Node2D>("Game/Collectibles").GetChildren())
        {
            var area = child as Area2D;

            if (area != null)
            {
                area.Connect("body_entered", this, nameof(OnPlayerCollects));
            }
        }

        _finishPoint.Connect("body_entered", this, nameof(OnPlayerFinishLevel));
    }

    public override void _Process(float delta)
    {
        _currentTime += delta;

        _timePassedLabel.Text = $"{TimeSpan.FromSeconds(_currentTime)}";

        if (Input.IsActionPressed("ui_cancel"))
        {
            var pause = FindNode("PauseDialog");
            if(pause != null)
            {
                pause.QueueFree();
                GetTree().Paused = false;
            }
            else
            {
                var dialog = GD.Load<PackedScene>("res://levels/dialogs/pause_dialog/pause_dialog.tscn").Instance() as PauseDialog;
                dialog.Connect(nameof(PauseDialog.ButtonPressed), this, nameof(OnPause));

                _dialogs.AddChild(dialog);
                GetTree().Paused = true;
            }
        }
    }


    public void OnPlayerCollects(Node player)
    {
        _score++;
        _scoreLabel.Text = $"Collected {_score} items!";
    }

    public void OnPause(PauseDialogButtons button)
    {
        switch(button)
        {
            case PauseDialogButtons.Resume:
                FindNode("PauseDialog", owned: false).QueueFree();
                GetTree().Paused = false;                
                break;

            case PauseDialogButtons.Restart:
                GetTree().Paused = false;
                GetTree().ReloadCurrentScene();
                break;

            case PauseDialogButtons.Exit:
                GetTree().Paused = false;
                GetTree().ChangeScene("res://scenes/main_menu/main_menu.tscn");
                break;
        }
    }
    public void OnPlayerFinishLevel(Node player)
    {
        if (player is Player)
        {
            GetTree().Paused = true;

            var dialog = GD.Load<PackedScene>("res://levels/dialogs/game_victory_dialog/game_victory_dialog.tscn").Instance() as GameVictoryDialog;
            dialog.Connect(nameof(GameVictoryDialog.ButtonPressed), this, nameof(OnGameVictoryButtonPressed));

            _dialogs.AddChild(dialog);
        }
    }

    private void OnGameVictoryButtonPressed(GameVictoryDialogButtons button)
    {
        switch (button)
        {
            case GameVictoryDialogButtons.Restart:
                GetTree().ReloadCurrentScene();
                break;

            case GameVictoryDialogButtons.Exit:
                GetTree().ChangeScene("res://scenes/main_menu/main_menu.tscn");
                break;
        }

        GetTree().Paused = false;
    }
}
