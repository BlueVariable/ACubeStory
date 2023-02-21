using BLUE.ACubeStory.Cube;
using UnityEngine;

public class CubeMothLandReference : MonoBehaviour
{
    #region Singleton

    private static CubeMothLandReference _instance;
    public static CubeMothLandReference Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CubeMothLandReference>();
            }

            return _instance;
        }
    }

    #endregion

    private readonly Vector3 _offset = new Vector3(-0.35f, 0.55f, 0.45f);
    
    private void Update()
    {
        transform.position = Cube.Instance.GetPosition() + _offset;
    }
}
