using Godot;

partial class Signals : Node
{
    [Signal] public delegate void CheckAcquaintanceEventHandler(bool acquaintance); 
    public static Signals Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void EmitSignalCheckAcquaintance(bool acquaintance)
    {
        EmitSignal(nameof(CheckAcquaintance), acquaintance);
    }
}