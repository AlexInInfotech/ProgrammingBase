using Godot;
using System;

public partial class Bullet : Sprite2D
{
    [Export] public int damage = 10;
    [Export] public int speed = 100; 
	private void onAreaEntered(Area2D area) //обработка колизии с врагом
    {
		if (area is Enemy enemy)
			enemy.TakeDamage(damage);
		QueueFree();
	}
    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Right * speed * (float)delta;
		if (Position.X >= 600)
			QueueFree();
    }
}
