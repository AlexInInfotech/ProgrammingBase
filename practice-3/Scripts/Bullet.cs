using Godot;
using System;

public partial class Bullet : Area3D
{
    private float speed = 100f;
    //private float damage = 10f;
    public void SetSpeed(float value)
    {
        speed = value;
    }


    public override void _PhysicsProcess(double delta)
    {
        Position += -Transform.Basis.Z * speed * (float)delta;
    }

}
