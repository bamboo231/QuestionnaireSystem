using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace QuestionnaireSystem.admin
{
    public interface IGetable
    {
        HiddenField GetMsg();
    }
}