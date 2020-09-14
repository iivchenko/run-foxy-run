using Godot;

namespace RunFoxyRun
{
	public class LeaderBoardScreen : Control
	{
		public override void _Ready()
		{
			var board = FindNode("Leaders", true) as VBoxContainer;

			foreach (var leader in LeaderBoardService.GetLeaders())
			{
				var label = new Label
				{
					Text = $"{leader.Name} ({leader.Score})"
				};

				board.AddChild(label);
			}
		}

		private void OnMainScreenButtonPressed()
		{
			GetTree().ChangeScene("res://ui/MainScreen.tscn");
		}
	}
}
