using Godot;
using System;

public partial class Dog : Enemy
{
    [Export] private float chaseRadius = 300;
    public Node2D player;
    public static Transform2D spawnPoint;
    public override Vector2 GetSpawnPosition()
    {
        return spawnPoint.Origin;
    }


    public override void _PhysicsProcess(double delta)
    {
        if (Math.Abs(player.Position.X - Position.X) < chaseRadius)
            if (player.Position.X - Position.X > 0)
                relativeSpeed = -2*baseSpeed;
            else
                relativeSpeed = 0;
        
        base._PhysicsProcess(delta);

    }
}
