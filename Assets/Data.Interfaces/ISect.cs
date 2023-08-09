using System.Collections.Generic;

namespace JiangH.Data.Interfaces
{
    public interface ISect
    {
        IOffice leaderOffice { get; }
        IEnumerable<IOffice> senatorOffices { get; }
    }

    public interface IOffice
    {
        string uid { get; }

        OfficeType type { get; }
        string name { get; }
    }

    public enum OfficeType
    {
        SectLeader,
        SectMaster,
        DepartLeader,
    }
}