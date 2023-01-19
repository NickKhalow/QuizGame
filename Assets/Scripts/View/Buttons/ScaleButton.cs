#nullable enable
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace View
{
    [RequireComponent(typeof(Button))]
    public class ScaleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private bool use = true;
        [SerializeField] private float holdScale = 0.8f;
        [SerializeField] private float releaseScale = 1.1f;
        [SerializeField] private float duration = 0.3f;
        [Header("Debug")] [SerializeField] private Vector3 defaultScale = Vector3.one;
        private Tween? currentTween;


        private void Awake()
        {
            defaultScale = transform.localScale;
        }


        public void OnPointerDown(PointerEventData _)
        {
            if (use == false)
            {
                return;
            }

            currentTween?.Kill();
            currentTween = transform.DOScale(holdScale, duration);
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            if (use == false)
            {
                return;
            }
            
            currentTween?.Kill();
            currentTween = DOTween.Sequence()
                !.Append(
                    transform.DOScale(
                        releaseScale,
                        duration
                    )!
                )
                !.Append(
                    transform.DOScale(
                        defaultScale,
                        duration
                    )!
                );
        }
    }
}