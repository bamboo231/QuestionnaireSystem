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

        /// <summary>
        /// 取得指定常用問題
        /// </summary>
        /// <param name="commonQuestID">傳入值為int(指定常用問題ID)</param>
        /// <returns>回傳值為CommonQuest</returns>
        public CommonQuest GetCommonQuest(int commonQuestID)
        {
            using (ContextModel contextModel = new ContextModel())
            {
                return contextModel.CommonQuests.Where(obj => obj.CommonQuestID == commonQuestID).FirstOrDefault();
            }
        }
        /// <summary>
        /// 取得指定常用問題
        /// </summary>
        /// <param name="commonQuestID">傳入值為string(指定常用問題ID)</param>
        /// <returns>回傳值為CommonQuest</returns>
        public CommonQuest GetCommonQuest(string commonQuestID)
        {
            int intCommonQuestID = Int32.Parse(commonQuestID);
            using (ContextModel contextModel = new ContextModel())
            {
                return contextModel.CommonQuests.Where(obj => obj.CommonQuestID == intCommonQuestID).FirstOrDefault();
            }
        }

        public void AddCommonQuest(CommonQuest addCommonQuest)
        {
            using (var context = new ORM.ContextModel())
            {
                context.CommonQuests.Add(addCommonQuest);
                context.SaveChanges();
            }
        }

        public void DeleteQuestionnaire(List<CommonQuest> deletedIDList)
        {
            using (var context = new ORM.ContextModel())
            {
                foreach (var item in deletedIDList)
                {
                    var checkedQnir = context.CommonQuests.Where(obj => obj.CommonQuestID == item.CommonQuestID).First();
                    context.CommonQuests.Remove(checkedQnir);
                    context.SaveChanges();
                }
            }
        }
    }
}