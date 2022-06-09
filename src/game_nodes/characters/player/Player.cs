using Godot;


public sealed class Player : KinematicBody2D
{
    private Vector2 _motion = Vector2.Zero;
    private AnimatedSprite _animation;
    private AudioStreamPlayer2D _audio;

    [Export]
    public float Gravity { get; set; } = 50;

    [Export]
    public float MaxSpeed { get; set; } = 250;

    [Export]
    public float JumpHigh { get; set; } = 800;

    public override void _Ready()
    {
        _animation = GetNode<AnimatedSprite>("Animation");
        _audio = GetNode<AudioStreamPlayer2D>("Audio");
    }

    public override void _PhysicsProcess(float delta)
    {
        _motion.y += Gravity;
        _motion.x = MaxSpeed;

        switch(IsOnFloor())
        {
            case false when _motion.y < 0:
                _animation.Play("jump");
                break;
            case false:
                _animation.Play("fall");
                break;
            default:
                _animation.Play("run");
                break;
        }

        _motion = MoveAndSlide(_motion, Vector2.Up);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("game_jump") && IsOnFloor())
        {
            _motion.y -= JumpHigh;
            _audio.Play();
        }
    }
}
