using Bibliotheek.Data_access;
using Bibliotheek.View;
using Bibliotheek.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheek
{
    public class Cordinator : ICordinator
    {
        LedenRepository lidRepo = new LedenRepository();
        OntleningRepository ontlenRep = new OntleningRepository();
        BoekRepository boekRep = new BoekRepository();
        DvDRepository dvdRep = new DvDRepository();

        public void LedenRegistrerenOpen()
        {
            LedenRegistrerenWindow ledenWindow = new LedenRegistrerenWindow();
            LedenRegistrerenViewModel lidRegistreren = new LedenRegistrerenViewModel(lidRepo, ledenWindow);
            ledenWindow.DataContext = lidRegistreren;
            ledenWindow.Show();
        }

        public void BoekBeherenOpen()
        {
            BoekBeherenWindowxaml boekBeheren = new BoekBeherenWindowxaml();
            BoekBeherenViewModel boekBeherenView = new BoekBeherenViewModel(boekRep,boekBeheren);
            boekBeheren.DataContext = boekBeherenView;
            boekBeheren.Show();
        }

        public void DvdBeherenOpen()
        {
            DvdBeherenWindow dvdBeheren = new DvdBeherenWindow();
            DvDBeherenViewModel dvDBeherenView = new DvDBeherenViewModel(dvdRep,dvdBeheren);
            dvdBeheren.DataContext = dvDBeherenView;
            dvdBeheren.Show();
        }

        public void WerkOntlenenOpen()
        {
            OntleningWindow ontleningWindow = new OntleningWindow();
            OntleningViewModel ontleningViewModel = new OntleningViewModel(ontleningWindow, this, boekRep, dvdRep, lidRepo, ontlenRep);
            ontleningWindow.DataContext = ontleningViewModel;
            ontleningWindow.Show();
        }

        public void WerkTerugBrengenOntlenen()
        {
            LijstOntleningenWindow lijstOntleningenWindow = new LijstOntleningenWindow();
            LijstOntleningenViewModel lijstOntleningenViewModel = new LijstOntleningenViewModel(ontlenRep, lijstOntleningenWindow, lidRepo, boekRep, dvdRep);
            lijstOntleningenWindow.DataContext = lijstOntleningenViewModel;
            lijstOntleningenWindow.Show();
        }

        public void MainWindowOpen()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainViewModel = new MainWindowViewModel(this, lidRepo, mainWindow);
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }
    }
}
