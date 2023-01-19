#nullable enable
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace View
{
    [RequireComponent(typeof(Image))]
    public class ColorButton : MonoBehaviour, IButtonEffect
    {
        [SerializeField] private bool useShake = true;
        [SerializeField] private float shakePower = 0.2f;
        [SerializeField] private float shakeDuration = 0.3f;
        [Space] [SerializeField] private bool useColor = true;
        [SerializeField] private Color correctColor;
        [SerializeField] private Color incorrectColor;
        [Space] [SerializeField] private float colorDuration = 0.5f;
        [SerializeField] [Header("Debug")] private Color defaultColor;
        private Image image = null!;


        private void Awake()
        {
            image = GetComponent<Image>()!;
            defaultColor = image.color;
        }


        public void Notify(bool correct)
        {
            if (useColor)
            {
                DOTween.Sequence()!
                    .Append(
                        image.DOColor(
                            correct
                                ? correctColor
                                : incorrectColor,
                            colorDuration
                        )!
                    )!
                    .Append(
                        image.DOColor(
                            defaultColor,
                            colorDuration
                        )!
                    );
            }

            if (useShake)
            {
                if (correct)
                {
                    return;
                }

                transform.DOKill();
                transform.DOPunchScale(Vector3.one * shakeDuration, shakeDuration);
            }
        }
    }
}