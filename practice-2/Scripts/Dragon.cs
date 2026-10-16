using Godot;
using System;
using static Godot.TextServer;

public partial class Dragon : Enemy
{

    [Export] private float fireDelay = 3f;
    [Export] public PackedScene fireScene;
    public Node2D player;
    public static Transform2D spawnPoint;
    private double timer = 0;
    private void CreateFire()
    {
        DragonFire fire = fireScene.Instantiate<DragonFire>();
        fire.Position = Position;
        fire.SetDirection(player.Transform.Origin - Transform.Origin);
        GetTree().CurrentScene.AddChild(fire);
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        timer += delta;
        if (timer > fireDelay)
        {
            CreateFire();
            timer= 0;
        }

    }
    public override Vector2 GetSpawnPosition()
    {
        return spawnPoint.Origin;
    }
}
