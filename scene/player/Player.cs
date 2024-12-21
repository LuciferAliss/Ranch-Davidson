using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	private float maxSpeed = 100;
	public int Satiety = 100;
	Vector2 direction;
	public bool accessTools = false; 
	private bool Pause = false;
	private bool useAction = false;
	private bool Inventory = false;
	public string currentTools { get; private set; } = ItemsName.ToolNames[0].ToString();
	public bool cooldown = true;
	public Camera2D camera;
	public AnimatedSprite2D animatedSp { get; private set; }
	public AnimationPlayer animationPl { get; private set; }
	private StateAnimationPlayer stateAnimation;
	private StateActionPlayer stateAction;
	public UIManager uIManager;
	public HitComponent hitComponent { get; private set; }
	public CollisionShape2D HitBox { get; private set; }
	public bool OpportunityDialogue = false;
	public BasicNpc npc; 

	public override void _Ready()
	{
		animatedSp = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animationPl = GetNode<AnimationPlayer>("AnimationPlayer");
		HitBox = GetNode<CollisionShape2D>("HitComponent/CollisionShape2D");
		hitComponent = GetNode<HitComponent>("HitComponent");
		uIManager = GetNode<UIManager>("UIPlayer");
		camera = GetNode<Camera2D>("Camera2D");

		HitBox.Disabled = true;
		HitBox.Position = new Vector2(0, 0);
		stateAnimation = new DownState(this);
		hitComponent.SetHitComponent(this);

		animationPl.AnimationFinished += FinishedAnimation;
		uIManager.inventory.UseItem += UsedItem;
		GameDialogueManager.Instance.AllowMovePlayer += OnAllowMovePlayer;
		
		uIManager.hungryBar.SetPlayer(this);
		uIManager.hud.SetPlayer(this);
		uIManager.pauseMenu.SetPlayer(this);
	}

	public override void _Process(double delta)
	{
		if (!cooldown)
		{
			if (!useAction)
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
		
		if (Input.IsActionJustPressed("Interaction"))
		{
			OpportunityDialogue = true;
		}

		if (OpportunityDialogue && npc != null)
		{
			npc.StartDialogue();
			direction = new Vector2(0, 0);
		}
		else if (Input.IsActionJustPressed("pause") && !uIManager.inventoryVisible && !cooldown)
		{
			uIManager.PauseVisibility();
		}
		else if (Input.IsActionJustPressed("inventory") && !uIManager.pauseVisible && !cooldown)
		{
			uIManager.InventoryVisibility();
		}
		else
		{
			direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		}
		
	}

	public void OpportunityForDialogue(bool Opportunity, BasicNpc npc)
	{
		this.npc = npc;
	}

	private void Move()
	{
		Vector2 velocity = maxSpeed * direction.Normalized();

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
		UserData.Instance.NumberActions += 1;
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
		else if (currentTools == "WheatSeeds")
		{
			ChangeStateAction(new StateSeeds(this));
			stateAnimation.Idle();
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
		cooldown = false;
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
				uIManager.hungryBar.UpdateHungryBar();
				break;
		}
	}

	public void OnAllowMovePlayer()
	{
		OpportunityDialogue = false;
	}
}
