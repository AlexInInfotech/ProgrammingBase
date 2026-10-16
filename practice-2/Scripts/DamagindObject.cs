using Godot;
using System;

public partial class DamagingObject : MovingObject
{
    [Export] public int damage = 1000;
    const string LEFTWALL = "Left_wall";
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered; 
    }

   
    private void OnBodyEntered(Node2D body)  //обраблотка коллизии с игроком
    {
        if (body.Name == LEFTWALL)
            QueueFree();
        if (body is Player player)
        {
            player.TakeDamage(damage);
            QueueFree();
        }
    }
   
}