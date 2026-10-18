using Godot;
using System;

public partial class Enemy : DamagingObject, Damagable
{
    [Export] public int health { get; set; } = 50;

    public virtual void TakeDamage(int amount) 
    {
        health -= amount;
        if (health <= 0)
            QueueFree();
    }

    
}