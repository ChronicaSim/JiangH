using JiangH.Data.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace JiangH.Data.Sects
{
    public class SectManager : IEnumerable<ISect>
    {
        private List<Sect> sects = new List<Sect>();

        public IEnumerator<ISect> GetEnumerator()
        {
            return ((IEnumerable<ISect>)sects).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)sects).GetEnumerator();
        }

        public SectManager()
        {
            for(int i=0; i<3; i++)
            {
                sects.Add(new Sect());
            }
        }
    }
}
