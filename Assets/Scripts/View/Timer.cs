#nullable enable
using Uitls;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace View
{
    [RequireComponent(typeof(Slider))]
    public class Timer : MonoBehaviour
    {
        public readonly UnityEvent done = new();
        [SerializeField] private Slider slider = null!;
        [SerializeField] private Image sliderBar = null!;
        [SerializeField] private Gradient gradient = new();
        [SerializeField] private float maxTime = 20;


        public float TimeLasts => slider.value;


        private void Awake()
        {
            sliderBar.EnsureNotNull();
            slider.EnsureNotNull();
            slider.maxValue = maxTime;
            slider.value = maxTime;
        }


        private void Update()
        {
            slider.value -= Time.deltaTime;
            if (slider.value <= 0)
            {
                done.Invoke();
            }

            sliderBar.color = gradient.Evaluate(slider.value / maxTime);
        }


        public void Add(float value)
        {
            slider.value += value;
        }
    }
}