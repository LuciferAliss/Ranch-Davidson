using System.ComponentModel;
using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	float maxSpeed = 100;
	public AnimatedSprite2D animatedSp { get; private set; }
	public AnimationPlayer animationPl { get; private set; }
	private StateAnimationPlayer stateAnimation;
	private StateActionPlayer stateAction;
	private bool useAction = false;
	public string currentTools { get; private set; } = ItemsName.ToolNames[0].ToString();
	private ToolsPanel hud;
	public HitComponent hitComponent { get; private set; }
	public CollisionShape2D HitBox { get; private set; }
	private PauseMenu pauseMenu;
	private ColorRect background;
	public Settings settings;
	private bool Pause = false;

	public override void _Ready()
	{
		GD.Print("Я люблю альтушек");
		animatedSp = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animationPl = GetNode<AnimationPlayer>("AnimationPlayer");
		hud = GetNode<ToolsPanel>("UIPlayer/MarginContainer/ToolsPanel");
		HitBox = GetNode<CollisionShape2D>("HitComponent/CollisionShape2D");
		hitComponent = GetNode<HitComponent>("HitComponent");
		pauseMenu = GetNode<PauseMenu>("UIPlayer/MarginContainer/PanelContainer");
		background = GetNode<ColorRect>("UIPlayer/Beckground");
		settings = GetNode<Settings>("UIPlayer/MarginContainer/settings");

		HitBox.Disabled = true;
		HitBox.Position = new Vector2(0, 0);
		stateAnimation = new DownState(this);
		hitComponent.SetHitComponent(this);

		animationPl.AnimationFinished += FinishedAnimation;
		
		hud.SetPlayer(this);
		pauseMenu.SetPlayer(this);
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			ManagerPauseMenu();
		}
		else if (!useAction)
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

	private Vector2 MovementVector()
	{
		float movement_x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		float movement_y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
		return new Vector2(movement_x, movement_y);
	}

	private void Move()
	{
		Vector2 direction = MovementVector().Normalized();
		Vector2 velocity = maxSpeed * direction;

		if (direction.X > 0 && direction.Y == 0)
		{
			ChangeStateAnimation(new RightState(this)); 
		}
		else if (direction.X < 0 && direction.Y == 0)
		{
			ChangeStateAnimation(new LeftState(this)); 
		}
		else if (direction.X == 0 && direction.Y > 0)
		{
			ChangeStateAnimation(new DownState(this)); 
		}
		else if (direction.X == 0 && direction.Y < 0)
		{
			ChangeStateAnimation(new UpState(this)); 
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

		if (currentTools == "None")
		{
			return;
		}

		else if (currentTools == "WateringCan")
		{
			ChangeStateAction(new StateWatering(this));
			stateAnimation.Watering();
			useAction = true;
			stateAction.Action();
		}
		else if (currentTools == "Axe")
		{
			ChangeStateAction(new StateCutting(this));
			stateAnimation.Cutting();
			useAction = true;
			stateAction.Action();
		}
		else if (currentTools == "Hoe")
		{
			ChangeStateAction(new StateTilling(this));
			stateAnimation.Tilling();
			useAction = true;
			stateAction.Action();
		}

		HitBox.Disabled = false;
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
		HitBox.Disabled = true;
	}

	public void ChangeTools(string tools)
	{
		currentTools = tools;
	}

	public void ManagerPauseMenu()
	{
		if (Pause)
		{
			pauseMenu.Hide();
			background.Hide();
			settings.Hide();
			hud.Show();
			Engine.TimeScale = 1;
		}
		else 
		{
			pauseMenu.Show();
			background.Show();
			hud.Hide();
			Engine.TimeScale = 0;
		}

		Pause = !Pause;
	}
}
