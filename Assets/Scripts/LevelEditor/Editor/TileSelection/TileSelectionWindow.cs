using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileSelectionWindow : EditorWindow
    {
        static public List<GameObject> Tiles = new List<GameObject>();

        private PreviewRenderUtility _renderUtility = new PreviewRenderUtility();
        
        private int _selectedIndex = -1;
        
        [MenuItem("BLUE/Level Editor/Tile Selection Window")]
        public static void ShowWindow()
        {
            GetWindow<TileSelectionWindow>();
        }

        private void OnGUI()
        {
            Tiles.Clear();
            
            GUILayout.BeginVertical();

            Color color_default = GUI.backgroundColor;
            Color color_selected = new Color(0, 1, 1, 0.5f);
            
            GUIStyle itemStyle = new GUIStyle(GUI.skin.button);  //make a new GUIStyle
             
            itemStyle.alignment = TextAnchor.MiddleLeft; //align text to the left
            itemStyle.active.background = itemStyle.normal.background;  //gets rid of button click background style.
            itemStyle.margin = new RectOffset(25, 25, 5, 5);

            TileSettings[] settingsArr = TileSettingsContainer.Instance.TileSettingsSo.TileSettings;

            for (int i = 0; i < settingsArr.Length; i++)
            {
                GUI.backgroundColor = _selectedIndex== i ? color_selected : Color.clear;

                string itemName = settingsArr[i].DisplayName;
                
                if (GUILayout.Button(itemName, itemStyle)) 
                {
                    _selectedIndex = i;
                }
                
                GUI.backgroundColor = color_default;
            }
            
            if (_selectedIndex >= 0)
            {
                GameObject gameObject = TileSettingsContainer.Instance.TileSettingsSo.TileSettings[_selectedIndex].Tile.gameObject;

                Rect rect = new Rect(50, 50, 250, 250);

                // BeginDraw(rect);

                // DrawRenderPreview(gameObject);

                // EndDraw(rect);
            }
            
            GUILayout.EndVertical();
        }

        private void OnValidate()
        {
            _renderUtility.Cleanup();
        }

        private void OnDisable()
        {
            _renderUtility.Cleanup();
        }

        private void OnDestroy()
        {
            _renderUtility.Cleanup();
        }

        private void BeginDraw(Rect rect)
        {
            if (_renderUtility == null)
            {
                _renderUtility = new PreviewRenderUtility();
            }
            
            _renderUtility.camera.transform.position = new Vector3(1, 2, 10);
            _renderUtility.camera.transform.LookAt(Vector3.zero, Vector3.up);
            _renderUtility.camera.farClipPlane = 30;
 
            _renderUtility.lights[0].intensity = 0.5f;
            _renderUtility.lights[0].transform.rotation = Quaternion.Euler(30f, 30f, 0f);
            _renderUtility.lights[1].intensity = 0.5f;
            
            _renderUtility.BeginPreview(rect, GUIStyle.none);
        }

        private void EndDraw(Rect rect)
        {
            bool fog = RenderSettings.fog;
            Unsupported.SetRenderSettingsUseFogNoDirty(false);
            _renderUtility.camera.Render();
            Unsupported.SetRenderSettingsUseFogNoDirty(fog);
 
            Texture texture = _renderUtility.EndPreview();
            GUI.DrawTexture(rect, texture);
        }

        private void DrawRenderPreview(GameObject gameObject)
        {
            MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();

            for (int i = 0; i < meshFilters.Length; i++)
            {
                if (meshFilters[i].sharedMesh)
                {
                    MeshRenderer meshRenderer = meshFilters[i].gameObject.GetComponent<MeshRenderer>();

                    for (int j = 0; j < meshFilters[i].sharedMesh.subMeshCount; j++)
                    {
                        if (meshRenderer != null)
                        {
                            _renderUtility.DrawMesh(
                                meshFilters[i].sharedMesh,
                                gameObject.transform.worldToLocalMatrix * meshRenderer.transform.localToWorldMatrix,
                                meshRenderer.sharedMaterials[j],
                                j);
                        }
                    }
                }
            }
        }
    }
}