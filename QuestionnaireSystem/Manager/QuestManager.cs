using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.Helper;
using QuestionnaireSystem.ORM;

namespace QuestionnaireSystem.Manager
{
    public class QuestManager
    {
        private transWholeAnswerManager _transMgr = new transWholeAnswerManager();    //轉換WholeAnswer

        static int countIndex;   //排序問題list時，計數用

        /// <summary>
        /// 將問題種類string轉換成int
        /// </summary>
        /// <param name="inpAnswerForm">傳入值為string</param>
        /// <returns>回傳值為int</returns>
        public int AnswerTextToInt(string inpAnswerForm)
        {
            try
            {
                int answer = 0;
                switch (inpAnswerForm)
                {
                    case "文字方塊":
                        return answer = 1;
                    case "數字":
                        return answer = 2;
                    case "Email":
                        return answer = 3;
                    case "日期":
                        return answer = 4;
                    case "單選方塊":
                        return answer = 5;
                    default:
                        return answer = 6;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.AnswerTextToInt", ex);
                throw;
            }
        }
        /// <summary>
        /// 將問題種類int轉換成string
        /// </summary>
        /// <param name="inpAnswerForm">傳入值為int</param>
        /// <returns>回傳值為string</returns>
        public string AnswerTextToInt(int inpAnswerForm)
        {
            try
            {
                switch (inpAnswerForm)
                {
                    case 1:
                        return "文字方塊";
                    case 2:
                        return "數字";
                    case 3:
                        return "Email";
                    case 4:
                        return "日期";
                    case 5:
                        return "單選方塊";
                    default:
                        return "複選方塊";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.AnswerTextToInt", ex);
                throw;
            }
        }

        /// <summary>
        /// 修改其中一筆問題
        /// </summary>
        /// <param name="inpQuest">記憶體中的問題列表(傳入值為List<WholeAnswer>)</param>
        /// <param name="UpdateDATA">更新的資料(傳入值為WholeAnswer)</param>
        /// <returns></returns>
        public List<WholeAnswer> UpdateQuest(List<WholeAnswer> inpQuest, WholeAnswer UpdateDATA)
        {
            try
            {
                //題號
                int updateOrder = UpdateDATA.QuestOrder;

                //修改List內的該筆資料
                var item = inpQuest.Skip(updateOrder - 1).FirstOrDefault();
                item.QuestID = UpdateDATA.QuestID;
                item.QuestionnaireID = UpdateDATA.QuestionnaireID;
                item.QuestContent = UpdateDATA.QuestContent;
                item.AnswerForm = UpdateDATA.AnswerForm;
                item.SelectItem = UpdateDATA.SelectItem;
                item.Required = UpdateDATA.Required;
                return inpQuest;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.UpdateQuest", ex);
                throw;
            }
        }





        /// <summary>
        /// 重新排序List<Question>
        /// </summary>
        /// <param name="questionList">輸入值為List<Question></param>
        /// <returns>回傳值為List<Question></returns>
        public List<WholeAnswer> ReOrderQuestionList(List<WholeAnswer> questionList)
        {
            countIndex = 0;
            try
            {
                //宣告空的List
                List<WholeAnswer> newList = new List<WholeAnswer>();
                //逐筆將原List資料修改順序後塞進新List
                foreach (WholeAnswer question in questionList)
                {
                    WholeAnswer a = new WholeAnswer();
                    countIndex++;
                    question.QuestOrder = countIndex;
                    a = question;
                    newList.Add(a);
                }
                return newList; //回傳新的List
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.ReOrderQuestionList", ex);
                throw;
            }
        }

        /// <summary>
        /// 將DB的問題選項切割成字串List
        /// </summary>
        /// <param name="inpSelectItem">傳入值為string</param>
        public void SplitSelectItem(string inpSelectItem, out int amount, out string[] splitArray)
        {
            try
            {
                amount = 0;
                splitArray = null;
                if (inpSelectItem != null)
                {
                    splitArray = inpSelectItem.Split(';');
                    amount = splitArray.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.SplitSelectItem", ex);
                throw;
            }
        }



        /// <summary>
        /// 依問卷號取出題目的List
        /// </summary>
        /// <param name="questionnaireID">傳入值為string</param>
        /// <returns>回傳值為List<Question></returns>
        public List<Question> GetQuestionList(string questionnaireID)
        {
            try
            {
                if (questionnaireID == null || questionnaireID == "")
                {
                    List<Question> empty = new List<Question>();
                    return empty;
                }
                else
                {
                    int intQnirID = Int32.Parse(questionnaireID);

                    using (ContextModel contextModel = new ContextModel())
                    {
                        List<Question> qList = contextModel.Questions.Where(obj => obj.QuestionnaireID == intQnirID).ToList();
                        //缺:如果沒有任何人填問卷該怎麼處理
                        return qList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.GetQuestionList", ex);
                throw;
            }
        }

        /// <summary>
        /// 依問卷號取出題目的List
        /// </summary>
        /// <param name="questionnaireID">傳入值為int</param>
        /// <param name="_isNewQstnir">傳入值為bool</param>
        /// <returns>回傳值為List<WholeAnswer></returns>
        public List<WholeAnswer> GetWholeQuestionList(int questionnaireID, bool _isNewQstnir)
        {
            try
            {
                List<WholeAnswer> newList = new List<WholeAnswer>();

                if (_isNewQstnir)
                {
                    return newList;
                }
                else
                {
                    List<Question> QuestionList = GetQuestionList(questionnaireID);

                    for (int i = 0; i < QuestionList.Count; i++)
                    {
                        WholeAnswer newWhole = new WholeAnswer()
                        {
                            QuestID = QuestionList[i].QuestID,
                            QuestionnaireID = QuestionList[i].QuestionnaireID,
                            QuestOrder = QuestionList[i].QuestOrder,
                            QuestContent = QuestionList[i].QuestContent,
                            AnswerForm = QuestionList[i].AnswerForm,
                            Required = QuestionList[i].Required,
                            SelectItem = QuestionList[i].SelectItem,
                        };
                        newList.Add(newWhole);
                    }
                }
                return newList;

            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.GetQuestionList", ex);
                throw;
            }
        }


        /// <summary>
        /// 依問卷號取出題目的List
        /// </summary>
        /// <param name="questionnaireID">傳入值為int</param>
        /// <returns>回傳值為List<Question></returns>
        public List<Question> GetQuestionList(int questionnaireID)
        {
            try
            {

                using (ContextModel contextModel = new ContextModel())
                {
                    List<Question> qList = contextModel.Questions.Where(obj => obj.QuestionnaireID == questionnaireID).ToList();
                    //缺:如果沒有任何人填問卷該怎麼處理
                    return qList;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.GetQuestionList", ex);
                throw;
            }
        }
        /// <summary>
        /// 依問卷號取出題目的List
        /// </summary>
        /// <param name="questionnaireID">傳入值為int</param>
        /// <returns>回傳值為List<Question></returns>
        public List<Question> GetQuestionList(int questionnaireID, bool isNewQstnir)
        {
            try
            {
                if (isNewQstnir)
                {
                    List<Question> qList = new List<Question>();
                    return qList;
                }
                else
                {
                    using (ContextModel contextModel = new ContextModel())
                    {
                        List<Question> qList = contextModel.Questions.Where(obj => obj.QuestionnaireID == questionnaireID).ToList();
                        //缺:如果沒有任何人填問卷該怎麼處理
                        return qList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.GetQuestionList", ex);
                throw;
            }
        }
        /// <summary>
        /// 依問卷號取出題目
        /// </summary>
        /// <param name="questionnaireID">傳入值為int</param>
        /// <returns>回傳值為Question類別<Question></returns>
        public Question GetQuestion(int questionnaireID)
        {
            try
            {

                using (ContextModel contextModel = new ContextModel())
                {
                    Question qst = contextModel.Questions.Where(obj => obj.QuestionnaireID == questionnaireID).First();
                    //缺:如果沒有任何人填問卷該怎麼處理
                    return qst;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.GetQuestionList", ex);
                throw;
            }
        }
        /// <summary>
        /// 依問卷號取出題目
        /// </summary>
        /// <param name="questionnaireID">傳入值為int</param>
        /// <returns>回傳值為Question類別<Question></returns>
        public Question GetQuestion(string questionnaireID)
        {
            int intQst = int.Parse(questionnaireID);
            try
            {

                using (ContextModel contextModel = new ContextModel())
                {
                    Question qst = contextModel.Questions.Where(obj => obj.QuestionnaireID == intQst).First();
                    //缺:如果沒有任何人填問卷該怎麼處理
                    return qst;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestManager.GetQuestionList", ex);
                throw;
            }
        }

        /// <summary>
        /// 取得必填問題
        /// </summary>
        /// <param name="QstnirID"></param>
        /// <returns></returns>
        public List<Question> GetRequiredQuest(int QstnirID)
        {
            using (ContextModel contextModel = new ContextModel())
            {

                var questList = contextModel.Questions.Where(obj => obj.QuestionnaireID == QstnirID).ToList();
                var focusQuest = questList.Where(obj => obj.Required == true).ToList();
                return focusQuest;
            }
        }
        public List<Question> GetRequiredQuest(string strQstnirID)
        {
            int QstnirID = int.Parse(strQstnirID);
            using (ContextModel contextModel = new ContextModel())
            {

                var questList = contextModel.Questions.Where(obj => obj.QuestionnaireID == QstnirID).ToList();
                var focusQuest = questList.Where(obj => obj.Required == true).ToList();
                return focusQuest;
            }
        }
    }
}