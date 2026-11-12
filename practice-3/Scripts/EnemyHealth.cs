using Godot;
using System;

public partial class EnemyHealth : HealthComponent
{
    [Export] int metallCount;
    protected override void Die()
    {
        base.Die();
        MetallManager.AddMetall(metallCount);
    }
}
