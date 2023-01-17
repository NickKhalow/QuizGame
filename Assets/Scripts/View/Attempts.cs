#nullable enable
using System;
using TMPro;
using Uitls;
using UnityEngine;


namespace View
{
    [RequireComponent(typeof(TMP_Text))]
    public class Attempts : MonoBehaviour
    {
        [SerializeField] private int count;
        private TMP_Text text = null!;


        public int Count => count;


        private void Awake()
        {
            text = GetComponent<TMP_Text>().EnsureNotNull();
            UpdateText();
        }


        public void Increase()
        {
            count++;
            UpdateText();
        }


        public void Decrease()
        {
            if (count <= 0)
            {
                throw new InvalidOperationException();
            }

            count--;
            UpdateText();
        }


        private void UpdateText()
        {
            text.SetText($"Attempts: {count}");
        }
    }
}