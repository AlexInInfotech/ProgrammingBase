using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node
{
    private Node3D parentNode => GetParent<Node3D>();

    [Export] protected float maxHealth = 100f;

    protected float currentHealth = 100f;

    public override void _Ready()
    {

        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
        
    }

    private void Die()
    {
        parentNode.QueueFree();
    }
}