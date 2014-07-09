using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RT.Core;
using ServiceStack.DataAnnotations;

namespace RT.Domain.Models
{
    /// <summary>
    ///     Рубрика
    /// </summary>
    [Table("rubric_cost")]
    public class RubricCost : IEntity
    {
        /// <summary>
        ///     Возможные родительские узлы (вспомогательное поле)
        /// </summary>
        private ObservableCollection<Rubric> _rubricDbSet;

        /// <summary>
        ///     ИД рубрики
        /// </summary>
        [Description("ИД рубрики")]
        [DisplayName("ИД рубрики")]
        [Column("rubric_id", TypeName = "integer")]
        [ServiceStack.OrmLite.ForeignKey(typeof (Rubric), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int RubricId { get; set; }

        /// <summary>
        ///     Стоимость рубрики
        /// </summary>
        [Required]
        [Description("Стоимость рубрики")]
        [DisplayName("Стоимость рубрики")]
        [Column("cost", TypeName = "money")]
        public decimal Cost { get; set; }

        /// <summary>
        ///     Комментарий
        /// </summary>
        [Description("Комментарий")]
        [DisplayName("Комментарий")]
        [Column("comment", TypeName = "text")]
        public string Comment { get; set; }

        /// <summary>
        ///     Автор
        /// </summary>
        [Required]
        [Description("Автор")]
        [DisplayName("Автор")]
        [Column("author", TypeName = "varchar(255)")]
        public string Author { get; set; }

        /// <summary>
        ///     Дата создания/модификации
        /// </summary>
        [Default(typeof (DateTime), "CURRENT_TIMESTAMP")]
        [Description("Дата изменения")]
        [DisplayName("Дата изменения")]
        [Column("modify_date", TypeName = "timestamp")]
        [DisplayFormat(DataFormatString = "{0:s}", ApplyFormatInEditMode = true)]
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        ///     Возможные родительские узлы (вспомогательное поле)
        /// </summary>
        [Ignore]
        public ObservableCollection<Rubric> RubricDbSet
        {
            get { return _rubricDbSet ?? (_rubricDbSet = new ObservableCollection<Rubric>()); }
            set { _rubricDbSet = value; }
        }

        /// <summary>
        ///     Ид
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Description("Идентификатор")]
        [DisplayName("Идентификатор")]
        [Column("id", TypeName = "integer")]
        public int Id { get; set; }
    }
}