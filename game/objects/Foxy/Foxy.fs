namespace RunFoxyRun

open Godot
open System

type public Foxy ()  =
    inherit KinematicBody2D ()

    let mutable gravity = 20.0f
    let mutable maxSpeed = 200.0f
    let mutable jumpHigh = 550.0f

    let up = Nullable<Vector2>(Vector2(0.0f, -1.0f))
    let mutable motion = Vector2(0.0f, 0.0f)
    let mutable animator : AnimatedSprite = null

    // TODO: move to the future Fogot (F# to Godot library)
    let ReadAxesInput ()  =
        let x = if Input.IsActionPressed("ui_right") 
                    then Input.GetActionStrength("ui_right")
                else if Input.IsActionPressed("ui_left") 
                    then -Input.GetActionStrength("ui_left")
                    else 0.0f

        let y = if Input.IsActionPressed("ui_up") 
                    then -Input.GetActionStrength("ui_up")
                else if Input.IsActionPressed("ui_down") 
                    then Input.GetActionStrength("ui_down")
                    else 0.0f
        (x, y)

    [<Export>]
    member this.Gravity
        with get () = gravity
        and set (value) = gravity <- value

    [<Export>]
    member this.MaxSpeed
        with get () = maxSpeed
        and set (value) = maxSpeed <- value

    [<Export>]
    member this.JumpHigh
        with get () = jumpHigh
        and set (value) = jumpHigh <- value

    override this._Ready() =
        animator <- base.GetNode<AnimatedSprite>(new NodePath("Animator"))

    override this._PhysicsProcess(delta:float32) = 

        motion.y <- motion.y + gravity

        match ReadAxesInput() with
        | (x, _) when x <> 0.0f ->
            motion.x <- maxSpeed * x
            animator.FlipH <- x < 0.0f
            animator.Play("run")
        | _ ->
            animator.Play("idle")

        match base.IsOnFloor() with
        | true -> 
             motion.x <- Mathf.Lerp(motion.x, 0.0f, 0.2f)
        | false when motion.y < 0.0f -> 
            motion.x <- Mathf.Lerp(motion.x, 0.0f, 0.05f)
            animator.Play("jump")
        | false -> 
            motion.x <- Mathf.Lerp(motion.x, 0.0f, 0.05f)
            animator.Play("fall")

        motion <- base.MoveAndSlide(motion, up)

    override this._Input(e: InputEvent) =
        if e.IsActionPressed("ui_up") && base.IsOnFloor()
            then
                 motion.y <- -jumpHigh