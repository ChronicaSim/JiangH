using JiangH.Data.Interfaces;

namespace JiangH.Data.Offices
{
    public class Office : IOffice
    {
        private static int count;

        public string uid { get; set; }

        public OfficeType type { get; set; }

        public string name { get; set; }

        public Office()
        {
            uid = count++.ToString();
        }
    }
}