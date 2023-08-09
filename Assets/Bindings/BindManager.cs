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

            var viewModel = scene.GetRootGameObjects()
                .Select(obj => obj.GetComponent<ViewModelBehaviour>())
                .SingleOrDefault(x => x != null);

            switch(viewModel)
            {
                case InitialView initialView:
                    {
                        initialView.OnSwitchScene.AddListener(NewSession);
                    }
                    break;
                case MainView mainView:
                    {
                        mainView.InitialViewModelDefault();
                    }
                    break;
                default:
                    break;
            }
        }

        private void NewSession()
        {
            var session = new Session();

            var mainView = SceneManager.GetActiveScene().GetRootGameObjects()
                .Select(obj => obj.GetComponent<MainView>())
                .Single(x => x != null);

            mainView.InitialViewModel();

            mainView.AfterOnStart = () =>
            {
                mainView.Refresh(session);
            };

            mainView.OnCommmand = ((param) =>
            {
                switch(param)
                {
                    case MainView.CmdNextTurnParameter:
                        session.OnNextTurn();
                        break;
                }

                mainView.Refresh(session);

                //session.OnMessage(param.ConvertToMessage());
            });
        }
    }

    //static class CommandParameterExtensions
    //{
    //    public static IMessage ConvertToMessage(this MainView.CommandParameter parameter)
    //    {

    //    }
    //}
}
