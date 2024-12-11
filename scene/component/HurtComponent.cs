using Godot;

public partial class HurtComponent : Area2D
{
	[Export]
	public string tool { get; private set; } = ItemsName.ToolNames[0];
	[Signal]
	public delegate void HurtEventHandler(int Damage);

	public override void _Ready()
	{
		if (!IsConnected("area_entered", new Callable(this, nameof(ActionPlayer))))
        {
            Connect("area_entered", new Callable(this, nameof(ActionPlayer)));
        }
	}

	public override void _Process(double delta)
	{
	}	

	private void ActionPlayer(Area2D Hit)
	{
		var HitComponent = Hit as HitComponent;
		if (tool == HitComponent.tool)
		{
			EmitSignal(nameof(Hurt), HitComponent.HitDamage);
		}
	}
}