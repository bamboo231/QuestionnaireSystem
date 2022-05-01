using QuestionnaireSystem.ORM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace QuestionnaireSystem.Manager
{
    public class Statistic
    {
        private AnswerManager _AnswerMgr = new AnswerManager();    //填寫資料
        private QuestionnaireManager _QtnirMgr = new QuestionnaireManager();    //問卷基本資料
        private QuestManager _QuestMgr = new QuestManager();    //填寫資料

        /// <summary>
        /// 依照指定的問卷編號，查出每道題目的投票數
        /// </summary>
        /// <param name="QnirID">指定問卷編號，傳入值為string</param>
        /// <returns>回傳值為Dictionary<int, int[]></returns>
        public Dictionary<string, int[]> getAllStatisticCount(string QnirID)
        {
            Dictionary<string, int[]> countEachSelectionInQuest = new Dictionary<string, int[]>();

            List<WholeAnswer> allData = _AnswerMgr.GetWholeDoneList(QnirID);//集合(數量為每張問卷*所有問題筆數)
            int answerAmount = _QuestMgr.GetQuestionList(QnirID).Count();//該問卷的總題數

            for (int i = 0; i < answerAmount; i++)
            {
                //所有回答這道題的筆數
                List<WholeAnswer> allThisQuestData = allData.Where(obj => obj.QuestOrder == i + 1).ToList();
                if (allThisQuestData.Count != 0)
                {
                    //這道題的選項(字串陣列)
                    WholeAnswer QuestOrderInThisQnir = allThisQuestData.First();
                    string[] arrString = null;
                    int selectionAmount = 0;
                    _QuestMgr.SplitSelectItem(QuestOrderInThisQnir.SelectItem, out selectionAmount, out arrString);


                    //依序取出每道題目的選項數目
                    if (arrString != null)
                    {
                        //陣列容納該道題每個選項的數量
                        int[] arrAllAmountInThisQuest = new int[selectionAmount];
                        for (int j = 0; j < selectionAmount; j++)
                        {
                            arrAllAmountInThisQuest[j] = allThisQuestData.Where(obj => obj.Answer == arrString[j]).Count();
                        }
                        int a = i+1;
                            countEachSelectionInQuest.Add(a.ToString(), arrAllAmountInThisQuest);
                    }
                    else//如果不是單選或多選
                    {
                        int a = i + 1;
                        countEachSelectionInQuest.Add(a.ToString(), null);
                    }
                }
                else//如果沒人做答
                {
                    int a = i + 1;
                    countEachSelectionInQuest.Add(a.ToString(), null);
                }
            }
            return countEachSelectionInQuest;
        }

        public Bitmap GetChart(int numerator)
        {
            Bitmap B = new Bitmap(numerator,20);
            Graphics G = Graphics.FromImage(B);
            //Color BrushColor = ColorTranslator.FromHtml("#00cccb");

            int pPixel = 2;
            Pen frame = new Pen(Color.Black, pPixel) ;
            G.DrawRectangle(frame,0,0, numerator, 20);//矩形

            //SolidBrush MyBrush = new SolidBrush(BrushColor);
            //G.FillRectangle(MyBrush, 0, numerator, 5, numerator);
            return B;
        }


        public static void RenderImage(string assembly, string image, HttpContext context)
        {
            using (System.Drawing.Image img = (System.Drawing.Image)HttpContext.GetGlobalResourceObject(assembly, image))
            {
                context.Response.ClearContent();
                context.Response.ContentType = "image/gif";
                img.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
            }
        }
    }
}