﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime Data_Nascimento { get; set; }

        public bool ValidarLogin()
        {
            string sql = $"SELECT ID, NOME, DATA_NASCIMENTO FROM USUARIO WHERE EMAIL = '{Email}' AND SENHA='{Senha}'";

            var dt = Util.DAL.RetDataTable(sql);
            if (dt.Rows.Count == 1)
            {
                Id = int.Parse(dt.Rows[0]["ID"].ToString());
                Nome = dt.Rows[0]["NOME"].ToString();
                Data_Nascimento = DateTime.Parse(dt.Rows[0]["DATA_NASCIMENTO"].ToString());

                return true;
            }

            return false;
        }
    }
}
