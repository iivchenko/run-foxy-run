using Godot;

namespace RunFoxyRun
{
    public class Destructor : Area2D
    {
        private const int Block = 16; // 16 pixels

        public override void _PhysicsProcess(float delta)
        {
            var foxy = GetNode(new NodePath("/root/World/Foxy")) as Node2D;

            if ((foxy.Position.x - Position.x) > 50 * Block)
            {
                this.MoveLocalX(16);
            }
        }

        private void OnBodyEntered(Node body)
        {
            body.QueueFree();
        }

        private void OnAreaEntered(Node area)
        {
            area.QueueFree();
        }
    }
}