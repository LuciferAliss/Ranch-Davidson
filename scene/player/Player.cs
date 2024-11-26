using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	float maxSpeed = 200;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		move();
	}

	private Vector2 Movement_vector()
	{
		float movement_x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		float movement_y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
		return new Vector2(movement_x, movement_y);
	}

	private void move()
	{
		Vector2 direction = Movement_vector().Normalized();
		Vector2 velocity = maxSpeed * direction;

		Velocity = velocity;
		MoveAndSlide();
	}
}
