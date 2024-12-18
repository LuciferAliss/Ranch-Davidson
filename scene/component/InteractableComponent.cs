using Godot;

public partial class InteractableComponent : Area2D
{
    [Signal]
    public delegate void InteractableActivatedEventHandler();
    [Signal]
    public delegate void InteractableDeactivatedEventHandler();

    private void PlayerEntered(Node2D body)
    {
        EmitSignal(nameof(InteractableActivated));
    }

    private void PlayerExit(Node body)
    {
        EmitSignal(nameof(InteractableDeactivated));
    }

}
