using JiangH.Data.Interfaces;

namespace JiangH.Data.Departs
{
    class Depart : IDepart
    {
        private static int count;

        public string uid { get; }

        public string name { get; }

        public Depart()
        {
            uid = count++.ToString();

            name = $"Depart{uid}";
        }
    }
}