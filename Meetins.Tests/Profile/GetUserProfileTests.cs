using Meetins.Abstractions.Repositories;
using Meetins.Models.Exceptions;
using Meetins.Models.Entities;
using Meetins.Services.Profile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Meetins.Tests.Profile
{   
    /// <summary>
    /// Класс для тестирования методов ProfileService на получение профиля.
    /// </summary>
    [TestClass]
    public class GetUserProfileTests : BaseServiceTest
    {
        [TestMethod]
        public async Task GetUserProfileByLoginAsyncTest_ThrowsNotFoundException()
        {
            //Arrange
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(a => a.GetUserByLoginAsync(It.IsAny<string>())).Throws(new NotFoundException("Пользователь с таким логином не найден."));

            var service = new ProfileService(mockRepository.Object, PostgreDbContext);

            //Act
            try
            {
                var result = await service.GetUserProfileByLoginAsync("super_dendi");
                Assert.Fail("Исключение NotFoundException в тестируемом сервисе не выбросилось.");
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsTrue(ex is NotFoundException);
            }
        }

        [TestMethod]
        public async Task GetUserProfileByLoginAsyncTest_ReturnsProfile()
        {
            //Arrange
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(a => a.GetUserByLoginAsync(It.IsAny<string>())).ReturnsAsync(new UserEntity());

            var service = new ProfileService(mockRepository.Object, PostgreDbContext);

            //Act
            var result = await service.GetUserProfileByLoginAsync("super_dendi");

            //Assert
            Assert.IsNotNull(result);            
        }

        [TestMethod]
        public async Task GetUserProfileByIdAsyncTest_ReturnsProfile()
        {
            //Arrange
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(a => a.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new UserEntity());

            var service = new ProfileService(mockRepository.Object, PostgreDbContext);

            //Act
            var result = await service.GetUserProfileAsync(Guid.NewGuid());

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetUserProfileByIdAsyncTest_ThrowsNotFoundException()
        {
            //Arrange
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(a => a.GetUserByIdAsync(It.IsAny<Guid>())).Throws(new NotFoundException("Пользователь с таким логином не найден."));

            var service = new ProfileService(mockRepository.Object, PostgreDbContext);

            //Act
            try
            {
                var result = await service.GetUserProfileAsync(Guid.NewGuid());
                Assert.Fail("Исключение NotFoundException в тестируемом сервисе не выбросилось.");
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsTrue(ex is NotFoundException);
            }
        }
    }
}
