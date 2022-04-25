using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.ORM;

namespace QuestionnaireSystem.Manager
{
    public class AnswerManager
    {
        /// <summary>
        /// 取得所有回覆問卷的List
        /// </summary>
        /// <returns>回傳值為List</returns>
        public List<BasicAnswer> GetBasicAnswerList()
        {
            using (ContextModel contextModel = new ContextModel())
            {
                return contextModel.BasicAnswers.ToList();
            }
        }
    }
}