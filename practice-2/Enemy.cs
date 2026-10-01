using Godot;
using System;

public partial class Enemy : DamagingObject
{
    [Export] public int health = 50;

    public virtual void TakeDamage(int amount) 
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    public virtual void Die()
    {
        QueueFree(); 
    }

    public virtual Vector2 GetSpawnPosition()
    {
        return Vector2.Zero; 
    }
}