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

namespace DvdBeherenViewModel.Tests
{
    [TestFixture]
    public class DvdBeherenTests
    {
        IRepositoryDvD IRepo;
        DvDBeherenViewModel sut;
        DvDGegevens dvd = new DvDGegevens();

        [SetUp]
        public void Setup()
        {
            IRepo = Substitute.For<IRepositoryDvD>();
            sut = new DvDBeherenViewModel(IRepo, null);

        }

        [Test]
        public void Lijst_Testen_bijOpsatrt()
        {
            dvd = new DvDGegevens("Miki", "1-12345-12345-8", 23.2, "Documentaire", 5);
            List<DvDGegevens> expected = new List<DvDGegevens>()
            {
                dvd
            };

            IRepo.GetAllBDvDs().Returns(expected);
            sut.Update();
            Assert.AreEqual(expected, sut.DvdS);
        }

        [Test]
        public void Invoeren_van_foutieve_Ecode()
        {
            IRepo.GetAllBDvDs().Returns(new List<DvDGegevens>());

            sut.TitelD = "Miki";
            sut.EcodeD = "123";
            sut.AankoopPrijsD = 23;
            sut.AantalExemplare = 5;
            sut.TypeKies = TypeDvd.Documentaire;
            sut.Toevoegen.Execute(null);

            IRepo.Received(0).CreateDvd(new DvDGegevens());
        }

        [Test]
        public void Create_dvd_met_juist_gegevens()
        {
            IRepo.GetAllBDvDs().Returns(new List<DvDGegevens>());

            sut.TitelD = "Miki";
            sut.EcodeD = "1-12345-12345-8";
            sut.AankoopPrijsD = 23;
            sut.AantalExemplare = 5;
            sut.TypeKies = TypeDvd.Documentaire;
            sut.Toevoegen.Execute(null);

            IRepo.Received(1).CreateDvd(new DvDGegevens("Miki", "1-12345-12345-8", 23, "Documentaire", 5));

        }

        [Test]
        public void Bewerken_bestaande_dvd()
        {
             dvd = new DvDGegevens("Miki", "1-12345-12345-8", 23, "Documentaire", 5);
            List<DvDGegevens> expected = new List<DvDGegevens>()
            {
                dvd
            };
            IRepo.GetAllBDvDs().Returns(expected);

            sut.TitelD = "Miki";
            sut.EcodeD = "1-12345-12345-8";
            sut.AankoopPrijsD = 23;
            sut.AantalExemplare = 5;
            sut.TypeKies = TypeDvd.Documentaire;
            sut.Bewerken.Execute(null);
            sut.repositoryDvD.Update(dvd);

            IRepo.Received(0).CreateDvd(new DvDGegevens("Miki", "1-12345-12345-8", 23, "Documentaire", 6));
        }
    }
}
