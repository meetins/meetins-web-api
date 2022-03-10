using System;

namespace Meetins.Models.Events.Output
{
    /// <summary>
    /// Выходная модель категорий событий.
    /// </summary>
    public class EventsCategoryOutput
    {
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public Guid EventsCategoryId { get; set; }
        /// <summary>
        /// Название категироии.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
