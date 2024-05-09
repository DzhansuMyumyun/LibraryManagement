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
     public class BoekBeherenViewModel : INotifyPropertyChanged
    {
        #region prop
        private List<BoekGegevens> _boeken;
        public List<BoekGegevens> Boeken
        {
            get { return _boeken; }
            set { _boeken = value;
                OnProprtyChanged();
            }
        }

        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value;
                OnProprtyChanged();
            }
        }

        private string _titel;
        public string TitelB
        {
            get { return _titel; }
            set { _titel = value;
                OnProprtyChanged();
            }
        }

        private string _auteur;
        public string AuteurB
        {
            get { return _auteur; }
            set { _auteur = value;
                OnProprtyChanged();
            }
        }


        private string _ISBN;
        public string ISBNB
        {
            get { return _ISBN; }
            set { _ISBN = value;
                OnProprtyChanged();
            }
        }

        private double _prijs;
        public double PrjsB
        {
            get { return _prijs; }
            set { _prijs = value;
                OnProprtyChanged();
            }
        }

        private bool _magUitgeleendJa;
        public bool MaguitgeleendJa
        {
            get { return _magUitgeleendJa; }
            set { _magUitgeleendJa = value;
                OnProprtyChanged();
            }
        }

        private bool _magUitgeleendNee;
        public bool MagUitgeleendNee
        {
            get { return _magUitgeleendNee; }
            set { _magUitgeleendNee = value;
                OnProprtyChanged();
            }
        }

        private int _exemplaren;
        public int AantalExemplarenB
        {
            get { return _exemplaren; }
            set { _exemplaren = value;
                OnProprtyChanged();
            }
        }

        private BoekGegevens _selectedBoek;
        public BoekGegevens SelectedBoek
        {
            get { return _selectedBoek; }
            set {
                _selectedBoek = value;
                SelectedValu();
            }
        }

        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                OnProprtyChanged();
            }
        }

        private bool _enable2;
        public bool Enable2
        {
            get { return _enable2; }
            set
            {
                _enable2 = value;
                OnProprtyChanged();
            }
        }


        public ICommand Toevoegen { get; set; }
        public ICommand Bewerken { get; set; }
        public ICommand Close { get; set; }
        public ICommand Info { get; set; }

        public IRepositoryBoek RepositoryBoek;
        public IClosable closable;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        //Constr
        public BoekBeherenViewModel( IRepositoryBoek repBoek, IClosable closable2)
        {
            closable = closable2;
            RepositoryBoek = repBoek;
            Toevoegen = new RelayCommand(BoekToevoegen);
            Bewerken = new RelayCommand(BewerkBoek);
            Close = new RelayCommand(Closable);
            Info = new RelayCommand(FormatCode);
            SelectedValu();
            Update();
            Enable2 = true;
        }

        #region  methods
        private void FormatCode()
        {
            MessageBox.Show("Format van de ISBN nummer: XXX-XX-XXX-XXXX-X");
        }
        
        //Verwijderen van velden
        private void Verwijder()
        {
            ID = AantalExemplarenB =  0;
            TitelB = AuteurB = ISBNB = null;
            PrjsB = 0;
            MaguitgeleendJa = MagUitgeleendNee = false;

            
        }

        //Window sluiten
        private void Closable()
        {
            closable.Close();
        }

        //Edit gegevens
        private void BewerkBoek()
        {
            Edit((BoekGegevens)SelectedBoek, TitelB, AuteurB, ISBNB, PrjsB, MaguitgeleendJa, MagUitgeleendNee, AantalExemplarenB);
        }

        private void Edit(BoekGegevens boek, string titel, string auteur, string isbn, double prijs, bool magJa, bool magNee, int exemplaar)
        {
            try
            {
                boek.Auteur = auteur;
                boek.ISBN = isbn;
                boek.AaankoopPrijs = prijs;
                if (magJa == true)
                {
                    boek.Maglenen = true;
                }
                else if (magNee == true)
                {
                    boek.Maglenen = false;
                }
                boek.Beschikbaar = exemplaar;

                //if (RepositoryBoek.NummerBestaat(ISBNB))
                //{
                //    MessageBox.Show("Deze nummer bestaat, schrijf een andedre");
                //}
                //else
                //{
                //    RepositoryBoek.Update2(boek);
                //    Update();
                //}
                RepositoryBoek.Update2(boek);
                Update();
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }
            boek.TitelBoek = titel;

        }

        //Toevoegen boek
        private void BoekToevoegen()
        {
            try
            {
                BoekGegevens boek = new BoekGegevens()
                {
                    TitelBoek = TitelB,
                    Auteur = AuteurB,
                    ISBN = ISBNB,
                    AaankoopPrijs = PrjsB,
                    Beschikbaar = AantalExemplarenB
                };

                if (MaguitgeleendJa == true)
                {
                    boek.Maglenen = true;
                }
                else
                {
                    boek.Maglenen = false;
                }

                if (RepositoryBoek.NummerBestaat(ISBNB))
                {
                    RepositoryBoek.Update(boek);
                }
                else
                {
                    RepositoryBoek.CreateBoek(boek);
                }


                Update();
                Verwijder();
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }         

        }

        //Lijst tonen
        public void Update()
        {
            Boeken = null;
            Boeken = RepositoryBoek.GetAllBoeken();
        }

        //Gegevens tonen van selected value
        private void SelectedValu()
        {
            if(SelectedBoek != null)
            {
                BoekGegevens boek = (BoekGegevens)SelectedBoek;
                ID = boek.ID;
                TitelB = boek.TitelBoek;
                AuteurB = boek.Auteur;
                ISBNB = boek.ISBN;
                PrjsB = boek.AaankoopPrijs;
                AantalExemplarenB = boek.Beschikbaar;

                if(boek.Maglenen == true)
                {
                    MaguitgeleendJa = true;
                }
                else if(boek.Maglenen == false)
                {

                    MagUitgeleendNee = true;
                }
                Enable = true;
                Enable2 = false;
            }
            else
            {
                Verwijder();
                Enable = false;
                Enable2 = true;
            }
        }

        private void OnProprtyChanged([CallerMemberName] string prop = null)
        {
            if(prop != null && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion
    }
}
