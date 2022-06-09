using Godot;

public class PauseDialog : Control
{
    [Signal]
    public delegate void ButtonPressed(PauseDialogButtons button);

    private Button _resumeButton;
    private Button _restartButton;
    private Button _exitButton;

    public override void _Ready()
    {
        _resumeButton = FindNode("ResumeButton") as Button;
        _restartButton = FindNode("RestartButton") as Button;
        _exitButton = FindNode("ExitButton") as Button;

        _resumeButton.Connect("pressed", this, nameof(OnResumeButtonPressed));
        _restartButton.Connect("pressed", this, nameof(OnRestatButtonPressed));
        _exitButton.Connect("pressed", this, nameof(OnExitButtonPressed));
    }

    private void OnResumeButtonPressed()
    {
        EmitSignal(nameof(ButtonPressed), PauseDialogButtons.Resume);

    }

    private void OnRestatButtonPressed()
    {
        EmitSignal(nameof(ButtonPressed), PauseDialogButtons.Restart);
    }

    private void OnExitButtonPressed()
    {
        EmitSignal(nameof(ButtonPressed), PauseDialogButtons.Exit);
    }
}

public enum PauseDialogButtons
{
    Resume,
    Restart,
    Exit
}

