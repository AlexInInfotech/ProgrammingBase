using Godot;
using System;

public partial class PlayerShoot : ShootComponent
{
    private double currentTime = 0f;
	public override void _Ready()
	{ currentTime = 0f;
	}

	public override void _Process(double delta)
    {
        currentTime += delta;
        if (Input.IsActionJustPressed("shoot") && currentTime > shootDelay)
        {
            TryShoot();
            currentTime = 0f;
        }
    }
}
