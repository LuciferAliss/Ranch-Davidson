using Godot;

public partial class InteractableComponent : Area2D
{
    [Signal]
    public delegate void InteractableActivatedEventHandler(Player player);
    [Signal]
    public delegate void InteractableDeactivatedEventHandler(Player player);

    private void PlayerEntered(Node2D body)
    {
        Player player = body as Player;
        EmitSignal(nameof(InteractableActivated), player);
    }

    private void PlayerExit(Node body)
    {
        Player player = body as Player; 
        EmitSignal(nameof(InteractableDeactivated), player);
    }

}
