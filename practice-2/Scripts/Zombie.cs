using Godot;
using System;

public partial class Zombie : Enemy
{
    [Export] private float speedOfJerk = 70;
    [Export] private float visibilityRadius = 200;
    private bool isJerk = false;

    public Node2D player;
    public static Transform2D spawnPoint;
    public override Vector2 GetSpawnPosition()
    {
        return spawnPoint.Origin;
    }


    public override void _PhysicsProcess(double delta) 
    {
        base._PhysicsProcess(delta);
        if (Math.Abs(player.Position.X - Position.X) <= visibilityRadius && !isJerk)
        {
            relativeSpeed = speedOfJerk;
            isJerk = true;
        }

    }
}

