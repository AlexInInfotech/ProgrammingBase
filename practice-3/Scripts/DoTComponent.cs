using Godot;
using System;


public partial class DoTComponent : DamagingObject
{
    [Export] public float tickDelay = 1.0f; // Интервал между тиками (в секундах)
    [Export] public int totalTicks { get; set; } = 5;         // Сколько всего раз сработает урон
    [Export] public Color fireColor;

    // Переопределяем метод нанесения урона
    public override void ApplyDamage(Node3D target)
    {
        HealthComponent helthComponent = target.GetNodeOrNull<HealthComponent>("HealthComponent");//Ищем компонент
        MeshInstance3D mesh = target.GetNodeOrNull<MeshInstance3D>("MeshInstance3D");

        if (helthComponent != null)
        {
            // Создаем динамический таймер, который будет жить внутри цели и тикать
            Timer dotTimer = new Timer();
            dotTimer.WaitTime = tickDelay;
            dotTimer.OneShot = false;

            int ticksRemaining = totalTicks;
            if (mesh != null && mesh.GetActiveMaterial(0) is StandardMaterial3D originalMaterial)
            // Получаем материал. Используем GetActiveMaterial(0), чтобы гарантированно достать рабочий материал
            {
                // Дублируем материал чтобы изменение цвета затронуло только эту башню
                StandardMaterial3D freezeMaterial = (StandardMaterial3D)originalMaterial.Duplicate();

                // Включаем и настраиваем синее свечение (Emission)
                freezeMaterial.EmissionEnabled = true;
                freezeMaterial.Emission = fireColor;
                freezeMaterial.EmissionEnergyMultiplier = 2.0f; // Яркость свечения

                // Применяем измененный материал к мешу
                mesh.SetSurfaceOverrideMaterial(0, freezeMaterial);


            }
            // Подключаем лямбда-выражение к сигналу Timeout таймера
            dotTimer.Timeout += () =>
            {
                if (GodotObject.IsInstanceValid(helthComponent))
                    helthComponent.TakeDamage(damage);

                ticksRemaining--;

                if (ticksRemaining <= 0)
                {
                    dotTimer.QueueFree();
                    mesh.SetSurfaceOverrideMaterial(0, null);
                }
                    
            };

            // Добавляем таймер в дерево сцены цели, чтобы он работал независимо от пули
            target.AddChild(dotTimer);
            dotTimer.Start();

            // Наносим первый тик урона сразу при попадании 
            helthComponent.TakeDamage(damage);
        }
    }
}