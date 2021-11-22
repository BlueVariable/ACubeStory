using BLUE.DialogueSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Dialogue
{
    public class DialogueDataProvider_SOContainer_SSD : 
        DialogueDataProvider_SOContainer<ESpeakerDialogue_SSD, DialogueData_SimpleSpeakerDialogue<ESpeaker_SSD>>
    {
    }
    
    [CreateAssetMenu(fileName = "SO_SSD", menuName = "BLUE/Dialogue/Create SO_SSD", order = 1)]
    public class DialogueDataProvider_SO_SimpleSpeakerDialogue_Sample : 
        DialogueDataProvider_SO<ESpeakerDialogue_SSD, DialogueData_SimpleSpeakerDialogue<ESpeaker_SSD>>
    {
    }
}