using QuestionnaireSystem.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionnaireSystem.Manager
{
    public class Statistic
    {
        private AnswerManager _AnswerMgr = new AnswerManager();    //填寫資料
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //問卷基本資料
        private QuestManager _QuestMgr = new QuestManager();    //填寫資料

        /// <summary>
        /// 依照指定的問卷編號，查出每道題目的投票數
        /// </summary>
        /// <param name="QnirID">指定問卷編號，傳入值為string</param>
        /// <returns>回傳值為Dictionary<int, int[]></returns>
        public Dictionary<int, int[]> getAllStatisticCount(string QnirID)
        {
            Dictionary<int, int[]> countEachSelectionInQuest = new Dictionary<int, int[]>();

            List<WholeAnswer> allData = _AnswerMgr.GetWholeDoneList(QnirID);//集合(數量為每張問卷*所有問題筆數)
            int answerAmount = _QuestMgr.GetQuestionList(QnirID).Count();//該問卷的總題數

            for (int i = 0; i < answerAmount; i++)
            {
                //依序取出每道題所有人的作答資料
                List<WholeAnswer> allThisQuestData = allData.Where(obj => obj.QuestOrder == i + 1).ToList();
                //依序取出每道題的選項
                WholeAnswer QuestOrderInThisQnir = allThisQuestData.First();
                try
                {
                    string[] arrString = _QuestMgr.SplitSelectItem(QuestOrderInThisQnir.SelectItem);
                    //依序取出每道題目的選項數目
                    int selectionAmount = arrString.Count();

                    //陣列容納該道題每個選項的數量
                    int[] ddd = new int[selectionAmount];
                    for (int j = 0; j < selectionAmount; j++)
                    {
                        ddd[j] = allThisQuestData.Where(obj => obj.Answer == arrString[j]).Count();
                    }
                    countEachSelectionInQuest.Add(i + 1, ddd);
                }
                catch {
                    countEachSelectionInQuest.Add(i + 1, null);

                }
            }
            return countEachSelectionInQuest;
        }



    }
}