using JiangH.Data.Interfaces;
using JiangH.Data.Offices;
using System.Collections.Generic;

namespace JiangH.Data.Sects
{
    public class Sect : ISect
    {
        public IOffice leaderOffice { get; } = new Office() { name = "SectLeader" };

        public IOffice viceLeaderOffice { get; } = new Office() { name = "ViceSectLeader" };

        public IEnumerable<IOffice> senatorOffices { get; } = new Office[]
        {
            new Office() { name = "Senator" },
            new Office() { name = "Senator" },
            new Office() { name = "Senator" },
            new Office() { name = "Senator" }
        };
    }
}
