using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace JiangH.Views
{
    public class InitialView : MonoBehaviour
    {
        public UnityEvent OnSwitchScene;

        // Start is called before the first frame update
        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("MainScene", new LoadSceneParameters(LoadSceneMode.Single));
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode model)
        {
            OnSwitchScene.Invoke();
        }
    }
}
