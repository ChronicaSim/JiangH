namespace JiangH.Data.Interfaces
{
    public interface IPerson
    {
        string uid { get; }

        bool isPlayer { get; }

        string fullName { get; }
        decimal money { get; }
        bool isLeader { get; set; }
    }
}