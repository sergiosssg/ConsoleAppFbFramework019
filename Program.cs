using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            string strConnection2FB = makeCoonectionString2FB();



            firebirdDBinfo(strConnection2FB);

            connToDBsimple(strConnection2FB);

            connToDBEntity(strConnection2FB);
        }


        private static DbContext connToDBEntity(string sConnectionString)
        {
            try
            {
                using (var dbContent = new DbAppContext(sConnectionString))
                {



                    /*                    var simpleQueryOfVidConnects = dbContent.pO_TEL_VID_CONNECTs.Where(s => s.Id > 0);


                                        Console.WriteLine("=================================================");
                                        foreach (var oneElement in simpleQueryOfVidConnects)
                                        {
                                            Console.WriteLine(" Id = {0}  Kod связи {1}  Название вида связи {2}", oneElement.Id, oneElement.KodOfConnect, oneElement.Name);
                                        }
                                        Console.WriteLine("=================================================");*/



                    var dataTable = GetProviderFactoryClasses();

                    var simpleVidConnects = dbContent.pO_TEL_VID_CONNECTs;


                    foreach(var oneTEL_VID_CONNECT in simpleVidConnects)
                    {
                        Console.WriteLine(" Id = {0}  Kod связи {1}  Название вида связи {2}", oneTEL_VID_CONNECT.Id, oneTEL_VID_CONNECT.KodOfConnect, oneTEL_VID_CONNECT.Name);

                    }



                    return dbContent;
                }

            }
            catch (Exception ex)
            {

            }
            return null;
        }


        private static int connToDBsimple(string sConnectionString)
        {
            bool correctConnectionToDB = false;
            int retCount = 0;


            using (DbConnection dbConnection = new FbConnection(sConnectionString))
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

        private static void firebirdDBinfo(string strConnection)
        {
            using (DbConnection dbConnection = new FbConnection(strConnection))
            {
                try
                {
                    FbDatabaseInfo dbInfo;

                    if (dbConnection.State == System.Data.ConnectionState.Closed)
                    {
                        dbConnection.Open();
                        dbInfo = new FbDatabaseInfo((FbConnection)dbConnection);
                        /*
                        Console.WriteLine("Server Version: {0}", dbInfo.GetServerVersionAsync);
                        Console.WriteLine("ISC Version : {0}", dbInfo.IscVersion);
                        Console.WriteLine("Server Class : {0}", dbInfo.ServerClass);
                        Console.WriteLine("Max memory : {0}", dbInfo.MaxMemory);
                        Console.WriteLine("Current memory : {0}", dbInfo.CurrentMemory);
                        Console.WriteLine("Page size : {0}", dbInfo.PageSize);
                        Console.WriteLine("ODS Mayor version : {0}", dbInfo.OdsVersion);
                        Console.WriteLine("ODS Minor version : {0}", dbInfo.OdsMinorVersion);
                        Console.WriteLine("Allocation pages: {0}", dbInfo.AllocationPages);
                        Console.WriteLine("Base level: {0}", dbInfo.BaseLevel);
                        Console.WriteLine("Database id: {0}", dbInfo.DbId);
                        Console.WriteLine("Database implementation: {0}", dbInfo.Implementation);
                        Console.WriteLine("No reserve: {0}", dbInfo.NoReserve);
                        Console.WriteLine("Forced writes: {0}", dbInfo.ForcedWrites);
                        Console.WriteLine("Sweep interval: {0}", dbInfo.SweepInterval);
                        Console.WriteLine("Number of page fetches: {0}", dbInfo.Fetches);
                        Console.WriteLine("Number of page marks: {0}", dbInfo.Marks);
                        Console.WriteLine("Number of page reads: {0}", dbInfo.Reads);
                        Console.WriteLine("Number of page writes: {0}", dbInfo.Writes);
                        Console.WriteLine("Removals of a version of a record: {0}", dbInfo.BackoutCount);
                        Console.WriteLine("Number of database deletes: {0}", dbInfo.DeleteCount);
                        Console.WriteLine("Number of removals of a record and all of its ancestors: {0}", dbInfo.ExpungeCount);
                        Console.WriteLine("Number of inserts: {0}", dbInfo.InsertCount);
                        Console.WriteLine("Number of removals of old versions of fully mature records: {0}", dbInfo.PurgeCount);
                        Console.WriteLine("Number of reads done via an index: {0}", dbInfo.ReadIdxCount);
                        Console.WriteLine("Number of sequential sequential table scans: {0}", dbInfo.ReadSeqCount);
                        Console.WriteLine("Number of database updates: {0}", dbInfo.UpdateCount);
                        Console.WriteLine("Database size in pages: {0}", dbInfo.DatabaseSizeInPages);
                        Console.WriteLine("Number of the oldest transaction: {0}", dbInfo.OldestTransaction);
                        Console.WriteLine("Number of the oldest active transaction: {0}", dbInfo.OldestActiveTransaction);
                        Console.WriteLine("Number of the oldest active snapshot: {0}", dbInfo.OldestActiveSnapshot);
                        Console.WriteLine("Number of the next transaction: {0}", dbInfo.NextTransaction);
                        Console.WriteLine("Number of active transactions: {0}", dbInfo.ActiveTransactions);
                        */
                        dbConnection.Close();
                    }
                }
                catch (SqlException sqlEx)
                {

                }
            }



        }


        private static string makeCoonectionString2FB()
        {

            string strExePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();


            //dbConnectionStringBuilder["ClientLibrary"] = @"C:\Program Files\Firebird\Firebird_2_5\bin\fbclient.dll";

            dbConnectionStringBuilder["Data Source"] = "localhost";
            //dbConnectionStringBuilder["Initial Catalog"] = @"C:\SSG\PROJECTs\TELET\DB4TELEFONE\sampd_cexs.fdb";//"sampd_cexs";
            dbConnectionStringBuilder["Database"] = Path.Combine(strExePath, "tmp.fdb");

            dbConnectionStringBuilder["User ID"] = "sysdba";
            dbConnectionStringBuilder["Password"] = "masterkey";

            dbConnectionStringBuilder["Charset"] = "UTF8";


            dbConnectionStringBuilder["Embedded"] = FbServerType.Embedded;
            //dbConnectionStringBuilder["integrated Security"] = "SSPI";

            return dbConnectionStringBuilder.ConnectionString;

        }



        private static DataTable GetProviderFactoryClasses()
        {
            // Retrieve the installed providers and factories.
            DataTable table = DbProviderFactories.GetFactoryClasses();
            //var tableQueriable = from ee in table.Rows where true select ee;
            // Display each row and column value.
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine(" \t {0}",row[column]);
                }
            }
            return table;
        }

    }
}
