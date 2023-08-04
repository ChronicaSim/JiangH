using JiangH.Data.Interfaces;
using JiangH.Sessions;
using JiangH.Views;
using System;
using System.Collections.Generic;
using System.Linq;

static class ViewRefreshExtension
{
    public static Func<IDepart, IEnumerable<IPerson>> GetDepart2Persons;

    public static void ClearDesignData(this MainView mainView)
    {
        mainView.persons.Clear();
    }

    public static void Refresh(this MainView mainView, Session session)
    {
        GetDepart2Persons = session.relationQuery.GetDepart2Persons;

        var depart = session.relationQuery.GetPerson2Depart(session.player);
        var persons = session.relationQuery.GetDepart2Persons(depart);

        var sect = session.relationQuery.GetSectByDepart(depart);
        var departs = session.relationQuery.GetDepartsBySect(sect);

        mainView.mainDepart.Refresh(departs.Single(x=>x.isMain));
        mainView.departs.Refresh(departs);
        mainView.persons.Refresh(persons);
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

        var persons = GetDepart2Persons(depart);

        var leader = persons.SingleOrDefault(x => x.isLeader);
        departView.leader.gameObject.SetActive(leader != null);

        if (leader != null)
        {
            departView.leader.personName.text = leader.fullName;
        }
    }
}