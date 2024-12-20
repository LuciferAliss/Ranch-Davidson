using Godot;

partial class Signals : Node
{
    [Signal] public delegate void InfNPCEventHandler(BasicNpc npc);
    
    public static Signals Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void EmitSignalInfNPC(BasicNpc npc)
    {
        EmitSignal(nameof(InfNPC), npc);
    }
}