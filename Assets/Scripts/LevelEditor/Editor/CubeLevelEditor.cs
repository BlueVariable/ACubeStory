using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [InitializeOnLoad]
    public static class CubeLevelEditor
    {
        private static List<LevelEditorHandlerBase> _handlers = new List<LevelEditorHandlerBase>()
        {
            new LevelEditorHandler_Activation(),
        };

        static CubeLevelEditor()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private static void OnSceneGUI(SceneView sceneView)
        {
            if (CubeEditor.Instance == null)
            {
                return;
            }
        
            Event e = Event.current;
            
            foreach (LevelEditorHandlerBase handler in _handlers)
            {
                handler.Handle(e);
            }
        }
    }
}