using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class MovingObject : Area2D
{
    public static float baseSpeed;
    [Export] protected float relativeSpeed = 0;
    public override void _PhysicsProcess(double delta)  //базовое движение всех объектов
    {
        Position += Vector2.Left * (baseSpeed + relativeSpeed)* (float)delta;
        
    }
    public virtual Vector2 GetSpawnPosition()
    {
        return Vector2.Zero;
    }
}
