using QuestionnaireSystem.Manager;
using QuestionnaireSystem.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace QuestionnaireSystem
{
    public partial class FillOutPage1 : System.Web.UI.Page
    {
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //問卷基本資料管理
        private AnswerManager _AnswerMgr = new AnswerManager();    //回覆管理
        private QuestManager _QuestMgr = new QuestManager();    //問題管理

        private static BasicAnswer basicQnir = new BasicAnswer();//問卷的基本資料
        private static List<Answer> AnswerList = new List<Answer>();//題目的List
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID
            string QuestOrder = this.Request.QueryString["QuestOrder"];  //從URL取得當前所在的問卷ID
            string currentBsicAnsID = this.Request.QueryString["BsicAnsID"];//從URL取得當前查看的問卷細節
            string targetplh = this.Request.QueryString["Targetplh"];//從URL取得要查看的頁籤


            if (currentQnirID == null)
            {
                HttpContext.Current.Session["MainMsg"] = "請由列表進入欲作答問卷。";
                this.Response.Redirect($"/Index.aspx");
            }

            #region//顯示問卷題目及填寫的控制項
            List<WholeAnswer> wholeQuestionnaire = _QtnirMgr.GetWholeQuestioniar(currentQnirID);//每個題目

            this.Caption.Text = wholeQuestionnaire[0].Caption.ToString();
            this.QuestionnaireContent.Text = wholeQuestionnaire[0].QuestionnaireContent.ToString();

            foreach (WholeAnswer item in wholeQuestionnaire)
            {

                string strselectItem = item.SelectItem;
                int amount = 0;
                string[] splitArray = null;
                _QuestMgr.SplitSelectItem(strselectItem, out amount, out splitArray);
                if (item.AnswerForm != 5 && item.AnswerForm != 6)
                {
                    string titleText = "";
                    if (item.Required == true)
                    {
                        titleText = $"{item.QuestOrder} . {item.QuestContent} (必填)";
                    }
                    Label dynLabel = new Label()
                    {
                        ID = $"dynTitle{item.QuestOrder}",
                        Text = $"{item.QuestOrder} . {item.QuestContent}",

                    };
                    plhDynDetail.Controls.Add(dynLabel);

                    Label br3 = new Label() { ID = $"lb{item.QuestOrder}_br1", Text = "<br/>", }; //分行
                    plhDynDetail.Controls.Add(br3);

                    TextBox textbox = new TextBox()
                    {
                        ID = $"{item.QuestOrder}",
                    };
                    plhDynDetail.Controls.Add(textbox);

                    Label br4 = new Label() { ID = $"lb{item.QuestOrder}_br2", Text = "<br/>", }; //分行
                    plhDynDetail.Controls.Add(br4);

                }
                else
                {
                    //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                    if (item.AnswerForm == 5)
                    {
                        Label dynLabel = new Label()//標題
                        {
                            ID = $"dynTitle{item.QuestOrder}",
                            Text = $"{item.QuestOrder} . {item.QuestContent}",
                        };
                        plhDynDetail.Controls.Add(dynLabel);
                        Label br3 = new Label() { ID = $"br{item.QuestOrder}_br3", Text = "<br/>", }; //分行
                        plhDynDetail.Controls.Add(br3);

                        //單選選項
                        string[] arrSelection = null;
                        int selectionCount = 0;
                        _QuestMgr.SplitSelectItem(item.SelectItem, out selectionCount, out arrSelection);

                        //RadioButtonList
                        RadioButtonList RBList = new RadioButtonList() { ID = $"{item.QuestOrder}" };
                        //RBList.SelectedIndexChanged += RBList_SelectedIndexChanged;

                        for (int j = 0; j < selectionCount; j++)
                        {
                            ListItem RBlistItem = new ListItem();
                            RBlistItem.Text = $"{arrSelection[j]}";
                            RBList.Items.Add(RBlistItem);

                        }

                        RBList.Items[0].Selected = true;
                        Label br4 = new Label() { ID = $"br{item.QuestOrder}_4", Text = "<br/>", }; //分行
                        plhDynDetail.Controls.Add(RBList);
                        plhDynDetail.Controls.Add(br4);

                    }
                    else
                    {
                        string titleText = "";
                        if (item.Required == true)
                        {
                            titleText = $"{item.QuestOrder} . {item.QuestContent} (必填)";
                        }
                        Label dynLabel = new Label()//標題
                        {
                            ID = $"dynTitle{item.QuestOrder}",
                            Text = titleText,
                        };
                        plhDynDetail.Controls.Add(dynLabel);
                        Label br3 = new Label() { ID = $"br{item.QuestOrder}_5", Text = "<br/>", }; //分行
                        plhDynDetail.Controls.Add(br3);

                        //多選選項
                        string[] arrSelection = null;
                        int selectionCount = 0;
                        _QuestMgr.SplitSelectItem(item.SelectItem, out selectionCount, out arrSelection);

                        //RadioButtonList
                        CheckBoxList CBList = new CheckBoxList() { ID = $"{item.QuestOrder}" };
                        //CBList.SelectedIndexChanged += CBList_SelectedIndexChanged;

                        for (int j = 0; j < selectionCount; j++)
                        {
                            ListItem CBlistItem = new ListItem();
                            CBlistItem.Text = $"{arrSelection[j]}";
                            CBList.Items.Add(CBlistItem);
                        }
                        Label br4 = new Label() { ID = $"br{item.QuestOrder}_6", Text = "<br/>", }; //分行
                        plhDynDetail.Controls.Add(CBList);
                        plhDynDetail.Controls.Add(br4);
                    }


                }
                Label br5 = new Label() { ID = $"br{item.QuestOrder}_next", Text = "<br/>", }; //分行
                plhDynDetail.Controls.Add(br5);
            }
            #endregion

        }
        //取消
        protected void btnQuestCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            this.Response.Redirect("Index.aspx");
        }

        //切換填寫
        private void Changebookmark1()
        {
            this.doIt.Visible = true;
            this.CheckIt.Visible = false;
        }
        //切換確認
        private void Changebookmark2()
        {
            this.doIt.Visible = false;
            this.CheckIt.Visible = true;
        }


        //填完
        protected void btnQuestSummit_Click(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID

            if (currentQnirID == null || this.doneName.Text == null || this.donePhone.Text == null || this.doneEmail.Text == null || this.doneAge.Text == null)
            {
                HttpContext.Current.Session["MainMsg"] = "基本資料皆為必填項目。";
            }
            else
            {
                //基本問題
                basicQnir.QuestionnaireID = int.Parse(currentQnirID);
                basicQnir.Nickname = this.doneName.Text;
                basicQnir.Phone = this.donePhone.Text;
                basicQnir.Email = this.doneEmail.Text;
                string intAge = this.doneAge.Text;
                basicQnir.Age = int.Parse(intAge);
                int qstAmount = _QuestMgr.GetQuestionList(currentQnirID).Count();

                //確認是否該填的都有填
                List<Question> wholeQuestionnaire = _QuestMgr.GetRequiredQuest(currentQnirID);//每個必填的題目
                bool checkFilled = true;//該填的都有填
                foreach (Question item in wholeQuestionnaire)
                {
                    string crlrName = $"{item.QuestOrder}";
                    if (item.AnswerForm == 1)//如果題目是文字方塊
                    {
                        TextBox dynBox = plhDynDetail.FindControl(crlrName) as TextBox;
                        if (dynBox.Text == null || dynBox.Text.Trim() == "")
                        {
                            checkFilled = false;//漏填
                        }
                    }
                    else
                    {
                        CheckBoxList dynCB = plhDynDetail.FindControl(crlrName) as CheckBoxList;
                        int amount = dynCB.Items.Count;
                        int check = 0;//計算被選取數量
                        for (int i = 0; i < amount - 1; i++)
                        {
                            if (dynCB.Items[i].Selected == true)
                                check++;
                        }
                        if (check == 0)
                            checkFilled = false;//漏填
                    }
                }
                if (checkFilled == false)
                    HttpContext.Current.Session["MainMsg"] = "請確認必填項目皆有填寫。";
                else
                {
                    int count = 1;
                    foreach (Control item in plhDynDetail.Controls)
                    {
                        string crlrName = $"{count}";
                        if (plhDynDetail.FindControl(crlrName) as TextBox != null)
                        {
                            TextBox dynBox = plhDynDetail.FindControl(crlrName) as TextBox;
                            Answer a = new Answer()
                            {
                                BasicAnswerID = int.Parse(currentQnirID),
                                QuestID = count,
                                Answer1 = dynBox.Text,
                            };
                        }
                        else if (plhDynDetail.FindControl(crlrName) as RadioButtonList != null)
                        {
                            RadioButtonList dynBox = plhDynDetail.FindControl(crlrName) as RadioButtonList;
                            Answer a = new Answer()
                            {
                                BasicAnswerID = int.Parse(currentQnirID),
                                QuestID = count,
                                Answer1 = dynBox.Text,
                            };
                            AnswerList.Add(a);
                        }
                        else
                        {

                            CheckBoxList dynBox = plhDynDetail.FindControl(crlrName) as CheckBoxList;
                            string oValues = "";
                            int times = 1;
                            if (times > qstAmount)
                            {
                                foreach (ListItem Item in dynBox.Items)
                                {
                                    if (Item.Selected == true)
                                    {
                                        if (oValues.Length > 0)
                                        {
                                            oValues += ",";
                                        }
                                        oValues += Item.Value;
                                    }
                                    times++;
                                }
                            }
                            Answer a = new Answer()
                            {
                                BasicAnswerID = int.Parse(currentQnirID),
                                QuestID = count,
                                Answer1 = oValues,
                            };
                            AnswerList.Add(a);
                        }
                        count++;
                    }

                    _AnswerMgr.SaveBasicQnir(basicQnir);
                    _AnswerMgr.SaveAnswer(AnswerList);

                    HttpContext.Current.Session["EditQstnirMsg"] = "請確認填妥後送出(送出後不可修改)。";
                    Changebookmark2();
                }
            }
        }

        //確認頁：修改
        protected void goBack_Click(object sender, EventArgs e)
        {

            Changebookmark1();
        }
        //確認頁：送出
        protected void btnChkSummit_Click(object sender, EventArgs e)
        {

            _AnswerMgr.SaveBasicQnir(basicQnir);
            _AnswerMgr.SaveAnswer(AnswerList);

            HttpContext.Current.Session["EditQstnirMsg"] = "問卷已送出。";
            this.Response.Redirect("Index.aspx");
        }
    }
}

