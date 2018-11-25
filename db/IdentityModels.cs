using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace db
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FIO { get; set; }
        public string Company { get; set; }
        public string NameImage { get; set; } 
        public byte[] Image { get; set; }
        public bool InAndOut { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
        public virtual ICollection<TasksManager> Tasks { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
        public ApplicationUser()
        {
            Tasks = new List<TasksManager>();
            ChatMessages = new List<ChatMessage>();
        }
       
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserStatistics> UserStatics { get; set; }
        public DbSet<TasksManager> TasksManager { get; set; }
        public DbSet<TypeDocument> TypeDocuments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Seasonality> Seasonality { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<FinishedProducts> FinishedProducts { get; set; }
        public DbSet<RawMaterialStatistics> RawMaterialStatistics { get; set; }
        public DbSet<Auto> Auto { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<FinishedGoodsStatistics> FinishedGoodsStatistics { get; set; }
        public DbSet<ProductStatistics> ProductStatistics { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Raw_Product> Raw_Product { get; set; }
        public DbSet<Packaging_ProductStatistics> Packaging_ProductStatistics { get; set; }
        public DbSet<FinishedProducts_FinishedGoodsStatistics> FinishedProducts_FinishedGoodsStatistics { get; set; }
        public DbSet<Gild> Gilds { get; set; }
        public DbSet<GildStatic> GildStatics { get; set; }
        public DbSet<Gild_GildStatic> Gild_GildStatics { get; set; }
        public DbSet<Condiments> Condiments { get; set; }
        public DbSet<CondimentStatic> CondimentStatic { get; set; }
        public DbSet<Condiments_Static> Condiments_Static { get; set; }
        public DbSet<FinishGildArray> FinishGildArray { get; set; }
        public DbSet<ClaimArray> ClaimArrays { get; set; }
        public DbSet<Shipment_Array> shipment_Arrays { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
     //   protected override void OnModelCreating(DbModelBuilder modelBuilder)
     //   {
     //       modelBuilder.Entity<Product>()
     //.HasMany(p => p.RawMaterialStatistics)
     //.WithMany(c => c.Product)
     
     //.Map(m =>
     //{
     //    m.ToTable("Raw_Product");
     //    m.MapLeftKey("Raw_Id");
     //    m.MapRightKey("Product_Id");
         
     //});
     //   }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}