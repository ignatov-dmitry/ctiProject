using Logics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using db;
namespace Logics
{
    public class ShipmentLogic
    {
        private EFRepository<Shipment> shipment;
        private EFRepository<Shipment_Array> shipment_array;
        private EFRepository<Auto> auto;
        private EFRepository<Driver> driver;
        private EFRepository<Claim> claim;
        public bool PrihClaimNewShipment(int idClaim)
        {
            var obObchClaim = ClaimOB(idClaim);
            return LogicShip(obObchClaim,idClaim);
        }
        private double ClaimOB(int idClaim)
        {
            claim = new EFRepository<Claim>(new ApplicationDbContext());
            var claimView = claim.FindById(idClaim);
            Double ob = 0;
            foreach (var item in claimView.ClaimArray)
            {
                ob += (double.Parse(item.Count.ToString()) * item.FinishedProducts.Packaging.Amount);
            }
            return ob;
        }
        private bool LogicShip(Double obClaim,int idClaim)
        {
            var state = false;
            shipment = new EFRepository<Shipment>(new ApplicationDbContext());
            var ss = shipment.Get();
            var dd = shipment.GetWithInclude(x => x.shipment_Arrays).ToList();
            auto = new EFRepository<Auto>(new ApplicationDbContext());
            int countAuto = 0;
            foreach (var item in auto.Get())
            {
                if (item.Status)
                {
                    ++countAuto;
                }
            }
            if (auto.Get().Count() == countAuto)
            {
                return true;
            }
            if (shipment.Get().Count()!=0)
            {
                foreach (var item in dd)
                {
                    if (item.MassOstAuto > obClaim && item.PrinVyhOtpr == null )
                    {
                        AddEstShip(obClaim, idClaim, item);
                        break;
                    }
                    else if (item.MassOstAuto <= obClaim && item.Status == null || item.MassOstAuto > obClaim && item.Status == null)
                    {
                        return AddNewShip(obClaim, idClaim);
                        
                    }
                    
                }
            }
            else if(shipment.Get().Count() == 0)
            {
                AddNewShip(obClaim, idClaim);
            }
            return state;
           
        }
        private void AddEstShip(Double obClaim,Int32 idClaim,Shipment ship)
        {
            claim = new EFRepository<Claim>(new ApplicationDbContext());
            var claimView = claim.FindById(idClaim);
            var user = new EFRepository<ApplicationUser>(new ApplicationDbContext());
            shipment = new EFRepository<Shipment>(new ApplicationDbContext());
            shipment_array = new EFRepository<Shipment_Array>(new ApplicationDbContext());
            ship.MassOstAuto -= obClaim;
            shipment_array.Create(new Shipment_Array() { DatePrin = DateTime.Now,ClaimId = idClaim,ObchMasClaim = obClaim,IdShipment=ship.Id, NameClient = user.FindById(claimView.IdClient.ToString()).UserName });
            shipment.Update(ship);
        }
        private bool AddNewShip(Double obClaim, Int32 idClaim)
        {
            var ship = new Shipment();
            List<Auto> ts = new List<Auto>();
            List<Auto> ast = new List<Auto>();
            var db = new ApplicationDbContext();
            foreach (var item in db.Auto.ToList())
            {
                if (db.Shipment.ToList().Count() != 0)
                {
                    foreach (var items in db.Shipment.ToList())
                    {
                        if (item.Id != items.AutoId)
                        {
                            ts.Add(item);
                        }
                        else
                        {
                            ast.Add(item);
                        }
                    }
                }
                else if (db.Shipment.ToList().Count() == 0)
                {
                    ts.Add(item);
                }
            }

            claim = new EFRepository<Claim>(new ApplicationDbContext());
            var claimView = claim.FindById(idClaim);
            claimView.OtprAuto = DateTime.Now;
            var vd = ts.GroupBy(x => x.Id).Select(x => x.First()).ToList();
           
            foreach (var item in ast)
            {
                vd.Remove(item);
            }
            if (vd.Count == 0)
            {
                return true;
            }
            ship.AutoId = vd.First().Id;
            ship.MassOstAuto = vd.First().ObmKuz;
            if (db.Shipment.ToList().Count() != 0)
            {
                ship.MassOstAuto -= obClaim;
            }
            else if (db.Shipment.ToList().Count() == 0)
            {
                ship.MassOstAuto = obClaim;
            }
            var ars = new Shipment_Array() { DatePrin = DateTime.Now, ClaimId = idClaim, ObchMasClaim = obClaim,NameClient = db.Users.Find(claimView.IdClient.ToString()).UserName };
            ship.shipment_Arrays.Add(ars);
            db.Shipment.Add(ship);
            
            db.SaveChanges();
            return false;
        }
        public IEnumerable<Shipment> ShipList()
        {
            shipment = new EFRepository<Shipment>(new ApplicationDbContext());
            return shipment.Get();
        }
        public void ShipArr(List<Shipment_Array> arr)
        {
            claim = new EFRepository<Claim>(new ApplicationDbContext());
            foreach (var item in arr)
            {
                var cl = claim.FindById(item.ClaimId.Value);
                cl.OtprAuto = DateTime.Now;
                claim.Update(cl);
            }
        }
    }
}
