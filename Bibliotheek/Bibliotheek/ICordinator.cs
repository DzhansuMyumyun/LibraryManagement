using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheek
{
    public interface ICordinator
    {
        void LedenRegistrerenOpen();
        void BoekBeherenOpen();
        void DvdBeherenOpen();
        void WerkOntlenenOpen();
        void WerkTerugBrengenOntlenen();
        void MainWindowOpen();
    }
}
