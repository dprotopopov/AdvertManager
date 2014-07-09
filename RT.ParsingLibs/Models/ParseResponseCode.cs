namespace RT.ParsingLibs.Models
{
    /// <summary>
    /// Код результата работы
    /// </summary>
    public enum ParseResponseCode
    {
        /// <summary>
        /// Успешная обработка
        /// </summary>
        Success = 0,

        /// <summary>
        /// Бан сайта
        /// </summary>
        BanResource = 1,

        /// <summary>
        /// Изменение структуры контента
        /// </summary>
        ContentChage = 2,

        /// <summary>
        /// Сайт вернул пустой контент
        /// </summary>
        ContentEmpty = 3,

        /// <summary>
        /// Сайт не отвечает
        /// </summary>
        NotAvailableResource = 4,

        /// <summary>
        /// В выборке не найден ID или DataTime или Хеш-MD5 последнего объявления
        /// </summary>
        NotFoundId = 5
    }
}