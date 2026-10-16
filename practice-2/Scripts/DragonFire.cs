using Godot;
using System;
using static Godot.TextServer;

public partial class DragonFire : Bullet, Damagable
{
    public int health { get; set; }
    public void TakeDamage(int damage)
    {
        QueueFree();
    }
    
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.Normalized();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        Position += _direction * speed * (float)delta;
       
    }
   
}
