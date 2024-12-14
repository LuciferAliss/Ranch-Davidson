using System.ComponentModel;
using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	private float maxSpeed = 100;
	public int Satiety = 100;
	private bool Pause = false;
	private bool useAction = false;
	private bool Inventory = false;
	public string currentTools { get; private set; } = ItemsName.ToolNames[0].ToString();
	public bool ExitInMainMenu = true;
	public AnimatedSprite2D animatedSp { get; private set; }
	public AnimationPlayer animationPl { get; private set; }
	private StateAnimationPlayer stateAnimation;
	private StateActionPlayer stateAction;
	public UIManager uIManager;
	public HitComponent hitComponent { get; private set; }
	public CollisionShape2D HitBox { get; private set; }

	public override void _Ready()
	{
		animatedSp = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animationPl = GetNode<AnimationPlayer>("AnimationPlayer");
		HitBox = GetNode<CollisionShape2D>("HitComponent/CollisionShape2D");
		hitComponent = GetNode<HitComponent>("HitComponent");
		uIManager = GetNode<UIManager>("UIPlayer");

		HitBox.Disabled = true;
		HitBox.Position = new Vector2(0, 0);
		stateAnimation = new DownState(this);
		hitComponent.SetHitComponent(this);

		animationPl.AnimationFinished += FinishedAnimation;
		uIManager.inventory.UseItem += UsedItem;
		
		uIManager.hungryBar.SetPlayer(this);
		uIManager.hud.SetPlayer(this);
		uIManager.pauseMenu.SetPlayer(this);
	}

	public override void _Process(double delta)
	{
		if (!ExitInMainMenu)
		{
			if (Input.IsActionJustPressed("pause") && !uIManager.inventoryVisible)
			{
				uIManager.PauseVisibility();
			}
			else if (Input.IsActionJustPressed("inventory") && !uIManager.pauseVisible)
			{
				uIManager.InventoryVisibility();
			}
			else if (!useAction)
			{
				Move();
			}
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				var mousePosition = GetViewport().GetMousePosition();
				if (uIManager.hud.GetGlobalRect().HasPoint(mousePosition))
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
		useAction = true;
		if (currentTools == "None")
		{
			ChangeStateAction(new StateNone(this));
			stateAnimation.Idle();
			stateAction.Action();
		}
		else if (currentTools == "WateringCan")
		{
			ChangeStateAction(new StateWatering(this));
			stateAnimation.Watering();
			stateAction.Action();
		}
		else if (currentTools == "Axe")
		{
			ChangeStateAction(new StateCutting(this));
			stateAnimation.Cutting();
			stateAction.Action();
		}
		else if (currentTools == "Hoe")
		{
			ChangeStateAction(new StateTilling(this));
			stateAnimation.Tilling();
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

	public void UsedItem(InventoryItem item)
	{
		switch(item.name)
		{
			case "Tomato":
				Satiety = Mathf.Min(100, Satiety + 10);
				uIManager.hungryBar.UpDateHungryBar();
				break;
		}
	}
}
