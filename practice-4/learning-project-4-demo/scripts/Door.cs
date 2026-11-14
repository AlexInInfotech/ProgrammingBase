using Godot;
using System;
[GlobalClass]
public partial class Door : Node3D, IDamagable
{
    [Export] float health;
    [Export] AnimationPlayer animation;
	bool isOpen = false;
	public void Interact()
	{
		if (isOpen)
			animation.PlayBackwards("Open");
        else
            animation.Play("Open");
		isOpen = !isOpen;
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
