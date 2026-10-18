using Godot;
using System;

public partial class Spike : Obstacle
{
    public static Transform2D spawnPoint;
    public override Vector2 GetSpawnPosition()
    {
        return spawnPoint.Origin;
    }
    [Export] public float amplitude = 70f;
    [Export] public float frequency = 4.0f;

    private float _time = 0.0f;
    private float _startY= spawnPoint.Origin.Y;
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        float fDelta = (float)delta;
        _time += fDelta;

        Vector2 pos = Position;
        pos.Y = _startY + Mathf.Sin(_time * frequency) * amplitude;
        Position = pos;
    }
}