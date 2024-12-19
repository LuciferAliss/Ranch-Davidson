using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class Level : Node2D
{
	DayNightCycleComponent dayNightCycleComponent;
	Player player;
	List<BasicNpc> npcs;

	public async override void _Ready()
	{
		SaveGameManager.Instance.LoadGame();
		dayNightCycleComponent = GetNode<DayNightCycleComponent>("DayNightCycleComponent");
		player = GetNode<Player>("Player");
		player.uIManager.Hide();

		LoadGameResource();

		Effect.Instance.PlayEffect("VignetteEffect_Close");
		GameStart.Instance.EmitSignal(nameof(GameStart.SignalGameStart), true);

		await Task.Delay(1500);
		player.uIManager.Show();
		player.camera.PositionSmoothingEnabled = true;
		player.cooldown = false;
	}

	private void LoadGameResource()
	{
		player.camera.PositionSmoothingEnabled = false;
		player.GlobalPosition = UserData.Instance.playerPosition;
		player.Satiety = UserData.Instance.satiety;
		player.accessTools = UserData.Instance.accessTools;
		player.uIManager.hud.UpdateStateTools();
		player.uIManager.inventory.inventory.slots = UserData.Instance.inventory;
		player.uIManager.inventory.UpdateInventory();
		player.uIManager.hungryBar.UpdateHungryBar();
		dayNightCycleComponent.SetTimeWorld(UserData.Instance.timeWorld);

		if (npcs != null)
		{
			foreach (var npc in npcs)
			{
				DataNpc dataNpc = NPCData.Instance.npcData[npc.Name];
				npc.GlobalPosition = dataNpc.globalPosition;
				npc.acquaintance = dataNpc.acquaintance;
			}
		}
	}

	private void GetNPCOnScene()
	{
		npcs = GetTree().GetNodesInGroup("NPC").Select(u => (BasicNpc)u).ToList();
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