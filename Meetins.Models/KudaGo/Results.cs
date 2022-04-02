namespace Meetins.Models.KudaGo
{
    /// <summary>
    /// Выходная модель для всех доступных мест из севиса KudaGo.
    /// </summary>
    public class Results
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

        /// <summary>
        /// Метро рядом.
        /// </summary>
        public string Subway { get; set; }

        /// <summary>
        /// Парковочные места.
        /// </summary>
        public bool Has_parking_lot { get; set; }
    }
}
