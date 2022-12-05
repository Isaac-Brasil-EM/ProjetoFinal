using FirebirdSql.Data.FirebirdClient;
//using Grpc.Core;
using Microsoft.VisualBasic.CompilerServices;
using MVCAlunos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAlunos.Data.DAL
{
    public class DAL
    {
        private static string User = "SYSDBA";
        private static string Password = "masterkey";
        private static string Database = "localhost:D:\\DBALUNO.fdb";
        private static int Port = 3054;
        private static string Dialect = "3";
        private static string Charset = FbCharset.None.ToString();

        private FbConnection connection;
        public DAL()
        {

            try
            {
                FbConnectionStringBuilder conn = new FbConnectionStringBuilder()
                {
                    Port = Port,
                    Password = Password,
                    Database = Database,
                    UserID = User,
                    Charset = Charset,

                };



                connection = new FbConnection(conn.ToString());
                connection.Open();
            }
            catch (Exception EX)
            {
                throw EX;
            }

        }

        //faz selects 

        public DataTable RetDataTable(string sql)
        {
            DataTable dataTable = new DataTable();
            FbCommand Comando = new FbCommand(sql, connection);
            FbDataAdapter da = new FbDataAdapter(Comando);
            da.Fill(dataTable);
            return dataTable;
        }



        public void DelDataTable(string sql)
        {
            DataTable dataTable = new();
            FbCommand Comando = new FbCommand(sql, connection);
            Comando.Parameters.Clear();
            //Comando.Parameters.Add("@matricula", tx_nome_cliente.Text);
            FbDataAdapter da = new FbDataAdapter(Comando);
            Comando.ExecuteNonQuery();
        }

        /*   public void CreateCommand(List<LAlunosViewModel> listaAlunos) {
               using (var connection = new FbConnection("database=localhost:dbaluno.fdb;user=sysdba;password=masterkey"))
               {
                   connection.Open();
                   using (var transaction = connection.BeginTransaction())
                   {
                       using (var command = new FbCommand("select * from demo", connection, transaction))
                       {
                           using (var reader = command.ExecuteReader())
                           {
                               while (reader.Read())
                               {
                                   int codigo = reader.GetInt32(1);
                                   string nome = reader.GetString(2);
                                   listaAlunos.Add(new Aluno { Matricula = codigo, Nome = nome });
                               }
                           }
                       }
                   }
               }
           }*/

        #region Desativados
        //public void ExecutaComandoSQL(string sql)
        //{
        //    MySqlCommand command = new MySqlCommand(sql, connection);
        //    command.ExecuteNonQuery();
        //}
        #endregion


    }
}