using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a Data")]
        public DateTime? Data { get; set; }


        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }


        [Required(ErrorMessage = "Informe o Tipo")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Informe o Valor")]
        public double Valor { get; set; }
        [Required(ErrorMessage = "Informe a Descrição")]
        public string Descricao { get; set; }
        public int Conta_Id { get; set; }
        public string Conta_Nome { get; set; }
        public int Plano_Contas_Id { get; set; }
        public string Plano_Contas_Descricao { get; set; }

        public List<TransacaoModel> GetTransacoes(int id_usuario_logado)
        {
            List<TransacaoModel> list = new List<TransacaoModel>();

            string filtro = "";

            if (DataInicio != null && DataFim != null)
            {
                filtro += $" AND T.DATA >= '{DataInicio?.ToString("yyyy/MM/dd")}' " +
                          $" AND T.DATA <= '{DataFim?.ToString("yyy/MM/dd")}'";
            }

            if (!string.IsNullOrEmpty(Tipo) && Tipo != "A")
            {
                filtro += $" AND T.TIPO = '{Tipo[0]}'";
            }

            if (Conta_Id > 0)
            {
                filtro += $" AND T.CONTA_ID = '{Conta_Id}'";
            }

            string sql = "SELECT " +
                "T.ID AS TRANSACAO_ID, " +
                "T.DATA, " +
                "T.TIPO, " +
                "T.VALOR, " +
                "T.DESCRICAO AS TRANSACAO_DESCRICAO, " +
                "T.CONTA_ID, " +
                "C.NOME AS CONTA_NOME, " +
                "PLANO_CONTAS_ID, " +
                "PC.DESCRICAO  AS PLANO_CONTAS_DESCRICAO " +
                "FROM TRANSACAO AS T " +
                "LEFT JOIN CONTA AS C ON C.Id = T.Conta_Id " +
                "LEFT JOIN PLANO_CONTAS AS PC ON PC.Id = T.Plano_Contas_Id " +
                $"WHERE PC.USUARIO_ID = {id_usuario_logado} {filtro}" +
                " ORDER BY T.DATA DESC ";

            var dt = Util.DAL.RetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new TransacaoModel()
                {
                    Id = int.Parse(row["TRANSACAO_ID"].ToString()),
                    Data = DateTime.Parse(row["DATA"].ToString()),
                    Descricao = row["TRANSACAO_DESCRICAO"].ToString(),
                    Tipo = row["TIPO"].ToString(),
                    Conta_Id = int.Parse(row["CONTA_ID"].ToString()),
                    Plano_Contas_Id = int.Parse(row["PLANO_CONTAS_ID"].ToString()),
                    Valor = double.Parse(row["VALOR"].ToString()),
                    Conta_Nome = row["CONTA_NOME"].ToString(),
                    Plano_Contas_Descricao = row["PLANO_CONTAS_DESCRICAO"].ToString()
                });
            }

            return list;
        }

        public TransacaoModel CarregarRegistro(int? id)
        {
            string sql = "SELECT " +
                "T.ID AS TRANSACAO_ID, " +
                "T.DATA, " +
                "T.TIPO, " +
                "T.VALOR, " +
                "T.DESCRICAO AS TRANSACAO_DESCRICAO, " +
                "T.CONTA_ID, " +
                "C.NOME AS CONTA_NOME, " +
                "PLANO_CONTAS_ID, " +
                "PC.DESCRICAO  AS PLANO_CONTAS_DESCRICAO " +
                "FROM TRANSACAO AS T " +
                "LEFT JOIN CONTA AS C ON C.Id = T.Conta_Id " +
                "LEFT JOIN PLANO_CONTAS AS PC ON PC.Id = T.Plano_Contas_Id " +
                $"WHERE T.ID = {id} " +
                "ORDER BY T.DATA DESC";

            var dt = Util.DAL.RetDataTable(sql);
            TransacaoModel item = new TransacaoModel()
            {
                Id = int.Parse(dt.Rows[0]["TRANSACAO_ID"].ToString()),
                Data = DateTime.Parse(dt.Rows[0]["DATA"].ToString()),
                Descricao = dt.Rows[0]["TRANSACAO_DESCRICAO"].ToString(),
                Tipo = dt.Rows[0]["TIPO"].ToString(),
                Conta_Id = int.Parse(dt.Rows[0]["CONTA_ID"].ToString()),
                Plano_Contas_Id = int.Parse(dt.Rows[0]["PLANO_CONTAS_ID"].ToString()),
                Valor = double.Parse(dt.Rows[0]["VALOR"].ToString()),
                Conta_Nome = dt.Rows[0]["CONTA_NOME"].ToString(),
                Plano_Contas_Descricao = dt.Rows[0]["PLANO_CONTAS_DESCRICAO"].ToString()
            };

            return item;
        }

        public void Update()
        {
            string sql = $"UPDATE TRANSACAO SET " +
                $"DATA = '{Data?.ToString("yyy/MM/dd")}', " +
                $"TIPO = '{Tipo}', " +
                $"VALOR = '{Valor}', " +
                $"DESCRICAO = '{Descricao}', " +
                $"CONTA_ID = '{Conta_Id}', " +
                $"PLANO_CONTAS_ID = '{Plano_Contas_Id}' " +
                $"WHERE ID = {Id}";
            Util.DAL.ExecutarComandoSql(sql);
        }

        public void Insert()
        {
            string sql = $"INSERT INTO TRANSACAO " +
                $"(DATA, TIPO, VALOR, DESCRICAO, CONTA_ID, PLANO_CONTAS_ID)" +
                $"VALUES" +
                $"('{Data?.ToString("yyy/MM/dd")}', '{Tipo}', '{Valor}', '{Descricao}', {Conta_Id}, {Plano_Contas_Id})";

            Util.DAL.ExecutarComandoSql(sql);
        }

        public void Excluir(int id)
        {
            string sql = $"DELETE FROM TRANSACAO WHERE ID = {id}";
            Util.DAL.ExecutarComandoSql(sql);
        }
    }


    public class DashboardModel
    {
        public double Total { get; set; }
        public string Descricao { get; set; }

        public List<DashboardModel> GetDadosGraficoPie(int id_usuario)
        {
            List<DashboardModel> list = new List<DashboardModel>();
            string sql = $"SELECT SUM(T.VALOR) AS TOTAL, P.DESCRICAO " +
                $"FROM TRANSACAO AS T " +
                $"LEFT JOIN PLANO_CONTAS AS P ON P.ID = T.PLANO_CONTAS_ID " +
                $"WHERE T.TIPO = 'D' AND P.USUARIO_ID = {id_usuario} " +
                $"GROUP BY P.DESCRICAO";

            var dt = Util.DAL.RetDataTable(sql);

            foreach (DataRow item in dt.Rows)
            {
                list.Add(new DashboardModel()
                {
                    Descricao = item["DESCRICAO"].ToString(),
                    Total = double.Parse(item["TOTAL"].ToString()),
                });
            }

            return list;

        }
    }
}
