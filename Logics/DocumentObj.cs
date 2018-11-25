using db;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Logics
{
    [Authorize]
    public class DocumentObj 
    {
        private ApplicationDbContext db;
        public DocumentObj()
        {
            db = new ApplicationDbContext();
        }
        public void Add(Document name)
        {
            throw new NotImplementedException();
        }

        public void Edit(Document name)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        

        public Document ViewsObj(int id)
        {
            throw new NotImplementedException();
        }
    }
}
