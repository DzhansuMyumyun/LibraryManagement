using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bibliotheek.Model
{
    public enum Geslacht
    {
        Man,
        Vrouw,
        Onbekend
    }

    public class LedenGegevens
    {
        #region properties
        private int _id;
        /// <summary>
        /// ID van het lid
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _voornaam;
        /// <summary>
        /// Voornaam van het lid
        /// </summary>
        public string Voornaam
        {
            get { return _voornaam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("Voornaam invullen");
                _voornaam = value;
            }
        }

        private string _familienaam;
        /// <summary>
        /// Familienaam van het lid
        /// </summary>
        public string Familienaam
        {
            get { return _familienaam; }
            set
            {
                if(string.IsNullOrWhiteSpace(value)) throw new Exception("Familienaam invullen");
                _familienaam = value;
            }
        }

        private string _email;
        /// <summary>
        /// Email van het lid
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                Regex controle = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = controle.Match(value);
                if (!match.Success)
                {
                    throw new Exception("Geldige email schrijven");
                }
                _email = value;
            }
        }

        private Geslacht _geslacht;
        /// <summary>
        /// Geslacht van het lid
        /// </summary>
        [NotMapped]
        public Geslacht GeslachtLid
        {
            get { return _geslacht; }
            set { _geslacht = value; }
        }

        private string _Ges;
        /// <summary>
        /// Geslacht lid
        /// </summary>
        public string Ges
        {
            get { return _Ges; }
            set { _Ges = value; }
        }

        private string _rijksregisternummer;
        /// <summary>
        /// RijksRegisterNummer van het lid
        /// </summary>
        public string Rijksregisternummer
        {
            get { return _rijksregisternummer; }
            set
            {
                Regex controle = new Regex(@"^[0-9]{2}([./ .])[0-9]{2}([./ .])[0-9]{2}([-/ .])[0-9]{3}([./ .])[0-9]{2}$");
                Match match = controle.Match(value);
                if (!match.Success)
                {
                    throw new Exception("Geldige rijksregisternummer schrijven");
                }
                _rijksregisternummer = value;
            }
        }

        [DataType(DataType.DateTime)]
        private DateTime _geboortedat;
        /// <summary>
        /// Geboortedatum lid
        /// </summary>
        public DateTime GeboorteDat
        {
            get {
                if(_geboortedat == null)
                {

                    new DateTime();
                    throw new Exception("Geboortedatum selecteren");
                }
                return _geboortedat; }
            set { _geboortedat = value; }
        }


        [DataType(DataType.DateTime)]
        private DateTime _datumBetalinLidgeld;
        /// <summary>
        /// Datum van laatste betaling lidgeld
        /// </summary>
        public DateTime DatumBetalingLidgeld
        {
            get { return _datumBetalinLidgeld; }
            set { _datumBetalinLidgeld = value; }
        }
        #endregion

        //constructor
        public LedenGegevens() { }
        public LedenGegevens( string voornaam, string familienaam, string email, string ges , DateTime Geboorte, string rijksnummer, DateTime datumBetaling)
        {
           
            Voornaam = voornaam;
            Familienaam = familienaam;
            Email = email;
            Ges = ges;
            GeboorteDat = Geboorte;
            Rijksregisternummer = rijksnummer;
            DatumBetalingLidgeld = datumBetaling;
        }


        //methods
        public override bool Equals(object obj)
        {
            if(obj is LedenGegevens)
            {
                var lid = (LedenGegevens)obj;
                return lid.Voornaam == Voornaam && lid.Familienaam == Familienaam && lid.Email == Email && lid.GeslachtLid == GeslachtLid && lid.Rijksregisternummer == Rijksregisternummer && lid.DatumBetalingLidgeld == DatumBetalingLidgeld;
            }
            return false;
        }

        public override string ToString()
        {
            return Voornaam + " " + Familienaam;
        }
    }
}
