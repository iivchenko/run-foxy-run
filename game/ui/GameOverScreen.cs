using Godot;

namespace RunFoxyRun
{
    public class GameOverScreen : Control
    {
        private LineEdit _playerName;

        public override void _Ready()
        {
            (FindNode("Scores", true) as Label).Text = $"Scores: {GlobalState.State.Scores}";
            _playerName = (FindNode("PlayerNameEditor", true) as LineEdit);

            if(!LeaderBoardService.CanAdd(GlobalState.State.Scores))
            {
                _playerName.Visible = false;
            }
        }

        private void OnRestartButtonPressed()
        {
            if (LeaderBoardService.CanAdd(GlobalState.State.Scores))
            {
                LeaderBoardService.UpdateLeaders(_playerName.Text, GlobalState.State.Scores);
            }
            
            GetTree().ChangeScene("res://objects/World/World.tscn");
        }

        private void OnMainMenuButtonPressed()
        {
            if (LeaderBoardService.CanAdd(GlobalState.State.Scores))
            {
                LeaderBoardService.UpdateLeaders(_playerName.Text, GlobalState.State.Scores);
            }

            GetTree().ChangeScene("res://ui/MainScreen.tscn");
        }
    }
}
