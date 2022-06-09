using Godot;

public class GameVictoryDialog : Control
{
    [Signal]
    public delegate void ButtonPressed(GameVictoryDialogButtons button);

    private Button _restartButton;
    private Button _exitButton;

    public override void _Ready()
    {
        _restartButton = FindNode("RestartButton") as Button;
        _exitButton = FindNode("ExitButton") as Button;

        _restartButton.Connect("pressed", this, nameof(OnRestatButtonPressed));
        _exitButton.Connect("pressed", this, nameof(OnExitButtonPressed));
    }

    private void OnRestatButtonPressed()
    {
        EmitSignal(nameof(ButtonPressed), GameVictoryDialogButtons.Restart);
    }

    private void OnExitButtonPressed()
    {
        EmitSignal(nameof(ButtonPressed), GameVictoryDialogButtons.Exit);
    }
}

public enum GameVictoryDialogButtons
{
    Restart,
    Exit
}
