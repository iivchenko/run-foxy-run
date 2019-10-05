using Godot;

namespace RunFoxyRun
{
    public class GameOverScreen : Control
    {
        public override void _Ready()
        {
            (FindNode("Scores", true) as Label).Text = $"Scores: {GlobalState.State.Scores}";
        }

        private void OnRestartButtonPressed()
        {
            GetTree().ChangeScene("res://objects/World/World.tscn");
        }

        private void OnMainMenuButtonPressed()
        {
            GetTree().ChangeScene("res://UI/MainMenu.tscn");
        }
    }
}