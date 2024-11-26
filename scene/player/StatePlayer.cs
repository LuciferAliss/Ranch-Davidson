using Godot;
using System;

public abstract class StateAnimationPlayer
{
	protected Player player;

	public StateAnimationPlayer(Player player) 
	{
		this.player = player;
	}

	public abstract void Move();
    public abstract void Idle();
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
}

class UpState : StateAnimationPlayer
{
	public UpState(Player player) : base(player) { }
    public override void Move()
    {
        player.ChangeAnimation("WalkUp");
	}

    public override void Idle()
    {
        player.ChangeAnimation("IdleUp");
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
}