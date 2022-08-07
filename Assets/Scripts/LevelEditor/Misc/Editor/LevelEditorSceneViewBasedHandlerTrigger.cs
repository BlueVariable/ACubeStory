using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [InitializeOnLoad]
    public static class LevelEditorSceneViewBasedHandlerTrigger
    {
        private static readonly List<LevelEditorHandlerBase> _handlers = new List<LevelEditorHandlerBase>()
        {
            new LevelEditorHandler_Activation(),
        };

        static LevelEditorSceneViewBasedHandlerTrigger()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private static void OnSceneGUI(SceneView sceneView)
        {
            if (LevelEditorContainer.Instance == null)
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