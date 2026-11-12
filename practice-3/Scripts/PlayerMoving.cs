using Godot;
using System;

public partial class PlayerMoving : CharacterBody3D
{
    [Export]float Speed = 20f;
    [Export] public Node3D camera;
    [Export] public float MinLookAngle  = -85.0f;
    [Export] public float MaxLookAngle  = 85.0f;
    [Export] public float MouseSensitivity = 0.002f;
    private float _rotationX = 0f;

    public override void _Ready()
    {
        CaptureMouse();
    }

    public override void _PhysicsProcess(double delta)
    {
        // 1. Движение (WASD) по осям X и Z (горизонтальная плоскость в 3D)
        Vector2 inputDir = Input.GetVector("left", "right", "forward", "back");


        // напрвление куда смотрит камера
        Vector3 direction = Vector3.Zero;
        direction = (camera.GlobalTransform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        Vector3 velocity = Velocity;


        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Y = 0; 
            velocity.Z = direction.Z * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();

    }
    public override void _Input(InputEvent @event)
    {

        // Поворот работает ТОЛЬКО когда мышь надежно захвачена в центре экрана
        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            // 1. Влево-вправо (Тело)
            RotateY(-mouseMotion.Relative.X * MouseSensitivity);

            // 2. Вверх-вниз (Камера)
            if (camera != null)
            {
                _rotationX -= mouseMotion.Relative.Y * MouseSensitivity;

                float minRad = Mathf.DegToRad(MinLookAngle);
                float maxRad = Mathf.DegToRad(MaxLookAngle);
                _rotationX = Mathf.Clamp(_rotationX, minRad, maxRad);

                Vector3 cameraRotation = camera.Rotation;
                cameraRotation.X = _rotationX;
                camera.Rotation = cameraRotation;
            }
        }

        // Освобождение мышки на ESC (для дебага в редакторе)
        if (@event.IsActionPressed("cancel"))
        {
            ReleaseMouse();
        }
    }
    // Метод для надежного скрытия и блокировки мыши
    private void CaptureMouse()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    // Метод для освобождения мыши
    private void ReleaseMouse()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }

}
