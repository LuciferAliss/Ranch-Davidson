public abstract class StateAnimationPlayer
{
	protected Player player;

	public StateAnimationPlayer(Player player) 
	{
		this.player = player;
	}

	public abstract void Move();
    public abstract void Idle();
    public abstract void Watering();
    public abstract void Cutting();
}

class DownState : StateAnimationPlayer
{
	public DownState(Player player) : base(player) { }
    public override void Move()
    {
        player.ChangeAnimation("WalkDown");
	}

    public override void Idle()
    {
        player.ChangeAnimation("IdleDown");
    }

    public override void Watering()
    {
        player.ChangeAnimation("WateringDown");
    }

    public override void Cutting()
    {
        player.ChangeAnimation("CuttingDown");
    }
}

class UpState : StateAnimationPlayer{
	public UpState(Player player) : base(player) { }
    public override void Move()
    {
        player.ChangeAnimation("WalkUp");
	}

    public override void Idle()
    {
        player.ChangeAnimation("IdleUp");
    }

    public override void Watering()
    {
        player.ChangeAnimation("WateringUp");
    }

    public override void Cutting()
    {
        player.ChangeAnimation("CuttingUp");
    }
}

class SideState : StateAnimationPlayer
{
	public SideState(Player player) : base(player) { }
    public override void Move()
    {
        player.ChangeAnimation("WalkSide");
	}

    public override void Idle()
    {
        player.ChangeAnimation("IdleSide");
    }

    public override void Watering()
    {
        player.ChangeAnimation("WateringSide");
    }

    public override void Cutting()
    {
        player.ChangeAnimation("CuttingSide");
    }
}

class DiagonallyDownState : StateAnimationPlayer
{
	public DiagonallyDownState(Player player) : base(player) { }
    public override void Move()
    {
        player.ChangeAnimation("WalkDiagonallyDown");
	}

    public override void Idle()
    {
        player.ChangeAnimation("IdleDiagonallyDown");
    }

    public override void Watering()
    {
        player.ChangeAnimation("WateringDiagonallyDown");
    }

    public override void Cutting()
    {
        player.ChangeAnimation("CuttingDiagonallyDown");
    }
}

class DiagonallyUpState : StateAnimationPlayer
{
	public DiagonallyUpState(Player player) : base(player) { }
    public override void Move()
    {
        player.ChangeAnimation("WalkDiagonallyUp");
	}

    public override void Idle()
    {
        player.ChangeAnimation("IdleDiagonallyUp");
    }

    public override void Watering()
    {
        player.ChangeAnimation("WateringDiagonallyUp");
    }

    public override void Cutting()
    {
        player.ChangeAnimation("CuttingDiagonallyUp");
    }
}