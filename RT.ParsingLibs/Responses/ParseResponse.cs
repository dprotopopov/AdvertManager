using System.Collections.Generic;
using System.Text;
using RT.ParsingLibs.Models;

namespace RT.ParsingLibs.Responses
{
    /// <summary>
    /// Ответ от парсера
    /// </summary>
    public class ParseResponse
    {
        /// <summary>
        /// Код ответа
        /// </summary>
        public ParseResponseCode ResponseCode { get; set; }

        /// <summary>
        /// Имя модуля
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// ИД последнего объявления в данной рубрике-регионе-дествии
        /// </summary>
        public string LastPublicationId { get; set; }

        /// <summary>
        /// Объявления
        /// </summary>
        public IList<WebPublication> Publications { get; set; }

        /// <summary>
        /// Преобразовать в строку
        /// </summary>
        /// <returns>Объект в виде строки</returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("ResponseCode = {0}", ResponseCode);
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("ModuleName = {0}", ModuleName);
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("LastPublicationId = {0}", LastPublicationId);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Publications:");

            if (Publications != null)
                foreach (var pub in Publications)
                {
                    stringBuilder.AppendFormat("Pub: \r\n{0}", pub);
                    stringBuilder.AppendLine();
                }

            return stringBuilder.ToString();
        }
    }
}