using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bibliotheek.Model
{
    public class BoekGegevens
    {
        #region prop
        private int _id;
        /// <summary>
        /// id van de boek 
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        
        [Required(ErrorMessage = "Please enter Titel")]
        [DataType(DataType.Text)]
        private string _titel;
        /// <summary>
        /// titel van de boek 
        /// </summary>      
        public string TitelBoek
        {
            get { return _titel; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("Titel moet ingevuld worden");
                _titel = value;
            }
        }
        

        [Required(ErrorMessage = "Please enter auteur"), MaxLength(30)]
        [DataType(DataType.Text)]
        private string _auteur;
        /// <summary>
        /// auteur van de boek 
        /// </summary>
        public string Auteur
        {
            get { return _auteur; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("auteur moet ingevuld worden");

                _auteur = value;
            }
        }
       

        [Required(ErrorMessage = "Please enter isbn "), MaxLength(40)]
        private string _isbn;
        /// <summary>
        /// isbn nummer van de boek 
        /// </summary>
        public string ISBN
        {
            get { return _isbn; }
            set
            {
                string checkISBN = "^[0-9]{3}([-/ .])[0-9]{2}([-/ .])[0-9]{3}([-/ .])[0-9]{4}([-/ .])[0-9]$";
                if (!Regex.IsMatch(value, checkISBN))
                {
                    throw new Exception("Schrijf een geldige ISBN nummer");
                }
                _isbn = value;
            }
        }

        [Required(ErrorMessage = "Please enter aankoopprijs"), MaxLength(40)]
        private double _aankoopprijs;
        /// <summary>
        /// prijs van de boek 
        /// </summary>
        ///  //Data Annotations constraints
        public double AaankoopPrijs
        {
            get { return _aankoopprijs; }
            set { _aankoopprijs = value; }
        }

        [Required(ErrorMessage = "Please enter maglenen"), MaxLength(30)]
        private bool _maglenen;
        /// <summary>
        /// mag lenen of niet   
        /// </summary>
        ///  //Data Annotations constraints
        public bool Maglenen
        {
            get { return _maglenen; }
            set { _maglenen = value; }
        }

        [Required(ErrorMessage = "Please enter aantal beschikbaar"), MaxLength(30)]
        [Range(1, 500, ErrorMessage = "Please enter correct value")]
        private int _beschikbaar;
        /// <summary>
        /// aantal beschikbare versies van de boek 
        /// </summary>
        public int Beschikbaar
        {
            get { return _beschikbaar; }
            set { _beschikbaar = value; }
        }
        #endregion

        public BoekGegevens(string titel, string auteur, string isbn, double prijs, bool maglenen, int besch)
        {

            TitelBoek = titel;
            Auteur = auteur;
            ISBN = isbn;
            AaankoopPrijs = prijs;
            Maglenen = maglenen;
            Beschikbaar = besch;

        }
        public BoekGegevens()
        {

        }

        #region  methods
        public override bool Equals(object obj)
        {
            if(obj is BoekGegevens)
            {
                var boek = (BoekGegevens)obj;
                return boek.TitelBoek == TitelBoek && boek.Auteur == Auteur && boek.ISBN == ISBN && boek.AaankoopPrijs == AaankoopPrijs && boek.Beschikbaar == Beschikbaar && boek.Maglenen == Maglenen;
            }

            return false;

        }

        public override string ToString()
        {
            return TitelBoek + ", Auteur: " + Auteur  + "( " + Beschikbaar + " )";
        }
        #endregion
    }
}
