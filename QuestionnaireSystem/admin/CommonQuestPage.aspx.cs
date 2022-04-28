using QuestionnaireSystem.Manager;
using QuestionnaireSystem.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace QuestionnaireSystem.admin
{
    public partial class CommonQuestPage : System.Web.UI.Page
    {
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //問卷基本資料管理
        private CommonQuestManager _CommonMgr = new CommonQuestManager();    //常用問題管理
        private AnswerManager _AnswerMgr = new AnswerManager();    //回覆管理
        private QuestManager _QuestMgr = new QuestManager();    //問題管理
        private Statistic _statisMgr = new Statistic();    //統計管理
        protected void Page_Load(object sender, EventArgs e)
        {
            List<CommonQuest> commonList = _CommonMgr.GetCommonQuestList();
            this.RptrCommonQuest.DataSource = commonList;
            this.RptrCommonQuest.DataBind();
            //若答案的種類沒有選項，禁止他輸入文字

        }
        protected void Page_Prerender(object sender, EventArgs e)
        {
            string strCommonQuestID = this.Request.QueryString["CommonQuestID"];//從URL取得要帶入文字框的值
            if (strCommonQuestID != null)
            {
                //把常用問題帶入編輯區
                CommonQuest targetCommonQuest = _CommonMgr.GetCommonQuest(strCommonQuestID);
                this.setQuest.Text = targetCommonQuest.QuestContent;
                this.setQuestForm.SelectedValue = targetCommonQuest.SelectItem;
                this.IsRequired.Checked = targetCommonQuest.Required;
                if (targetCommonQuest.SelectItem != null)
                    this.textSelectItem.Text = targetCommonQuest.SelectItem.ToString();
            }
        }
        //新增常用問題
        protected void btnAddToQuest_Click(object sender, EventArgs e)
        {

            //將問題種類轉換成數字
            int intQuestForm = _QuestMgr.AnswerTextToInt(this.setQuestForm.Text);
            //將題目新增至List
            CommonQuest newCommonQuest = new CommonQuest()
            {
                QuestContent = this.setQuest.Text,
                AnswerForm = intQuestForm,
                SelectItem = this.textSelectItem.Text,
                Required = this.IsRequired.Checked
            };
            _CommonMgr.AddCommonQuest(newCommonQuest);
            Response.Redirect(Request.RawUrl);
        }
        //刪除常用問題
        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            List<CommonQuest> newList = new List<CommonQuest>();//即將被刪除的List
            //逐筆取得repeater中被勾選的資料
            for (int i = 0; i < this.RptrCommonQuest.Items.Count; i++)
                //foreach (RepeaterItem c in this.RptrQuest.Items)
            {
                CheckBox cbx = (CheckBox)RptrCommonQuest.Items[i].FindControl("chkBxQuest");
                TextBox tbx = (TextBox)RptrCommonQuest.Items[i].FindControl("tbxTableName");
                
                int deletedID = 0;
                if (cbx != null && cbx.Checked == true)
                    deletedID = Int32.Parse(tbx.Text);
                if (deletedID != 0)
                {
                    CommonQuest deletedCommonQuest = new CommonQuest() { CommonQuestID = deletedID };
                    newList.Add(deletedCommonQuest);
                }
            }
            _CommonMgr.DeleteQuestionnaire(newList);
            Response.Redirect(Request.RawUrl);
        }
        //返回列表
        protected void btnEditsOver_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("/admin/MyQuestionnaire.aspx");
        }


        protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<CommonQuest> newList = new List<CommonQuest>();//即將被刪除的List
            CheckBox checkbox = new CheckBox();
            //逐筆取得repeater中被勾選的資料
            for (int i = 0; i < this.RptrCommonQuest.Items.Count; i++)
            //foreach (RepeaterItem c in this.RptrQuest.Items)
            {
                checkbox = (CheckBox)RptrCommonQuest.Items[i].FindControl("chkBxQuest");//取對象
                HtmlInputCheckBox chkDisplayTitle = (HtmlInputCheckBox)RptrCommonQuest.Items[i].FindControl($"chkBxQuest{i}");
                if (chkDisplayTitle != null && chkDisplayTitle.Checked)
                {
                    //HERE IS YOUR VALUE: chkAddressSelected.Value
                }
                //CheckBox cbx = (CheckBox)RptrQuest.Items[].FindControl("chkBxQuest");
                CheckBox cbx = (CheckBox)RptrCommonQuest.Items[i].FindControl("chkBxQuest");
                TextBox tbx = (TextBox)RptrCommonQuest.Items[i].FindControl("tbxTableName");

                int deletedID = 0;
                if (cbx != null && cbx.Checked == true)
                    deletedID = Int32.Parse(tbx.Text);
                if (deletedID != 0)
                {
                    CommonQuest deletedCommonQuest = new CommonQuest() { CommonQuestID = deletedID };
                    newList.Add(deletedCommonQuest);
                }
            }
            _CommonMgr.DeleteQuestionnaire(newList);
            Response.Redirect(Request.RawUrl);
        }
    }
}