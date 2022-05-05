using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionnaireSystem.ORM
{
    public class WholeAnswer
    {
        public int QuestID { get; set; }
        public int AnswerForm { get; set; }
        public string strAnswerForm { get; set; }
        public string Answer { get; set; }
        public string SelectItem { get; set; }

        public int QuestionnaireID { get; set; }
        public int QuestOrder { get; set; }
        public string Caption { get; set; }
        public string QuestionnaireContent { get; set; }

        public string QuestContent { get; set; }
        public int BasicAnswerID { get; set; }
        public string Nickname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public bool Required { get; set; }
        public int CommonQuestID { get; set; }
        public DateTime BuildDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool VoidStatus { get; set; }
        public string OpenOrNot { get; set; }
    }
}