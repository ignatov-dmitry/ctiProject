using Logics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using db;

namespace Logics
{
    public class ClaimLogic
    {
        private EFRepository<Claim> Claim;
        private EFRepository<ClaimArray> ClaimArray;
        private EFRepository<Gild> Gild;
        public ClaimLogic()
        {
            Claim = new EFRepository<Claim>(new ApplicationDbContext());
            ClaimArray = new EFRepository<ClaimArray>(new ApplicationDbContext());
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
        }
        public IEnumerable<Claim> ClaimInList()
        {
            var db = new ApplicationDbContext();
            var users = db.Users.Where(x => x.InAndOut == true);
            var ClaimIN = Claim.GetWithInclude(x=>x.ClaimArray);
            List<Claim> cls = new List<Claim>();
            foreach (var item in ClaimIN)
            {
                foreach (var us in users)
                {
                    if (item.IdClient.ToString() == us.Id)
                    {
                        cls.Add(item);
                    }
                }
            }
            return cls;
        }
        public IEnumerable<Claim> ClaimOutList()
        {
            var db = new ApplicationDbContext();
            var users = db.Users.Where(x => x.InAndOut == false);
            var ClaimIN = Claim.GetWithInclude(x => x.ClaimArray);
            List<Claim> cls = new List<Claim>();
            foreach (var item in ClaimIN)
            {
                foreach (var us in users)
                {
                    if (item.IdClient.ToString() == us.Id)
                    {
                        cls.Add(item);
                    }
                }
            }
            return cls;
        }

        public Gild GildView(int id)
        {
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
            return Gild.FindById(id);
        }
    }
}
