using UnityEditor;
using UnityEngine;
using BLUE.Extensions;
using System.Collections.Generic;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileSelectionWindow : EditorWindow
    {
        static public List<GameObject> Tiles = new List<GameObject>();

        private PreviewRenderUtility _renderUtility;
        private Vector2 _scrollPos;
        
        private Color _defaultBGColor;
        private Color _selectedBGColor = new Color(0, 1, 1, 0.5f);
        
        private int _selectedIndex = -1;
        
        [MenuItem("BLUE/Level Editor/Tile Selection Window")]
        public static void ShowWindow()
        {
            GetWindow<TileSelectionWindow>().Close();
            
            GetWindow<TileSelectionWindow>();
        }

        private void OnEnable()
        {
            _defaultBGColor = GUI.backgroundColor;
        }

        private void OnValidate()
        {
            _defaultBGColor = GUI.backgroundColor;
            
            CleanUpPreviewRenderUtility();
        }

        private void OnDisable()
        {
            CleanUpPreviewRenderUtility();
        }

        private void OnDestroy()
        {
            CleanUpPreviewRenderUtility();
        }

        private void OnGUI()
        {
            Tiles.Clear();

            GUILayout.BeginVertical();

            CreateTileScrollView();
            
            GUILayout.Space(10);

            this.Divider();
            
            GUILayout.Space(25);

            CreateUpdateButton();
            
            GUILayout.Space(25);

            this.Divider();

            CreateTilePreview();
            
            GUILayout.EndVertical();
        }

        private void CreateTileScrollView()
        {
            _scrollPos = GUILayout.BeginScrollView(_scrollPos, false, true, GUILayout.Width(position.width), GUILayout.Height(0));
            
            GUIStyle itemStyle = new GUIStyle(GUI.skin.button);
            itemStyle.alignment = TextAnchor.MiddleLeft; 
            itemStyle.margin = new RectOffset(25, 25, 5, 5);

            TileSettings[] settingsArr = TileSettingsContainer.Instance.TileSettingsSo.TileSettings;

            for (int i = 0; i < settingsArr.Length; i++)
            {
                GUI.backgroundColor = _selectedIndex == i ? _selectedBGColor : Color.clear;

                string itemName = settingsArr[i].DisplayName;
                
                if (GUILayout.Button(itemName, itemStyle)) 
                {
                    _selectedIndex = i;
                }
                
                GUI.backgroundColor = _defaultBGColor;
            }
            
            GUILayout.EndScrollView();
        }

        private void CreateUpdateButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            GUIStyle selectionButtonStyle = new GUIStyle(GUI.skin.button);
            selectionButtonStyle.margin = new RectOffset(25, 25, 0, 0);
            selectionButtonStyle.fixedWidth = 150;

            if (GUILayout.Button("Update Selected Tile", selectionButtonStyle))
            {
                TileProvider_Editor.Instance.SelectedTileID = _selectedIndex;
            }
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        #region Mesh Drawing

        private void CreateTilePreview()
        {
            if (_selectedIndex >= 0)
            {   
                GameObject gameObject = TileSettingsContainer.Instance.TileSettingsSo.TileSettings[_selectedIndex].Tile.gameObject;
                
                Rect rect = new Rect(0, position.height - 250, position.width, 250);

                BeginDraw(rect);
                DrawRenderPreview(gameObject);
                EndDraw(rect);
            }
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

            GUIStyle renderPrev = new GUIStyle();
            renderPrev.active.background = Texture2D.whiteTexture;
            
            _renderUtility.BeginPreview(rect, renderPrev);
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
                            int matIndex = Mathf.Clamp(j, 0, meshRenderer.sharedMaterials.Length - 1);
                            
                            _renderUtility.DrawMesh(
                                meshFilters[i].sharedMesh,
                                gameObject.transform.worldToLocalMatrix * meshRenderer.transform.localToWorldMatrix,
                                meshRenderer.sharedMaterials[matIndex],
                                j);
                        }
                    }
                }
            }
        }

        private void CleanUpPreviewRenderUtility()
        {
            if (_renderUtility != null)
            {
                _renderUtility.Cleanup();
            }
        }

        #endregion
    }
}