namespace QuestionnaireSystem.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BasicAnswer")]
    public partial class BasicAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BasicAnswerID { get; set; }

        public int QuestionnaireID { get; set; }

        public DateTime AnswerDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Nickname { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public int Age { get; set; }

        public virtual BasicAnswer BasicAnswer1 { get; set; }

        public virtual BasicAnswer BasicAnswer2 { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }
    }
}
