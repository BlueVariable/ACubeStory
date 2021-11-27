using System;
using BLUE.DialogueSystem;

namespace BLUE.ACubeStory.Dialogue
{
    public class DialoguePlayer_SimpleSpeaker : DialoguePlayer_SimpleSpeakerBase<ESimpleSpeakerDialogue, ESimpleSpeaker>
    {
        private static DialoguePlayer_SimpleSpeaker _instance;
        public static DialoguePlayer_SimpleSpeaker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DialoguePlayer_SimpleSpeaker>();
                }

                return _instance;
            }
        }
    }
    
    [Serializable]
    public class DialogueData_SimpleSpeaker : DialogueData_SimpleSpeaker<ESimpleSpeaker>
    {
    }
    
    public enum ESimpleSpeaker
    {
        None = 0,
        
        Narrative = 1,
        
        Cube = 2,
        
        Tutorial = 3,
    }
    
    public enum ESimpleSpeakerDialogue
    {
        None = 0,
        
        ACubeStory = 1,
        
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
        
        Cube_Line_Fall_01 = 3001,
        Cube_Line_Fall_02 = 3002,
        Cube_Line_Fall_03 = 3003,
        Cube_Line_Fall_04 = 3004,
        
        Cube_Line_MergePuzzle_Undo_01 = 4001,
        Cube_Line_MergePuzzle_Undo_02 = 4002,
        Cube_Line_MergePuzzle_Undo_03 = 4003,
        Cube_Line_MergePuzzle_Undo_04 = 4004,
        
        Cube_Line_MergePuzzle_TryLeave_01 = 5001,
        Cube_Line_MergePuzzle_TryLeave_02 = 5002,
        
        Tutorial_Roll_01 = 60001,
        Tutorial_AvoidFallingToVoid_01 = 60002,
        Tutorial_FallToVoid_01 = 60003,
        Tutorial_MainMenu_Select_01 = 60004,
        
    }
}