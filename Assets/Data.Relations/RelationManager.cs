using JiangH.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace JiangH.Sessions
{
    public class RelationManager : IRelationQuery
    {
        public Dictionary<IPerson, IDepart> person2Depart = new Dictionary<IPerson, IDepart>();
        public Dictionary<IDepart, List<IPerson>> depart2Persons = new Dictionary<IDepart, List<IPerson>>();

        public Dictionary<IDepart, ISect> depart2Sect = new Dictionary<IDepart, ISect>();
        public Dictionary<ISect, List<IDepart>> sect2Departs = new Dictionary<ISect, List<IDepart>>();

        public IDepart GetPerson2Depart(IPerson person)
        {
            return person2Depart[person];
        }

        public IEnumerable<IPerson> GetDepart2Persons(IDepart person)
        {
            return depart2Persons[person];
        }

        public void Initialize(IEnumerable<IPerson> persons, IEnumerable<IDepart> departs, IEnumerable<ISect> sects)
        {
            for (int i = 0; i < departs.Count(); i++)
            {
                var depart = departs.ElementAt(i);

                var sect = sects.ElementAt(i % sects.Count());

                Associate(depart, sect);
            }

            for (int i=0; i< persons.Count(); i++)
            {
                var person = persons.ElementAt(i);

                var depart = departs.ElementAt(i% departs.Count());

                Associate(person, depart);
            }

            foreach(var departsInSect in sect2Departs.Values)
            {
                departsInSect.First().isMain = true;
            }

            foreach(var personsInDepart in depart2Persons.Values)
            {
                var player = personsInDepart.SingleOrDefault(x => x.isPlayer);
                if(player != null)
                {
                    player.isLeader = true;
                    continue;
                }

                personsInDepart.First().isLeader = true;
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

        private void Associate(IDepart depart, ISect sect)
        {
            depart2Sect[depart] = sect;

            if(!sect2Departs.TryGetValue(sect, out var departs))
            {
                departs = new List<IDepart>();
                sect2Departs.Add(sect, departs);
            }

            departs.Add(depart);
        }

        public ISect GetSectByDepart(IDepart depart)
        {
            return depart2Sect[depart];
        }

        public IEnumerable<IDepart> GetDepartsBySect(ISect sect)
        {
            return sect2Departs[sect];
        }
    }
}
