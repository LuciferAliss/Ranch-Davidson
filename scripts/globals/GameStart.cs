using Godot;

partial class GameStart : Node
{
    [Signal]
    public delegate void SignalGameStartEventHandler(bool value);
    public static GameStart Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        EmitSignal(nameof(SignalGameStart), false);
    }
}  