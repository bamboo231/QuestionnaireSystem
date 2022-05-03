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
        private static List<Answer> AnswerList = new List<Answer>();//答案的List
        protected void Page_Init(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID

            if (currentQnirID == null)
            {
                HttpContext.Current.Session["MainMsg"] = "請由列表進入欲作答問卷。";
                this.Response.Redirect($"/Index.aspx");
            }

            #region//問卷
            List<WholeAnswer> wholeQuestionnaire = _QtnirMgr.GetWholeQuestioniar(currentQnirID);//每個題目

            this.Caption.Text = wholeQuestionnaire[0].Caption.ToString();
            this.Caption2.Text = wholeQuestionnaire[0].Caption.ToString();
            this.QuestionnaireContent.Text = wholeQuestionnaire[0].QuestionnaireContent.ToString();
            this.QuestionnaireContent2.Text = wholeQuestionnaire[0].QuestionnaireContent.ToString();

            foreach (WholeAnswer item in wholeQuestionnaire)
            {
                int qstNumber = item.QuestOrder;//題號
                string strselectItem = item.SelectItem;//問題選項
                _QuestMgr.SplitSelectItem(strselectItem, out int amount, out string[] splitArray);//amount:題數;splitArray:問題陣列
                if (item.AnswerForm == 1)
                {
                    string titleText = "";//問題內容

                    //判斷是否為必填
                    if (item.Required == true)
                        titleText = $"{qstNumber} . {item.QuestContent} (必填)";
                    else
                        titleText = $"{qstNumber} . {item.QuestContent}";

                    Label dynLabel = new Label()//標題
                    {
                        ID = $"dynTitle1_{qstNumber}",
                        Text = titleText,

                    };
                    plhDynDetail.Controls.Add(dynLabel);

                    //分行
                    Label br3 = new Label() { ID = $"lb{qstNumber}_br1", Text = "<br/>", };
                    plhDynDetail.Controls.Add(br3);

                    TextBox textbox = new TextBox()//文字方塊
                    {
                        ID = $"textbox{ qstNumber}",
                    };
                    plhDynDetail.Controls.Add(textbox);

                    //分行
                    Label br4 = new Label() { ID = $"lb{qstNumber}_br2", Text = "<br/>", };
                    plhDynDetail.Controls.Add(br4);
                }
                else if (item.AnswerForm == 2)
                {
                    string titleText = "";//問題內容

                    //判斷是否為必填
                    if (item.Required == true)
                        titleText = $"{qstNumber} . {item.QuestContent} (必填)";
                    else
                        titleText = $"{qstNumber} . {item.QuestContent}";

                    Label dynLabel = new Label()//標題
                    {
                        ID = $"dynTitle2_{qstNumber}",
                        Text = titleText,

                    };
                    plhDynDetail.Controls.Add(dynLabel);

                    //分行
                    Label br3 = new Label() { ID = $"lb{qstNumber}_br1", Text = "<br/>", };
                    plhDynDetail.Controls.Add(br3);

                    TextBox textbox = new TextBox()//文字方塊(數字)
                    {
                        ID = $"textbox{ qstNumber}",
                        TextMode = TextBoxMode.Number,
                    };
                    plhDynDetail.Controls.Add(textbox);

                    //分行
                    Label br4 = new Label() { ID = $"lb{qstNumber}_br2", Text = "<br/>", };
                    plhDynDetail.Controls.Add(br4);
                }
                else if (item.AnswerForm == 3)
                {
                    string titleText = "";//問題內容

                    //判斷是否為必填
                    if (item.Required == true)
                        titleText = $"{qstNumber} . {item.QuestContent} (必填)";
                    else
                        titleText = $"{qstNumber} . {item.QuestContent}";

                    Label dynLabel = new Label()//標題
                    {
                        ID = $"dynTitle3_{qstNumber}",
                        Text = titleText,

                    };
                    plhDynDetail.Controls.Add(dynLabel);

                    //分行
                    Label br3 = new Label() { ID = $"lb{qstNumber}_br1", Text = "<br/>", };
                    plhDynDetail.Controls.Add(br3);

                    TextBox textbox = new TextBox()//文字方塊(email)
                    {
                        ID = $"textbox{ qstNumber}",
                        TextMode = TextBoxMode.Email,
                    };
                    plhDynDetail.Controls.Add(textbox);

                    //分行
                    Label br4 = new Label() { ID = $"lb{qstNumber}_br2", Text = "<br/>", };
                    plhDynDetail.Controls.Add(br4);
                }
                else if (item.AnswerForm == 4)
                {
                    string titleText = "";//問題內容

                    //判斷是否為必填
                    if (item.Required == true)
                        titleText = $"{qstNumber} . {item.QuestContent} (必填)";
                    else
                        titleText = $"{qstNumber} . {item.QuestContent}";

                    Label dynLabel = new Label()//標題
                    {
                        ID = $"dynTitle4_{qstNumber}",
                        Text = titleText,

                    };
                    plhDynDetail.Controls.Add(dynLabel);

                    //分行
                    Label br3 = new Label() { ID = $"lb{qstNumber}_br1", Text = "<br/>", };
                    plhDynDetail.Controls.Add(br3);

                    TextBox textbox = new TextBox()//文字方塊(日期)
                    {
                        ID = $"textbox{ qstNumber}",
                        TextMode = TextBoxMode.Date,
                    };
                    plhDynDetail.Controls.Add(textbox);

                    //分行
                    Label br4 = new Label() { ID = $"lb{qstNumber}_br2", Text = "<br/>", };
                    plhDynDetail.Controls.Add(br4);
                }
                else if (item.AnswerForm == 5)
                {
                    string titleText = "";//問題內容

                    //判斷是否為必填
                    if (item.Required == true)
                        titleText = $"{qstNumber} . {item.QuestContent} (必填)";
                    else
                        titleText = $"{qstNumber} . {item.QuestContent}";

                    Label dynLabel = new Label()//標題
                    {
                        ID = $"dynTitle5_{qstNumber}",
                        Text = titleText,
                    };
                    plhDynDetail.Controls.Add(dynLabel);

                    //分行
                    Label br3 = new Label() { ID = $"br{qstNumber}_br3", Text = "<br/>", };
                    plhDynDetail.Controls.Add(br3);

                    //單選選項
                    string[] arrSelection = null;
                    int selectionCount = 0;
                    _QuestMgr.SplitSelectItem(item.SelectItem, out selectionCount, out arrSelection);

                    //RadioButtonList
                    RadioButtonList RBList = new RadioButtonList() { ID = $"RB{qstNumber}" };
                    //選項
                    for (int j = 0; j < selectionCount; j++)
                    {
                        ListItem RBlistItem = new ListItem();
                        RBlistItem.Text = $"{arrSelection[j]}";
                        RBList.Items.Add(RBlistItem);
                    }
                    //永遠預設勾選第一項
                    RBList.Items[0].Selected = true;

                    Label br4 = new Label() { ID = $"br{qstNumber}_4", Text = "<br/>", }; //分行
                    plhDynDetail.Controls.Add(RBList);
                    plhDynDetail.Controls.Add(br4);
                }
                else
                {
                    string titleText = "";//問題內容

                    //判斷是否為必填
                    if (item.Required == true)
                        titleText = $"{qstNumber} . {item.QuestContent} (必填)";
                    else
                        titleText = $"{qstNumber} . {item.QuestContent}";

                    Label dynLabel = new Label()//標題
                    {
                        ID = $"dynTitle6_{qstNumber}",
                        Text = titleText,
                    };
                    plhDynDetail.Controls.Add(dynLabel);
                    Label br3 = new Label() { ID = $"br{qstNumber}_5", Text = "<br/>", }; //分行
                    plhDynDetail.Controls.Add(br3);

                    //多選選項
                    string[] arrSelection = null;
                    int selectionCount = 0;
                    _QuestMgr.SplitSelectItem(item.SelectItem, out selectionCount, out arrSelection);

                    //CheckBoxList
                    CheckBoxList CBList = new CheckBoxList() { ID = $"CB{item.QuestOrder}" };
                    //選項
                    for (int j = 0; j < selectionCount; j++)
                    {
                        ListItem CBlistItem = new ListItem();
                        CBlistItem.Text = $"{arrSelection[j]}";
                        CBList.Items.Add(CBlistItem);
                    }
                    Label br4 = new Label() { ID = $"br{qstNumber}_6", Text = "<br/>", }; //分行
                    plhDynDetail.Controls.Add(CBList);
                    plhDynDetail.Controls.Add(br4);
                }
                Label br5 = new Label() { ID = $"br{qstNumber}_next", Text = "<br/>", }; //分行
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
                    HttpContext.Current.Session["MainMsg"] = "請由列表進入作答問卷。<br/>(自動跳轉回列表頁)";
                    this.Response.Redirect($"/Index.aspx");
                }
            }

            ////如果記憶體中一題都沒有就跳到作答頁
            //if (AnswerList.Count == 0)
            //    Changebookmark1();
            //else
            //    Changebookmark2();


            //藉由預存session跳出視窗 的字串
            if (Session["MainMsg"] != null)
            {
                FrontIGetable o = (FrontIGetable)this.Master;
                var m = o.GetMsg();
                m.Value = Session["MainMsg"] as string;
                Session.Remove("MainMsg");
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
            string strName = this.doneName.Text.Trim();
            string strPhone = this.donePhone.Text.Trim();
            string strEmail = this.doneEmail.Text.Trim();
            string strAge = this.doneAge.Text.Trim();

            if (currentQnirID == "" || strName == "" || strPhone == "" || strEmail == "" || strAge == "")
            {
                HttpContext.Current.Session["MainMsg"] = "基本資料皆為必填項目。";
                return;
            }
            else if (strPhone.Length != 10)
            {
                HttpContext.Current.Session["MainMsg"] = "電話格式錯誤。";
                return;
            }
            else if (strPhone.Substring(0, 2) != "09")
            {
                HttpContext.Current.Session["MainMsg"] = "電話格式錯誤。";
                return;
            }
            if (strEmail.IndexOf("@") == -1 || strEmail.IndexOf(".") == -1 || strEmail.IndexOf("@") > strEmail.IndexOf("."))
            {
                HttpContext.Current.Session["MainMsg"] = "email格式錯誤。";
            }
            else if (int.Parse(strAge) <= 0)
            {
                HttpContext.Current.Session["MainMsg"] = "你沒有那麼年輕ㄛ。";
                return;
            }
            else
            {
                //基本問題儲存在記憶體
                basicQnir.QuestionnaireID = int.Parse(currentQnirID);
                basicQnir.Nickname = strName;
                basicQnir.Phone = strPhone;
                basicQnir.Email = strEmail;
                basicQnir.Age = int.Parse(strAge);
                int qstAmount = _QuestMgr.GetQuestionList(currentQnirID).Count();//取出本張問卷的問題數量

                //取出必填的題目
                List<Question> RequiredQuestionnaire = _QuestMgr.GetRequiredQuest(currentQnirID);


                //判斷該填的都有填
                bool checkFilled = true;
                foreach (Question item in RequiredQuestionnaire)
                {
                    //題號
                    int qstNumber = item.QuestOrder;//題號

                    if (item.AnswerForm == 1 || item.AnswerForm == 2 || item.AnswerForm == 3 || item.AnswerForm == 4)//文字方塊
                    {
                        string textboxTitle = $"textbox{qstNumber}";
                        TextBox dynBox = plhDynDetail.FindControl(textboxTitle) as TextBox;
                        if (dynBox.Text == null || dynBox.Text.Trim() == "")
                        {
                            checkFilled = false;//必填沒填
                        }
                    }
                    else
                    {
                        CheckBoxList dynCB = plhDynDetail.FindControl($"CB{qstNumber}") as CheckBoxList;
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

                int lastBasicID = 0;
                //計算前一筆的BasicAnswer
                if ( _AnswerMgr.GetDoneList(currentQnirID).Count!=0 )
                {
                lastBasicID = _AnswerMgr.GetDoneList(currentQnirID).LastOrDefault().BasicAnswerID;
                }


                if (checkFilled == false)
                {
                    HttpContext.Current.Session["MainMsg"] = "請確認必填項目皆有填寫。";
                    Changebookmark1();
                }

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
                                BasicAnswerID = lastBasicID+1,
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
                                BasicAnswerID = lastBasicID + 1,
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
                                BasicAnswerID = lastBasicID + 1,
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

                    //文字方塊
                    if (wholeQuestionnaire2[i].AnswerForm != 5 && wholeQuestionnaire2[i].AnswerForm != 6)
                    {
                        string titleText = $"{i + 1} . {wholeQuestionnaire2[i].QuestContent}";
                        if (wholeQuestionnaire2[i].Required == true)
                        {
                            titleText = $"{i + 1} . {wholeQuestionnaire2[i].QuestContent} (必填)";
                        }
                        Label dynLabel = new Label()//標題
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

                    //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                    else if (wholeQuestionnaire2[i].AnswerForm == 5)
                    {
                        string titleText = $"{i + 1} . {wholeQuestionnaire2[i].QuestContent}";
                        if (wholeQuestionnaire2[i].Required == true)
                        {
                            titleText = $"{i + 1} . {wholeQuestionnaire2[i].QuestContent} (必填)";
                        }
                        Label dynLabel = new Label()//標題
                        {
                            ID = $"dynTitle5{i}",
                            Text = titleText,
                        };
                        chkDynDetail.Controls.Add(dynLabel);

                        //分行
                        Label br3 = new Label() { ID = $"br{i}_br3", Text = "<br/>", };
                        chkDynDetail.Controls.Add(br3);

                        //單選選項
                        Label RBList = new Label()
                        {
                            ID = $"{i}",
                            Text = AnswerList[i].Answer1
                        };
                        chkDynDetail.Controls.Add(RBList);

                        //分行
                        Label br4 = new Label() { ID = $"br{i}_4", Text = "<br/>", };
                        chkDynDetail.Controls.Add(br4);

                    }
                    else
                    {
                        //標題
                        string titleText = $"{i + 1} . {wholeQuestionnaire2[i].QuestContent}";
                        if (wholeQuestionnaire2[i].Required == true)
                        {
                            titleText = $"{i + 1} . {wholeQuestionnaire2[i].QuestContent} (必填)";
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
                            answers += $"{arrSelection[j]};";
                        }
                        answers = answers.TrimEnd(';');

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
                    //每題的結尾分行
                    Label br5 = new Label() { ID = $"br{i}_next", Text = "<br/>", };
                    chkDynDetail.Controls.Add(br5);

                }
                #endregion



                this.chkName.Text = basicQnir.Nickname;
                this.chkPhone.Text = basicQnir.Phone;
                this.chkEmail.Text = basicQnir.Email;
                this.chkAge.Text = basicQnir.Age.ToString();

                HttpContext.Current.Session["MainMsg"] = "請確認內容填寫無誤後，按下送出鍵。";


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

