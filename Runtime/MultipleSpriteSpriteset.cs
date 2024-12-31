using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.SpritesetAnimation
{
    [CreateAssetMenu(menuName = CreateAssetPath.Root + "Multiple Sprite Spriteset")]
    public class MultipleSpriteSpriteset : Spriteset
    {
        [SerializeField]
        private SpritesetRow[] sprites;
		private Dictionary<string, SpritesetRow> rowsByName;

        public override IReadOnlyList<Sprite> this[int index] => sprites[index];

		public IReadOnlyList<Sprite> this[string name]
		{
			get
			{
				if (rowsByName == null)
					CreateDictionary();

				return rowsByName.TryGetValue(name, out var row) ? row : null;
			}
		}

		private void CreateDictionary()
		{
			rowsByName = new Dictionary<string, SpritesetRow>();
			foreach (var row in sprites) 
				rowsByName.Add(row.Name, row);
		}

		public override int RowCount => sprites.Length;

		public override int GetFramesCount(int rowIndex) => sprites[rowIndex].Count;
	}

    [System.Serializable]
	public class SpritesetRow : IReadOnlyList<Sprite>
    {
		[SerializeField]
		private string name;
		public string Name => name;

        [SerializeField]
        private Sprite[] sprites;

		public Sprite this[int index] => sprites[index];

		public int Count => sprites.Length;

		public IEnumerator<Sprite> GetEnumerator()
		{
			foreach (var sprite in sprites) 
				yield return sprite;
		}

		IEnumerator IEnumerable.GetEnumerator() => sprites.GetEnumerator();
	}
}
