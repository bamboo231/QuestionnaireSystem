using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.ORM;

namespace QuestionnaireSystem.Manager
{
    public class QuestionnaireManager
    {
        public List<Questionnaire> GetQuestionnaireList()
        { 
            using (ContextModel contextModel = new ContextModel())
            {
                return contextModel.Questionnaires.ToList();
            }
        }
    }
}