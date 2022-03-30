using Meetins.Abstractions.Repositories;
using Meetins.Core.Exceptions;
using Meetins.Models.KudaGo;
using Meetins.Services.KudaGo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Tests.KudaGo
{
    /// <summary>
    /// Класс для тестирования получения списка городов с KudaGo.
    /// </summary>
    [TestClass]
    public class GetAllAvailableCitiesTests : BaseServiceTest
    {    
        [TestMethod]
        public async Task GetAllAvailableCitiesAsyncTest_ReturnsCities()
        {
            //Arrange
            async Task<IEnumerable<KudaGoOutput>> GetMockedCities()
            {
                return new List<KudaGoOutput>
                {
                new KudaGoOutput { Name ="Москва", Slug ="msk"},
                new KudaGoOutput { Name ="Санкт-Петербург", Slug ="spb"},
                new KudaGoOutput { Name ="Екатеринбург", Slug ="ekb"}
                };
            }
                        
            var mockRepository = new Mock<IKudaGoRepository>();
            mockRepository.Setup(a => a.GetAllAvailableCitiesAsync()).Returns(GetMockedCities());

            var service = new KudaGoService(mockRepository.Object, PostgreDbContext);

            //Act
            var result = await service.GetAllAvailableCitiesAsync();

            //Assert
            Assert.IsTrue(result.Any());           
        }

        [TestMethod]
        public async Task GetAllAvailableCitiesAsyncTest_ThrowsNotFoundException()
        {
            //Arrange
            var mockRepository = new Mock<IKudaGoRepository>();
            mockRepository.Setup(a => a.GetAllAvailableCitiesAsync()).Throws(new NotFoundException("KudaGo Api locations notfound result code"));

            var service = new KudaGoService(mockRepository.Object, PostgreDbContext);
            
            try
            {
                //Act
                var result = await service.GetAllAvailableCitiesAsync();
                
                //Assert
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsTrue(ex is NotFoundException);            
            }
        }
    }
}

