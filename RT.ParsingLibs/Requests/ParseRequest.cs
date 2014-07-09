namespace RT.ParsingLibs.Requests
{
    /// <summary>
    /// Запрос на парсинг
    /// </summary>
    public class ParseRequest
    {
        /// <summary>
        /// ИД рубрики
        /// </summary>
        public long RubricId { get; set; }

        /// <summary>
        /// ИД региона
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// ИД действия
        /// </summary>
        public long ActionId { get; set; }

        /// <summary>
        /// ИД последнего объявления в данной рубрике-регионе-дествии
        /// </summary>
        public string LastPublicationId { get; set; }
    }
}