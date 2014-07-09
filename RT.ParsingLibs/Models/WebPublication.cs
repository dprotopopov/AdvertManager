using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RT.ParsingLibs.Models
{
    /// <summary>
    /// Объявление
    /// </summary>
    public class WebPublication
    {
        /// <summary>
        /// ИД объявления в данной рубрике-регионе-дествии
        /// </summary>
        public string PublicationId { get; set; }

        /// <summary>
        /// Дата создания/изменения объявления
        /// </summary>
        public DateTime ModifyDate { get; set; }

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
        /// Ссылка на объявление
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Ссылка на сайт
        /// </summary> 
        public Uri Site { get; set; }

        /// <summary>
        /// Текст объявления
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Контакт объявления
        /// </summary>
        public WebPublicationContact Contact { get; set; }

        /// <summary>
        /// Url на изображения
        /// </summary>
        public IList<Uri> Photos { get; set; }

        /// <summary>
        /// Дополнительная информация специфичная для каждой рубрики
        /// </summary>
        public AdditionalInfo AdditionalInfo { get; set; }

        /// <summary>
        /// Преобразовать в строку
        /// </summary>
        /// <returns>Объект в виде строки</returns>
        public override string ToString()
        {
            var properties = GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(this, null));
            var stringBuilder = new StringBuilder();
            foreach (var property in properties)
            {
                stringBuilder.AppendLine();
                var val = property.Value;
                if (property.Value is IEnumerable<Uri>)
                    val = string.Join(",", property.Value as IEnumerable<Uri>);
                stringBuilder.AppendFormat("{0} = {1}", property.Key, val);
            }
            return stringBuilder.ToString();
        }
    }
}