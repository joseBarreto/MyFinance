using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class PlanoContaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a Descrição")]
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int Usuario_Id { get; set; }

        public List<PlanoContaModel> GetPlanoContas(int id_usuario_logado)
        {
            List<PlanoContaModel> list = new List<PlanoContaModel>();

            string sql = $"SELECT ID, DESCRICAO, TIPO, USUARIO_ID FROM PLANO_CONTAS WHERE USUARIO_ID = {id_usuario_logado}";

            var dt = Util.DAL.RetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new PlanoContaModel()
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Descricao = row["Descricao"].ToString(),
                    Tipo = row["Tipo"].ToString(),
                    Usuario_Id = int.Parse(row["USUARIO_ID"].ToString())
                });
            }

            return list;
        }

        public PlanoContaModel CarregarRegistro(int? id)
        {
            string sql = $"SELECT ID, DESCRICAO, TIPO, USUARIO_ID FROM PLANO_CONTAS WHERE ID = {id}";

            var dt = Util.DAL.RetDataTable(sql);
            PlanoContaModel item = new PlanoContaModel()
            {
                Id = int.Parse(dt.Rows[0]["ID"].ToString()),
                Descricao = dt.Rows[0]["Descricao"].ToString(),
                Tipo = dt.Rows[0]["Tipo"].ToString(),
                Usuario_Id = int.Parse(dt.Rows[0]["USUARIO_ID"].ToString())
            };

            return item;
        }

        public void Update()
        {
            string sql = $"UPDATE PLANO_CONTAS SET Descricao = '{Descricao}', TIPO = '{Tipo}' WHERE ID = {Id}";
            Util.DAL.ExecutarComandoSql(sql);
        }

        public void Insert(int id_usuario_logado)
        {
            string sql = $"INSERT INTO PLANO_CONTAS " +
                $"( DESCRICAO, TIPO, USUARIO_ID)" +
                $"VALUES" +
                $"('{Descricao}', '{Tipo}', '{id_usuario_logado}')";

            Util.DAL.ExecutarComandoSql(sql);
        }

        public void Excluir(int id)
        {
            string sql = $"DELETE FROM PLANO_CONTAS WHERE ID = {id}";
            Util.DAL.ExecutarComandoSql(sql);
        }
    }
}
