using UnityEngine;
using UnityEditor;

namespace BLUE.ACubeStory.Helper
{
    [InitializeOnLoad]
    public class SceneViewCameraFollower
    {
        private static Transform _targetCameraTransform;
        
        private static bool _isFollowing = false;

        static SceneViewCameraFollower()
        {
            EditorApplication.update -= CameraUpdate;
            EditorApplication.update += CameraUpdate;
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

        [MenuItem("BLUE/Scene/Toggle Camera Follower #t")]
        static void ToggleCameraFollower()
        {
            _isFollowing = !_isFollowing;

            if (_isFollowing)
            {
                GameObject cameraGameObject = GameObject.Find("SceneViewCameraFollower");

                if (cameraGameObject == null)
                {
                    Debug.Log("Couldn't find SceneViewCameraFollower gameObject.");

                    return;
                }
                
                _targetCameraTransform = cameraGameObject.transform;
            }
        }
    }
}