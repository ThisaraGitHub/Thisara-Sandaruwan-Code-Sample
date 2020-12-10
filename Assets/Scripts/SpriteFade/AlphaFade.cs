using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AnimaExamProject.Utils
{

    // This class is used to fade the title sprite. 

    namespace AnimaExamProject.Fade
    {
        public class AlphaFade : MonoBehaviour
        {
            [SerializeField] private float duration = 5.0f;          // Fade duration
            [SerializeField] private SpriteRenderer sprite;          // Reference to the sprite renderer

            private float startTime;                                 // Fade starting time
            private float minimum = 0.0f;                            // Fade minimum ammout
            private float maximum = 1f;                              // Fade maximum ammount

            void Start()
            {
                startTime = Time.time;
            }
            void Update()
            {
                Scene scene = SceneManager.GetActiveScene();
                // switch between scenes according to the build index
                float t = (Time.time - startTime) / duration;
                sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));
            }
        }
    }
}

