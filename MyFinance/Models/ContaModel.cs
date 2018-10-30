using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome da Conta")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o valor inicial desta conta")]
        public double? Saldo { get; set; }
        public int Usuario_Id { get; set; }


        public List<ContaModel> GetContas(int id_usuario_logado)
        {
            List<ContaModel> list = new List<ContaModel>();

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

        public ContaModel CarregarRegistro(int? id)
        {
            string sql = $"SELECT ID, NOME, SALDO, USUARIO_ID FROM CONTA WHERE ID = {id}";
            var dt = Util.DAL.RetDataTable(sql);
            ContaModel conta = new ContaModel()
            {
                Id = int.Parse(dt.Rows[0]["ID"].ToString()),
                Nome = dt.Rows[0]["NOME"].ToString(),
                Saldo = double.Parse(dt.Rows[0]["SALDO"].ToString()),
                Usuario_Id = int.Parse(dt.Rows[0]["USUARIO_ID"].ToString())
            };

            return conta;
        }

        public void Insert(int id_usuario_logado)
        {
            string sql = $"INSERT INTO CONTA " +
                $"( NOME, SALDO, USUARIO_ID)" +
                $"VALUES" +
                $"('{Nome}', '{Saldo}', '{id_usuario_logado}')";

            Util.DAL.ExecutarComandoSql(sql);
        }

        public void Excluir(int id)
        {
            string sql = $"DELETE FROM CONTA WHERE ID = {id}";
            Util.DAL.ExecutarComandoSql(sql);
        }

        public void Update()
        {
            string sql = $"UPDATE CONTA SET NOME = '{Nome}', SALDO = '{Saldo}' WHERE ID = {Id}";
            Util.DAL.ExecutarComandoSql(sql);
        }
    }
}
