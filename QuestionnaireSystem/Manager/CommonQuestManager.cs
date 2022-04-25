using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.ORM;


namespace QuestionnaireSystem.Manager
{
    public class CommonQuestManager
    {
        /// <summary>
        /// 取得所有常用問題的List
        /// </summary>
        /// <returns>回傳值為List</returns>
        public List<CommonQuest> GetCommonQuestList()
        {
            using (ContextModel contextModel = new ContextModel())
            {
                return contextModel.CommonQuests.ToList();
            }
        }
    }
}