using GeoLib.Contracts;
using GeoLib.Data;
using GeoLib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GeoLib.Tests
{
    [TestClass]
    public class ManagerTests
    {
        [TestMethod]
        public void TestGetZip()
        {
			Mock<IZipCodeRepository> mockZipRepo = new Mock<IZipCodeRepository>();
			ZipCode zipCode = new ZipCode()
			{
				City = "LINCOLN PARK",
				State = new State() { Abbreviation = "NJ" },
				Zip = "07035"
			};

			mockZipRepo.Setup(obj => obj.GetByZip("07035")).Returns(zipCode);

			IGeoService geoService = new GeoManager(mockZipRepo.Object);
			ZipCodeData data = geoService.GetZipInfo("07035");
			Assert.IsTrue(data.City.ToUpper() == "LINCOLN PARK");
			Assert.IsTrue(data.State == "NJ");
        }
    }
}
