using Godot;

public class World : Node2D
{
    private int _scores;
    private Label _scoresLabel;

    public override void _Ready()
    {
        _scores = 0;
        _scoresLabel = GetNode<Label>(new NodePath("HUD/Scores"));
    }

    public void ScorePlayer(int scores)
    {
        _scores += scores;
        _scoresLabel.Text = $"Scores: {_scores}";
    }
}
