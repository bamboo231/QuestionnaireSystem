using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.ORM;

namespace QuestionnaireSystem.Manager
{
    public class QuestionnaireManager
    {
        /// <summary>
        /// 取得所有問卷List
        /// </summary>
        /// <returns>回傳值為List</returns>
        public List<Questionnaire> GetQuestionnaireList()
        {
            using (ContextModel contextModel = new ContextModel())
            {
                return contextModel.Questionnaires.ToList();
            }
        }


        /// <summary>
        /// 取得所有問卷List
        /// </summary>
        public void DeleteQuestionnaire(int QnirID)
        {
            using (var context = new ORM.ContextModel())
            {
                //取得目標的問卷(基本資料Table)
                var checkedQnir = context.Questionnaires.Where(obj => obj.QuestionnaireID == QnirID).First();
                //取得目標的問卷(題目Table)
                var fkQuest = context.Questions.Where(obj => obj.QuestionnaireID == QnirID).AsEnumerable();
                context.Questionnaires.Remove(checkedQnir);
                context.Questions.RemoveRange(fkQuest);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 新增問卷
        /// </summary>
        /// <param name="addQnir">新增的問卷基本資料</param>
        /// <param name="questionList">新增的問卷題目</param>
        public void AddQuestionnaire(Questionnaire addQnir, List<Question> questionList)
        {
            using (var context = new ORM.ContextModel())
            {
                context.Questionnaires.Add(addQnir);

                foreach (var question in questionList)
                {
                    context.Questions.Add(question);
                }
                context.SaveChanges();
            }
        }

        public Questionnaire GetQuestionnaire(string questionnaireID)
        {
            int intQnirID = Int32.Parse(questionnaireID);

            using (ContextModel contextModel = new ContextModel())
            {
                return contextModel.Questionnaires.Where(obj => obj.QuestionnaireID== intQnirID).First();
            }
        }
    }
}