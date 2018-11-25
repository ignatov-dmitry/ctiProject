using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logics.Repository;
using db;

namespace Logics
{
    public class BookkeepingLogic
    {
        private EFRepository<FinishedProducts> FinishedProducts;
        private EFRepository<FinishedGoodsStatistics> FinishedGoodsStatistics;
        private EFRepository<FinishedProducts_FinishedGoodsStatistics> FinishedProducts_FinishedGoodsStatistics;
       public BookkeepingLogic()
        {
            FinishedProducts = new EFRepository<FinishedProducts>(new ApplicationDbContext());
            FinishedGoodsStatistics = new EFRepository<FinishedGoodsStatistics>(new ApplicationDbContext());
            FinishedProducts_FinishedGoodsStatistics = new EFRepository<FinishedProducts_FinishedGoodsStatistics>(new ApplicationDbContext());
        }
        public IEnumerable<FinishedProducts> FinishedProductsList()
        {
            return FinishedProducts.Get();
        }
        public void CheckIn(FinishedProducts finishedProducts)
        {
            var prod = FinishedProducts.FindById(finishedProducts.Id);
            prod.PriceIn = finishedProducts.PriceIn;
            FinishedProducts.Update(prod);
        }
        public void CheckOut(FinishedProducts finishedProducts)
        {
            var prod = FinishedProducts.FindById(finishedProducts.Id);
            prod.PriceOut = finishedProducts.PriceOut;
            FinishedProducts.Update(prod);
            FinishedGoodsStatistics eF = new FinishedGoodsStatistics();
            eF.Count = finishedProducts.Count;
            eF.Date = DateTime.Now;
            eF.FinishGildArray = prod.FinishGildArray;
            eF.PriceIn = prod.PriceIn;
            eF.PriceOut = prod.PriceOut;
            FinishedGoodsStatistics.Create(eF);
            var fn = FinishedGoodsStatistics.Get(x => x.Count == eF.Count  && x.Date.Value.Day == eF.Date.Value.Day && x.Date.Value.Month == eF.Date.Value.Month && x.Date.Value.Year == eF.Date.Value.Year && x.PriceIn == eF.PriceIn && x.PriceOut == eF.PriceOut && x.Date.Value.Hour == eF.Date.Value.Hour && x.Date.Value.Minute == eF.Date.Value.Minute && x.Date.Value.Second == eF.Date.Value.Second).First();
            FinishedProducts_FinishedGoodsStatistics.Create(new db.FinishedProducts_FinishedGoodsStatistics() { FinishedProductsId = prod.Id, FinishedGoodsStatisticsId = fn.Id });
        }
    }
}
