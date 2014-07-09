namespace RT.ParsingLibs.Models
{
    /// <summary>
    /// Бинд (тройка ИД рубрика-регион-действие)
    /// </summary>
    public class Bind : System.Object
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Bind(){}

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="rubricId">ИД рубрики</param>
        /// <param name="actionId">ИД действия</param>
        /// <param name="regionId">ИД региона</param>
        public Bind(int rubricId, int actionId, int regionId)
        {
            RubricId = rubricId;
            RegionId = regionId;
            ActionId = actionId;
        }

        /// <summary>
        /// ИД рубрики
        /// </summary>
        public int RubricId { get; set; }

        /// <summary>
        /// ИД региона
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// ИД действия
        /// </summary>
        public int ActionId { get; set; }

        /// <summary>
        /// Преобразовать в строку
        /// </summary>
        /// <returns>Объект в виде строки</returns>
        public override string ToString()
        {
            return string.Format("RubricId = {0}; ActionId = {1}; RegionId = {2};", RubricId, ActionId, RegionId);
        }

        /// <summary>
        /// Метод сравнения объектов
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns>TRUE - в случае равенства объектов, иначе FALSE</returns>
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
                return false;

            // If parameter cannot be cast to Point return false.
            var p = obj as Bind;
            if (p == null)
                return false;

            // Return true if the fields match:
            return (RubricId == p.RubricId) && (RegionId == p.RegionId) && (ActionId == p.ActionId);
        }

        /// <summary>
        /// Метод сравнения объектов
        /// </summary>
        /// <param name="p">Сравниваемый объект</param>
        /// <returns>TRUE - в случае равенства объектов, иначе FALSE</returns>
        public bool Equals(Bind p)
        {
            // If parameter is null return false:
            if (p == null)
                return false;

            // Return true if the fields match:
            return (RubricId == p.RubricId) && (RegionId == p.RegionId) && (ActionId == p.ActionId);
        }

        /// <summary>
        /// Получить хэш-код
        /// </summary>
        /// <returns>Хэш-код</returns>
        public override int GetHashCode()
        {
            return RegionId ^ ActionId ^ RubricId;
        }
    }
}
