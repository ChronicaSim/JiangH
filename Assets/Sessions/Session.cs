using JiangH.Data.Departs;
using JiangH.Data.Interfaces;
using JiangH.Data.Persons;
using JiangH.Data.Sects;
using System;
using System.Linq;

namespace JiangH.Sessions
{
    public class Session
    {
        public IPerson player => personManager.Single(x => x.isPlayer);

        public IRelationQuery relationQuery => relationManager;

        private PersonManager personManager;
        private DepartManager departManager;
        private SectManager sectManager;
        private RelationManager relationManager;


        public Session()
        {
            personManager = new PersonManager();
            departManager = new DepartManager();
            sectManager = new SectManager();

            relationManager = new RelationManager();
            relationManager.Initialize(personManager, departManager, sectManager);
        }

        public void OnNextTurn()
        {
            personManager.OnNextTurn();
        }
    }
}
