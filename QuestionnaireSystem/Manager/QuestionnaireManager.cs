﻿using System;
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
                var srchList = contextModel.Questionnaires.ToList();
                srchList = srchList.OrderByDescending(obj => obj.BuildDate).ToList();
                return srchList;
            }
        }
        /// <summary>
        /// 取得所有問卷List
        /// </summary>
        /// <returns>回傳值為List</returns>
        public List<Questionnaire> GetEnableQstnir()
        {
            using (ContextModel contextModel = new ContextModel())
            {
                var srchList = contextModel.Questionnaires.Where(obj => obj.VoidStatus == true).ToList();
                srchList = srchList.OrderByDescending(obj => obj.BuildDate).ToList();
                return srchList;
            }
        }

        //要改
        /// <summary>
        /// 刪除某筆問卷
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

        /// <summary>
        /// 取得問卷題目
        /// </summary>
        /// <param name="strCurrentQnirID">傳入值為string</param>
        /// <returns></returns>
        public List<WholeAnswer> GetWholeQuestioniar(string strCurrentQnirID)
        {
            int currentQnirID = int.Parse( strCurrentQnirID);
            using (ContextModel contextModel = new ContextModel())
            {
                List<Questionnaire> QuestionnaireList = contextModel.Questionnaires.ToList();
                List<Question> questList = contextModel.Questions.ToList();

                var newList =
                    from qstnir in QuestionnaireList
                    join qst in questList on qstnir.QuestionnaireID equals qst.QuestionnaireID
                    where qstnir.QuestionnaireID == currentQnirID
                    select new WholeAnswer
                    {
                        QuestionnaireID = qstnir.QuestionnaireID,
                        QuestID = qst.QuestID,
                        Caption = qstnir.Caption,
                        QuestionnaireContent = qstnir.QuestionnaireContent,
                        QuestOrder = qst.QuestOrder,
                        QuestContent =qst.QuestContent,
                        AnswerForm = qst.AnswerForm,
                        SelectItem = qst.SelectItem,
                        Required = qst.Required,
                    };
                List<WholeAnswer> wholeQuestionnaire = new List<WholeAnswer>(newList).ToList();
                return wholeQuestionnaire;
            }
        }

        /// <summary>
        /// 取得問卷題目
        /// </summary>
        /// <param name="currentQnirID">傳入值為int</param>
        /// <returns></returns>
        public List<WholeAnswer> GetWholeQuestioniar(int currentQnirID)
        {
            using (ContextModel contextModel = new ContextModel())
            {
                List<Questionnaire> QuestionnaireList = contextModel.Questionnaires.ToList();
                List<Question> questList = contextModel.Questions.ToList();

                var newList =
                    from qstnir in QuestionnaireList
                    join qst in questList on qstnir.QuestionnaireID equals qst.QuestionnaireID
                    where qstnir.QuestionnaireID == currentQnirID
                    select new WholeAnswer
                    {
                        QuestionnaireID = qstnir.QuestionnaireID,
                        QuestOrder = qst.QuestOrder,
                        QuestContent = qst.QuestContent,
                        AnswerForm = qst.AnswerForm,
                        SelectItem = qst.SelectItem,
                        Required = qst.Required,
                    };
                List<WholeAnswer> wholeQuestionnaire = new List<WholeAnswer>(newList).ToList();
                return wholeQuestionnaire;
            }
        }
    }
}