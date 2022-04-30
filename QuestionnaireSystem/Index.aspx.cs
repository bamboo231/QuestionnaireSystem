﻿using QuestionnaireSystem.Manager;
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
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //主問卷的Manager
        private SearchManager _srchMgr = new SearchManager();    //主問卷資訊

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Questionnaire> QtnirList = _QtnirMgr.GetEnableQstnir();
                this.RptrQtnir.DataSource = QtnirList;
                this.RptrQtnir.DataBind();
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
                HttpContext.Current.Session["IndexMsg"] = "輸入的關鍵字或日期不可為空。";
            }
            else
            {

                DateTime DTBeginDate = Convert.ToDateTime(srchBeginDate);
                DateTime DTEndDate = Convert.ToDateTime(srchEndDate);
                if (DTBeginDate > DTEndDate)
                {
                    HttpContext.Current.Session["IndexMsg"] = "開始日期不能晚於結束日期。";
                }
                else
                {
                    //篩選出包含關鍵字的資料
                    srchQuestionnaireList = _srchMgr.GetIncludeTextList(srchKey, srchQuestionnaireList);
                    //篩選出於時間範圍內的資料
                    srchQuestionnaireList = _srchMgr.GetIncludeDate(srchBeginDate, srchEndDate, srchQuestionnaireList);
                    this.RptrQtnir.DataSource = srchQuestionnaireList;
                    this.RptrQtnir.DataBind();
                    if (srchQuestionnaireList.Count == 0)
                        this.Label1.Visible = true;
                }
            }


        }

    }
}