using Godot;
using System;

public partial class Bullet : Sprite2D
{
    [Export] public int damage = 10;
    [Export] public int speed = 100;
     protected Vector2 _direction = Vector2.Right;
    private void onBodyEntered(Node2D body){
        QueueFree();
    }
    private void onAreaEntered(Area2D area) //обработка колизии с врагом
    {
        if (area is Damagable creature)
            creature.TakeDamage(damage);
        if (area.GetParent() is Damagable parent)
            parent.TakeDamage(damage);
        QueueFree();
    }
    public override void _PhysicsProcess(double delta)
    {
        Position += _direction * speed * (float)delta;
		
    }
}
