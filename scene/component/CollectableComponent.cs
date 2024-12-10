using Godot;
using System;

public partial class CollectableComponent : Area2D
{
	[Signal]
	public delegate void TakeItemEventHandler(InventoryItem item);
	[Export]
	string collectableName;
	[Export]
	InventoryItem item;
	public static CollectableComponent Instance { get; private set; }
	
	public override void _Ready()
	{
		Instance = this;
	}

	public override void _Process(double delta)
	{
	}

	private void SearchPlayer(Node2D body)
	{
		GetParent().QueueFree();
		EmitSignal(nameof(TakeItem), item);
	}
}
