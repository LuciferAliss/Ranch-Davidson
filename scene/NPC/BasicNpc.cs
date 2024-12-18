using Godot;
using System;

public partial class BasicNpc : Node2D
{
    PackedScene balloonScene;
    protected Control interactableLable;
    InteractableComponent interactableComponent;
    bool isRange = false;

    public override void _Ready()
    {
        interactableComponent = GetNode<InteractableComponent>("InteractableComponent");
        interactableLable = GetNode<Control>("InteractableLabelComponent");
        balloonScene = GD.Load<PackedScene>("res://dialogue/GameDialogueBalloon.tscn");

        interactableComponent.InteractableActivated += OnInteractableActivated;
        interactableComponent.InteractableDeactivated += OnInteractableDeactivated;

        interactableLable.Hide();
    }

    private void OnInteractableActivated()
    {
        interactableLable.Show();
        isRange = true;
    }

    private void OnInteractableDeactivated()
    {
        interactableLable.Hide();
        isRange = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("Interaction") && isRange)
        {
            BaseGameDialogueBalloon balloon = (BaseGameDialogueBalloon)balloonScene.Instantiate();
            GetTree().CurrentScene.AddChild(balloon);
            balloon.Start(GD.Load("res://dialogue/conversations/GloriaStuart.dialogue"), "start");
        }
    }
}
