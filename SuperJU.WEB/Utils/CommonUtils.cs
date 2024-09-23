using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SuperJU.WEB.Utils
{
    public static class CommonUtils
    {
        public static void Alerta(Page page, string msg)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('" + msg + "');", true);
        }

        public static void AlertaCampoObrigatorio(Page page, string field)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('O campo " + field + " é obrigatório!');", true);
        }
    }
}