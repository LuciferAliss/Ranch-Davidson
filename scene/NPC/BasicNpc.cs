using DialogueManagerRuntime;
using Godot;
using System;

public partial class BasicNpc : Node2D
{
    protected PackedScene balloonScene;
    protected Control interactableLable;
    protected InteractableComponent interactableComponent;
    protected bool isRange = false;

    public override void _Ready()
    {
        interactableComponent = GetNode<InteractableComponent>("InteractableComponent");
        interactableLable = GetNode<Control>("InteractableLabelComponent");

        interactableComponent.InteractableActivated += OnInteractableActivated;
        interactableComponent.InteractableDeactivated += OnInteractableDeactivated;

        interactableLable.Hide();
    }

    protected void OnInteractableActivated(Player player)
    {
        interactableLable.Show();
        isRange = true;
        player.OpportunityForDialogue(isRange, this);
    }

    protected void OnInteractableDeactivated(Player player)
    {
        interactableLable.Hide();
        isRange = false;
        player.OpportunityForDialogue(isRange, null);
    }

    public virtual void StartDialogue()
    {
    }
}
