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
    public partial class EditQuestionnaire : System.Web.UI.Page
    {
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //問卷基本資料
        private CommonQuestManager _CommonMgr = new CommonQuestManager();    //常用問題
        private AnswerManager _AnswerMgr = new AnswerManager();    //填寫資料
        private QuestManager _QuestMgr = new QuestManager();    //填寫資料

        //新增題目的次數
        private static int addQuestTime = 0;
        //題目的List
        private static List<Question> QuestSessionsList = new List<Question>();


        //未做
        //開始時間
        //結束時間
        //刪除問題
        //送出基本資料後，跳轉業面至編輯問題
        //問題表格的必填被選取之後，要跟著修改session
        //儲存問卷
        //取消問卷
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region//常用問題下拉選單
                List<CommonQuest> CommonList = _CommonMgr.GetCommonQuestList();
                foreach (CommonQuest quest in CommonList)
                {
                    this.setQuestType.Items.Add(quest.QuestContent);
                }
                #endregion

                #region 已填寫的問卷列表
                List<BasicAnswer> AnswerList = _AnswerMgr.GetBasicAnswerList();
                this.RptrAnswerList.DataSource = AnswerList;
                this.RptrAnswerList.DataBind();
                #endregion
            }
        }
        protected void Page_Prerender(object sender, EventArgs e)
        {
            //帶入常用問題至TEXTBOX


            //Dictionary<> SetQuestFrom
            //    for (int i = 0; i < this.RptrAnswerList.Items.Count;)

            //    #region//Session裡的題目寫到list
            //    if (application.)

            //        string questTable = $"<tr><td><asp:CheckBox ID=\"ChkBx\" runat=\"server\" /></td><td>1</td><td>問題一</td><td>問題種類</td><td><asp:CheckBox ID=\"CheckBox4" runat = "server" /></ td >< td >< asp:LinkButton ID = "LinkButton1" runat = "server" > 編輯 </ asp:LinkButton ></ td ></ tr > "
            //#endregion

        }

        //點擊分頁-問卷基本題目
        protected void bookmark1_Click(object sender, EventArgs e)
        {
            bookmark1.Attributes.Add("class", "nav-link active");
            this.plhbookmark1.Visible = true;
            bookmark1.Attributes.Remove("class");
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = false;
        }
        //點擊分頁-問題編輯
        protected void bookmark2_Click(object sender, EventArgs e)
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = true;
            this.plhbookmark3.Visible = false;
        }
        //點擊分頁-填寫資料
        protected void bookmark3_Click(object sender, EventArgs e)
        {
            this.plhbookmark1.Visible = false;
            this.plhbookmark2.Visible = false;
            this.plhbookmark3.Visible = true;
        }

        //問卷基本資料送出按鈕
        protected void btnBasicSummit_Click(object sender, EventArgs e)
        {

            HttpContext.Current.Session["Caption"] = this.textCaption.Text;
            HttpContext.Current.Session["QuestionnaireContent"] = this.textQuestionnaireContent.Text;
            HttpContext.Current.Session["StartDate"] = this.textStartDate.Text;
            HttpContext.Current.Session["EndDate"] = this.textEndDate.Text;
        }

        //問卷基本資料取消按鈕
        protected void btnBasicCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["Caption"] = null;
            HttpContext.Current.Session["QuestionnaireContent"] = null;
            HttpContext.Current.Session["StartDate"] = null;
            HttpContext.Current.Session["EndDate"] = null;
        }

        //編輯題目-按下加入按鈕後，存到Session
        protected void btnAddToQuest_Click(object sender, EventArgs e)
        {
            addQuestTime++;

            //將問題種類轉換成數字
            int intQuestForm = _QuestMgr.AnswerTextTOInt(this.setQuestForm.Text);

            //若答案的種類沒有選項，禁止他輸入文字

            //將題目新增至List
            Question arrQuest = new Question()
            {
                QuestOrder = addQuestTime,
                QuestContent = this.setQuest.Text,
                AnswerForm = intQuestForm,
                Required = this.IsRequired.Checked
            };
            QuestSessionsList.Add(arrQuest);

            //將更新後的List存入Session
            HttpContext.Current.Session["setQuest"] = QuestSessionsList;

            //將編輯的題目繫節至表格
            this.RptrQuest.DataSource = QuestSessionsList;
            this.RptrQuest.DataBind();
            #region//作廢
            //addQuestTime++;
            //string strSetQuestFrom = "SetQuestFrom" + addQuestTime.ToString();
            //string strSetQuest = "SetQuest" + addQuestTime.ToString();
            //string strSetQuestType = "SetQuestType" + addQuestTime.ToString();
            //string strIsRequired = "IsRequired" + addQuestTime.ToString();
            //HttpContext.Current.Session[strSetQuestFrom] = this.setQuestFrom.Text;
            //HttpContext.Current.Session[strSetQuest] = this.setQuest.Text;
            //HttpContext.Current.Session[strSetQuestType] = this.setQuestType.Text;
            //HttpContext.Current.Session[strIsRequired] = this.IsRequired.Text;
            #endregion
        }
        //刪除題目按鈕
        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            //this.
        }
        //送出問題
        protected void btnQuestSummit_Click(object sender, EventArgs e)
        {


        }
        //取消問題
        protected void btnQuestCancel_Click(object sender, EventArgs e)
        {

        }


    }
}