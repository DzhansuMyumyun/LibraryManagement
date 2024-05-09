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
    public class LedenRegistrerenViewModel : INotifyPropertyChanged
    {
        #region properties
        private List<LedenGegevens> _leden;
        public List<LedenGegevens> Leden
        {
            get { return _leden; }
            set { _leden = value;
                OnProperyChanged();
                
            }
        }

        private string _voornaam;
        public string Voornaam
        {
            get { return _voornaam; }
            set { _voornaam = value;
                OnProperyChanged();
            }
        }

        private string _famileinaam;
        public string Familienaam
        {
            get { return _famileinaam; }
            set { _famileinaam = value;
                OnProperyChanged();
            }
        }

        private DateTime _geboorte;
        public DateTime GeboorteDatum
        {
            get { return _geboorte; }
            set { _geboorte = value;
                OnProperyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value;
                OnProperyChanged();
            }
        }

        private Geslacht _geslachtselected;
        public Geslacht GeslachtSelected
        {
            get { return _geslachtselected; }
            set {
                _geslachtselected = value;
                OnProperyChanged();
            }
        }

        private Array _geslachtKiezen;
        public Array GeslachtK
        {
            get { return _geslachtKiezen; }
            set { _geslachtKiezen = value; }
        }

        private string _selectedGes;
        public string SelectedGes
        {
            get { return _selectedGes; }
            set { _selectedGes = value; }
        }

        private string _rijksNummer;
        public string RijksNummer
        {
            get { return _rijksNummer; }
            set { _rijksNummer = value;
                OnProperyChanged();
            }
        }

        private DateTime _datumBetaling = DateTime.Today;
        public DateTime DatumBetalinLidgeld
        {
            get { return _datumBetaling; }
            set { _datumBetaling = value;
                OnProperyChanged();
            }
        }

        private LedenGegevens _select;
        public LedenGegevens LidSelect
        {
            get
            {
                return _select;
            }
            set
            {
                _select = value;
                Selected();
               
            }
        }

        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value;
                OnProperyChanged();
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

        public ICommand Toevoegen { get; set; }
        public ICommand Bewerken { get; set; }
        public ICommand Close { get; set; }
        public ICommand Info { get; set; }

        public IRepositoryLeden ledenrep;
        public IClosable closable;
        public ICordinator cordinator;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        //constructor
        public LedenRegistrerenViewModel(IRepositoryLeden repositoryLeden, IClosable closable2)
        {
            ledenrep = repositoryLeden;
            closable = closable2;
            
            GeslachtK = Enum.GetValues(typeof(Geslacht));
            Toevoegen = new RelayCommand(ToevoegenLid);
            Bewerken = new RelayCommand(BewerkenLid);
            Close = new RelayCommand(Closable);
            Info = new RelayCommand(InfoToon);

            Enable2 = true;
            Update();
            Selected();
        }

        #region methods
        //Informatie over format
        private void InfoToon()
        {
            MessageBox.Show("Format van de rijksregisternummer: XX.XX.XX-XXX.XX" + Environment.NewLine + "Aatal cijfers = 11");
        }

        //Scherm te kunnen scluiten
        private void Closable()
        {
            closable.Close();
        }

        //Lid registreren
        private void ToevoegenLid()
        {

            try
            {
                LedenGegevens lid = new LedenGegevens()
                {
                    Voornaam = Voornaam,
                    Familienaam = Familienaam,
                    Email = Email,
                    GeslachtLid = GeslachtSelected,
                    GeboorteDat = GeboorteDatum,
                    Rijksregisternummer = RijksNummer,
                    DatumBetalingLidgeld = DatumBetalinLidgeld
                };


                switch (GeslachtSelected)
                {
                    case Geslacht.Man:
                        lid.Ges = "Man";
                        break;
                    case Geslacht.Vrouw:
                        lid.Ges = "Vrouw";
                        break;
                    case Geslacht.Onbekend:
                        lid.Ges = "Onbekend";
                        break;
                }


                if (ledenrep.BestaatRijksNummer(RijksNummer))
                {
                    throw new Exception("Mag niet zelfde rijksregisternummer");
                }
                else
                {
                    ledenrep.CreatLeden(lid);
                    Clear();
                }

                Update();
            
            }
            catch (Exception exep)
            {
                MessageBox.Show(exep.Message);
            }



        }

        //Velden verwijderen
        private void Clear()
        {
            Voornaam = null;
            Familienaam = null;
            GeboorteDatum = new DateTime();
            Email = null;
            GeslachtSelected = 0;
            RijksNummer = null;
            GeboorteDatum = new DateTime();
            Enable = false;
            Enable2 = true;
        }

        //Lid bewerken
        private void BewerkenLid()
        {
            Edit((LedenGegevens)LidSelect, Voornaam, Familienaam, GeboorteDatum, Email,GeslachtSelected, RijksNummer, DatumBetalinLidgeld);
            Clear();
        }

        private void Edit(LedenGegevens lid, string voornaam, string familie, DateTime gebo, string email, Geslacht  geslacht, string rijks, DateTime datumBetaling)
        {
            
                try
                {
                    lid.Voornaam = voornaam;
                    lid.Familienaam = familie;
                    lid.GeboorteDat = gebo;
                    lid.Email = email;
                    lid.GeslachtLid = geslacht;
                    switch (GeslachtSelected)
                    {
                        case Geslacht.Man:
                            lid.Ges = "Man";
                            break;
                        case Geslacht.Vrouw:
                            lid.Ges = "Vrouw";
                            break;
                        case Geslacht.Onbekend:
                            lid.Ges = "Onbekend";
                            break;
                    }
                    lid.Rijksregisternummer = rijks;
                    lid.DatumBetalingLidgeld = datumBetaling;

                    //if (ledenrep.BestaatRijksNummer(RijksNummer))
                    //{
                    //    throw new Exception("Mag niet zelfde rijksregisternummer");
                    //}
                    //else
                    //{
                    //    ledenrep.UpdateLeden(lid);
                    //    Update();
                    //}

                    ledenrep.UpdateLeden(lid);
                    Update();


                }
                catch (Exception exep)
                {
                    MessageBox.Show(exep.Message);
                }

                
         

        }

        //Lijst elke keer updaten
        public void Update()
        {
            Leden = null;
            Leden = ledenrep.GetAllLeden();
        }

        //Wanneer er een item geselecteed wordt
        private void Selected()
        {
            if(LidSelect != null)
            {
                LedenGegevens value = (LedenGegevens)LidSelect;
                Voornaam = value.Voornaam;
                Familienaam = value.Familienaam;
                GeboorteDatum = value.GeboorteDat;
                Email = value.Email;
                if (value.Ges == "Man") value.GeslachtLid = Geslacht.Man;
                else if(value.Ges == "Vrouw") value.GeslachtLid = Geslacht.Vrouw;
                else if (value.Ges == "Onbekend") value.GeslachtLid = Geslacht.Onbekend;
                GeslachtSelected = value.GeslachtLid;
                RijksNummer = value.Rijksregisternummer;
                DatumBetalinLidgeld = value.DatumBetalingLidgeld;
                Enable = true;
                Enable2 = false;
            }
            else if(LidSelect == null)
            {
                Clear();
                Enable = false;
                Enable2 = true;
            }
           
        }

        //Bij elke verandering automatish laten tonen
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
