using UnityEditor;
using UnityEngine;

namespace Bipolar.SpritesetAnimation.Editor
{
    [CustomEditor(typeof(MultipleSpriteSpriteset))]
    public class SpritesetEditor : UnityEditor.Editor
    {
        private const string ColumnCountPropertyName = "columnCount";
        private const string SpritesPropertyName = "sprites";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var spritesProperty = serializedObject.FindProperty(SpritesPropertyName);

            var columnsProperty = serializedObject.FindProperty(ColumnCountPropertyName);
            int columnCount = columnsProperty.intValue;
            int spritesCount = spritesProperty.arraySize;

            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
            bool hasChanged = false;
            for (int rowIndex = 0, spriteIndex = 0; spriteIndex < spritesCount; rowIndex++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int columnIndex = 0; columnIndex < columnCount && spriteIndex < spritesCount; columnIndex++, spriteIndex++)
                {
                    var spriteProperty = spritesProperty.GetArrayElementAtIndex(spriteIndex);
                    var sprite = EditorGUILayout.ObjectField(GUIContent.none,
                        spriteProperty.objectReferenceValue, typeof(Sprite), allowSceneObjects: false, GUILayout.Width(66));
                    if (sprite != spriteProperty.objectReferenceValue)
                    {
                        spriteProperty.objectReferenceValue = sprite;
                        hasChanged = true;
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            }
            if (hasChanged)
                serializedObject.ApplyModifiedProperties();
        }

        public override bool HasPreviewGUI() => true;

        private int previewStartTime;
        private int previewFrameIndex;
        private Texture2D previewTexture;
        public override void OnInteractivePreviewGUI(Rect previewRect, GUIStyle background)
        {
            if (previewStartTime == 0)
                previewStartTime = (int)EditorApplication.timeSinceStartup;

            var spritesProperty = serializedObject.FindProperty(SpritesPropertyName);
            int spritesCount = spritesProperty.arraySize;
            int frameIndex = ((int)EditorApplication.timeSinceStartup - previewStartTime) % spritesCount;
            if (previewFrameIndex != frameIndex)
            {
                previewFrameIndex = frameIndex;
                var sprite = (Sprite)spritesProperty.GetArrayElementAtIndex(previewFrameIndex).objectReferenceValue;
                previewTexture = AssetPreview.GetAssetPreview(sprite);
            }

            if (previewTexture)
                EditorGUI.DrawTextureTransparent(previewRect, previewTexture);
        }
    }
}
