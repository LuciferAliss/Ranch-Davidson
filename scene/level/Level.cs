using Godot;
using System.Threading.Tasks;

public partial class Level : Node2D
{
	DayNightCycleComponent dayNightCycleComponent;
	Player player;

	public async override void _Ready()
	{
		SaveGameManager.Instance.LoadGame();
		dayNightCycleComponent = GetNode<DayNightCycleComponent>("DayNightCycleComponent");
		player = GetNode<Player>("Player");
		player.uIManager.Hide();
		player.camera.PositionSmoothingEnabled = false;
		player.GlobalPosition = UserData.Instance.playerPosition;
		player.Satiety = UserData.Instance.satiety;
		player.uIManager.inventory.inventory.slots = UserData.Instance.inventory;
		player.uIManager.inventory.UpdateInventory();
		player.uIManager.hungryBar.UpdateHungryBar();
		dayNightCycleComponent.SetTimeWorld(UserData.Instance.timeWorld);
		GD.Print(UserData.Instance.satiety);
		Effect.Instance.PlayEffect("VignetteEffect_Close");
		GameStart.Instance.EmitSignal(nameof(GameStart.SignalGameStart), true);

		await Task.Delay(1500);
		player.uIManager.Show();
		player.camera.PositionSmoothingEnabled = true;
		player.cooldown = false;
	}

	public override void _Process(double delta)
	{
	}

    public override void _ExitTree()
    {
        GameStart.Instance.EmitSignal(nameof(GameStart.SignalGameStart), false);
		dayNightCycleComponent.QueueFree();
		player.QueueFree();
		QueueFree();
    }
}