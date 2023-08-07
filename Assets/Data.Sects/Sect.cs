using JiangH.Data.Interfaces;
using System.Collections.Generic;

namespace JiangH.Data.Sects
{
    public class Sect : ISect
    {
        public IOffice leaderOffice => throw new System.NotImplementedException();

        public IEnumerable<IOffice> masterOffices => throw new System.NotImplementedException();
    }
}
