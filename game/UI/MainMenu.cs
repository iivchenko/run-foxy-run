using Godot;

public class MainMenu : Control
{
	private void OnStartGameButtonPressed()
	{
	    GetTree().ChangeScene("res://objects/World/World.tscn");
	}
	
	private void OnExiteGamePressed()
	{
        GetTree().Quit();
	}
}