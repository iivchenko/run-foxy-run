using Godot;

namespace RunFoxyRun
{
    public sealed class FoxyWrap : KinematicBody2D
    {
        private Vector2 Up = new Vector2(0, -1);
        private int Gravity = 20;
        private int MaxSpeed = 200;
        private int JumpHigh = -550;
        private int Acceleration = 50;

        private Vector2 _motion;
        private bool _friction = false;

        public override void _PhysicsProcess(float delta)
        {
            _motion.y += Gravity;

            if (Input.IsActionPressed("ui_right"))
            {
                _motion.x = Mathf.Min(_motion.x + Acceleration, MaxSpeed);
                GetNode<AnimatedSprite>(new NodePath("Animator")).FlipH = false;
                GetNode<AnimatedSprite>(new NodePath("Animator")).Play("run");
            }
            else if (Input.IsActionPressed("ui_left"))
            {
                _motion.x = Mathf.Max(_motion.x - Acceleration, -MaxSpeed);
                GetNode<AnimatedSprite>(new NodePath("Animator")).FlipH = true;
                GetNode<AnimatedSprite>(new NodePath("Animator")).Play("run");
            }
            else
            {
                _friction = true;
                GetNode<AnimatedSprite>(new NodePath("Animator")).Play("idle");
            }

            if (IsOnFloor())
            {
                if (Input.IsActionJustPressed("ui_up"))
                {
                    _motion.y = JumpHigh;
                }

                if (_friction)
                    _motion.x = Mathf.Lerp(_motion.x, 0, 0.2f);
            }
            else
            {
                if (_motion.y < 0)
                    GetNode<AnimatedSprite>(new NodePath("Animator")).Play("jump");
                else
                    GetNode<AnimatedSprite>(new NodePath("Animator")).Play("fall");
                if (_friction)
                    _motion.x = Mathf.Lerp(_motion.x, 0, 0.05f);
            }

            _motion = MoveAndSlide(_motion, Up);
        }
    }
}