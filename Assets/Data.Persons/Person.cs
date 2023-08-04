using JiangH.Data.Interfaces;

namespace JiangH.Data.Persons
{
    class Person : IPerson
    {
        private static int count;

        public string uid { get; }

        public bool isPlayer { get; internal set; }

        public string fullName { get; }
        public decimal money { get; internal set; }
        public bool isLeader { get ; set ; }

        public Person()
        {
            uid = count++.ToString();
            fullName = $"Name{uid}";
            money = count;
        }
    }
}