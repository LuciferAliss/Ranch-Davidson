using Godot;

public abstract class StateActionPlayer
{
	protected Player player;

	public StateActionPlayer(Player player) 
	{
		this.player = player;
	}

    public abstract void Action();
}

public class StateWatering : StateActionPlayer
{
    public StateWatering(Player player) : base(player) {}

    public override void Action()
    {
        GD.Print("Полил");
    }
}

public class StateCutting : StateActionPlayer
{
    public StateCutting(Player player) : base(player) {}

    public override void Action()
    {
        GD.Print("Рубил");
    }
}

public class StateTilling : StateActionPlayer
{
    public StateTilling(Player player) : base(player) {}

    public override void Action()
    {
        GD.Print("Культивировал");
    }
}

public class StateNone : StateActionPlayer
{
    public StateNone(Player player) : base(player) {}

    public override void Action()
    {
        player.HitBox.Position = new Vector2(0, -12);
        player.HitBox.Scale = new Vector2(2, 2);
        GD.Print("Руками");
    }
}

public class StateSeeds : StateActionPlayer
{
    public StateSeeds(Player player) : base(player) {}    
    public override void Action()
    {
        player.HitBox.Position = new Vector2(0, -12);
        player.HitBox.Scale = new Vector2(2, 2);
        GD.Print("Руками");
    }
}