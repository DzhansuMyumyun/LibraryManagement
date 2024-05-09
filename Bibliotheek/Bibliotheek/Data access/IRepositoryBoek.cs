using Bibliotheek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheek.Data_access
{
    public interface IRepositoryBoek
    {
        //CRUD

        void CreateBoek(BoekGegevens boek);
        List<BoekGegevens> GetAllBoeken();
        List<BoekGegevens> GetSelectedBoeken();
        void Update(BoekGegevens boek);
        bool NummerBestaat(string code);
        void Update2(BoekGegevens boek);
    }
}
