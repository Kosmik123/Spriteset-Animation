using UnityEditor;
using UnityEngine;

namespace Bipolar.SpritesetAnimation.Editor
{
    [CustomEditor(typeof(TextureSpriteset))]
    public class TextureSpritesetEditor : UnityEditor.Editor
    {
        public const string rowCountPropertyName = "rowCount";

        private TextureSpriteset _spriteset;
        public TextureSpriteset Spriteset
        {
            get 
            {
                if (_spriteset == null) 
                    _spriteset = target as TextureSpriteset;
                return _spriteset;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var columnsProperty = serializedObject.FindProperty(SpritesetEditorUtility.ColumnCountPropertyName);
            int columnCount = columnsProperty.intValue;

            var rowCountProperty = serializedObject.FindProperty(rowCountPropertyName);
            int rowCount = rowCountProperty.intValue;

            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
            EditorGUI.BeginDisabledGroup(true);
            for (int j = 0; j < rowCount; j++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int i = 0; i < columnCount; i++)
                {
                    int index = j * columnCount + i;
                    EditorGUILayout.ObjectField(GUIContent.none, Spriteset[index], typeof(Sprite), allowSceneObjects: true, GUILayout.Width(66));
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            }

            EditorGUI.EndDisabledGroup();
        }

        public override bool HasPreviewGUI() => true;

        private int previewStartTime;
        private int previewFrameIndex;
        private Texture2D previewTexture;
        public override void OnInteractivePreviewGUI(Rect previewRect, GUIStyle background)
        {
            if (previewStartTime == 0)
                previewStartTime = (int)EditorApplication.timeSinceStartup;

            int spritesCount = Spriteset.Count;
            int frameIndex = ((int)EditorApplication.timeSinceStartup - previewStartTime) % spritesCount;
            if (previewFrameIndex != frameIndex)
            {
                previewFrameIndex = frameIndex;
                var sprite = Spriteset[previewFrameIndex];
                previewTexture = AssetPreview.GetAssetPreview(sprite);
            }

            if (previewTexture)
                EditorGUI.DrawTextureTransparent(previewRect, previewTexture);
        }
    }
}
