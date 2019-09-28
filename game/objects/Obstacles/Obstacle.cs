using Godot;

namespace RunFoxyRun
{
    public class Obstacle : Node
    {
        private void OnOverlapped(PhysicsBody2D body)
        {
            if (body.Name == "Foxy")
            {
                GetTree().ReloadCurrentScene();
            }
        }
    }
}
