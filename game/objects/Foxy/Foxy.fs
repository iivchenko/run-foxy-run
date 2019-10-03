namespace RunFoxyRun

open Godot
open System

type public Foxy ()  =
    inherit KinematicBody2D ()

    // Export stuff
    let mutable gravity = 20.0f
    let mutable maxSpeed = 200.0f
    let mutable jumpHigh = 550.0f

    let up = Nullable<Vector2>(Vector2(0.0f, -1.0f))
    let mutable motion = Vector2(0.0f, 0.0f)
    let mutable animator : AnimatedSprite = null

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
        motion.x <- maxSpeed
        animator.Play("run")

        match base.IsOnFloor() with
        | false when motion.y < 0.0f ->
            animator.Play("jump")
        | false ->
            animator.Play("fall")
        | _ -> ()

        motion <- base.MoveAndSlide(motion, up)

    override this._Input(e: InputEvent) =
        if e.IsActionPressed("ui_up") && base.IsOnFloor()
            then
                 motion.y <- -jumpHigh