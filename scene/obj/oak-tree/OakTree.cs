using Godot;
using System;

public partial class OakTree : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void OnInvisibility(Node2D body)
	{
		Color color = new Color(1, 1, 1, 0.7f);
		this.Modulate = color;
	}

	void OffInvisibility(Node2D body)
	{
		Color color = new Color(1, 1, 1, 1);
		this.Modulate = color;
	}
}
