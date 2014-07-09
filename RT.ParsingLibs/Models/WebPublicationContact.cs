using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RT.ParsingLibs.Models
{
    /// <summary>
    /// Контактная информация объявления
    /// </summary>
    public class WebPublicationContact
    {
        /// <summary>
        /// Автор (Юр. лицо, агентство, собственник)
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Url-автора
        /// </summary>
        public Uri AuthorUrl { get; set; }

        /// <summary>
        /// Контактное лицо 
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Телефоны
        /// </summary>
        public IList<string> Phone { get; set; }

        /// <summary>
        /// Email-адреса
        /// </summary>
        public IList<string> Email { get; set; }

        /// <summary>
        /// Логин скайпа
        /// </summary>
        public string Skype { get; set; }

        /// <summary>
        /// ICQ-номер
        /// </summary>
        public uint Icq { get; set; }


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
                if (property.Value is IEnumerable<string>)
                    val = string.Join(",", property.Value as IEnumerable<string>);
             
                stringBuilder.AppendFormat("{0} = {1}", property.Key, val);
            }
            return stringBuilder.ToString();
        }
    }
}