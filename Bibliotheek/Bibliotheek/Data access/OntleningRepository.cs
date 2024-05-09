using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bibliotheek.Model;

namespace Bibliotheek.Data_access
{
    public class OntleningRepository : IRepositoryOntlening
    {
        BibliotheetContext context = new BibliotheetContext();

        //Onlening maken
        public void CreatWerk(OntleningGegevens ontlening)
        {
            context.ontleningen.Add(ontlening);
            context.SaveChanges();
        }

        //Ontlening van boek
        public void BoekToevoegen(OntleningGegevens ontleningboek)
        {
            LedenGegevens lid = context.ledenGegevens.First(naam => naam.Voornaam == ontleningboek.Lid.Voornaam);
            BoekGegevens boek = context.boekGegevens.First(b => b.TitelBoek == ontleningboek.Boek.TitelBoek);
            ontleningboek.Lid = lid;
            ontleningboek.Boek = boek;

            context.ontleningen.Add(ontleningboek);
            context.SaveChanges();
        }

        //Ontlening van Dvd
        public void DvdToevoegen(OntleningGegevens ontleningdvd)
        {
            LedenGegevens lid = context.ledenGegevens.First(naam => naam.Voornaam == ontleningdvd.Lid.Voornaam);
            DvDGegevens dvd = context.dvDGegevens.First(b => b.Titel == ontleningdvd.Dvd.Titel);
            ontleningdvd.Lid = lid;
            ontleningdvd.Dvd = dvd;

            context.ontleningen.Add(ontleningdvd);
            context.SaveChanges();
        }

        //Lijst van alle ontleningen
        public List<OntleningGegevens> GetAllOntleningen()
        {
            return context.ontleningen.ToList();            
        }
        
        //Lijst van ontleningen aan de hand van datum
        public List<OntleningGegevens> GetOntleningen()
        {
            return context.ontleningen.Where(x => x.DatumVanInlevering == null).ToList();
        }

        //Lijst Update
        public void UpdateLijst(OntleningGegevens ontlening)
        {
            context.SaveChanges();
        }


    }
}
