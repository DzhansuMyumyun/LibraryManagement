using Bibliotheek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheek.Data_access
{
    public interface IRepositoryLeden
    {
        //CRUD
        void CreatLeden(LedenGegevens lid);
        List<LedenGegevens> GetAllLeden();
        List<LedenGegevens> GetLeden();
        void UpdateLeden(LedenGegevens lid);
        bool BestaatRijksNummer(string rijksRegisternummer);

    }
}
