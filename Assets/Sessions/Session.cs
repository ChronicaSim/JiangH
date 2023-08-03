using JiangH.Data.Departs;
using JiangH.Data.Interfaces;
using JiangH.Data.Persons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JiangH.Sessions
{
    public class Session
    {
        public IPerson player => personManager.Single(x => x.isPlayer);

        public Relation relation = new Relation();

        private PersonManager personManager;
        private DepartManager departManager;

        public Session()
        {
            personManager = new PersonManager();
            departManager = new DepartManager();

            relation.Initialize(personManager, departManager);
        }

        public void OnNextTurn()
        {
            personManager.OnNextTurn();
        }
    }

    public class Relation
    {
        public Dictionary<IPerson, IDepart> person2Depart = new Dictionary<IPerson, IDepart>();
        public Dictionary<IDepart, List<IPerson>> depart2Persons = new Dictionary<IDepart, List<IPerson>>();

        public IDepart GetPerson2Depart(IPerson person)
        {
            return person2Depart[person];
        }

        public IEnumerable<IPerson> GetDepart2Persons(IDepart person)
        {
            return depart2Persons[person];
        }

        internal void Initialize(PersonManager personManager, DepartManager departManager)
        {
            for(int i=0; i< personManager.Count(); i++)
            {
                var person = personManager.ElementAt(i);

                var depart = departManager.ElementAt(i% departManager.Count());

                Associate(person, depart);
            }
        }

        private void Associate(IPerson person, IDepart depart)
        {
            person2Depart[person] = depart;

            if(!depart2Persons.TryGetValue(depart, out var persons))
            {
                persons = new List<IPerson>();
                depart2Persons.Add(depart, persons);
            }

            persons.Add(person);
        }
    }
}
