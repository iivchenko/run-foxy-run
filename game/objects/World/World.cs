using Godot;

public class World : Node2D
{
    public void ScorePlayer(int scores)
    {
        GD.Print($"Score {scores}!");
    }
}
