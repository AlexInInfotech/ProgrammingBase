using Godot;
using System;

public partial class Spike : Obstacle
{
    [Export] public float amplitude = 70f;
    [Export] public float frequency = 4.0f;
    public static Transform2D spawnPoint;

    private float _time = 0.0f;
    private float _startY;

    public override void _Ready()
    {
        _startY = Position.Y;
    }

    public override void _PhysicsProcess(double delta)
    {
        float fDelta = (float)delta;
        _time += fDelta;

        Vector2 pos = Position;
        pos.X += Vector2.Left.X * (relativeSpeed + baseSpeed) * fDelta;
        pos.Y = _startY + Mathf.Sin(_time * frequency) * amplitude;
        Position = pos;
    }

    public override Vector2 GetSpawnPosition()
    {
        return spawnPoint.Origin;
    }
}