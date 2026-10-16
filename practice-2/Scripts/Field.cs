using Godot;
using System;

public partial class Field : Node2D
{
    [Export] private float baseSpeed = 170;
    [Export] public PackedScene batScene;
    [Export] public PackedScene zombieScene;
    [Export] public PackedScene rockScene;
    [Export] public PackedScene dragonScene;
    [Export] public PackedScene dogScene;
    [Export] public PackedScene spikeScene;
    [Export] public RemoteTransform2D batTransform;
    [Export] public RemoteTransform2D zombieTransform;
    [Export] public RemoteTransform2D rockTransform;
    [Export] public RemoteTransform2D dragonTransform;
    [Export] public RemoteTransform2D dogTransform;
    [Export] public RemoteTransform2D spikeTransform;
    [Export] public Node2D player;
    [Export] public float delay = 3f;
    private float _currentTime = 0;
    public override void _Ready() //инициализация spawnpoint для каждого объекта
    {
        MovingObject.baseSpeed = baseSpeed;
        Bat.spawnPoint = batTransform.Transform;
        Zombie.spawnPoint = zombieTransform.Transform;
        Rock.spawnPoint = rockTransform.Transform;
        Dragon.spawnPoint = dragonTransform.Transform;
        Dog.spawnPoint = dogTransform.Transform;
        Spike.spawnPoint = spikeTransform.Transform;
    }
    public override void _Process(double delta) //инициализация рандомного предмета с определенной задержкой
    {
        _currentTime -= (float)delta;
        if (_currentTime < 0)
        {
            _currentTime = delay;
            switch (GD.RandRange(1, 6))
            {
                case 1:
                    Bat bat = batScene.Instantiate<Bat>();
                    bat.Position = bat.GetSpawnPosition();
                    GetTree().CurrentScene.AddChild(bat);
                    break;
                case 2:
                    Zombie zombie = zombieScene.Instantiate<Zombie>();
                    zombie.Position = zombie.GetSpawnPosition();
                    zombie.player = player;
                    GetTree().CurrentScene.AddChild(zombie);
                    break;
                case 3:
                    Rock rock = rockScene.Instantiate<Rock>();
                    rock.Position = rock.GetSpawnPosition();

                    GetTree().CurrentScene.AddChild(rock);
                    break;
                case 4:
                    Dragon dragon = dragonScene.Instantiate<Dragon>();
                    dragon.Position = dragon.GetSpawnPosition();
                    dragon.player = player;
                    GetTree().CurrentScene.AddChild(dragon);
                    break;
                case 5:
                    Dog dog = dogScene.Instantiate<Dog>();
                    dog.Position = dog.GetSpawnPosition();
                    dog.player = player;
                    GetTree().CurrentScene.AddChild(dog);
                    break;
                case 6:
                    Spike spike = spikeScene.Instantiate<Spike>();
                    spike.Position = spike.GetSpawnPosition();
                    GetTree().CurrentScene.AddChild(spike);
                    break;
            }
        }

    }

}
