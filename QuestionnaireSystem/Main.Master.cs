using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionnaireSystem
{
    public partial class Main : System.Web.UI.MasterPage, FrontIGetable
    {
        //接收來自子版頁面的訊息，然後傳入HTML
        public HiddenField GetMsg()
        {
            return this.MainMsg;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Prerender(object sender, EventArgs e)
        {
            if (Session["MainMsg"] != null)
            {
                this.MainMsg.Value = Session["MainMsg"] as string;
                Session.Remove("MainMsg");
            }
        }
    }
}