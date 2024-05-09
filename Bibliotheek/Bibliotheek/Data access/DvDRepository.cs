using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bibliotheek.Model;

namespace Bibliotheek.Data_access
{
    public class DvDRepository : IRepositoryDvD
    {
        BibliotheetContext context = new BibliotheetContext();


        //Create
        public void CreateDvd(DvDGegevens dvd)
        {
            context.dvDGegevens.Add(dvd);
            context.SaveChanges();
        }

        //Get set
        public List<DvDGegevens> GetSelectedBDvDs()
        {
            return context.dvDGegevens.Where(dvd => dvd.Aantal > 0).ToList();
        }

        //Get
        public List<DvDGegevens> GetAllBDvDs()
        {
            return context.dvDGegevens.ToList();
        }

        //Update
        public void Update(DvDGegevens dvd)
        {
            var dvd2 = context.dvDGegevens.Where(d => d.Ecode == dvd.Ecode).First();
            dvd2.Aantal += 1;
            context.SaveChanges();
        }

        public void Update2(DvDGegevens dvd)
        {
            context.SaveChanges();
        }

        //Check
        public bool NummerBestaat(string code)
        {
            return context.dvDGegevens.Where(n => n.Ecode == code).Count() > 0;
        }


    }
}
