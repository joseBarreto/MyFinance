using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MyFinance.Util
{
    public class DAL
    {
        private static readonly string Server = "localhost";
        private static readonly string Database = "financeiro";
        private static readonly string User = "root";
        private static readonly string Password = "";
        private static string ConnectionString
        {
            get
            {
                return $"Server={Server};Database={Database};Uid={User};Pwd={Password}";
            }
        }

        /// <summary>
        /// Execute Selects in db
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable RetDataTable(string sql)
        {
            DataTable dataTable = new DataTable();

            using (var Connection = new MySqlConnection(ConnectionString))
            {
                using (var command = new MySqlCommand(sql, Connection))
                {
                    Connection.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dataTable);
                    Connection.Close();
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Execute Inserts, Updates, Deletes
        /// </summary>
        /// <param name="sql"></param>
        public static void ExecutarComandoSql(string sql)
        {
            using (var Connection = new MySqlConnection(ConnectionString))
            {
                using (var command = new MySqlCommand(sql, Connection))
                {
                    Connection.Open();
                    command.ExecuteNonQuery();
                    Connection.Close();
                }
            }

        }
    }
}
