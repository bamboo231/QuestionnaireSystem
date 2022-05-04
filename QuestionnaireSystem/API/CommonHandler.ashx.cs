using QuestionnaireSystem.Manager;
using QuestionnaireSystem.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Net;

namespace QuestionnaireSystem.API
{
    /// <summary>
    /// CommonHandler1 的摘要描述
    /// </summary>
    public class CommonHandler1 : IHttpHandler
    {

        private CommonQuestManager _CommonMgr = new CommonQuestManager();    //常用問題管理
        private transWholeAnswerManager _transMgr = new transWholeAnswerManager();    //轉換WholeAnswer

        public void ProcessRequest(HttpContext context)
        {
            //取得

            if (string.Compare("GET", context.Request.HttpMethod, true) == 0)
            {
                List<CommonQuest> CommonList = _CommonMgr.GetCommonQuestList();

                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(CommonList);

                context.Response.ContentType = "JSON";
                context.Response.Write(jsonText);
                return;
            }
            //回傳

            if (string.Compare("POST", context.Request.HttpMethod) == 0 && string.Compare("ChangeSelect", context.Request.QueryString["Action"], true) == 0)
            {

                try
                {
                    string getCommon = context.Request.Form["QuestContent"];

                    List<CommonQuest> commons = _CommonMgr.GetCommonQuestList();
                    List<WholeAnswer> wholes = _transMgr.CommonToWholeList(commons);

                    //準備要塞進JSON的
                    WholeAnswer modeldown = new WholeAnswer();

                    foreach (WholeAnswer item in wholes)
                    {
                        if (getCommon == item.QuestContent)
                        {
                            modeldown.QuestContent = item.QuestContent;
                            modeldown.Required = item.Required;
                            modeldown.SelectItem = item.SelectItem;
                            modeldown.strAnswerForm = item.strAnswerForm;
                            modeldown.AnswerForm = item.AnswerForm;

                        }
                    }
                    List<WholeAnswer> a = new List<WholeAnswer>();
                    a.Add(modeldown);
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(a);
                    context.Response.ContentType = "JSON";
                    context.Response.Write(jsonText);
                    return;

                }
                catch
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("error");
                    return;
                }
            }


        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}