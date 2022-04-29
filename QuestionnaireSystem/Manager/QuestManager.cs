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
        static int countIndex;   //排序問題list時，計數用

        /// <summary>
        /// 將問題種類string轉換成int
        /// </summary>
        /// <param name="inpAnswerForm">傳入值為string</param>
        /// <returns>回傳值為int</returns>
        public int AnswerTextToInt(string inpAnswerForm)
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
        /// <summary>
        /// 將問題種類int轉換成string
        /// </summary>
        /// <param name="inpAnswerForm">傳入值為int</param>
        /// <returns>回傳值為string</returns>
        public string AnswerTextToInt(int inpAnswerForm)
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

        /// <summary>
        /// 重新排序List<Question>
        /// </summary>
        /// <param name="questionList">輸入值為List<Question></param>
        /// <returns>回傳值為List<Question></returns>
        public List<Question> ReOrderQuestionList(List<Question> questionList)
        {
            countIndex = 0;
            try
            {
                //宣告空的List
                List<Question> newList = new List<Question>();
                //逐筆將原List資料修改順序後塞進新List
                foreach (Question question in questionList)
                {
                    Question Quest = question;
                    countIndex++;
                    Quest.QuestOrder = countIndex;
                    newList.Add(Quest);
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
                int intQnirID = Int32.Parse(questionnaireID);

                using (ContextModel contextModel = new ContextModel())
                {
                    List<Question> qList = contextModel.Questions.Where(obj => obj.QuestionnaireID == intQnirID).ToList();
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

    }
}