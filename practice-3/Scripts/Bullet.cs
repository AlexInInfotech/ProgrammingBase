using Godot;
using System;

public partial class Bullet : Area3D
{
    private float speed = 100f;
    private float damage = 10f;
    public void SetSpeed(float value)
    {
        speed = value;
    }

    public void SetDamage(float value)
    {
        damage = value;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += -Transform.Basis.Z * speed * (float)delta;
    }

    public void OnBulletBodyEntered(Node3D body)
    {
        if (body is CharacterBody3D character)
        {
            HealthComponent healthComponent = character.GetNodeOrNull<HealthComponent>("HealthComponent");
            if (healthComponent != null)
                healthComponent.TakeDamage(damage);
        }
        QueueFree();
    }
}
