using UnityEngine;
using UnityEditor;

namespace BLUE.ACubeStory.Helper
{
    [InitializeOnLoad]
    public class SceneViewCameraFollowHelper
    {
        private static Transform _targetCameraTransform;
        
        private static bool _isFollowing = false;

        static SceneViewCameraFollowHelper()
        {
            EditorApplication.update -= CameraUpdate;
            EditorApplication.update += CameraUpdate;
        }
        
        [MenuItem("BLUE/Scene/Toggle Camera Follower #t")]
        private static void ToggleCameraFollower()
        {
            _isFollowing = !_isFollowing;

            if (_isFollowing)
            {
                GameObject cameraGameObject = GameObject.Find("SceneViewCameraFollowHelper");

                if (cameraGameObject == null)
                {
                    Debug.Log("Couldn't find \\\"SceneViewCameraFollowHelper\\\" object.");

                    return;
                }
                
                _targetCameraTransform = cameraGameObject.transform;
            }
        }

        private static void CameraUpdate()
        {
            if (!_isFollowing)
            {
                return;
            }

            SceneView sceneView = SceneView.lastActiveSceneView;

            if (sceneView == null || 
                sceneView.camera == null)
            {
                return;
            }

            if (_targetCameraTransform == null)
            {
                return;
            }
            
            Camera sceneCam = sceneView.camera;
            
            Vector3 direction = (sceneCam.transform.position - sceneView.pivot).normalized;

            _targetCameraTransform.transform.position = sceneView.pivot + (direction * sceneView.cameraDistance);
        }
    }
}