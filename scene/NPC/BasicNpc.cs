using Godot;
using System;

public partial class BasicNpc : Node2D
{
    protected Control interactableLable;
    InteractableComponent interactableComponent;

    public override void _Ready()
    {
        interactableComponent = GetNode<InteractableComponent>("InteractableComponent");
        interactableLable = GetNode<Control>("InteractableLabelComponent");

        interactableComponent.InteractableActivated += OnInteractableActivated;
        interactableComponent.InteractableDeactivated += OnInteractableDeactivated;

        interactableLable.Hide();
    }

    private void OnInteractableActivated()
    {
        interactableLable.Show();
    }

    private void OnInteractableDeactivated()
    {
        interactableLable.Hide();
    }
}
