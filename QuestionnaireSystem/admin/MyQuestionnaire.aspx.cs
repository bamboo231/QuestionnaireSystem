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
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //主問卷資訊

        //未做
        //更改問卷內容
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Questionnaire> QtnirList = _QtnirMgr.GetQuestionnaireList();
            this.RptrQtnir.DataSource = QtnirList;
            this.RptrQtnir.DataBind();

        }
    }
}