using UnityEditor;
using UnityEngine;

namespace Bipolar.SpritesetAnimation.Editor
{
	//[CustomPropertyDrawer(typeof(SpritesetRow))]
    public class SpritesetRowDrawer : PropertyDrawer
    {
        private const string innerArrayPropertyName = "sprites";
        private const string namePropertyName = "name";

        private static readonly GUIContent empty = new GUIContent(null, null, null);

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
            var arrayProperty = property.FindPropertyRelative(innerArrayPropertyName);
            return EditorGUI.GetPropertyHeight(arrayProperty, label);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
            var arrayProperty = property.FindPropertyRelative(innerArrayPropertyName);
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(position, arrayProperty, label);
            EditorGUI.indentLevel--;

            var nameProperty = property.FindPropertyRelative(namePropertyName);
            Rect nameRect = position;
            nameRect.height = EditorGUIUtility.singleLineHeight;
            nameRect.xMin += 32;
            nameRect.xMax -= 32;
            EditorGUI.PropertyField(nameRect, nameProperty, empty);
        }
	}
}
