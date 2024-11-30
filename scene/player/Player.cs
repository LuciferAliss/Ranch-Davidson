using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	float maxSpeed = 100;
	private AnimatedSprite2D animatedSp;
	private AnimationPlayer animationPl;
	private StateAnimationPlayer stateAnimation;

	public override void _Ready()
	{
		animatedSp = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animationPl = GetNode<AnimationPlayer>("AnimationPlayer");
		
		stateAnimation = new DownState(this); 
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

		if(direction.X < 0)
		{
			animatedSp.FlipH = true;
		} 
		else if (direction.X > 0)
		{
			animatedSp.FlipH = false;
		}

		if (direction.X != 0 && direction.Y > 0)
		{
			stateAnimation = new DiagonallyDownState(this); 
		}
		else if (direction.X != 0 && direction.Y < 0)
		{
			stateAnimation = new DiagonallyUpState(this); 
		}
		else if (direction.X != 0 && direction.Y == 0)
		{
			stateAnimation = new SideState(this); 
		}
		else if (direction.X == 0 && direction.Y > 0)
		{
			stateAnimation = new DownState(this); 
			animatedSp.FlipH = false;
		}
		else if (direction.X == 0 && direction.Y < 0)
		{
			stateAnimation = new UpState(this); 
			animatedSp.FlipH = false;
		}

		if (direction.X == 0 && direction.Y == 0)
		{
			stateAnimation.Idle();
		}
		else
		{
			stateAnimation.Move();
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}

	public void ChangeAnimation(string nameAnimation)
	{
		animationPl.Play(nameAnimation);
	}
}
