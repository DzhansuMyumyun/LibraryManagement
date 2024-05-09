using Bibliotheek.Data_access;
using Bibliotheek.Model;
using Bibliotheek.ViewModel;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LedenRegistreren.Tests
{
    [TestFixture]
    public class LedenRegistrerenViewModelTests
    {
        IRepositoryLeden repoLid;
        LedenRegistrerenViewModel sut;

        [Test]
        public void LedenRegistratieWindow_opstraten_alle_leden_in_lijst_plaatsen()
        {
            
            var expectedlist = new List<LedenGegevens>()
            {
                new LedenGegevens("Coni", "Boni", "coni@abv.bg","Man",new DateTime(1999,10,10),"11.11.11-123.11",new DateTime(2020,6,9))
            };
            repoLid = Substitute.For<IRepositoryLeden>();
            repoLid.GetAllLeden().Returns(expectedlist);
            sut = new LedenRegistrerenViewModel(repoLid, null);

            Assert.AreEqual(expectedlist, sut.Leden);
        }

        [Test]
        public void LedenRegistratie_foutieve_email()
        {
            repoLid = Substitute.For<IRepositoryLeden>();
            repoLid.GetAllLeden().Returns(new List<LedenGegevens>());
            sut = new LedenRegistrerenViewModel(repoLid, null);

            sut.Voornaam = "Dzhansu";
            sut.Familienaam = "Halim";
            sut.Email = "dzhansu";
            sut.GeslachtSelected = Geslacht.Vrouw;
            sut.GeboorteDatum = new DateTime(1999, 6, 9);
            sut.RijksNummer = "11.11.11-123.11";
            sut.DatumBetalinLidgeld = new DateTime(2020, 6, 9);
            //sut.Toevoegen.Execute(null);

            repoLid.Received(0).CreatLeden(new LedenGegevens());

        }

        [Test]
        public void LedenRegistratie_foutieve_rijksregisternummer()
        {
            repoLid = Substitute.For<IRepositoryLeden>();
            repoLid.GetAllLeden().Returns(new List<LedenGegevens>());
            sut = new LedenRegistrerenViewModel(repoLid, null);

            sut.Voornaam = "Dzhansu";
            sut.Familienaam = "Halim";
            sut.Email = "dzhansu@abv.bg";
            sut.GeslachtSelected = Geslacht.Vrouw;
            sut.GeboorteDatum = new DateTime(1999, 6, 9);
            sut.RijksNummer = "11.11.11-123";
            sut.DatumBetalinLidgeld = new DateTime(2020, 6, 9);
            //sut.Toevoegen.Execute(null);

            repoLid.Received(0).CreatLeden(new LedenGegevens());
        }

        [Test]
        public void LedenRegistratie_ontbreken_gegevens()
        {
            repoLid = Substitute.For<IRepositoryLeden>();
            repoLid.GetAllLeden().Returns(new List<LedenGegevens>());
            sut = new LedenRegistrerenViewModel(repoLid,null);

            sut.Voornaam = null;
            sut.Familienaam = "Halim";
            sut.Email = null;
            sut.GeslachtSelected = Geslacht.Vrouw;
            sut.GeboorteDatum = new DateTime(1999, 6, 9);
            sut.RijksNummer = null;
            sut.DatumBetalinLidgeld = new DateTime(2020, 6, 9);
            //sut.Toevoegen.Execute(null);

            repoLid.Received(0).CreatLeden(new LedenGegevens());
        }

        [Test]
        public void LedenRegistratieWindow_lid_goed_geregistreerd()
        {

            var expectedlist = new List<LedenGegevens>()
            {
                new LedenGegevens("Coni", "Boni", "coni@abv.bg","Man",new DateTime(1999,10,10),"11.11.11-123.11",new DateTime(2020,6,9))
            };
            var repoLid = Substitute.For<IRepositoryLeden>();
            repoLid.GetAllLeden().Returns(expectedlist);
            LedenRegistrerenViewModel sut = new LedenRegistrerenViewModel(repoLid, null);

            sut.Voornaam = "Coni";
            sut.Familienaam = "Boni";
            sut.Email = "coni@abv.bg";
            sut.GeslachtSelected = Geslacht.Man;
            sut.GeboorteDatum = new DateTime(1999, 10, 10);
            sut.RijksNummer = "11.11.11-123.11";
            sut.DatumBetalinLidgeld = new DateTime(2020, 6, 9);
            sut.Toevoegen.Execute(null);

            Assert.AreEqual(expectedlist, sut.Leden);
        }

        [Test]
        public void LedenRegistratieWindow_bestaande_lidGegevens_bewerken()
        {
            LedenGegevens lid = new LedenGegevens("Coni", "Boni", "coni@abv.bg", "Man", new DateTime(1999, 10, 10), "11.11.11-123.11", new DateTime(2020, 6, 9));
            var expectedlist = new List<LedenGegevens>()
            {
                lid
            };

             repoLid = Substitute.For<IRepositoryLeden>();
            repoLid.GetAllLeden().Returns(expectedlist);
             sut = new LedenRegistrerenViewModel(repoLid, null);

            sut.Voornaam = "Coni";
            sut.Familienaam = "Boni";
            sut.Email = "coni@outlook.com";
            sut.GeslachtSelected = Geslacht.Vrouw;
            sut.GeboorteDatum = new DateTime(1999, 10, 10);
            sut.RijksNummer = "11.11.11-123.12";
            sut.DatumBetalinLidgeld = new DateTime(2020, 6, 9);            
            sut.ledenrep.UpdateLeden(lid);

            Assert.AreEqual(expectedlist, sut.Leden);
        }

    }
}
