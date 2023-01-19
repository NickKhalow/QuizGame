using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace View
{
    [RequireComponent(typeof(Button))]
    public class ShakeButton : MonoBehaviour, IButtonEffect
    {
        [SerializeField] private float power = 0.2f;
        [SerializeField] private float duration = 0.3f;


        public void Notify(bool correct)
        {
            if (correct)
            {
                return;
            }
            
            transform.DOKill();
            transform.DOPunchScale(Vector3.one * power, duration);
        }
    }
}