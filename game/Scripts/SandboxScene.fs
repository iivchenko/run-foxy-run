namespace RunFoxRun

open Godot

type public SandboxScene () =
    inherit Node2D ()

    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    override this._Ready () =   
        // Called every time the node is added to the scene.
        // Initialization here
        ()

    override this._Process (delta:float32) =
        this.Position <- Vector2(this.Position.x + float32 1, this.Position.y)
        ()