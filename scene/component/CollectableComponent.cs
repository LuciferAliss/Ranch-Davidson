using Godot;
using System;

public partial class CollectableComponent : Area2D
{
	[Export]
	string collectableName;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	private void SearchPlayer(Node2D body)
	{
		GetParent().QueueFree();
	}
}
