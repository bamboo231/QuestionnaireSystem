using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuestionnaireSystem.Manager;
using QuestionnaireSystem.ORM;

namespace QuestionnaireSystem.admin
{
    public partial class EditQuestionnaire : System.Web.UI.Page
    {
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //問卷基本資料管理
        private CommonQuestManager _CommonMgr = new CommonQuestManager();    //常用問題管理
        private AnswerManager _AnswerMgr = new AnswerManager();    //回覆管理
        private QuestManager _QuestMgr = new QuestManager();    //問題管理
        private Statistic _statisMgr = new Statistic();    //統計管理

        private static int addQuestTime = 0;//新增題目的次數
        private static Questionnaire basicQnir = new Questionnaire();//題目的基本資料
        private static List<Question> questSessionsList = new List<Question>();//題目的List
        private static List<Answer> targetDoneID = new List<Answer>();//檢視特定作答詳細內容


        //bug:刪除問題後回到此頁並顯示問題，並且順序號碼要重新整理、DB無法帶入(觀看細節)動態控制項、統計頁數據有誤、統計缺%數
        //未做:提示、帶入常用問題、修改問題、分頁
        //待改善:頁籤事件改成enum
        //待問:ItemTemplate和AlternatingItemTemplate的控制項ID能不能相同
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID
            string currentBsicAnsID = this.Request.QueryString["BsicAnsID"];//從URL取得當前查看的問卷細節
            string targetplh = this.Request.QueryString["Targetplh"];//從URL取得要查看的頁籤

            if (!IsPostBack)
            {
                #region//設問卷ID至session
                if (currentQnirID == null)//沒有帶問卷ID就是新增
                {
                    int newID = this._QtnirMgr.GetQuestionnaireList().Count() + 1;
                    HttpContext.Current.Session["QnirID"] = newID;
                }
                else//帶ID就是修改舊的內容
                {
                    HttpContext.Current.Session["QnirID"] = currentQnirID;//將QnirID存進session

                }
                #endregion

                #region//常用問題下拉選單
                List<CommonQuest> CommonList = _CommonMgr.GetCommonQuestList();
                foreach (CommonQuest quest in CommonList)
                {
                    this.setQuestType.Items.Add(quest.QuestContent);
                }
                #endregion

                #region//繳回的問卷列表
                if (currentQnirID != null)
                {
                    List<BasicAnswer> doneList = _AnswerMgr.GetDoneList(currentQnirID);//既有問卷才帶入DB繳回問卷列表

                    this.RptrAnswerList.DataSource = doneList;
                    this.RptrAnswerList.DataBind();
                }
                #endregion

                #region//繳回的問卷細節
                if (currentQnirID != null && currentBsicAnsID != null)
                {
                    this.plhdoneList.Visible = false;
                    this.plhDoneDetail.Visible = true;
                    #region//繳回的問卷的細節
                    //顯示問卷題目及填寫的內容 
                    this.plhDynDetail.Controls.Clear();     //rptDoneDetail:訪客填問卷的細節的繫節
                    List<WholeAnswer> answers = _AnswerMgr.GetTargetType(currentBsicAnsID);
                    foreach (WholeAnswer item in answers)
                    {
                        //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                        if (item.AnswerForm == 5)
                        {
                            Label dynLabel = new Label()
                            {
                                ID = $"dynTitle{item.QuestID}",
                                Text = $"{item.QuestID} + {item.Answer}",
                            };

                            RadioButtonList dynRadioButton = new RadioButtonList()
                            {
                                ID = $"dynAnswer{item.QuestID}"
                            };
                            plhDynDetail.Controls.Add(dynLabel);
                            plhDynDetail.Controls.Add(dynRadioButton);

                            //單選選項切割字串，並動態輸入選項
                            string[] arrSelection = _QuestMgr.SplitSelectItem(item.SelectItem);
                            foreach (string selection in arrSelection)
                            {
                                ListItem dynListItem = new ListItem()
                                {
                                    Text = $"{item.SelectItem}"
                                };
                                dynRadioButton.Items.Add(dynListItem);
                            }
                            //this.ViewState["TextBoxAdded"] = true;
                        }
                        else if (item.AnswerForm == 6)
                        {
                            Label dynLabel = new Label()
                            {
                                ID = $"dynTitle{item.QuestID}",
                                Text = $"{item.QuestID} + {item.Answer}",
                            };

                            CheckBoxList dynCheckBoxList = new CheckBoxList()
                            {
                                ID = $"dynAnswer{item.QuestID}"
                            };
                            plhDynDetail.Controls.Add(dynLabel);
                            plhDynDetail.Controls.Add(dynCheckBoxList);

                            //單選選項切割字串，並動態輸入選項
                            string[] arrSelection = _QuestMgr.SplitSelectItem(item.SelectItem);
                            foreach (string selection in arrSelection)
                            {
                                ListItem dynListItem = new ListItem()
                                {
                                    Text = $"{item.SelectItem}"
                                };
                                dynCheckBoxList.Items.Add(dynListItem);
                            }
                            //this.ViewState["TextBoxAdded"] = true;
                        }
                        else
                        {
                            Label dynLabel = new Label()
                            {
                                ID = $"dynTitle{item.QuestID}",
                                Text = $"{item.QuestID} . {item.Answer}",
                            };

                            TextBox textbox = new TextBox()
                            {
                                ID = $"dynAnswer{item.QuestID}",
                                ReadOnly = true,
                                Text = $"{item.Answer}",
                            };
                            plhDynDetail.Controls.Add(dynLabel);
                            plhDynDetail.Controls.Add(textbox);
                            //this.ViewState["TextBoxAdded"] = true;
                        }
                    }
                    #endregion

                }
                #endregion

                #region//如果是既有問卷把舊資料帶入
                if (currentQnirID != null)
                {
                    Questionnaire questionnaire = _QtnirMgr.GetQuestionnaire(currentQnirID);
                    this.textCaption.Text = questionnaire.Caption;
                    this.textQuestionnaireContent.Text = questionnaire.QuestionnaireContent;
                    this.textStartDate.Text = questionnaire.StartDate.ToString("yyyy/MM/dd");
                    this.textEndDate.Text = questionnaire.EndDate.ToString("yyyy/MM/dd");
                    this.ChkBxVoidStatus.Checked = questionnaire.VoidStatus;

                    questSessionsList = _QuestMgr.GetQuestionList(currentQnirID);
                    this.RptrQuest.DataSource = questSessionsList;
                    this.RptrQuest.DataBind();
                }
                #endregion

            }
            else//回此頁面
            {
                //將編輯的題目繫節至表格
                this.RptrQuest.DataSource = questSessionsList;
                this.RptrQuest.DataBind();
            }

        }
        protected void Page_Prerender(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID
            string currentBsicAnsID = this.Request.QueryString["BsicAnsID"];//從URL取得當前查看的問卷細節
            string targetplh = this.Request.QueryString["Targetplh"];//從URL取得要查看的頁籤

            //進入哪一個頁籤
            if (targetplh == "3")
            {
                this.plhbookmark1.Visible = false;
                this.plhbookmark2.Visible = false;
                this.plhbookmark3.Visible = true;
            }
            #region//後台統計頁
            if (currentQnirID != null)
            {
                //顯示問卷題目及填寫的內容 
                this.plhbookmark4.Controls.Clear();     //清空統計的placeHolder
                                                        //題目的資料
                List<Question> dispQuestion = _QuestMgr.GetQuestionList(currentQnirID);

                //取得統計的數量
                Dictionary<int, int[]> allAmount = _statisMgr.getAllStatisticCount(currentQnirID);
                foreach (Question item in dispQuestion)
                {

                    //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                    if (item.AnswerForm == 5)
                    {
                        Label dynLabel = new Label()//標題
                        {
                            ID = $"dynTitle{item.QuestID}",
                            Text = $"{item.QuestID} . {item.QuestContent}",
                        };
                        Label lb = new Label() { ID = $"lb{item.QuestID}", Text = "<br>", }; //分行

                        plhbookmark4.Controls.Add(dynLabel);
                        plhbookmark4.Controls.Add(lb);
                        string[] arrSelectItem = _QuestMgr.SplitSelectItem(item.SelectItem);


                        //本題選項數量
                        int selectionCount = _QuestMgr.SplitSelectItem(item.SelectItem).Count();
                        for (int i = 0; i < selectionCount; i++)
                        {
                            Label dynSelection = new Label()
                            {
                                ID = $"Selection",
                                Text = $"{arrSelectItem[i]}",
                            };

                            int count = 0;
                            if (arrSelectItem[i] != null)
                                count = arrSelectItem[i].Length;
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
                            ID = $"dynTitle{item.QuestID}",
                            Text = $"{item.QuestID} . {item.QuestContent}",
                        };

                        string[] arrSelectItem = _QuestMgr.SplitSelectItem(item.SelectItem);
                        plhbookmark4.Controls.Add(dynLabel);
                        plhbookmark4.Controls.Add(lb);

                        //本題選項數量
                        int selectionCount = _QuestMgr.SplitSelectItem(item.SelectItem).Count();
                        for (int i = 0; i < selectionCount; i++)
                        {

                            Label dynSelection = new Label()
                            {
                                ID = $"Selection",
                                Text = $"{arrSelectItem[i]}",
                            };

                            int count = 0;
                            if (arrSelectItem[i] != null)
                                count = arrSelectItem[i].Length;
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
                            ID = $"dynTitle{item.QuestID}",
                            Text = $"{item.QuestID} . {item.QuestContent}",
                        };
                        Label content = new Label()
                        {
                            ID = "noneDisplay",
                            Text = "(-)",
                        };
                        Label lb = new Label() { ID = $"lb{item.QuestID}", Text = "<br>", }; //分行

                        plhbookmark4.Controls.Add(dynLabel);
                        plhbookmark4.Controls.Add(content);
                        plhbookmark4.Controls.Add(lb);
                        Label lb3 = new Label() { ID = $"lb{item.QuestID}_2", Text = "<br>", }; //分行
                        plhbookmark4.Controls.Add(lb3);
                    }
                }
            }
            #endregion

            //缺:帶入常用問題至TEXTBOX
        }

        //點擊分頁-問卷基本題目
        protected void bookmark1_Click(object sender, EventArgs e)
        {
            this.plhbookmark1.Visible = true;
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = false;
            this.plhbookmark4.Visible = false;
        }
        //點擊分頁-問題編輯
        protected void bookmark2_Click(object sender, EventArgs e)
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = true;
            this.plhbookmark3.Visible = false;
            this.plhbookmark4.Visible = false;
        }
        //點擊分頁-填寫資料
        protected void bookmark3_Click(object sender, EventArgs e)
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = true;
            this.plhbookmark4.Visible = false;
        }
        //點擊分頁-統計
        protected void bookmark4_Click(object sender, EventArgs e)
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = false;
            this.plhbookmark4.Visible = true;
        }
        //問卷基本資料送出按鈕
        protected void btnBasicSummit_Click(object sender, EventArgs e)
        {
            //將輸入值存入靜態Questionnaire，並存成Session
            basicQnir.QuestionnaireID = (int)HttpContext.Current.Session["QnirID"];
            basicQnir.Caption = this.textCaption.Text;
            basicQnir.QuestionnaireContent = this.textQuestionnaireContent.Text;
            basicQnir.StartDate = Convert.ToDateTime(this.textStartDate.Text);
            basicQnir.EndDate = Convert.ToDateTime(this.textEndDate.Text);
            basicQnir.VoidStatus = this.ChkBxVoidStatus.Checked;
            HttpContext.Current.Session["Qnir"] = basicQnir;
            //跳轉到編輯問題的部分
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = true;
            this.plhbookmark3.Visible = false;
        }

        //問卷基本資料取消按鈕，清除Session並回到問卷列表
        protected void btnBasicCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["QnirID"] = null;
            HttpContext.Current.Session["Qnir"] = null;
            this.Response.Redirect("/admin/MyQuestionnaire.aspx");
        }

        //編輯題目-按下加入按鈕後，存到Session
        protected void btnAddToQuest_Click(object sender, EventArgs e)
        {
            addQuestTime++;

            //將問題種類轉換成數字
            int intQuestForm = _QuestMgr.AnswerTextToInt(this.setQuestForm.Text);

            //若答案的種類沒有選項，禁止他輸入文字

            //將題目新增至List
            Question arrQuest = new Question()
            {
                QuestionnaireID = basicQnir.QuestionnaireID,
                QuestOrder = addQuestTime,
                QuestContent = this.setQuest.Text,
                AnswerForm = intQuestForm,
                Required = this.IsRequired.Checked
            };
            questSessionsList.Add(arrQuest);

            //將更新後的List存入Session
            HttpContext.Current.Session["setQuest"] = questSessionsList;

            //將編輯的題目繫節至表格
            this.RptrQuest.DataSource = questSessionsList;
            this.RptrQuest.DataBind();
            #region//作廢
            //addQuestTime++;
            //string strSetQuestFrom = "SetQuestFrom" + addQuestTime.ToString();
            //string strSetQuest = "SetQuest" + addQuestTime.ToString();
            //string strSetQuestType = "SetQuestType" + addQuestTime.ToString();
            //string strIsRequired = "IsRequired" + addQuestTime.ToString();
            //HttpContext.Current.Session[strSetQuestFrom] = this.setQuestFrom.Text;
            //HttpContext.Current.Session[strSetQuest] = this.setQuest.Text;
            //HttpContext.Current.Session[strSetQuestType] = this.setQuestType.Text;
            //HttpContext.Current.Session[strIsRequired] = this.IsRequired.Text;
            #endregion
        }
        //刪除題目按鈕
        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            //逐筆取得repeater中被勾選的資料列並刪除
            foreach (Control c in this.RptrQuest.Controls)
            {
                CheckBox cbx = (CheckBox)c.FindControl("chkBxQuest");
                TextBox tbx = (TextBox)c.FindControl("tbxTableName");

                if (cbx != null && cbx.Checked == true)
                {
                    //int QuestOrder = Int32.Parse(tbx.Text);
                    //questSessionsList.RemoveAt(QuestOrder);
                    //將刪除指定資料後的list排序重設，並存入Session
                    List<Question> newList = _QuestMgr.ReOrderQuestionList(questSessionsList);
                    HttpContext.Current.Session["setQuest"] = newList;
                }
            }
            Response.Redirect(Request.RawUrl);
        }
        //將問卷寫進資料庫
        protected void btnQuestSummit_Click(object sender, EventArgs e)
        {

            this._QtnirMgr.AddQuestionnaire(basicQnir, questSessionsList);
            HttpContext.Current.Session["setQuest"] = null;
            HttpContext.Current.Session["Qnir"] = null;
            this.Response.Redirect("/admin/MyQuestionnaire.aspx");
        }
        //取消問題
        protected void btnQuestCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["setQuest"] = null;
            HttpContext.Current.Session["Qnir"] = null;
            this.Response.Redirect("/admin/MyQuestionnaire.aspx");
        }
        //匯出CSV檔
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //缺:如果沒有資料的提示
            //缺:排除新增問卷的狀況
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID
            string strCSV = "";
            //填寫者的基本資料List
            List<BasicAnswer> allBasicAnswer = _AnswerMgr.GetDoneList(currentQnirID);
            //每筆回答
            List<WholeAnswer> answersToExport = _AnswerMgr.GetWholeDoneList(currentQnirID);
            WholeAnswer firstItem = answersToExport.FirstOrDefault();

            for (int i = 0; i < allBasicAnswer.Count; i++)
            {
                strCSV += $"{firstItem.Nickname},{firstItem.Phone},{firstItem.Email},{firstItem.Age}";
                int basicID = allBasicAnswer.First().BasicAnswerID;
                List<WholeAnswer> detailList = _AnswerMgr.GetTargetType(basicID);
                foreach (WholeAnswer item in detailList)
                {
                    strCSV += $"{item.QuestContent},{item.Answer},";
                    strCSV = strCSV.Remove(strCSV.LastIndexOf(","), 1);
                }
                strCSV += "\r\n";
            }

            File.WriteAllText("D:\\tryCSV.csv", strCSV.ToString());
            //缺:匯出完成提示
        }


    }
}