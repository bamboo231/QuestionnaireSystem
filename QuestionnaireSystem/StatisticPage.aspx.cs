using QuestionnaireSystem.Manager;
using QuestionnaireSystem.ORM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionnaireSystem
{
    public partial class StatisticPage : System.Web.UI.Page
    {
        private QuestManager _QuestMgr = new QuestManager();    //問題管理
        private Statistic _statisMgr = new Statistic();    //統計管理
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentQnirID = this.Request.QueryString["QnirID"];  //從URL取得當前所在的問卷ID

            this.currentID.Value = currentQnirID;

            #region//後台統計頁
            if (currentQnirID != null)
            {
                //顯示問卷題目及填寫的內容 
                this.PlhDym.Controls.Clear();     //清空統計的placeHolder
                                                  //題目的資料
                List<Question> dispQuestion = _QuestMgr.GetQuestionList(currentQnirID);

                //取得統計的數量
                Dictionary<string, int[]> allAmount = _statisMgr.getAllStatisticCount(currentQnirID);
                if (allAmount.Count() == 0)
                {
                    this.NAStatistic.Visible = true;
                }
                else
                {
                    int questID = 0;
                    foreach (Question item in dispQuestion)
                    {
                        questID++;

                        string stringQuestID = questID.ToString();

                        int[] arrAllThisQuestAmount = allAmount[stringQuestID];//取出該題的票數陣列
                        int allThisQst = 0;
                        if (arrAllThisQuestAmount != null)
                        {
                            for (int i = 0; i < arrAllThisQuestAmount.Length; i++)
                                allThisQst = allThisQst + arrAllThisQuestAmount[i];
                        }

                        //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                        if (item.AnswerForm == 5)
                        {

                            Label dynLabel = new Label()//標題
                            {
                                ID = $"dynTitle{item.QuestOrder}",
                                Text = $"{item.QuestOrder} . {item.QuestContent}",
                            };
                            PlhDym.Controls.Add(dynLabel);

                            Label lb = new Label() { ID = $"lb{item.QuestOrder}", Text = "<br>", }; //分行
                            PlhDym.Controls.Add(lb);


                            _QuestMgr.SplitSelectItem(item.SelectItem, out int selectionCount, out string[] arrSelectItem);//本題選項數量,本題選項陣列
                            int sumVoid = 0;
                            for (int i = 0; i < selectionCount; i++)//用來算同一題的票數總數
                            {
                                sumVoid = sumVoid + arrAllThisQuestAmount[i];
                            }

                            for (int i = 0; i < selectionCount; i++)
                            {
                                Label dynSelection = new Label()//選項文字
                                {
                                    ID = $"Selection",
                                    Text = $"{arrSelectItem[i]}",
                                };
                                PlhDym.Controls.Add(dynSelection);
                                Label lb2 = new Label() { ID = $"lb{item.QuestID}", Text = "<br>", }; //分行
                                PlhDym.Controls.Add(lb2);

                                //GDI+圖形
                                byte[] imgBytes = null;
                                Bitmap barChart = _statisMgr.GetChart(arrAllThisQuestAmount[i], sumVoid);

                                using (var stream = new MemoryStream())
                                {
                                    barChart.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    imgBytes = stream.ToArray();
                                }
                                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image()
                                {
                                    ImageUrl = "Data:Image/png;base64," + Convert.ToBase64String(imgBytes),
                                };
                                PlhDym.Controls.Add(img);//圖形


                                float floPecentCount = (float)arrAllThisQuestAmount[i] / allThisQst;
                                string strPecent = floPecentCount.ToString("P1");
                                Label pecentCount = new Label()//百分比
                                {
                                    ID = $"PecentCount",
                                    Text = $"{strPecent}",
                                };
                                PlhDym.Controls.Add(pecentCount);//百分比


                                Label dynCount = new Label()//數量
                                {
                                    ID = $"Count",
                                    Text = $"({arrAllThisQuestAmount[i]})",
                                };
                                PlhDym.Controls.Add(dynCount);//數量

                                Label lb3 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br>", }; //分行
                                PlhDym.Controls.Add(lb3);//分行

                            }
                            Label lb4 = new Label() { ID = $"lb{item.QuestID}_2", Text = "<br>", }; //分行
                            PlhDym.Controls.Add(lb4);
                        }
                        else if (item.AnswerForm == 6)
                        {

                            Label dynLabel = new Label()//標題
                            {
                                ID = $"dynTitle{item.QuestOrder}",
                                Text = $"{item.QuestOrder} . {item.QuestContent}",
                            };
                            PlhDym.Controls.Add(dynLabel);

                            Label lb = new Label() { ID = $"lb{item.QuestID}", Text = "<br>", }; //分行
                            PlhDym.Controls.Add(lb);

                            _QuestMgr.SplitSelectItem(item.SelectItem, out int selectionCount, out string[] arrSelectItem);  //選項的數量,選項的陣列

                            int sumVoid = 0;
                            for (int i = 0; i < selectionCount; i++)//用來算同一題的票數總數
                            {
                                int count = 0;
                                if (arrSelectItem[i] != null)
                                    count = arrAllThisQuestAmount[i];
                                sumVoid += count;
                            }

                            //本題選項數量
                            for (int i = 0; i < selectionCount; i++)
                            {

                                Label dynSelection = new Label()//題目
                                {
                                    ID = $"Selection",
                                    Text = $"{arrSelectItem[i]}",
                                };
                                PlhDym.Controls.Add(dynSelection);
                                Label lb2 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br>", };//分行
                                PlhDym.Controls.Add(lb2);


                                int count = 0;
                                if (arrSelectItem[i] != null)
                                    count = arrAllThisQuestAmount[i];//單題票數

                                //GDI+圖形
                                byte[] imgBytes = null;
                                //Bitmap chart2 = new Bitmap(600, 30);
                                Bitmap chart2 = _statisMgr.GetChart(count, allThisQst);

                                //Bitmap bitmap = new Bitmap(600, 30);
                                using (var stream = new MemoryStream())
                                {
                                    chart2.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    imgBytes = stream.ToArray();
                                }
                                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image()
                                {
                                    ImageUrl = "Data:Image/png;base64," + Convert.ToBase64String(imgBytes),
                                };
                                PlhDym.Controls.Add(img);//圖形


                                float floPecentCount = (float)count / allThisQst;
                                string strPecent = floPecentCount.ToString("P1");
                                Label pecentCount = new Label()//百分比
                                {
                                    ID = $"PecentCount",
                                    Text = $"{strPecent}",
                                };
                                PlhDym.Controls.Add(pecentCount);//百分比


                                Label dynCount = new Label()//票數
                                {
                                    ID = $"Count",
                                    Text = $"({count})",
                                };
                                PlhDym.Controls.Add(dynCount);//票數
                                                              //分行
                                Label lb2_2 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br>", };
                                PlhDym.Controls.Add(lb2_2);
                            }
                            Label lb3 = new Label() { ID = $"lb{item.QuestID}_2", Text = "<br>", }; //分行
                            PlhDym.Controls.Add(lb3);
                        }
                        else
                        {
                            Label dynLabel = new Label()
                            {
                                ID = $"dynTitle{item.QuestOrder}",
                                Text = $"{item.QuestOrder} . {item.QuestContent}",
                            };
                            Label content = new Label()
                            {
                                ID = "noneDisplay",
                                Text = "-",
                            };
                            Label lb = new Label() { ID = $"lb{item.QuestOrder}", Text = "<br>", }; //分行
                            PlhDym.Controls.Add(dynLabel);
                            PlhDym.Controls.Add(lb);

                            Label lb2 = new Label() { ID = $"lb{item.QuestOrder}", Text = "<br>", }; //分行
                            PlhDym.Controls.Add(content);
                            PlhDym.Controls.Add(lb2);

                            Label lb3 = new Label() { ID = $"lb{item.QuestOrder}_2", Text = "<br>", }; //分行
                            PlhDym.Controls.Add(lb3);
                        }
                    }
                }
            }
            #endregion
        }
        protected void Page_Init(object sender, EventArgs e)
        {

        }


    }
}