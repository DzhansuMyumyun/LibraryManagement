using Bibliotheek.Data_access;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bibliotheek.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        //prop
        public ICommand LedenRegister { get; set; }
        public ICommand BoekBeheren { get; set; }
        public ICommand DvdBeheren { get; set; }
        public ICommand WerkOntleen { get; set; }
        public ICommand WerkTerug { get; set; }

        IRepositoryLeden repositoryLeden;
        IRepositoryOntlening repositoryOntlening;
        ICordinator cordinator;
        IClosable closable;

        public event PropertyChangedEventHandler PropertyChanged;

        //const
        public MainWindowViewModel(ICordinator cordinator2, IRepositoryLeden repositoryLeden2, IClosable closable2)
        {
            cordinator = cordinator2;
            repositoryLeden = repositoryLeden2;
            closable = closable2;
            //repositoryOntlening = repositoryOntlening2;


            LedenRegister = new RelayCommand(RegistrerenLid);
            BoekBeheren = new RelayCommand(BeherenBoek);
            DvdBeheren = new RelayCommand(BeherenDvd);
            WerkOntleen = new RelayCommand(OntlenenWerk);
            WerkTerug = new RelayCommand(TerugWerk);
        }

        //Methods

        private void RegistrerenLid()
        {
            try
            {
                cordinator.LedenRegistrerenOpen();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void BeherenBoek()
        {
            try
            {
                cordinator.BoekBeherenOpen();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BeherenDvd()
        {
            try
            {
                cordinator.DvdBeherenOpen();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OntlenenWerk()
        {
            try
            {
                cordinator.WerkOntlenenOpen();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void TerugWerk()
        {
            try
            {
                cordinator.WerkTerugBrengenOntlenen();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
