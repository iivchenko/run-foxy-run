using Godot;

namespace RunFoxyRun
{
    public class SpawnerWrap : Area2D
    {
        private const int Block = 16; // 16 pixels

        [Export]
        public PackedScene Node { get; set; }

        [Export]
        public string TargetScene { get; set; }

        private Node _target;

        public override void _Ready()
        {
            _target = GetNode(new NodePath(TargetScene));
        }

        public override void _PhysicsProcess(float delta)
        {
            var foxy = GetNode(new NodePath("/root/World/Foxy")) as Node2D;

            if ((Position.x - foxy.Position.x) < 15 * Block)
            {
                this.MoveLocalX(16);

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
}