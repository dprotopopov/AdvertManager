using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RT.ParsingLibs.Models
{
    /// <summary>
    ///     Дополнительная информация для рубрики "Транспортные средства"
    /// </summary>
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class AutomotoAdditionalInfo
    {
        /// <summary>
        ///     VIN
        ///     Идентификатор транспортного средства
        /// </summary>
        public string Vin { get; set; }

        /// <summary>
        ///     Состояние транспортного средства
        ///     Новый
        ///     С пробегом
        ///     Битый
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        ///     Cтрана производства
        /// </summary>
        public string MadeIn { get; set; }

        /// <summary>
        ///     Cтрана импорта
        /// </summary>
        public string ImportFrom { get; set; }

        /// <summary>
        ///     Тип транспортного средства
        ///     легковой автомобиль
        ///     коммерческий транспорт
        ///     прицеп
        ///     внедорожник
        ///     мотоцикл
        ///     мопед
        /// </summary>
        public string AutomotoType { get; set; }

        /// <summary>
        ///     Год выпуска
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        ///     Пробег по одометру
        /// </summary>
        public decimal OdometerValue { get; set; }

        /// <summary>
        ///     Тип коробки передач
        /// </summary>
        public string GearboxType { get; set; }

        /// <summary>
        ///     Тип привода
        /// </summary>
        public string TransmissionType { get; set; }

        /// <summary>
        ///     Тип подвески
        /// </summary>
        public string SuspensionType { get; set; }

        /// <summary>
        ///     Грузоподъёмность
        /// </summary>
        public decimal Capacity { get; set; }

        /// <summary>
        ///     Масса в снаряжённом состоянии
        /// </summary>
        public decimal CurbWeight { get; set; }

        /// <summary>
        ///     Количество передач
        /// </summary>
        public int GearsNumber { get; set; }

        /// <summary>
        ///     Количество осей
        /// </summary>
        public int AxlesNumber { get; set; }

        /// <summary>
        ///     Количество сидений
        /// </summary>
        public int SeatsNumber { get; set; }

        /// <summary>
        ///     Количество дверей
        /// </summary>
        public int DoorsNumber { get; set; }

        /// <summary>
        ///     Индекс безопасности
        /// </summary>
        public int SafetyIndex { get; set; }

        /// <summary>
        ///     Таможня
        /// </summary>
        public string Customs { get; set; }

        /// <summary>
        ///     Расположение руля
        /// </summary>
        public string SteeringWheel { get; set; }

        /// <summary>
        ///     Тип топлива
        /// </summary>
        public string FuelType { get; set; }

        /// <summary>
        ///     Клиренс
        /// </summary>
        public decimal Clearance { get; set; }

        /// <summary>
        ///     Расход топлива на 100 км
        /// </summary>
        public decimal FuelConsumption { get; set; }

        /// <summary>
        ///     Пробег на одном баке топлива
        /// </summary>
        public decimal MileageOnATank { get; set; }

        /// <summary>
        ///     Время разгона до 100 км/ч
        /// </summary>
        public decimal AccelerationUpTo100 { get; set; }

        /// <summary>
        ///     Максимальная скорость
        /// </summary>
        public decimal MaximumSpeed { get; set; }

        /// <summary>
        ///     Количество предыдущих владельцев
        /// </summary>
        public int PreviousOwners { get; set; }

        /// <summary>
        ///     Цена за транспортное средство
        /// </summary>
        public decimal CostAll { get; set; }

        /// <summary>
        ///     Требуется ремонт
        /// </summary>
        public bool RequiresRepair { get; set; }

        /// <summary>
        ///     Наличие тюнинга
        /// </summary>
        public bool Tuning { get; set; }

        #region Гарантии и обязательства

        /// <summary>
        ///     В залоге
        /// </summary>
        [Category("Гарантии и обязательства")]
        public bool InEscrow { get; set; }

        /// <summary>
        ///     На гарантии
        /// </summary>
        [Category("Гарантии и обязательства")]
        public bool UnderWarranty { get; set; }

        #endregion

        #region Колёса и шины

        /// <summary>
        ///     Размер дисков
        /// </summary>
        [Category("Колёса и шины")]
        public string DiskSize { get; set; }

        /// <summary>
        ///     Брэнд дисков
        /// </summary>
        [Category("Колёса и шины")]
        public string DiskBrand { get; set; }

        /// <summary>
        ///     Тип дисков
        /// </summary>
        [Category("Колёса и шины")]
        public string DiskType { get; set; }

        /// <summary>
        ///     Размер установленного комплекта шин
        /// </summary>
        [Category("Колёса и шины")]
        public string TiresSize { get; set; }

        /// <summary>
        ///     Брэнд установленного комплекта шин
        /// </summary>
        [Category("Колёса и шины")]
        public string TiresBrand { get; set; }

        /// <summary>
        ///     Дополнительный комплект дисков
        /// </summary>
        [Category("Колёса и шины")]
        public bool AdditionalDisks { get; set; }

        /// <summary>
        ///     Дополнительный комплект шин
        /// </summary>
        [Category("Колёса и шины")]
        public bool AdditionalTires { get; set; }

        #endregion

        #region Фары и фонари

        /// <summary>
        ///     Наличие ксеноновых фар
        /// </summary>
        [Category("Фары и фонари")]
        public bool Xenon { get; set; }

        /// <summary>
        ///     Наличие светодиодных фар
        /// </summary>
        [Category("Фары и фонари")]
        public bool Led { get; set; }

        /// <summary>
        ///     Противотуманные фары
        /// </summary>
        [Category("Фары и фонари")]
        public bool FogLights { get; set; }

        /// <summary>
        ///     Противотуманные фонари
        /// </summary>
        [Category("Фары и фонари")]
        public bool FogBackLights { get; set; }

        /// <summary>
        ///     Прожектор
        /// </summary>
        [Category("Фары и фонари")]
        public bool Spotlight { get; set; }

        #endregion

        #region Комплектация

        /// <summary>
        ///     Кондиционер
        /// </summary>
        [Category("Комплектация")]
        public bool Conditioner { get; set; }

        /// <summary>
        ///     Климат-контроль
        /// </summary>
        [Category("Комплектация")]
        public bool ClimateControl { get; set; }

        /// <summary>
        ///     Количество зон климат-контроля
        /// </summary>
        [Category("Комплектация")]
        public int ClimateNumber { get; set; }

        /// <summary>
        ///     Наличие ABS
        /// </summary>
        [Category("Комплектация")]
        public bool Abs { get; set; }

        /// <summary>
        ///     Наличие подушек безопасности
        /// </summary>
        [Category("Комплектация")]
        public bool Airbags { get; set; }

        /// <summary>
        ///     Количество подушек безопасности
        /// </summary>
        [Category("Комплектация")]
        public int AirbagsNumber { get; set; }

        /// <summary>
        ///     Тонированные стекла
        /// </summary>
        [Category("Комплектация")]
        public bool TintedWindows { get; set; }

        /// <summary>
        ///     Охранная система
        /// </summary>
        [Category("Комплектация")]
        public bool SecuritySystem { get; set; }

        /// <summary>
        ///     Центральный замок
        /// </summary>
        [Category("Комплектация")]
        public bool CentralLocking { get; set; }

        /// <summary>
        ///     Иммобилайзер
        /// </summary>
        [Category("Комплектация")]
        public bool Immobilizer { get; set; }

        /// <summary>
        ///     Обогрев зеркал
        /// </summary>
        [Category("Комплектация")]
        public bool HeatedMirrors { get; set; }

        /// <summary>
        ///     Бортовой компьютер
        /// </summary>
        [Category("Комплектация")]
        public bool OnboardComputer { get; set; }

        /// <summary>
        ///     Датчик дождя
        /// </summary>
        [Category("Комплектация")]
        public bool RainSensor { get; set; }

        /// <summary>
        ///     Датчик света
        /// </summary>
        [Category("Комплектация")]
        public bool LightSensor { get; set; }

        /// <summary>
        ///     Круиз-контроль
        /// </summary>
        [Category("Комплектация")]
        public bool CruiseControl { get; set; }

        /// <summary>
        ///     Спортивный режим
        /// </summary>
        [Category("Комплектация")]
        public bool SportMode { get; set; }

        /// <summary>
        ///     Усилитель руля
        /// </summary>
        [Category("Комплектация")]
        public bool PowerSteering { get; set; }

        /// <summary>
        ///     Обогрев сидений
        /// </summary>
        [Category("Комплектация")]
        public bool SeatHeating { get; set; }

        /// <summary>
        ///     Электропривод зеркал
        /// </summary>
        [Category("Комплектация")]
        public bool PowerMirrors { get; set; }

        /// <summary>
        ///     Стеклоподъемники
        /// </summary>
        [Category("Комплектация")]
        public bool PowerWindows { get; set; }

        /// <summary>
        ///     Регулировка руля
        /// </summary>
        [Category("Комплектация")]
        public bool AdjustableSteeringWheel { get; set; }

        /// <summary>
        ///     Автоматическая регулировка сидений
        /// </summary>
        [Category("Комплектация")]
        public bool AutomaticSeatAdjustment { get; set; }

        /// <summary>
        ///     Антипробуксовка
        /// </summary>
        [Category("Комплектация")]
        public bool Dtc { get; set; }

        /// <summary>
        ///     Курсовая стабилизация
        /// </summary>
        [Category("Комплектация")]
        public bool StabilityControl { get; set; }

        /// <summary>
        ///     Парктроник
        /// </summary>
        [Category("Комплектация")]
        public bool Parktronic { get; set; }

        /// <summary>
        ///     Навигационная система
        /// </summary>
        [Category("Комплектация")]
        public bool NavigationSystem { get; set; }

        /// <summary>
        ///     Дистанционный запуск двигателя
        /// </summary>
        [Category("Комплектация")]
        public bool RemoteControl { get; set; }

        /// <summary>
        ///     Спортивный руль
        /// </summary>
        [Category("Комплектация")]
        public bool RacingSteeringWheel { get; set; }

        /// <summary>
        ///     Лебёдка
        /// </summary>
        [Category("Комплектация")]
        public bool Winch { get; set; }

        /// <summary>
        ///     Кенгурятник
        /// </summary>
        [Category("Комплектация")]
        public bool Kangaroo { get; set; }

        /// <summary>
        ///     Люк
        /// </summary>
        [Category("Комплектация")]
        public bool Sunroof { get; set; }

        /// <summary>
        ///     Панорамная крыша
        /// </summary>
        [Category("Комплектация")]
        public bool PanoramicRoof { get; set; }

        /// <summary>
        ///     Откидной верх
        /// </summary>
        [Category("Комплектация")]
        public bool ConvertibleTop { get; set; }

        /// <summary>
        ///     Фаркоп
        /// </summary>
        [Category("Комплектация")]
        public bool Hitch { get; set; }

        /// <summary>
        ///     Подогрев масла в картере
        /// </summary>
        [Category("Комплектация")]
        public bool CrankcaseHeater { get; set; }

        #endregion

        #region Дом на-колёсах

        /// <summary>
        ///     Санузел
        /// </summary>
        [Category("Дом на-колёсах")]
        public bool Wc { get; set; }

        /// <summary>
        ///     Кухня
        /// </summary>
        [Category("Дом на-колёсах")]
        public bool Kitchen { get; set; }

        /// <summary>
        ///     Количество спальных мест
        /// </summary>
        [Category("Дом на-колёсах")]
        public int SleepsNumber { get; set; }

        #endregion

        #region Мультимедиа

        /// <summary>
        ///     Радиоприёмник
        /// </summary>
        [Category("Мультимедиа")]
        public bool RadioReceiver { get; set; }

        /// <summary>
        ///     Телеприёмник
        /// </summary>
        [Category("Мультимедиа")]
        public bool TvReceiver { get; set; }

        /// <summary>
        ///     Спутниковая тарелка
        /// </summary>
        [Category("Мультимедиа")]
        public bool SatelliteReceiver { get; set; }

        /// <summary>
        ///     Кассетный плеер
        /// </summary>
        [Category("Мультимедиа")]
        public bool CassettePlayer { get; set; }

        /// <summary>
        ///     CD плеер
        /// </summary>
        [Category("Мультимедиа")]
        public bool СdPlayer { get; set; }

        /// <summary>
        ///     MP3 плеер
        /// </summary>
        [Category("Мультимедиа")]
        public bool Mp3Player { get; set; }

        /// <summary>
        ///     DVD плеер
        /// </summary>
        [Category("Мультимедиа")]
        public bool DvdPlayer { get; set; }

        /// <summary>
        ///     Видео плеер
        /// </summary>
        [Category("Мультимедиа")]
        public bool VideoPlayer { get; set; }

        #endregion

        #region Марка

        /// <summary>
        ///     Брэнд производителя
        /// </summary>
        [Category("Марка")]
        public string Brand { get; set; }

        /// <summary>
        ///     Марка/модель транспортного средства
        /// </summary>
        [Category("Марка")]
        public string Model { get; set; }

        /// <summary>
        ///     Поколение модели транспортного средства
        /// </summary>
        [Category("Марка")]
        public string Generation { get; set; }

        /// <summary>
        ///     Модификация модели транспортного средства
        /// </summary>
        [Category("Марка")]
        public string Modification { get; set; }

        #endregion

        #region Салон и сиденья

        /// <summary>
        ///     Обивка салона
        /// </summary>
        [Category("Салон и сиденья")]
        public string InteriorMaterial { get; set; }

        /// <summary>
        ///     Цвет салона
        /// </summary>
        [Category("Салон и сиденья")]
        public string InteriorColor { get; set; }

        /// <summary>
        ///     Обивка салона
        /// </summary>
        [Category("Салон и сиденья")]
        public string SeatsMaterial { get; set; }

        /// <summary>
        ///     Цвет салона
        /// </summary>
        [Category("Салон и сиденья")]
        public string SeatsColor { get; set; }

        #endregion

        #region Кузов

        /// <summary>
        ///     Тип кузова
        /// </summary>
        [Category("Кузов")]
        public string BodyType { get; set; }

        /// <summary>
        ///     Размер кузова
        /// </summary>
        [Category("Кузов")]
        public string BodySize { get; set; }

        /// <summary>
        ///     Цвет кузова
        /// </summary>
        [Category("Кузов")]
        public string BodyColor { get; set; }

        #endregion

        #region Двигатель и баттарея

        /// <summary>
        ///     Тип двигателя
        /// </summary>
        [Category("Двигатель и баттарея")]
        public string EngineType { get; set; }

        /// <summary>
        ///     Модель двигателя
        /// </summary>
        [Category("Двигатель и баттарея")]
        public string EngineModel { get; set; }

        /// <summary>
        ///     Мощность двигателя
        /// </summary>
        [Category("Двигатель и баттарея")]
        public decimal EnginePower { get; set; }

        /// <summary>
        ///     Объём двигателя
        /// </summary>
        [Category("Двигатель и баттарея")]
        public decimal EngineVolume { get; set; }

        /// <summary>
        ///     Расположение цилиндров
        /// </summary>
        [Category("Двигатель и баттарея")]
        public string PositionOfCylinders { get; set; }

        /// <summary>
        ///     Напряжение питания
        /// </summary>
        [Category("Двигатель и баттарея")]
        public string BattaryVoltage { get; set; }

        /// <summary>
        ///     Полярность батареи
        /// </summary>
        [Category("Двигатель и баттарея")]
        public string BattaryPolarity { get; set; }

        #endregion

        #region Багажник

        /// <summary>
        ///     Объём багажника
        /// </summary>
        [Category("Багажник")]
        public decimal CargoVolume { get; set; }

        /// <summary>
        ///     Тип багажника
        /// </summary>
        [Category("Багажник")]
        public string CargoType { get; set; }

        /// <summary>
        ///     Грузовой лифт
        /// </summary>
        [Category("Багажник")]
        public bool CargoLift { get; set; }

        /// <summary>
        ///     Кран
        /// </summary>
        [Category("Багажник")]
        public bool Crane { get; set; }

        #endregion

        #region Для инвалидов

        /// <summary>
        ///     Места для инвалидов
        /// </summary>
        [Category("Для инвалидов")]
        public bool DesignatedDisabled { get; set; }

        /// <summary>
        ///     Лифт для инвалидов
        /// </summary>
        [Category("Для инвалидов")]
        public bool LiftForDisabled { get; set; }

        #endregion

        /// <summary>
        ///     Преобразовать в строку
        /// </summary>
        /// <returns>Объект в виде строки</returns>
        public override string ToString()
        {
            Dictionary<string, object> properties = GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(this, null));
            var stringBuilder = new StringBuilder();
            foreach (var property in properties)
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("{0} = {1}", property.Key, property.Value);
            }
            return stringBuilder.ToString();
        }
    }
}