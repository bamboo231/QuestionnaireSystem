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

        //未做
        //更改問卷內容
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Questionnaire> QtnirList = _QtnirMgr.GetQuestionnaireList();
                this.RptrQtnir.DataSource = QtnirList;
                this.RptrQtnir.DataBind();
            }
        }

        protected void srchButton_Click(object sender, EventArgs e)
        {
            string srchKey = this.srchKey.Text; //輸入的關鍵字
            string srchBeginDate = this.srchBeginDateText.Text; //輸入的開始時間
            string srchEndDate = this.srchEndDateText.Text; //輸入的結束時間
            List<Questionnaire> srchQuestionnaireList = _QtnirMgr.GetQuestionnaireList();   //取得DB裡所有問卷

            //篩選出包含關鍵字的資料
            srchQuestionnaireList = _srchMgr.GetIncludeTextList(srchKey, srchQuestionnaireList);
            //篩選出於時間範圍內的資料
            srchQuestionnaireList = _srchMgr.GetIncludeDate(srchBeginDate, srchEndDate, srchQuestionnaireList);
            this.RptrQtnir.DataSource = srchQuestionnaireList;
            this.RptrQtnir.DataBind();

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
                CheckBox cbx2 = (CheckBox)c.FindControl("ChkBxQnir2");
                TextBox tbx2 = (TextBox)c.FindControl("tbxTableName2");
                if (cbx2 != null && cbx2.Checked == true)
                {
                    int qnirID = Int32.Parse(tbx2.Text);
                    this._QtnirMgr.DeleteQuestionnaire(qnirID);
                }
            }
            Response.Redirect(Request.RawUrl);

        }
        //新增問卷
        protected void ImageAdd_Click(object sender, ImageClickEventArgs e)
        {
            this.Response.Redirect("/admin/EditQuestionnaire.aspx");
        }

    }
}