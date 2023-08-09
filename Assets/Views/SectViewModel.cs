using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using uguimvvm;
using System;
using System.Windows.Input;

namespace JiangH.Views
{
    [MvvmDataContext]
    public class SectViewModel : ViewModel
    {
        private OfficeViewModel leaderOffice;
        public OfficeViewModel LeaderOffice
        {
            get => leaderOffice;
            set => SetProperty(ref leaderOffice, value);
        }

        private OfficeViewModel viceLeaderOffice;
        public OfficeViewModel ViceLeaderOffice
        {
            get => viceLeaderOffice;
            set => SetProperty(ref viceLeaderOffice, value);
        }

        private ObservableCollection<OfficeViewModel> senatorOffices;
        public ObservableCollection<OfficeViewModel> SenatorOffices
        {
            get => senatorOffices;
            set => SetProperty(ref senatorOffices, value);
        }

        private ObservableCollection<OfficeViewModel> departLeaderOffices;
        public ObservableCollection<OfficeViewModel> DepartLeaderOffices
        {
            get => departLeaderOffices;
            set => SetProperty(ref departLeaderOffices, value); 
        }

        public SectViewModel()
        {
            leaderOffice = new OfficeViewModel();
            viceLeaderOffice = new OfficeViewModel();
            senatorOffices = new ObservableCollection<OfficeViewModel>();
            departLeaderOffices = new ObservableCollection<OfficeViewModel>();
        }

        public static SectViewModel Default { get; } = new SectViewModel()
        {
            LeaderOffice = OfficeViewModel.Default,
            ViceLeaderOffice = OfficeViewModel.Default,
            SenatorOffices = new ObservableCollection<OfficeViewModel>(OfficeViewModel.DefaultCollection),
            DepartLeaderOffices = new ObservableCollection<OfficeViewModel>(OfficeViewModel.DefaultCollection)
        };
    }

    [MvvmDataContext]
    public class OfficeViewModel : ViewModel
    {
        public string uid;

        public RelayCommand RevokeCommand { get; }

        private PersonViewModel personViewModel;
        public PersonViewModel PersonItemView
        {
            get => personViewModel;
            set
            {
                if(SetProperty(ref personViewModel, value))
                {
                    RevokeCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string officeName;
        public string OfficeName
        {
            get => officeName;
            set => SetProperty(ref officeName, value);
        }

        public OfficeViewModel()
        {
            RevokeCommand = new RelayCommand(() => SendMessage(new RevokeOffice(uid, PersonItemView.uid)), ()=> PersonItemView!= null);
        }

        public static OfficeViewModel Default { get; } = new OfficeViewModel()
        {
            OfficeName = "DEFAULT_OFFICE_NAME",
            PersonItemView = PersonViewModel.Default
        };

        public static IEnumerable<OfficeViewModel> DefaultCollection { get; } = new OfficeViewModel[]
        {
            new OfficeViewModel(){ OfficeName = "DEFAULT_OFFICE_NAME_1", PersonItemView = PersonViewModel.Default },
            new OfficeViewModel(){ OfficeName = "DEFAULT_OFFICE_NAME_2", PersonItemView = PersonViewModel.Default },
            new OfficeViewModel(){ OfficeName = "DEFAULT_OFFICE_NAME_3", PersonItemView = PersonViewModel.Default },
        };
    }

    [MvvmDataContext]
    public class PersonViewModel : ViewModel
    {
        public string uid;

        private string personName;
        public string PersonName
        {
            get => personName;
            set => SetProperty(ref personName, value);
        }

        public static PersonViewModel Default { get; } = new PersonViewModel()
        {
            PersonName = "DEFAULT_PERSON_NAME"
        };
    }

    public interface IMessage
    {

    }

    public class RevokeOffice : IMessage
    {
        public readonly string officeUid;
        public readonly string personUid;

        public RevokeOffice(string officeUid, string personUid)
        {
            this.officeUid = officeUid;
            this.personUid = personUid;
        }
    }

    public class NextTurn : IMessage
    {

    }

    public class ViewModel : INotifyPropertyChanged
    {
        public static Action<IMessage> SendMessage;

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
