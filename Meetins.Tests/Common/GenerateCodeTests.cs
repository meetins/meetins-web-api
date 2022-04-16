using Meetins.Abstractions.Repositories;
using Meetins.Services.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Meetins.Tests.Common
{
    [TestClass]
    public class GenerateCodeTests : BaseServiceTest 
    {
        [TestMethod]
        public async Task GenerateCodeAsyncTest()
        {
            //Arrange
            var mockRepository = new Mock<ICommonRepository>();            

            var service = new CommonService(mockRepository.Object, PostgreDbContext);

            //Act
            var result = await service.GenerateCodeAsync();

            //Assert
            Assert.IsTrue(result.Length == 6);
        }
    }
}
