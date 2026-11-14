using Godot;
using System;

public interface IReadable : IInterectable
{
	void Open() { }
	void Close() { }
}
