using Godot;
using System;
using System.Runtime.InteropServices.JavaScript;

public partial class Build : Node3D
{
	private bool isBuilding = false;
    const string BUILD = "buildingMode";
    [Export] PackedScene ghostTower;
    [Export] PackedScene[] towers;
    [Export] float distance;
    [Export] int towerCost = 2;
    private byte _currentTowerIndex = 0;
    private Node3D _currentGhostInstance;
    private void StartBuild()
    {

        
        _currentGhostInstance = (Node3D)ghostTower.Instantiate();

        AddChild(_currentGhostInstance);

        _currentGhostInstance.Position = new Vector3(0, 0, -distance);


    }
   private void EndBuild()
    {
        MetallManager.AddMetall(-towerCost);

        Node3D realTower = (Node3D)towers[_currentTowerIndex].Instantiate();
        
        GetTree().Root.GetChild(0).AddChild(realTower);

        realTower.GlobalPosition = _currentGhostInstance.GlobalPosition;
        realTower.GlobalRotation = _currentGhostInstance.GlobalRotation;


        _currentGhostInstance.QueueFree();
        _currentGhostInstance = null;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("first"))
            _currentTowerIndex = 0;
        if (Input.IsActionJustPressed("second"))
            _currentTowerIndex = 1;
        if (Input.IsActionJustPressed("third"))
            _currentTowerIndex = 2;
        UIManager.UpdateTowerType((byte) _currentTowerIndex);
        if (Input.IsActionJustPressed(BUILD))
        {
            if (MetallManager.currentCount >= towerCost)
            {
                isBuilding = !isBuilding;
                if (isBuilding)
                    StartBuild();
                else
                    EndBuild();
            }
            else
            {
                GD.Print("Недостаточно металла!!!!");
            }
        }
    }
   
}
