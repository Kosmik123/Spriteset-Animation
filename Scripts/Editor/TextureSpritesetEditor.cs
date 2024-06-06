using UnityEditor;
using UnityEngine;

namespace Bipolar.SpritesetAnimation.Editor
{
    [CustomEditor(typeof(TextureSpriteset))]
    public class TextureSpritesetEditor : UnityEditor.Editor
    {
        public const string rowCountPropertyName = "rowCount";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var columnsProperty = serializedObject.FindProperty(SpritesetEditorUtility.ColumnCountPropertyName);
            int columnCount = columnsProperty.intValue;

            var rowCountProperty = serializedObject.FindProperty(rowCountPropertyName);
            int rowCount = rowCountProperty.intValue;

            var spriteset = target as TextureSpriteset;
            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
            EditorGUI.BeginDisabledGroup(true);
            for (int j = 0; j < rowCount; j++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int i = 0; i < columnCount; i++)
                {
                    int index = j * columnCount + i;
                    EditorGUILayout.ObjectField(GUIContent.none, spriteset[index], typeof(Sprite), allowSceneObjects: true, GUILayout.Width(66));
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            }


            EditorGUI.EndDisabledGroup();
        }
    }
}
