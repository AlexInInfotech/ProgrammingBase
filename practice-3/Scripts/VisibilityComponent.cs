using Godot;
using System;

[GlobalClass]
public partial class VisibilityComponent : Area3D
{
    private Node3D parentNode => GetParent<Node3D>();

    private Node3D target;

    [Export] public ShootComponent shootComponent;

    [Export] public MoveComponent moveComponent;

    private Godot.Collections.Array<Node3D> detectedBodies = new Godot.Collections.Array<Node3D>();

    public override void _Ready()
    {

        if (this.GetNodeOrNull<CollisionShape3D>("CollisionShape3D") != null)
        {
            this.BodyEntered += (Node3D body) => OnBodyEntered(body); 
            this.BodyExited += (Node3D body) => OnBodyExited(body); 

            Godot.Collections.Array<Node3D> overlappingBodies = GetOverlappingBodies(); // Получаем тела, которые находятся в области

            for (int i = 0; i < overlappingBodies.Count; i++)
                OnBodyEntered(overlappingBodies[i]); 
        }
    }

    private void OnBodyEntered(Node3D body)
    {
        bool isInSameGroup = false;

        if (
            (body.IsInGroup("Enemy") && parentNode.IsInGroup("Enemy"))
            ||
            (body.IsInGroup("Tower") && parentNode.IsInGroup("Tower"))
        )
            isInSameGroup = true;

        if (!isInSameGroup)
        {
            HealthComponent helthComponent = body.GetNodeOrNull<HealthComponent>("HealthComponent");

            if (helthComponent != null)
                detectedBodies.Add(body);  //если есть компонент здоровья и из разных групп то добавляем в список целей
        }
    }

    private void OnBodyExited(Node3D body)
    {
        detectedBodies.Remove(body); // Удаляем объект из списка потенциальных целей
    }

    public override void _Process(double delta)
    {
        Node3D closestBody = null;
        // Перебираем все обнаруженные объекты и определяем близжащий
        for (int i = 0; i < detectedBodies.Count; i++)
        {
            Vector3 distanceToTarget = GlobalPosition - detectedBodies[i].GlobalPosition;

            if (closestBody == null
                || distanceToTarget.Length() < (GlobalPosition - closestBody.GlobalPosition).Length())
                closestBody = detectedBodies[i];
        }
        target = closestBody;

        if (shootComponent != null)
            shootComponent.SetTarget(target);

        if (moveComponent != null)
            moveComponent.SetTarget(target);
    }
}
