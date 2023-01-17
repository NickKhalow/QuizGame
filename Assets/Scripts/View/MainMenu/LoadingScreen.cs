#nullable enable
using System.Collections;
using Uitls;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace View.MainMenu
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Canvas canvas = null!;
        [SerializeField] private Slider slider = null!;
        [SerializeField] private float artificialDelay = 0.2f;
        private const float FullLoad = 1;


        private void Awake()
        {
            slider.EnsureNotNull();
            canvas.EnsureNotNull().enabled = false;
            DontDestroyOnLoad(gameObject);
        }


        public void LoadScene(int sceneId)
        {
            StartCoroutine(Load(sceneId));
        }


        private IEnumerator Load(int sceneId)
        {
            canvas.enabled = true;
            var operation = SceneManager.LoadSceneAsync(sceneId)!;
            while (operation.isDone == false)
            {
                slider.value = operation.progress;
                yield return null;
            }

            slider.value = FullLoad;
            yield return new WaitForSecondsRealtime(artificialDelay);
            canvas.enabled = false;
        }
    }
}