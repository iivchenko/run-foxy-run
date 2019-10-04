using Godot;

namespace RunFoxyRun
{
    public class Obstacle : Node2D
    {
        private RayCast2D _ray;
        private AudioStreamPlayer _player;

        [Export]
        public int Scores { get; set; }

        [Signal]
        public delegate void OverCrossed(int scores);

        public override void _Ready()
        {
            _ray = GetNode<RayCast2D>("Ray");
            _player = GetNode<AudioStreamPlayer>("Audio");
        }

        public override void _PhysicsProcess(float delta)
        {
            if (_ray.IsColliding())
            {
                _ray.Enabled = false;
                _player.Play();

                var scores = new Label
                {
                    Text = $"+{Scores}"
                };

                scores.SetModulate(Color.ColorN("Black"));

                var collider = (_ray.GetCollider() as Node2D);
                var position = collider.Position - Position;
                position.y -= 10;

                scores.SetPosition(position);

                AddChild(scores);

                EmitSignal(nameof(OverCrossed), Scores);
            }
        }

        private void OnOverlapped(PhysicsBody2D body)
        {
            GetTree().ReloadCurrentScene();
        }
    }
}