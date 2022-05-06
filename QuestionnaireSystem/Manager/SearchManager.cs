using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.Helper;
using QuestionnaireSystem.ORM;


namespace QuestionnaireSystem.Manager
{
    public class SearchManager
    {
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //主問卷的Manager
        private CheckInputManager _chkInpMgr = new CheckInputManager();    //確認輸入值的Manager

        /// <summary>
        /// 搜尋問卷List內，包含關鍵字的資料
        /// </summary>
        /// <param name="srchText">使用者輸入的關鍵字(string)</param>
        /// <param name="checkedList">被比對的問卷List(List<Questionnaire>)</param>
        /// <returns>回傳值為List<Questionnaire></returns>
        public List<Questionnaire> GetIncludeTextList(string srchKey, List<Questionnaire> checkedList)
        {
            try
            {
                if (srchKey == null)
                    return checkedList;
                else
                {
                    List<Questionnaire> includeList = new List<Questionnaire>();
                    int keyAmount = 0;
                    if (_chkInpMgr.IncludeText(srchKey, " "))
                    {
                        keyAmount = srchKey.Count(f => f == ' ') + 1;

                        string[] subs = new string[keyAmount];
                        if (!string.IsNullOrWhiteSpace(srchKey))
                            subs = srchKey.Split(' ');

                        foreach (Questionnaire item in checkedList)
                        {
                            foreach (string sub in subs)
                            {
                                if (_chkInpMgr.IncludeText(item.Caption, sub))
                                {
                                    includeList.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Questionnaire item in checkedList)
                        {
                            if (_chkInpMgr.IncludeText(item.Caption, srchKey))
                            {
                                includeList.Add(item);
                            }
                        }
                    }

                    return includeList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("SearchManager.GetIncludeTextList", ex);
                throw;
            }
        }

        /// <summary>
        /// 搜尋問卷List內，於指定時間內的資料
        /// </summary>
        /// <param name="beginDate">開始時間(string)</param>
        /// <param name="endDate">結束時間(string)</param>
        /// <param name="checkedList">被比對的問卷List(List<Questionnaire>)</param>
        /// <returns>回傳值為List<Questionnaire></returns>
        public List<Questionnaire> GetIncludeDate(string beginDate, string endDate, List<Questionnaire> checkedList)
        {
            try
            {
                //將輸入的時間string轉為DateTime
                DateTime DTBeginDate = Convert.ToDateTime(beginDate);
                DateTime DTEndDate = Convert.ToDateTime(endDate);

                List<Questionnaire> result = new List<Questionnaire>();
                //將標題不包含關鍵字的資料從List移除
                foreach (Questionnaire item in checkedList)
                {
                    if (_chkInpMgr.InDateRange(DTBeginDate, DTEndDate, item.StartDate) && _chkInpMgr.InDateRange(DTBeginDate, DTEndDate, item.EndDate))
                    {
                        result.Add(item);
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                Logger.WriteLog("SearchManager.GetIncludeDate", ex);
                throw;
            }

        }
    }
}