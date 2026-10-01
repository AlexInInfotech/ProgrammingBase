using Godot;
using System;

public partial class DamagingObject : Area2D
{
    [Export] public float speed =170f;
    [Export] public int damage = 1000;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered; //подписка на колизии
    }
   

    public override void _PhysicsProcess(double delta)  //базовое движение всех объектов
    {
        Position += Vector2.Left * speed * (float)delta;
        if (Position.X < -700)
            QueueFree();
    }
    private void OnBodyEntered(Node2D body)  //обраблотка коллизии с игроком
    {
        if (body is Player player)
        {
            player.TakeDamage(damage);
            QueueFree();
        }
    }
   
}