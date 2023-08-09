using System.Collections.Generic;

namespace JiangH.Data.Interfaces
{
    public interface IRelationQuery
    {
        IDepart GetPerson2Depart(IPerson person);

        IEnumerable<IPerson> GetDepart2Persons(IDepart person);
        ISect GetSectByDepart(IDepart depart);
        IEnumerable<IDepart> GetDepartsBySect(ISect sect);
        IPerson GetOffice2Persons(IOffice office);
    }
}