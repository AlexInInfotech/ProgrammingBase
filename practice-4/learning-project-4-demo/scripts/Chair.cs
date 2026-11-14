using Godot;
using System;
[GlobalClass]

public partial class Chair : StaticBody3D, IDamagable
{
    [Export] float health;
    [Export] AnimationPlayer animation;
    public void Interact()
    {
      
    }
    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if (health < 0)
            Die();
        else
        {
            animation.Play("Hit");
        }

    }
    public void Die()
    {
        animation.Play("Destroy");
    }
}
