using Godot;

public partial class GameDialogueManager : Node
{
	[Signal]
	public delegate void AllowMovePlayerEventHandler();
	[Signal]
	public delegate void ActivationToolsEventHandler();
	public static GameDialogueManager Instance { get; private set; }
	public bool acquaintance = false;

	public override void _Ready()
	{
		Instance = this;
		Signals.Instance.CheckAcquaintance += SetAcquaintance;
	}

	public override void _Process(double delta)
	{
	}

    public void SetAcquaintance(bool acquaintance)
    {
		this.acquaintance = acquaintance; 
		GD.Print(this.acquaintance);
	}

	public void Exit()
	{
		GetParent().QueueFree();
	}

	public void OnAllowMovePlayer()
	{
		EmitSignal(nameof(AllowMovePlayer));
	}

	public void OnActivationTools()
    {
		EmitSignal(nameof(ActivationTools));
	}
}