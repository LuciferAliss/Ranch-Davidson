using Godot;
using System;
using System.Threading.Tasks;

public partial class Level : Node2D
{
	DayNightCycleComponent dayNightCycleComponent;
	Player player;

	public async override void _Ready()
	{
		Effect.Instance.PlayEffect("VignetteEffect_Close");
		GameStart.Instance.EmitSignal(nameof(GameStart.SignalGameStart), true);

		player = GetNode<Player>("Player");
		await Task.Delay(1500);
		player.cooldown = false;
		dayNightCycleComponent = GetNode<DayNightCycleComponent>("DayNightCycleComponent");
		SaveGameManager.Instance.LoadGame();
	}

	public override void _Process(double delta)
	{
	}

    public override void _ExitTree()
    {
        GameStart.Instance.EmitSignal(nameof(GameStart.SignalGameStart), false);
		dayNightCycleComponent = null;
    }
}