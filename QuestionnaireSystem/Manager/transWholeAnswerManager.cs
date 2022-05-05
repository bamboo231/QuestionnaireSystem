using QuestionnaireSystem.Helper;
using QuestionnaireSystem.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionnaireSystem.Manager
{
    public class transWholeAnswerManager
    {
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //問卷基本資料管理

        public List<WholeAnswer> QstToWholeList(List<Question> questions)
        {
            try
            {
                List<WholeAnswer> QstListToDisplay = new List<WholeAnswer>();
                for (int i = 0; i < questions.Count; i++)
                {
                    if (questions[i].QuestionnaireID == 0)
                        QstListToDisplay[i].QuestionnaireID = _QtnirMgr.GetNextQuestionnaireID();//賦予新的ID
                    else
                        QstListToDisplay[i].QuestionnaireID = questions[i].QuestionnaireID;//存進舊的ID

                    QstListToDisplay[i].QuestContent = questions[i].QuestContent;//存進舊的ID
                    QstListToDisplay[i].Required = questions[i].Required;//存進舊的ID
                    QstListToDisplay[i].AnswerForm = questions[i].AnswerForm;//存進舊的ID
                    QstListToDisplay[i].SelectItem = questions[i].SelectItem;//存進舊的ID
                    if (questions[i].AnswerForm == 1)
                        QstListToDisplay[i].strAnswerForm = "文字方塊";
                    else if (questions[i].AnswerForm == 2)
                        QstListToDisplay[i].strAnswerForm = "數字";
                    else if (questions[i].AnswerForm == 3)
                        QstListToDisplay[i].strAnswerForm = "Email";
                    else if (questions[i].AnswerForm == 4)
                        QstListToDisplay[i].strAnswerForm = "日期";
                    else if (questions[i].AnswerForm == 5)
                        QstListToDisplay[i].strAnswerForm = "單選方塊";
                    else if (questions[i].AnswerForm == 6)
                        QstListToDisplay[i].strAnswerForm = "複選方塊";
                }
                return QstListToDisplay;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("transWholeAnswer.QstToWholeList", ex);
                throw;
            }
        }

        /// <summary>
        /// 將Question轉換成WholeAnswer
        /// </summary>
        /// <param name="question">傳入值為Question</param>
        /// <returns>傳出值為WholeAnswer</returns>
        public WholeAnswer QstToWhole(Question question)
        {
            try
            {
                WholeAnswer wholeToDisplay = new WholeAnswer();

                if (question.QuestionnaireID == 0)//如果是新的問卷，取得新的ID給他
                    wholeToDisplay.QuestionnaireID = _QtnirMgr.GetNextQuestionnaireID();//賦予新的ID
                else
                    wholeToDisplay.QuestionnaireID = question.QuestionnaireID;//存進舊的ID

                wholeToDisplay.QuestContent = question.QuestContent;//存進舊的ID
                wholeToDisplay.Required = question.Required;//存進舊的ID
                wholeToDisplay.AnswerForm = question.AnswerForm;//存進舊的ID
                wholeToDisplay.SelectItem = question.SelectItem;//存進舊的ID
                if (question.AnswerForm == 1)
                    wholeToDisplay.strAnswerForm = "文字方塊";
                else if (question.AnswerForm == 2)
                    wholeToDisplay.strAnswerForm = "數字";
                else if (question.AnswerForm == 3)
                    wholeToDisplay.strAnswerForm = "Email";
                else if (question.AnswerForm == 4)
                    wholeToDisplay.strAnswerForm = "日期";
                else if (question.AnswerForm == 5)
                    wholeToDisplay.strAnswerForm = "單選方塊";
                else if (question.AnswerForm == 6)
                    wholeToDisplay.strAnswerForm = "複選方塊";

                return wholeToDisplay;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("transWholeAnswer.QstToWhole", ex);
                throw;
            }
        }

        public List<Question> WholeToQstList(List<WholeAnswer> wholeAnswers, int currentQnirID)
        {
            try
            {
                List<Question> newQstList = new List<Question>();
                for (int i = 0; i < wholeAnswers.Count; i++)
                {
                    newQstList[i].QuestionnaireID = currentQnirID;//存進舊的ID
                    newQstList[i].QuestContent = wholeAnswers[i].QuestContent;//存進舊的ID
                    newQstList[i].Required = wholeAnswers[i].Required;//存進舊的ID
                    newQstList[i].AnswerForm = wholeAnswers[i].AnswerForm;//存進舊的ID
                    newQstList[i].SelectItem = wholeAnswers[i].SelectItem;//存進舊的ID

                }
                return newQstList;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("transWholeAnswer.QstToWholeList", ex);
                throw;
            }
        }



        /// <summary>
        /// 將舊的值轉進新的List(除了QuestionnaireID)
        /// </summary>
        /// <param name="questions">List<WholeAnswer></param>
        /// <returns>回傳值為List<WholeAnswer></returns>
        public List<WholeAnswer> WholeToWholeList(List<WholeAnswer> questions)
        {
            try
            {
                List<WholeAnswer> QstListToDisplay = new List<WholeAnswer>();
                for (int i = 0; i < questions.Count; i++)
                {
                    WholeAnswer wholeAnswer = new WholeAnswer();
                    if (questions[i].QuestionnaireID == 0)
                        wholeAnswer.QuestionnaireID = _QtnirMgr.GetNextQuestionnaireID();//賦予新的ID
                    else
                        wholeAnswer.QuestionnaireID = questions[i].QuestionnaireID;//存進舊的ID

                    wholeAnswer.QuestContent = questions[i].QuestContent;//存進舊的ID
                    wholeAnswer.Required = questions[i].Required;//存進舊的ID
                    wholeAnswer.AnswerForm = questions[i].AnswerForm;//存進舊的ID
                    wholeAnswer.SelectItem = questions[i].SelectItem;//存進舊的ID
                    if (questions[i].AnswerForm == 1)
                        wholeAnswer.strAnswerForm = "文字方塊";
                    else if (questions[i].AnswerForm == 2)
                        wholeAnswer.strAnswerForm = "數字";
                    else if (questions[i].AnswerForm == 3)
                        wholeAnswer.strAnswerForm = "Email";
                    else if (questions[i].AnswerForm == 4)
                        wholeAnswer.strAnswerForm = "日期";
                    else if (questions[i].AnswerForm == 5)
                        wholeAnswer.strAnswerForm = "單選方塊";
                    else if (questions[i].AnswerForm == 6)
                        wholeAnswer.strAnswerForm = "複選方塊";
                    QstListToDisplay.Add(wholeAnswer);
                }
                return QstListToDisplay;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("transWholeAnswer.WholeToWholeList", ex);
                throw;
            }
        }
        public List<WholeAnswer> WholeToWholeList2(List<WholeAnswer> questions)//有Order的版本
        {
            try
            {
                List<WholeAnswer> QstListToDisplay = new List<WholeAnswer>();
                for (int i = 0; i < questions.Count; i++)
                {
                    WholeAnswer wholeAnswer = new WholeAnswer();
                    if (questions[i].QuestionnaireID == 0)
                        wholeAnswer.QuestionnaireID = _QtnirMgr.GetNextQuestionnaireID();//賦予新的ID
                    else
                        wholeAnswer.QuestionnaireID = questions[i].QuestionnaireID;//存進舊的ID

                    wholeAnswer.QuestContent = questions[i].QuestContent;//存進舊的ID
                    wholeAnswer.QuestOrder = questions[i].QuestOrder;//存進舊的ID
                    wholeAnswer.Required = questions[i].Required;//存進舊的ID
                    wholeAnswer.AnswerForm = questions[i].AnswerForm;//存進舊的ID
                    wholeAnswer.SelectItem = questions[i].SelectItem;//存進舊的ID
                    if (questions[i].AnswerForm == 1)
                        wholeAnswer.strAnswerForm = "文字方塊";
                    else if (questions[i].AnswerForm == 2)
                        wholeAnswer.strAnswerForm = "數字";
                    else if (questions[i].AnswerForm == 3)
                        wholeAnswer.strAnswerForm = "Email";
                    else if (questions[i].AnswerForm == 4)
                        wholeAnswer.strAnswerForm = "日期";
                    else if (questions[i].AnswerForm == 5)
                        wholeAnswer.strAnswerForm = "單選方塊";
                    else if (questions[i].AnswerForm == 6)
                        wholeAnswer.strAnswerForm = "複選方塊";
                    QstListToDisplay.Add(wholeAnswer);
                }
                return QstListToDisplay;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("transWholeAnswer.WholeToWholeList", ex);
                throw;
            }
        }


        public List<WholeAnswer> CommonToWholeList(List<CommonQuest> commons)
        {
            try
            {
                List<WholeAnswer> QstListToDisplay = new List<WholeAnswer>();
                for (int i = 0; i < commons.Count; i++)
                {
                    WholeAnswer wholeAnswer = new WholeAnswer();
                    wholeAnswer.CommonQuestID = commons[i].CommonQuestID;//存進舊的ID
                    wholeAnswer.QuestContent = commons[i].QuestContent;//存進舊的ID
                    wholeAnswer.AnswerForm = commons[i].AnswerForm;//存進舊的ID
                    wholeAnswer.SelectItem = commons[i].SelectItem;//存進舊的ID
                    wholeAnswer.Required = commons[i].Required;//存進舊的ID

                    if (commons[i].AnswerForm == 1)
                        wholeAnswer.strAnswerForm = "文字方塊";
                    else if (commons[i].AnswerForm == 2)
                        wholeAnswer.strAnswerForm = "數字";
                    else if (commons[i].AnswerForm == 3)
                        wholeAnswer.strAnswerForm = "Email";
                    else if (commons[i].AnswerForm == 4)
                        wholeAnswer.strAnswerForm = "日期";
                    else if (commons[i].AnswerForm == 5)
                        wholeAnswer.strAnswerForm = "單選方塊";
                    else if (commons[i].AnswerForm == 6)
                        wholeAnswer.strAnswerForm = "複選方塊";
                    QstListToDisplay.Add(wholeAnswer);
                }
                return QstListToDisplay;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("transWholeAnswer.CommonToWholeList", ex);
                throw;
            }
        }
    }
}