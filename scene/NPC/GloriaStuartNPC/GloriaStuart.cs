using Godot;
using DialogueManagerRuntime;
using System.Collections.Generic;
using static NPCData;

namespace Helpers
{
    public partial class GloriaStuart : BasicNpc
    {
        [Export] public Resource dialogueResponse;
        [Export] public string dialogueStart = "start";

        public override void _Ready()
        {
            base._Ready();

            questNPCs = new List<QuestNPC>
            {
                new QuestNPC("Collect 10 logs", StateQuest.NotTaken),
                new QuestNPC("Collect 20 logs and 30 apples", StateQuest.NotTaken),
                new QuestNPC("Collect 10 logs, 14 apples and 10 wheat", StateQuest.NotTaken)
            };
        }

        public override void StartDialogue()
        {
            Signals.Instance.EmitSignalInfNPC(this);
            DialogueManager.ShowDialogueBalloon(dialogueResponse, dialogueStart);
            acquaintance = true;
        }

        public override void UpdateStateQuest(string name, int state)
        {
            for (int i = 0; i < questNPCs.Count; i++)
            {
                if (questNPCs[i].nameQuest == name)
                {
                    questNPCs[i].stateQuest = (StateQuest)state;
                    GD.Print((StateQuest)state);
                    break;
                }
            }
            
        }
    }
}