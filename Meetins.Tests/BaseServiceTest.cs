using Meetins.Core.Data;
using Meetins.Services.Common;
using Meetins.Services.KudaGo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Meetins.Tests
{
    public class BaseServiceTest
    {
        protected string PostgreConfigString { get; set; }
        protected IConfiguration AppConfiguration { get; set; }

        protected PostgreDbContext PostgreDbContext;

        protected CommonService CommonService;
        protected CommonRepository CommonRepository;

        protected KudaGoService KudaGoService;
        protected KudaGoRepository KudaGoRepository;

        public BaseServiceTest()
        {

            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();
                            
            AppConfiguration = builder.Build();

            PostgreConfigString = AppConfiguration["ConnectionStrings:NpgTestSqlConnection"];

            // Настройка тестовых контекстов.
            var optionsBuilder = new DbContextOptionsBuilder<PostgreDbContext>();
            optionsBuilder.UseNpgsql(PostgreConfigString);
            PostgreDbContext = new PostgreDbContext(optionsBuilder.Options);

            // Настройка экземпляров сервисов для тестов.
            CommonRepository = new CommonRepository(PostgreDbContext);
            CommonService = new CommonService(CommonRepository, PostgreDbContext);

            KudaGoRepository = new KudaGoRepository();
            KudaGoService = new KudaGoService(KudaGoRepository,PostgreDbContext);
        }
    }
}
