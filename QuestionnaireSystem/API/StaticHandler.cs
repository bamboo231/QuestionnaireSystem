using QuestionnaireSystem.Manager;
using QuestionnaireSystem.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionnaireSystem.API
{
    public class StaticHandler
    {
        private QuestManager _QuestMgr = new QuestManager();    //問題管理
        private Statistic _statisMgr = new Statistic();    //統計管理
        public void ProcessRequest(HttpContext context)
        {

            //取得
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0)
            {
                int currentID = int.Parse(context.Request.Form["currentID"]);

                if (currentID != null)
                {
                    //顯示問卷題目及填寫的內容 
                    List<Question> dispQuestion = _QuestMgr.GetQuestionList(currentID);

                    //取得統計的數量
                    Dictionary<string, int[]> allAmount = _statisMgr.getAllStatisticCount(currentID);
                    int questID = 0;
                    foreach (Question item in dispQuestion)
                    {
                        questID++;

                        string stringQuestID = questID.ToString();

                        int[] arrAllThisQuestAmount = allAmount[stringQuestID];

                        //動態新增控制項(題號&題目(5:單選/6:多選/其他:文字方塊))
                        if (item.AnswerForm == 5)
                        {
                            Label dynLabel = new Label()//標題
                            {
                                ID = $"dynTitle{item.QuestOrder}",
                                Text = $"{item.QuestOrder} . {item.QuestContent}",
                            };
                            Label lb = new Label() { ID = $"lb{item.QuestOrder}", Text = "<br>", }; //分行

                            PlhDym.Controls.Add(dynLabel);
                            PlhDym.Controls.Add(lb);
                            string[] arrSelectItem = null;
                            int selectionCount = 0;
                            _QuestMgr.SplitSelectItem(item.SelectItem, out selectionCount, out arrSelectItem);//本題選項數量,本題選項陣列

                            int sumVoid = 0;
                            for (int i = 0; i < selectionCount; i++)//用來算同一題的票數總數
                            {
                                int count = 0;
                                if (arrSelectItem[i] != null)
                                    count = arrAllThisQuestAmount[i];
                                sumVoid += count;
                            }


                            for (int i = 0; i < selectionCount; i++)
                            {
                                Label dynSelection = new Label()//選項文字
                                {
                                    ID = $"Selection",
                                    Text = $"{arrSelectItem[i]}",
                                };

                                int count = 0;
                                if (arrSelectItem[i] != null)
                                    count = arrAllThisQuestAmount[i];
                                int percentVoid = count / sumVoid;
                                string barChart = $"<div class=\"progress\">< div class=\"progress-bar\" role=\"progressbar\" style=\'width: {percentVoid} %\' aria-valuenow=\"50\" aria-valuemin=\"0\" aria-valuemax=\"100\"></div></div>";
                                Literal ltlbarChart = new Literal()//長條圖
                                {
                                    Text = barChart,
                                };
                                Label dynCount = new Label()//數量
                                {
                                    ID = $"Count",
                                    Text = $"({count})",
                                };

                                Label lb2 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br>", }; //分行

                                PlhDym.Controls.Add(dynSelection);//題目文字
                                PlhDym.Controls.Add(ltlbarChart);//長條圖
                                PlhDym.Controls.Add(dynCount);//數量
                                PlhDym.Controls.Add(lb2);//分行

                            }
                            Label lb3 = new Label() { ID = $"lb{item.QuestID}_2", Text = "<br>", }; //分行
                            PlhDym.Controls.Add(lb3);
                        }
                        else if (item.AnswerForm == 6)
                        {
                            Label lb = new Label() { ID = $"lb{item.QuestID}", Text = "<br>", }; //分行

                            Label dynLabel = new Label()//標題
                            {
                                ID = $"dynTitle{item.QuestOrder}",
                                Text = $"{item.QuestOrder} . {item.QuestContent}",
                            };

                            PlhDym.Controls.Add(dynLabel);
                            PlhDym.Controls.Add(lb);

                            string[] arrSelectItem = null;
                            int selectionCount = 0;
                            _QuestMgr.SplitSelectItem(item.SelectItem, out selectionCount, out arrSelectItem);  //選項的數量,選項的陣列

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

                                Label dynSelection = new Label()
                                {
                                    ID = $"Selection",
                                    Text = $"{arrSelectItem[i]}",
                                };

                                int count = 0;
                                if (arrSelectItem[i] != null)
                                    count = arrAllThisQuestAmount[i];


                                //+++
                                Bitmap chart2 = _statisMgr.GetChart(100, 200);
                                chart2.Save(Response.OutputStream, ImageFormat.Jpeg);

                                MemoryStream ms = new MemoryStream();
                                chart2.Save(ms, ImageFormat.Gif);
                                var base64Data = Convert.ToBase64String(ms.ToArray());
                                imgCtrl.Src = "data:image/gif;base64," + base64Data;

                                ImageMap imageMap = new ImageMap();




                                Label dynCount = new Label()//數量
                                {
                                    ID = $"Count",
                                    Text = $"({count})",
                                };
                                Label lb2 = new Label() { ID = $"lb{item.QuestID}_{i}", Text = "<br>", }; //分行

                                PlhDym.Controls.Add(dynSelection);
                                PlhDym.Controls.Add(dynCount);
                                PlhDym.Controls.Add(lb2);
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














                List<Pboard> PBoardList = this._pbmgr.GetPBoardList();

                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(PBoardList);

                context.Response.ContentType = "JSON";
                context.Response.Write(jsonText);
                return;


            }

        }

    }
}