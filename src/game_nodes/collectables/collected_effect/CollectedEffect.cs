using Godot;

public class CollectedEffect : Node2D
{
    private AnimatedSprite _animation;
    private AudioStreamPlayer _audio;

    private int _finished = 0;

    public override void _Ready()
    {
        _animation = GetNode<AnimatedSprite>("Animation");
        _audio = GetNode<AudioStreamPlayer>("Sound");

        _animation.Connect("animation_finished", this, nameof(OnAnimationFinished));
        _audio.Connect("finished", this, nameof(OnAudioFinished));

        _animation.Play("idle");
        _audio.Play();
    }

    public override void _Process(float delta)
    {
        if (_finished >= 2)
        {
            QueueFree();
        }
    }

    private void OnAnimationFinished()
    {
        _finished++;
    }

    private void OnAudioFinished()
    {
        _finished++;
    }
}
