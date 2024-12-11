using Godot;
using System;

public partial class CollectableComponent : Area2D
{
	[Signal]
    public delegate void TakeItemEventHandler(InventoryItem item);
	[Export]
	InventoryItem item;
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	private void SearchPlayer(Node2D body)
	{
		GetParent().QueueFree();
		var player = body as Player;
		player.uIManager.inventory.ConnectCollectable(this);
		EmitSignal(nameof(TakeItem), item);
	}
}