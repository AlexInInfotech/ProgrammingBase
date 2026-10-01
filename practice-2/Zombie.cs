using Godot;
using System;

public partial class Zombie : Enemy
{
    public static Transform2D spawnPoint;
    public override Vector2 GetSpawnPosition()
    {
        return spawnPoint.Origin;
    }
}

