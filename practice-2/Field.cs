using Godot;
using System;

public partial class Field : Node2D
{
    [Export] public PackedScene batScene;
    [Export] public PackedScene zombieScene;
    [Export] public PackedScene rockScene;
    [Export] public RemoteTransform2D batTransform;
    [Export] public RemoteTransform2D zombieTransform;
    [Export] public RemoteTransform2D rockTransform;
    [Export] public float delay = 3f;
    private float _currentTime = 0;
    public override void _Ready() //инициализация spawnpoint для каждого объекта
    {
        Bat.spawnPoint = batTransform.Transform;
        Zombie.spawnPoint = zombieTransform.Transform;
        Rock.spawnPoint = rockTransform.Transform;

    }
    public override void _Process(double delta) //инициализация рандомного предмета с определенной задержкой
    {
        _currentTime -= (float)delta;
        if (_currentTime < 0)
        {
            _currentTime = delay;
            switch (GD.RandRange(1, 3))
            {
                case 1:
                    Bat bat = batScene.Instantiate<Bat>();
                    bat.Position = bat.GetSpawnPosition();
                    GetTree().CurrentScene.AddChild(bat);
                    break;
                case 2:
                    Zombie zombie = zombieScene.Instantiate<Zombie>();
                    zombie.Position = zombie.GetSpawnPosition();

                    GetTree().CurrentScene.AddChild(zombie);
                    break;
                case 3:
                    Rock rock = rockScene.Instantiate<Rock>();
                    rock.Position = rock.GetSpawnPosition();

                    GetTree().CurrentScene.AddChild(rock);
                    break;

            }
        }

    }

}
