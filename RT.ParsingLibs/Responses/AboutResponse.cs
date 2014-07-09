using System;
using System.Reflection;

namespace RT.ParsingLibs.Responses
{
    /// <summary>
    /// Информация от разработчика
    /// </summary>
    public class AboutResponse
    {
        /// <summary>
        /// Информация от разработчика
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Координаты разработчика
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// Информация о передаче исключительных прав
        /// </summary>
        public string CopyRight { get; set; }

        /// <summary>
        /// Преобразовать в строку
        /// </summary>
        /// <returns>Объект в виде строки</returns>
        public override string ToString()
        {
            return string.Format("Info = {0}{3}Contacts = {1}{3}CopyRight = {2}", Info, Contacts, CopyRight, Environment.NewLine);
        }
    }
}