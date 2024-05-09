using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheek.Model
{
    public class OntleningGegevens
    {
        #region properties
        private int _id;
        /// <summary>
        /// ID van ontleningen
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
     
        private LedenGegevens _lid;
        /// <summary>
        /// Wie heeft het werk uitgeleend
        /// </summary>
        public LedenGegevens Lid
        {
            get { return _lid; }
            set { _lid = value; }
        }

        private BoekGegevens _boek;
        /// <summary>
        /// Boek ontlenen
        /// </summary>
        public BoekGegevens Boek
        {
            get { return _boek; }
            set { _boek = value; }
        }

        private DvDGegevens _dvd;
        /// <summary>
        /// Dvd dat ontleend
        /// </summary>
        public DvDGegevens Dvd
        {
            get { return _dvd; }
            set { _dvd = value; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        private DateTime _datumOntlening;
        /// <summary>
        /// Datum van ontlening
        /// </summary>
        public DateTime DatumOntlening
        {
            get {
                if (_uitersteInleverDatum == null) new DateTime();
                return _datumOntlening; }
            set { _datumOntlening = value; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        private DateTime _uitersteInleverDatum;
        /// <summary>
        /// UitersteInleverDaum
        /// </summary>
        public DateTime UitersteInleverDatum
        {
            get
            {
                if (_uitersteInleverDatum == null) new DateTime();
                return _uitersteInleverDatum; }
            set { _uitersteInleverDatum = value; }
        }


        private string _datumVanInlevering;
        /// <summary>
        /// Inlever datum van het werk
        /// </summary>
        public string DatumVanInlevering
        {
            get {
                
                return _datumVanInlevering; }
            set { _datumVanInlevering = value; }
        }
        #endregion

        //cons
        public OntleningGegevens() { }

        public OntleningGegevens(LedenGegevens lid, BoekGegevens boek, DateTime datumOntlening, DateTime datumUiterst)
        {
            Lid = lid;
            Boek = boek;
            DatumOntlening = datumOntlening;
            UitersteInleverDatum = datumUiterst;
            //DatumVanInlevering = new DateTime();

        }

        public OntleningGegevens(LedenGegevens lid, DvDGegevens dvD, DateTime datumOntlening, DateTime datumUiterst)
        {
            Lid = lid;
            Dvd = dvD;
            DatumOntlening = datumOntlening;
            UitersteInleverDatum = datumUiterst;
           // DatumVanInlevering = new DateTime();

        }

        //methods
        public override bool Equals(object obj)
        {
            if (obj is OntleningGegevens)
            {
                var werk = (OntleningGegevens)obj;
                return werk.Lid == Lid && werk.Boek == Boek && werk.Dvd == Dvd && werk.DatumOntlening == DatumOntlening && werk.DatumVanInlevering == DatumVanInlevering && werk.UitersteInleverDatum == UitersteInleverDatum;
            }
            return false;
        }

        public override string ToString()
        {

            return "ID Ontlening: " + ID + ", Uiterste inlever datum " + UitersteInleverDatum;

        }

    }
}
