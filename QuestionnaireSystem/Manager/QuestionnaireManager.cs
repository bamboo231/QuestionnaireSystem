﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireSystem.ORM;

namespace QuestionnaireSystem.Manager
{
    public class QuestionnaireManager
    {
        /// <summary>
        /// 取得所有問卷List
        /// </summary>
        /// <returns>回傳值為List</returns>
        public List<Questionnaire> GetQuestionnaireList()
        { 
            using (ContextModel contextModel = new ContextModel())
            {
                return contextModel.Questionnaires.ToList();
            }
        }
    }
}