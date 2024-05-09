namespace Bibliotheek.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tabellen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoekGegevens",
                c => new
                    {
                        TitelBoek = c.String(nullable: false, maxLength: 128),
                        ID = c.Int(nullable: false, identity: true),
                        Auteur = c.String(),
                        ISBN = c.String(),
                        AaankoopPrijs = c.Double(nullable: false),
                        Maglenen = c.Boolean(nullable: false),
                        Beschikbaar = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TitelBoek);
            
            CreateTable(
                "dbo.DvDGegevens",
                c => new
                    {
                        Titel = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false, identity: true),
                        Ecode = c.String(),
                        Prijs = c.Double(nullable: false),
                        GekozenType = c.String(),
                        Aantal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Titel);
            
            CreateTable(
                "dbo.LedenGegevens",
                c => new
                    {
                        Voornaam = c.String(nullable: false, maxLength: 128),
                        ID = c.Int(nullable: false, identity: true),
                        Familienaam = c.String(),
                        Email = c.String(),
                        Ges = c.String(),
                        Rijksregisternummer = c.String(),
                        GeboorteDat = c.DateTime(nullable: false),
                        DatumBetalingLidgeld = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Voornaam);
            
            CreateTable(
                "dbo.OntleningGegevens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumOntlening = c.DateTime(nullable: false),
                        UitersteInleverDatum = c.DateTime(nullable: false),
                        DatumVanInlevering = c.String(),
                        Boek_TitelBoek = c.String(maxLength: 128),
                        Dvd_Titel = c.String(maxLength: 128),
                        Lid_Voornaam = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BoekGegevens", t => t.Boek_TitelBoek)
                .ForeignKey("dbo.DvDGegevens", t => t.Dvd_Titel)
                .ForeignKey("dbo.LedenGegevens", t => t.Lid_Voornaam)
                .Index(t => t.Boek_TitelBoek)
                .Index(t => t.Dvd_Titel)
                .Index(t => t.Lid_Voornaam);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OntleningGegevens", "Lid_Voornaam", "dbo.LedenGegevens");
            DropForeignKey("dbo.OntleningGegevens", "Dvd_Titel", "dbo.DvDGegevens");
            DropForeignKey("dbo.OntleningGegevens", "Boek_TitelBoek", "dbo.BoekGegevens");
            DropIndex("dbo.OntleningGegevens", new[] { "Lid_Voornaam" });
            DropIndex("dbo.OntleningGegevens", new[] { "Dvd_Titel" });
            DropIndex("dbo.OntleningGegevens", new[] { "Boek_TitelBoek" });
            DropTable("dbo.OntleningGegevens");
            DropTable("dbo.LedenGegevens");
            DropTable("dbo.DvDGegevens");
            DropTable("dbo.BoekGegevens");
        }
    }
}
