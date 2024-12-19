using Godot;
using System;

public partial class GloriaStuart : BasicNpc
{
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
