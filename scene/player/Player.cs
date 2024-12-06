using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	float maxSpeed = 100;
	private AnimatedSprite2D animatedSp;
	private AnimationPlayer animationPl;
	private StateAnimationPlayer stateAnimation;
	private StateActionPlayer stateAction;
	private bool useAction = false;
	public string curentTools { get; private set; } = ItemsName.ToolNames[0].ToString();
	ToolsPanel hud;

	public override void _Ready()
	{
		animatedSp = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animationPl = GetNode<AnimationPlayer>("AnimationPlayer");
		hud = GetNode<ToolsPanel>("HUD/MarginContainer/ToolsPanel");
		stateAnimation = new DownState(this);

		animationPl.AnimationFinished += FinishedAnimation;
		hud.SetPlayer(this);
	}

	public override void _Process(double delta)
	{
		if (!useAction)
		{
			Move();
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				var mousePosition = GetViewport().GetMousePosition();
				if (hud.GetGlobalRect().HasPoint(mousePosition))
				{
					return;
				}
				else if (!useAction)
				{
					Action();
				}
			}
		}
	}

	private Vector2 Movement_vector()
	{
		float movement_x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		float movement_y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
		return new Vector2(movement_x, movement_y);
	}

	private void Move()
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
			ChangeStateAnimation(new DiagonallyDownState(this)); 
		}
		else if (direction.X != 0 && direction.Y < 0)
		{
			ChangeStateAnimation(new DiagonallyUpState(this)); 
		}
		else if (direction.X != 0 && direction.Y == 0)
		{
			ChangeStateAnimation(new SideState(this)); 
		}
		else if (direction.X == 0 && direction.Y > 0)
		{
			ChangeStateAnimation(new DownState(this)); 
			animatedSp.FlipH = false;
		}
		else if (direction.X == 0 && direction.Y < 0)
		{
			ChangeStateAnimation(new UpState(this)); 
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

	private void Action()
	{

		if (curentTools == "None")
		{
			return;
		}
		else if (curentTools == "WateringCan")
		{
			ChangeStateAction(new StateWatering(this));
			stateAnimation.Watering();
			useAction = true;
			stateAction.Action();
		}
		else if (curentTools == "Axe")
		{
			ChangeStateAction(new StateCutting(this));
			stateAnimation.Cutting();
			useAction = true;
			stateAction.Action();
		}
	}
	
	public void ChangeAnimation(string nameAnimation)
	{
		animationPl.Play(nameAnimation);
	}

	public void ChangeStateAnimation(StateAnimationPlayer state)
	{
		stateAnimation = state;
	}

	public void ChangeStateAction(StateActionPlayer state)
	{
		stateAction = state;
	}

	public void FinishedAnimation(StringName NameAnime)
	{
		useAction = false;
	}

	public void ChangeTools(string tools)
	{
		curentTools = tools;
	}
}
