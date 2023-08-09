using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using uguimvvm;
using UnityEngine;
using UnityEngine.UI;

namespace JiangH.Views
{
    public class MainView : ViewModelBehaviour
    {
        private SectViewModel sectViewModel;
        public SectViewModel SectViewModel
        {
            get => sectViewModel;
            set => SetProperty(ref sectViewModel, value);
        }

        public SectView sect;

        public ContainerView departs;

        public List<PersonView> persons = new List<PersonView>();

        public Button nextTurn;

        public override void OnStart()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitialViewModelDefault()
        {
            SectViewModel = SectViewModel.Default;
        }

        public void InitialViewModel()
        {
            SectViewModel = new SectViewModel();
        }
    }


    [Serializable]
    public class PersonView
    {
        public string uuid;
        public string name;
        public float money;
    }

    public abstract class ViewModelBehaviour : MonoBehaviour, INotifyPropertyChanged
    {
        public Action AfterOnStart;
        public Action BeforeOnStart;

        void Start()
        {
            BeforeOnStart?.Invoke();

            OnStart();

            AfterOnStart?.Invoke();
        }

        public abstract void OnStart();

        /// <summary>
        ///     Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Checks if a property already matches a desired value.  Sets the property and
        ///     notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        ///     Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}