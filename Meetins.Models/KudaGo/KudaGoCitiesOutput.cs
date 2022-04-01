namespace Meetins.Models.KudaGo
{
    /// <summary>
    /// Выходная модель для списка городов из сервиса KudaGo.
    /// </summary>
    public class KudaGoCitiesOutput
    {
        /// <summary>
        /// Название города.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Слаг города.
        /// </summary>
        public string Slug { get; set; }
    }
}
