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
    public partial class Index : System.Web.UI.Page
    {
        private transWholeAnswerManager _transMgr = new transWholeAnswerManager();    //轉換WholeAnswer
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //主問卷的Manager
        private SearchManager _srchMgr = new SearchManager();    //主問卷資訊
        private const int _pageSize = 10;  //每頁10筆資料

        protected void Page_Load(object sender, EventArgs e)
        {
            string pageIndexText = this.Request.QueryString["Page"];
            int pageIndex =
                (string.IsNullOrWhiteSpace(pageIndexText))
                    ? 1
                    : Convert.ToInt32(pageIndexText);
            if (!IsPostBack)
            {
                string keyword = this.Request.QueryString["keyword"];
                if (!string.IsNullOrWhiteSpace(keyword))
                    this.srchKey.Text = keyword;

                List<Questionnaire> QtnirList = _QtnirMgr.GetEnableQstnir();
                var list = this._QtnirMgr.TakeEnableQstnir(QtnirList, _pageSize, pageIndex, out int totalRows);
                list = _srchMgr.GetIncludeTextList(keyword, list);

                this.ucPager.TotalRows = totalRows;
                this.ucPager.PageIndex = pageIndex;
                this.ucPager.Bind("keyword", keyword);
                //篩選出包含關鍵字的資料

                List<WholeAnswer> wholeList = _transMgr.QstnirToWholeList(list);
                foreach (var item in wholeList)
                {
                    item.OpenOrNot = "投票中";
                    if (item.StartDate > DateTime.Now)
                        item.OpenOrNot = "未開始";
                    else if (item.EndDate < DateTime.Now)
                        item.OpenOrNot = "已完結";
                }
                this.RptrQtnir.DataSource = wholeList;
                this.RptrQtnir.DataBind();

                if (list.Count < 1)
                    NoData.Visible = true;
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
                HttpContext.Current.Session["MainMsg"] = "輸入的關鍵字或日期不可為空。";
            }
            else
            {

                DateTime DTBeginDate = Convert.ToDateTime(srchBeginDate);
                DateTime DTEndDate = Convert.ToDateTime(srchEndDate);
                if (DTBeginDate > DTEndDate)
                {
                    HttpContext.Current.Session["MainMsg"] = "開始日期不能晚於結束日期。";
                }
                else
                {

                    //篩選出包含關鍵字的資料
                    //srchQuestionnaireList = _srchMgr.GetIncludeTextList(srchKey, srchQuestionnaireList);
                    //篩選出於時間範圍內的資料
                    srchQuestionnaireList = _srchMgr.GetIncludeDate(srchBeginDate, srchEndDate, srchQuestionnaireList);

                    string url = this.Request.Url.LocalPath + "?keyword=" + srchKey;

                    //this.RptrQtnir.DataSource = srchQuestionnaireList;
                    //this.RptrQtnir.DataBind();
                    if (srchQuestionnaireList.Count == 0)
                        this.NoData.Visible = true;
                    this.Response.Redirect(url);
                }
            }

        }

    }
}