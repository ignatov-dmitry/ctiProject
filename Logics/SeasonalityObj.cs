using db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Logics
{
    [Authorize]
    public class SeasonalityObj : ILogic<Seasonality>
    {
        private ApplicationDbContext db;
        public SeasonalityObj()
        {
            db = new ApplicationDbContext();
        }
        public void Add(Seasonality name)
        {
            throw new NotImplementedException();
        }

        public void Edit(Seasonality name)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public List<Seasonality> ViewsListObj()
        {
            var jsondata = db.Seasonality.ToList<Seasonality>();
            return jsondata;
        }

        public Seasonality ViewsObj(int id)
        {
            throw new NotImplementedException();
        }
    }
}
