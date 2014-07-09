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
    ///     Действие
    /// </summary>
    [Table("action")]
    public class Action : IEntity
    {
        /// <summary>
        ///     Возможные родительские узлы (вспомогательное поле)
        /// </summary>
        private ObservableCollection<Action> _actionDbSet;

        private ObservableCollection<Action> _actionRootSet;

        /// <summary>
        ///     Дочерние узлы (вспомогательное поле)
        /// </summary>
        private ObservableCollection<Action> _children;

        /// <summary>
        ///     Противоположное действие
        /// </summary>
        [Description("Противоположное действие")]
        [DisplayName("Противоположное действие")]
        [Column("anti_action_id", TypeName = "integer")]
        public int? AntiActionId { get; set; }

        /// <summary>
        ///     Заголовок
        /// </summary>
        [Required]
        [Description("Заголовок")]
        [DisplayName("Заголовок")]
        [StringLength(500)]
        [Column("title", TypeName = "text")]
        public string Title { get; set; }

        /// <summary>
        ///     Дата изменения
        /// </summary>
        [Default(typeof (DateTime), "CURRENT_TIMESTAMP")]
        [Description("Дата изменения")]
        [DisplayName("Дата изменения")]
        [Column("modify_date", TypeName = "timestamp")]
        [DisplayFormat(DataFormatString = "{0:s}", ApplyFormatInEditMode = true)]
        public DateTime ModifyDate { get; set; }

        /// <summary>
        ///     Дочерние узлы (вспомогательное поле)
        /// </summary>
        [Ignore]
        public ObservableCollection<Action> Children
        {
            get { return _children ?? (_children = new ObservableCollection<Action>()); }
        }

        /// <summary>
        ///     Возможные родительские узлы (вспомогательное поле)
        /// </summary>
        [Ignore]
        public ObservableCollection<Action> ActionDbSet
        {
            get { return _actionDbSet ?? (_actionDbSet = new ObservableCollection<Action>()); }
            set { _actionDbSet = value; }
        }

        [Ignore]
        public ObservableCollection<Action> ActionRootSet
        {
            get { return _actionRootSet ?? (_actionRootSet = new ObservableCollection<Action>()); }
            set { _actionRootSet = value; }
        }

        [Ignore]
        public bool HasChild { get; set; }

        [Ignore]
        public int? ParentId { get; set; }

        [Ignore]
        public short Level { get; set; }

        /// <summary>
        ///     ИД действия
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Description("Идентификатор")]
        [DisplayName("Идентификатор")]
        [Column("id", TypeName = "integer")]
        public int Id { get; set; }
    }
}