using Godot;
using System;

public partial class CoreHealth : HealthComponent
{
    protected override void Die()
    {
        base.Die();
        GetTree().Quit();
    }
}
