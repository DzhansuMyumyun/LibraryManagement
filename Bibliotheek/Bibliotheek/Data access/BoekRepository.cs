using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bibliotheek.Model;

namespace Bibliotheek.Data_access
{
    public class BoekRepository : IRepositoryBoek
    {
        BibliotheetContext context = new BibliotheetContext();

        //Create boek
        public void CreateBoek(BoekGegevens boek)
        {
            context.boekGegevens.Add(boek);
            context.SaveChanges();
        }

        //Alle boeken
        public List<BoekGegevens> GetAllBoeken()
        {
            return context.boekGegevens.ToList();
        }

        //Boeken die meer dan 0 exemplaren bestaat en die mogen uitgeleend worden
        public List<BoekGegevens> GetSelectedBoeken()
        {
            return context.boekGegevens.Where(boek => boek.Beschikbaar > 0 && boek.Maglenen == true).ToList();

        }

        //Als de gegeven ISBN nummer al bestaat, +1 voor de exemplaar 
        public void Update(BoekGegevens boek)
        {
            var boek2 = context.boekGegevens.Where(b => b.ISBN == boek.ISBN).First();
            boek2.Beschikbaar += 1;
            context.SaveChanges();
        }

        //Kijken of gegeven nummer bestaat
        public bool NummerBestaat(string code)
        {
            return context.boekGegevens.Where(n => n.ISBN == code).Count() > 0;
        }

        //Update lijst
        public void Update2(BoekGegevens boek)
        {
            context.SaveChanges();
        }
    }
}
