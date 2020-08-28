namespace RunFoxyRun

open Godot

//module FNodes = 
    

type World () =
    inherit Node2D ()

    let setText (label : Label) text =
        label.Text <- text

    let mutable scoresLabel : Label = null

    //let withNode<'a> path (node : Node) = 
    //    node.GetNode<'a>(path)
        

    override this._Ready() =        
        GlobalState.State.Scores <- 0
        scoresLabel <- base.GetNode<Label>(NodePath("HUD/Scores"))

    member public this.ScorePlayer(scores : int) = 
        GlobalState.State.Scores <- GlobalState.State.Scores + scores
        scoresLabel.Text <- System.String.Format("Scores: {0}", GlobalState.State.Scores)

    member public this.OnPlayerCollidedObstacle() =
        base.GetTree().ChangeScene("res://UI/GameOverScreen.tscn")