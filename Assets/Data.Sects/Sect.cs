using JiangH.Data.Interfaces;
using JiangH.Data.Offices;
using System.Collections.Generic;

namespace JiangH.Data.Sects
{
    public class Sect : ISect
    {
        public IOffice leaderOffice { get; } = new Office() { name = "SectLeader" };

        public IEnumerable<IOffice> senatorOffices => throw new System.NotImplementedException();
    }
}
