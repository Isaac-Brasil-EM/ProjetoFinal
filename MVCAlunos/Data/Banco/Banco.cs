using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAlunos.Data.Banco
{
    public class Banco
    {
        private static string pUser = "SYSDBA";
        private static string pPassword = "masterkey";
        private static string pDatabase = "localhost:D:\\DBALUNO.fdb";
        private static int pPort = 3054;
        private static int pDialect = 3;
        private static string pCharset = FbCharset.Utf8.ToString();
        public bool bconexao { get; set; }
        private FbConnection connection;

        public Banco()
        {
            FbConnectionStringBuilder stringconection = new()
            {
                Port = pPort,
                UserID = pUser,
                Password = pPassword,
                Database = pDatabase,
                Dialect = pDialect,
                Charset = pCharset

            };
            try
            {
                connection = new FbConnection(stringconection.ToString());
                connection.Open();
                bconexao = true;
            }
            catch (Exception ex)
            {
                bconexao = false;

                //throw;
            }

        }

        public DataTable RetornoTabela(string select)
        {
            DataTable tabela = new ();
            FbCommand comando = new (select, connection);
            FbDataAdapter fbda = new(comando);
            fbda.Fill(tabela);
            return tabela;

        }

    }
}