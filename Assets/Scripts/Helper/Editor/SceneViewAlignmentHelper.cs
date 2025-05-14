using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.Helper
{
    public class SceneViewAlignmentHelper : EditorWindow
    {
        [MenuItem("BLUE/Scene/Align Scene View #r")]
        private static void AlignView()
        {
            Vector3 rotationEuler = new Vector3(35, -135, 0);
            
            SceneView sceneView = SceneView.lastActiveSceneView;
            
            if (sceneView == null)
            {
                Debug.LogWarning("No active scene view found.");
                
                return;
            }

            Quaternion rotation = Quaternion.Euler(rotationEuler);

            sceneView.LookAt(
                sceneView.pivot,
                rotation,
                sceneView.cameraDistance,
                true);
            
            sceneView.Repaint();
        }
    }
}