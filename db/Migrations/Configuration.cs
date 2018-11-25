namespace db.Migrations
{
    using BusinessAnalytics;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    internal sealed class Configuration : DbMigrationsConfiguration<db.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "manager" };
            var role3 = new IdentityRole { Name = "Buh" };
            var role4 = new IdentityRole { Name = "Client" };
            var role5 = new IdentityRole { Name = "Auto"};
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);
            roleManager.Create(role4);
            roleManager.Create(role5);
            var Document = new List<TypeDocument>()
            {
                new TypeDocument(){ Name = "������"},
                new TypeDocument(){ Name = "����"},
                new TypeDocument(){ Name = "���������"}
            };
            context.TypeDocuments.AddRange(Document);
            var syr = new List<Product>()
            {
                new Product(){Name = "������",Count=1000},
                new Product(){Name = "���������",Count=1000},
                new Product(){Name = "�������",Count=1000},
                new Product(){Name = "���",Count=1000},
                new Product(){Name = "������",Count=1000},
                new Product(){Name = "������ (��������)",Count=1000},
                new Product(){Name = "�������",Count=1000},
                new Product(){Name = "����",Count=1000},
                new Product(){Name = "�����",Count=1000},
                new Product(){Name = "������",Count=1000},
                new Product(){Name = "������ ������������",Count=1000},
                new Product(){Name = "������� (2-� ����)",Count=1000}
            };
            context.Products.AddRange(syr);
            var spec = new List<Condiments>()
            {
                new Condiments(){Name ="�����",Count = 1000},
                new Condiments(){Name ="����",Count = 1000},
                new Condiments(){Name ="����",Count = 1000},
                new Condiments(){Name ="�����",Count = 1000},
                new Condiments(){Name ="���������",Count = 1000},
                new Condiments(){Name ="����� ��������",Count = 1000},
                new Condiments(){Name ="��������",Count = 1000},
                new Condiments(){Name ="����� '����'",Count = 1000}
            };
            context.Condiments.AddRange(spec);
            var pack = new List<Packaging>()
            {
                new Packaging(){Name = "����� 0,5��",Count =1000,Amount =0.5 },
                new Packaging(){Name = "����� 3��",Count =1000,Amount = 3},
                new Packaging(){Name = "����� 'Bag&Box' ����� 5 �.(�� ������)",Count =1000,Amount = 5},
                new Packaging(){Name = "����� 'Bag&Box' ����� 5 �.",Count =1000,Amount=5},
                new Packaging(){Name = "����� ��� ���. ���� 200�300",Count =1000,Amount = 3},
                new Packaging(){Name = "����� 1��",Count =1000,Amount=1},
                new Packaging(){Name = "����� 5 ��",Count =1000,Amount = 5}
            };
            context.Packagings.AddRange(pack);
            var admin = new ApplicationUser { Email = "somemail@mail.ru", UserName = "somemail@mail.ru" };
            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);

            var manager = new ApplicationUser { Email = "manager@mail.ru", UserName = "Manager",FIO = "�������� �.�." };
            string passwordManager = "ad46D_ewr3";
            var resultTwo = userManager.Create(manager, passwordManager);

            var bookkeeping = new ApplicationUser { Email = "bookkeeping@mail.ru", UserName = "Bookkeeping",FIO="�����������" };
            string passwordBookkeeping = "ad46D_ewr3";
            var resultThree = userManager.Create(bookkeeping, passwordBookkeeping);

            var client = new ApplicationUser { Email = "client@mail.ru", UserName = "Antei",InAndOut=true };
            string passwordClient = "ad46D_ewr3";
            var resultFhoo = userManager.Create(client, passwordClient);
            var client2 = new ApplicationUser { Email = "client@mail.ru", UserName = "Nika", InAndOut = true };
            string passwordClient2 = "ad46D_ewr3";
            var resultFhoo2 = userManager.Create(client2, passwordClient2);
            var client3 = new ApplicationUser { Email = "client@mail.ru", UserName = "Kolos", InAndOut = true };
            string passwordClient3 = "ad46D_ewr3";
            var resultFhoo3 = userManager.Create(client3, passwordClient3);
            var client4 = new ApplicationUser { Email = "client@mail.ru", UserName = "Kastryschnik", InAndOut = true };
            string passwordClient4 = "ad46D_ewr3";
            var resultFhoo4 = userManager.Create(client4, passwordClient4);
            var client5 = new ApplicationUser { Email = "client@mail.ru", UserName = "Kvasovski_dvor", InAndOut = true };
            string passwordClient5 = "ad46D_ewr3";
            var resultFhoo5 = userManager.Create(client5, passwordClient5);
            var client6 = new ApplicationUser { Email = "client@mail.ru", UserName = "Uyt", InAndOut = true };
            string passwordClient6 = "ad46D_ewr3";
            var resultFhoo6 = userManager.Create(client6, passwordClient6);

            var autopark = new ApplicationUser { Email = "park@mail.ru", UserName = "Autopark", InAndOut = true };
            string passwordautopark = "ad46D_ewr3";
            var resultautopark = userManager.Create(autopark, passwordautopark);
            if (result.Succeeded && resultTwo.Succeeded && resultThree.Succeeded && resultFhoo.Succeeded)
            {
                userManager.AddToRole(autopark.Id,role5.Name);
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(manager.Id,role2.Name);
                userManager.AddToRole(bookkeeping.Id, role3.Name);
                userManager.AddToRole(client.Id, role4.Name);
                userManager.AddToRole(client2.Id,role4.Name);
                userManager.AddToRole(client3.Id,role4.Name);
                userManager.AddToRole(client4.Id,role4.Name);
                userManager.AddToRole(client5.Id,role4.Name);
                userManager.AddToRole(client6.Id,role4.Name);
            }
        }
    }
}
