using Godot;
using System;

public partial class UIManager : Node
{
    [Export] Label _metallLabel;
    [Export] Label _towerLabel;
    [Export] string[] _towerNames;
    const string TEXT = "Металл: ";

    static Label metallLabel;
    static Label towerLabel;
    static string[] towerNames;
    public override void _Ready()
	{
        metallLabel = _metallLabel;
        towerLabel = _towerLabel;
        towerNames = _towerNames;
        UpdateMetallCount(MetallManager.currentCount);
        UpdateTowerType(0);

    }
    public static void UpdateTowerType(byte index)
    {
        towerLabel.Text = towerNames[index];
    }
    public static void UpdateMetallCount(int value)
    {
        metallLabel.Text = TEXT + value;
    }
}
