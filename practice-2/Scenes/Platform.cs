using Godot;
using System;

public partial class Platform : MovingObject
{
    public static Transform2D spawnPoint;
    private float _spawnRadius = 20f;
    public override void _PhysicsProcess(double delta)  //базовое движение всех объектов
    {
       base._PhysicsProcess(delta);
        if (Position.X < -700)
            QueueFree();

    }
    public override Vector2 GetSpawnPosition()
    {
        Vector2 position = spawnPoint.Origin;
        position.Y += (float)GD.RandRange(-_spawnRadius, _spawnRadius);
        return position;
    }
}
