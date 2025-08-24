using FreeLayout.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreeLayout
{
    public partial class EquipmentBorrowHistory : System.Web.UI.Page
    {
        DataConn cnn = new DataConn();
        public DataTable dt_history = new DataTable();
        //public DataTable dt1 = new DataTable();
        public DataTable dtcate = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtcate = DataConn.StoreFillDS("Get_Type_device_IT", System.Data.CommandType.StoredProcedure);
                DataRow newRow2 = dtcate.NewRow();
                newRow2["DESCRIPTION"] = "==select==";
                dtcate.Rows.InsertAt(newRow2, 0);
                dr_filter_cate.DataSource = dtcate;
                dr_filter_cate.DataBind();

                dt_history = DataConn.StoreFillDS("Get_history_device_borrow", System.Data.CommandType.StoredProcedure);
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
                string nam = _date.Substring(6, 4);
                string thang = _date.Substring(3, 2);
                string ngay = _date.Substring(0, 2);
                string _date2 = nam + "-" + thang + "-" + ngay;

                dt_history = DataConn.StoreFillDS("Get_mater_device_borrow_part2", System.Data.CommandType.StoredProcedure, _partno);
                datepicker.Value = ngay + "-" + thang + "-" + nam;

            }
            else
            {
                //loc theo ngay
                if (_date == "")
                {
                    dt_history = DataConn.StoreFillDS("Get_history_device_borrow", System.Data.CommandType.StoredProcedure);
                }
                else
                {
                    string nam = _date.Substring(6, 4);
                    string thang = _date.Substring(3, 2);
                    string ngay = _date.Substring(0, 2);
                    string _date2 = nam + "-" + thang + "-" + ngay;

                    string _cate = dr_filter_cate.Text;
                    string typefilter = "all";



                    dt_history = DataConn.StoreFillDS("Get_history_device_borrow_date", System.Data.CommandType.StoredProcedure, _date2);
                    datepicker.Value = ngay + "-" + thang + "-" + nam;
                }
            }
        }

        protected void filter_cate_Change(object sender, EventArgs e)
        {
            string _cate = dr_filter_cate.Text;
            dt_history = DataConn.StoreFillDS("Get_history_device_borrow_cate", System.Data.CommandType.StoredProcedure, _cate);
        }

        public void Download_Click2(object sender, EventArgs e)
        {
            string _itemid = itemid.Value.ToString();
            Response.Redirect("ReportBorrowReturn.aspx?itemid='"+ _itemid + "' ");
        }
            public void Download_Click(object sender, EventArgs e)
        {
            DataTable dt_dowload = new DataTable();
            if (dr_filter_cate.Text == "==select==")
            {
                dt_dowload = DataConn.StoreFillDS("Get_history_device_borrow", CommandType.StoredProcedure);
            }
            else
            {
                string _cate = dr_filter_cate.Text;
                dt_dowload = DataConn.StoreFillDS("Get_history_device_borrow_cate", System.Data.CommandType.StoredProcedure, _cate);
            }
                    

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Baocao_lichsu_muon.xls");
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