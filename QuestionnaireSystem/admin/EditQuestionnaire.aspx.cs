using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuestionnaireSystem.Manager;
using QuestionnaireSystem.ORM;

namespace QuestionnaireSystem.admin
{
    public partial class EditQuestionnaire : System.Web.UI.Page
    {
        private QuestionnaireManager _QstnirMgr = new QuestionnaireManager();    //問卷基本資料管理
        private CommonQuestManager _CommonMgr = new CommonQuestManager();    //常用問題管理
        private AnswerManager _AnswerMgr = new AnswerManager();    //回覆管理
        private QuestManager _QuestMgr = new QuestManager();    //問題管理
        private Statistic _statisMgr = new Statistic();    //統計管理
        private CheckInputManager _checksMgr = new CheckInputManager();    //統計管理
        private transWholeAnswerManager _transMgr = new transWholeAnswerManager();    //轉換WholeAnswer

        private static Questionnaire _basicQstnir = new Questionnaire();//問卷的基本資料
        private static List<WholeAnswer> _currentQuestList = new List<WholeAnswer>();//題目的List
        private static bool _isNewQstnir = false;
        private static int _currentQnirID = 0;

        //bug:刪除問題後回到此頁並顯示問題，並且順序號碼要重新整理、DB無法帶入(觀看細節)動態控制項、統計頁數據有誤、統計缺%數、
        //未做:提示、帶入常用問題、修改問題、分頁
        //待改善:頁籤事件改成enum
        protected void Page_Load(object sender, EventArgs e)
        {
            string updateOrder = this.Request.QueryString["UpdateOrder"];  //當前正在編輯的題目
            string currentBsicAnsID = this.Request.QueryString["BsicAnsID"];//當前查看的問卷細節
            string targetplh = this.Request.QueryString["Targetplh"];//要查看的頁籤
            if (!IsPostBack)
            {
                #region//_currentQnirID的值
                if (_currentQnirID == 0)
                {
                    if (HttpContext.Current.Session["QstnirID"] == null)//不是由列表按新增進入
                    {
                        if (this.Request.QueryString["QstnirID"] != null)
                        {
                            _currentQnirID = int.Parse(this.Request.QueryString["QstnirID"]);
                            HttpContext.Current.Session["IsNewQstnir"] = null;
                            HttpContext.Current.Session["QstnirID"] = null;
                        }
                        else
                        {
                            HttpContext.Current.Session["EditMsg"] = "請由問卷列表頁進入編輯頁面。";
                            this.Response.Redirect($"/admin/MyQuestionnaire.aspx");
                        }
                    }
                    else
                    {
                        _isNewQstnir = (bool)HttpContext.Current.Session["IsNewQstnir"];
                        _currentQnirID = (int)HttpContext.Current.Session["QstnirID"];
                        HttpContext.Current.Session["IsNewQstnir"] = null;
                        HttpContext.Current.Session["QstnirID"] = null;
                    }
                }
                else
                {
                    _isNewQstnir = false;
                    _currentQnirID = int.Parse(this.Request.QueryString["QstnirID"]);
                }
                #endregion

                #region//繳回的填寫列表
                if (!_isNewQstnir)
                {
                    List<BasicAnswer> doneList = _AnswerMgr.GetDoneList(_currentQnirID, _isNewQstnir);//既有問卷才帶入DB繳回問卷列表
                    this.RptrAnswerList.DataSource = doneList;
                    this.RptrAnswerList.DataBind();
                    if (doneList.Count == 0)
                        this.NoneDATA.Visible = true;
                }
                #endregion

                #region//繳回的問卷細節
                if (!_isNewQstnir && currentBsicAnsID != null)//不是新問卷且有指定哪一張回填，顯示填寫的細節
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
                    Label br1 = new Label() { ID = "br1", Text = "<br/>", }; //分行
                    plhDynDetail.Controls.Add(br1);

                    #region//繳回的問卷的細節的繫節
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
                if (!_isNewQstnir)
                {
                    //顯示問卷題目及填寫的內容 
                    this.plhbookmark4.Controls.Clear();     //清空統計的placeHolder

                    List<Question> dispQuestion = _QuestMgr.GetQuestionList(_currentQnirID, _isNewQstnir);//題目的資料

                    //取得統計的數量
                    Dictionary<string, int[]> allAmount = _statisMgr.getAllStatisticCount(_currentQnirID, _isNewQstnir);
                    if (allAmount.Count() == 0)
                    {
                        this.NAStatistic.Visible = true;
                    }
                    else
                    {
                        int questID = 0;
                        foreach (Question item in dispQuestion)
                        {
                            questID++;

                            string stringQuestID = questID.ToString();

                            int[] arrAllThisQuestAmount = allAmount[stringQuestID];//取出該題的票數陣列
                            int allThisQst = 0;
                            if (arrAllThisQuestAmount != null)
                            {
                                for (int i = 0; i < arrAllThisQuestAmount.Length; i++)
                                    allThisQst = allThisQst + arrAllThisQuestAmount[i];
                            }

                            //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                            if (item.AnswerForm == 5)
                            {

                                Label dynLabel = new Label()//標題
                                {
                                    ID = $"dynTitle{item.QuestOrder}",
                                    Text = $"{item.QuestOrder} . {item.QuestContent}",
                                };
                                plhbookmark4.Controls.Add(dynLabel);

                                _QuestMgr.SplitSelectItem(item.SelectItem, out int selectionCount, out string[] arrSelectItem);//本題選項數量,本題選項陣列
                                int sumVoid = 0;
                                if (arrAllThisQuestAmount != null)
                                {
                                    for (int i = 0; i < selectionCount; i++)//用來算同一題的票數總數
                                    {
                                        sumVoid = sumVoid + arrAllThisQuestAmount[i];
                                    }
                                }
                                for (int i = 0; i < selectionCount; i++)
                                {
                                    Label dynSelection = new Label()//選項文字
                                    {
                                        ID = $"Selection",
                                        Text = $"{arrSelectItem[i]}",
                                    };
                                    plhbookmark4.Controls.Add(dynSelection);
                                    Label lb2 = new Label() { ID = $"lb{item.QuestID}", Text = "<br>", }; //分行
                                    plhbookmark4.Controls.Add(lb2);



                                    float floPecentCount = (float)arrAllThisQuestAmount[i] / allThisQst;
                                    string strPecent = floPecentCount.ToString("P1");
                                    Label pecentCount = new Label()//百分比
                                    {
                                        ID = $"PecentCount",
                                        Text = $"{strPecent}",
                                    };
                                    plhbookmark4.Controls.Add(pecentCount);//百分比


                                    Label dynCount = new Label()//數量
                                    {
                                        ID = $"Count",
                                        Text = $"({arrAllThisQuestAmount[i]})",
                                    };
                                    plhbookmark4.Controls.Add(dynCount);//數量

                                    Label lb3 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br>", }; //分行
                                    plhbookmark4.Controls.Add(lb3);//分行

                                }
                                Label lb4 = new Label() { ID = $"lb{item.QuestID}_2", Text = "<br>", }; //分行
                                plhbookmark4.Controls.Add(lb4);
                            }
                            else if (item.AnswerForm == 6)
                            {

                                Label dynLabel = new Label()//標題
                                {
                                    ID = $"dynTitle{item.QuestOrder}",
                                    Text = $"{item.QuestOrder} . {item.QuestContent}",
                                };
                                plhbookmark4.Controls.Add(dynLabel);

                                _QuestMgr.SplitSelectItem(item.SelectItem, out int selectionCount, out string[] arrSelectItem);  //選項的數量,選項的陣列

                                int sumVoid = 0;
                                for (int i = 0; i < selectionCount; i++)//用來算同一題的票數總數
                                {
                                    int count = 0;
                                    if (arrSelectItem[i] != null)
                                        count = arrAllThisQuestAmount[i];
                                    sumVoid += count;
                                }

                                //本題選項數量
                                for (int i = 0; i < selectionCount; i++)
                                {

                                    Label dynSelection = new Label()//題目
                                    {
                                        ID = $"Selection",
                                        Text = $"{arrSelectItem[i]}",
                                    };
                                    plhbookmark4.Controls.Add(dynSelection);
                                    Label lb2 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br>", };//分行
                                    plhbookmark4.Controls.Add(lb2);


                                    int count = 0;
                                    if (arrSelectItem[i] != null)
                                        count = arrAllThisQuestAmount[i];//單題票數


                                    float floPecentCount = (float)count / allThisQst;
                                    string strPecent = floPecentCount.ToString("P1");
                                    Label pecentCount = new Label()//百分比
                                    {
                                        ID = $"PecentCount",
                                        Text = $"{strPecent}",
                                    };
                                    plhbookmark4.Controls.Add(pecentCount);//百分比


                                    Label dynCount = new Label()//票數
                                    {
                                        ID = $"Count",
                                        Text = $"({count})",
                                    };
                                    plhbookmark4.Controls.Add(dynCount);//票數
                                                                        //分行
                                    Label lb2_2 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br>", };
                                    plhbookmark4.Controls.Add(lb2_2);
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
                }
                #endregion

                #region//如果是既有問卷把舊資料帶入
                if (!_isNewQstnir)
                {
                    Questionnaire questionnaire = _QstnirMgr.GetQuestionnaire(_currentQnirID, _isNewQstnir);
                    this.textCaption.Text = questionnaire.Caption;
                    this.textQuestionnaireContent.Text = questionnaire.QuestionnaireContent;
                    this.textStartDate.Text = questionnaire.EndDate.ToString("yyyy-MM-dd");
                    this.textEndDate.Text = questionnaire.EndDate.ToString("yyyy-MM-dd");
                    this.ChkBxVoidStatus.Checked = questionnaire.VoidStatus;
                    _basicQstnir = questionnaire;

                    //第一次帶入DB
                    List<WholeAnswer> qstsWithIntAnswerForm = new List<WholeAnswer>();
                    qstsWithIntAnswerForm = _QuestMgr.GetWholeQuestionList(_currentQnirID, _isNewQstnir);
                    _currentQuestList = _transMgr.WholeToWholeList2(qstsWithIntAnswerForm);//將問題類型轉成字串
                }
                #endregion
            }


        }
        protected void Page_Prerender(object sender, EventArgs e)
        {
            //檢查:_currentQnirID能不能保留
            string updateOrder = this.Request.QueryString["UpdateOrder"];  //當前正在編輯的題目
            string currentBsicAnsID = this.Request.QueryString["BsicAnsID"];//當前查看的問卷細節
            string targetplh = this.Request.QueryString["Targetplh"];//要查看的頁籤


            if (updateOrder != null)
                HttpContext.Current.Session["UpdateOrder"] = updateOrder;


            #region//由URL判斷進入哪一個頁籤
            if (targetplh == "1")
                Changebookmark1();
            else if (targetplh == "2")
                Changebookmark2();
            else if (targetplh == "3")
                Changebookmark3();
            else if (targetplh == "4")
            {


                Changebookmark4();
            }
            #endregion

            //如果網址帶有UpdateOrder，就切換到更新問題頁籤
            if (updateOrder != null)
            {
                Changebookmark2();
                this.Updatequest(updateOrder);
            }



            //帶入問題列表
            if (HttpContext.Current.Session["CurrentQuests"] == null)//如果沒有儲存修改後的題目列表，直接帶入DB
            {
                this.RptrQuest.DataSource = _currentQuestList;
                this.RptrQuest.DataBind();
            }
            else
            {
                this.RptrQuest.DataSource = HttpContext.Current.Session["CurrentQuests"];//帶入Session的題目列表，
                this.RptrQuest.DataBind();
            }

            #region//如果有指定回覆的填寫，顯示繳回的問卷的細節
            if (currentBsicAnsID != null)
            {
                Changebookmark3();

                //顯示問卷題目及填寫的內容 
                this.plhDynDetail.Controls.Clear();     //rptDoneDetail:訪客填問卷的細節的繫節
                List<WholeAnswer> personWholeAnswers = _AnswerMgr.GetTargetType(currentBsicAnsID);//基本資料
                Label br2 = new Label() { ID = $"br", Text = "<br/>", }; //分行
                plhDynDetail.Controls.Add(br2);

                //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                foreach (WholeAnswer item in personWholeAnswers)
                {
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
            }
            #endregion

            //藉由預存session跳出視窗 的字串
            if (Session["EditMsg"] != null)
            {
                IGetable o = (IGetable)this.Master;
                var m = o.GetMsg();
                m.Value = Session["EditMsg"] as string;
                Session.Remove("EditMsg");
            }
        }

        //點擊分頁-問卷基本題目
        protected void bookmark1_Click(object sender, EventArgs e)
        {
            string updateOrder = this.Request.QueryString["UpdateOrder"];
            if (updateOrder == null)
                this.Response.Redirect($"/admin/EditQuestionnaire.aspx?&QstnirID={_currentQnirID}&Targetplh=1");
            else
                this.Response.Redirect($"/admin/EditQuestionnaire.aspx?QstnirID={_currentQnirID}&UpdateOrder={updateOrder}&Targetplh=1");//確認會不會進postback
        }
        //點擊分頁-問題編輯
        protected void bookmark2_Click(object sender, EventArgs e)
        {
            this.bookmark2.Enabled = false;
            this.Response.Redirect($"/admin/EditQuestionnaire.aspx?QstnirID={_currentQnirID}&Targetplh=2");
        }
        //點擊分頁-填寫資料
        protected void bookmark3_Click(object sender, EventArgs e)
        {
            string currentBsicAnsID = this.Request.QueryString["BsicAnsID"];//從URL取得當前查看的問卷細節
            if (_isNewQstnir)
                HttpContext.Current.Session["EditMsg"] = "新增的問卷沒有填寫過的資料。";

            //觀看填寫列表
            else if (!_isNewQstnir && currentBsicAnsID == null)
            {
                this.bookmark3.Enabled = false;
                this.Response.Redirect($"/admin/EditQuestionnaire.aspx?QstnirID={_currentQnirID}&Targetplh=3");
            }
        }
        //點擊分頁-統計
        protected void bookmark4_Click(object sender, EventArgs e)
        {
            if (_isNewQstnir)
                HttpContext.Current.Session["EditMsg"] = "新增的問卷沒有統計資料。";
            this.bookmark4.Enabled = false;
            this.Response.Redirect($"/admin/EditQuestionnaire.aspx?QstnirID={_currentQnirID}&Targetplh=4");
        }

        //問卷基本資料送出按鈕
        protected void btnBasicSummit_Click(object sender, EventArgs e)
        {

            if (this.textCaption.Text == "" || this.textQuestionnaireContent.Text == "" || this.textStartDate.Text == "" || this.textEndDate.Text == "")
                HttpContext.Current.Session["EditMsg"] = "此頁面皆為必填項目。";
            else
            {
                //將輸入值存入靜態Questionnaire，並存成Session
                _basicQstnir.Caption = this.textCaption.Text;
                _basicQstnir.QuestionnaireContent = this.textQuestionnaireContent.Text;
                _basicQstnir.StartDate = Convert.ToDateTime(this.textStartDate.Text);
                _basicQstnir.EndDate = Convert.ToDateTime(this.textEndDate.Text);
                _basicQstnir.VoidStatus = this.ChkBxVoidStatus.Checked;
                _basicQstnir.QuestionnaireID = _currentQnirID;
                _basicQstnir.BuildDate = DateTime.Now;
                HttpContext.Current.Session["EditMsg"] = "資料尚未儲存，請繼續編輯問題後送出。";
                this.Response.Redirect($"/admin/EditQuestionnaire.aspx?QstnirID={_currentQnirID}&Targetplh=2");

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
            string updateOrder = null;
            if (HttpContext.Current.Session["UpdateOrder"] != null)
                updateOrder = HttpContext.Current.Session["UpdateOrder"] as string;  //從URL取得當前正在編輯的題目
            HttpContext.Current.Session["UpdateOrder"] = null;

            #region//定義題目屬性
            string questContent = this.setQuest.Text;                               //問題描述
            int intQuestForm = int.Parse(this.setQuestForm.Text);                   //問題種類
            string strQuestForm = _QuestMgr.AnswerTextToInt(intQuestForm);          //問題種類
            bool required = this.IsRequired.Checked;                                //是否必填
            string selectItem = this.textSelectItem.Text;                           //選項
            #endregion

            #region//檢查輸入值
            if (questContent == null)//驗證是否有輸入問題
            {
                HttpContext.Current.Session["EditMsg"] = "請輸入問題。";
                return;
            }
            if (intQuestForm == 5 || intQuestForm == 6)//驗證選擇題是否有輸入選項
            {
                if (selectItem == null || _checksMgr.IsMistakeSemicolon(selectItem))
                {
                    HttpContext.Current.Session["EditMsg"] = "請設置單選及多選方塊的選項，(以半形;符號區分)。";
                    return;
                }
                if (intQuestForm == 5 && !required)//如果選擇題沒有輸入選項
                {
                    HttpContext.Current.Session["EditMsg"] = "單選題必須為必選。";
                    return;
                }
            }
            else
            {
                if (selectItem != "")
                {
                    HttpContext.Current.Session["EditMsg"] = "選擇題以外的題目不需輸入選項。";
                    return;
                }
            }
            #endregion

            //現在正在修改題目
            if (updateOrder != null)
            {
                WholeAnswer wholeUpdateQuest = new WholeAnswer()
                {
                    QuestionnaireID = _currentQnirID,
                    QuestOrder = int.Parse(updateOrder),
                    QuestContent = questContent,
                    AnswerForm = intQuestForm,
                    strAnswerForm = strQuestForm,
                    Required = required,
                    SelectItem = selectItem
                };

                _currentQuestList = _QuestMgr.UpdateQuest(_currentQuestList, wholeUpdateQuest);

                if (!_isNewQstnir)
                    this.Response.Redirect($"/admin/EditQuestionnaire.aspx/?Targetplh=2");//新增的問卷修改題目
                else
                    this.Response.Redirect($"/admin/EditQuestionnaire.aspx/?QstnirID={_currentQnirID}&Targetplh=2");//舊的問卷修改題目
            }
            else//新增題目
            {
                //將題目更新至List
                WholeAnswer arrWhole = new WholeAnswer()
                {
                    QuestionnaireID = _currentQnirID,
                    QuestOrder = _currentQuestList.Count + 1,
                    QuestContent = questContent,
                    AnswerForm = intQuestForm,
                    strAnswerForm = strQuestForm,
                    Required = required,
                    SelectItem = selectItem
                };

                _currentQuestList.Add(arrWhole);
                HttpContext.Current.Session["SessionCurrentQuests"] = _currentQuestList;//編輯中的問卷列表
            }
        }

        //bug:修改題目-帶入題目至編輯區
        private void Updatequest(string updateID)
        {
            WholeAnswer qst = _currentQuestList.Find(n => n.QuestOrder == int.Parse(updateID));
            this.setQuest.Text = qst.QuestContent;                    //問題描述
            this.setQuestForm.SelectedValue = qst.AnswerForm.ToString();   //問題種類
            this.IsRequired.Checked = qst.Required;                                //是否必填
            this.textSelectItem.Text = qst.SelectItem;                           //選項

            HttpContext.Current.Session["QuestOrder"] = qst.QuestOrder;
        }

        //刪除題目按鈕
        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            List<WholeAnswer> newList = new List<WholeAnswer>();

            //逐筆取得repeater中被勾選的資料列並刪除
            foreach (Control c in this.RptrQuest.Controls)
            {
                CheckBox cbx = (CheckBox)c.FindControl("A");
                Label tbx = (Label)c.FindControl("lblText");

                if (cbx != null && cbx.Checked == true)
                {
                    int QuestOrder = Int32.Parse(tbx.Text);
                    _currentQuestList.RemoveAt((int)QuestOrder - 1);
                    //將刪除指定資料後的list排序重設，並存入Session
                    newList = _QuestMgr.ReOrderQuestionList(_currentQuestList);
                }
            }
            HttpContext.Current.Session["SessionCurrentQuests"] = newList;
            _currentQuestList = newList;
        }

        //將問卷寫進資料庫
        protected void btnQuestSummit_Click(object sender, EventArgs e)
        {
            List<Question> saveQstList = _transMgr.WholeToQstList(_currentQuestList, _currentQnirID);//記憶體的題目列表
            if (_isNewQstnir)
                this._QstnirMgr.AddQuestionnaire(_basicQstnir, saveQstList);//存進DB
            else
                this._QstnirMgr.Updateuestionnaire(_basicQstnir, saveQstList);//存進DB
            HttpContext.Current.Session.Abandon();      //清除所有session並返回
            HttpContext.Current.Session["EditMsg"] = "問卷已更新。";
            this.Response.Redirect("/admin/MyQuestionnaire.aspx");
        }

        //取消問題(待看)
        protected void btnQuestCancel_Click(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QstnirID"] as string;

            HttpContext.Current.Session.Abandon();      //清除所有session並返回
            _basicQstnir = null;
            this.Response.Redirect($"/admin/EditQuestionnaire.aspx?QstnirID={currentQnirID}&Targetplh=2");
        }

        //匯出CSV檔
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QstnirID"] as string;
            string strCSV = "";
            //填寫者的基本資料List
            List<BasicAnswer> allBasicAnswer = _AnswerMgr.GetDoneList(currentQnirID);
            if (allBasicAnswer.Count == 0)
                HttpContext.Current.Session["EditMsg"] = "查無資料，無法匯出檔案。";
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
                File.WriteAllText("C:\\tryCSV.csv", strCSV, Encoding.UTF8);
                HttpContext.Current.Session["EditMsg"] = "預設路徑在C槽最外層，檔案匯出成功。(注意：不包含尚在變更中的問卷。)";
            }
        }

        //切換分頁的事件
        private void Changebookmark1()
        {
            this.plhbookmark1.Visible = true;
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = false;
            this.plhbookmark4.Visible = false;
            this.bookmark1.Enabled = false;
            this.bookmark2.Enabled = true;
            this.bookmark3.Enabled = true;
            this.bookmark4.Enabled = true;

        }
        //切換分頁的事件
        private void Changebookmark2()
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = true;
            this.plhbookmark3.Visible = false;
            this.plhbookmark4.Visible = false;
            this.bookmark1.Enabled = true;
            this.bookmark2.Enabled = false;
            this.bookmark3.Enabled = true;
            this.bookmark4.Enabled = true;

        }
        //切換分頁的事件
        private void Changebookmark3()
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = true;
            this.plhbookmark4.Visible = false;
            this.bookmark1.Enabled = true;
            this.bookmark2.Enabled = true;
            this.bookmark3.Enabled = false;
            this.bookmark4.Enabled = true;

        }
        //切換分頁的事件
        private void Changebookmark4()
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = false;
            this.plhbookmark4.Visible = true;
            this.bookmark1.Enabled = true;
            this.bookmark2.Enabled = true;
            this.bookmark3.Enabled = true;
            this.bookmark4.Enabled = false;
        }

    }
}