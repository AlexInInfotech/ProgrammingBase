using Godot;
using System;

public partial class Obstacle : DamagingObject
{

    public virtual Vector2 GetSpawnPosition()
    {
        return Vector2.Zero;
    }
}
