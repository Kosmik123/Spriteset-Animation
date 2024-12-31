﻿using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.SpritesetAnimation
{
    public abstract class Spriteset : ScriptableObject
    {
        public int ColumnCount => 4;

        public abstract int RowCount { get; }

        public abstract IReadOnlyList<Sprite> this[int rowIndex] { get; }

        public abstract int GetFramesCount(int rowIndex);
    }
}
