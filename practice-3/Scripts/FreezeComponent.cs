using Godot;
using System;
using System.Runtime.InteropServices.JavaScript;

public partial class FreezeComponent : DamagingObject
{
    [Export] public float freezeDuration = 3.0f; // Длительность замедления (сек)
    [Export] public float speedMultiplier = 0.5f;
    [Export] public Color freezeColor;


    public override void ApplyDamage(Node3D target)
    {
        base.ApplyDamage(target);
        MoveComponent move = target.GetNodeOrNull<MoveComponent>("MoveComponent");//Ищем компонент
        MeshInstance3D mesh = target.GetNodeOrNull<MeshInstance3D>("MeshInstance3D");

        // Предполагаем, что у MoveComponent есть свойство speed или MaxSpeed
        if (move != null)
        {
            // Получаем текущую скорость (сохраняем, чтобы вернуть)
            float originalSpeed = move.speed;

            // Замедляем цель
            move.speed = originalSpeed * speedMultiplier;

            if (mesh != null && mesh.GetActiveMaterial(0) is StandardMaterial3D originalMaterial)
            // Получаем материал. Используем GetActiveMaterial(0), чтобы гарантированно достать рабочий материал
            {
                // Дублируем материал чтобы изменение цвета затронуло только эту башню
                StandardMaterial3D freezeMaterial = (StandardMaterial3D)originalMaterial.Duplicate();

                // Включаем и настраиваем синее свечение (Emission)
                freezeMaterial.EmissionEnabled = true;
                freezeMaterial.Emission = freezeColor;
                freezeMaterial.EmissionEnergyMultiplier = 2.0f; // Яркость свечения

                // Применяем измененный материал к мешу
                mesh.SetSurfaceOverrideMaterial(0, freezeMaterial);


            }
            // Создаем одноразовый таймер для отмены эффекта
            SceneTreeTimer timer = GetTree().CreateTimer(freezeDuration);
            timer.Timeout += () =>
            {
                // Проверяем жив ли еще враг и его компонент движения
                if (GodotObject.IsInstanceValid(move))
                {
                    move.speed = originalSpeed;
                    mesh.SetSurfaceOverrideMaterial(0, null);
                }
            };
        }
    }
}