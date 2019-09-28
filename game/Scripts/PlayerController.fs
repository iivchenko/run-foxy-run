namespace RunFoxyRun

open Godot

type public PlayerController =    
    inherit RigidBody2D

    [<Export>]
    val mutable public _speed : int

    new () =
        {
            _speed = 20
        }

     override this._IntegrateForces (state : Physics2DDirectBodyState) =
        ()    


    // member public this.GetInput () =
    
    //     let mutable x =  
    //         if Input.IsActionPressed("right") 
    //             then 1.0f   
    //         elif Input.IsActionPressed("left") 
    //             then (-1.0f)
    //         else 0.0f
           
    //     let mutable y =
    //         if Input.IsActionPressed("down")
    //             then 1.0f
    //         elif Input.IsActionPressed("up")
    //             then - 1.0f
    //         else 0.0f

    //     let x =  x * float32 this._speed
    //     let y = y * float32 this._speed

    //     Vector2 (x, y)

    // override this._PhysicsProcess(delta : float32) =
    //     this.GetInput()
    //     |> this.MoveAndSlide
    //     |> ignore

    //     this._IntegrateForces