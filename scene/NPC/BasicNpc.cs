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
        balloonScene = GD.Load<PackedScene>("res://dialogue/GameDialogueBalloon.tscn");

        interactableComponent.InteractableActivated += OnInteractableActivated;
        interactableComponent.InteractableDeactivated += OnInteractableDeactivated;

        interactableLable.Hide();
    }

    protected void OnInteractableActivated()
    {
        interactableLable.Show();
        isRange = true;
    }

    protected void OnInteractableDeactivated()
    {
        interactableLable.Hide();
        isRange = false;
    }
}
