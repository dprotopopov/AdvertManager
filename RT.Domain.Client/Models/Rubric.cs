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
    [Table("rubric")]
    public class Rubric : IEntity
    {
        /// <summary>
        ///     Дочерние узлы (вспомогательное поле)
        /// </summary>
        private ObservableCollection<Rubric> _children;

        /// <summary>
        ///     Возможные родительские узлы (вспомогательное поле)
        /// </summary>
        private ObservableCollection<RubricCost> _costs;

        /// <summary>
        ///     Возможные родительские узлы (вспомогательное поле)
        /// </summary>
        private ObservableCollection<Rubric> _rubricDbSet;
        private ObservableCollection<Rubric> _rubricRootSet;

        /// <summary>
        ///     ИД родительской рубрики
        /// </summary>
        [Description("ИД родительской рубрики")]
        [DisplayName("ИД родительской рубрики")]
        [Column("parent_id", TypeName = "integer")]
        public int? ParentId { get; set; }

        /// <summary>
        ///     Заголовок рубрики
        /// </summary>
        [Required]
        [StringLength(500)]
        [Description("Заголовок")]
        [DisplayName("Заголовок")]
        [Column("title", TypeName = "text")]
        public string Title { get; set; }

        /// <summary>
        ///     Дата создания/изменения рубрики
        /// </summary>
        [Default(typeof (DateTime), "CURRENT_TIMESTAMP")]
        [Description("Дата изменения")]
        [DisplayName("Дата изменения")]
        [Column("modify_date", TypeName = "timestamp")]
        [DisplayFormat(DataFormatString = "{0:s}", ApplyFormatInEditMode = true)]
        public DateTime ModifyDate { get; set; }

        /// <summary>
        ///     Уровень рубрики
        /// </summary>
        [Description("Уровень рубрики")]
        [DisplayName("Уровень рубрики")]
        [Column("level", TypeName = "integer")]
        public short Level { get; set; }

        /// <summary>
        ///     Наличие дочерних узлов рубрики
        /// </summary>
        [Description("Наличие дочерних узлов")]
        [DisplayName("Наличие дочерних узлов")]
        [Column("has_child", TypeName = "bool")]
        public bool HasChild { get; set; }

        /// <summary>
        ///     Путь ИД рубрик
        /// </summary>
        [Required]
        [Description("Путь ИД рубрик")]
        [DisplayName("Путь ИД рубрик")]
        [Column("id_path", TypeName = "text")]
        public string IdPath { get; set; }

        /// <summary>
        ///     Дочерние узлы (вспомогательное поле)
        /// </summary>
        [Ignore]
        public ObservableCollection<Rubric> Children
        {
            get { return _children ?? (_children = new ObservableCollection<Rubric>()); }
        }

        /// <summary>
        ///     Возможные родительские узлы (вспомогательное поле)
        /// </summary>
        [Ignore]
        public ObservableCollection<Rubric> RubricDbSet
        {
            get { return _rubricDbSet ?? (_rubricDbSet = new ObservableCollection<Rubric>()); }
            set { _rubricDbSet = value; }
        }
        [Ignore]
        public ObservableCollection<Rubric> RubricRootSet
        {
            get { return _rubricRootSet ?? (_rubricRootSet = new ObservableCollection<Rubric>()); }
            set { _rubricRootSet = value; }
        }

        /// <summary>
        ///     Возможные родительские узлы (вспомогательное поле)
        /// </summary>
        [Ignore]
        public ObservableCollection<RubricCost> Costs
        {
            get { return _costs ?? (_costs = new ObservableCollection<RubricCost>()); }
            set { _costs = value; }
        }

        /// <summary>
        ///     Ид рубрики
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Description("Идентификатор")]
        [DisplayName("Идентификатор")]
        [Column("id", TypeName = "integer")]
        public int Id { get; set; }
    }
}