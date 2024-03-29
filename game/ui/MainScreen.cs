using Godot;

namespace RunFoxyRun
{
    public class MainScreen : Control
    {
        private void OnStartGameButtonPressed()
        {
            GetTree().ChangeScene("res://objects/World/World.tscn");
        }

        private void OnLeadersButtonPressed()
        {
            GetTree().ChangeScene("res://ui/LeaderBoardScreen.tscn");
        }

        private void OnExiteGamePressed()
        {
            GetTree().Quit();
        }
    }
}
