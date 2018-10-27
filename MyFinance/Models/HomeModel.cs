using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class HomeModel
    {
        public string LerNomeUsuario()
        {
            var dt = Util.DAL.RetDataTable("SELECT * FROM USUARIO");
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["NOME"].ToString();
            }
            return "Nome não encontrado";
        }
    }
}
