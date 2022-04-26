namespace QuestionnaireSystem.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        [Key]
        public int QuestID { get; set; }

        public int QuestionnaireID { get; set; }

        public int QuestOrder { get; set; }

        [StringLength(500)]
        public string QuestContent { get; set; }

        public int AnswerForm { get; set; }

        [StringLength(500)]
        public string SelectItem { get; set; }

        public bool Required { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }
    }
}
