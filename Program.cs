using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFbFramework019
{
    class Program
    {
        static void Main(string[] args)
        {
            connToDBsimple();

            connToDBEntity();
        }


        public static DbContext  connToDBEntity()
        {
            string strExePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();


            dbConnectionStringBuilder["Data Source"] = "localhost";
            //dbConnectionStringBuilder["Initial Catalog"] = @"C:\SSG\PROJECTs\TELET\DB4TELEFONE\sampd_cexs.fdb";//"sampd_cexs";
            dbConnectionStringBuilder["Database"] = Path.Combine(strExePath, "sampd_cexs.fdb");

            dbConnectionStringBuilder["User ID"] = "sysdba";
            dbConnectionStringBuilder["Password"] = "masterkey";

            dbConnectionStringBuilder["Charset"] = "UTF8";


            dbConnectionStringBuilder["Embedded"] = FbServerType.Embedded;
            //dbConnectionStringBuilder["integrated Security"] = "SSPI";
            string sCon = dbConnectionStringBuilder.ConnectionString;

            using (var dbContent = new DbAppContext(sCon))
            {

                var simpleQueryOfVidConnects = dbContent.pO_TEL_VID_CONNECTs.Where(s => s.Id > 0);



                Console.WriteLine("=================================================");
                foreach (var oneElement in simpleQueryOfVidConnects)
                {
                    Console.WriteLine(" Id = {0}  Kod связи {1}  Название вида связи {2}", oneElement.Id, oneElement.KodOfConnect, oneElement.Name);
                }
                Console.WriteLine("=================================================");


                return dbContent;
            }
            return null;

        }


        public static int connToDBsimple()
        {

            string strExePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder(); //new SqlConnectionStringBuilder();

            dbConnectionStringBuilder["Data Source"] = "localhost";
            //dbConnectionStringBuilder["Initial Catalog"] = @"C:\SSG\PROJECTs\TELET\DB4TELEFONE\sampd_cexs.fdb";//"sampd_cexs";
            dbConnectionStringBuilder["Database"] = Path.Combine(strExePath, "sampd_cexs.fdb");

            dbConnectionStringBuilder["User ID"] = "sysdba";
            dbConnectionStringBuilder["Password"] = "masterkey";

            dbConnectionStringBuilder["Charset"] = "UTF8";


            dbConnectionStringBuilder["Embedded"] = FbServerType.Embedded;
            //dbConnectionStringBuilder["integrated Security"] = "SSPI";
            string sCon = dbConnectionStringBuilder.ConnectionString;

            bool correctConnectionToDB = false;
            int  retCount  = 0;


            using (DbConnection dbConnection = new FbConnection(sCon))
            {
                try
                {
                    dbConnection.Open();
                    string sSqlSelect = "SELECT * FROM TEL_VID_CONNECT;";
                    //SqlCommand sqlCommand = new SqlCommand(sSqlSelect, (SqlConnection)dbConnection);
                    FbCommand sqlCommand = new FbCommand(sSqlSelect, (FbConnection)dbConnection);


                    FbDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        retCount++;
                        Console.WriteLine("\t{0}\t{1}\t{2}", sqlDataReader[0], sqlDataReader[1], sqlDataReader[2]);
                    }


                    dbConnection.Close();
                    correctConnectionToDB = true;
                }
                catch (SqlException sqlEx)
                {
                    ;
                }
            }



            return retCount;
        }



    }
}
