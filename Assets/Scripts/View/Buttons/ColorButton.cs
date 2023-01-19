#nullable enable
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace View
{
    [RequireComponent(typeof(Image))]
    public class ColorButton : MonoBehaviour, IButtonEffect
    {
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
    }
}