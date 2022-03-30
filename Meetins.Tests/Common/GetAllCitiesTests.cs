using Meetins.Abstractions.Repositories;
using Meetins.Models.Common;
using Meetins.Services.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Tests.Common
{
    [TestClass]
    public class GetAllCitiesTests : BaseServiceTest
    {
        [TestMethod]
        public async Task GetAllCitiesAsyncTest_ReturnsCities()
        {
            //Arrange
            async Task<IEnumerable<CityOutput>> GetMockedCities()
            {
                return new List<CityOutput>
                {
                new CityOutput { CityName ="Москва", HasKudagoEvents= true},
                new CityOutput { CityName ="Санкт-Петербург", HasKudagoEvents= true},
                new CityOutput { CityName ="Екатеринбург", HasKudagoEvents= true}
                };
            }

            var mockRepository = new Mock<ICommonRepository>();
            mockRepository.Setup(a => a.GetAllCitiesAsync()).Returns(GetMockedCities());

            var service = new CommonService(mockRepository.Object, PostgreDbContext);

            //Act
            var result = await service.GetAllCitiesAsync();

            //Assert
            Assert.IsTrue(result.Any());
        }

    }
}
