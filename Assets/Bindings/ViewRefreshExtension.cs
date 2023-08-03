using JiangH.Data.Interfaces;
using JiangH.Sessions;
using JiangH.Views;
using System.Collections.Generic;
using System.Linq;

static class ViewRefreshExtension
{
    public static void ClearDesignData(this MainView mainView)
    {
        mainView.persons.Clear();
    }

    public static void Refresh(this MainView mainView, Session session)
    {
        var depart = session.relation.GetPerson2Depart(session.player);

        mainView.persons.Refresh(session.relation.GetDepart2Persons(depart));
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

    public static void Refresh(this PersonView personView, IPerson person)
    {
        personView.name = person.fullName;
        personView.money = (float)person.money;
    }
}