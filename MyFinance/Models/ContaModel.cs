using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Saldo { get; set; }
        public int Usuario_Id { get; set; }

        public List<ContaModel> GetContas()
        {
            List<ContaModel> list = new List<ContaModel>();

            int id_usuario_logado = 1;
            string sql = $"SELECT ID, NOME, SALDO, USUARIO_ID FROM CONTA WHERE USUARIO_ID = {id_usuario_logado}";

            var dt = Util.DAL.RetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ContaModel()
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Nome = row["NOME"].ToString(),
                    Saldo = double.Parse(row["SALDO"].ToString()),
                    Usuario_Id = int.Parse(row["USUARIO_ID"].ToString())
                });
            }

            return list;
        }
    }
}
