﻿using UnityEngine;

namespace Bipolar.SpritesetAnimation
{
    [CreateAssetMenu]
    public class TextureSpriteset : Spriteset
    {
        private static readonly Vector2 Center = new Vector2 (0.5f, 0.5f);

        [SerializeField, Min(1)]
        private int rowCount = 1;

        [SerializeField]
        private Texture2D texture;
        private Sprite[] sprites;

        [SerializeField, Min(0.001f)]
        private float pixelsPerUnit = 100;

        public override Sprite this[int index]
        {
            get
            {
                if (sprites == null)
                    CreateSpritesArray();
                return sprites[index];
            }
        }

        public override int RowCount => rowCount;
        public override int Count => RowCount * ColumnCount;

        private void OnEnable()
        {
            CreateSpritesArray();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

        [ContextMenu("Create")]
        private void CreateSpritesArray()
        {
            sprites = new Sprite[Count];

            float spriteWidth = texture.width / ColumnCount;
            float spriteHeight = texture.height / rowCount;
            
            for (int j = 0; j < rowCount; j++)
            {
                for (int i = 0; i < ColumnCount; i++)
                {
                    int index = j * ColumnCount + i;
                    var rect = new Rect(i * spriteWidth, j * spriteHeight, spriteWidth, spriteHeight);
                    sprites[index] = Sprite.Create(texture, rect, Center, pixelsPerUnit);
                }
            }
        }

        private void OnValidate()
        {
            CreateSpritesArray();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

#if UNITY_EDITOR
        private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange change)
        {
            if (change == UnityEditor.PlayModeStateChange.EnteredPlayMode || change == UnityEditor.PlayModeStateChange.EnteredEditMode)
                CreateSpritesArray();
        }
#endif
    }
}
