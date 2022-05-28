using Godot;

public class Collectible : Area2D
{
    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnPlayerCollied));
    }

    private void OnPlayerCollied(Node _)
    {
        var effect = GD.Load<PackedScene>("res://game_nodes/collectables/collected_effect/collected_effect.tscn").Instance() as Node2D;
        effect.Transform = Transform;

        GetParent().AddChild(effect);

        QueueFree();
    }
}
