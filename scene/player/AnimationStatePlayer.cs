using Godot;

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
    public abstract void Tilling();
}

class DownState : StateAnimationPlayer
{
	public DownState(Player player) : base(player) { }
    public override void Move()
    {
        player.animationPl.Play("WalkDown");
        player.animatedSp.FlipH = false;
	}

    public override void Idle()
    {
        player.animationPl.Play("IdleDown");
    }

    public override void Cutting()
    {
        player.HitBox.Position = new Vector2(0, 0);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("CuttingDown");
    }

    public override void Watering()
    {
        player.HitBox.Position = new Vector2(-6, 3);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("WateringDown");
    }

    public override void Tilling()
    {
        player.HitBox.Position = new Vector2(-6, 3);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("TillingDown");
    }
}

class UpState : StateAnimationPlayer
{
	public UpState(Player player) : base(player) { }
    public override void Move()
    {
        player.animationPl.Play("WalkUp");
        player.animatedSp.FlipH = false;
	}

    public override void Idle()
    {
        player.animationPl.Play("IdleUp");
    }

    public override void Cutting()
    {
        player.HitBox.Position = new Vector2(0, -19);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("CuttingUp");
    }

    public override void Watering()
    {
        player.HitBox.Position = new Vector2(6, -3);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("WateringUp");
    }

    public override void Tilling()
    {
        player.HitBox.Position = new Vector2(6, -3);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("TillingUp");
    }
}

class RightState : StateAnimationPlayer
{
	public RightState(Player player) : base(player) { }
    public override void Move()
    {
        player.animationPl.Play("WalkSide");
        player.animatedSp.FlipH = false;
	}

    public override void Idle()
    {
        player.animationPl.Play("IdleSide");
        player.animatedSp.FlipH = false;
    }

    public override void Cutting()
    {
        player.HitBox.Position = new Vector2(11, -14);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("CuttingSide");
    }

    public override void Watering()
    {
        player.HitBox.Position = new Vector2(16, 0);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("WateringSide");
    }

    public override void Tilling()
    {
        player.HitBox.Position = new Vector2(16, 0);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("TillingSide");
    }
}

class LeftState : StateAnimationPlayer
{
    public LeftState(Player player) : base(player) { }
    public override void Move()
    {
        player.animationPl.Play("WalkSide");
        player.animatedSp.FlipH = true ;
	}

    public override void Idle()
    {
        player.animationPl.Play("IdleSide");
        player.animatedSp.FlipH = true ;
    }

    public override void Cutting()
    {
        player.HitBox.Position = new Vector2(-11, -14);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("CuttingSide");
    }

    public override void Watering()
    {
        player.HitBox.Position = new Vector2(-16, 0);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("WateringSide");
    }

    public override void Tilling()
    {
        player.HitBox.Position = new Vector2(-16, 0);
        player.HitBox.Scale = new Vector2(0.6f, 0.6f);
        player.animationPl.Play("TillingSide");
    }
}