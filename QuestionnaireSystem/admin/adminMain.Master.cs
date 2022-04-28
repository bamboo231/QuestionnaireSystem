using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionnaireSystem.admin
{
    public partial class adminMain : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region//頁面橫幅顯示的標題文字
            if (Request.FilePath == "/admin/MyQuestionnaire.aspx")
                this.pageTitle.Text = "後台 - 問卷管理列表";
            else if (Request.FilePath == "/admin/EditQuestionnaire.aspx")
                this.pageTitle.Text = "後台 - 編輯問卷";
            else if (Request.FilePath == "/admin/CommonQuestPage.aspx")
                this.pageTitle.Text = "後台 - 常用問題管理";
            #endregion
        }
        protected void Page_Prerender(object sender, EventArgs e)
        {
            if (Session["adminMainMsg"] != null)
            {
                this.adminMainMsg.Value = Session["adminMainMsg"] as string;
                Session.Remove("adminMainMsg");
            }
        }
    }
}