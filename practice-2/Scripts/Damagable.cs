using Godot;
using System;

public partial interface Damagable 
{
    public int health {  get;  set; }
    public void TakeDamage(int damage);
}
