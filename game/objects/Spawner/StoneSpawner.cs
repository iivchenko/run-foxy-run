using Godot;
using System;

namespace RunFoxyRun
{
    public class StoneSpawner : Node2D
    {
        private const int Block = 16; // 16 pixels

        [Export]
        public PackedScene Node { get; set; }

        [Export]
        public string TargetScene { get; set; }

        [Export]
        public bool Spawning { get; set; }

        private Node _target;
        private Random _random;
        private int _stonesPerFrame = 2;

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
                this.MoveLocalX(28);

                if (_random.Next(10) == 1)
                {
                    if(_stonesPerFrame > 0)
                    {
                        Spawn();
                        _stonesPerFrame--;

                        if (_stonesPerFrame == 0)
                            _stonesPerFrame = -10;
                    }
                }
                else if (_stonesPerFrame < 2)
                {
                    _stonesPerFrame++;
                }
            }
        }

        public void Spawn()
        {
            if (Spawning)
            {
                var node = Node.Instance() as Obstacle;
                node.SetPosition(Position);

                node.Connect("OverCrossed", _target, "ScorePlayer");
                node.Connect("Collided", _target, "OnPlayerCollidedObstacle");
                _target.AddChild(node);
            }
        }
    }
}
