using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace AnimaExamProject.Utils
{
    // This class is used to fade the entire screen to black (or any chosen colour). 
    // It should be used to smooth out the transition between scenes or restarting of a scene.

    public class CameraFade : MonoBehaviour
    {
        public event Action OnFadeComplete;                             // This is called when the fade in or out has finished.


        [SerializeField] private Image fadeImage;                     // Reference to the image that covers the screen.
        [SerializeField] private Color fadeColor = Color.black;       // The colour the image fades out to.
        [SerializeField] private float fadeDuration = 2.0f;           // How long it takes to fade in seconds.
        [SerializeField] private bool fadeInOnSceneLoad = false;      // Whether a fade in should happen as soon as the scene is loaded.
        [SerializeField] private bool fadeInOnStart = false;          // Whether a fade in should happen just but Updates start.

        
        private bool isFading;                                        // Whether the screen is currently fading.
        private Color fadeOutColor;                                   // This is a transparent version of the fade colour, it will ensure fading looks normal.


        public bool IsFading { get { return isFading; } }


        private void Awake()
        {
			SceneManager.sceneLoaded += HandleSceneLoaded;

            fadeOutColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);
            fadeImage.enabled = true;
        }


        private void Start()
        {
            // If applicable set the immediate colour to be faded out and then fade in.
            if (fadeInOnStart)
            {
                fadeImage.color = fadeColor;
                FadeIn();
            }
        }


		private void HandleSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            // If applicable set the immediate colour to be faded out and then fade in.
            if (fadeInOnSceneLoad)
            {
                fadeImage.color = fadeColor;
                FadeIn();
            }
        }

        
        // Since no duration is specified with this overload use the default duration.
        public void FadeOut()
        {
            FadeOut(fadeDuration);
        }


        public void FadeOut(float duration)
        {
            // If not already fading start a coroutine to fade from the fade out colour to the fade colour.
            if (isFading)
                return;
            StartCoroutine(BeginFade(fadeOutColor, fadeColor, duration));
        }


        // Since no duration is specified with this overload use the default duration.
        public void FadeIn()
        {
            FadeIn(fadeDuration);
        }


        public void FadeIn(float duration)
        {
            // If not already fading start a coroutine to fade from the fade colour to the fade out colour.
            if (isFading)
                return;
            StartCoroutine(BeginFade(fadeColor, fadeOutColor, duration));

        }


        public IEnumerator BeginFadeOut ()
        {
            yield return StartCoroutine(BeginFade(fadeOutColor, fadeColor, fadeDuration));
        }


        public IEnumerator BeginFadeOut(float duration)
        {
            yield return StartCoroutine(BeginFade(fadeOutColor, fadeColor, duration));
        }


        public IEnumerator BeginFadeIn ()
        {
            yield return StartCoroutine(BeginFade(fadeColor, fadeOutColor, fadeDuration));
        }


        public IEnumerator BeginFadeIn(float duration)
        {
            yield return StartCoroutine(BeginFade(fadeColor, fadeOutColor, duration));
        }


        private IEnumerator BeginFade(Color startCol, Color endCol, float duration)
        {
            // Fading is now happening.  This ensures it won't be interupted by non-coroutine calls.
            isFading = true;

            // Execute this loop once per frame until the timer exceeds the duration.
            float timer = 0f;
            while (timer <= duration)
            {
                // Set the colour based on the normalised time.
                fadeImage.color = Color.Lerp(startCol, endCol, timer / duration);

                // Increment the timer by the time between frames and return next frame.
                timer += Time.deltaTime;
                yield return null;
            }

            // Fading is finished so allow other fading calls again.
            isFading = false;

            // If anything is subscribed to OnFadeComplete call it.
            if (OnFadeComplete != null)
                OnFadeComplete();
        }

		void OnDestroy()
		{
			SceneManager.sceneLoaded -= HandleSceneLoaded;
		}
    }
}