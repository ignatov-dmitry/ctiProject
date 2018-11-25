using db;
using Logics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logics
{
    public class SkladLogic
    {
        private EFRepository<Product> product;
        private EFRepository<RawMaterialStatistics> statistic;
        private EFRepository<Raw_Product> Raw_Product;
        private EFRepository<Packaging> Packaging;
        private EFRepository<ProductStatistics> ProductStatistics;
        private EFRepository<Packaging_ProductStatistics> Packaging_ProductStatistics;
        private EFRepository<FinishedProducts> FinishedProducts;
        private EFRepository<FinishedProducts_FinishedGoodsStatistics> FinishedProducts_FinishedGoodsStatistics;
        private EFRepository<FinishedGoodsStatistics> FinishedGoodsStatistics;
        private EFRepository<Gild> Gild;
        private EFRepository<GildStatic> GildStatic;
        private EFRepository<Gild_GildStatic> Gild_GildStatic;
        private EFRepository<Auto> Auto;
        private EFRepository<Condiments> Condiments;
        private EFRepository<CondimentStatic> CondimentStatic;
        private EFRepository<Condiments_Static> Condiments_Static;
        public SkladLogic()
        {
            product = new EFRepository<Product>(new ApplicationDbContext());
            statistic = new EFRepository<RawMaterialStatistics>(new ApplicationDbContext());
            Raw_Product = new EFRepository<Raw_Product>(new ApplicationDbContext());
            Packaging = new EFRepository<Packaging>(new ApplicationDbContext());
            ProductStatistics = new EFRepository<ProductStatistics>(new ApplicationDbContext());
            Packaging_ProductStatistics = new EFRepository<db.Packaging_ProductStatistics>(new ApplicationDbContext());
            FinishedGoodsStatistics = new EFRepository<db.FinishedGoodsStatistics>(new ApplicationDbContext());
            FinishedProducts = new EFRepository<db.FinishedProducts>(new ApplicationDbContext());
            FinishedProducts_FinishedGoodsStatistics = new EFRepository<db.FinishedProducts_FinishedGoodsStatistics>(new ApplicationDbContext());
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
            GildStatic = new EFRepository<GildStatic>(new ApplicationDbContext());
            Gild_GildStatic = new EFRepository<Gild_GildStatic>(new ApplicationDbContext());
            Auto = new EFRepository<db.Auto>(new ApplicationDbContext());
        }
        #region Сырье
        public Task AddProduct(Product obj)
        {
            return Task.Run(() =>
            {
                if (product.Get(x => x.Name == obj.Name).Any())
                {
                    var pr = product.Get(x => x.Name == obj.Name).First();
                    RawMaterialStatistics stat = new RawMaterialStatistics();
                    stat.Count = obj.Count;
                    stat.Date = DateTime.Now;
                    pr.Count += obj.Count;
                    Task.Factory.StartNew(() => { product.Update(pr); }).Wait();

                    Task.Factory.StartNew(() => { statistic.Create(stat); }).Wait();

                    var st = statistic.Get(x => x.Count == stat.Count && x.Date.Value.Day == stat.Date.Value.Day && x.Date.Value.Month == stat.Date.Value.Month && x.Date.Value.Year == stat.Date.Value.Year && x.Date.Value.Hour == stat.Date.Value.Hour && x.Date.Value.Minute == stat.Date.Value.Minute && x.Date.Value.Second == stat.Date.Value.Second).First();
                    var prod = product.Get(x => x.Count == obj.Count && x.Name == obj.Name).First();

                    var rw = new db.Raw_Product();
                    rw.ProductId = prod.Id;
                    rw.RawId = st.Id;
                    Task.Factory.StartNew(() => { Raw_Product.Create(rw); }).Wait();

                }
                else
                {
                    RawMaterialStatistics stat = new RawMaterialStatistics();
                    stat.Count = obj.Count;
                    stat.Date = DateTime.Now;
                    Task.Factory.StartNew(() =>
                    {
                        product.Create(obj);
                    }).Wait();
                }

            });
        }
        //приход на склад сырья
        public Task PrihodSyr(Product obj)
        {
            return Task.Run(() =>
            {
                var prodo = product.FindById(obj.Id);

                RawMaterialStatistics stat = new RawMaterialStatistics();
                stat.Id = 0;
                stat.Count = obj.Count;
                stat.Date = DateTime.Now;

                prodo.Count += obj.Count;

                Task.Factory.StartNew(() =>
                {
                    product.Update(prodo);
                }).Wait();
                Task.Factory.StartNew(() =>
                {
                    statistic.Create(stat);
                }).Wait();
                var st = statistic.Get(x => x.Count == stat.Count && x.Date.Value.Day == stat.Date.Value.Day && x.Date.Value.Month == stat.Date.Value.Month && x.Date.Value.Year == stat.Date.Value.Year && x.Date.Value.Hour == stat.Date.Value.Hour && x.Date.Value.Minute == stat.Date.Value.Minute && x.Date.Value.Second == stat.Date.Value.Second).First();

                Raw_Product rw = new db.Raw_Product();
                rw.ProductId = obj.Id;
                rw.RawId = st.Id;
                Task.Factory.StartNew(() =>
                {
                    Raw_Product.Create(rw);
                }).Wait();
            });
        }
        public IEnumerable<Product> GetProductList()
        {
            return product.Get();
        }
        #endregion
        #region Упковка
        public IEnumerable<Packaging> PackagingList()
        {
            return Packaging.Get();
        }
        public Task PackagingAdd(Packaging pack)
        {
            return Task.Run(() =>
            {
                Task.Factory.StartNew(() =>
                {
                    Packaging.Create(pack);
                }).Wait();
                ProductStatistics prod = new ProductStatistics();
                prod.Count = pack.Count;
                prod.Date = DateTime.Now;
                Task.Factory.StartNew(() =>
                {
                    ProductStatistics.Create(prod);
                }).Wait();
                ProductStatistics now = ProductStatistics.Get(x => x.Count == prod.Count && x.Date.Value.Day == prod.Date.Value.Day && x.Date.Value.Month == prod.Date.Value.Month && x.Date.Value.Year == prod.Date.Value.Year && x.Date.Value.Hour == prod.Date.Value.Hour && x.Date.Value.Minute == prod.Date.Value.Minute && x.Date.Value.Second == prod.Date.Value.Second).First();
                Packaging packNow = Packaging.Get(x => x.Count == pack.Count && x.Name == pack.Name).First();
                Packaging_ProductStatistics statistics = new Packaging_ProductStatistics() { PackagingId = packNow.Id, ProductStatisticsId = now.Id };
                Task.Factory.StartNew(() =>
                {
                    Packaging_ProductStatistics.Create(statistics);
                }).Wait();
            });
        }
        public Task PrihodPack(Packaging pack)
        {
            return Task.Run(() =>
            {
                var pck = Packaging.FindById(pack.Id);
                pck.Count += pack.Count;
                Task.Factory.StartNew(() =>
                {
                    Packaging.Update(pck);
                }).Wait();
                ProductStatistics prod = new ProductStatistics();
                prod.Count = pack.Count;
                prod.Date = DateTime.Now;
                Task.Factory.StartNew(() =>
                {
                    ProductStatistics.Create(prod);
                }).Wait();
                ProductStatistics now = ProductStatistics.Get(x => x.Count == prod.Count && x.Date.Value.Day == prod.Date.Value.Day && x.Date.Value.Month == prod.Date.Value.Month && x.Date.Value.Year == prod.Date.Value.Year && x.Date.Value.Hour == prod.Date.Value.Hour && x.Date.Value.Minute == prod.Date.Value.Minute && x.Date.Value.Second == prod.Date.Value.Second).First();
                Packaging_ProductStatistics statistics = new Packaging_ProductStatistics() { PackagingId = pack.Id, ProductStatisticsId = now.Id };
                Task.Factory.StartNew(() =>
                {
                    Packaging_ProductStatistics.Create(statistics);
                }).Wait();
            });
        }
        #endregion
        #region Цех
        public IEnumerable<Gild> GildsViewList()
        {
            return Gild.Get();
        }
        /// <summary>
        /// Добавление гильдии
        /// </summary>
        /// <param name="gild"></param>
        public bool GildAdd(Gild gild, double Con)
        {
            
            
                Condiments = new EFRepository<Condiments>(new ApplicationDbContext());
                GildStatic gl = new GildStatic();
                if (gild.ProductId != null)
                {
                    var prod = product.FindById(gild.ProductId.Value);
                    gild.Name = prod.Name;
                    prod.Count -= gild.Count;
                    if (prod.Count < 0)
                    {
                        return false;
                    }

                    Task.Factory.StartNew(() =>
                    {
                        product.Update(prod);
                    }).Wait();
                    
                }
                else
                {
                    var con = Condiments.FindById(gild.CondimentsId.Value);
                    con.Count -= Con;
                    if (con.Count < 0)
                    {
                        return false;
                    }
                    Task.Factory.StartNew(() =>
                    {
                        Condiments.Update(con);
                    }).Wait();
                    var sp = Condiments.FindById(gild.CondimentsId.Value);
                    gild.Name = sp.Name;
                
                }
                if (Gild.Get(x => x.Name == gild.Name).Any())
                {
                    var gls = Gild.Get(x => x.Name == gild.Name).First();

                    if (gild.ProductId != null)
                    {
                        gls.Count += gild.Count;
                        gl.CountGild = gild.Count;
                    }
                    else
                    {
                        gls.Count += int.Parse(Con.ToString());
                        gl.CountGild = Con;
                        gl.CountCondiments = Con;
                    }
                    Task.Factory.StartNew(() =>
                    {
                        Gild.Update(gls);
                    }).Wait();
                    gl.Date = DateTime.Now;
                    Task.Factory.StartNew(() =>
                    {
                        GildStatic.Create(gl);
                    }).Wait();
                    GildStatic b = GildStatic.Get(x => x.CountGild == gl.CountGild && x.Date.Day == gl.Date.Day && x.Date.Month == gl.Date.Month && x.Date.Year == gl.Date.Year && x.Date.Hour == gl.Date.Hour && x.Date.Minute == gl.Date.Minute && x.Date.Second == gl.Date.Second).First();
                    Task.Factory.StartNew(() =>
                    {
                        Gild_GildStatic.Create(new db.Gild_GildStatic() { GildId = gls.Id, GildStaticId = b.Id });
                    }).Wait();
                    return true;
                }
                else
                {

                    if (gild.ProductId != null)
                    {

                        gl.CountGild = gild.Count;
                    }
                    else
                    {
                        gild.Count += int.Parse(Con.ToString());
                        gl.CountGild = Con;
                    }
                    Task.Factory.StartNew(() =>
                    {
                        Gild.Create(gild);
                    }).Wait();
                    gl.Date = DateTime.Now;
                    Task.Factory.StartNew(() =>
                    {
                        GildStatic.Create(gl);
                    }).Wait();
                    var a = Gild.Get(x => x.Name == gild.Name && x.Count == gild.Count).First();
                    GildStatic b = GildStatic.Get(x => x.CountGild == gl.CountGild && x.Date.Day == gl.Date.Day && x.Date.Month == gl.Date.Month && x.Date.Year == gl.Date.Year && x.Date.Hour == gl.Date.Hour && x.Date.Minute == gl.Date.Minute && x.Date.Second == gl.Date.Second).First();
                    Task.Factory.StartNew(() =>
                    {
                        Gild_GildStatic.Create(new db.Gild_GildStatic() { GildId = a.Id, GildStaticId = b.Id });
                    }).Wait();
                    return true;
                }
            


            
        }
        public Gild getGild(int id)
        {
            return Gild.FindById(id);
        }
        /// <summary>
        /// Поменять!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="gild"></param>
        public List<GildStatic> gildStaticsList()
        {
            return GildStatic.Get(js => js.Date.Day == DateTime.Now.Day && js.Date.Month == DateTime.Now.Month && js.Date.Year == DateTime.Now.Year).ToList();
        }
        public Task ModGild(Gild gild, double Con)
        {
            Condiments = new EFRepository<Condiments>(new ApplicationDbContext());
            return Task.Run(() =>
            {
                var prod = product.FindById(gild.ProductId.Value);
                prod.Count -= gild.Count;
                Task.Factory.StartNew(() =>
                {
                    product.Update(prod);
                }).Wait();
                var gld = Gild.FindById(gild.Id);
                gild.Count += gld.Count;
                Task.Factory.StartNew(() =>
                {
                    Gild.Update(gild);
                }).Wait();
                var con = Condiments.FindById(gild.CondimentsId.Value);
                con.Count -= Con;
                Task.Factory.StartNew(() =>
                {
                    Condiments.Update(con);
                }).Wait();
                GildStatic gl = new GildStatic();
                gl.Date = DateTime.Now;
                gl.CountGild = gild.Count;
                gl.CountCondiments = Con;
                Task.Factory.StartNew(() =>
                {
                    GildStatic.Create(gl);
                }).Wait();
                var b = GildStatic.Get(x => x.CountGild == gl.CountGild && x.Date.Day == gl.Date.Day && x.Date.Month == gl.Date.Month && x.Date.Year == gl.Date.Year && x.Date.Hour == gl.Date.Hour && x.Date.Minute == gl.Date.Minute && x.Date.Second == gl.Date.Second).First();
                Task.Factory.StartNew(() =>
                {
                    Gild_GildStatic.Create(new db.Gild_GildStatic() { GildId = gild.Id, GildStaticId = b.Id });
                }).Wait();
            });
        }
        #endregion
        #region Готовая продукция
        public IEnumerable<FinishedProducts> GetFinishedProducts()
        {
            return FinishedProducts.Get();
        }
        public Task AddFinishedProduct(FinishedProducts finishedProducts)
        {
            return Task.Run(() =>
            {
                FinishedProducts.Create(finishedProducts);
            });

        }
        public void InFinishedProduct(FinishedProducts finishedProducts)
        {
            List<int> CountSpec = new List<int>();
            List<int> CountProd = new List<int>();
            Gild = new EFRepository<Gild>(new ApplicationDbContext());
            var prod = FinishedProducts.FindById(finishedProducts.Id);
            prod.Count += finishedProducts.Count;
            var pack = Packaging.FindById(prod.PackagingId.Value);
            pack.Count -= finishedProducts.Count;
            if (pack.Count<0)
            {
                return;
            }
            foreach (var item in prod.FinishGildArray)
            {
                var gild = Gild.FindById(item.GildId.Value);
                if (gild.ProductId != null)
                {
                    CountProd.Add(int.Parse(item.CountSyr.ToString()));
                    gild.Count -= int.Parse(item.CountSyr.ToString());
                    
                    Task.Factory.StartNew(() =>
                    {
                        Gild.Update(gild);
                    }).Wait();
                }
                else
                {

                    gild.Count -= int.Parse(item.CountSpec.ToString());
                    CountSpec.Add(int.Parse(item.CountSpec.ToString()));
                    Task.Factory.StartNew(() =>
                    {
                        Gild.Update(gild);
                    }).Wait();
                }

            }
            
            Task.Factory.StartNew(() =>
            {
                Packaging.Update(pack);
            }).Wait();
            Task.Factory.StartNew(() =>
            {
                FinishedProducts.Update(prod);
            }).Wait();
            FinishedGoodsStatistics eF = new FinishedGoodsStatistics() { Count = finishedProducts.Count, Date = DateTime.Now, FinishGildArray = finishedProducts.FinishGildArray, PriceIn = prod.PriceIn, PriceOut = prod.PriceOut, CountPack = finishedProducts.Count};
            Task.Factory.StartNew(() =>
            {
                FinishedGoodsStatistics.Create(eF);
            }).Wait();
            var b = FinishedGoodsStatistics.Get(x => x.Count == eF.Count && x.Date.Value.Day == eF.Date.Value.Day && x.Date.Value.Month == eF.Date.Value.Month && x.Date.Value.Year == eF.Date.Value.Year && x.PriceIn == eF.PriceIn && x.PriceOut == eF.PriceOut && x.Date.Value.Hour == eF.Date.Value.Hour && x.Date.Value.Minute == eF.Date.Value.Minute && x.Date.Value.Second == eF.Date.Value.Second).First().Id;
            Task.Factory.StartNew(() =>
            {
                FinishedProducts_FinishedGoodsStatistics.Create(new db.FinishedProducts_FinishedGoodsStatistics() { FinishedProductsId = prod.Id, FinishedGoodsStatisticsId = b });
            }).Wait();
        }


        #endregion
        #region Приправа
        public IEnumerable<Condiments> CondimentsViewList()
        {
            Condiments = new EFRepository<Condiments>(new ApplicationDbContext());
            return Condiments.Get();
        }
        public Task AddCondiments(Condiments condiments)
        {
            return Task.Run(() =>
            {
                Condiments = new EFRepository<Condiments>(new ApplicationDbContext());
                Condiments.Create(condiments);
            });
        }
        public Task InCondiments(Condiments condiments)
        {
            return Task.Run(() =>
            {
                Condiments = new EFRepository<Condiments>(new ApplicationDbContext());
                CondimentStatic = new EFRepository<CondimentStatic>(new ApplicationDbContext());
                Condiments_Static = new EFRepository<Condiments_Static>(new ApplicationDbContext());
                var skl = Condiments.FindById(condiments.Id);
                skl.Count += condiments.Count;
                Task.Factory.StartNew(() =>
                {
                    Condiments.Update(skl);
                }).Wait();
                var gl = new CondimentStatic() { Count = condiments.Count, Date = DateTime.Now };
                Task.Factory.StartNew(() =>
                {
                    CondimentStatic.Create(gl);
                }).Wait();
                var a = CondimentStatic.Get(x => x.Count == gl.Count && x.Date.Day == gl.Date.Day && x.Date.Month == gl.Date.Month && x.Date.Year == gl.Date.Year && x.Date.Hour == gl.Date.Hour && x.Date.Minute == gl.Date.Minute && x.Date.Second == gl.Date.Second).First();
                Task.Factory.StartNew(() =>
                {
                    Condiments_Static.Create(new db.Condiments_Static() { CondimentsId = condiments.Id, CondimentStaticId = gl.Id });
                }).Wait();
            });
        }
        #endregion

    }
}
