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
    ///     Регион
    /// </summary>
    [Table("region")]
    public class Region : IEntity
    {
        /// <summary>
        ///     Дочерние узлы (вспомогательное поле)
        /// </summary>
        private ObservableCollection<Region> _children;

        /// <summary>
        ///     Возможные родительские узлы (вспомогательное поле)
        /// </summary>
        private ObservableCollection<Region> _regionDbSet;
        private ObservableCollection<Region> _regionRootSet;

        /// <summary>
        ///     ИД родительского региона
        /// </summary>
        [Description("ИД родительского региона")]
        [DisplayName("ИД родительского региона")]
        [Column("parent_id", TypeName = "integer")]
        public int? ParentId { get; set; }

        /// <summary>
        ///     Заголовок региона
        /// </summary>
        [Required]
        [Description("Заголовок")]
        [DisplayName("Заголовок")]
        [StringLength(500)]
        [Column("title", TypeName = "text")]
        public string Title { get; set; }

        /// <summary>
        ///     Дата модификации
        /// </summary>
        [Default(typeof (DateTime), "CURRENT_TIMESTAMP")]
        [Description("Дата изменения")]
        [DisplayName("Дата изменения")]
        [Column("modify_date", TypeName = "timestamp")]
        [DisplayFormat(DataFormatString = "{0:s}", ApplyFormatInEditMode = true)]
        public DateTime ModifyDate { get; set; }

        /// <summary>
        ///     Уровень региона
        /// </summary>
        [Description("Уровень региона")]
        [DisplayName("Уровень региона")]
        [Column("level", TypeName = "integer")]
        public short Level { get; set; }

        /// <summary>
        ///     Наличие дочерних узлов
        /// </summary>
        [Description("Наличие дочерних узлов")]
        [DisplayName("Наличие дочерних узлов")]
        [Column("has_child", TypeName = "bool")]
        public bool HasChild { get; set; }

        /// <summary>
        ///     Путь ИД
        /// </summary>
        [Required]
        [Description("Путь ИД")]
        [DisplayName("Путь ИД")]
        [Column("id_path", TypeName = "text")]
        public string IdPath { get; set; }

        /// <summary>
        ///     Дочерние узлы (вспомогательное поле)
        /// </summary>
        [Ignore]
        public ObservableCollection<Region> Children
        {
            get { return _children ?? (_children = new ObservableCollection<Region>()); }
        }

        /// <summary>
        ///     Возможные родительские узлы (вспомогательное поле)
        /// </summary>
        [Ignore]
        public ObservableCollection<Region> RegionDbSet
        {
            get { return _regionDbSet ?? (_regionDbSet = new ObservableCollection<Region>()); }
            set { _regionDbSet = value; }
        }
        [Ignore]
        public ObservableCollection<Region> RegionRootSet
        {
            get { return _regionRootSet ?? (_regionRootSet = new ObservableCollection<Region>()); }
            set { _regionRootSet = value; }
        }

        /// <summary>
        ///     ИД региона
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Description("Идентификатор")]
        [DisplayName("Идентификатор")]
        [Column("id", TypeName = "integer")]
        public int Id { get; set; }
    }
}