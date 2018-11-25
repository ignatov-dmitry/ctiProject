namespace db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Marka = c.String(),
                        Model = c.String(),
                        NumberAuto = c.String(),
                        ObmDvig = c.Double(nullable: false),
                        TypeTopl = c.String(),
                        ObmKuz = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserSetId = c.Guid(),
                        UserGetId = c.Guid(),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(),
                        Read = c.Boolean(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        UserGet_Id = c.String(maxLength: 128),
                        UserSet_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserGet_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserSet_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.UserGet_Id)
                .Index(t => t.UserSet_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FIO = c.String(),
                        Company = c.String(),
                        NameImage = c.String(),
                        Image = c.Binary(),
                        InAndOut = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TasksManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        Name = c.String(),
                        Text = c.String(),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ClaimArrays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FinishProductId = c.Int(),
                        Count = c.Int(nullable: false),
                        Claim_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinishedProducts", t => t.FinishProductId)
                .ForeignKey("dbo.Claims", t => t.Claim_Id)
                .Index(t => t.FinishProductId)
                .Index(t => t.Claim_Id);
            
            CreateTable(
                "dbo.FinishedProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                        CountPack = c.Int(nullable: false),
                        PriceIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.Binary(),
                        PackagingId = c.Int(),
                        ProductStatistics_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packagings", t => t.PackagingId)
                .ForeignKey("dbo.ProductStatistics", t => t.ProductStatistics_Id)
                .Index(t => t.PackagingId)
                .Index(t => t.ProductStatistics_Id);
            
            CreateTable(
                "dbo.FinishedProducts_FinishedGoodsStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FinishedProductsId = c.Int(),
                        FinishedGoodsStatisticsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinishedGoodsStatistics", t => t.FinishedGoodsStatisticsId)
                .ForeignKey("dbo.FinishedProducts", t => t.FinishedProductsId)
                .Index(t => t.FinishedProductsId)
                .Index(t => t.FinishedGoodsStatisticsId);
            
            CreateTable(
                "dbo.FinishedGoodsStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Count = c.Int(nullable: false),
                        PriceIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackagingId = c.Int(),
                        CountProd = c.Double(nullable: false),
                        CountSpec = c.Double(nullable: false),
                        CountPack = c.Int(nullable: false),
                        ProductStatistics_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packagings", t => t.PackagingId)
                .ForeignKey("dbo.ProductStatistics", t => t.ProductStatistics_Id)
                .Index(t => t.PackagingId)
                .Index(t => t.ProductStatistics_Id);
            
            CreateTable(
                "dbo.FinishGildArrays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GildId = c.Int(),
                        FinishId = c.Int(),
                        FinishStaticId = c.Int(),
                        CountSyr = c.Double(),
                        CountSpec = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinishedGoodsStatistics", t => t.FinishStaticId)
                .ForeignKey("dbo.FinishedProducts", t => t.FinishId)
                .Index(t => t.FinishId)
                .Index(t => t.FinishStaticId);
            
            CreateTable(
                "dbo.Packagings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Packaging_ProductStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackagingId = c.Int(),
                        ProductStatisticsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packagings", t => t.PackagingId)
                .ForeignKey("dbo.ProductStatistics", t => t.ProductStatisticsId)
                .Index(t => t.PackagingId)
                .Index(t => t.ProductStatisticsId);
            
            CreateTable(
                "dbo.ProductStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Count = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdClient = c.Guid(nullable: false),
                        IdManager = c.Guid(),
                        PrinClient = c.Boolean(),
                        DateStart = c.DateTime(),
                        PrinManager = c.DateTime(),
                        OtprAuto = c.DateTime(),
                        ObshSum = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Communications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Guid(nullable: false),
                        ManagerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Condiments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Count = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Condiments_Static",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CondimentsId = c.Int(),
                        CondimentStaticId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Condiments", t => t.CondimentsId)
                .ForeignKey("dbo.CondimentStatics", t => t.CondimentStaticId)
                .Index(t => t.CondimentsId)
                .Index(t => t.CondimentStaticId);
            
            CreateTable(
                "dbo.CondimentStatics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gilds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                        ProductId = c.Int(),
                        CondimentsId = c.Int(),
                        State = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Condiments", t => t.CondimentsId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.CondimentsId);
            
            CreateTable(
                "dbo.Gild_GildStatic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GildId = c.Int(),
                        GildStaticId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gilds", t => t.GildId)
                .ForeignKey("dbo.GildStatics", t => t.GildStaticId)
                .Index(t => t.GildId)
                .Index(t => t.GildStaticId);
            
            CreateTable(
                "dbo.GildStatics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountGild = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CountCondiments = c.Double(nullable: false),
                        CountProduct = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Count = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Raw_Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        RawId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.RawMaterialStatistics", t => t.RawId)
                .Index(t => t.ProductId)
                .Index(t => t.RawId);
            
            CreateTable(
                "dbo.RawMaterialStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Count = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Text = c.String(),
                        TypeDocument_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeDocuments", t => t.TypeDocument_Id)
                .Index(t => t.TypeDocument_Id);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FIO = c.String(),
                        Phone = c.String(),
                        NumprPrav = c.String(),
                        AutoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        AspNetUserId = c.Guid(nullable: false),
                        Message = c.String(),
                        Status = c.Boolean(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Seasonalities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mounth = c.String(),
                        Count = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MassOstAuto = c.Double(nullable: false),
                        PrinVyhOtpr = c.DateTime(),
                        Status = c.Boolean(),
                        AutoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shipment_Array",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObchMasClaim = c.Double(nullable: false),
                        ClaimId = c.Int(),
                        DatePrin = c.DateTime(),
                        Status = c.Boolean(),
                        NameClient = c.String(),
                        IdShipment = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shipments", t => t.IdShipment)
                .Index(t => t.IdShipment);
            
            CreateTable(
                "dbo.TypeDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        URL = c.String(),
                        Message = c.String(),
                        DateOfCompletion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "TypeDocument_Id", "dbo.TypeDocuments");
            DropForeignKey("dbo.Shipment_Array", "IdShipment", "dbo.Shipments");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Raw_Product", "RawId", "dbo.RawMaterialStatistics");
            DropForeignKey("dbo.Raw_Product", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Gilds", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Gild_GildStatic", "GildStaticId", "dbo.GildStatics");
            DropForeignKey("dbo.Gild_GildStatic", "GildId", "dbo.Gilds");
            DropForeignKey("dbo.Gilds", "CondimentsId", "dbo.Condiments");
            DropForeignKey("dbo.Condiments_Static", "CondimentStaticId", "dbo.CondimentStatics");
            DropForeignKey("dbo.Condiments_Static", "CondimentsId", "dbo.Condiments");
            DropForeignKey("dbo.ClaimArrays", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.FinishedProducts_FinishedGoodsStatistics", "FinishedProductsId", "dbo.FinishedProducts");
            DropForeignKey("dbo.Packaging_ProductStatistics", "ProductStatisticsId", "dbo.ProductStatistics");
            DropForeignKey("dbo.FinishedProducts", "ProductStatistics_Id", "dbo.ProductStatistics");
            DropForeignKey("dbo.FinishedGoodsStatistics", "ProductStatistics_Id", "dbo.ProductStatistics");
            DropForeignKey("dbo.Packaging_ProductStatistics", "PackagingId", "dbo.Packagings");
            DropForeignKey("dbo.FinishedProducts", "PackagingId", "dbo.Packagings");
            DropForeignKey("dbo.FinishedGoodsStatistics", "PackagingId", "dbo.Packagings");
            DropForeignKey("dbo.FinishGildArrays", "FinishId", "dbo.FinishedProducts");
            DropForeignKey("dbo.FinishGildArrays", "FinishStaticId", "dbo.FinishedGoodsStatistics");
            DropForeignKey("dbo.FinishedProducts_FinishedGoodsStatistics", "FinishedGoodsStatisticsId", "dbo.FinishedGoodsStatistics");
            DropForeignKey("dbo.ClaimArrays", "FinishProductId", "dbo.FinishedProducts");
            DropForeignKey("dbo.ChatMessages", "UserSet_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChatMessages", "UserGet_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TasksManagers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChatMessages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Shipment_Array", new[] { "IdShipment" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Documents", new[] { "TypeDocument_Id" });
            DropIndex("dbo.Raw_Product", new[] { "RawId" });
            DropIndex("dbo.Raw_Product", new[] { "ProductId" });
            DropIndex("dbo.Gild_GildStatic", new[] { "GildStaticId" });
            DropIndex("dbo.Gild_GildStatic", new[] { "GildId" });
            DropIndex("dbo.Gilds", new[] { "CondimentsId" });
            DropIndex("dbo.Gilds", new[] { "ProductId" });
            DropIndex("dbo.Condiments_Static", new[] { "CondimentStaticId" });
            DropIndex("dbo.Condiments_Static", new[] { "CondimentsId" });
            DropIndex("dbo.Packaging_ProductStatistics", new[] { "ProductStatisticsId" });
            DropIndex("dbo.Packaging_ProductStatistics", new[] { "PackagingId" });
            DropIndex("dbo.FinishGildArrays", new[] { "FinishStaticId" });
            DropIndex("dbo.FinishGildArrays", new[] { "FinishId" });
            DropIndex("dbo.FinishedGoodsStatistics", new[] { "ProductStatistics_Id" });
            DropIndex("dbo.FinishedGoodsStatistics", new[] { "PackagingId" });
            DropIndex("dbo.FinishedProducts_FinishedGoodsStatistics", new[] { "FinishedGoodsStatisticsId" });
            DropIndex("dbo.FinishedProducts_FinishedGoodsStatistics", new[] { "FinishedProductsId" });
            DropIndex("dbo.FinishedProducts", new[] { "ProductStatistics_Id" });
            DropIndex("dbo.FinishedProducts", new[] { "PackagingId" });
            DropIndex("dbo.ClaimArrays", new[] { "Claim_Id" });
            DropIndex("dbo.ClaimArrays", new[] { "FinishProductId" });
            DropIndex("dbo.TasksManagers", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ChatMessages", new[] { "UserSet_Id" });
            DropIndex("dbo.ChatMessages", new[] { "UserGet_Id" });
            DropIndex("dbo.ChatMessages", new[] { "ApplicationUser_Id" });
            DropTable("dbo.UserStatistics");
            DropTable("dbo.TypeDocuments");
            DropTable("dbo.Shipment_Array");
            DropTable("dbo.Shipments");
            DropTable("dbo.Seasonalities");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Notifications");
            DropTable("dbo.Events");
            DropTable("dbo.Drivers");
            DropTable("dbo.Documents");
            DropTable("dbo.RawMaterialStatistics");
            DropTable("dbo.Raw_Product");
            DropTable("dbo.Products");
            DropTable("dbo.GildStatics");
            DropTable("dbo.Gild_GildStatic");
            DropTable("dbo.Gilds");
            DropTable("dbo.CondimentStatics");
            DropTable("dbo.Condiments_Static");
            DropTable("dbo.Condiments");
            DropTable("dbo.Communications");
            DropTable("dbo.Claims");
            DropTable("dbo.ProductStatistics");
            DropTable("dbo.Packaging_ProductStatistics");
            DropTable("dbo.Packagings");
            DropTable("dbo.FinishGildArrays");
            DropTable("dbo.FinishedGoodsStatistics");
            DropTable("dbo.FinishedProducts_FinishedGoodsStatistics");
            DropTable("dbo.FinishedProducts");
            DropTable("dbo.ClaimArrays");
            DropTable("dbo.TasksManagers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ChatMessages");
            DropTable("dbo.Autoes");
        }
    }
}
