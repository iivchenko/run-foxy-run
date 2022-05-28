using Godot;

public class Collectible : Area2D
{
    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnPlayerCollied));
    }

    private void OnPlayerCollied(Node _)
    {
        QueueFree();
    }
}
