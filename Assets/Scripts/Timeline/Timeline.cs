using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AnimaExamProject.Utils
{
    public class Timeline : MonoBehaviour
    {
        // This class is used to manage the timeline throught the game

        public GameObject loadedRing1;
        public GameObject loadedRing2;
        public GameObject loadedRing3;

        [SerializeField] private CameraFade fadeCamera;           // Reference to the script that fades the scene to black.
        void Awake()
        {
            // Check CoreAssects is there and if there multiple, destroy it
            GameObject[] objs = GameObject.FindGameObjectsWithTag("CoreAssects");

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        }
        private void Start()
        {
            //PlayerPrefs.DeleteAll();

            loadedRing1.SetActive(false);
            loadedRing2.SetActive(false);
            loadedRing3.SetActive(false);
        }
        private void Update()
        {
            //Switching the scenes 

            Scene scene = SceneManager.GetActiveScene();

            switch (scene.buildIndex)
            {
                case 0:
                    loadedRing1.SetActive(true);

                    loadedRing2.SetActive(false);
                    loadedRing3.SetActive(false);
                    break;

                case 1:
                    loadedRing2.SetActive(true);

                    loadedRing1.SetActive(false);
                    loadedRing3.SetActive(false);
                    break;

                case 2:
                    loadedRing3.SetActive(true);

                    loadedRing1.SetActive(false);
                    loadedRing2.SetActive(false);
                    break;
            }
        }

        public void LoadLevel(string levelName)
        {
            StartCoroutine(FadeToMenu(levelName));
        }
        private IEnumerator FadeToMenu(string m_MenuSceneName)
        {
            // Wait for the screen to fade out.
            yield return StartCoroutine(fadeCamera.BeginFadeOut());

            // Load the main menu by itself.
            SceneManager.LoadScene(m_MenuSceneName, LoadSceneMode.Single);
        }
    }
}