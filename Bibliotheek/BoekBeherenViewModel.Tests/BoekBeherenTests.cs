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

namespace BoekbeherenViewModel.Tests
{
    [TestFixture]
    public class BoekbeherenTests
    {
        IRepositoryBoek IRepository;
        BoekBeherenViewModel sut;
        BoekGegevens boek = new BoekGegevens();

        [SetUp]
        public void Setup()
        {
            IRepository = Substitute.For<IRepositoryBoek>();
            sut = new BoekBeherenViewModel(IRepository, null);
        }


        [Test]
        public void Lijst_metAlleBoeken_tonen_bijOpstart()
        {
                    
            boek = new BoekGegevens()
            {
                TitelBoek = "Pepe",
                Auteur = "Piki",
                ISBN = "123-12-123-1234-5",
                AaankoopPrijs = 12,
                Maglenen = true,
                Beschikbaar = 2
            };
            List<BoekGegevens> expected = new List<BoekGegevens>()
            {
                boek
            };
            IRepository.GetAllBoeken().Returns(expected);
            sut.Update();

            Assert.AreEqual(expected, sut.Boeken);
        }

        [Test]
        public void Invullen_foutieve_ISBNnummer()
        {
            IRepository.GetAllBoeken().Returns(new List<BoekGegevens>());

            sut.TitelB = "Mono";
            sut.AuteurB = "Pepe";
            sut.ISBNB = "123";
            sut.PrjsB = 23.2;
            sut.MaguitgeleendJa = true;
            sut.MagUitgeleendNee = false;
            sut.AantalExemplarenB = 5;
            sut.Toevoegen.Execute(null);

            IRepository.Received(0).CreateBoek(new BoekGegevens());
        }

        [Test]
        public void Invullen_juiste_ISBNnummer_CreateBoek()
        {
            IRepository.GetAllBoeken().Returns(new List<BoekGegevens>());

            sut.TitelB = "Mono";
            sut.AuteurB = "Pepe";
            sut.ISBNB = "123-12-123-1235-1";
            sut.PrjsB = 23.2;
            sut.MaguitgeleendJa = true;
            sut.MagUitgeleendNee = false;
            sut.AantalExemplarenB = 5;
            sut.Toevoegen.Execute(null);

            IRepository.Received(1).CreateBoek(new BoekGegevens()
            {
                TitelBoek = "Mono",
                Auteur = "Pepe",
                ISBN = "123-12-123-1235-1",
                AaankoopPrijs = 23.2,
                Maglenen = true,
                Beschikbaar = 5
            });
        }

        [Test]
        public void Testen_wanneer_erTweeKeer_zelfde_ISBNnummer_ingevoerdWordt()
        {
            boek = new BoekGegevens()
            {
                TitelBoek = "Pepe",
                Auteur = "Piki",
                ISBN = "123-12-123-1234-5",
                AaankoopPrijs = 12,
                Maglenen = true,
                Beschikbaar = 2
            };

            List<BoekGegevens> expected = new List<BoekGegevens>()
            {
                boek
            };

            IRepository.GetAllBoeken().Returns(expected);

            sut.TitelB = "Mono";
            sut.AuteurB = "Pepe";
            sut.ISBNB = "123-12-123-1234-5";
            sut.PrjsB = 23.2;
            sut.MaguitgeleendJa = true;
            sut.MagUitgeleendNee = false;
            sut.AantalExemplarenB = 5;
            sut.Toevoegen.Execute(null);
            sut.RepositoryBoek.Update(boek);

            IRepository.Received(0).CreateBoek(new BoekGegevens()
            {
                TitelBoek = "Pepe",
                Auteur = "Piki",
                ISBN = "123-12-123-1234-5",
                AaankoopPrijs = 12,
                Maglenen = true,
                Beschikbaar = 3
            });
        }
    }
}
