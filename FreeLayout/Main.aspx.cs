using FreeLayout.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreeLayout
{
    public partial class Main : System.Web.UI.Page
    {
        DataConn cnn = new DataConn();
        public DataTable dt1 = new DataTable();
        public DataTable dt2 = new DataTable();
        public DataTable dt3 = new DataTable();
        public DataTable dt4 = new DataTable();
        public DataTable dt5 = new DataTable();

        public DataTable dt_test = new DataTable();
        public DataTable dt_test2 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    string script = "$(document).ready(function () { $('[id*=bttLoadTime]').click(); });";
            //    ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            //}

                //string date = Request.Form[datepicker.UniqueID];
            if (Request.QueryString["material"] is null)
            {
                mahang.Text = "";
            }
            else
            {
                string _material = Request.QueryString["material"];
                mahang.Text = _material;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "search_material2();", true);
            }

            

            dt1 = DataConn.StoreFillDS("Get_Position_MCS_All_new", System.Data.CommandType.StoredProcedure);
            dt2 = DataConn.StoreFillDS("Get_Position_MCS_All2_new", System.Data.CommandType.StoredProcedure);
            dt3 = DataConn.StoreFillDS("Get_Position_MCS_All3_new", System.Data.CommandType.StoredProcedure);
            dt4 = DataConn.StoreFillDS("Get_Position_MCS_All4_new", System.Data.CommandType.StoredProcedure);

            dt5 = DataConn.StoreFillDS("Get_Position_MCS_All5_new", System.Data.CommandType.StoredProcedure);

            dt_test = DataConn.StoreFillDS("Get_mater_list_MCS_urgent", System.Data.CommandType.StoredProcedure);
            dt_test2 = DataConn.StoreFillDS("Get_mater_list_MCS_urgent2", System.Data.CommandType.StoredProcedure);

            //DataSet ds1 = obj.GetRecord(); /*this obj is referring to some class in which GetRecord method is present which return the record from database. You can write your //own class and method.*/
            string s1;
            string s2;
            //s1 = "<table><tr><td>";
            s1 = "";
            s2 = "";

            for (int i = 0; i < dt_test.Rows.Count; i++)
            {
                //s1 += "<a style=font-family: fantasy; font-size: large; font-weight:bold; font-style: normal; color: #660066>" + dt_test.Rows[i][1].ToString() + "</a></td>";
                //s1 += "<br/>";                
                s1 += "<a href='#' style='font-family: tahoma; font-size: 24px; font-style: normal; color: #006600'> &nbsp;" + dt_test.Rows[i][1].ToString() +
                "<a href='#' style='font-family: tahoma; font-size: 24px; font-style: normal; color: #FF3333'>  =>&nbsp; Position :" + dt_test.Rows[i][2].ToString() + "</a>";                                        
                s1 += "<br/>";
            }                      
            lt1.Text = s1.ToString();
            //lt2.Text = s2.ToString();


            //if (Request.QueryString["material"] != "")
            //{                
            //    dt1 = DataConn.StoreFillDS("Get_Position_MCS_All", System.Data.CommandType.StoredProcedure);
            //    dt2 = DataConn.StoreFillDS("Get_Position_MCS_All2", System.Data.CommandType.StoredProcedure);
            //    dt3 = DataConn.StoreFillDS("Get_Position_MCS_All3", System.Data.CommandType.StoredProcedure);
            //    dt4 = DataConn.StoreFillDS("Get_Position_MCS_All4", System.Data.CommandType.StoredProcedure);
            //    string _material = Request.QueryString["material"];
            //    mahang.Text = _material;
            //}
            //else
            //{
            //    dt1 = DataConn.StoreFillDS("Get_Position_MCS_All", System.Data.CommandType.StoredProcedure);
            //    dt2 = DataConn.StoreFillDS("Get_Position_MCS_All2", System.Data.CommandType.StoredProcedure);
            //    dt3 = DataConn.StoreFillDS("Get_Position_MCS_All3", System.Data.CommandType.StoredProcedure);
            //    dt4 = DataConn.StoreFillDS("Get_Position_MCS_All4", System.Data.CommandType.StoredProcedure);
            //}
        }

        //public void bttLogout_Click(object sender, EventArgs e)
        //{
        //    Session.Clear();
        //    Session.Abandon();
        //    Session.RemoveAll();
        //}

        //protected void bttSearch_Click(object sender, EventArgs e)
        //{
        //    string dt = Request.Form[datepicker.UniqueID];

        //    //dt_gr = DataConn.SQLFillDS("pro_gr_mcs_control1", CommandType.StoredProcedure, dt);
        //    dt1 = DataConn.StoreFillDS("Get_Position_MCS_All", System.Data.CommandType.StoredProcedure);
        //    dt2 = DataConn.StoreFillDS("Get_Position_MCS_All2", System.Data.CommandType.StoredProcedure);
        //    dt3 = DataConn.StoreFillDS("Get_Position_MCS_All3", System.Data.CommandType.StoredProcedure);
        //    dt4 = DataConn.StoreFillDS("Get_Position_MCS_All4", System.Data.CommandType.StoredProcedure);
        //}

        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    dt1 = DataConn.StoreFillDS("Get_Position_MCS_All", System.Data.CommandType.StoredProcedure);
        //    dt2 = DataConn.StoreFillDS("Get_Position_MCS_All2", System.Data.CommandType.StoredProcedure);
        //    dt3 = DataConn.StoreFillDS("Get_Position_MCS_All3", System.Data.CommandType.StoredProcedure);
        //    dt4 = DataConn.StoreFillDS("Get_Position_MCS_All4", System.Data.CommandType.StoredProcedure);

        //    dt5 = DataConn.StoreFillDS("Get_Position_MCS_All5", System.Data.CommandType.StoredProcedure);
        //}

        //protected void bttLoadTime_Click(object sender, EventArgs e)
        //{

        //    System.Threading.Thread.Sleep(1000);            
        //}

        [WebMethod]
        public static string GetMaHang(string vitri)
        {
            String daresult = null;
            DataTable yourDatable = new DataTable();
            DataSet ds = new DataSet();

            //yourDatable = DataConn.StoreFillDS("Get_List_Material_MCS", System.Data.CommandType.StoredProcedure, vitri);
            yourDatable = DataConn.StoreFillDS("Get_List_Material_MCS_new", System.Data.CommandType.StoredProcedure, vitri);

            DataTable dt2 = new DataTable();
            dt2 = yourDatable.Copy();

            ds.Tables.Add(dt2);
            daresult = DataSetToJSON(ds);
            return daresult;

        }

        public static string DataSetToJSON(DataSet ds)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (DataTable dt in ds.Tables)
            {
                object[] arr = new object[dt.Rows.Count + 1];

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    arr[i] = dt.Rows[i].ItemArray;
                }

                //dict.Add(dt.TableName, arr);
                dict.Add(dt.TableName, arr);
            }

            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(dict);
        }

        [WebMethod]
        public static string Checkmahang(string mahang)
        {
            DataTable dt = new DataTable();

            dt = DataConn.StoreFillDS("Get_Position_MCS", System.Data.CommandType.StoredProcedure, mahang);

            DataTable dt2 = new DataTable();
            dt2 = dt.Copy();

            String daresult = null;
            DataSet ds = new DataSet();
            ds.Tables.Add(dt2);
            daresult = DataSetToJSON(ds);
            return daresult;
        }

        [WebMethod]
        public static string checkvendor(string vendor)
        {
            DataTable dt = new DataTable();

            dt = DataConn.StoreFillDS("Get_Position_Vendor", System.Data.CommandType.StoredProcedure, vendor);

            DataTable dt2 = new DataTable();
            dt2 = dt.Copy();

            String daresult = null;
            DataSet ds = new DataSet();
            ds.Tables.Add(dt2);
            daresult = DataSetToJSON(ds);
            return daresult;
        }


    }
}