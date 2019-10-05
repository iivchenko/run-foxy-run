using Godot;

namespace RunFoxyRun
{
    public class World : Node2D
    {
        private Label _scoresLabel;

        public override void _Ready()
        {
            GlobalState.State.Scores = 0;

            _scoresLabel = GetNode<Label>(new NodePath("HUD/Scores"));
        }

        public void ScorePlayer(int scores)
        {
            GlobalState.State.Scores += scores;
            _scoresLabel.Text = $"Scores: {GlobalState.State.Scores}";
        }

        public void OnPlayerCollidedObstacle()
        {
            GetTree().ChangeScene("res://UI/GameOverScreen.tscn");
        }
    }

}