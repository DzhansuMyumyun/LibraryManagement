using Bibliotheek.Data_access;
using Bibliotheek.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bibliotheek.ViewModel
{
    public class LijstOntleningenViewModel : INotifyPropertyChanged
    {
        //prop


        public List<OntleningGegevens> Selected(OntleningGegevens ontlening)
        {
            return LijstOntlening.Where(x => x.Lid == ontlening.Lid).ToList();
        }

        private List<OntleningGegevens> _lijstOntleningen;
        public List<OntleningGegevens> LijstOntlening
        {
            get { return _lijstOntleningen; }
            set { _lijstOntleningen = value;
                OnProprtyChanged();
            }
        }

        private OntleningGegevens _selectedOnt;
        public OntleningGegevens SelectedOnt
        {
            get { return _selectedOnt; }
            set { _selectedOnt = value;
                if(SelectedOnt != null)
                {
                    Enable = true;
                }
                else
                {
                    Enable = false;
                }
                OnProprtyChanged();
            }
        }

        private DateTime _inleverDatum = DateTime.Today;
        public DateTime InleverDatum
        {
            get { return _inleverDatum; }
            set { _inleverDatum = value;
                OnProprtyChanged();
            }
        }

        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value;
                OnProprtyChanged();
            }
        }


        public ICommand Opslaan { get; set; }
        public ICommand Close { get; set; }

        public IRepositoryOntlening repositoryOntlening;
        public IRepositoryLeden repositoryLeden;
        public IRepositoryBoek repositoryB;
        public IRepositoryDvD repositoryDvD;

        public IClosable closable;

        public event PropertyChangedEventHandler PropertyChanged;
        //cons
        public LijstOntleningenViewModel(IRepositoryOntlening repositoryOntlening2, IClosable closable2, IRepositoryLeden repositoryLeden2 , IRepositoryBoek repositoryBoek2, IRepositoryDvD repositoryDvD2)
        {
            repositoryOntlening = repositoryOntlening2;
            repositoryLeden = repositoryLeden2;
            closable = closable2;
            repositoryDvD = repositoryDvD2;
            repositoryB = repositoryBoek2;

            Opslaan = new RelayCommand(DatumOpslaan);
            Close = new RelayCommand(Closable);
            UploadData();
        }


        //Methods
        private void UploadData()
        {

            LijstOntlening = null;
            LijstOntlening = repositoryOntlening.GetOntleningen();
            

        }

        private void DatumOpslaan()
        {
            try
            {
                OntleningGegevens werk = (OntleningGegevens)SelectedOnt;
                if (werk.Boek != null)
                {
                    werk.Boek.Beschikbaar++;
                    repositoryB.Update2(werk.Boek);
                }
                else if (werk.Dvd != null)
                {
                    werk.Dvd.Aantal++;
                    repositoryDvD.Update2(werk.Dvd);
                }

                werk.DatumVanInlevering = InleverDatum.ToString();
                if (Convert.ToDateTime(werk.DatumVanInlevering) > werk.UitersteInleverDatum && werk.Boek != null)
                {
                    MessageBox.Show("Boete €0,09");
                }
                else if (Convert.ToDateTime(werk.DatumVanInlevering) > werk.UitersteInleverDatum && werk.Dvd != null)
                {

                    MessageBox.Show("Boete €0,09");

                }

                repositoryOntlening.UpdateLijst(werk);
                UploadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void Closable()
        {
            closable.Close();
        }

        private void OnProprtyChanged([CallerMemberName] string prop = null)
        {
            if (prop != null && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

    }
}
