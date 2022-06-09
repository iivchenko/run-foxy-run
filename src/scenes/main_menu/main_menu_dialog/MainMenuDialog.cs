using Godot;

public class MainMenuDialog : Control
{
    [Signal]
    public delegate void ButtonPressed();

    private Button _start;
    private Button _exit;

    public override void _Ready()
    {
        _start = GetNode<Button>("StartButton");
        _exit = GetNode<Button>("ExitButton");

        _start.Connect("pressed", this, nameof(StartButtonPressed));
        _exit.Connect("pressed", this, nameof(ExitButtonPressed));
    }

    private void StartButtonPressed()
    {
        EmitSignal(nameof(ButtonPressed), MainMenuDialogButton.Start);
    }

    private void ExitButtonPressed()
    {
        EmitSignal(nameof(ButtonPressed), MainMenuDialogButton.Exit);
    }
}


public enum MainMenuDialogButton
{
    Start,
    Exit
}
