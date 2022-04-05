namespace Meetins.Models.KudaGo
{
    /// <summary>
    /// Выходная модель для списка категорий событий из сервиса KudaGo.
    /// </summary>
    public class KudaGoCategoriesOutput
    {
        /// <summary>
        /// Идентификатор категории событий.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Слаг категории событий.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Название категории событий.
        /// </summary>
        public string Name { get; set; }
    }
}
