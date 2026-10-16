using Godot;
using System;

[GlobalClass]
public partial class ShootComponent : Node3D
{
    
    [Export] private PackedScene bulletScene;
    [Export] private float bulletSpeed = 100f;
    [Export] private float bulletDamage = 10f;
    [Export] private float fireRate = 2f;

    private Node3D target;

    private Timer shootTimer = new Timer();

    public override void _Ready()
    {
        shootTimer.WaitTime = 1.0f / fireRate; //создаем таймер
        shootTimer.OneShot = false;
        AddChild(shootTimer);
        shootTimer.Timeout += () => TryShoot();
    }
    public void SetTarget(Node3D target)
    {
        this.target = target;
    }

    public override void _Process(double delta)
    {
        if (target != null)
        {
            LookAt(target.GlobalPosition);
            TurnOnCannon();
        }
        else
        {
            TurnOffCannon();
            Rotation = new Vector3(Rotation.X, Rotation.Y + (float)delta * 0.5f, Rotation.Z);
        }
    }

    private void TryShoot()
    {
        if (bulletScene != null)
            SpawnBullet();
        else
            GD.Print("No bullet scene attached"); 
    }

    private void SpawnBullet()
    {
        Bullet bullet = bulletScene.Instantiate<Bullet>();
        GetTree().CurrentScene.AddChild(bullet);
        bullet.GlobalPosition = GlobalPosition;
        bullet.Rotation = GlobalRotation;
        bullet.SetSpeed(bulletSpeed);
        bullet.SetDamage(bulletDamage);
    }

    private void TurnOnCannon()
    {
        if (shootTimer.IsStopped())
            shootTimer.Start(); 
    }

    private void TurnOffCannon()
    {
        if (!shootTimer.IsStopped())
            shootTimer.Stop(); 
    }

}
