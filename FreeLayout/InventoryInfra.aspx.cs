using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreeLayout.App_Code;

namespace FreeLayout
{
    public partial class InventoryInfra : System.Web.UI.Page
    {
        DataConn cnn = new DataConn();
        public DataTable dt = new DataTable();
        public DataTable dt1 = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dtprocess = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtcate = DataConn.StoreFillDS("Get_DM_NhomHang_infra", System.Data.CommandType.StoredProcedure);
                DataRow newRow2 = dtcate.NewRow();
                newRow2["DeviceTypeID"] = "==select==";
                dtcate.Rows.InsertAt(newRow2, 0);
                dr_filter_cate.DataSource = dtcate;
                dr_filter_cate.DataBind();


                dtprocess = DataConn.StoreFillDS("Get_DM_ProcessID_new", System.Data.CommandType.StoredProcedure);
                DataRow newRow3 = dtprocess.NewRow();
                newRow3["ProcessID"] = "==Select==";
                dtprocess.Rows.InsertAt(newRow3, 0);
                dr_filter_process.DataSource = dtprocess;
                dr_filter_process.DataBind();

                string year_now = DateTime.Now.Year.ToString();
                string month_now = DateTime.Now.Month.ToString();
                txtYear.Value = year_now;

                drMonth.Text = month_now;
            }
            dt = DataConn.StoreFillDS("Get_DM_GR_Device_infra", System.Data.CommandType.StoredProcedure);
        }

        protected void filter_cate_Change(object sender, EventArgs e)
        {
            string nhomhang = dr_filter_cate.Text;
            string processid = dr_filter_process.Text;
            string year_ = DateTime.Now.Year.ToString();

            if (dr_filter_cate.Text == "==Select==")
            {
                dt = DataConn.StoreFillDS("Get_DM_GR_Device_infra", System.Data.CommandType.StoredProcedure);
            }
            else
            {
                dt = DataConn.StoreFillDS("Get_Infra_report_nhomhang2_web", System.Data.CommandType.StoredProcedure, nhomhang, year_);
            }            
        }

        protected void filter_process_Change(object sender, EventArgs e)
        {
            string processid = dr_filter_process.Text;
            string year_ = DateTime.Now.Year.ToString();

            if (dr_filter_process.Text == "==Select==")
            {
                dt = DataConn.StoreFillDS("Get_DM_GR_Device_infra", System.Data.CommandType.StoredProcedure);
            }
            else
            {
                dt = DataConn.StoreFillDS("Get_DM_ProcessID_filter2_web", System.Data.CommandType.StoredProcedure, processid, year_);
            }

            
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _date = Request.Form[datepicker.UniqueID];
            string _checkpartno = Request.Form["check_partno_search"];
            string _partno = partno_search.Value.ToString();
            //filter_type.Text = "";

            if (_checkpartno == "on")
            {
                //loc theo ma
                //string nam = _date.Substring(6, 4);
                //string thang = _date.Substring(3, 2);
                //string ngay = _date.Substring(0, 2);
                //string _date2 = nam + "-" + thang + "-" + ngay;

                string tang = txtFloor.Value.ToString();
                string khuvuc = txtarea.Value.ToString();
                string ban = txttable.Value.ToString();
                string ManagementID = _partno;
                string _ProcessID = dr_filter_process.Text.ToString();
                string nhomhang = dr_filter_cate.Text.ToString();
                string Serial = txtserial.Value.ToString();

                string _nam = txtYear.Value.ToString();
                string _thang = drMonth.Text.ToString();
                string _ngay = "";
                                
                //dt = DataConn.StoreFillDS("Get_mater_list_QC", System.Data.CommandType.StoredProcedure);
                dt = DataConn.StoreFillDS("Get_Infra_report_2_web", System.Data.CommandType.StoredProcedure, ManagementID, Serial, nhomhang, tang, khuvuc, ban, _nam, _thang, _ngay, _ProcessID);
                //datepicker.Value = ngay + "-" + thang + "-" + nam;

            }
            else
            {
                //loc theo ngay
                if (_date == "")
                {
                    dt = DataConn.StoreFillDS("Get_DM_GR_Device_infra", System.Data.CommandType.StoredProcedure);
                }
                else
                {
                    string tang = txtFloor.Value.ToString();
                    string khuvuc = txtarea.Value.ToString();
                    string ban = txttable.Value.ToString();
                    string ManagementID = _partno;
                    string _ProcessID = dr_filter_process.Text.ToString();
                    string nhomhang = dr_filter_cate.Text.ToString();
                    string Serial = txtserial.Value.ToString();

                    string _nam = txtYear.Value.ToString();
                    string _thang = drMonth.Text.ToString();
                    string _ngay = "";

                    //dt = DataConn.StoreFillDS("Get_mater_list_QC", System.Data.CommandType.StoredProcedure);
                    dt = DataConn.StoreFillDS("Get_Infra_report_2_web", System.Data.CommandType.StoredProcedure, ManagementID, Serial, nhomhang, tang, khuvuc, ban, _nam, _thang, _ngay, _ProcessID);
                    //datepicker.Value = ngay + "-" + thang + "-" + nam;
                }
            }
        }

        public void Download_Click(object sender, EventArgs e)
        {
            DataTable dt_dowload = new DataTable();

            dt_dowload = DataConn.StoreFillDS("Get_DM_GR_Device_infra", CommandType.StoredProcedure);

            //dt_test = DataConn.StoreFillDS("Export_Combine_normalform", CommandType.StoredProcedure, typefilter);
            //dt_test.Columns.Add(new DataColumn("timeinsert", typeof(string)));

            //foreach (DataRow dr in dt_test.Rows)
            //{
            //    string _material = dr["mahang"].ToString();
            //    string _plan = dr["Plant"].ToString();
            //    string _invoice = dr["Invoice"].ToString();
            //    dtgettime = DataConn.StoreFillDS("Export_Combine_gettime", CommandType.StoredProcedure, _material, _plan, _invoice);
            //    if (dtgettime.Rows[0][0].ToString() == "1")
            //    {
            //        dr["timeinsert"] = dtgettime.Rows[0][1];
            //    }
            //    else
            //    {
            //        dr["timeinsert"] = "";
            //    }
            //}

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Baocaoinventory.xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";

            //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            //response.Clear();
            //response.Buffer = true;
            //response.Charset = "";
            //response.ContentType = "text/csv";
            //response.AddHeader("Content-Disposition", "attachment;filename=myfilename.csv");

            if (dt_dowload != null)
            {
                foreach (DataColumn dc in dt_dowload.Columns)
                {
                    Response.Write(dc.ColumnName + "\t");

                }
                Response.Write(System.Environment.NewLine);
                foreach (DataRow dr in dt_dowload.Rows)
                {
                    for (int i = 0; i < dt_dowload.Columns.Count; i++)
                    {
                        Response.Write(dr[i].ToString() + "\t");
                    }
                    Response.Write("\n");
                }
            }
            Response.End();  //must this sentence



        }



    }
}