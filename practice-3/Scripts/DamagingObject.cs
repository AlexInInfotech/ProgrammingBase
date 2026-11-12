using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class DamagingObject : Area3D
{
    [Export] public float damage = 10;
    [Export] public bool isDisposable  = true;

    [Export] public string[] TargetGroups;
    [Export] private float delay = 1f;
    private Node3D bodyInCollision;

    private Timer _damageTimer;
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited; 
        _damageTimer = new Timer();
        _damageTimer.WaitTime = delay;
        _damageTimer.OneShot = false; // Таймер должен повторяться
        _damageTimer.Timeout += OnDamageTimerTimeout; // Привязываем событие тика
        AddChild(_damageTimer);
    }
    private void OnDamageTimerTimeout()
    {
        if(bodyInCollision != null) 
            ApplyDamage(bodyInCollision);
    }
    private void OnBodyExited(Node3D body)
    {
        if (!isDisposable)
            _damageTimer.Stop();
    }

    private void OnBodyEntered(Node3D body)
    {
        //GD.Print("bool");
        if (IsTargetValid(body)) 
        {
            ApplyDamage(body);
            if (isDisposable) //если объект одноразовый то уничтожаем
                SelfDestruct();
            else
            {
                GD.Print("Timer start");
                _damageTimer.Start();
            }
        }

    }

    private bool IsTargetValid(Node3D target)// Проверяем входит ли объект хотя бы в одну из разрешенных групп
    {
        if (TargetGroups.Length == 0) return false;

        foreach (string group in TargetGroups)
            if (target.IsInGroup(group))
                return true;
        return false;
    }

    public virtual void ApplyDamage(Node3D target)
    {
        if (target == null)
            return;
        
        HealthComponent helthComponent = target.GetNodeOrNull<HealthComponent>("HealthComponent");//Ищем компонент

        if (helthComponent != null)
        {
            helthComponent.TakeDamage(damage);
        }
    }

    private void SelfDestruct()
    {
        // Уничтожаем родительский объект а не только компонент
        GetParent().QueueFree();
    }
}