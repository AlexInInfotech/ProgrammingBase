using Godot;

public static class NodeExtensions
{
    public static T GetParentOfType<T>(this Node node) where T : class
    {
        Node current = node.GetParent();

        while (current != null)
        {
            if (current is T typedParent)
            {
                return typedParent;
            }
            current = current.GetParent();
        }

        return null;
    }
}