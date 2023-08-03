using UnityEngine;

namespace JiangH.Bindings
{
    class RuntimeInitializeFacade
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]

        public static void LoadGameController()
        {
            var main = new GameObject("SessionManager");
            main.AddComponent<BindManager>();

            Object.DontDestroyOnLoad(main);
        }
    }
}
