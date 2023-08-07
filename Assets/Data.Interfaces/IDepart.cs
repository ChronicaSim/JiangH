namespace JiangH.Data.Interfaces
{
    public interface IDepart
    {
        string uid { get; }

        bool isMain { get; set; }

        string name { get; }

        IOffice leaderOffice { get; }
    }
}