namespace QuestionnaireSystem.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommonQuest")]
    public partial class CommonQuest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommonQuestID { get; set; }

        [StringLength(500)]
        public string QuestContent { get; set; }

        public int? AnswerForm { get; set; }

        [StringLength(500)]
        public string SelectItem { get; set; }

        public bool? Required { get; set; }
    }
}
