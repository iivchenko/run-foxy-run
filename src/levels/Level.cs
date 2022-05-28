using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Level : Node2D
{
    private float _currentTime;

    private Label _timePassedLabel; // TODO: for debug only, so remove it from release version
    private Label _scoreLabel;
    
    private int _score;

    public override void _Ready()
    {
        _currentTime = 0;
        _score = 0;

        _timePassedLabel = GetNode<Label>("Ui/TimePassedLabel");
        _scoreLabel = GetNode<Label>("Ui/ScoreLabel");

        foreach(Node child in GetNode<Node2D>("Collectibles").GetChildren())
        {
            var area = child as Area2D;

            if (area != null)
            {
                area.Connect("body_entered", this, nameof(OnPlayerCollects));
            }
        }
    }

    public override void _Process(float delta)
    {
        _currentTime += delta;

        _timePassedLabel.Text = $"{TimeSpan.FromSeconds(_currentTime)}";
    }


    public void OnPlayerCollects(Node player)
    {
        _score++;
        _scoreLabel.Text = $"Collected {_score} items!";
    }
}
