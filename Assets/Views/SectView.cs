using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace JiangH.Views
{
    public class View : MonoBehaviour
    {

    }

    public class SectView : View
    {
        public PersonItemView leader;

        public Text testText;
    }

    public abstract class ViewModelBase : MonoBehaviour
    {
        protected virtual void RefreshDataSource()
        {

        }

        private View view;

        protected abstract void RefreshView(View view);

        void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            RefreshDataSource();
            RefreshView(view);
        }
    }

    public abstract class ViewModel<T> : ViewModelBase
        where T : View
    {
        protected override void RefreshView(View view)
        {
            RefreshView(view as T);
        }

        protected abstract void RefreshView(T view);

    }

    //public class SectViewModelImp : SectViewModel
    //{
    //    private SectTest sect;

    //    public void AssocateDataSource(SectTest sect)
    //    {
    //        this.sect = sect;
    //        RefreshDataSource();
    //    }

    //    protected override void RefreshDataSource()
    //    {
    //        test = sect.name;
    //    }
    //}

    //public class SectTest
    //{
    //    public string name;
    //}

    //class RuntimeInitializeFacade
    //{
    //    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]

    //    public static void LoadGameController()
    //    {
    //        Assembly currentAssem = Assembly.GetExecutingAssembly();

    //        var views = Object.FindObjectsOfType<View>(true);
    //        foreach (var view in views)
    //        {
    //            var viewModel = currentAssem.GetTypes().Where(x => x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition() == typeof(ViewModel<>))
    //                .Where(x => x.BaseType.GetGenericArguments()[0] == view.GetType())
    //                .FirstOrDefault();

    //            view.gameObject.AddComponent(viewModel);
    //        }
    //    }
    //}
}
