using Godot;

namespace RunFoxyRun
{
    public class TestWorld_Case03 : Node2D
    {
        public override void _Ready()
        {
            var groundUpper = (PackedScene)ResourceLoader.Load("res://objects/Grounds/GroundUpper.tscn");
            var groundMiddle = (PackedScene)ResourceLoader.Load("res://objects/Grounds/GroundMiddle.tscn");

            ObjectPool.Pool.RegisterCollection("GroundUpper", 
                () => 
                    {
                        var node = groundUpper.Instance();
                        AddChild(node);

                        return node;
                    });
            ObjectPool.Pool.RegisterCollection("GroundMiddle",
                () =>
                {
                    var node = groundMiddle.Instance();
                    AddChild(node);

                    return node;
                });
        }
    }

}