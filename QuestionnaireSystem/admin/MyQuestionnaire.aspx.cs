using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuestionnaireSystem.Manager;
using QuestionnaireSystem.ORM;

namespace QuestionnaireSystem.admin
{
    public partial class MyQuestionnaire : System.Web.UI.Page
    {
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //主問卷的Manager
        private SearchManager _srchMgr = new SearchManager();    //主問卷資訊
        private transWholeAnswerManager _transMgr = new transWholeAnswerManager();    //轉換WholeAnswer

        //未做
        //更改問卷內容
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Questionnaire> QtnirList = _QtnirMgr.GetQuestionnaireList();
                List<WholeAnswer> wholeList=_transMgr.QstnirToWholeList(QtnirList);
                foreach (WholeAnswer item in wholeList)
                {
                    if (item.VoidStatus == true)
                        item.OpenOrNot= "開放";
                    else
                        item.OpenOrNot = "已關閉";
                }
                this.RptrQtnir.DataSource = wholeList;
                this.RptrQtnir.DataBind();
            }

        }
        protected void Page_Prerender(object sender, EventArgs e)
        {
            //藉由預存session跳出視窗 的字串
            if (Session["MyQstnirMsg"] != null)
            {
                IGetable o = (IGetable)this.Master;
                var m = o.GetMsg();
                m.Value = Session["MyQstnirMsg"] as string;
                Session.Remove("MyQstnirMsg");
            }
        }

        //搜尋符合條件的問卷
        protected void srchButton_Click(object sender, EventArgs e)
        {
            string srchKey = this.srchKey.Text; //輸入的關鍵字
            string srchBeginDate = this.srchBeginDateText.Text; //輸入的開始時間
            string srchEndDate = this.srchEndDateText.Text; //輸入的結束時間

            List<Questionnaire> srchQuestionnaireList = _QtnirMgr.GetQuestionnaireList();   //取得DB裡所有問卷
            if (srchKey == "" || srchBeginDate == "" || srchEndDate == "")
            {
                HttpContext.Current.Session["MyQstnirMsg"] = "輸入的關鍵字或日期不可為空。";
            }
            else
            {

                DateTime DTBeginDate = Convert.ToDateTime(this.srchBeginDateText.Text);
                DateTime DTEndDate = Convert.ToDateTime(this.srchEndDateText.Text);
                if (DTBeginDate > DTEndDate)
                {
                    HttpContext.Current.Session["MyQstnirMsg"] = "開始日期不能晚於結束日期。";
                }
                else
                {
                    //篩選出包含關鍵字的資料
                    srchQuestionnaireList = _srchMgr.GetIncludeTextList(srchKey, srchQuestionnaireList);
                    //篩選出於時間範圍內的資料
                    srchQuestionnaireList = _srchMgr.GetIncludeDate(srchBeginDate, srchEndDate, srchQuestionnaireList);
                    this.RptrQtnir.DataSource = srchQuestionnaireList;
                    this.RptrQtnir.DataBind();
                    if(srchQuestionnaireList.Count==0)
                        this.Label1.Visible = true;
                }
            }


        }


        //刪除列表中勾選的資料
        protected void ImageDelete_Click(object sender, ImageClickEventArgs e)
        {
            //逐筆取得repeater中被勾選的資料ID並刪除
            foreach (Control c in this.RptrQtnir.Controls)
            {
                CheckBox cbx = (CheckBox)c.FindControl("ChkBxQnir");
                TextBox tbx = (TextBox)c.FindControl("tbxTableName");
                if (cbx != null && cbx.Checked == true)
                {
                    int qnirID = Int32.Parse(tbx.Text);
                    this._QtnirMgr.DeleteQuestionnaire(qnirID);
                }

            }
            Response.Redirect(Request.RawUrl);
        }
        //新增問卷
        protected void ImageAdd_Click(object sender, ImageClickEventArgs e)
        {
            HttpContext.Current.Session["QstnirID"] = _QtnirMgr.GetNextQuestionnaireID();
            HttpContext.Current.Session["IsNewQstnir"] = true;
            this.Response.Redirect("/admin/EditQuestionnaire.aspx");
        }

    }
}