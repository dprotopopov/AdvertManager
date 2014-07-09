using System.Linq;
using System.Reflection;
using System.Text;

namespace RT.ParsingLibs.Models
{
    /// <summary>
    /// Дополнительная информация для рубрики "Недвижимость"
    /// </summary>
    public class RealtyAdditionalInfo
    {
        /// <summary>
        /// Количество этажей
        /// </summary>
        public int FloorNumber { get; set; }

        /// <summary>
        /// Этаж
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// Количество комнат
        /// </summary>
        public int RoomNumber { get; set; }

        /// <summary>
        /// Тип недвижимости 
        /// </summary>
        public string RealEstateType { get; set; }

        /// <summary>
        /// Район
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Материал стен
        /// </summary>
        public string WallМaterial { get; set; }

        /// <summary>
        /// Отделка
        /// </summary>
        public string Furnish { get; set; }

        /// <summary>
        /// Общая площадь
        /// </summary>
        public int TotalSpace { get; set; }

        /// <summary>
        /// Жилая площадь
        /// </summary>
        public int LivingSpace { get; set; }

        /// <summary>
        /// Площадь кухни
        /// </summary>
        public int KitchenSpace { get; set; }

        /// <summary>
        /// Цена за весь объект
        /// </summary>
        public decimal CostAll { get; set; }

        /// <summary>
        /// Цена за метр
        /// </summary>
        public decimal CostPerMeter { get; set; }

        /// <summary>
        /// Балкон/лоджия
        /// </summary>
        public bool IsLoggia { get; set; }

        /// <summary>
        /// Санузел
        /// </summary>
        public string Wc { get; set; }

        /// <summary>
        /// Вид из окон
        /// </summary>
        public string ViewFromProperty { get; set; }

        /// <summary>
        /// Паркинг во дворе или доме
        /// </summary>
        public bool IsParking { get; set; }

        /// <summary>
        /// Срок аренды
        /// </summary>
        public int Tenancy { get; set; }

        /// <summary>
        /// Сдаваемая площадь
        /// </summary>
        public int LeasableSpace { get; set; }

        /// <summary>
        /// Назначение помещения
        /// </summary>
        public string AppointmentOfRoom { get; set; }

        /// <summary>
        /// Площадь участка, соток
        /// </summary>
        public int LandSpace { get; set; }

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
                stringBuilder.AppendFormat("{0} = {1}",property.Key, property.Value);
            }
            return stringBuilder.ToString();
        }
    }
}