using Godot;
using System;
[GlobalClass]
public partial class Note : StaticBody3D, ICollectable, IReadable
{
	[Export]string name;
    [Export] string text;
    [Export]CanvasLayer layer;
    bool isOpen = false;

    public void Interact() {
        if (!isOpen)
            Open();
        else
            Close();
        isOpen = !isOpen;
    }
    public async void Open() {
        if(layer != null)
            layer.Visible = true;
        RichTextLabel label = layer.GetNode<RichTextLabel>("RichTextLabel");
        label.Text = text;
    }
    public void Close() { layer.Visible = false; Collect(); }
    public void Collect() { QueueFree(); }

}
