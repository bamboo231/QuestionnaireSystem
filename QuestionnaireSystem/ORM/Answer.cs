namespace QuestionnaireSystem.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BasicAnswerID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string QuestID { get; set; }

        [Column("Answer")]
        [StringLength(500)]
        public string Answer1 { get; set; }
    }
}
