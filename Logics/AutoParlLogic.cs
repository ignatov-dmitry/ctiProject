using db;
using Logics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessAnalytics.Models;

namespace Logics
{
    public class AutoParlLogic
    {
        private EFRepository<Auto> Auto;
        private EFRepository<Driver> Driver;
        public IList<Auto> AutoAllList()
        {
            Auto = new EFRepository<Auto>(new ApplicationDbContext());
            return Auto.Get().ToList();
        }
        public IList<Auto> AutoNoneDriverList()
        {
            Auto = new EFRepository<Auto>(new ApplicationDbContext());
            Driver = new EFRepository<Driver>(new ApplicationDbContext());
            List<Auto> autos = Auto.Get().ToList();
            List<Auto> ts = new List<Auto>();
            var s = (from sd in Driver.Get()
                    where sd.AutoId != null
                    select sd.AutoId).ToList();
            List<Auto> ast = new List<Auto>(); 

            foreach (var item in autos)
            {
                foreach (var items in s)
                {
                    if (item.Id != items.Value)
                    {
                        ts.Add(item);
                    }
                    else
                    {
                        ast.Add(item);
                    }
                }
            }
                
            
            var vd = ts.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            foreach (var item in ast)
            {
                vd.Remove(item);
            }
            return vd;
        }
        public Task CreateNewAuto(Auto auto)
        {
            return Task.Run(() =>
            {
                Auto = new EFRepository<Auto>(new ApplicationDbContext());
                Auto.Create(auto);
            });
        }
        public Task RemoveAutoIn(int id)
        {
            return Task.Run(() =>
            {
                Auto = new EFRepository<Auto>(new ApplicationDbContext());
                var p = Auto.FindById(id);
                Auto.Remove(p);
            });
        }
        public Task EditAutoIn(Auto auto)
        {
            return Task.Run(() =>
            {
                Auto = new EFRepository<Auto>(new ApplicationDbContext());
                Auto.Update(auto);
            });
        }

        public IEnumerable<ViewDriver> DriverAllList()
        {
            Driver = new EFRepository<Driver>(new ApplicationDbContext());
            Auto = new EFRepository<Auto>(new ApplicationDbContext());
            var view = from db in Driver.Get()
                       select new ViewDriver
                       {
                           Id = db.Id,
                           FIO = db.FIO,
                           NumprPrav = db.NumprPrav,
                           Phone = db.Phone,
                           Auto = Auto.FindById(db.AutoId.Value).Marka + "-" + Auto.FindById(db.AutoId.Value).Model
                       };
            return view;
        }
        public Auto ViewOneAuto(int id)
        {
            Auto = new EFRepository<Auto>(new ApplicationDbContext());
            return Auto.FindById(id);
        }
        public Driver ViewOneDriver(int id)
        {
            Driver = new EFRepository<Driver>(new ApplicationDbContext());
            return Driver.FindById(id);
        }
        public Task CreateNewDriver(Driver driver)
        {
            return Task.Run(() =>
            {
                Driver = new EFRepository<Driver>(new ApplicationDbContext());
                Driver.Create(driver);
            });
        }
        public Task RemoveDriverIn(int id)
        {
            return Task.Run(() =>
            {
                Driver = new EFRepository<Driver>(new ApplicationDbContext());
                var p = Driver.FindById(id);
                Driver.Remove(p);
            });
        }
        public Task EditDriverIn(Driver driver)
        {
            return Task.Run(() =>
            {
                Driver = new EFRepository<Driver>(new ApplicationDbContext());
                Driver.Update(driver);
            });
        }
    }
}
