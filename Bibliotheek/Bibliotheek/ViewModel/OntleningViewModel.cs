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
    public class OntleningViewModel : INotifyPropertyChanged
    {
        #region properties
        private List<BoekGegevens> _boeken;
        public List<BoekGegevens> Boeken
        {
            get { return _boeken; }
            set { _boeken = value;
                SelectedValuBoek();
                OnProperyChanged();
            }
        }

        private int _idBoek;
        public int IdBoek
        {
            get { return _idBoek; }
            set { _idBoek = value;
                OnProperyChanged();
            }
        }

        private string _titelBoek;
        public string TitelBoek
        {
            get { return _titelBoek; }
            set { _titelBoek = value;
                OnProperyChanged();
            }
        }

        private string _auteurBoek;
        public string AuteurBoek
        {
            get { return _auteurBoek; }
            set { _auteurBoek = value;
                OnProperyChanged();
            }
        }

        private string _isbnBoek;
        public string IsbnBoek
        {
            get { return _isbnBoek; }
            set { _isbnBoek = value;
                OnProperyChanged();
            }
        }

        private double _prijsBoek;
        public double PrijsBoek
        {
            get { return _prijsBoek; }
            set { _prijsBoek = value;
                OnProperyChanged();
            }
        }

        private int _exemplaarBoek;
        public int ExemplaarBoek
        {
            get { return _exemplaarBoek; }
            set { _exemplaarBoek = value;
                OnProperyChanged();
            }
        }

        private bool _magJa;
        public bool MagJa
        {
            get { return _magJa; }
            set { _magJa = value;
                OnProperyChanged();
            }
        }

        private bool _magNee;
        public bool MagNee
        {
            get { return _magNee; }
            set { _magNee = value;
                OnProperyChanged();
            }
        }

        private BoekGegevens _selectedBoek;
        public BoekGegevens SelectedBoek
        {
            get { return _selectedBoek; }
            set
            {
                _selectedBoek = value;
                SelectedValuBoek();
            }
        }

        private void SelectedValuBoek()
        {            
            if (SelectedBoek != null)
            {
                BoekGegevens boek = (BoekGegevens)SelectedBoek;
                IdBoek = boek.ID;
                TitelBoek = boek.TitelBoek;
                AuteurBoek = boek.Auteur;
                IsbnBoek = boek.ISBN;
                PrijsBoek = boek.AaankoopPrijs;
                ExemplaarBoek = boek.Beschikbaar;

                if(boek.Maglenen == true)
                {
                    MagJa = true;
                }
                else if(boek.Maglenen == false)
                {
                    MagNee = true;
                }
                Enable = true;               
            }
            else
            {                
                DeleteBoekenGegevens();
                Enable = false;
            }
        }

        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                OnProperyChanged();
            }
        }

        private bool _eanable3;
        public bool Enable3
        {
            get { return _eanable3; }
            set { _eanable3 = value;
                OnProperyChanged();
            }
        }

        //Dvd proprety

        private List<DvDGegevens> _dvdS;
        public List<DvDGegevens> DvdS
        {
            get { return _dvdS; }
            set { _dvdS = value;
                OnProperyChanged();
                SelectedValueDvd();
            }
        }

        private int _idDvd;
        public int IdDvd
        {
            get { return _idDvd; }
            set { _idDvd = value;
                OnProperyChanged();
            }
        }

        private string _titelDvd;
        public string TitelDvd
        {
            get { return _titelDvd; }
            set { _titelDvd = value;
                OnProperyChanged();
            }
        }

        private string _EanCode;
        public string EANcode
        {
            get { return _EanCode; }
            set { _EanCode = value;
                OnProperyChanged();
            }
        }

        private double _prijdDvd;
        public double PrijsDvd
        {
            get { return _prijdDvd; }
            set { _prijdDvd = value;
                OnProperyChanged();
            }
        }

        private Array _type;
        public Array Type
        {
            get { return _type; }
            set { _type = value;
                OnProperyChanged();
            }
        }

        private TypeDvd _typeKies;
        public TypeDvd KiesTypeDvd
        {
            get { return _typeKies; }
            set { _typeKies = value;
                OnProperyChanged();
            }
        }

        private int _exemplaarDvd;
        public int ExemplaarDvd
        {
            get { return _exemplaarDvd; }
            set { _exemplaarDvd = value;
                OnProperyChanged();
            }
        }

        private DvDGegevens _selecteditem;
        public DvDGegevens SelectedItem
        {
            get
            {
                return _selecteditem;
            }
            set
            {
                _selecteditem = value;
                SelectedValueDvd();

            }
        }

        private void SelectedValueDvd()
        {
            if(SelectedItem != null)
            {
                DvDGegevens dvd = (DvDGegevens)SelectedItem;
                IdDvd = dvd.Id;
                TitelDvd = dvd.Titel;
                EANcode = dvd.Ecode;
                PrijsDvd = dvd.Prijs;
                ExemplaarDvd = dvd.Aantal;

                if (dvd.GekozenType == "Documentaire")
                {
                    dvd.Typedvd = TypeDvd.Documentaire;
                }
                else if (dvd.GekozenType == "Speelfilm")
                {
                    dvd.Typedvd = TypeDvd.Speelfilm;
                }
                KiesTypeDvd = dvd.Typedvd;
                Enable2 = true;
                
            }
            else
            {
                
                DeleteDvdGegevens();
                Enable2 = false;
            }
        }

        private bool _enable2;
        public bool Enable2
        {
            get { return _enable2; }
            set
            {
                _enable2 = value;
                OnProperyChanged();
            }
        }

        private bool _enable4;
        public bool Enable4
        {
            get { return _enable4; }
            set { _enable4 = value;
                OnProperyChanged();
            }
        }

        //Proprety
        private List<LedenGegevens> _leden;
        public List<LedenGegevens> Leden
        {
            get { return _leden; }
            set { _leden = value;
                OnProperyChanged();
            }
        }

        private LedenGegevens _selectedLid;
        public LedenGegevens SelectedLid
        {
            get { return _selectedLid; }
            set
            {                
                _selectedLid = value;
                SelectLid();
                
            }
        }

        private void SelectLid()
        {
            if (SelectedLid != null)
            {
                Enable3 = true;
                Enable4 = true;
            }
            else
            {
                Enable3 = false;
                Enable4 = false;
            }
        }

        private int _aantalBoekOntleend;
        public int AantalBoekOntleend
        {
            get { return _aantalBoekOntleend; }
            set { _aantalBoekOntleend = value;
                OnProperyChanged();
            }
        }

        private int _aantalDvdOntleend;
        public int AantalDvdOntleend
        {
            get { return _aantalDvdOntleend; }
            set { _aantalDvdOntleend = value;
                OnProperyChanged();
            }
        }

        private DateTime time;
        private DateTime maxTime;

        private List<OntleningGegevens> _lijstOntleningen;
        public List<OntleningGegevens> LijstOntleningen
        {
            get { return _lijstOntleningen; }
            set { _lijstOntleningen = value;
                OnProperyChanged();
                
            }
        }

        private int tellerBoek = 0;
        private int telleDvd = 0;

        public IRepositoryOntlening repoOntlen;
        public IRepositoryBoek repositoryBoek;
        public IRepositoryDvD repositoryDvD;
        public IRepositoryLeden repositoryLeden;        

        public ICommand BoekOntlenen { get; set; }
        public ICommand DvdOntlenen { get; set; }
        public ICommand Close { get; set; }

        public IClosable closable;
        public ICordinator cordinator;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        //constructor
        public OntleningViewModel(IClosable closable2, ICordinator cordinator2, IRepositoryBoek repositoryBoek2, IRepositoryDvD repositoryDvD2, IRepositoryLeden repositoryLeden2,IRepositoryOntlening repositoryOntlening)
        {
            repositoryBoek = repositoryBoek2;
            repositoryDvD = repositoryDvD2;
            repositoryLeden = repositoryLeden2;
            Type = Enum.GetValues(typeof(TypeDvd));

            repoOntlen = repositoryOntlening;
            closable = closable2;
            cordinator = cordinator2;

            Close = new RelayCommand(Closable);
            BoekOntlenen = new RelayCommand(BoekToevoegen);
            DvdOntlenen = new RelayCommand(DvdToevoegen);
            SelectedValuBoek();
            SelectedValueDvd();

            UploadData();

        }

        #region methods
        //Create Boek ontlening
        private void BoekToevoegen()
        {

            try
            {
                tellerBoek++;
                if (tellerBoek == 5 || telleDvd == 3)
                {
                    MessageBox.Show("Er kunnen max 5 boeken en 3 dvd's ontleend worden");
                }
                else
                {

                    DateTime time = DateTime.Today;
                    DateTime maxTime = DateTime.Today.AddDays(28);
                    LedenGegevens lid = (LedenGegevens)SelectedLid;
                    BoekGegevens boek = (BoekGegevens)SelectedBoek;
                    boek.Beschikbaar--;
                    repositoryBoek.Update2(boek);
                    AddBoek(lid, boek, time, maxTime);
                    UploadData();
                    AantalBoekOntleend = tellerBoek;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void AddBoek(LedenGegevens lid, BoekGegevens boek, DateTime datumOntl, DateTime UitersteDatum)
        {
            OntleningGegevens ontlening = new OntleningGegevens(lid, boek, datumOntl, UitersteDatum);
            repoOntlen.BoekToevoegen(ontlening);
        }

        //Create Dvd ontlening
        private void DvdToevoegen()
        {
            try
            {
                telleDvd++;

                if (tellerBoek == 5 || telleDvd == 3)
                {
                    MessageBox.Show("Er kunnen max 5 boeken en 3 dvd's ontleend worden");
                }
                else
                {

                    time = DateTime.Today;
                    LedenGegevens lid = (LedenGegevens)SelectedLid;
                    DvDGegevens dvD = (DvDGegevens)SelectedItem;
                    if (dvD.GekozenType == "Documentaire")
                    {
                        maxTime = DateTime.Today.AddDays(21);
                    }
                    else if (dvD.GekozenType == "Speelfilm")
                    {
                        maxTime = DateTime.Today.AddDays(14);
                    }
                    dvD.Aantal--;
                    repositoryDvD.Update2(dvD);
                    AddDvd(lid, dvD, time, maxTime);
                    UploadData();
                    AantalDvdOntleend = telleDvd;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }
        private void AddDvd(LedenGegevens lid, DvDGegevens dvd, DateTime datumOntl, DateTime UitersteDatum)
        {
            OntleningGegevens ontlening = new OntleningGegevens(lid, dvd, datumOntl, UitersteDatum);
            repoOntlen.DvdToevoegen(ontlening);
        }

        //Delete selected gegevens
        private void DeleteBoekenGegevens()
        {
            IdBoek = ExemplaarBoek = 0;
            TitelBoek = AuteurBoek = IsbnBoek = null;
            PrijsBoek = 0;
            MagJa = MagNee = false;
        }
        private void DeleteDvdGegevens()
        {
            IdDvd = ExemplaarDvd = 0;
            TitelDvd = EANcode = null;
            PrijsDvd = 0;
            KiesTypeDvd = 0;
        }

        //Upload data
        private void UploadData()
        {
            Boeken = null;           
            Boeken = repositoryBoek.GetSelectedBoeken();

            DvdS = null;
            DvdS = repositoryDvD.GetSelectedBDvDs();

            Leden = null;
            Leden = repositoryLeden.GetAllLeden();

            LijstOntleningen = null;
            LijstOntleningen = repoOntlen.GetAllOntleningen();

        }

        //Scherm sluiten
        private void Closable()
        {
            closable.Close();           
        }

        private void OnProperyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
