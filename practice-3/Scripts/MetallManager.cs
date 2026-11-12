using Godot;

public static partial class MetallManager 
{
    public static int currentCount { get; private set; } = 0;
    public static void AddMetall(int value)
	{
		currentCount += value;
        UIManager.UpdateMetallCount(currentCount);
    }
}
