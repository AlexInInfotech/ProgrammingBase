using Godot;
using System;

public partial class Rock : Obstacle
{
    public static Transform2D spawnPoint;
    public override Vector2 GetSpawnPosition()
    {
        return spawnPoint.Origin;
    }
}
