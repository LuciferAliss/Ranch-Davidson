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