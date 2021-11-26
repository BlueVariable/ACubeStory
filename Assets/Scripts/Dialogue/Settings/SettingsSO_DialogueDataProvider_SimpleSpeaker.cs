using BLUE.DialogueSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Dialogue
{
    [CreateAssetMenu(fileName = "SettingsSO_DialogueDataProvider_SimpleSpeaker", 
        menuName = "Settings/Dialogue/Create SettingsSO_DialogueDataProvider_SimpleSpeaker")]
    public class SettingsSO_DialogueDataProvider_SimpleSpeaker : 
        SettingsSO_DialogueDataProviderBase<ESimpleSpeakerDialogue, DialogueData_SimpleSpeaker<ESimpleSpeaker>>
    {
    }
}