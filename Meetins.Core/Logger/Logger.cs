using Meetins.Core.Data;
using Meetins.Models.Entities;
using System;
using System.Threading.Tasks;

namespace Meetins.Core.Logger
{
    /// <summary>
    /// Класс логирования ошибок в БД.
    /// </summary>
    public class Logger
    {
        private readonly LogEntity _log;
        private readonly PostgreDbContext _postgreDbContext;

        public Logger(PostgreDbContext postgreDbContext, string typeException, string exception, string stackTrace)
        {
            /// <summary>
            /// Инициализация объекта лога.
            /// </summary>
            _log = new LogEntity
            {
                TypeException = typeException,
                Exception = exception,
                StackTrace = stackTrace,
                Date = DateTime.Now
            };

            _postgreDbContext = postgreDbContext;
        }

        /// <summary>
        /// Метод пишет лог в базу с типом Critical.
        /// </summary>
        public async Task LogCritical()
        {
            _log.LogLvl = "Critical";
            await _postgreDbContext.Logs.AddAsync(_log);
            await _postgreDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Метод пишет лог в базу с типом Information.
        /// </summary>
        public async Task LogInformation()
        {
            _log.LogLvl = "Information";
            await _postgreDbContext.Logs.AddAsync(_log);
            await _postgreDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Метод пишет лог в базу с типом Error.
        /// </summary>
        public async Task LogError()
        {
            _log.LogLvl = "Error";
            await _postgreDbContext.Logs.AddAsync(_log);
            await _postgreDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Метод пишет лог в базу с типом Warning.
        /// </summary>
        public async Task LogWarning()
        {
            _log.LogLvl = "Warning";
            await _postgreDbContext.Logs.AddAsync(_log);
            await _postgreDbContext.SaveChangesAsync();
        }
    }
}
