using UnityEngine;

namespace Bipolar.SpritesetAnimation
{
    public class SpritesetAnimator : MonoBehaviour
    {
        public const int idleAnimationSpeed = 4;

        [Header("Settings")]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Spriteset spriteset;

        [Header("Properties")]
        [SerializeField, Min(0)]
        private float animationSpeed = 4;
        public float AnimationSpeed
        {
            get => animationSpeed;
            set => animationSpeed = Mathf.Max(value, 0);
        }

        [SerializeField]
        private bool isReversed;
        public bool IsReversed
        {
            get => isReversed;
            set => isReversed = value;
        }

        [SerializeField]
        private int currentAnimationIndex;
        public int CurrentAnimationIndex
        {
            get => currentAnimationIndex;
            set
            {
                if (currentAnimationIndex == value)
                    return;

                currentAnimationIndex = value;
                ValidateAnimationIndex();
                animationTimer = 1;
            }
        }

        [SerializeField]
        private int overrideSequenceLength;
        public int OverrideSequenceLength
        {
            get => overrideSequenceLength;
            set => overrideSequenceLength = value;
        }

        [SerializeField]
        private int frameIndexOffset;
        public int FrameIndexOffset
        {
            get => frameIndexOffset;
            set => frameIndexOffset = value;
        }

        [SerializeField]
#if NAUGHTY_ATTRIBUTES
        [NaughtyAttributes.ReadOnly]
#endif
        private int baseFrameIndex;
        public int BaseFrameIndex
        {
            get => baseFrameIndex;
            set => baseFrameIndex = value;
        }

        public int CurrentFrameIndex => baseFrameIndex + frameIndexOffset;
        public int SpriteIndex => currentAnimationIndex * spriteset.ColumnCount + CurrentFrameIndex;

        public int CurrentSequenceLength => overrideSequenceLength > 0
            ? Mathf.Min(overrideSequenceLength, spriteset.Sprites.Count - currentAnimationIndex * spriteset.ColumnCount)
            : spriteset.ColumnCount;

        public int RowCount => spriteset ? spriteset.RowCount : 0;

#if NAUGHTY_ATTRIBUTES
        [NaughtyAttributes.ShowNonSerializedField]
#endif
        private float animationTimer;

        private void Start()
        {
            currentAnimationIndex = 0;
            animationTimer = 0;
        }

        private void Update()
        {
            animationTimer += Time.deltaTime * animationSpeed;
            if (animationTimer > 1)
            {
                animationTimer -= 1;
                int sequenceLength = CurrentSequenceLength;
                int indexChange = isReversed ? -1 : 1;
                baseFrameIndex = (baseFrameIndex + indexChange + sequenceLength) % sequenceLength;
                RefreshSprite();
            }
        }

        public void RefreshSprite()
        {
            spriteRenderer.sprite = spriteset.Sprites[SpriteIndex];
        }

        private void ValidateAnimationIndex()
        {
            currentAnimationIndex = Mathf.Clamp(currentAnimationIndex, 0, RowCount - 1);
        }

        private void OnValidate()
        {
            ValidateAnimationIndex();
        }
    }
}
