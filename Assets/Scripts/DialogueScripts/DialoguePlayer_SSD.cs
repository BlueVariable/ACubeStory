using System;
using BLUE.DialogueSystem;

namespace BLUE.ACubeStory.Dialogue
{
    public class DialoguePlayer_SSD : DialoguePlayer_SimpleSpeakerDialogueBase<ESpeakerDialogue_SSD, ESpeaker_SSD>
    {
        private static DialoguePlayer_SSD _instance;

        public static DialoguePlayer_SSD Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DialoguePlayer_SSD>();
                }

                return _instance;
            }
        }
    }
    
    [Serializable]
    public class DialogueData_SSD : DialogueData_SimpleSpeakerDialogue<ESpeaker_SSD>
    {
    }
    
    public enum ESpeaker_SSD
    {
        None = 0,
        
        Narrative = 1,
        
        Cube = 2,
    }
    
    public enum ESpeakerDialogue_SSD
    {
        None = 0,
        
        Narrative_Level01_01 = 1001,
        Narrative_Level01_02 = 1002,
        Narrative_Level01_03 = 1003,
        Narrative_Level01_04 = 1004,
        Narrative_Level01_05 = 1005,
        Narrative_Level01_06 = 1006,
        
        Cube_Level01_01 = 2001,
        Cube_Level01_02 = 2002,
        Cube_Level01_03 = 2003,
        Cube_Level01_04 = 2004,
        Cube_Level01_05 = 2005,
        Cube_Level01_06 = 2006,
    }
}