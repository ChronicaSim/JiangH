using JiangH.Sessions;
using JiangH.Views;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JiangH.Bindings
{
    class BindManager : MonoBehaviour
    {
        void Start()
        {
            var scene = SceneManager.GetActiveScene();

            Debug.Log($"SessionManager get current scene '{scene.name}'");

            var initView = scene.GetRootGameObjects()
                .Select(obj => obj.GetComponent<InitialView>())
                .SingleOrDefault(x => x != null);

            initView?.OnSwitchScene.AddListener(NewSession);
        }

        private void NewSession()
        {
            var mainView = SceneManager.GetActiveScene().GetRootGameObjects()
                .Select(obj => obj.GetComponent<MainView>())
                .Single(x => x != null);

            mainView.ClearDesignData();

            var session = new Session();
            mainView.Refresh(session);

            mainView.nextTurn.onClick.AddListener(() =>
            {
                session.OnNextTurn();
                mainView.Refresh(session);
            });
        }
    }
}
