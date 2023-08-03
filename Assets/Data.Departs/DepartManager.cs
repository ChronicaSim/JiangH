using JiangH.Data.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace JiangH.Data.Departs
{
    public class DepartManager : IEnumerable<IDepart>
    {
        private List<Depart> departs = new List<Depart>();

        public IEnumerator<IDepart> GetEnumerator()
        {
            return ((IEnumerable<IDepart>)departs).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)departs).GetEnumerator();
        }

        public DepartManager()
        {
            for (int i = 0; i < 10; i++)
            {
                departs.Add(new Depart());
            }
        }
    }
}

