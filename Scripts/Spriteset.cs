using UnityEngine;

namespace Bipolar.SpritesetAnimation
{
    public abstract class Spriteset : ScriptableObject
    {
        [SerializeField, Min(1)]
        private int columnCount = 1;
        public int ColumnCount
        {
            get
            {
                if (columnCount < 1)
                    columnCount = 1;
                return columnCount;
            }
        }

        public abstract int Count { get; }
        public abstract int RowCount { get; }

        public abstract Sprite this[int index] { get; }
    }
}
