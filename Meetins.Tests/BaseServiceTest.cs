using Meetins.Core.Data;
using Meetins.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Meetins.Tests
{
    public class BaseServiceTest
    {
        protected string PostgreConfigString { get; set; }
        protected IConfiguration AppConfiguration { get; set; }

        protected PostgreDbContext PostgreDbContext;

        protected CommonService CommonService;
        protected CommonRepository CommonRepository;

        public BaseServiceTest()
        {
            // Настройка тестовых строк подключения.
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            AppConfiguration = builder.Build();
            PostgreConfigString = AppConfiguration["ConnectionStrings:NpgTestSqlConnection"];

            // Настройка тестовых контекстов.
            var optionsBuilder = new DbContextOptionsBuilder<PostgreDbContext>();
            optionsBuilder.UseNpgsql(PostgreConfigString);
            PostgreDbContext = new PostgreDbContext(optionsBuilder.Options);


            // Настройка экземпляров сервисов для тестов.
            CommonRepository = new CommonRepository(PostgreDbContext);
            CommonService = new CommonService(CommonRepository, PostgreDbContext); 
        }
    }
}
