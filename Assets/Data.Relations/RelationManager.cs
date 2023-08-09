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

        public Dictionary<IPerson, List<IOffice>> person2Offices = new Dictionary<IPerson, List<IOffice>>();
        public Dictionary<IOffice, IPerson> office2Person = new Dictionary<IOffice, IPerson>();

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

            foreach(var pair in depart2Persons)
            {
                var depart = pair.Key;
                var personsInDepart = pair.Value;

                var deparLeader = personsInDepart.SingleOrDefault(x => x.isPlayer);
                if(deparLeader == null)
                {
                    deparLeader = personsInDepart.First();
                }

                Associate(deparLeader, depart.leaderOffice);
            }

            foreach (var pair in sect2Departs)
            {
                var sect = pair.Key;
                var departsInSect = pair.Value;

                var departLeaders = departsInSect.Select(x => office2Person[x.leaderOffice]);
                var sectLeader = departLeaders.SingleOrDefault(x => x.isPlayer);
                if(sectLeader == null)
                {
                    sectLeader = departLeaders.FirstOrDefault();
                }

                Associate(sectLeader, sect.leaderOffice);
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

        private void Associate(IPerson person, IOffice office)
        {
            office2Person[office] = person;
            if(!person2Offices.TryGetValue(person, out var offices))
            {
                offices = new List<IOffice>();
                person2Offices.Add(person, offices);
            }

            offices.Add(office);
        }

        public ISect GetSectByDepart(IDepart depart)
        {
            return depart2Sect[depart];
        }

        public IEnumerable<IDepart> GetDepartsBySect(ISect sect)
        {
            return sect2Departs[sect];
        }

        public IPerson GetOffice2Persons(IOffice office)
        {
            if(office2Person.TryGetValue(office, out var person))
            {
                return person;
            }

            return null;
        }
    }
}
