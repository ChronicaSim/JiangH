using JiangH.Data.Interfaces;
using JiangH.Data.Offices;

namespace JiangH.Data.Departs
{
    class Depart : IDepart
    {
        private static int count;

        public string uid { get; }

        public string name { get; }

        public bool isMain { get; set; }

        public IOffice leaderOffice { get; } = new Office() { name = "DepartLeader" };

        public Depart()
        {
            uid = count++.ToString();

            name = $"Depart{uid}";
        }
    }
}