using QuestionnaireSystem.Manager;
using QuestionnaireSystem.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionnaireSystem
{
    public partial class StatisticPage : System.Web.UI.Page
    {
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //問卷基本資料管理
        private CommonQuestManager _CommonMgr = new CommonQuestManager();    //常用問題管理
        private AnswerManager _AnswerMgr = new AnswerManager();    //回覆管理
        private QuestManager _QuestMgr = new QuestManager();    //問題管理
        private Statistic _statisMgr = new Statistic();    //統計管理
        private CheckInputManager _checksMgr = new CheckInputManager();    //統計管理
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID
            string QuestOrder = this.Request.QueryString["QuestOrder"];  //從URL取得當前所在的問卷ID
            string currentBsicAnsID = this.Request.QueryString["BsicAnsID"];//從URL取得當前查看的問卷細節
            string targetplh = this.Request.QueryString["Targetplh"];//從URL取得要查看的頁籤


            #region//後台統計頁
            if (currentQnirID != null)
            {
                //顯示問卷題目及填寫的內容 
                this.plhbookmark4.Controls.Clear();     //清空統計的placeHolder
                                                        //題目的資料
                List<Question> dispQuestion = _QuestMgr.GetQuestionList(currentQnirID);

                //取得統計的數量
                Dictionary<string, int[]> allAmount = _statisMgr.getAllStatisticCount(currentQnirID);
                int questID = 0;
                foreach (Question item in dispQuestion)
                {
                    questID++;

                    string stringQuestID = questID.ToString();

                    int[] arrAllThisQuestAmount = allAmount[stringQuestID];

                    //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                    if (item.AnswerForm == 5)
                    {
                        Label dynLabel = new Label()//標題
                        {
                            ID = $"dynTitle{item.QuestOrder}",
                            Text = $"{item.QuestOrder} . {item.QuestContent}",
                        };
                        Label lb = new Label() { ID = $"lb{item.QuestOrder}", Text = "<br>", }; //分行

                        plhbookmark4.Controls.Add(dynLabel);
                        plhbookmark4.Controls.Add(lb);
                        string[] arrSelectItem = null;
                        int selectionCount = 0;
                        _QuestMgr.SplitSelectItem(item.SelectItem, out selectionCount, out arrSelectItem);//本題選項數量,本題選項陣列

                        for (int i = 0; i < selectionCount; i++)
                        {
                            Label dynSelection = new Label()
                            {
                                ID = $"Selection",
                                Text = $"{arrSelectItem[i]}",
                            };

                            int count = 0;
                            if (arrSelectItem[i] != null)
                                count = arrAllThisQuestAmount[i];
                            Label dynCount = new Label()
                            {
                                ID = $"Count",
                                Text = $"({count})",
                            };
                            Label lb2 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br>", }; //分行

                            plhbookmark4.Controls.Add(dynSelection);
                            plhbookmark4.Controls.Add(dynCount);
                            plhbookmark4.Controls.Add(lb2);

                        }
                        Label lb3 = new Label() { ID = $"lb{item.QuestID}_2", Text = "<br>", }; //分行
                        plhbookmark4.Controls.Add(lb3);
                    }
                    else if (item.AnswerForm == 6)
                    {
                        Label lb = new Label() { ID = $"lb{item.QuestID}", Text = "<br>", }; //分行

                        Label dynLabel = new Label()//標題
                        {
                            ID = $"dynTitle{item.QuestOrder}",
                            Text = $"{item.QuestOrder} . {item.QuestContent}",
                        };

                        plhbookmark4.Controls.Add(dynLabel);
                        plhbookmark4.Controls.Add(lb);

                        string[] arrSelectItem = null;
                        int selectionCount = 0;
                        _QuestMgr.SplitSelectItem(item.SelectItem, out selectionCount, out arrSelectItem);  //選項的數量,選項的陣列

                        //本題選項數量
                        for (int i = 0; i < selectionCount; i++)
                        {

                            Label dynSelection = new Label()
                            {
                                ID = $"Selection",
                                Text = $"{arrSelectItem[i]}",
                            };

                            int count = 0;
                            if (arrSelectItem[i] != null)
                                count = arrAllThisQuestAmount[i];
                            Label dynCount = new Label()
                            {
                                ID = $"Count",
                                Text = $"({count})",
                            };
                            Label lb2 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br>", }; //分行

                            plhbookmark4.Controls.Add(dynSelection);
                            plhbookmark4.Controls.Add(dynCount);
                            plhbookmark4.Controls.Add(lb2);
                        }
                        Label lb3 = new Label() { ID = $"lb{item.QuestID}_2", Text = "<br>", }; //分行
                        plhbookmark4.Controls.Add(lb3);
                    }
                    else
                    {
                        Label dynLabel = new Label()
                        {
                            ID = $"dynTitle{item.QuestOrder}",
                            Text = $"{item.QuestOrder} . {item.QuestContent}",
                        };
                        Label content = new Label()
                        {
                            ID = "noneDisplay",
                            Text = "-",
                        };
                        Label lb = new Label() { ID = $"lb{item.QuestOrder}", Text = "<br>", }; //分行
                        plhbookmark4.Controls.Add(dynLabel);
                        plhbookmark4.Controls.Add(lb);

                        Label lb2 = new Label() { ID = $"lb{item.QuestOrder}", Text = "<br>", }; //分行
                        plhbookmark4.Controls.Add(content);
                        plhbookmark4.Controls.Add(lb2);

                        Label lb3 = new Label() { ID = $"lb{item.QuestOrder}_2", Text = "<br>", }; //分行
                        plhbookmark4.Controls.Add(lb3);
                    }
                }
            }
            #endregion
        }
    }
}