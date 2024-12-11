using Godot;
using System;

public partial class Level : Node2D
{
	DayNightCycleComponent dayNightCycleComponent;
	public override void _Ready()
	{
		GameStart.Instance.EmitSignal(nameof(GameStart.SignalGameStart), true);
		dayNightCycleComponent = GetNode<DayNightCycleComponent>("DayNightCycleComponent");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _ExitTree()
    {
        GameStart.Instance.EmitSignal(nameof(GameStart.SignalGameStart), false);
		dayNightCycleComponent = null;
    }
}