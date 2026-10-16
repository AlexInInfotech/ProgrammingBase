using Godot;
using System;

[GlobalClass]
public partial class MoveComponent : Node
{
    protected CharacterBody3D parentNode;

    protected Node3D target;

    [Export] protected float speed = 10f;

    [Export] protected float stopRadius = 0.5f;

    public override void _Ready()
    {
        if (GetParent() is CharacterBody3D parent)
            parentNode = parent;
    }

    public void SetTarget(Node3D target)
    {
        this.target = target;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (parentNode != null)
            Move(parentNode);
    }

    private void Move(CharacterBody3D body)
    {
        Vector3 diff; 
        if (target != null)
            diff = target.GlobalPosition - body.GlobalPosition;
        else  // Если цели нет, движемся к началу координат (0, 0, 0)
            diff = Vector3.Zero - body.GlobalPosition;

        Vector3 direction = diff.Normalized();

        if (diff.Length() > stopRadius)
        {
            Vector3 velocity = new Vector3(direction.X * speed, 0, direction.Z * speed);
            body.Velocity = velocity;
            body.MoveAndSlide();
        }
        else
            body.Velocity = Vector3.Zero;
    }
}
