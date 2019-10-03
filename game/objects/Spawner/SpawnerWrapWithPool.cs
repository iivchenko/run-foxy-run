﻿using Godot;

namespace RunFoxyRun
{ 
    public sealed class SpawnerWrapWithPool : Area2D
    {
        private const int Block = 16; // 16 pixels

        [Export]
        public string Node { get; set; }

        [Export]
        public string TargetScene { get; set; }

        [Export]
        public bool Spawning { get; set; }

        private Node _target;

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

                Spawn();
            }
        }

        public void Spawn()
        {
            if (Spawning)
            {
                var node = ObjectPool.Pool.Retriev(Node) as Node2D;
                node.SetPosition(Position);
            }
        }
    }
}
