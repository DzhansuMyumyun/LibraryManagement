namespace Bibliotheek
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Bibliotheek.Model;

    public partial class BibliotheetContext : DbContext
    {
        public BibliotheetContext(): base("name=BibliotheetContext")
        {

        }

       public DbSet<LedenGegevens> ledenGegevens { get; set; }
        public DbSet<BoekGegevens> boekGegevens { get; set; }
        public DbSet<DvDGegevens> dvDGegevens { get; set; }
        public DbSet<OntleningGegevens> ontleningen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LedenGegevens>().HasKey(lid => lid.Voornaam);
            modelBuilder.Entity<LedenGegevens>().Property(p => p.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<BoekGegevens>().HasKey(boek => boek.TitelBoek);
            modelBuilder.Entity<BoekGegevens>().Property(b => b.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<DvDGegevens>().HasKey(dvd =>  dvd.Titel);
            modelBuilder.Entity<DvDGegevens>().Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<OntleningGegevens>().HasKey(ontleningen => ontleningen.ID);
            modelBuilder.Entity<OntleningGegevens>().Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);




        }
    }
}
