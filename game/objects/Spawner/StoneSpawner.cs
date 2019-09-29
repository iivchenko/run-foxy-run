using Godot;
using System;

public class StoneSpawner : Node2D
{
    private const int Block = 16; // 16 pixels

    [Export]
    public PackedScene Node { get; set; }

    [Export]
    public string TargetScene { get; set; }

    private Node _target;
    private Random _random;

    public StoneSpawner()
    {
        _random = new Random();
    }

    public override void _Ready()
    {
        _target = GetNode(new NodePath(TargetScene));
    }

    public override void _PhysicsProcess(float delta)
    {
        var foxy = GetNode(new NodePath("/root/World/Foxy")) as Node2D;

        if ((Position.x - foxy.Position.x) < 50 * Block)
        {
            this.MoveLocalX(16);
        }
    }

    public override void _Process(float delta)
    {
        var foxy = GetNode(new NodePath("/root/World/Foxy")) as Node2D;

        //if ((Position.x - foxy.Position.x) < 5 * Block && _random.Next(2) == 1)
        if (_random.Next(100) == 1)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        var node = Node.Instance() as Node2D;
        node.SetPosition(Position);

        _target.AddChild(node);
    }
}
