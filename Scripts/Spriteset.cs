using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bipolar.SpritesetAnimation
{
    [CreateAssetMenu]
    public class Spriteset : ScriptableObject
    {
        [SerializeField]
        private Sprite[] sprites;
        public IReadOnlyList<Sprite> Sprites => sprites;
        public int Count => sprites.Length;

        [SerializeField, Min(1)]
        [FormerlySerializedAs("columnsCount")]
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

        public int RowCount => (Count + ColumnCount - 1) / columnCount;
    }
}
