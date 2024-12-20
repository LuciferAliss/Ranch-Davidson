using Godot;
using DialogueManagerRuntime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helpers
{
    public partial class GloriaStuart : BasicNpc
    {
        [Export] public Resource dialogueResponse;
        [Export] public string dialogueStart = "start";

        public override void _Ready()
        {
            tasks = new List<Tasks>
            {
                new Tasks("Bring a tree", StateTask.NotAccepted),
                new Tasks("Bring 10 wheat", StateTask.NotAccepted),
                new Tasks("Bring 25 wheat and 10 apples", StateTask.NotAccepted)
            };
        }

        public override void StartDialogue()
        {
            Signals.Instance.EmitSignalCheckAcquaintance(acquaintance);
            DialogueManager.ShowDialogueBalloon(dialogueResponse, dialogueStart);
            acquaintance = true;
        }
    }
}