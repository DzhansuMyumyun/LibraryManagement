using Bibliotheek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheek.Data_access
{
    public interface IRepositoryDvD
    {
        //CRUD

        void CreateDvd(DvDGegevens dvd);
        List<DvDGegevens> GetAllBDvDs();
        List<DvDGegevens> GetSelectedBDvDs();
        void Update(DvDGegevens dvd);
        bool NummerBestaat(string code);
        void Update2(DvDGegevens dvd);
    }
}
