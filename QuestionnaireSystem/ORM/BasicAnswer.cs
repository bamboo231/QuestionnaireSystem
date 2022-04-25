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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BasicAnswer()
        {
            Answers = new HashSet<Answer>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }
    }
}
