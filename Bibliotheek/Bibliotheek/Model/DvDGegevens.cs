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
    public enum TypeDvd
    {
        Documentaire,
        Speelfilm
    }

    public class DvDGegevens
    {
        #region prop
        private int _id;
        /// <summary>
        /// ID van dvd
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Required(ErrorMessage = "Please enter Titel")]
        [DataType(DataType.Text)]
        private string _titel;
        /// <summary>
        /// Titel van dvd
        /// </summary>
        public string Titel
        {
            get { return _titel; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("Titel moet ingevuld worden");
                _titel = value;
            }
        }

        [Required(ErrorMessage = "Please enter Ecode"), MaxLength(40)]
        private string _ecode;
        /// <summary>
        /// Ecode van Dvd
        /// </summary>
        public string Ecode
        {
            get { return _ecode; }
            set
            {
                string checkEcode = "^[0-9]([-/ .])[0-9]{5}([-/ .])[0-9]{5}([-/ .])[0-9]$";
                if (!Regex.IsMatch(value, checkEcode))
                {
                    throw new Exception("Schrijf een geldige Ecode nummer");
                }
                _ecode = value;
            }
        }

        [Required(ErrorMessage = "Please enter Prijs")]
        private double _prijs;
        /// <summary>
        /// Prijs van dvd
        /// </summary>
        public double Prijs
        {
            get { return _prijs; }
            set { _prijs = value; }
        }

        
        private TypeDvd _type;
        /// <summary>
        /// Type van dvd
        /// </summary>
        [NotMapped]
        public TypeDvd Typedvd
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }

        [Required(ErrorMessage = "Please enter GekozenType")]
        [DataType(DataType.Text)]
        private string _gekozenType;
        /// <summary>
        /// Gekozen Type
        /// </summary>
        public string GekozenType
        {
            get { return _gekozenType; }
            set { _gekozenType = value; }
        }

        [Required(ErrorMessage = "Please enter aantal")]
        private int _aantal;
        /// <summary>
        /// Aantal exemplaren dvd
        /// </summary>
        public int Aantal
        {
            get { return _aantal; }
            set { _aantal = value; }
        }
        #endregion

        //constructo
        public DvDGegevens( string titel, string ecode, double prijs, string type, int aantal)
        {
            Titel = titel;
            Ecode = ecode;
            Prijs = prijs;
            GekozenType = type;
            Aantal = aantal;
        }
        public DvDGegevens()
        {

        }

        #region  methods
        public override bool Equals(object obj)
        {
            if (obj is DvDGegevens)
            {
                var dvd = (DvDGegevens)obj;
                return dvd.Titel == Titel && dvd.Ecode == Ecode && dvd.Prijs == Prijs && dvd.Typedvd == Typedvd && dvd.Aantal == Aantal;
            }

            return false;

        }

        public override string ToString()
        {
            return Titel + ", Type dvd: " + GekozenType + "( " + Aantal + " )" ;
        }
        #endregion
    }
}
