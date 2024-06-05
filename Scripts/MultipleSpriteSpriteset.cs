﻿using UnityEngine;

namespace Bipolar.SpritesetAnimation
{

    [CreateAssetMenu]
    public class MultipleSpriteSpriteset : Spriteset
    {
        [SerializeField]
        private Sprite[] sprites;

        public override Sprite this[int index] => sprites[index];
        
        public override int Count => sprites.Length;
        
        public override int RowCount => (Count + ColumnCount - 1) / ColumnCount;
    }
}
