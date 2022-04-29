using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.Helper;
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

        //要改
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


        //有問題要改
        public void Updateuestionnaire(Questionnaire addQnir, List<Question> questionList)
        {
            try
            {
                using (var context = new ORM.ContextModel())
                {
                    var targetQnir = context.Questionnaires.Where(obj => obj.QuestionnaireID == addQnir.QuestionnaireID).FirstOrDefault();

                    targetQnir.Caption = addQnir.Caption;
                    targetQnir.QuestionnaireContent = addQnir.QuestionnaireContent;
                    targetQnir.StartDate = addQnir.StartDate;
                    targetQnir.EndDate = addQnir.EndDate;
                    targetQnir.VoidStatus = addQnir.VoidStatus;

                    var targetQst = context.Questions.Where(obj => obj.QuestionnaireID == addQnir.QuestionnaireID).ToList();
                    int i = 0;
                    for (i = 0; i < targetQst.Count; i++)
                    {
                        var targetDate = targetQst.Where(obj => obj.QuestOrder == i).FirstOrDefault();

                        targetDate.QuestContent = questionList[i].QuestContent;
                        targetDate.AnswerForm = questionList[i].AnswerForm;
                        targetDate.SelectItem = questionList[i].SelectItem;
                        targetDate.Required = questionList[i].Required;
                    }
                    //如果新的資料列比舊的多，
                    if(questionList.Count> targetQst.Count)
                    {
                        for(i=0;i<questionList.Count;i++)
                        { 
                        context.Questions.Add(questionList[i]);
                        }
                    }
                    //如果題目被刪掉了，就刪掉題目順序
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.Updateuestionnaire", ex);
                throw;
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