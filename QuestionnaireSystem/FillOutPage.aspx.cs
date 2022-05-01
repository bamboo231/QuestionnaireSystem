using QuestionnaireSystem.admin;
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
    public partial class FillOutPage1 : System.Web.UI.Page
    {
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //問卷基本資料管理
        private AnswerManager _AnswerMgr = new AnswerManager();    //回覆管理
        private QuestManager _QuestMgr = new QuestManager();    //問題管理

        private static BasicAnswer basicQnir = new BasicAnswer();//問卷的基本資料
        private static List<Answer> AnswerList = new List<Answer>();//題目的List
        protected void Page_Init(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID

            #region//填寫問卷
            List<WholeAnswer> wholeQuestionnaire = _QtnirMgr.GetWholeQuestioniar(currentQnirID);//每個題目

            this.Caption.Text = wholeQuestionnaire[0].Caption.ToString();
            this.Caption2.Text = wholeQuestionnaire[0].Caption.ToString();
            this.QuestionnaireContent.Text = wholeQuestionnaire[0].QuestionnaireContent.ToString();
            this.QuestionnaireContent2.Text = wholeQuestionnaire[0].QuestionnaireContent.ToString();

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
                        ID = $"dynTitle1{item.QuestOrder}",
                        Text = $"{item.QuestOrder} . {item.QuestContent}",

                    };
                    plhDynDetail.Controls.Add(dynLabel);

                    Label br3 = new Label() { ID = $"lb{item.QuestOrder}_br1", Text = "<br/>", }; //分行
                    plhDynDetail.Controls.Add(br3);
                    string textboxTitle = $"textbox{ item.QuestOrder}";
                    TextBox textbox = new TextBox()
                    {
                        ID = textboxTitle,
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
                            ID = $"dynTitle5{item.QuestOrder}",
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
                        RadioButtonList RBList = new RadioButtonList() { ID = $"RB{item.QuestOrder}" };
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
                            ID = $"dynTitle6{item.QuestOrder}",
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
                        CheckBoxList CBList = new CheckBoxList() { ID = $"CB{item.QuestOrder}" };

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

        protected void Page_Load(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID

            if (!IsPostBack)
            {
                if (currentQnirID == null)
                {
                    HttpContext.Current.Session["MainMsg"] = "請由列表進入欲作答問卷。";
                    this.Response.Redirect($"/Index.aspx");
                }
            }



            if (AnswerList.Count == 0)
                Changebookmark1();
            else
                Changebookmark2();



            //藉由預存session跳出視窗 的字串
            if (Session["FillOutMsg"] != null)
            {
                FrontIGetable o = (FrontIGetable)this.Master;
                var m = o.GetMsg();
                m.Value = Session["FillOutMsg"] as string;
                Session.Remove("FillOutMsg");
            }


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

            if (currentQnirID == "" || this.doneName.Text == "" || this.donePhone.Text == "" || this.doneEmail.Text == "" || this.doneAge.Text == "")
            {
                HttpContext.Current.Session["FillOutMsg"] = "基本資料皆為必填項目。";
            }
            else
            {
                //基本問題
                basicQnir.QuestionnaireID = int.Parse(currentQnirID);
                basicQnir.Nickname = this.doneName.Text;
                basicQnir.Phone = this.donePhone.Text;
                basicQnir.Email = this.doneEmail.Text;
                int intAge = int.Parse(this.doneAge.Text);
                basicQnir.Age = intAge;
                int qstAmount = _QuestMgr.GetQuestionList(currentQnirID).Count();

                //確認是否該填的都有填
                List<Question> RequiredQuestionnaire = _QuestMgr.GetRequiredQuest(currentQnirID);//每個必填的題目
                bool checkFilled = true;//該填的都有填
                foreach (Question item in RequiredQuestionnaire)
                {
                    string crlrName = $"{item.QuestOrder}";
                    if (item.AnswerForm == 1)//如果題目是文字方塊
                    {
                        string textboxTitle = $"textbox{ crlrName}";
                        TextBox dynBox = plhDynDetail.FindControl(textboxTitle) as TextBox;
                        if (dynBox.Text == null || dynBox.Text.Trim() == "")
                        {
                            checkFilled = false;//漏填
                        }
                    }
                    else
                    {
                        CheckBoxList dynCB = plhDynDetail.FindControl($"CB{ crlrName}") as CheckBoxList;
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
                    HttpContext.Current.Session["FillOutMsg"] = "請確認必填項目皆有填寫。";
                else
                {
                    List<WholeAnswer> wholeQuestionnaire = _QtnirMgr.GetWholeQuestioniar(currentQnirID);

                    int count = 1;
                    foreach (Control item in plhDynDetail.Controls)
                    {
                        int theQuestID = AnswerList.Count();
                        string crlrName = $"{count}";
                        if (plhDynDetail.FindControl($"textbox{theQuestID + 1}") as TextBox != null)
                        {
                            TextBox dynBox = plhDynDetail.FindControl($"textbox{theQuestID + 1}") as TextBox;
                            Answer a = new Answer()
                            {
                                BasicAnswerID = int.Parse(currentQnirID),
                                QuestID = wholeQuestionnaire[theQuestID].QuestID,
                                Answer1 = dynBox.Text,
                            };
                            AnswerList.Add(a);
                        }
                        else if (plhDynDetail.FindControl($"RB{ theQuestID + 1}") as RadioButtonList != null)
                        {
                            RadioButtonList dynBox = plhDynDetail.FindControl($"RB{ theQuestID + 1}") as RadioButtonList;
                            Answer a = new Answer()
                            {
                                BasicAnswerID = int.Parse(currentQnirID),
                                QuestID = wholeQuestionnaire[theQuestID].QuestID,
                                Answer1 = dynBox.Text,
                            };
                            AnswerList.Add(a);
                        }
                        else if (plhDynDetail.FindControl($"CB{theQuestID + 1}") as CheckBoxList != null)
                        {

                            CheckBoxList dynBox = plhDynDetail.FindControl($"CB{ theQuestID + 1}") as CheckBoxList;
                            int amount = dynBox.Items.Count;
                            string oValues = "";
                            int times = 1;
                            if (times < amount)
                            {
                                foreach (ListItem Item in dynBox.Items)
                                {
                                    if (Item.Selected == true)
                                    {
                                        if (oValues.Length > 0)
                                        {
                                            oValues += ";";
                                        }
                                        oValues += Item.Value;
                                    }
                                    times++;
                                }
                            }
                            Answer a = new Answer()
                            {
                                BasicAnswerID = int.Parse(currentQnirID),
                                QuestID = wholeQuestionnaire[theQuestID].QuestID,
                                Answer1 = oValues,
                            };
                            AnswerList.Add(a);
                        }
                        count++;
                    }

                    HttpContext.Current.Session["FillOutMsg"] = "請確認填妥後送出(送出後不可修改)。";
                }



                #region//顯示確認頁
                List<WholeAnswer> wholeQuestionnaire2 = _QtnirMgr.GetWholeQuestioniar(currentQnirID);//每個題目

                this.Caption.Text = wholeQuestionnaire2[0].Caption.ToString();
                this.QuestionnaireContent.Text = wholeQuestionnaire2[0].QuestionnaireContent.ToString();

                for (int i = 0; i < AnswerList.Count; i++)
                {

                    string strselectItem = wholeQuestionnaire2[i].SelectItem;
                    int amount = 0;
                    string[] splitArray = null;
                    _QuestMgr.SplitSelectItem(strselectItem, out amount, out splitArray);
                    if (wholeQuestionnaire2[i].AnswerForm != 5 && wholeQuestionnaire2[i].AnswerForm != 6)
                    {
                        string titleText = "";
                        if (wholeQuestionnaire2[i].Required == true)
                        {
                            titleText = $"{i+1} . {wholeQuestionnaire2[i].QuestContent} (必填)";
                        }
                        Label dynLabel = new Label()
                        {
                            ID = $"dynTitle{i}",
                            Text = titleText,
                        };
                        chkDynDetail.Controls.Add(dynLabel);

                        Label br3 = new Label() { ID = $"lb{i}_br1", Text = "<br/>", }; //分行
                        chkDynDetail.Controls.Add(br3);

                        Label textbox = new Label()
                        {
                            ID = $"{i}",
                            Text = AnswerList[i].Answer1.ToString()
                        };
                        chkDynDetail.Controls.Add(textbox);

                        Label br4 = new Label() { ID = $"lb{i}_br2", Text = "<br/>", }; //分行
                        chkDynDetail.Controls.Add(br4);

                    }
                    else
                    {
                        //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                        if (wholeQuestionnaire2[i].AnswerForm == 5)
                        {
                            Label dynLabel = new Label()//標題
                            {
                                ID = $"dynTitle5{i}",
                                Text = $"{i} . {wholeQuestionnaire2[i].QuestContent}",
                            };
                            chkDynDetail.Controls.Add(dynLabel);
                            Label br3 = new Label() { ID = $"br{i}_br3", Text = "<br/>", }; //分行
                            chkDynDetail.Controls.Add(br3);

                            //單選選項
                            Label RBList = new Label()
                            {
                                ID = $"{i}",
                                Text = AnswerList[i].Answer1
                            };
                            chkDynDetail.Controls.Add(RBList);

                            Label br4 = new Label() { ID = $"br{i}_4", Text = "<br/>", }; //分行
                            chkDynDetail.Controls.Add(br4);

                        }
                        else
                        {
                            //標題
                            string titleText = "";
                            if (wholeQuestionnaire2[i].Required == true)
                            {
                                titleText = $"{i} . {wholeQuestionnaire2[i].QuestContent} (必填)";
                            }
                            Label dynLabel = new Label()
                            {
                                ID = $"dynTitle6{i}",
                                Text = titleText,
                            };
                            chkDynDetail.Controls.Add(dynLabel);

                            //分行
                            Label br3 = new Label() { ID = $"br{i}_5", Text = "<br/>", };
                            chkDynDetail.Controls.Add(br3);

                            //多選選項
                            _QuestMgr.SplitSelectItem(AnswerList[i].Answer1, out int selectionCount, out string[] arrSelection);
                            string answers = "";
                            for (int j = 0; j < selectionCount; j++)
                            {
                                answers += $"{arrSelection[j]}";
                            }
                            Label CBList = new Label()
                            {
                                ID = $"{i}",
                                Text = answers,
                            };
                            chkDynDetail.Controls.Add(CBList);

                            //分行
                            Label br4 = new Label() { ID = $"br{i}_6", Text = "<br/>", };
                            chkDynDetail.Controls.Add(br4);
                        }

                    }
                    //每題的結尾分行
                    Label br5 = new Label() { ID = $"br{i}_next", Text = "<br/>", };
                    chkDynDetail.Controls.Add(br5);

                }
                #endregion



                this.chkName.Text = basicQnir.Nickname;
                this.chkPhone.Text = basicQnir.Phone;
                this.chkEmail.Text = basicQnir.Email;
                this.chkAge.Text = basicQnir.Age.ToString();



                Changebookmark2();
            }
        }


        //確認頁：修改
        protected void btnGoBack_Click(object sender, EventArgs e)
        {

            Changebookmark1();
        }
        //確認頁：送出
        protected void btnChkSummit_Click(object sender, EventArgs e)
        {

            _AnswerMgr.SaveBasicQnir(basicQnir);
            _AnswerMgr.SaveAnswer(AnswerList);

            HttpContext.Current.Session["FillOutMsg"] = "問卷已送出。";
            this.Response.Redirect("Index.aspx");
        }

    }
}

