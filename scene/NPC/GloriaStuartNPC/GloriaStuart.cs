using Godot;
using DialogueManagerRuntime;

namespace Helpers
{
    public partial class GloriaStuart : BasicNpc
    {
        [Export] public Resource dialogueResponse;
        [Export] public string dialogueStart = "start";
        private bool acquaintance = false;

        public override void StartDialogue()
        {
            Signals.Instance.EmitSignalCheckAcquaintance(acquaintance);
            DialogueManager.ShowDialogueBalloon(dialogueResponse, dialogueStart);
            acquaintance = true;
        }
    }
}