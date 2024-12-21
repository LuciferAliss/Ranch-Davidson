using Godot;

partial class Signals : Node
{
    [Signal] public delegate void InfNPCEventHandler(BasicNpc npc);
    [Signal] public delegate int SearchItemEventHandler(int index, int count);
    [Signal] public delegate void ClearItemEventHandler(int index, int count);

    public static Signals Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void EmitSignalInfNPC(BasicNpc npc)
    {
        EmitSignal(nameof(InfNPC), npc);
    }

    public void EmitSignalSearchItem(int index, int count)
    {
        EmitSignal(nameof(SearchItem), index, count);
    }

    public void EmitSignalClearItem(int index, int count)
    {
        EmitSignal(nameof(ClearItem), index, count);
    }
}