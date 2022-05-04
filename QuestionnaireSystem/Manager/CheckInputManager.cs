using QuestionnaireSystem.Helper;
using QuestionnaireSystem.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QuestionnaireSystem.Manager
{
    public class CheckInputManager
    {

        /// <summary>
        /// 判斷字串文字是否皆為英數
        /// </summary>
        /// <param name="str">(string)檢測的字串</param>
        /// <param name="outAccount">(string)可用於輸出alert的字串，若boolean結果為false，輸出值將為空字串</param>
        /// <returns>回傳值為boolean</returns>
        public bool IsNumAndEG(string str, out string outAccount)
        {
            try
            {
                Regex NumandEG = new Regex("[^A-Za-z0-9]");
                bool result = !NumandEG.IsMatch(str);

                outAccount = "";
                if (result)
                    outAccount = str;
                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("CheckInputManager.IsNumAndEG", ex);
                throw;
            }
        }

        /// <summary>
        /// 確認字串是否包含輸入值
        /// </summary>
        /// <param name="checkedData">需要被確認的字串</param>
        /// <param name="inpText">輸入值</param>
        /// <returns>回傳值為boolean</returns>
        public bool IncludeText(string checkedData, string inpText)
        {
            string inpString = inpText.Trim();   //將使用者輸入的字串去除Space

            try
            {
                bool isInclude = checkedData.Contains(inpText);  //checkString：輸入字串

                return isInclude;
            }

            catch (Exception ex)
            {
                Logger.WriteLog("CheckInputManager.IncludeText", ex);
                throw;
            }
        }

        /// <summary>
        /// 確認字串是否包含全形分號；
        /// </summary>
        /// <param name="checkedData">需要被確認的字串</param>
        /// <returns>回傳值為boolean</returns>
        public bool IsMistakeSemicolon(string checkedData)
        {
            string mistakeSemicolon = "；";

            try
            {
                bool isInclude = checkedData.Contains(mistakeSemicolon);  //checkString：輸入字串，BanWord：禁字

                return isInclude;
            }

            catch (Exception ex)
            {
                Logger.WriteLog("CheckInputManager.IncludeText", ex);
                throw;
            }
        }

        /// <summary>
        /// 確認日期是否於時間範圍內
        /// </summary>
        /// <param name="beginDate">起始日期(DateTime)</param>
        /// <param name="endDate">結束日期(DateTime)</param>
        /// <param name="checkedData">需要被確認的日期(DateTime)</param>
        /// <returns></returns>
        public bool InDateRange(DateTime beginDate, DateTime endDate, DateTime checkedData)
        {
            bool isInclude = false;
            try
            {
                if(beginDate < checkedData && checkedData < endDate)
                {
                    return isInclude = true;
                }
                return isInclude;
            }

            catch (Exception ex)
            {
                Logger.WriteLog("CheckInputManager.InDateRange", ex);
                throw;
            }
        }


    }
}