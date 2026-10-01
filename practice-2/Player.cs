using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [ExportGroup("Настройки прыжка")]
    [Export(PropertyHint.Range, "100,1500,10")]
    public float jumpForce = 400.0f;
    [ExportGroup("Физика")]
    [Export(PropertyHint.Range, "100,3000,10")]
    public float customGravity = 980.0f;
    [Export] public PackedScene bulletScene { get; set; }
    private int _health = 50;
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
            QueueFree();
    }
    private void CreateBullet()     //инициализация пули
    {
        Bullet bullet = bulletScene.Instantiate<Bullet>();
        bullet.Position = Position;
        GetTree().CurrentScene.AddChild(bullet);
    }

    public override void _PhysicsProcess(double delta)  //обработка нажатых клавиш
    {
        if (Input.IsActionJustPressed("shoot"))
            CreateBullet();

        Vector2 velocity = Velocity;
        if (!IsOnFloor())
            velocity.Y += customGravity * (float)delta;

        if (Input.IsActionJustPressed("jump") && IsOnFloor())
            velocity.Y = -jumpForce; 
        velocity.X = 0;
        Velocity = velocity;
        MoveAndSlide();
    }
}
