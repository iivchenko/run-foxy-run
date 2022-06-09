using Godot;

public sealed class LevelSelectorDialog : Control
{
    [Signal]
    public delegate void BackButtonPressed();

    private Button _level01;
    private Button _level02;
    private Button _level03;
    private Button _level04;
    private Button _level05;
    private Button _level06;
    private Button _level07;
    private Button _level08;
    private Button _level09;

    private Button _back;

    public override void _Ready()
    {
        _level01 = FindNode("Level01") as Button;
        _level02 = FindNode("Level02") as Button;
        _level03 = FindNode("Level03") as Button;
        _level04 = FindNode("Level04") as Button;
        _level05 = FindNode("Level05") as Button;
        _level06 = FindNode("Level06") as Button;
        _level07 = FindNode("Level07") as Button;
        _level08 = FindNode("Level08") as Button;
        _level09 = FindNode("Level09") as Button;

        _back = FindNode("BackButton") as Button;

        _level01.Connect("pressed", this, nameof(StartLevel01));
        _back.Connect("pressed", this, nameof(OnBackButtonPressed));

        _level02.Disabled = true;
        _level02.FocusMode = FocusModeEnum.None;
        _level03.Disabled = true;
        _level03.FocusMode = FocusModeEnum.None;
        _level04.Disabled = true;
        _level04.FocusMode = FocusModeEnum.None;
        _level05.Disabled = true;
        _level05.FocusMode = FocusModeEnum.None;
        _level06.Disabled = true;
        _level06.FocusMode = FocusModeEnum.None;
        _level07.Disabled = true;
        _level07.FocusMode = FocusModeEnum.None;
        _level08.Disabled = true;
        _level08.FocusMode = FocusModeEnum.None;
        _level09.Disabled = true;
        _level09.FocusMode = FocusModeEnum.None;

        _back.GrabFocus();
    }

    private void StartLevel01()
    {
        StartLevel("level01.tscn");
    }

    private void OnBackButtonPressed()
    {
        EmitSignal(nameof(BackButtonPressed));
    }

    private void StartLevel(string level)
    {
        GetTree().ChangeScene("res://levels/" + level);
    }
}
