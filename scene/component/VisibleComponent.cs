using Godot;
using System;

public partial class VisibleComponent : Area2D
{
	Sprite2D sprite;

	public override void _Ready()
	{
		sprite = GetParent() as Sprite2D;
	}

	public override void _Process(double delta)
	{
	}

	void OnInvisibility(Node2D body)
	{
		Color color = new Color(1, 1, 1, 0.4f);
		sprite.Modulate = color;
	}

	void OffInvisibility(Node2D body)
	{
		Color color = new Color(1, 1, 1, 1);
		sprite.Modulate = color;
	}
}
