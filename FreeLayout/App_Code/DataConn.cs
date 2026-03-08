using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.Common;
using System.Data.Odbc;
using System.Configuration;
using System.Net.Mail;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
namespace FreeLayout.App_Code
{
    public class DataConn
    {
        public static string source;
        public static string source2;
        public static string source3;
        private static SqlConnection con;
        public static int gio;
        public static DbDataAdapter adapter;
        static DataConn()
        {                    
            
            source = @"Data Source=10.92.184.22\hienpc;Initial Catalog=LichTau;User ID=sa;Password=Hien304@";
            //source = @"Data Source=LT-DE2302026;Initial Catalog=LichTau;Integrated Security=True;TrustServerCertificate=True;";

            source2 = @"Data Source=192.168.128.1;Initial Catalog=ScrapSystem;User ID=sa;Password=Psnvdb2013";
            //local  //DESKTOP-P69S4E5
            //source2 = @"Data Source=LT-DE2302026;Initial Catalog=ScrapSystem;Integrated Security=True;TrustServerCertificate=True;";


            //source3 = @"Data Source=.30;Initial Catalog=Issue_MaterialInOut;User ID=sa;Password=Psnvdb2013";
            source3 = @"Data Source=192.168.128.1;Initial Catalog=Issue_MaterialInOut;User ID=sa;Password=Psnvdb2013";

            con = new SqlConnection(source);
            try
            {
                con.Open();
            }
            catch
            {
            }
        }

        public static DataTable DataTable_Sql(string sql)
        {
            using (SqlConnection conn = new SqlConnection(source))
            {
                using (SqlDataAdapter dap = new SqlDataAdapter(sql, conn))
                {
                    using (DataSet ds = new DataSet())
                    {
                        dap.Fill(ds);
                        conn.Close();
                        conn.Dispose();
                        return ds.Tables[0];
                    }
                }
            }
        }

        

        public static int Execute_NonSQL(string sql)
        {
            SqlTransaction sqltran = null;
            //try
            //{
            SqlConnection conn = new SqlConnection(source);

            int row = 0;
            conn.Open();
            sqltran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand(sql, conn, sqltran);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            row = cmd.ExecuteNonQuery();
            sqltran.Commit();
            conn.Dispose();
            conn.Close();
            return row;
            //}
            //catch (Exception ex)
            //{
            //    // throw new Exception(ex.Message);
            //    sqltran.Rollback();
            //    var _ex = new Exception(ex.Message + "Chỗ này sai rồi... : '" + sql + "'");
            //    throw _ex;
            //}
        }

        public static int Execute_NonSQL3(string sql)
        {
            SqlTransaction sqltran = null;
            //try
            //{
            SqlConnection conn = new SqlConnection(source3);

            int row = 0;
            conn.Open();
            sqltran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand(sql, conn, sqltran);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            row = cmd.ExecuteNonQuery();
            sqltran.Commit();
            conn.Dispose();
            conn.Close();
            return row;
            //}
            //catch (Exception ex)
            //{
            //    // throw new Exception(ex.Message);
            //    sqltran.Rollback();
            //    var _ex = new Exception(ex.Message + "Chỗ này sai rồi... : '" + sql + "'");
            //    throw _ex;
            //}
        }

        public static DataTable StoreFillDS(string query_object, CommandType type, params object[] obj)
        {
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query_object, conn);
            cmd.CommandType = type;
            SqlCommandBuilder.DeriveParameters(cmd);
            for (int i = 1; i <= obj.Length; i++)
            {
                cmd.Parameters[i].Value = obj[i - 1];
            }
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            conn.Dispose();
            conn.Close();
            return ds.Tables[0];
        }

        public static DataTable StoreFillDS2(string query_object, CommandType type, params object[] obj)
        {
            SqlConnection conn = new SqlConnection(source2);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query_object, conn);
            cmd.CommandType = type;
            SqlCommandBuilder.DeriveParameters(cmd);
            for (int i = 1; i <= obj.Length; i++)
            {
                cmd.Parameters[i].Value = obj[i - 1];
            }
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            conn.Dispose();
            conn.Close();
            return ds.Tables[0];
        }

        /*Nguyen Hien*/
        //Store Procedure tra ve datatable
        public static int ExecuteStore(string query_object, CommandType type, params object[] obj)
        {
            int row = 0;
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query_object, conn);
            cmd.CommandType = type;
            SqlCommandBuilder.DeriveParameters(cmd);
            for (int i = 1; i <= obj.Length; i++)
            {
                cmd.Parameters[i].Value = obj[i - 1];
            }
            cmd.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();
            return row;
        }
        /*Tien Chung*/
        //Store Procedure tra ve datatable
        public static DataTable FillStore(string storename, CommandType type, params object[] obj)
        {
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            SqlCommand cmd = new SqlCommand(storename, conn);
            cmd.CommandType = type;
            SqlCommandBuilder.DeriveParameters(cmd);
            for (int i = 1; i <= obj.Length; i++)
            {
                cmd.Parameters[i].Value = obj[i - 1];
            }
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            //cmd.ExecuteNonQuery();
            dap.Fill(ds);
            conn.Dispose();
            conn.Close();
            return ds.Tables[0];
        }



    }
}