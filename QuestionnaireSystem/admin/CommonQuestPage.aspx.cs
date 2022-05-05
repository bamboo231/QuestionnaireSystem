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
        private transWholeAnswerManager _transMgr = new transWholeAnswerManager();    //轉換WholeAnswer
        private CheckInputManager _checksMgr = new CheckInputManager();    //統計管理

        protected void Page_Load(object sender, EventArgs e)
        {



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
            List<CommonQuest> commonList = _CommonMgr.GetCommonQuestList();
            commonList.RemoveAt(0);
            List<WholeAnswer> wholeList = _transMgr.CommonToWholeList(commonList);
            this.RptrCommonQuest.DataSource = wholeList;
            this.RptrCommonQuest.DataBind();
        }
        //新增常用問題
        protected void btnAddToQuest_Click(object sender, EventArgs e)
        {
            #region//定義題目屬性
            string questContent = this.setQuest.Text;                               //問題描述
            string strQuestForm = this.setQuestForm.Text;                           //問題種類
            int intQuestForm = _QuestMgr.AnswerTextToInt(strQuestForm);            //問題種類
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
            CommonQuest newCommonQuest = new CommonQuest()
            {
                QuestContent = questContent,
                AnswerForm = intQuestForm,
                SelectItem = selectItem,
                Required = required
            };
            _CommonMgr.AddCommonQuest(newCommonQuest);
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

        }
        //返回列表
        protected void btnEditsOver_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("/admin/MyQuestionnaire.aspx");
        }


    }
}