using JiangH.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JiangH.Data.Persons
{
    public class PersonManager : IEnumerable<IPerson>
    {
        private List<Person> persons = new List<Person>();

        public IEnumerator<IPerson> GetEnumerator()
        {
            return ((IEnumerable<IPerson>)persons).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)persons).GetEnumerator();
        }

        public void OnNextTurn()
        {
            foreach(var person in persons)
            {
                person.money -= 0.1m;
            }
        }

        public PersonManager()
        {
            for(int i=0; i<100; i++)
            {
                persons.Add(new Person());
            }

            persons.First().isPlayer = true;
        }
    }
}