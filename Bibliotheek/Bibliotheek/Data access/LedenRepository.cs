using System;
using System.Collections.Generic;
using System.Linq;

using Bibliotheek.Model;

namespace Bibliotheek.Data_access
{
    public class LedenRepository : IRepositoryLeden
    {
        BibliotheetContext context = new BibliotheetContext();

        //Lid registreren
        public void CreatLeden(LedenGegevens lid)
        {
            context.ledenGegevens.Add(lid);
            context.SaveChanges();
        }

        //Leden die hun lidgeld dit kalenderjaar betaald hebben
        public List<LedenGegevens> GetAllLeden()
        {
            return context.ledenGegevens.Where(lid => lid.DatumBetalingLidgeld.Year == DateTime.Today.Year).ToList();
        }

        //Alle gerigistreerde leden
        public List<LedenGegevens> GetLeden()
        {
            return context.ledenGegevens.ToList();
        }

        //Update van een lid 
        public void UpdateLeden(LedenGegevens lid)
        {
            context.SaveChanges();
        }

        //Kijken of gegevens rijksregisternummer al bestaat
        public bool BestaatRijksNummer(string rijksRegisternummer)
        {
            return context.ledenGegevens.Where(x => x.Rijksregisternummer == rijksRegisternummer).Count() > 0;
        }
    }
}
