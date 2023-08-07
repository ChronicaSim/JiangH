using System.Collections.Generic;

namespace JiangH.Data.Interfaces
{
    public interface ISect
    {
        IOffice leaderOffice { get; }
        IEnumerable<IOffice> masterOffices { get; }
    }

    public interface IOffice
    {
        OfficeType type { get; }
    }

    public enum OfficeType
    {
        SectLeader,
        SectMaster,
        DepartLeader,
    }
}