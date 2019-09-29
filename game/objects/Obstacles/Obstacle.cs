using Godot;

namespace RunFoxyRun
{
    public class Obstacle : Node2D
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
