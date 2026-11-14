using Godot;
using System;

public interface IDamagable : IInterectable
{
	void ReceiveDamage(float damage) { }
	void Die() { }
}
