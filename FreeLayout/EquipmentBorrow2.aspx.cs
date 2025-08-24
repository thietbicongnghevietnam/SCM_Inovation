using FreeLayout.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreeLayout
{
    public partial class EquipmentBorrow2 : System.Web.UI.Page
    {
        DataConn cnn = new DataConn();
        public DataTable dt = new DataTable();
        public DataTable dt1 = new DataTable();
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

                //load ly do muon
                DataTable dt_lydo = new DataTable();
                dt_lydo = DataConn.DataTable_Sql("select Title from tblReason_borrow");
                dr_lydo.DataSource = dt_lydo;
                dr_lydo.DataBind();
                //load bo phan
                DataTable dt_bophan = new DataTable();
                dt_bophan = DataConn.DataTable_Sql("select Maphongban from tblPhongban");
                dr_phongban.DataSource = dt_bophan;
                dr_phongban.DataBind();
            }

            dt = DataConn.StoreFillDS("Get_mater_device_borrow", System.Data.CommandType.StoredProcedure);
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

                //dt = DataConn.StoreFillDS("Get_mater_list_QC", System.Data.CommandType.StoredProcedure);
                dt = DataConn.StoreFillDS("Get_mater_device_borrow_part", System.Data.CommandType.StoredProcedure, _partno);
                datepicker.Value = ngay + "-" + thang + "-" + nam;

            }
            else
            {
                //loc theo ngay
                if (_date == "")
                {
                    dt = DataConn.StoreFillDS("Get_mater_device_borrow", System.Data.CommandType.StoredProcedure);
                }
                else
                {
                    string nam = _date.Substring(6, 4);
                    string thang = _date.Substring(3, 2);
                    string ngay = _date.Substring(0, 2);
                    string _date2 = nam + "-" + thang + "-" + ngay;

                    string _cate = dr_filter_cate.Text;
                    string typefilter = "all";


                    //dt = DataConn.StoreFillDS("Get_mater_list_QC", System.Data.CommandType.StoredProcedure);
                    dt = DataConn.StoreFillDS("Get_mater_device_borrow_filter_cate", System.Data.CommandType.StoredProcedure, _date2, typefilter, _cate);
                    datepicker.Value = ngay + "-" + thang + "-" + nam;
                }
            }


        }

        protected void filter_cate_Change(object sender, EventArgs e)
        {
            string _cate = dr_filter_cate.Text;
            dt = DataConn.StoreFillDS("Get_mater_device_borrow_filter_onlycate", System.Data.CommandType.StoredProcedure, _cate);
        }

        protected void Filter_Pending_Click(object sender, EventArgs e)
        {
            string _date = Request.Form[datepicker.UniqueID];
            string _cate = dr_filter_cate.Text;
            //filter_type.Text = "checking";
            dr_filter_cate.Text = "==select==";

            string date_now = DateTime.Now.ToString("yyyy-MM-dd");
            string typefilter = "pending";
            //09-08-2021
            if (_date == "")
            {
                dt = DataConn.StoreFillDS("Get_mater_device_borrow_filter", System.Data.CommandType.StoredProcedure, typefilter, _cate);
            }
            else
            {
                string nam = _date.Substring(6, 4);
                string thang = _date.Substring(3, 2);
                string ngay = _date.Substring(0, 2);
                string _date2 = nam + "-" + thang + "-" + ngay;
                dt = DataConn.StoreFillDS("Get_mater_device_borrow_filter_cate", System.Data.CommandType.StoredProcedure, _date2, typefilter, _cate);
            }
        }

        protected void Filter_Finish_Click(object sender, EventArgs e)
        {
            string _date = Request.Form[datepicker.UniqueID];
            string _cate = dr_filter_cate.Text;
            //filter_type.Text = "checking";
            dr_filter_cate.Text = "==select==";

            string date_now = DateTime.Now.ToString("yyyy-MM-dd");
            string typefilter = "finish";
            //09-08-2021
            if (_date == "")
            {
                dt = DataConn.StoreFillDS("Get_mater_device_borrow_filter", System.Data.CommandType.StoredProcedure, typefilter, _cate);
            }
            else
            {
                string nam = _date.Substring(6, 4);
                string thang = _date.Substring(3, 2);
                string ngay = _date.Substring(0, 2);
                string _date2 = nam + "-" + thang + "-" + ngay;
                dt = DataConn.StoreFillDS("Get_mater_device_borrow_filter_cate", System.Data.CommandType.StoredProcedure, _date2, typefilter, _cate);
            }
        }

        protected void Filter_Order_Click(object sender, EventArgs e)
        {
            string _date = Request.Form[datepicker.UniqueID];
            string _cate = dr_filter_cate.Text;
            dr_filter_cate.Text = "==select==";

            string date_now = DateTime.Now.ToString("yyyy-MM-dd");
            string typefilter = "order";
            //09-08-2021
            if (_date == "")
            {
                string _date2 = DateTime.Now.ToString("yyyy-MM-dd");
                //dt = DataConn.StoreFillDS("Get_mater_device_borrow", System.Data.CommandType.StoredProcedure);
                dt = DataConn.StoreFillDS("Get_mater_device_borrow_order", System.Data.CommandType.StoredProcedure, _date2, typefilter, _cate);
            }
            else
            {
                string nam = _date.Substring(6, 4);
                string thang = _date.Substring(3, 2);
                string ngay = _date.Substring(0, 2);
                string _date2 = nam + "-" + thang + "-" + ngay;
                dt = DataConn.StoreFillDS("Get_mater_device_borrow_order", System.Data.CommandType.StoredProcedure, _date2, typefilter, _cate);
            }
        }

        public void Download_Click(object sender, EventArgs e)
        {
            DataTable dt_dowload = new DataTable();

            dt_dowload = DataConn.StoreFillDS("Get_mater_device_borrow", CommandType.StoredProcedure);

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
            Response.AddHeader("content-disposition", "attachment;filename=BaocaothietbiMuon.xls");
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

        public void BtnOrderItem(object sender, EventArgs e)
        {
            string userid = txtuserid.Text.ToString();
            string itemorder = txtdevice.Text.ToString();
            string lydo = dr_lydo.Text.ToString();
            string bophan = dr_phongban.Text.ToString();
            string _ngaymuon = Request.Form[txtngaymuon.UniqueID];//txtngaymuon.Text.ToString();
            string _ngaytra = Request.Form[txtngaytra.UniqueID];// txtngaytra.Text.ToString();

            string nam = _ngaymuon.Substring(6, 4);
            string thang = _ngaymuon.Substring(3, 2);
            string ngay = _ngaymuon.Substring(0, 2);
            string ngaymuon = nam + "-" + thang + "-" + ngay;

            string _nam = _ngaytra.Substring(6, 4);
            string _thang = _ngaytra.Substring(3, 2);
            string _ngay = _ngaytra.Substring(0, 2);
            string ngaytra = _nam + "-" + _thang + "-" + _ngay;


            DataTable dt5 = new DataTable();
            DataTable dtuser = new DataTable();
            if (userid == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, user does not input infor!'); ", true);
            }
            else
            {
                //check user co trong bang user khong?
                dtuser = DataConn.StoreFillDS("Get_userPSNV", System.Data.CommandType.StoredProcedure, userid);
                if (dtuser.Rows[0][0].ToString() == "1")
                {
                    ////user eixsts mater
                    dt5 = DataConn.StoreFillDS("Update_order_thietbi", System.Data.CommandType.StoredProcedure, itemorder, userid, lydo, bophan, ngaymuon, ngaytra);
                    if (dt5.Rows[0][0].ToString() == "1")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
                        dt = DataConn.StoreFillDS("Get_mater_device_borrow", System.Data.CommandType.StoredProcedure);
                        txtuserid.Text = "";
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, You do not Order!'); ", true);
                        txtuserid.Text = "";
                    }
                }
                else
                {
                    //user not eixsts
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, user does not exists!'); ", true);
                    txtuserid.Text = "";
                }
            }
        }

        [WebMethod]
        public static string getusermuon(string userid)
        {
            String thongbao = "";
            DataTable dtuser = new DataTable();
            dtuser = DataConn.StoreFillDS("getusermuon", System.Data.CommandType.StoredProcedure, userid);

            if (dtuser.Rows[0][0].ToString() == "1")
            {
                thongbao = "OK" + "," + dtuser.Rows[0][1].ToString() + "," + dtuser.Rows[0][2].ToString() + "," + dtuser.Rows[0][3].ToString() + "," + dtuser.Rows[0][4].ToString();
            }
            else
            {
                thongbao = "NG";
            }
            return thongbao;
        }



    }
}