using Godot;

public partial class HitComponent : Area2D
{
	Player player;
	[Export]
	public string tool = ItemsName.ToolNames[0];
	public int HitDamage = 1;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void SetHitComponent(Player player)
	{
		this.player = player;
	}
}
