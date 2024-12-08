using Godot;
using System;

public partial class DamageComponent : Node2D
{
	[Export]
	int maxDamage = 1;
	int currentDamage = 0;

	[Signal]
	public delegate void MaxDamagedReachedEventHandler();

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void ApplyDamage(int damage)
	{
		currentDamage = Mathf.Clamp(currentDamage + damage, 0, maxDamage);

        if (currentDamage == maxDamage)
        {
            EmitSignal(nameof(MaxDamagedReached));
        }
	}
}