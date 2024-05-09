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
    public class DvDBeherenViewModel : INotifyPropertyChanged
    {

        #region prop
        private List<DvDGegevens> _dvdS;
        public List<DvDGegevens> DvdS
        {
            get { return _dvdS; }
            set { _dvdS = value;
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

        private string _titelD;
        public string TitelD
        {
            get { return _titelD; }
            set { _titelD = value;
                OnProprtyChanged();
            }
        }

        private string _ecodeD;
        public string EcodeD
        {
            get { return _ecodeD; }
            set { _ecodeD = value;
                OnProprtyChanged();
            }
        }

        private double _aankooprijsD; 
        public double AankoopPrijsD
        {
            get { return _aankooprijsD; }
            set { _aankooprijsD = value;
                OnProprtyChanged();
            }
        }

        private Array _type;
        public Array Type
        {
            get { return _type; }
            set { _type = value;
                OnProprtyChanged();
            }
        }

        private TypeDvd _typeKies;
        public TypeDvd TypeKies
        {
            get { return _typeKies; }
            set { _typeKies = value;
                OnProprtyChanged();
            }
        }

        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set { _selectedType = value;
                OnProprtyChanged();
            }
        }

        private int _exemplaren;
        public int AantalExemplare
        {
            get { return _exemplaren; }
            set { _exemplaren = value;
                OnProprtyChanged();
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
                SelectedValue();

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

        public IRepositoryDvD repositoryDvD;
        public IClosable closable;


        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        //constr
        public DvDBeherenViewModel(IRepositoryDvD repositorydvd, IClosable closable2)
        {
            closable = closable2;
            repositoryDvD = repositorydvd;
            Type = Enum.GetValues(typeof(TypeDvd));
            Toevoegen = new RelayCommand(ToevoegenDvd);
            Bewerken = new RelayCommand(BewerkenDvd);
            Close = new RelayCommand(Closable);
            Info = new RelayCommand(FormatEcode);
            SelectedValue();
            Update();
            Enable2 = true;
        }

        #region  methods

        private void FormatEcode()
        {
            MessageBox.Show("Format van de Ean-code: X-XXXXX-XXXXX-X");
        }

        //Window sluiten
        private void Closable()
        {
            closable.Close();
        }

        //Edit gegevens
        private void BewerkenDvd()
        {
            DvdBewerken((DvDGegevens)SelectedItem, TitelD, EcodeD, AankoopPrijsD, TypeKies, AantalExemplare);
        }

        private void DvdBewerken(DvDGegevens dvd, string titel, string code, double prijs, TypeDvd typeDvd, int exemplaren)
        {
            try
            {
                dvd.Titel = titel;
                dvd.Ecode = code;
                dvd.Prijs = prijs;
                dvd.Typedvd = typeDvd;
                dvd.Aantal = exemplaren;

                if (TypeKies == TypeDvd.Documentaire)
                {
                    dvd.GekozenType = "Documentaire";
                }
                else if (TypeKies == TypeDvd.Speelfilm)
                {
                    dvd.GekozenType = "Speelfilm";
                }

                //if (repositoryDvD.NummerBestaat(EcodeD))
                //{
                //    MessageBox.Show("Deze code bestaat, schrijf een andedre");
                //}
                //else
                //{
                //    repositoryDvD.Update2(dvd);
                //    Update();
                //}

                repositoryDvD.Update2(dvd);
                Update();
                Verwijder();
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }

   
        }

        //Toevoegen Dvd
        private void ToevoegenDvd()
        {
            try
            {
                DvDGegevens dvd = new DvDGegevens()
                {
                    Titel = TitelD,
                    Ecode = EcodeD,
                    Prijs = AankoopPrijsD,
                    Aantal = AantalExemplare,
                    Typedvd = TypeKies
                };

                switch (TypeKies)
                {
                    case TypeDvd.Documentaire:
                        dvd.GekozenType = "Documentaire";
                        break;
                    case TypeDvd.Speelfilm:
                        dvd.GekozenType = "Speelfilm";
                        break;
                }

                if (repositoryDvD.NummerBestaat(EcodeD))
                {
                    repositoryDvD.Update(dvd);
                }
                else
                {
                    repositoryDvD.CreateDvd(dvd);
                }

                Update();
                Verwijder();
            }
            catch(Exception message)
            {
                MessageBox.Show(message.Message);
            }


        }

        //Lijst update
        public void Update()
        {
            DvdS = null;
            DvdS = repositoryDvD.GetAllBDvDs();
        }

        //Verwijder velden
        private void Verwijder()
        {
            ID = AantalExemplare = 0;
            TitelD = EcodeD = null;
            AankoopPrijsD = 0;
            TypeKies = 0;
        }

        //Gegevens tonen van selected value
        private void SelectedValue()
        {
            if(SelectedItem != null)
            {
                DvDGegevens dvd = (DvDGegevens)SelectedItem;
                ID = dvd.Id;
                TitelD = dvd.Titel;
                EcodeD = dvd.Ecode;
                AankoopPrijsD = dvd.Prijs;
                AantalExemplare = dvd.Aantal;

                if(dvd.GekozenType == "Documentaire")
                {
                    dvd.Typedvd = TypeDvd.Documentaire;
                }
                else if(dvd.GekozenType == "Speelfilm")
                {
                    dvd.Typedvd = TypeDvd.Speelfilm;
                }
                TypeKies = dvd.Typedvd;
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
            if (prop != null && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        #endregion
    }
}
