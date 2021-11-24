using BLUE.DialogueSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Dialogue
{
    [CreateAssetMenu(fileName = "SO_SSD", menuName = "BLUE/Dialogue/Create SO_SSD", order = 1)]
    public class DialogueDataProvider_SO_SSD : 
        DialogueDataProvider_SO<ESpeakerDialogue_SSD, DialogueData_SimpleSpeakerDialogue<ESpeaker_SSD>>
    {
    }
}