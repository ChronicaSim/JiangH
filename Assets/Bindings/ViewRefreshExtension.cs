using JiangH.Data.Interfaces;
using JiangH.Sessions;
using JiangH.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

static class ViewRefreshExtension
{
    public static Func<IDepart, IEnumerable<IPerson>> GetDepart2Persons;
    public static Func<IOffice, IPerson> GetOffice2Person;
    public static Func<ISect, IEnumerable<IDepart>> GetSect2Departs;

    public static void Refresh(this MainView mainView, Session session)
    {
        GetDepart2Persons = session.relationQuery.GetDepart2Persons;
        GetOffice2Person = session.relationQuery.GetOffice2Persons;
        GetSect2Departs = session.relationQuery.GetDepartsBySect;

         var depart = session.relationQuery.GetPerson2Depart(session.player);
        var persons = session.relationQuery.GetDepart2Persons(depart);

        var sect = session.relationQuery.GetSectByDepart(depart);
        var departs = session.relationQuery.GetDepartsBySect(sect);

        mainView.SectViewModel.Refresh(sect);
        //mainView.sect.Refresh(sect);
        //mainView.departs.Refresh(departs);
        //mainView.persons.Refresh(persons);
    }

    public static void Refresh(this SectViewModel viewModel, ISect sect)
    {
        viewModel.LeaderOffice.Refresh(sect.leaderOffice);

        var sectLeader = GetOffice2Person(sect.leaderOffice);

        var departs = GetSect2Departs(sect).Where(x=>
        {
            var departLeader = GetOffice2Person(x.leaderOffice);
            return departLeader != sectLeader;
        });

        viewModel.DepartLeaderOffices.Refresh(departs.Select(x=>x.leaderOffice));
    }

    public static void Refresh(this ObservableCollection<OfficeViewModel> viewModels, IEnumerable<IOffice> office)
    {
        var dictOffice = office.ToDictionary(x => x.uid);
        var dictViewModels = viewModels.ToDictionary(x => x.uid);

        var needAdd = dictOffice.Keys.Except(dictViewModels.Keys).ToArray();
        var needRemove = dictViewModels.Keys.Except(dictOffice.Keys).ToArray();

        foreach (var key in needRemove)
        {
            viewModels.Remove(dictViewModels[key]);
        }

        foreach (var key in needAdd)
        {
            var view = new OfficeViewModel();
            view.uid = key;

            viewModels.Add(view);
        }

        foreach (var view in viewModels)
        {
            view.Refresh(dictOffice[view.uid]);
        }
    }

    public static void Refresh(this OfficeViewModel viewModel, IOffice office)
    {
        viewModel.OfficeName = office.name;

        var person = GetOffice2Person(office);
        if(person == null)
        {
            viewModel.PersonItemView = null;
            return;
        }

        if(viewModel.PersonItemView == null)
        {
            viewModel.PersonItemView = new PersonViewModel();
        }

        viewModel.PersonItemView.Refresh(person);
    }

    public static void Refresh(this PersonViewModel viewModel, IPerson person)
    {
        viewModel.PersonName = person.fullName;
    }

    public static void Refresh(this List<PersonView> personViews, IEnumerable<IPerson> persons)
    {
        var dictPerson = persons.ToDictionary(x => x.uid);
        var dictViewModels = personViews.ToDictionary(x => x.uuid);

        var needAdd = dictPerson.Keys.Except(dictViewModels.Keys).ToArray();
        var needRemove = dictViewModels.Keys.Except(dictPerson.Keys).ToArray();

        foreach (var key in needRemove)
        {
            personViews.Remove(dictViewModels[key]);
        }

        foreach (var key in needAdd)
        {
            var view = new PersonView();
            view.uuid = key;

            personViews.Add(view);
        }

        foreach (var view in personViews)
        {
            view.Refresh(dictPerson[view.uuid]);
        }
    }

    public static void Refresh(this ContainerView containerView, IEnumerable<IDepart> departs)
    {
        containerView.defaultItem.gameObject.SetActive(false);

        var dictDepart = departs.Where(x=>!x.isMain).ToDictionary(x => x.uid);
        var dictView = containerView.items.ToDictionary(x => x.uuid);

        var needAdd = dictDepart.Keys.Except(dictView.Keys).ToArray();
        var needRemove = dictView.Keys.Except(dictDepart.Keys).ToArray();

        foreach (var key in needRemove)
        {
            containerView.RemoveItem(dictView[key]);
        }

        foreach (var key in needAdd)
        {
            var itemView =containerView.AddItem();
            itemView.gameObject.SetActive(true);
            itemView.uuid = key;
        }

        foreach (var itemView in containerView.items)
        {
            itemView.Refresh(dictDepart[itemView.uuid]);
        }
    }

    public static void Refresh(this PersonView personView, IPerson person)
    {
        personView.name = person.fullName;
        personView.money = (float)person.money;
    }

    public static void Refresh(this ItemView itemView, IDepart depart)
    {
        var departView = itemView as DepartItemView;
        departView.departName.text = depart.name;

        var leader = GetOffice2Person(depart.leaderOffice);
        departView.leader.gameObject.SetActive(leader != null);

        if (leader != null)
        {
            departView.leader.personName.text = leader.fullName;
        }
    }

    public static void Refresh(this SectView sectView, ISect sect)
    {
        var leader = GetOffice2Person(sect.leaderOffice);
        sectView.leader.gameObject.SetActive(leader != null);

        if (leader != null)
        {
            sectView.leader.personName.text = leader.fullName;
        }
    }
}