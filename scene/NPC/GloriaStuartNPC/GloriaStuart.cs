using Godot;
using DialogueManagerRuntime;

public partial class GloriaStuart : BasicNpc
{
    [Export] public Resource dialogueResponse;
    [Export] public string dialogueStart = "start";
    public bool acquaintance = false;

    public override void StartDialogue()
    {
        DialogueManager.ShowDialogueBalloon(dialogueResponse, dialogueStart);
    }
}