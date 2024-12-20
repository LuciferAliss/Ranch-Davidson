using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NPCData;

public partial class Level : Node2D
{
	DayNightCycleComponent dayNightCycleComponent;
	Player player;
	List<BasicNpc> npcs;

	public async override void _Ready()
	{
		dayNightCycleComponent = GetNode<DayNightCycleComponent>("DayNightCycleComponent");
		player = GetNode<Player>("Player");
		
		if (GameStart.Instance.newGame)
		{
			NewGame();
		}
		else
		{
			ContinuationGame();
		}

		
		player.uIManager.Hide();


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
				DataNpc dataNpc = Instance.npcData[npc.Name];
				npc.GlobalPosition = dataNpc.globalPosition;
				npc.acquaintance = dataNpc.acquaintance;
				npc.questNPCs = dataNpc.questNPCs;
			}
		}
	}

	private void GetNPCOnScene()
	{
		npcs = GetTree().GetNodesInGroup("NPC").Select(u => (BasicNpc)u).ToList();
	}

	private void NewGame()
	{
		player.accessTools = false;
		player.uIManager.hud.UpdateStateTools();
	}

	private void ContinuationGame()
	{
		SaveGameManager.Instance.LoadGame();
		GetNPCOnScene();
		LoadGameResource();
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