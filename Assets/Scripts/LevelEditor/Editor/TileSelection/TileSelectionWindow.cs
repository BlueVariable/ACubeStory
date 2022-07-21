using UnityEditor;
using UnityEngine;
using BLUE.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileSelectionWindow : EditorWindow
    {
        static public List<GameObject> Tiles = new List<GameObject>();

        private PreviewRenderUtility _renderUtility;
        private Vector2 _scrollPos;
        
        private Color _defaultBGColor;
        private Color _selectedBGColor = new Color(0, 1, 1, 0.5f);

        private ETileCategory _selectedTileCategory = ETileCategory.None;
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
            
            if (TileSettingsContainer.Instance == null)
            {
                return;
            }

            float totalHeight = position.height - 30;

            GUILayout.BeginVertical();

            GUILayout.Space(10);
            
            CreateTileCategorySelection();
            
            GUILayout.Space(10);
            
            CreateTileScrollView(totalHeight * 0.6f);
            
            GUILayout.Space(10);

            this.AddHorizontalDivider();
            
            CreateTilePreview(totalHeight * 0.4f);
            
            GUILayout.EndVertical();
        }

        private void CreateTileCategorySelection()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            EditorGUIUtility.labelWidth = 100; 
            
            ETileCategory selectedTileCategory = (ETileCategory) EditorGUILayout.EnumPopup("Tile Category", _selectedTileCategory, GUILayout.Width(350));

            if (_selectedTileCategory != selectedTileCategory)
            {
                _selectedIndex = -1;
            }
            
            _selectedTileCategory = selectedTileCategory;
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void CreateTileScrollView(float height)
        {
            _scrollPos = GUILayout.BeginScrollView(_scrollPos, false, true, GUILayout.Width(position.width), GUILayout.Height(height));
            
            GUIStyle itemStyle = new GUIStyle(GUI.skin.button);
            itemStyle.alignment = TextAnchor.MiddleLeft; 
            itemStyle.margin = new RectOffset(25, 25, 5, 5);

            TileSettings[] settingsArr =
                TileSettingsContainer.Instance.TileSettingsSo.TileSettings.Where(i =>
                    i.Tile.TileCategory == _selectedTileCategory).ToArray();

            for (int i = 0; i < settingsArr.Length; i++)
            {
                GUI.backgroundColor = _selectedIndex == i ? _selectedBGColor : Color.clear;

                string itemName = settingsArr[i].DisplayName;
                
                if (GUILayout.Button(itemName, itemStyle)) 
                {
                    _selectedIndex = i;
                    
                    TileProvider_Editor.Instance.SelectedTileID = settingsArr[i].TileID;
                }
                
                GUI.backgroundColor = _defaultBGColor;
            }
            
            GUILayout.EndScrollView();
        }

        #region Mesh Drawing

        private void CreateTilePreview(float height)
        {
            if (_selectedIndex >= 0)
            {
                TileSettings[] settingsArr =
                    TileSettingsContainer.Instance.TileSettingsSo.TileSettings.Where(i =>
                        i.Tile.TileCategory == _selectedTileCategory).ToArray();
                
                GameObject gameObject = settingsArr[_selectedIndex].Tile.gameObject;
                
                Rect rect = new Rect(0, position.height - (height - 35), position.width, height);

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
            _renderUtility.camera.fieldOfView = 10;
 
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