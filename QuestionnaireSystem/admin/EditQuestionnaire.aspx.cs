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
        private CheckInputManager _checksMgr = new CheckInputManager();    //統計管理

        private static Questionnaire basicQnir = new Questionnaire();//問卷的基本資料
        private static List<Question> questSessionsList = new List<Question>();//題目的List


        //bug:刪除問題後回到此頁並顯示問題，並且順序號碼要重新整理、DB無法帶入(觀看細節)動態控制項、統計頁數據有誤、統計缺%數、
        //未做:提示、帶入常用問題、修改問題、分頁
        //待改善:頁籤事件改成enum
        //待問:

        protected void Page_Load(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID
            string QuestOrder = this.Request.QueryString["QuestOrder"];  //從URL取得當前所在的問卷ID
            string currentBsicAnsID = this.Request.QueryString["BsicAnsID"];//從URL取得當前查看的問卷細節
            string targetplh = this.Request.QueryString["Targetplh"];//從URL取得要查看的頁籤

            if (!IsPostBack)
            {

                #region//繳回的問卷列表
                if (currentQnirID != null)
                {
                    List<BasicAnswer> doneList = _AnswerMgr.GetDoneList(currentQnirID);//既有問卷才帶入DB繳回問卷列表

                    this.RptrAnswerList.DataSource = doneList;
                    this.RptrAnswerList.DataBind();
                }
                #endregion

                #region//常用問題下拉選單
                List<CommonQuest> CommonList = _CommonMgr.GetCommonQuestList();
                foreach (CommonQuest quest in CommonList)
                {
                    this.setQuestType.Items.Add(quest.QuestContent);
                }
                #endregion

                #region//繳回的問卷細節
                if (currentQnirID != null && currentBsicAnsID != null)
                {
                    this.plhdoneList.Visible = false;
                    this.plhDoneDetail.Visible = true;

                    BasicAnswer doneData = _AnswerMgr.GetDoneData(currentBsicAnsID);
                    this.doneName.Text = doneData.Nickname;
                    this.donePhone.Text = doneData.Phone;
                    this.doneEmail.Text = doneData.Email;
                    this.doneAge.Text = doneData.Age.ToString();
                    this.AnswerDate.Text = doneData.AnswerDate.ToString();
                    Label br0 = new Label() { ID = "br0", Text = "<br/>", }; //分行
                    plhDynDetail.Controls.Add(br0);
                    Label br1= new Label() { ID = "br1", Text = "<br/>", }; //分行
                    plhDynDetail.Controls.Add(br1);

                    #region//繳回的問卷的細節
                    //顯示問卷題目及填寫的內容 
                    this.plhDynDetail.Controls.Clear();     //rptDoneDetail:訪客填問卷的細節的繫節
                    List<WholeAnswer> personWholeAnswers = _AnswerMgr.GetTargetType(currentBsicAnsID);//基本資料
                    Label br2 = new Label() { ID = $"br", Text = "<br/>", }; //分行
                    plhDynDetail.Controls.Add(br2);

                    foreach (WholeAnswer item in personWholeAnswers)
                    {
                        //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                        if (item.AnswerForm == 5)
                        {
                            Label dynLabel = new Label()//標題
                            {
                                ID = $"dynTitle{item.QuestOrder}",
                                Text = $"{item.QuestOrder} . {item.QuestContent}",
                            };
                            Label br3 = new Label() { ID = $"lb{item.QuestOrder}", Text = "<br/>", }; //分行
                            plhDynDetail.Controls.Add(dynLabel);
                            plhDynDetail.Controls.Add(br3);

                            //單選選項
                            string[] arrSelection = null;
                            int selectionCount = 0;
                            _QuestMgr.SplitSelectItem(item.SelectItem, out selectionCount, out arrSelection);
                            for (int i = 0; i < selectionCount; i++)
                            {

                                WholeAnswer ans = personWholeAnswers.Where(a => a.Answer == arrSelection[i]).FirstOrDefault();
                                bool chk = false;
                                if (ans != null)
                                    chk = true;

                                RadioButton RB = new RadioButton()
                                {
                                    ID = "RB",
                                    Text = $"{arrSelection[i]}",
                                    Checked = chk,
                                    Enabled = false
                                };
                                Label br4 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br/>", }; //分行
                                plhDynDetail.Controls.Add(RB);
                                plhDynDetail.Controls.Add(br4);
                            }
                        }
                        else if (item.AnswerForm == 6)
                        {
                            Label dynLabel = new Label()//標題
                            {
                                ID = $"dynTitle{item.QuestOrder}",
                                Text = $"{item.QuestOrder} . {item.QuestContent}",
                            };
                            Label br3 = new Label() { ID = $"lb{item.QuestOrder}", Text = "<br/>", }; //分行
                            plhDynDetail.Controls.Add(dynLabel);
                            plhDynDetail.Controls.Add(br3);

                            //選項切割字串，並動態輸入選項
                            string[] arrSelection = null;
                            int selectionCount = 0;
                            _QuestMgr.SplitSelectItem(item.SelectItem, out selectionCount, out arrSelection);
                            for (int i = 0; i < selectionCount; i++)
                            {
                                WholeAnswer ans = personWholeAnswers.Where(a => a.Answer == arrSelection[i]).FirstOrDefault();
                                bool chk = false;
                                if (ans != null)
                                    chk = true;

                                CheckBox CB = new CheckBox()
                                {
                                    ID = "CB",
                                    Text = $"{arrSelection[i]}",
                                    Checked = chk,
                                    Enabled = false
                                };
                                plhDynDetail.Controls.Add(CB);
                            }
                        }
                        else
                        {
                            Label dynLabel = new Label()
                            {
                                ID = $"dynTitle{item.QuestOrder}",
                                Text = $"{item.QuestOrder} . {item.QuestContent}",
                            };
                            Label br3 = new Label() { ID = $"lb{item.QuestID}_br3", Text = "<br/>", }; //分行
                            plhDynDetail.Controls.Add(dynLabel);
                            plhDynDetail.Controls.Add(br3);

                            TextBox textbox = new TextBox()
                            {
                                ID = $"dynAnswer{item.QuestOrder}",
                                Text = $"{item.Answer}",
                                Enabled = false
                            };
                            plhDynDetail.Controls.Add(textbox);

                            Label br4 = new Label() { ID = $"lb{item.QuestID}_br4", Text = "<br/>", }; //分行
                            plhDynDetail.Controls.Add(br4);

                        }
                        Label br5 = new Label() { ID = $"lb{item.QuestOrder}_next", Text = "<br/>", }; //分行
                        plhDynDetail.Controls.Add(br5);
                    }
                    #endregion

                }
                #endregion

                #region//後台統計頁
                if (currentQnirID != null)
                {
                    //顯示問卷題目及填寫的內容 
                    this.plhbookmark4.Controls.Clear();     //清空統計的placeHolder
                                                            //題目的資料
                    List<Question> dispQuestion = _QuestMgr.GetQuestionList(currentQnirID);

                    //取得統計的數量
                    Dictionary<string, int[]> allAmount = _statisMgr.getAllStatisticCount(currentQnirID);//空的統計
                    int questID = 0;
                    foreach (Question item in dispQuestion)
                    {
                        questID++;

                        string stringQuestID = questID.ToString();

                        int[] arrAllThisQuestAmount = allAmount[stringQuestID];

                        //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                        if (item.AnswerForm == 5)
                        {
                            //+++



                            //+++
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
                                if (arrSelectItem[i] != null || arrAllThisQuestAmount!= null)
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

            #region//如果是既有問卷把舊資料帶入
            if (this.Request.QueryString["QnirID"] != null)
            {
                Questionnaire questionnaire = _QtnirMgr.GetQuestionnaire(currentQnirID);
                this.textCaption.Text = questionnaire.Caption;
                this.textQuestionnaireContent.Text = questionnaire.QuestionnaireContent;
                this.textStartDate.Text = questionnaire.EndDate.ToString("yyyy-MM-dd");
                this.textEndDate.Text = questionnaire.EndDate.ToString("yyyy-MM-dd");
                this.ChkBxVoidStatus.Checked = questionnaire.VoidStatus;

                questSessionsList = _QuestMgr.GetQuestionList(currentQnirID);
                this.RptrQuest.DataSource = questSessionsList;
                this.RptrQuest.DataBind();
            }
            else
            {
                this.RptrQuest.DataSource = questSessionsList;
                this.RptrQuest.DataBind();
            }
            #endregion
        }
        protected void Page_Prerender(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID
            string QuestOrder = this.Request.QueryString["updateOrder"];  //從URL取得當前所選的題目
            string currentBsicAnsID = this.Request.QueryString["BsicAnsID"];//從URL取得當前查看的問卷細節
            string targetplh = HttpContext.Current.Session["Targetplh"] as string; //要跳轉的頁籤
            if (targetplh == null)
            {
                targetplh = this.Request.QueryString["Targetplh"];//從URL取得要查看的頁籤
                HttpContext.Current.Session["Targetplh"] = null;
            }

            //進入哪一個頁籤
            if (targetplh == "2")
            {
                HttpContext.Current.Session["Targetplh"] = null;
                Changebookmark2();
            }
            else if (targetplh == "3")
            {
                HttpContext.Current.Session["Targetplh"] = null;
                Changebookmark3();
            }
            else if (targetplh == "4")
            {
                HttpContext.Current.Session["Targetplh"] = null;
                Changebookmark4();
            }
            //切換到更新問題頁籤
            if (QuestOrder != null)
            {
                Changebookmark2();  //切換頁籤
                updatequest(currentQnirID, QuestOrder);
            }
            //藉由預存session跳出視窗 的字串
            if (Session["EditQstnirMsg"] != null)
            {
                IGetable o = (IGetable)this.Master;
                var m = o.GetMsg();
                m.Value = Session["EditQstnirMsg"] as string;
                Session.Remove("EditQstnirMsg");
            }


            //缺:帶入常用問題至TEXTBOX
        }

        //點擊分頁-問卷基本題目
        protected void bookmark1_Click(object sender, EventArgs e)
        {
            string QnirID = this.Request.QueryString["QnirID"];
            string updateOrder = this.Request.QueryString["updateOrder"];
            if (updateOrder == null)
                this.Response.Redirect($"/admin/EditQuestionnaire.aspx?&QnirID={QnirID}&Targetplh=1");
            else
                this.Response.Redirect($"/admin/EditQuestionnaire.aspx?QnirID={QnirID}&updateOrder={updateOrder}&Targetplh=1");
            Changebookmark1();
        }
        //點擊分頁-問題編輯
        protected void bookmark2_Click(object sender, EventArgs e)
        {
            Changebookmark2();
        }
        //點擊分頁-填寫資料
        protected void bookmark3_Click(object sender, EventArgs e)
        {
            Changebookmark3();
        }
        //點擊分頁-統計
        protected void bookmark4_Click(object sender, EventArgs e)
        {
            Changebookmark4();
        }
        //問卷基本資料送出按鈕
        protected void btnBasicSummit_Click(object sender, EventArgs e)
        {
            //防止用其他方式進入本頁面
            if (HttpContext.Current.Session["QnirID"] == null)
            {
                HttpContext.Current.Session["EditQstnirMsg"] = "編輯問卷請由列表頁進入，點擊確認後將自動跳轉回問卷列表。";
                this.Response.Redirect("/admin/MyQuestionnaire.aspx");
            }
            else
            {

                if (this.textCaption.Text == null || this.textQuestionnaireContent.Text == null || this.textStartDate.Text == null || this.textEndDate.Text == null)
                {
                    HttpContext.Current.Session["EditQstnirMsg"] = "此頁面皆為必填項目。";
                }
                else
                {
                    //將輸入值存入靜態Questionnaire，並存成Session
                    basicQnir.QuestionnaireID = int.Parse(this.Request.QueryString["QnirID"]);
                    basicQnir.Caption = this.textCaption.Text;
                    basicQnir.QuestionnaireContent = this.textQuestionnaireContent.Text;
                    basicQnir.StartDate = Convert.ToDateTime(this.textStartDate.Text);
                    basicQnir.EndDate = Convert.ToDateTime(this.textEndDate.Text);
                    basicQnir.VoidStatus = this.ChkBxVoidStatus.Checked;
                    HttpContext.Current.Session["EditQstnirMsg"] = "資料尚未儲存，請繼續編輯問題後送出。";
                    HttpContext.Current.Session["Targetplh"] = "2";
                }
            }
        }

        //問卷基本資料取消按鈕，清除Session並回到問卷列表
        protected void btnBasicCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Abandon();      //清除所有session並返回
            this.Response.Redirect("/admin/MyQuestionnaire.aspx");
        }

        //新增題目-按下加入按鈕後，存到Session
        protected void btnAddToQuest_Click(object sender, EventArgs e)
        {
            string UpdateQuestOrder = this.Request.QueryString["QuestOrder"];
            if (UpdateQuestOrder == null)//新增的問題
            {

                if (this.setQuest.Text == null)
                {
                    HttpContext.Current.Session["EditQstnirMsg"] = "請輸入問題。";
                }
                else
                {
                    int QstnirID = basicQnir.QuestionnaireID;                               //問卷ID
                    string QuestContent = this.setQuest.Text;                               //問題描述
                    int intQuestForm = _QuestMgr.AnswerTextToInt(this.setQuestForm.Text);   //問題種類
                    bool Required = this.IsRequired.Checked;                                //是否必填
                    string SelectItem = this.textSelectItem.Text;                           //選項


                    if (intQuestForm == 5 || intQuestForm == 6)
                    {
                        if (SelectItem == "" || _checksMgr.IsMistakeSemicolon(SelectItem))
                            HttpContext.Current.Session["EditQstnirMsg"] = "請設置單選及多選方塊的選項，(以半形;符號區分)。";
                    }
                    else
                    {

                        //將題目新增至List
                        Question arrQuest = new Question()
                        {
                            QuestionnaireID = QstnirID,
                            QuestOrder = questSessionsList.Count + 1,
                            QuestContent = QuestContent,
                            AnswerForm = intQuestForm,
                            Required = Required,
                            SelectItem = SelectItem
                        };
                        questSessionsList.Add(arrQuest);


                    }
                }
            }
            else
            {

                int QstnirID = int.Parse(this.Request.QueryString["QnirID"]);                               //問卷ID
                string QuestContent = this.setQuest.Text;                               //問題描述
                int intQuestForm = _QuestMgr.AnswerTextToInt(this.setQuestForm.Text);   //問題種類
                bool Required = this.IsRequired.Checked;                                //是否必填
                string SelectItem = this.textSelectItem.Text;                           //選項

                if (this.setQuest.Text == null)
                {
                    HttpContext.Current.Session["EditQstnirMsg"] = "請輸入問題。";
                }
                else
                {
                    if (intQuestForm == 5 || intQuestForm == 6)
                    {
                        if (SelectItem == null || _checksMgr.IsMistakeSemicolon(SelectItem))
                            HttpContext.Current.Session["EditQstnirMsg"] = "請設置單選及多選方塊的選項，(以半形;符號區分)。";
                    }
                    else
                    {

                        //將題目更新至List
                        Question arrQuest = new Question()
                        {
                            QuestionnaireID = QstnirID,
                            QuestOrder = questSessionsList.Count + 1,
                            QuestContent = QuestContent,
                            AnswerForm = intQuestForm,
                            Required = Required,
                            SelectItem = SelectItem
                        };
                        questSessionsList.Remove(arrQuest);
                        questSessionsList.Add(arrQuest);

                        //將更新後的List存入Session
                        HttpContext.Current.Session["setQuest"] = questSessionsList;

                    }
                }
            }
        }

        //bug:修改題目-帶入題目至編輯區
        private void updatequest(string currentQnirID, string updateID)
        {
            Question qst = questSessionsList.Find(n => n.QuestOrder == int.Parse(updateID));
            this.setQuest.Text = qst.QuestContent;                    //問題描述
            this.setQuestForm.Text = _QuestMgr.AnswerTextToInt(qst.AnswerForm);   //問題種類
            this.IsRequired.Checked = qst.Required;                                //是否必填
            this.textSelectItem.Text = qst.SelectItem;                           //選項
        }

        //刪除題目按鈕
        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            //逐筆取得repeater中被勾選的資料列並刪除
            foreach (Control c in this.RptrQuest.Controls)
            {
                CheckBox cbx = (CheckBox)c.FindControl("A");
                TextBox tbx = (TextBox)c.FindControl("tbxTableName");

                if (cbx != null && cbx.Checked == true)
                {
                    int QuestOrder = Int32.Parse(tbx.Text);
                    questSessionsList.RemoveAt(QuestOrder);
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

            this._QtnirMgr.Updateuestionnaire(basicQnir, questSessionsList);
            HttpContext.Current.Session["setQuest"] = null;
            HttpContext.Current.Session["Qnir"] = null;
            HttpContext.Current.Session["EditQstnirMsg"] = "問卷已更新。";
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
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID
            string strCSV = "";
            //填寫者的基本資料List
            List<BasicAnswer> allBasicAnswer = _AnswerMgr.GetDoneList(currentQnirID);
            if (allBasicAnswer.Count == 0)
            {
                HttpContext.Current.Session["EditQstnirMsg"] = "查無資料，無法匯出檔案。";
            }
            else
            {
                //每筆回答
                List<WholeAnswer> answersToExport = _AnswerMgr.GetWholeDoneList(currentQnirID);
                WholeAnswer firstItem = answersToExport.FirstOrDefault();

                for (int i = 0; i < allBasicAnswer.Count; i++)
                {
                    strCSV += $"{firstItem.Nickname},{firstItem.Phone},{firstItem.Email},{firstItem.Age}";
                    int basicID = allBasicAnswer.FirstOrDefault().BasicAnswerID;
                    List<WholeAnswer> detailList = _AnswerMgr.GetTargetType(basicID);
                    foreach (WholeAnswer item in detailList)
                    {
                        strCSV += $"{item.QuestContent},{item.Answer},";
                        strCSV = strCSV.Remove(strCSV.LastIndexOf(","), 1);
                    }
                    strCSV += "\r\n";
                }

                File.WriteAllText("C:\\tryCSV.csv", strCSV.ToString());
                HttpContext.Current.Session["EditQstnirMsg"] = "預設路徑在C槽最外層，檔案匯出成功。(注意：不包含尚在變更中的問卷。)";
            }
        }

        //切換分頁的事件
        private void Changebookmark1()
        {
            this.plhbookmark1.Visible = true;
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = false;
            this.plhbookmark4.Visible = false;
        }
        //切換分頁的事件
        private void Changebookmark2()
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = true;
            this.plhbookmark3.Visible = false;
            this.plhbookmark4.Visible = false;
        }
        //切換分頁的事件
        private void Changebookmark3()
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = true;
            this.plhbookmark4.Visible = false;
        }
        //切換分頁的事件
        private void Changebookmark4()
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = false;
            this.plhbookmark4.Visible = true;
        }
    }
}