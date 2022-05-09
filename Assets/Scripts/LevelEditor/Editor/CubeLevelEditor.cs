using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [InitializeOnLoad]
    public static class CubeLevelEditor
    {
        private static List<CubeLevelEditorHandlerBase> _handlers = new List<CubeLevelEditorHandlerBase>()
        {
            new CubeLevelEditorHandler_Activation(),
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
            
            foreach (CubeLevelEditorHandlerBase handler in _handlers)
            {
                handler.Handle(e);
            }
        }
    }
}