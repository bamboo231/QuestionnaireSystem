using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.Helper;
using QuestionnaireSystem.ORM;

namespace QuestionnaireSystem.Manager
{
    public class AnswerManager
    {
        private CheckInputManager _checksMgr = new CheckInputManager();    //統計管理

        /// <summary>
        /// 取得此問卷的基本資料
        /// </summary>
        /// <param name="QnirID">傳入值為問卷ID(string)</param>
        /// <returns>回傳值為List<BasicAnswer></returns>
        public BasicAnswer GetDoneData(string basicAnswerID)
        {
            int intbasicAnswerID = Int32.Parse(basicAnswerID);
            using (ContextModel contextModel = new ContextModel())
            {
                BasicAnswer basicAnswer = contextModel.BasicAnswers.Where(obj => obj.BasicAnswerID == intbasicAnswerID).FirstOrDefault();
                return basicAnswer;
            }
        }

        /// <summary>
        /// 取得此問卷的所有回覆List
        /// </summary>
        /// <param name="QnirID">傳入值為問卷ID(string)</param>
        /// <returns>回傳值為List<BasicAnswer></returns>
        public List<BasicAnswer> GetDoneList(string QnirID)
        {
            if (QnirID == "")
            {
                List<BasicAnswer> noDATA = new List<BasicAnswer>();
                return noDATA;
            }
            else
            {
                int intQnirID = Int32.Parse(QnirID);
                using (ContextModel contextModel = new ContextModel())
                {
                    List<BasicAnswer> basicAnswerList = contextModel.BasicAnswers.Where(obj => obj.QuestionnaireID == intQnirID).ToList();
                    return basicAnswerList;
                }
            }

        }

        /// <summary>
        /// 取得此問卷的所有回覆List
        /// </summary>
        /// <param name="QnirID">傳入值為問卷ID(string)</param>
        /// <returns>回傳值為List<BasicAnswer></returns>
        public List<WholeAnswer> GetWholeDoneList(string QnirID)//不要改
        {
            int intQnirID = Int32.Parse(QnirID);
            //使用者資訊、每個問題、每個問題的答案
            using (ContextModel contextModel = new ContextModel())
            {
                List<Question> questList = contextModel.Questions.ToList();
                List<BasicAnswer> basicAnswerList = contextModel.BasicAnswers.ToList();
                List<Answer> answerList = contextModel.Answers.ToList();
                //bug:取得的問題(QuestContent)與回答(Answer)對不上
                var listQAndB =
                    from b in basicAnswerList
                    join a in answerList on b.BasicAnswerID equals a.BasicAnswerID
                    where b.QuestionnaireID == intQnirID
                    select new WholeAnswer
                    {
                        QuestID = a.QuestID,
                        QuestionnaireID = b.QuestionnaireID,
                        BasicAnswerID = b.BasicAnswerID,
                        Nickname = b.Nickname,
                        Phone = b.Phone,
                        Email = b.Email,
                        Age = b.Age,
                        Answer = a.Answer1,
                    };

                var listQAndBAndA =
                    from n in listQAndB
                    join q in questList on n.QuestionnaireID equals q.QuestionnaireID
                    where q.QuestionnaireID == intQnirID
                    select new WholeAnswer
                    {
                        QuestID = n.QuestID,
                        QuestionnaireID = q.QuestionnaireID,
                        QuestContent = q.QuestContent,
                        BasicAnswerID = n.BasicAnswerID,
                        Nickname = n.Nickname,
                        Phone = n.Phone,
                        Email = n.Email,
                        Age = n.Age,
                        Answer = n.Answer,
                        QuestOrder = q.QuestOrder,
                        SelectItem = q.SelectItem,
                    };

                List<WholeAnswer> newWholeAnswerstList = new List<WholeAnswer>(listQAndBAndA);
                return newWholeAnswerstList;
            }
        }
        public List<WholeAnswer> GetWholeDoneList2(string QnirID)
        {
            try
            {
                int intQnirID = Int32.Parse(QnirID);
                //使用者資訊、每個問題、每個問題的答案
                using (ContextModel contextModel = new ContextModel())
                {
                    List<Question> questList = contextModel.Questions.ToList();
                    List<BasicAnswer> basicAnswerList = contextModel.BasicAnswers.ToList();
                    List<Answer> answerList = contextModel.Answers.ToList();
                    //bug:取得的問題(QuestContent)與回答(Answer)對不上
                    var listQAndB =
                        from a in answerList
                        join b in basicAnswerList on a.BasicAnswerID equals b.BasicAnswerID
                        where b.QuestionnaireID == intQnirID
                        select new WholeAnswer
                        {
                            QuestID = a.QuestID,
                            QuestionnaireID = b.QuestionnaireID,
                            BasicAnswerID = b.BasicAnswerID,
                            Nickname = b.Nickname,
                            Phone = b.Phone,
                            Email = b.Email,
                            Age = b.Age,
                            Answer = a.Answer1,
                        };

                    var listQAndBAndA =
                        from n in listQAndB
                        join q in questList on n.QuestID equals q.QuestID
                        where q.QuestionnaireID == intQnirID
                        select new WholeAnswer
                        {
                            QuestID = n.QuestID,
                            QuestionnaireID = q.QuestionnaireID,
                            QuestContent = q.QuestContent,
                            BasicAnswerID = n.BasicAnswerID,
                            Nickname = n.Nickname,
                            Phone = n.Phone,
                            Email = n.Email,
                            Age = n.Age,
                            Answer = n.Answer,
                            QuestOrder = q.QuestOrder,
                            SelectItem = q.SelectItem,
                            AnswerForm = q.AnswerForm,

                        };

                    List<WholeAnswer> newWholeAnswerstList = new List<WholeAnswer>(listQAndBAndA);
                    return newWholeAnswerstList;
                }
            }
            catch(Exception ex)
            {
                Logger.WriteLog("AnswerManager.GetWholeDoneList2", ex);
                throw;
            }

        }

            /// <summary>
            /// 取得目標問卷的問題號碼及答案種類
            /// </summary>
            /// <param name="strBasicAnswerID">傳入值為填寫的作答號ID(string)</param>
            /// <returns>回傳值為List<Question></returns>
            public List<WholeAnswer> GetTargetType(int basicAnswerID)
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    List<Question> questList = contextModel.Questions.ToList();
                    List<Answer> answerList = contextModel.Answers.ToList();

                    var newList =
                        from q in questList
                        join a in answerList on q.QuestID equals a.QuestID
                        where a.BasicAnswerID == basicAnswerID
                        select new WholeAnswer
                        {
                            QuestID = q.QuestID,
                            AnswerForm = q.AnswerForm,
                            Answer = a.Answer1,
                            SelectItem = q.SelectItem,
                        };
                    List<WholeAnswer> newQstList = new List<WholeAnswer>(newList);
                    return newQstList;
                }
            }
            public List<WholeAnswer> GetTargetType(string strBasicAnswerID)
            {
                int basicAnswerID = Int32.Parse(strBasicAnswerID);
                using (ContextModel contextModel = new ContextModel())
                {
                    List<Question> questList = contextModel.Questions.ToList();
                    List<BasicAnswer> basicAnswerList = contextModel.BasicAnswers.ToList();
                    List<Answer> answerList = contextModel.Answers.ToList();

                    var listQAndB =
                        from b in basicAnswerList
                        join a in answerList on b.BasicAnswerID equals a.BasicAnswerID
                        where b.BasicAnswerID == basicAnswerID
                        select new WholeAnswer
                        {
                            QuestID = a.QuestID,
                            QuestionnaireID = b.QuestionnaireID,
                            BasicAnswerID = b.BasicAnswerID,
                            Nickname = b.Nickname,
                            Phone = b.Phone,
                            Email = b.Email,
                            Age = b.Age,
                            Answer = a.Answer1,
                        };

                    var listQAndBAndA =
                        from q in questList
                        join n in listQAndB on q.QuestionnaireID equals n.QuestionnaireID
                        where q.QuestID == n.QuestID
                        select new WholeAnswer
                        {
                            QuestID = n.QuestID,
                            QuestionnaireID = q.QuestionnaireID,
                            QuestContent = q.QuestContent,
                            BasicAnswerID = n.BasicAnswerID,
                            Nickname = n.Nickname,
                            Phone = n.Phone,
                            Email = n.Email,
                            Age = n.Age,
                            Answer = n.Answer,
                            QuestOrder = q.QuestOrder,
                            SelectItem = q.SelectItem,
                        };
                    List<WholeAnswer> newWholeAnswerstList = new List<WholeAnswer>(listQAndBAndA);

                    return newWholeAnswerstList;
                }
            }

            /// <summary>
            /// 儲存填寫問卷的基本資料
            /// </summary>
            /// <param name="basicQnir">傳入BasicAnswer</param>
            public void SaveBasicQnir(BasicAnswer basicQnir)
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    contextModel.BasicAnswers.Add(basicQnir);
                    contextModel.SaveChanges();
                }
            }

            /// <summary>
            /// 取得新增的基本資料號
            /// </summary>
            /// <param name="basicQnir">傳入BasicAnswer</param>
            public int GetBasicQnir()
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    List<BasicAnswer> basicAnswerList = contextModel.BasicAnswers.ToList();
                    int newNumber = basicAnswerList.Count() + 1;
                    return newNumber;
                }
            }

            /// <summary>
            /// 儲存填寫問卷的問題內容
            /// </summary>
            /// <param name="AnswerList">傳入List<Answer></param>
            public void SaveAnswer(List<Answer> AnswerList)
            {

                int newNumber = GetBasicQnir();
                using (ContextModel contextModel = new ContextModel())
                {
                    foreach (Answer answer in AnswerList)
                    {
                        contextModel.Answers.Add(answer);
                    }
                    contextModel.SaveChanges();
                }
            }


        }
    }