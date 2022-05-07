using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionnaireSystem.ShareControls
{
    public partial class ucPager : System.Web.UI.UserControl
    {
        /// <summary> 目前頁數 </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary> 一頁幾筆 </summary>
        public int PageSize { get; set; } = 10;

        /// <summary> 共幾筆 </summary>
        public int TotalRows { get; set; } = 0;

        private string _url = null;

        /// <summary> 要跳至哪個 URL (預設為本頁) </summary>
        public string Url
        {
            get
            {
                if (this._url == null)
                    return Request.Url.LocalPath; //只取到檔案名稱
                else
                    return this._url;
            }
            set
            {
                this._url = value;
            }
        }
        public string Url2
        {
            get
            {
                if (this._url == null)
                    return Request.RawUrl; //取整個URL
                else
                    return this._url;
            }
            set
            {
                this._url = value;
            }
        }
        public void Bind()
        {
            NameValueCollection collection = new NameValueCollection();

            this.Bind(collection);
        }
        public void Bind2()
        {
            NameValueCollection collection = new NameValueCollection();

            this.Bind2(collection);
        }

        public void Bind(string paramKey, string paramValue)
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add(paramKey, paramValue);
            this.Bind(collection);
        }
        public void Bind2(string paramKey, string paramValue)
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add(paramKey, paramValue);
            this.Bind2(collection);
        }

        public void Bind(NameValueCollection collection)
        {
            int pageCount = (this.TotalRows / this.PageSize);
            if ((this.TotalRows % this.PageSize) > 0)
                pageCount += 1;

            string url = this.Url;
            string qsText = this.BuildQueryString(collection);

            this.aLinkFirst.HRef = url + "?Page=1" + qsText;
            this.aLinkPrev.HRef = url + "?Page=" + (this.PageIndex - 1) + qsText;
            if (this.PageIndex - 1 < 1 || this.PageIndex> pageCount)
                this.aLinkPrev.HRef = url + "?Page=1" + qsText;

            this.aLinkNext.HRef = url + "?Page=" + (this.PageIndex + 1) + qsText;
            if (this.PageIndex+1 > pageCount)
                this.aLinkNext.HRef = url + "?Page=" + pageCount + qsText;

            this.aLinkLast.HRef = url + "?Page=" + pageCount + qsText;
            if (this.PageIndex ==0 && this.PageIndex > 1)
                    this.aLinkLast.HRef = url + "&Page=1" + qsText;

            this.aLinkPage1.HRef = url + "?Page=" + (this.PageIndex - 2) + qsText;
            this.aLinkPage1.InnerText = (this.PageIndex - 2).ToString();
            if (this.PageIndex <= 2)
                this.aLinkPage1.Visible = false;

            this.aLinkPage2.HRef = url + "?Page=" + (this.PageIndex - 1) + qsText;
            this.aLinkPage2.InnerText = (this.PageIndex - 1).ToString();
            if (this.PageIndex <= 1)
                this.aLinkPage2.Visible = false;

            this.aLinkPage3.HRef = "";
            this.aLinkPage3.InnerText = this.PageIndex.ToString();

            this.aLinkPage4.HRef = url + "?Page=" + (this.PageIndex + 1) + qsText;
            this.aLinkPage4.InnerText = (this.PageIndex + 1).ToString();
            if ((this.PageIndex + 1) > pageCount)
                this.aLinkPage4.Visible = false;

            this.aLinkPage5.HRef = url + "?Page=" + (this.PageIndex + 2) + qsText;
            this.aLinkPage5.InnerText = (this.PageIndex + 2).ToString();
            if ((this.PageIndex + 2) > pageCount)
                this.aLinkPage5.Visible = false;
        }
        public void Bind2(NameValueCollection collection)
        {
            int pageCount = (this.TotalRows / this.PageSize);
            if ((this.TotalRows % this.PageSize) > 0)
                pageCount += 1;

            string url = this.Url2;
            if(this.Request.QueryString["Page"]!=null)
            {
                int c = url.IndexOf("Page");//Page的位置
                url=url.Remove(c-1);//從c開始移除後面的字
            }

            string qsText = this.BuildQueryString(collection);

            this.aLinkFirst.HRef = url + "&Page=1" + qsText;
            this.aLinkPrev.HRef = url + "&Page=" + (this.PageIndex - 1) + qsText;
            if (this.PageIndex - 1 < 1 || this.PageIndex > pageCount)
                this.aLinkPrev.HRef = url + "&Page=1" + qsText;

            this.aLinkNext.HRef = url + "&Page=" + (this.PageIndex + 1) + qsText;
            if (this.PageIndex  > pageCount)
                this.aLinkNext.HRef = url + "&Page=" + this.PageIndex + qsText;

            this.aLinkLast.HRef = url + "&Page=" + (this.PageIndex + 1) + qsText;
            if (this.PageIndex == 0 && this.PageIndex > 1)
                this.aLinkLast.HRef = url + "&Page=1" + qsText;

            this.aLinkPage1.HRef = url + "&Page=" + (this.PageIndex - 2) + qsText;
            this.aLinkPage1.InnerText = (this.PageIndex - 2).ToString();
            if (this.PageIndex <= 2)
                this.aLinkPage1.Visible = false;

            this.aLinkPage2.HRef = url + "&Page=" + (this.PageIndex - 1) + qsText;
            this.aLinkPage2.InnerText = (this.PageIndex - 1).ToString();
            if (this.PageIndex <= 1)
                this.aLinkPage2.Visible = false;

            this.aLinkPage3.HRef = "";
            this.aLinkPage3.InnerText = this.PageIndex.ToString();

            this.aLinkPage4.HRef = url + "&Page=" + (this.PageIndex + 1) + qsText;
            this.aLinkPage4.InnerText = (this.PageIndex + 1).ToString();
            if ((this.PageIndex + 1) > pageCount)
                this.aLinkPage4.Visible = false;

            this.aLinkPage5.HRef = url + "&Page=" + (this.PageIndex + 2) + qsText;
            this.aLinkPage5.InnerText = (this.PageIndex + 2).ToString();
            if ((this.PageIndex + 2) > pageCount)
                this.aLinkPage5.Visible = false;
        }
        /// <summary> 組合 QueryString </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private string BuildQueryString(NameValueCollection collection)
        {
            List<string> paramList = new List<string>();
            // &key=value
            foreach (string key in collection.AllKeys)
            {
                if (collection.GetValues(key) == null)
                    continue;

                foreach (string val in collection.GetValues(key))
                {
                    paramList.Add($"&{key}={val}");
                }
            }
            string result = string.Join("", paramList);
            return result;
        }
    }
}
