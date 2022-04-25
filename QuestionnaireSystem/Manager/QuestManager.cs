using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionnaireSystem.Manager
{
    public class QuestManager
    {
        /// <summary>
        /// 將問題種類轉換成數字
        /// </summary>
        /// <param name="inpAnswerForm">傳入值為string</param>
        /// <returns>回傳值為int</returns>
        public int AnswerTextTOInt(string inpAnswerForm)
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
    }
}