using System.Collections.Generic;

namespace Meetins.Models.KudaGo
{
    /// <summary>
    /// Выходная модель для доступных мест из сервиса KudaGo.
    /// </summary>
    public class KudaGoPlacesOutput
    {
        /// <summary>
        /// Количество доступных мест.
        /// </summary>
        public int Count { get; set; }

        public int Page { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public IEnumerable<KudaGoPlacesResults> Results { get; set; }
    }

    /// <summary>
    /// Выходная модель для списка всех доступных мест из севиса KudaGo.
    /// </summary>
    public class KudaGoPlacesResults
    {
        /// <summary>
        /// Идентификационный номер.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Слаг.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Адресс.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Месторасположение.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// URL сайта места.
        /// </summary>
        public string Site_url { get; set; }

        /// <summary>
        /// Закрыто ли место.
        /// </summary>
        public bool Is_closed { get; set; }

        /// <summary>
        /// Телефон.
        /// </summary>
        public string Phone { get; set; }
    }
}
