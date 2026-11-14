using Godot;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;

[GlobalClass]
public partial class Player : CharacterBody3D
{
    private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    [Export]
    private float Speed = 5.0f;
    [Export]
    private float MouseSensitivity = 0.002f;
    [Export]
    private Godot.Collections.Array<Node> Inventory = new Godot.Collections.Array<Node>();

    [Export] float damage = 10f;
    bool isLocked = false;
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;
        velocity.Y -= gravity * (float)delta;

        Vector2 input = Input.GetVector("left", "right", "forward", "backward");
        Vector3 movementDirection = Transform.Basis * new Vector3(input.X, 0, input.Y);
        velocity.X = movementDirection.X * Speed;
        velocity.Z = movementDirection.Z * Speed;
        if (isLocked)
            velocity = Vector3.Zero;
        Velocity = velocity;
        MoveAndSlide();
    }
    Node collider;
    IInterectable interactable;
    public override void _Process(double delta)
    {
        Label label = GetNode<Label>("CanvasLayer/CameraLabel");
        RayCast3D rayCast = GetNode<RayCast3D>("Camera3D/RayCast3D");

        rayCast.ForceRaycastUpdate();
        collider = rayCast.GetCollider() as Node;
        if (collider != null )
        {
            if(collider is IInterectable a)
            {
                label.Text = $"Press 'E' to interact with {collider.Name}";
                interactable = a;
            }
            else
            {
                interactable = collider.GetParentOfType<IInterectable>();
                if (interactable != null)
                    label.Text = $"Press 'E' to interact";
                else
                    label.Text = "";
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion && !isLocked)
        {
            RotateY(-mouseMotion.Relative.X * MouseSensitivity);

            Camera3D camera = GetNode<Camera3D>("Camera3D");
            camera.RotateX(-mouseMotion.Relative.Y * MouseSensitivity);
            Vector3 cameraRotation = camera.Rotation;
            cameraRotation.X = Mathf.Clamp(cameraRotation.X, Mathf.DegToRad(-70), Mathf.DegToRad(70));
            camera.Rotation = cameraRotation;
        }
        if (@event.IsActionPressed("quit"))
        {
            GetTree().Quit();
        }
        if ( collider != null && interactable != null)
        {
            if (@event.IsActionPressed("interact"))
            {
                interactable.Interact();
                if (interactable is IReadable)
                    isLocked = !isLocked;
                if (interactable is ICollectable)
                    Inventory.Add(interactable as Node);
            }
            if (@event.IsActionPressed("attack") && interactable is IDamagable i && !isLocked)
            {
                i.ReceiveDamage(damage);
            }
        }
    }
}
