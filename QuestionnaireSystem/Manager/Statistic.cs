using QuestionnaireSystem.Helper;
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
        /// 取得該問卷每一題的數量
        /// </summary>
        /// <param name="QnirID">傳入值為string</param>
        /// <returns>回傳值為Dictionary<string, int[]></returns>
        public Dictionary<string, int[]> getAllStatisticCount(string QnirID)
        {
            Dictionary<string, int[]> countEachSelectionInQuest = new Dictionary<string, int[]>();

            List<WholeAnswer> allData = _AnswerMgr.GetWholeDoneList2(QnirID);//集合(數量為每張問卷*所有問題筆數)
            int answerAmount = _QuestMgr.GetQuestionList(QnirID).Count();//該問卷的總題數
            int BAAmount = allData.Count() / answerAmount;

            for (int i = 0; i < answerAmount; i++)
            {
                //所有回答這道題的筆數
                List<WholeAnswer> allThisQuestData = allData.Where(obj => obj.QuestOrder == i + 1).ToList();
                if (allThisQuestData.Count != 0)
                {
                    //這道題的選項(字串陣列)
                    WholeAnswer QuestOrderInThisQnir = allThisQuestData.First();
                    if (QuestOrderInThisQnir.AnswerForm == 5 || QuestOrderInThisQnir.AnswerForm == 6)
                    {
                        _QuestMgr.SplitSelectItem(QuestOrderInThisQnir.SelectItem, out int selectionAmount, out string[] arrString);

                        int aaa = allThisQuestData.Count();
                        if (aaa > 0)
                        {
                            string strAll = "";
                            for (int r = 0; r < aaa; r++)
                            {
                                strAll = strAll + ";" + allThisQuestData[r].Answer;
                            }
                            strAll = strAll.Remove(0, 1);

                            foreach (WholeAnswer item in allThisQuestData)
                            {
                                _QuestMgr.SplitSelectItem(strAll, out int itemAmount, out string[] itemArrString);


                                //陣列容納該道題每個選項的數量
                                int[] arrAllAmountInThisQuest = new int[selectionAmount];//每一題的數量

                                for (int j = 0; j < selectionAmount; j++)
                                {
                                    foreach (string h in itemArrString)
                                    {
                                        if (h == arrString[j])
                                            arrAllAmountInThisQuest[j]++;
                                    }
                                }
                                int a = i + 1;
                                if (countEachSelectionInQuest.Count() == i)
                                    countEachSelectionInQuest.Add(a.ToString(), arrAllAmountInThisQuest);
                            }
                        }
                        else//如果沒人做答
                        {
                            int a = i + 1;
                            countEachSelectionInQuest.Add(a.ToString(), null);
                        }

                    }
                    else//如果是文字方塊
                    {
                        int a = i + 1;
                        countEachSelectionInQuest.Add(a.ToString(), null);
                    }
                }

            }
            return countEachSelectionInQuest;
        }

        public Dictionary<string, int[]> getAllStatisticCount(int QnirID, bool _isNewQstnir)
        {
            try
            {


                if (_isNewQstnir)
                {
                    Dictionary<string, int[]> noDATA = new Dictionary<string, int[]>();
                    return noDATA;
                }
                else
                {
                    Dictionary<string, int[]> countEachSelectionInQuest = new Dictionary<string, int[]>();
                    string strQnirID = QnirID.ToString();
                    List<WholeAnswer> allData = _AnswerMgr.GetWholeDoneList2(strQnirID);//集合(數量為每張問卷*所有問題筆數)
                    int answerAmount = _QuestMgr.GetQuestionList(QnirID).Count();//該問卷的總題數
                    int BAAmount = allData.Count() / answerAmount;

                    for (int i = 0; i < answerAmount; i++)
                    {
                        //所有回答這道題的筆數
                        List<WholeAnswer> allThisQuestData = allData.Where(obj => obj.QuestOrder == i + 1).ToList();
                        if (allThisQuestData.Count != 0)
                        {
                            //這道題的選項(字串陣列)
                            WholeAnswer QuestOrderInThisQnir = allThisQuestData.First();
                            if (QuestOrderInThisQnir.AnswerForm == 5 || QuestOrderInThisQnir.AnswerForm == 6)
                            {
                                _QuestMgr.SplitSelectItem(QuestOrderInThisQnir.SelectItem, out int selectionAmount, out string[] arrString);

                                int aaa = allThisQuestData.Count();
                                if (aaa > 0)
                                {
                                    string strAll = "";
                                    for (int r = 0; r < aaa; r++)
                                    {
                                        strAll = strAll + ";" + allThisQuestData[r].Answer;
                                    }
                                    strAll = strAll.Remove(0, 1);

                                    foreach (WholeAnswer item in allThisQuestData)
                                    {
                                        _QuestMgr.SplitSelectItem(strAll, out int itemAmount, out string[] itemArrString);


                                        //陣列容納該道題每個選項的數量
                                        int[] arrAllAmountInThisQuest = new int[selectionAmount];//每一題的數量

                                        for (int j = 0; j < selectionAmount; j++)
                                        {
                                            foreach (string h in itemArrString)
                                            {
                                                if (h == arrString[j])
                                                    arrAllAmountInThisQuest[j]++;
                                            }
                                        }
                                        int a = i + 1;
                                        if (countEachSelectionInQuest.Count() == i)
                                            countEachSelectionInQuest.Add(a.ToString(), arrAllAmountInThisQuest);
                                    }
                                }
                                else//如果沒人做答
                                {
                                    int a = i + 1;
                                    countEachSelectionInQuest.Add(a.ToString(), null);
                                }

                            }
                            else//如果是文字方塊
                            {
                                int a = i + 1;
                                countEachSelectionInQuest.Add(a.ToString(), null);
                            }
                        }

                    }
                    return countEachSelectionInQuest;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Statistic.getAllStatisticCount", ex);
                throw;
            }
        }
        public Bitmap GetChart(int numerator, int sumTOTAL)
        {
            float preLongCount = (float)numerator / sumTOTAL;
            float longCount = preLongCount * 600;

            Bitmap B = new Bitmap(600, 20);
            Graphics G = Graphics.FromImage(B);
            G.Clear(Color.White);

            int pPixel = 2;

            SolidBrush mySolidBrush = new SolidBrush(Color.SteelBlue);
            G.FillRectangle(mySolidBrush, 0, 0, longCount, 20);//矩形填色

            Pen myPen = new Pen(Color.Black, pPixel);
            G.DrawRectangle(myPen, 0, 0, 600, 20);//矩形外框

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