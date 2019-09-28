namespace RunFoxyRun

open Godot
open System

type public Foxy () =
    inherit KinematicBody2D ()

    let _up = Nullable<Vector2>(Vector2(0.0f, -1.0f))
    let _gravity = 20.0f
    let _maxSpeed = 200.0f
    let _jumpHigh = -550.0f
    let _acceleration = 50.0f

    let mutable _motion = Vector2(0.0f, 0.0f)
    let mutable _friction = false

    override this._PhysicsProcess(delta:float32) = 
        _motion.y <- _motion.y + _gravity
        let animator = base.GetNode<AnimatedSprite>(new NodePath("Animator"))
        
        if Input.IsActionPressed("ui_right")
            then
                _motion.x <- Mathf.Min(_motion.x + _acceleration, _maxSpeed)
                animator.FlipH <- false
                animator.Play("run")
            else if Input.IsActionPressed("ui_left")
                then 
                    _motion.x <- Mathf.Max(_motion.x - _acceleration, -_maxSpeed)
                    animator.FlipH <- true
                    animator.Play("run")
            else 
                _friction <- true
                animator.Play("idle")

        if base.IsOnFloor() 
            then 
                if Input.IsActionJustPressed("ui_up")
                    then
                        _motion.y <- _jumpHigh
                
                if _friction
                    then 
                        _motion.x <- Mathf.Lerp(_motion.x, 0.0f, 0.2f);
            else 
                if _motion.y < 0.0f
                    then 
                        animator.Play("jump")
                    else
                        animator.Play("fall");
                if _friction
                    then
                        _motion.x <- Mathf.Lerp(_motion.x, 0.0f, 0.05f);
                    else
                        ()

        _motion <- base.MoveAndSlide(_motion, _up)