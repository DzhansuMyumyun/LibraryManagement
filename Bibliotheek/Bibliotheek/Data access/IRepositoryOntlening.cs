using Bibliotheek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheek.Data_access
{
    public interface IRepositoryOntlening
    {
        //CRUD
        void CreatWerk(OntleningGegevens ontlening);
        List<OntleningGegevens> GetAllOntleningen();
        void UpdateLijst(OntleningGegevens ontlening);
        List<OntleningGegevens> GetOntleningen();
        void BoekToevoegen(OntleningGegevens ontleningboek);
        void DvdToevoegen(OntleningGegevens ontleningdvd);

    }
}
