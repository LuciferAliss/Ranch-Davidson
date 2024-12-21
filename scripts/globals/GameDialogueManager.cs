using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using static NPCData;

public partial class GameDialogueManager : Node
{
	[Signal]
	public delegate void AllowMovePlayerEventHandler();
	[Signal]
	public delegate void ActivationToolsEventHandler();
	public static GameDialogueManager Instance { get; private set; }
	public bool acquaintance = false;
	public List<QuestNPC> questNPC = new();
	public int questNPCState;
	public int CountQuest = 0;
	public int i = 0;
	public bool valueForQuest = false;
	BasicNpc thisNpc;


	public override void _Ready()
	{
		Instance = this;
		Signals.Instance.InfNPC += GetInfNPC;
	}

    public override void _ExitTree()
    {
        Signals.Instance.InfNPC -= GetInfNPC;
    }

    public override void _Process(double delta)
	{
	}

    public void GetInfNPC(BasicNpc npc)
    {
		thisNpc = npc;
		acquaintance = thisNpc.acquaintance;
		questNPC = thisNpc.questNPCs.ToList();
		CountQuest = questNPC.Count;
		questNPCState = (int)questNPC[0].stateQuest;
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

	public void ChangeState()
	{
		questNPCState = (int)questNPC[i].stateQuest;
	}

	public void UpdateStateQuest(int state)
	{
		thisNpc.UpdateStateQuest(questNPC[i].nameQuest, state);
	}

	public void CheckItem(int index, int count)
	{
		Signals.Instance.EmitSignalSearchItem(index, count);
		GD.Print(valueForQuest);
	}

	public void ClearItem(int index, int count)
	{
		Signals.Instance.EmitSignalClearItem(index, count);
	}
}