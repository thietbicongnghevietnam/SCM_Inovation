using FreeLayout.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.Style;

namespace FreeLayout
{
    public partial class frmVanningDateSCM : System.Web.UI.Page
    {
        public DataTable dt_plan = new DataTable();
        public DataTable dt_getmodel = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dtgroup = new DataTable();
        public DataTable dt_update = new DataTable();
        public DataTable dtkeyconvert = new DataTable();

        public bool chk_khactuan;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Date1.Value = DateTime.Now.ToString("yyyy-MM-dd");
                ngaychiid.Value = DateTime.Now.ToString("yyyy-MM-dd");

                string _fromdate = Date1.Value;
                string _todate = ngaychiid.Value;


                dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate", System.Data.CommandType.StoredProcedure);
                //Date1.Value = DateTime.Now.ToString("dd-MM-yyyy");
                //ngaychiid.Value = DateTime.Now.ToString("dd-MM-yyyy");
                dtcate = DataConn.StoreFillDS("pro_get_categogy", System.Data.CommandType.StoredProcedure);
                DataRow newRow1 = dtcate.NewRow();
                newRow1["Description"] = "==Category==";
                dtcate.Rows.InsertAt(newRow1, 0);
                dr_filter_Cate.DataSource = dtcate;
                dr_filter_Cate.DataBind();

                dtgroup = DataConn.StoreFillDS("pro_get_namegroup", System.Data.CommandType.StoredProcedure);
                DataRow newRow2 = dtgroup.NewRow();
                newRow2["NameGroup"] = "==UploadNo==";
                dtgroup.Rows.InsertAt(newRow2, 0);
                dr_filter_namegroup.DataSource = dtgroup;
                dr_filter_namegroup.DataBind();
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  // Dùng cho mục đích phi thương mại

                ////keyname convert
                dtkeyconvert = DataConn.StoreFillDS("pro_get_categogy_keyconvert", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
                if (dtkeyconvert.Rows.Count > 0)
                {
                    DataRow newRow3 = dtkeyconvert.NewRow();
                    newRow3["KeyConvert"] = "==TemplateName==";
                    dtkeyconvert.Rows.InsertAt(newRow3, 0);
                    dr_filter_keyconvert.DataSource = dtkeyconvert;
                    dr_filter_keyconvert.DataBind();
                }

                //Date1.Value = DateTime.Now.ToString("yyyy-MM-dd");
                //ngaychiid.Value = DateTime.Now.ToString("yyyy-MM-dd");
            }

        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string _modelname = model_search.Value.ToString();
            string _countryname = country_search.Value.ToString();

            string _checkpartno = Request.Form["check_history_search"];

            string category = dr_filter_Cate.SelectedValue;

            string uploadno = dr_filter_namegroup.SelectedValue;

            string statushistory = "off";

            if (_checkpartno == "on")
            {
                statushistory = "on";
            }

            //loc theo ngay
            if (_fromdate == "" || _fromdate == "")
            {
                //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban nen chon ngay!!!'); ", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Ban nen chon ngay!');", true);
            }
            else
            {
                dtkeyconvert = DataConn.StoreFillDS("pro_get_categogy_keyconvert", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
                // XÓA TRƯỚC KHI BIND
                dr_filter_keyconvert.Items.Clear();

                if (dtkeyconvert.Rows.Count > 0)
                {
                    DataRow newRow3 = dtkeyconvert.NewRow();
                    newRow3["KeyConvert"] = "==TemplateName==";
                    dtkeyconvert.Rows.InsertAt(newRow3, 0);
                    dr_filter_keyconvert.DataSource = dtkeyconvert;
                    dr_filter_keyconvert.DataBind();
                }

                if (category == "==Category==")
                {
                    //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);                                     
                    dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2_HS", System.Data.CommandType.StoredProcedure, _fromdate, _todate, statushistory, uploadno, _modelname, _countryname);
                    if (_checkpartno == "on")
                    {
                        check_history_search.Checked = true;
                    }
                }
                else
                {
                    dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2_cate_HS", System.Data.CommandType.StoredProcedure, _fromdate, _todate, category, statushistory, uploadno, _modelname, _countryname);
                    if (_checkpartno == "on")
                    {
                        check_history_search.Checked = true;
                    }
                }

                //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan_theongay", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
                //ngaychiid.Value = ngay + "-" + thang + "-" + nam;
            }
        }



        protected void dr_filter_Plan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //// Lấy giá trị đã chọn
            //string _fromdate = Request.Form[Date1.UniqueID];
            //string _todate = Request.Form[ngaychiid.UniqueID];
            //string category = dr_filter_Cate.SelectedValue;
            //if (category == "==Category==")
            //{
            //    //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
            //    dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
            //}
            //else
            //{
            //    dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2_cate", System.Data.CommandType.StoredProcedure, _fromdate, _todate, category);
            //}


            // Ví dụ:
            //Label lblMessage = new Label();
            //lblMessage.Text = "Bạn đã chọn: " + selectedValue;
            //this.Controls.Add(lblMessage);
        }

        protected void btnSplitCont(object sender, EventArgs e)
        {
            DataTable dt_splitcont = new DataTable();
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string Category_ = "";
            if (rblDP.Checked)
            {
                Category_ = rblDP.Text;
            }
            else if (rblDECT.Checked)
            {
                Category_ = rblDECT.Text;
            }

            if (_fromdate == "" || _fromdate == "")
            {
                //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon ngay tinh lich tau!'); ", true);
            }
            else
            {
                //chia cont
                if (Category_ == "DECT")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Category nay khong co chuc nang nay!'); ", true);
                }
                else if (Category_ == "MW")
                {

                }
                else if (Category_ == "DP")
                {

                }
            }
        }

        protected void btnSaveHistory(object sender, EventArgs e)
        {
            DataTable dt_save = new DataTable();
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string Category_ = "";
            if (rblDP.Checked)
            {
                Category_ = rblDP.Text;
            }
            else if (rblDECT.Checked)
            {
                Category_ = rblDECT.Text;
            }
            else if (rblMW.Checked)
            {
                Category_ = rblMW.Text;
            }
            //else if (rblSound.Checked)
            //{
            //    Category_ = rblSound.Text;
            //}

            if (_fromdate == "" || _fromdate == "")
            {
                //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon ngay tinh lich tau!'); ", true);
            }
            else
            {
                try
                {
                    DataTable dt_get_infor = new DataTable();
                    dt_save = DataConn.StoreFillDS("Save_lichtau_history", System.Data.CommandType.StoredProcedure, Category_, _fromdate, _todate);
                    if (dt_save.Rows[0][0].ToString() == "1")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Save data thanh cong!');", true);
                        dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2_cate", System.Data.CommandType.StoredProcedure, _fromdate, _todate, Category_);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Khong ton tai ban ghi nao!'); ", true);
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            //else if (Category_ == "")
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon category!'); ", true);
            //}
            //else
            //{

            //}
        }

        protected void delete_item(object sender, EventArgs e)
        {
            DataTable dt_update = new DataTable();
            string IDdel = txtid.Text.ToString();
            string userid = txtuser.Text.ToString();
            if (userid != "")
            {
                dt_update = DataConn.StoreFillDS("Delete_EXfactory_ETD", System.Data.CommandType.StoredProcedure, IDdel, userid);
                if (dt_update.Rows[0][0].ToString() == "1")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Delete data sucessful!');", true);
                    dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate", System.Data.CommandType.StoredProcedure);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, check information again!'); ", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG,user null!'); ", true);
            }

        }

        //Updatethongtin
        protected void Updatethongtin(object sender, EventArgs e)
        {
            DataTable dt_update = new DataTable();
            string Exfactory_date = Request.Form[exFactoryDate.UniqueID];
            string ETD_date = Request.Form[etdDate.UniqueID];
            string IDupdate = IDedit.Text.ToString();

            try
            {
                if (Exfactory_date == "" || ETD_date == "")
                {
                    //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon ngay update!'); ", true);
                }
                else
                {
                    dt_update = DataConn.StoreFillDS("update_EXfactory_ETD", System.Data.CommandType.StoredProcedure, IDupdate, Exfactory_date, ETD_date);
                    if (dt_update.Rows[0][0].ToString() == "1")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Du lieu update thanh cong!');", true);
                        dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate", System.Data.CommandType.StoredProcedure);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Kiem tra lai thong tin!'); ", true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void btnRisK(object sender, EventArgs e)
        {
            DataTable dt_tinhlichtau = new DataTable();
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string Category_ = "";
            if (rblDP.Checked)
            {
                Category_ = rblDP.Text;
            }
            else if (rblDECT.Checked)
            {
                Category_ = rblDECT.Text;
            }

            if (_fromdate == "" || _fromdate == "")
            {
                //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon ngay tinh lich tau!'); ", true);
            }
            else if (Category_ == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon category!'); ", true);
            }
            else
            {
                DataTable dt_get_infor = new DataTable();
                dt_tinhlichtau = DataConn.StoreFillDS("Calculator_lichtau", System.Data.CommandType.StoredProcedure, Category_, _fromdate, _todate);
                try
                {
                    if (Category_ == "DECT")
                    {
                        for (int i = 0; i < dt_tinhlichtau.Rows.Count; i++)
                        {
                            float TTLvol = 0;
                            string ID_lichtau = dt_tinhlichtau.Rows[i]["ID"].ToString();
                            string modelname = dt_tinhlichtau.Rows[i]["Model"].ToString();
                            string Country = dt_tinhlichtau.Rows[i]["Country"].ToString();

                            string cancombine = dt_tinhlichtau.Rows[i]["Cancombine"].ToString();
                            string Destination = dt_tinhlichtau.Rows[i]["Destination"].ToString();

                            string Exfactorydate = dt_tinhlichtau.Rows[i]["Exfactorydate"].ToString(); //lay ngay exfactory date
                            //DateTime date_exfactory = DateTime.ParseExact(Exfactorydate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);                            

                            //update thong tin theo rule tren sql
                            dt_get_infor = DataConn.StoreFillDS("get_infor_cal_risk", System.Data.CommandType.StoredProcedure, Destination, Exfactorydate, cancombine, ID_lichtau, Category_);
                        }

                        //load lai du lieu
                        dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2_cate", System.Data.CommandType.StoredProcedure, _fromdate, _todate, Category_);
                    }
                    else if (Category_ == "DP")
                    {

                    }
                    else if (Category_ == "MW")
                    {

                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }

        // Kiểm tra value2 có thuộc tuần trước so với value1 không  ==> de tinh ra cot Remark
        private bool IsDateInPreviousWeek(DateTime value2, DateTime value1)
        {
            // Tính ngày đầu tuần (ví dụ bắt đầu tuần là thứ 2) của value1
            // Nếu bạn muốn tuần bắt đầu từ Chủ nhật, điều chỉnh accordingly.
            DayOfWeek firstDayOfWeek = DayOfWeek.Monday;

            // Tìm ngày đầu tuần của value1
            int diff = (7 + (value1.DayOfWeek - firstDayOfWeek)) % 7;
            DateTime startOfWeekValue1 = value1.AddDays(-diff).Date;

            // Tuần trước là khoảng 7 ngày trước đó
            DateTime startOfPreviousWeek = startOfWeekValue1.AddDays(-7);
            DateTime endOfPreviousWeek = startOfWeekValue1.AddDays(-1);

            // Kiểm tra value2 nằm trong tuần trước không
            return value2.Date >= startOfPreviousWeek && value2.Date <= endOfPreviousWeek;
        }

        protected void btnRemark(object sender, EventArgs e)
        {
            DataTable dt_remark = new DataTable();
            DataTable dt_check_specialnote = new DataTable();
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string Category_ = "";
            if (rblDP.Checked)
            {
                Category_ = rblDP.Text;
            }
            else if (rblDECT.Checked)
            {
                Category_ = rblDECT.Text;
            }

            if (_fromdate == "" || _todate == "")
            {
                //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon ngay tinh remark!'); ", true);
            }
            else if (Category_ == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon category!'); ", true);
            }
            else
            {
                DataTable dt_update = new DataTable();
                dt_remark = DataConn.StoreFillDS("Calculator_lichtau", System.Data.CommandType.StoredProcedure, Category_, _fromdate, _todate);
                try
                {
                    if (Category_ == "DECT")
                    {
                        for (int i = 0; i < dt_remark.Rows.Count; i++)
                        {
                            if (dt_remark.Rows[i]["Destination"].ToString() == "")
                            {
                                //nothing khong tinh remark
                            }
                            else
                            {
                                //lay ngay ETD PSNV = ngay ex-factory date
                                string Exfactorydate = dt_remark.Rows[i]["Exfactorydate"].ToString(); //lay ngay exfactory date
                                                                                                      //lay ngay ATP jit date
                                string ATPdate = dt_remark.Rows[i]["ATPdate"].ToString();

                                //lay ngay ETD 
                                string ETDdate = dt_remark.Rows[i]["ETD"].ToString();

                                //****pending
                                //float TTLvol = 0;
                                string ID_lichtau = dt_remark.Rows[i]["ID"].ToString();
                                string modelname = dt_remark.Rows[i]["Model"].ToString();
                                string Country = dt_remark.Rows[i]["Country"].ToString();
                                //string cancombine = dt_remark.Rows[i]["Cancombine"].ToString();
                                string Destination = dt_remark.Rows[i]["Destination"].ToString();

                                DateTime date_exfactory = DateTime.ParseExact(Exfactorydate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                                DateTime date_ATP = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                                //check hai ngay tren co khac tuan hay khong?
                                //tuan exfactory date co som hon ATP hay khong?
                                ////Nếu ngày ETD PSNV trước tuần ATP jit date --> Show lý do
                                bool isValue2InPreviousWeek = IsDateInPreviousWeek(date_exfactory, date_ATP);

                                //Response.Write($"Giá trị 2 có thuộc tuần trước so với giá trị 1? {isValue2InPreviousWeek}");

                                string note_remark = "";

                                if (isValue2InPreviousWeek == true)
                                {
                                    //kiem tra xem co special note hay khong???
                                    dt_check_specialnote = DataConn.StoreFillDS("Check_special_note_remark", System.Data.CommandType.StoredProcedure, Category_, modelname, Country, Destination);
                                    if (dt_check_specialnote.Rows[0][0].ToString() != "0")
                                    {
                                        //ton tai trong mater
                                        string Special_exfactory_date = dt_check_specialnote.Rows[0][1].ToString();     //theo ngày xuất hàng muộn nhất của tháng
                                        string SpecialETD_week = dt_check_specialnote.Rows[0][2].ToString();            //theo tuan
                                        string Special_ETA_Date = dt_check_specialnote.Rows[0][3].ToString();           //theo ngay ETA
                                                                                                                        //Nếu ngày ETD PSNV(ex-factorydate)  trước tuần ATP jit date --> Show lý do

                                        if (Special_exfactory_date != "")
                                        {
                                            note_remark = "Customer request special exfactory date : " + Special_exfactory_date;
                                        }
                                        else if (SpecialETD_week != "")
                                        {
                                            note_remark = "Customer request special ETD week : " + SpecialETD_week; // + "/Date :" + ETDdate.Substring(0, 9);
                                        }
                                        else if (Special_ETA_Date != "")
                                        {
                                            //note_remark = "Customer request ETA date by : " + Special_ETA_Date;
                                            note_remark = "Customer request by ETA : " + Special_ETA_Date + "th";
                                        }
                                        else
                                        {
                                            //truong hop khong co sepecial note  => Show rõ "Carrier request early cut-off time"
                                            //note_remark = "Carrier request early cut-off time (no special note)";
                                            note_remark = "Vessel cut-off ";
                                        }
                                        //update remark  theo rule tren sql
                                        dt_update = DataConn.StoreFillDS("update_infor_remark", System.Data.CommandType.StoredProcedure, note_remark, ID_lichtau);

                                    }
                                    else
                                    {
                                        //khong ton tai trong mater
                                        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, du lieu model khong co trong mater!!!'); ", true);
                                        //nothing
                                    }
                                }
                                else
                                {
                                    //nothing
                                }
                            }
                        }

                        //load lai du lieu
                        dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2_cate", System.Data.CommandType.StoredProcedure, _fromdate, _todate, Category_);
                    }
                    else if (Category_ == "DP")
                    {

                    }
                    else if (Category_ == "MW")
                    {

                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }

        protected void btnTinhLichTau(object sender, EventArgs e)
        {
            DataTable dt_tinhlichtau = new DataTable();
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string Category_ = "";
            if (rblDP.Checked)
            {
                Category_ = rblDP.Text;
            }
            else if (rblDECT.Checked)
            {
                Category_ = rblDECT.Text;
            }

            if (_fromdate == "" || _fromdate == "")
            {
                //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon ngay tinh lich tau!'); ", true);
            }
            else
            {
                if (Category_ == "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon category!'); ", true);
                }
                else
                {
                    if (dr_filter_keyconvert.SelectedValue.ToString() != "==TemplateName==" && dr_filter_keyconvert.SelectedValue.ToString() != "")   //DPOversea28112025
                    {
                        string temp = dr_filter_keyconvert.SelectedValue.ToString();  //DPOversea28112025
                        Category_ = temp.Substring(0, temp.Length - 8); //DPOversea
                        //hang Dophone Oversea
                        //Category_ = "DPOversea";
                    }

                    DataTable dt_mater_vessel = new DataTable();
                    dt_tinhlichtau = DataConn.StoreFillDS("Calculator_lichtau", System.Data.CommandType.StoredProcedure, Category_, _fromdate, _todate);

                    try
                    {
                        //tinh toan lich tau trong code C#
                        int count = 0;
                        if (Category_ == "DECT")
                        {
                            for (int i = 0; i < dt_tinhlichtau.Rows.Count; i++)
                            {
                                string ID_lichtau = dt_tinhlichtau.Rows[i]["ID"].ToString();
                                string modelname = dt_tinhlichtau.Rows[i]["Model"].ToString();
                                string Destination = dt_tinhlichtau.Rows[i]["Destination"].ToString();
                                string Country = dt_tinhlichtau.Rows[i]["Country"].ToString();

                                string ATPdate = dt_tinhlichtau.Rows[i]["ATPdate"].ToString(); //tinh so tuan cua ngay ATP date

                                //DateTime date_request = DateTime.ParseExact(ATPdate.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                                DateTime date_request = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                                //int weekOfMonth_rq = GetWeekOfMonth(date_request);

                                int weekOfMonth_rq = GetWeekOfMonth_New(date_request);

                                dt_mater_vessel = DataConn.StoreFillDS("Get_info_vessel", System.Data.CommandType.StoredProcedure, Destination, Country, Category_);
                                if (dt_mater_vessel.Rows[0][0].ToString() == "1")
                                {
                                    //tinh ngay ETD
                                    string FCL_Ex_factory = dt_mater_vessel.Rows[0]["FCL_Ex_factory"].ToString();
                                    string FCL_ETD = dt_mater_vessel.Rows[0]["FCL_ETD"].ToString();
                                    string FCL_ETA = dt_mater_vessel.Rows[0]["FCL_ETA"].ToString();
                                    string LLC_Ex_factory = dt_mater_vessel.Rows[0]["LLC_Ex_factory"].ToString();
                                    string LLC_ETD = dt_mater_vessel.Rows[0]["LLC_ETD"].ToString();
                                    string LLC_ETA = dt_mater_vessel.Rows[0]["LLC_ETA"].ToString();
                                    string AIR_Ex_factory = dt_mater_vessel.Rows[0]["AIR_Ex_factory"].ToString();
                                    string AIR_ETD = dt_mater_vessel.Rows[0]["AIR_ETD"].ToString();
                                    string AIR_ETA = dt_mater_vessel.Rows[0]["AIR_ETA"].ToString();

                                    string Special_exfactory_date = dt_mater_vessel.Rows[0]["Special_exfactory_date"].ToString();
                                    string SpecialETD_week = dt_mater_vessel.Rows[0]["SpecialETD_week"].ToString();
                                    string Special_ETA_Date = dt_mater_vessel.Rows[0]["Special_ETA_Date"].ToString();

                                    string stansit_time = "0";
                                    string stansit_time2 = "0";
                                    stansit_time = dt_mater_vessel.Rows[0]["FCL_ETA"].ToString();  //transit time
                                    stansit_time2 = dt_mater_vessel.Rows[0]["LLC_ETA"].ToString();  //transit time

                                    //lay theo tuan request date
                                    DayOfWeek? day1 = null;
                                    DayOfWeek? day1b = null;
                                    DayOfWeek? day2 = null;
                                    DayOfWeek? day2b = null;
                                    if (FCL_Ex_factory != "")
                                    {
                                        day1 = ConvertToDayOfWeek(FCL_Ex_factory); // "THU"; // giá trị truyền vào thu 5
                                    }
                                    if (FCL_ETD != "")
                                    {
                                        day1b = ConvertToDayOfWeek(FCL_ETD); // "MON"; // giá trị truyền vào thu 2
                                    }
                                    if (LLC_Ex_factory != "")
                                    {
                                        day2 = ConvertToDayOfWeek(LLC_Ex_factory);  // "TUE"; // giá trị truyền vào thu 3
                                    }
                                    if (LLC_ETD != "")
                                    {
                                        day2b = ConvertToDayOfWeek(LLC_ETD);  // "SUN"; // giá trị truyền vào thu CN
                                    }

                                    //check truong hop ca 2 lich deu khac tuan ==> so sanh ngay 
                                    bool isFCL = false;
                                    bool isLCL = false;
                                    if (day1.HasValue && day1b.HasValue)
                                    {
                                        isFCL = IsDifferentWeek2(day1, day1b);
                                    }
                                    if (day2.HasValue && day2b.HasValue)
                                    {
                                        isLCL = IsDifferentWeek2(day2, day2b);
                                    }
                                    if (isFCL == true && isLCL == true)   //check truong hop ca 2 lich deu khac tuan ==> so sanh ngay 
                                    {
                                        chk_khactuan = true;
                                    }
                                    else
                                    {
                                        chk_khactuan = false;
                                    }

                                    DateTime date_request1 = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                                    if (SpecialETD_week != "" && SpecialETD_week != "0")
                                    {
                                        //truong hop 1
                                        //tinh ra tuan cua special note //neu bang thi lay theo truoc do?
                                        if (weekOfMonth_rq <= int.Parse(SpecialETD_week))
                                        {
                                            //so sanh truong hop day1 va day2 xem truong hop null khong?
                                            if (day1.HasValue && day2.HasValue)
                                            {
                                                if (chk_khactuan == true)
                                                {
                                                    //*** truong hop nay co ****** doi xem co xay vao truong hop nay khong **** fix tiep trong tuong lai ******
                                                    //kiem tra ngay ATP co phai la tuan dau tien cua thang khong?? => neu la tuan dau lay luon lich trong tuan luon!   //Test 2 **** test file 2
                                                    //bool isFirstWeekOfMonth = date_request1.Day <= 7;
                                                    //if (isFirstWeekOfMonth == true)

                                                    //truong hop ca 2 lich deu khac tuan so sanh ngay ex-factorydate
                                                    if ((int)day1 < (int)day2)
                                                    {
                                                        //lay theo lich FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else if ((int)day2 < (int)day1)
                                                    {
                                                        //lay theo lich LCL
                                                        string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //(int)day2 = (int)day1 //*** truon hop nay tam thoi cu lay theo lich FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                }
                                                else
                                                {
                                                    if ((int)day1 < (int)day2)
                                                    {
                                                        //kiem tra ngay ATP co phai la tuan dau tien cua thang khong?? => neu la tuan dau lay luon lich trong tuan luon!   //Test 2 **** test file 2
                                                        bool isFirstWeekOfMonth = date_request1.Day <= 7;
                                                        //so sanh them ngay ETD trong tuan co phu hop khong??? //tuan dau cua thang  ****tuan dau - thang 9
                                                        if (weekOfMonth_rq == 1)   // && (int)day1 < (int)day2b
                                                        //if (isFirstWeekOfMonth == true)
                                                        {
                                                            //lay ngay ETD => cua FCL so sanh voi ngay ATP co cung thang hay khong????
                                                            string check_ETD = LLC_ETD;   //lay ngay ETD => cua LLC
                                                            DayOfWeek ck_ETD = ConvertToDayOfWeek(check_ETD);
                                                            DateTime ck_ngay_ETD = GetSpecificDayInWeek(date_request1, ck_ETD);  //tinh ra ngay ETD
                                                            //check ngay ATP va ngay ETD co cung tuan hay khong (cung thang)??? ***pending
                                                            bool isSameMonth = (ck_ngay_ETD.Month == date_request1.Month) && (ck_ngay_ETD.Year == date_request1.Year);

                                                            //code old 26.09.2025
                                                            ////lay lich FCL 
                                                            //string inputDay = FCL_Ex_factory;
                                                            //DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            //string inputDay2 = FCL_ETD;
                                                            //DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            //DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD                                                            
                                                            ////tinh ra ngay ETA =  Ngay ETD + transitime
                                                            //DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            //count = count + 1;

                                                            //****** so sanh tuan ATP va tuan ETD cung thang khong ?*****
                                                            if (isSameMonth)
                                                            {
                                                                //Response.Write("Hai ngày cùng tháng.");
                                                                string inputDay = LLC_Ex_factory;
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = LLC_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                                DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD                                                            
                                                                                                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }
                                                            else if ((int)day2 < (int)day1b)  //tuan dau cua thang  ****tuan dau - thang 9
                                                            {
                                                                //lay lich FCL 
                                                                string inputDay = FCL_Ex_factory;
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = FCL_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD                                                            
                                                                                                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }
                                                            else
                                                            {
                                                                //Response.Write("Hai ngày KHÔNG cùng tháng.");
                                                                string inputDay = FCL_Ex_factory;
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = FCL_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD                                                            
                                                                                                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }


                                                        }
                                                        else
                                                        {
                                                            //bool isDiffWeek = IsDifferentWeek(day2, day2b);  //lay day2b lam goc
                                                            bool isDiffWeek = IsDifferentWeek2(day2, day2b);  //lay day2b lam goc
                                                            if (isDiffWeek == true)
                                                            {
                                                                //khac tuan => lay lich tau nguoc lai ***** 
                                                                string inputDay = LLC_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = LLC_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);  //ngay ETD

                                                                DateTime Datecuoithang = GetSpecificDayInWeek(date_request1, targetDay2); //ngay ETD la ngay cuoi thang
                                                                bool isLastDayOfMonth = Datecuoithang.Day == DateTime.DaysInMonth(Datecuoithang.Year, Datecuoithang.Month);
                                                                if (isLastDayOfMonth)
                                                                {
                                                                    //Console.WriteLine("ResultDay là ngày cuối tháng.");  //ngay ETD la ngay cuoi thang
                                                                    //giu nguyen lich cach tinh cu ***** vi day1 < day2
                                                                    string inputDayb = FCL_Ex_factory;
                                                                    DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                                    string inputDay2b = FCL_ETD;
                                                                    DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                                    DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD

                                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                }
                                                                else
                                                                {
                                                                    // Console.WriteLine("ResultDay KHÔNG phải là ngày cuối tháng."); 
                                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                                }

                                                                ////neu la tuan dau cua thang
                                                                //if (IsFirstWeekOfMonth(date_request1) == true)
                                                                //{}
                                                                //else
                                                                //{
                                                                //    // TH khong phai tuan dau cua thang
                                                                //}
                                                                count = count + 1;
                                                            }
                                                            else
                                                            {
                                                                //giu nguyen lich cach tinh cu *****   
                                                                string inputDay = FCL_Ex_factory;
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = FCL_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                        //update len 2 gia tri len co so du lieu *****
                                                                                                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }
                                                        }

                                                    }
                                                    else if ((int)day2 < (int)day1)
                                                    {
                                                        //Response.Write("Ngày đứng trước là: " + day2);
                                                        //string ATPdate1 = ATPdate; // e.g., "2025-07-02 00:00:00.000"
                                                        //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                                        //DateTime check_date_request = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                                                        //kiem tra ngay ATP co phai la tuan dau tien cua thang khong?? => neu la tuan dau lay luon lich trong tuan luon!   //Test 2 **** test file 2
                                                        bool isFirstWeekOfMonth = date_request1.Day <= 7;
                                                        //&& (int)day2 < (int)day1b so sanh them ngay ETD trong tuan co phu hop khong???
                                                        if (weekOfMonth_rq == 1) //&& (int)day2 < (int)day1b //tuan dau cua thang  ****tuan dau - thang 9
                                                        //if (isFirstWeekOfMonth == true)
                                                        {

                                                            //lay ngay ETD => cua FCL so sanh voi ngay ATP co cung thang hay khong????
                                                            string check_ETD = FCL_ETD;   //lay ngay ETD => cua FCL
                                                            DayOfWeek ck_ETD = ConvertToDayOfWeek(check_ETD);
                                                            DateTime ck_ngay_ETD = GetSpecificDayInWeek(date_request1, ck_ETD);  //tinh ra ngay ETD
                                                            //check ngay ATP va ngay ETD co cung tuan hay khong (cung thang)??? ***pending
                                                            bool isSameMonth = (ck_ngay_ETD.Month == date_request1.Month) && (ck_ngay_ETD.Year == date_request1.Year);

                                                            //code old 26.09.2025
                                                            //string inputDayb = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                            //DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                            //string inputDay2b = LLC_ETD;
                                                            //DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                            //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                            //DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD

                                                            //DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            //count = count + 1;

                                                            if (isSameMonth)
                                                            {
                                                                //Response.Write("Hai ngày cùng tháng.");
                                                                //so sanh 2 ngay ex-factory xem ngay nao nho hon thi lay theo lich (tren da so sanh roi : ((int)day2 < (int)day1) )
                                                                //lay lich FCC / FCC
                                                                string inputDayb = FCL_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                                DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                                string inputDay2b = FCL_ETD;
                                                                DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                                DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD

                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }
                                                            else if ((int)day2 < (int)day1b)  //tuan dau cua thang  ****tuan dau - thang 9
                                                            {
                                                                string inputDayb = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                                DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                                string inputDay2b = LLC_ETD;
                                                                DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD

                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }
                                                            else
                                                            {
                                                                //Response.Write("Hai ngày KHÔNG cùng tháng.");
                                                                //lay lich LLC
                                                                string inputDayb = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                                DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                                string inputDay2b = LLC_ETD;
                                                                DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD

                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }

                                                        }
                                                        else
                                                        {
                                                            //1*so sánh tương quan ngày Ex-factory và ngày ETD xem có cùng tuần hay không? 
                                                            //neu khac tuan thi phai lay lich tau khac
                                                            //bool isDiffWeek = IsDifferentWeek(day1, day1b);  //lay day1b lam goc
                                                            bool isDiffWeek = IsDifferentWeek2(day1, day1b);  //lay day1b lam goc                                                                                                     
                                                            if (isDiffWeek == true)
                                                            {
                                                                //khac tuan => lay lich tau nguoc lai *****   
                                                                string inputDay = FCL_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = FCL_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2); //ngay ETD

                                                                DateTime Datecuoithang = GetSpecificDayInWeek(date_request1, targetDay2); //ngay ETD la ngay cuoi thang
                                                                bool isLastDayOfMonth = Datecuoithang.Day == DateTime.DaysInMonth(Datecuoithang.Year, Datecuoithang.Month);
                                                                if (isLastDayOfMonth)
                                                                {
                                                                    //Console.WriteLine("ResultDay là ngày cuối tháng.");  //ngay ETD la ngay cuoi thang
                                                                    //giu nguyen lich cach tinh cu *****  vi day2 < day 1                                              
                                                                    string inputDayb = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                                    DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                                    string inputDay2b = LLC_ETD;
                                                                    DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                                    DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD
                                                                                                                                             //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                }
                                                                else
                                                                {
                                                                    // Console.WriteLine("ResultDay KHÔNG phải là ngày cuối tháng."); 
                                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                                }

                                                                count = count + 1;
                                                            }
                                                            else
                                                            {
                                                                //giu nguyen lich cach tinh cu *****                                                
                                                                string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = LLC_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                                //update len 2 gia tri len co so du lieu *****
                                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                                count = count + 1;
                                                                // In kết quả
                                                                //Response.Write($"Ngày {inputDay} trong tuần chứa {date_request:dd/MM/yyyy} là: {resultDay:dd/MM/yyyy}");
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        //*** truong hop nay co ****** doi xem co xay vao truong hop nay khong **** fix tiep trong tuong lai ******
                                                        //kiem tra ngay ATP co phai la tuan dau tien cua thang khong?? => neu la tuan dau lay luon lich trong tuan luon!   //Test 2 **** test file 2
                                                        //bool isFirstWeekOfMonth = date_request1.Day <= 7;
                                                        //if (isFirstWeekOfMonth == true)

                                                        //(int)day2 = (int)day1
                                                        DayOfWeek day11 = ConvertToDayOfWeek(FCL_ETD);
                                                        DayOfWeek day22 = ConvertToDayOfWeek(LLC_ETD);
                                                        if ((int)day11 < (int)day22)
                                                        {
                                                            string inputDay = FCL_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        else if ((int)day22 < (int)day11)
                                                        {
                                                            string inputDay = LLC_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = LLC_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            //update len 2 gia tri len co so du lieu *****
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        else if (Int32.Parse(stansit_time) > Int32.Parse(stansit_time2))
                                                        {
                                                            //lay theo FCL
                                                            string inputDay = FCL_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        else if (Int32.Parse(stansit_time) < Int32.Parse(stansit_time2))
                                                        {
                                                            //lay theo LLC
                                                            string inputDay = LLC_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = LLC_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        else
                                                        {
                                                            //lay theo FCL
                                                            string inputDay = FCL_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }

                                                    }
                                                }

                                            }
                                            else if (day1.HasValue && !day2.HasValue)
                                            {
                                                // truong hop 1 co gia tri, truong hop 2 khong co gia tri
                                                //giu nguyen lich cach tinh cu *****   
                                                string inputDay = FCL_Ex_factory;
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = FCL_ETD;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                count = count + 1;
                                            }
                                            else if (!day1.HasValue && day2.HasValue)
                                            {
                                                // truong hop 2 co gia tri, truong hop 1 khong co gia tri
                                                //giu nguyen lich cach tinh cu *****                                                
                                                string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = LLC_ETD;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                //update len 2 gia tri len co so du lieu *****
                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                count = count + 1;
                                            }
                                        }
                                        else
                                        {
                                            //lay theo tuan special note   ==> khong lay theo ngay ATPdate ==> quy tac van giong tren (khac ngay ATP date => chon ngay thu 2 trong tuan dacbiet)
                                            // Lấy năm & tháng từ ATPdate (hoặc gán thủ công nếu bạn biết tháng)
                                            //DateTime atpDate = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                            DateTime atpDate = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                                            int year = atpDate.Year;
                                            int month = atpDate.Month;

                                            // Tuần đặc biệt (SpecialETD_week) truyền vào từ DB
                                            int specialWeek = int.Parse(SpecialETD_week); // ví dụ: 3
                                            // Tìm ngày Thứ Hai của tuần thứ N trong tháng
                                            DateTime firstDayOfMonth = new DateTime(year, month, 1);
                                            int dayOffset = ((int)firstDayOfMonth.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                                            DateTime firstMonday = firstDayOfMonth.AddDays(-dayOffset <= 0 ? -dayOffset : 7 - dayOffset); // bắt đầu từ tuần chứa ngày đầu tháng

                                            DateTime mondayOfSpecialWeek = firstMonday.AddDays((specialWeek - 1) * 7);  //tim ra ngay 1 ngay cua tuan dac biet (thu 2 dau tuan)

                                            // so sanh lich trong shipment
                                            //DayOfWeek day1 = ConvertToDayOfWeek(FCL_Ex_factory); // "THU"; // giá trị truyền vào thu 5
                                            //DayOfWeek day1b = ConvertToDayOfWeek(FCL_ETD); // "MON"; // giá trị truyền vào thu 2
                                            //DayOfWeek day2 = ConvertToDayOfWeek(LLC_Ex_factory);  // "TUE"; // giá trị truyền vào thu 3
                                            //DayOfWeek day2b = ConvertToDayOfWeek(LLC_ETD);  // "SUN"; // giá trị truyền vào thu CN

                                            if (day1.HasValue && day2.HasValue)
                                            {
                                                if (chk_khactuan == true)
                                                {
                                                    if ((int)day1 < (int)day2)
                                                    {
                                                        //lay theo lich FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else if ((int)day2 < (int)day1)
                                                    {
                                                        //lay theo lich LCL
                                                        string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //(int)day2 = (int)day1 //*** truon hop nay tam thoi cu lay theo lich FCL
                                                        //lay theo lich FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }

                                                }
                                                else
                                                {
                                                    if ((int)day1 < (int)day2)
                                                    {
                                                        //Response.Write("Ngày đứng trước là: " + day1);
                                                        //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);                                                
                                                        //bool isDiffWeek = IsDifferentWeek(day2, day2b);  //lay day2b lam goc
                                                        bool isDiffWeek = IsDifferentWeek2(day2, day2b);  //lay day2b lam goc
                                                        if (isDiffWeek == true)
                                                        {
                                                            //khac tuan => lay lich tau nguoc lai ***** 
                                                            string inputDay = LLC_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = LLC_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                            //neu la tuan dau cua thang
                                                            //if (IsFirstWeekOfMonth(mondayOfSpecialWeek) == true)
                                                            //{                                                        
                                                            //}
                                                            //else
                                                            //{
                                                            //    // TH khong phai tuan dau cua thang                                                        
                                                            //}
                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD
                                                                                                                                          //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                          //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                            count = count + 1;
                                                        }
                                                        else
                                                        {
                                                            //giu nguyen lich cach tinh cu *****   
                                                            string inputDay = FCL_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                            DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD
                                                                                                                                          //update len 2 gia tri len co so du lieu *****
                                                                                                                                          //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                          //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }

                                                    }
                                                    else if ((int)day2 < (int)day1)
                                                    {
                                                        //Response.Write("Ngày đứng trước là: " + day2);
                                                        //string ATPdate1 = ATPdate; // e.g., "2025-07-02 00:00:00.000"
                                                        //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                                                        //1*so sánh tương quan ngày Ex-factory và ngày ETD xem có cùng tuần hay không? 
                                                        //neu khac tuan thi phai lay lich tau khac
                                                        //bool isDiffWeek = IsDifferentWeek(day1, day1b);  //lay day1b lam goc
                                                        bool isDiffWeek = IsDifferentWeek2(day1, day1b);  //lay day1b lam goc

                                                        if (isDiffWeek == true)
                                                        {
                                                            //khac tuan => lay lich tau nguoc lai *****   
                                                            string inputDay = FCL_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD
                                                                                                                                          //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                          //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        else
                                                        {
                                                            //giu nguyen lich cach tinh cu *****                                                
                                                            string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = LLC_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                            //update len 2 gia tri len co so du lieu *****
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                            // In kết quả
                                                            //Response.Write($"Ngày {inputDay} trong tuần chứa {date_request:dd/MM/yyyy} là: {resultDay:dd/MM/yyyy}");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //(int)day2 = (int)day1  ==> TH bang nhau // so sanh ngay ETD de lay lich tau
                                                        DayOfWeek day11 = ConvertToDayOfWeek(FCL_ETD);
                                                        DayOfWeek day22 = ConvertToDayOfWeek(LLC_ETD);
                                                        if ((int)day11 < (int)day22)
                                                        {
                                                            string inputDay = FCL_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD
                                                                                                                                          //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                          //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        if ((int)day22 < (int)day11)
                                                        {
                                                            string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = LLC_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                            //update len 2 gia tri len co so du lieu *****
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                            count = count + 1;
                                                        }
                                                        //*****de y xem con truong hop so sanh theo transit time theo truong hop nay khong????
                                                    }
                                                }

                                            }
                                            else if (day1.HasValue && !day2.HasValue)
                                            {
                                                string inputDay = FCL_Ex_factory;
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = FCL_ETD;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day  //ngay tuan dac biet  //test 2******
                                                DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                count = count + 1;
                                            }
                                            else if (!day1.HasValue && day2.HasValue)
                                            {
                                                string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = LLC_ETD;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                //update len 2 gia tri len co so du lieu *****
                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                count = count + 1;
                                            }

                                        }
                                    }
                                    else if (Special_ETA_Date != "" && SpecialETD_week != "0")
                                    {
                                        //truong hop 3
                                        //dựa vào ngày ship date tháng hiện tại là tháng nào  
                                        //dt_tinhlichtau.Rows[i]["ATPdate"].ToString() //  7/16/2025  => thang 8
                                        //tinh Special_ETA_Date tang them 1 thang so ngay la 14: => 8/14/2025  ***** dua vao so ngay la bao nhieu thi cong them so thang
                                        int soNgay = Int32.Parse(dt_mater_vessel.Rows[0]["Special_ETA_Date"].ToString());
                                        DateTime ngayGoc = DateTime.Parse(dt_tinhlichtau.Rows[i]["ATPdate"].ToString());
                                        //DateTime ngayCongSoNgay = ngayGoc.AddDays(soNgay); // ngày sau khi cộng 14 ngày
                                        DateTime ngayThay = new DateTime(ngayGoc.Year, ngayGoc.Month, soNgay);
                                        DateTime ketQua = ngayThay;

                                        //dua vao so ngay transit time la bao nhieu ? thi cong them so thang
                                        //co 2 transit time => chon transitme cua lich tau nao???
                                        //??? tam lay transit time cua lich tau 1
                                        if (Int32.Parse(stansit_time) <= 45)
                                        {
                                            ketQua = ngayThay.AddMonths(1); // cộng thêm 1 tháng vào ngày trên   
                                        }
                                        else
                                        {
                                            ketQua = ngayThay.AddMonths(2);
                                        }

                                        // Trả về ngày dạng "dd/MM/yyyy" hoặc bạn muốn
                                        //string ketQuaNgay = ketQua.ToString("dd/MM/yyyy");

                                        //So sánh ngày trong tuần (FCL & LCL) ngày nào trước thì chọn để lấy ra transit time de tru di
                                        DayOfWeek day11 = ConvertToDayOfWeek(FCL_ETD); // "FRI"; // giá trị truyền vào thu 5
                                        DayOfWeek day22 = ConvertToDayOfWeek(LLC_ETD);  // "MON"; // giá trị truyền vào thu 3

                                        if ((int)day1 == (int)day2)  //truong hop 2 ngay ex-factory bang nhau  ==> so sanh ngay ETD
                                        {
                                            if ((int)day11 < (int)day22)
                                            {
                                                //lay ngay nao nho < hon thi tinh toan****
                                                //so sanh Thứ ngày ETD trước thứ ngày Ex-factory thì lấy ngày ex-factory là tuần trước. còn nếu sau thì vẫn lấy trong tuần.

                                                //tinh toan nhu cu  ****
                                                //stansit_time = dt_mater_vessel.Rows[0]["FCL_ETA"].ToString();  //transit time
                                                DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));

                                                bool sameWeek = IsSameWeek(date_request1, ngayDatru1);
                                                if (sameWeek == true)
                                                {
                                                    //string ngaycantim1 = ngayDatru1.ToString("dd/MM/yyyy");  // giu nguyen cach tinh cu *****
                                                    string inputDay = FCL_ETD;
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay2 = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day

                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                    if (date_request1.Date < ngayDatru1.Date)
                                                    {
                                                        // lay theo tuan ATP request
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay2 = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day

                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        // giu nguyen cach tinh cu *****
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay2 = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day

                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                }

                                            }
                                            else if ((int)day22 < (int)day11)
                                            {
                                                //lay gia tri transit time  la "MON" < "WED"
                                                //lay ngay tang len 1 thang - transit time : 8/14/2025 - 28
                                                //stansit_time = dt_mater_vessel.Rows[0]["LLC_ETA"].ToString();  ////transit time

                                                //tinh toan nhu cu ******
                                                DateTime ngayDatru2 = ketQua.AddDays(-Int32.Parse(stansit_time2));  //ngay deadline muộn nhất ngày ETD
                                                //so sanh tuan muon nhat nay ETD voi tuan ATP xem cung tuan khong?

                                                bool sameWeek = IsSameWeek(date_request1, ngayDatru2);
                                                if (sameWeek == true)
                                                {
                                                    //cung tuan nhau  // giu nguyen cach tinh cu *****
                                                    //string ngaycantim2 = ngayDatru2.ToString("dd/MM/yyyy");

                                                    string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = LLC_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru2, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru2, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)                                                                                                                            
                                                                                                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                    if (date_request1.Date < ngayDatru2.Date)
                                                    {
                                                        // lay theo tuan ATP request
                                                        string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)                                                                                                                            

                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        // giu nguyen cach tinh cu *****
                                                        string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru2, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru2, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)                                                                                                                            

                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                }

                                            }
                                            else if (Int32.Parse(stansit_time) > Int32.Parse(stansit_time2))
                                            {
                                                //lay teho trantsit time1  //lay theo FCL
                                                DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));
                                                //string ngaycantim1 = ngayDatru1.ToString("dd/MM/yyyy");
                                                bool sameWeek = IsSameWeek(date_request1, ngayDatru1);
                                                if (sameWeek == true)
                                                {
                                                    //giu nguyen cach tinh cu ****
                                                    string inputDay = FCL_ETD;
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                    if (date_request1.Date < ngayDatru1.Date)
                                                    {
                                                        // lay theo tuan ATP date
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                       //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //giu nguyen cach tinh cu ****
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                }
                                            }
                                            else if (Int32.Parse(stansit_time) < Int32.Parse(stansit_time2))
                                            {
                                                //lay teho trantsit time2  //lay theo LLC
                                                DateTime ngayDatru2 = ketQua.AddDays(-Int32.Parse(stansit_time2));

                                                bool sameWeek = IsSameWeek(date_request1, ngayDatru2);
                                                if (sameWeek == true)
                                                {
                                                    //lay theo cach tinh cu ****
                                                    string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = LLC_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru2, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru2, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)
                                                                                                                                //DateTime ngayTru1Tuan2 = resultDay2.AddDays(-7); // Trừ 7 ngày
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                    if (date_request1.Date < ngayDatru2.Date)
                                                    {
                                                        //lay theo ngay ATP date
                                                        string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)
                                                                                                                                       //DateTime ngayTru1Tuan2 = resultDay2.AddDays(-7); // Trừ 7 ngày
                                                                                                                                       //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //lay theo cach tinh cu ****
                                                        string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru2, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru2, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)
                                                                                                                                    //DateTime ngayTru1Tuan2 = resultDay2.AddDays(-7); // Trừ 7 ngày
                                                                                                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                //lay teho trantsit time1  //lay theo FCL
                                                DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));

                                                bool sameWeek = IsSameWeek(date_request1, ngayDatru1);
                                                if (sameWeek == true)
                                                {
                                                    //lay theo cach tinh cu ****
                                                    string inputDay = FCL_ETD;
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                    if (date_request1.Date < ngayDatru1.Date)
                                                    {
                                                        //lay theo ngay ATP date
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                       //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //lay theo cach tinh cu ****
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                }

                                            }
                                        }
                                        else if ((int)day1 < (int)day2)
                                        {
                                            //tinh toan nhu cu  ****
                                            //stansit_time = dt_mater_vessel.Rows[0]["FCL_ETA"].ToString();  //transit time
                                            DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));

                                            bool sameWeek = IsSameWeek(date_request1, ngayDatru1);
                                            if (sameWeek == true)
                                            {
                                                //lay theo cach tinh cu ****
                                                string inputDay = FCL_ETD;// "THU"; // giá trị truyền vào thu 5
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = FCL_Ex_factory;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                count = count + 1;
                                            }
                                            else
                                            {
                                                //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                if (date_request1.Date < ngayDatru1.Date)
                                                {
                                                    //lay theo ngay ATP date
                                                    string inputDay = FCL_ETD;// "THU"; // giá trị truyền vào thu 5
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //lay theo cach tinh cu ****
                                                    string inputDay = FCL_ETD;// "THU"; // giá trị truyền vào thu 5
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                            }


                                        }
                                        else if ((int)day2 < (int)day1)
                                        {
                                            DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time2));

                                            bool sameWeek = IsSameWeek(date_request1, ngayDatru1);
                                            if (sameWeek == true)
                                            {
                                                //lay theo cach tinh cu ****
                                                string inputDay = LLC_ETD;// "THU"; // giá trị truyền vào thu 5
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = LLC_Ex_factory;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                count = count + 1;
                                            }
                                            else
                                            {
                                                //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                if (date_request1.Date < ngayDatru1.Date)
                                                {
                                                    //lay theo ngay ATP date
                                                    string inputDay = LLC_ETD;// "THU"; // giá trị truyền vào thu 5
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = LLC_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //lay theo cach tinh cu ****
                                                    string inputDay = LLC_ETD;// "THU"; // giá trị truyền vào thu 5
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = LLC_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                            }

                                        }
                                    }
                                    else if (Special_exfactory_date != "" && SpecialETD_week != "0")
                                    {
                                        //truong hop 2  => cate dect khong co truong hop 2
                                    }
                                    else
                                    {
                                        //truong hop khong co ngay special note => quy ra tuần ATP -> Đối chiếu lịch tàu trong tuần đó 

                                        //so sanh truong hop day1 va day2 xem truong hop null khong?
                                        if (day1.HasValue && day2.HasValue)
                                        {
                                            if (chk_khactuan == true)
                                            {
                                                //truong hop 2 lich deu khac tuan
                                                if ((int)day1 < (int)day2)
                                                {
                                                    //lay theo lich FCL
                                                    string inputDay = FCL_Ex_factory;
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_ETD;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                    //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                    //===> test file 2 vi khac tuan phai lay truoc 1 tuan*** chu y test lai nguoc 1         //*****test2
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);
                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                                else if ((int)day2 < (int)day1)
                                                {
                                                    //lay theo lich LCL
                                                    string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = LLC_ETD;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                    //===> test file 2 vi khac tuan phai lay truoc 1 tuan*** chu y test lai nguoc 1                     //*****test2
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day 
                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                    //update len 2 gia tri len co so du lieu *****
                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //(int)day2 = (int)day1 //*** truon hop nay tam thoi cu lay theo lich FCL
                                                    string inputDay = FCL_Ex_factory;
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_ETD;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                    //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                    //===> test file 2 vi khac tuan phai lay truoc 1 tuan*** chu y test lai nguoc 1                     //*****test2
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);
                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                            }
                                            else
                                            {
                                                if ((int)day1 < (int)day2)
                                                {
                                                    //Response.Write("Ngày đứng trước là: " + day1);
                                                    //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                                    //bool isDiffWeek = IsDifferentWeek(day2, day2b);  //lay day2b lam goc
                                                    bool isDiffWeek = IsDifferentWeek2(day2, day2b);  //chu nhat la 7
                                                    if (isDiffWeek == true)
                                                    {
                                                        //khac tuan => lay lich tau nguoc lai ***** 
                                                        string inputDay = LLC_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        //neu la tuan dau cua thang
                                                        if (IsFirstWeekOfMonth(date_request1) == true)
                                                        {
                                                            //DateTime resultDay = GetSpecificDayInWeek_back(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        }
                                                        else
                                                        {
                                                            // TH khong phai tuan dau cua thang
                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        }
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //giu nguyen lich cach tinh cu *****   
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD                                                                                                                                
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }

                                                }
                                                else if ((int)day2 < (int)day1)
                                                {
                                                    //Response.Write("Ngày đứng trước là: " + day2);
                                                    //string ATPdate1 = ATPdate; // e.g., "2025-07-02 00:00:00.000"
                                                    //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                                    //DateTime date_request1 = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                                                    //1*so sánh tương quan ngày Ex-factory và ngày ETD xem có cùng tuần hay không? 
                                                    //neu khac tuan thi phai lay lich tau khac
                                                    //bool isDiffWeek = IsDifferentWeek(day1, day1b);   
                                                    bool isDiffWeek = IsDifferentWeek2(day1, day1b);
                                                    //Console.WriteLine(isDiffWeek ? "Khác tuần" : "Cùng tuần");
                                                    if (isDiffWeek == true)
                                                    {
                                                        //khac tuan => lay lich tau nguoc lai *****   
                                                        string inputDay = FCL_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        //neu la tuan dau cua thang
                                                        if (IsFirstWeekOfMonth(date_request1) == true)
                                                        {
                                                            //DateTime resultDay = GetSpecificDayInWeek_back(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        }
                                                        else
                                                        {
                                                            // TH khong phai tuan dau cua thang
                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        }

                                                        //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        //DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //giu nguyen lich cach tinh cu *****                                                
                                                        string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                        // In kết quả
                                                        //Response.Write($"Ngày {inputDay} trong tuần chứa {date_request:dd/MM/yyyy} là: {resultDay:dd/MM/yyyy}");
                                                    }

                                                }
                                                else
                                                {
                                                    //(int)day2 = (int)day1
                                                    DayOfWeek day11 = ConvertToDayOfWeek(FCL_ETD);
                                                    DayOfWeek day22 = ConvertToDayOfWeek(LLC_ETD);

                                                    if ((int)day11 < (int)day22)
                                                    {
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                //update len 2 gia tri len co so du lieu *****
                                                                                                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else if ((int)day22 < (int)day11)
                                                    {
                                                        string inputDay = LLC_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else if (Int32.Parse(stansit_time) > Int32.Parse(stansit_time2))
                                                    {
                                                        //truong hop nay so sanh vao phan transit time =>  ben nao transit time nao dai hon thi lay
                                                        //lay theo FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                //update len 2 gia tri len co so du lieu *****
                                                                                                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else if (Int32.Parse(stansit_time) < Int32.Parse(stansit_time2))
                                                    {
                                                        //lay theo LLC
                                                        string inputDay = LLC_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //lay theo FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                //update len 2 gia tri len co so du lieu *****
                                                                                                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }

                                                }
                                            }

                                        }
                                        else if (day1.HasValue && !day2.HasValue)
                                        {
                                            // truong hop 1 co gia tri, truong hop 2 khong co gia tri
                                            //giu nguyen lich cach tinh cu *****   
                                            string inputDay = FCL_Ex_factory;
                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                            string inputDay2 = FCL_ETD;
                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                            count = count + 1;
                                        }
                                        else if (!day1.HasValue && day2.HasValue)
                                        {
                                            // truong hop 2 co gia tri, truong hop 1 khong co gia tri
                                            //giu nguyen lich cach tinh cu *****                                                
                                            string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                            string inputDay2 = LLC_ETD;
                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                            //update len 2 gia tri len co so du lieu *****
                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                            count = count + 1;
                                        }
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                        else if (Category_ == "DPOversea")
                        {
                            //ap thang tinh lich tau cua DECT  //****
                            for (int i = 0; i < dt_tinhlichtau.Rows.Count; i++)
                            {
                                string ID_lichtau = dt_tinhlichtau.Rows[i]["ID"].ToString();
                                string modelname = dt_tinhlichtau.Rows[i]["Model"].ToString();
                                string Destination = dt_tinhlichtau.Rows[i]["Destination"].ToString();
                                string Country = dt_tinhlichtau.Rows[i]["Country"].ToString();

                                string ATPdate = dt_tinhlichtau.Rows[i]["ATPdate"].ToString(); //tinh so tuan cua ngay ATP date

                                //DateTime date_request = DateTime.ParseExact(ATPdate.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                                DateTime date_request = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                                //int weekOfMonth_rq = GetWeekOfMonth(date_request);

                                int weekOfMonth_rq = GetWeekOfMonth_New(date_request);

                                dt_mater_vessel = DataConn.StoreFillDS("Get_info_vessel", System.Data.CommandType.StoredProcedure, Destination, Country, Category_);
                                if (dt_mater_vessel.Rows[0][0].ToString() == "1")
                                {
                                    //tinh ngay ETD
                                    string FCL_Ex_factory = dt_mater_vessel.Rows[0]["FCL_Ex_factory"].ToString();
                                    string FCL_ETD = dt_mater_vessel.Rows[0]["FCL_ETD"].ToString();
                                    string FCL_ETA = dt_mater_vessel.Rows[0]["FCL_ETA"].ToString();
                                    string LLC_Ex_factory = dt_mater_vessel.Rows[0]["LLC_Ex_factory"].ToString();
                                    string LLC_ETD = dt_mater_vessel.Rows[0]["LLC_ETD"].ToString();
                                    string LLC_ETA = dt_mater_vessel.Rows[0]["LLC_ETA"].ToString();
                                    string AIR_Ex_factory = dt_mater_vessel.Rows[0]["AIR_Ex_factory"].ToString();
                                    string AIR_ETD = dt_mater_vessel.Rows[0]["AIR_ETD"].ToString();
                                    string AIR_ETA = dt_mater_vessel.Rows[0]["AIR_ETA"].ToString();

                                    string Special_exfactory_date = dt_mater_vessel.Rows[0]["Special_exfactory_date"].ToString();
                                    string SpecialETD_week = dt_mater_vessel.Rows[0]["SpecialETD_week"].ToString();
                                    string Special_ETA_Date = dt_mater_vessel.Rows[0]["Special_ETA_Date"].ToString();

                                    string stansit_time = "0";
                                    string stansit_time2 = "0";
                                    stansit_time = dt_mater_vessel.Rows[0]["FCL_ETA"].ToString();  //transit time
                                    stansit_time2 = dt_mater_vessel.Rows[0]["LLC_ETA"].ToString();  //transit time

                                    //lay theo tuan request date
                                    DayOfWeek? day1 = null;
                                    DayOfWeek? day1b = null;
                                    DayOfWeek? day2 = null;
                                    DayOfWeek? day2b = null;
                                    if (FCL_Ex_factory != "")
                                    {
                                        day1 = ConvertToDayOfWeek(FCL_Ex_factory); // "THU"; // giá trị truyền vào thu 5
                                    }
                                    if (FCL_ETD != "")
                                    {
                                        day1b = ConvertToDayOfWeek(FCL_ETD); // "MON"; // giá trị truyền vào thu 2
                                    }
                                    if (LLC_Ex_factory != "")
                                    {
                                        day2 = ConvertToDayOfWeek(LLC_Ex_factory);  // "TUE"; // giá trị truyền vào thu 3
                                    }
                                    if (LLC_ETD != "")
                                    {
                                        day2b = ConvertToDayOfWeek(LLC_ETD);  // "SUN"; // giá trị truyền vào thu CN
                                    }

                                    //check truong hop ca 2 lich deu khac tuan ==> so sanh ngay 
                                    bool isFCL = false;
                                    bool isLCL = false;
                                    if (day1.HasValue && day1b.HasValue)
                                    {
                                        isFCL = IsDifferentWeek2(day1, day1b);
                                    }
                                    if (day2.HasValue && day2b.HasValue)
                                    {
                                        isLCL = IsDifferentWeek2(day2, day2b);
                                    }
                                    if (isFCL == true && isLCL == true)   //check truong hop ca 2 lich deu khac tuan ==> so sanh ngay 
                                    {
                                        chk_khactuan = true;
                                    }
                                    else
                                    {
                                        chk_khactuan = false;
                                    }

                                    DateTime date_request1 = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                                    if (SpecialETD_week != "" && SpecialETD_week != "0")
                                    {
                                        //truong hop 1
                                        //tinh ra tuan cua special note //neu bang thi lay theo truoc do?
                                        if (weekOfMonth_rq <= int.Parse(SpecialETD_week))
                                        {
                                            //so sanh truong hop day1 va day2 xem truong hop null khong?
                                            if (day1.HasValue && day2.HasValue)
                                            {
                                                if (chk_khactuan == true)
                                                {
                                                    //*** truong hop nay co ****** doi xem co xay vao truong hop nay khong **** fix tiep trong tuong lai ******
                                                    //kiem tra ngay ATP co phai la tuan dau tien cua thang khong?? => neu la tuan dau lay luon lich trong tuan luon!   //Test 2 **** test file 2
                                                    //bool isFirstWeekOfMonth = date_request1.Day <= 7;
                                                    //if (isFirstWeekOfMonth == true)

                                                    //truong hop ca 2 lich deu khac tuan so sanh ngay ex-factorydate
                                                    if ((int)day1 < (int)day2)
                                                    {
                                                        //lay theo lich FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else if ((int)day2 < (int)day1)
                                                    {
                                                        //lay theo lich LCL
                                                        string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //(int)day2 = (int)day1 //*** truon hop nay tam thoi cu lay theo lich FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                }
                                                else
                                                {
                                                    if ((int)day1 < (int)day2)
                                                    {
                                                        //kiem tra ngay ATP co phai la tuan dau tien cua thang khong?? => neu la tuan dau lay luon lich trong tuan luon!   //Test 2 **** test file 2
                                                        bool isFirstWeekOfMonth = date_request1.Day <= 7;
                                                        //so sanh them ngay ETD trong tuan co phu hop khong??? //tuan dau cua thang  ****tuan dau - thang 9
                                                        if (weekOfMonth_rq == 1)   // && (int)day1 < (int)day2b
                                                        //if (isFirstWeekOfMonth == true)
                                                        {
                                                            //lay ngay ETD => cua FCL so sanh voi ngay ATP co cung thang hay khong????
                                                            string check_ETD = LLC_ETD;   //lay ngay ETD => cua LLC
                                                            DayOfWeek ck_ETD = ConvertToDayOfWeek(check_ETD);
                                                            DateTime ck_ngay_ETD = GetSpecificDayInWeek(date_request1, ck_ETD);  //tinh ra ngay ETD
                                                            //check ngay ATP va ngay ETD co cung tuan hay khong (cung thang)??? ***pending
                                                            bool isSameMonth = (ck_ngay_ETD.Month == date_request1.Month) && (ck_ngay_ETD.Year == date_request1.Year);

                                                            //code old 26.09.2025
                                                            ////lay lich FCL 
                                                            //string inputDay = FCL_Ex_factory;
                                                            //DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            //string inputDay2 = FCL_ETD;
                                                            //DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            //DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD                                                            
                                                            ////tinh ra ngay ETA =  Ngay ETD + transitime
                                                            //DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            //count = count + 1;

                                                            //****** so sanh tuan ATP va tuan ETD cung thang khong ?*****
                                                            if (isSameMonth)
                                                            {
                                                                //Response.Write("Hai ngày cùng tháng.");
                                                                string inputDay = LLC_Ex_factory;
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = LLC_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                                DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD                                                            
                                                                                                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }
                                                            else if ((int)day2 < (int)day1b)  //tuan dau cua thang  ****tuan dau - thang 9
                                                            {
                                                                //lay lich FCL 
                                                                string inputDay = FCL_Ex_factory;
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = FCL_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD                                                            
                                                                                                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }
                                                            else
                                                            {
                                                                //Response.Write("Hai ngày KHÔNG cùng tháng.");
                                                                string inputDay = FCL_Ex_factory;
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = FCL_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD                                                            
                                                                                                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }


                                                        }
                                                        else
                                                        {
                                                            //bool isDiffWeek = IsDifferentWeek(day2, day2b);  //lay day2b lam goc
                                                            bool isDiffWeek = IsDifferentWeek2(day2, day2b);  //lay day2b lam goc
                                                            if (isDiffWeek == true)
                                                            {
                                                                //khac tuan => lay lich tau nguoc lai ***** 
                                                                string inputDay = LLC_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = LLC_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);  //ngay ETD

                                                                DateTime Datecuoithang = GetSpecificDayInWeek(date_request1, targetDay2); //ngay ETD la ngay cuoi thang
                                                                bool isLastDayOfMonth = Datecuoithang.Day == DateTime.DaysInMonth(Datecuoithang.Year, Datecuoithang.Month);
                                                                if (isLastDayOfMonth)
                                                                {
                                                                    //Console.WriteLine("ResultDay là ngày cuối tháng.");  //ngay ETD la ngay cuoi thang
                                                                    //giu nguyen lich cach tinh cu ***** vi day1 < day2
                                                                    string inputDayb = FCL_Ex_factory;
                                                                    DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                                    string inputDay2b = FCL_ETD;
                                                                    DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                                    DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD

                                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                }
                                                                else
                                                                {
                                                                    // Console.WriteLine("ResultDay KHÔNG phải là ngày cuối tháng."); 
                                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                                }

                                                                ////neu la tuan dau cua thang
                                                                //if (IsFirstWeekOfMonth(date_request1) == true)
                                                                //{}
                                                                //else
                                                                //{
                                                                //    // TH khong phai tuan dau cua thang
                                                                //}
                                                                count = count + 1;
                                                            }
                                                            else
                                                            {
                                                                //giu nguyen lich cach tinh cu *****   
                                                                string inputDay = FCL_Ex_factory;
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = FCL_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                        //update len 2 gia tri len co so du lieu *****
                                                                                                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }
                                                        }

                                                    }
                                                    else if ((int)day2 < (int)day1)
                                                    {
                                                        //Response.Write("Ngày đứng trước là: " + day2);
                                                        //string ATPdate1 = ATPdate; // e.g., "2025-07-02 00:00:00.000"
                                                        //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                                        //DateTime check_date_request = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                                                        //kiem tra ngay ATP co phai la tuan dau tien cua thang khong?? => neu la tuan dau lay luon lich trong tuan luon!   //Test 2 **** test file 2
                                                        bool isFirstWeekOfMonth = date_request1.Day <= 7;
                                                        //&& (int)day2 < (int)day1b so sanh them ngay ETD trong tuan co phu hop khong???
                                                        if (weekOfMonth_rq == 1) //&& (int)day2 < (int)day1b //tuan dau cua thang  ****tuan dau - thang 9
                                                        //if (isFirstWeekOfMonth == true)
                                                        {

                                                            //lay ngay ETD => cua FCL so sanh voi ngay ATP co cung thang hay khong????
                                                            string check_ETD = FCL_ETD;   //lay ngay ETD => cua FCL
                                                            DayOfWeek ck_ETD = ConvertToDayOfWeek(check_ETD);
                                                            DateTime ck_ngay_ETD = GetSpecificDayInWeek(date_request1, ck_ETD);  //tinh ra ngay ETD
                                                            //check ngay ATP va ngay ETD co cung tuan hay khong (cung thang)??? ***pending
                                                            bool isSameMonth = (ck_ngay_ETD.Month == date_request1.Month) && (ck_ngay_ETD.Year == date_request1.Year);

                                                            //code old 26.09.2025
                                                            //string inputDayb = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                            //DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                            //string inputDay2b = LLC_ETD;
                                                            //DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                            //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                            //DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD

                                                            //DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            //count = count + 1;

                                                            if (isSameMonth)
                                                            {
                                                                //Response.Write("Hai ngày cùng tháng.");
                                                                //so sanh 2 ngay ex-factory xem ngay nao nho hon thi lay theo lich (tren da so sanh roi : ((int)day2 < (int)day1) )
                                                                //lay lich FCC / FCC
                                                                string inputDayb = FCL_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                                DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                                string inputDay2b = FCL_ETD;
                                                                DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                                DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD

                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }
                                                            else if ((int)day2 < (int)day1b)  //tuan dau cua thang  ****tuan dau - thang 9
                                                            {
                                                                string inputDayb = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                                DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                                string inputDay2b = LLC_ETD;
                                                                DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD

                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }
                                                            else
                                                            {
                                                                //Response.Write("Hai ngày KHÔNG cùng tháng.");
                                                                //lay lich LLC
                                                                string inputDayb = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                                DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                                string inputDay2b = LLC_ETD;
                                                                DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD

                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                count = count + 1;
                                                            }

                                                        }
                                                        else
                                                        {
                                                            //1*so sánh tương quan ngày Ex-factory và ngày ETD xem có cùng tuần hay không? 
                                                            //neu khac tuan thi phai lay lich tau khac
                                                            //bool isDiffWeek = IsDifferentWeek(day1, day1b);  //lay day1b lam goc
                                                            bool isDiffWeek = IsDifferentWeek2(day1, day1b);  //lay day1b lam goc                                                                                                     
                                                            if (isDiffWeek == true)
                                                            {
                                                                //khac tuan => lay lich tau nguoc lai *****   
                                                                string inputDay = FCL_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = FCL_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2); //ngay ETD

                                                                DateTime Datecuoithang = GetSpecificDayInWeek(date_request1, targetDay2); //ngay ETD la ngay cuoi thang
                                                                bool isLastDayOfMonth = Datecuoithang.Day == DateTime.DaysInMonth(Datecuoithang.Year, Datecuoithang.Month);
                                                                if (isLastDayOfMonth)
                                                                {
                                                                    //Console.WriteLine("ResultDay là ngày cuối tháng.");  //ngay ETD la ngay cuoi thang
                                                                    //giu nguyen lich cach tinh cu *****  vi day2 < day 1                                              
                                                                    string inputDayb = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                                    DayOfWeek targetDayb = ConvertToDayOfWeek(inputDayb);
                                                                    string inputDay2b = LLC_ETD;
                                                                    DayOfWeek targetDay2b = ConvertToDayOfWeek(inputDay2b);

                                                                    DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDayb);   // tinh ra ngay Ex-factory day
                                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2b);  //tinh ra ngay ETD
                                                                                                                                             //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                                }
                                                                else
                                                                {
                                                                    // Console.WriteLine("ResultDay KHÔNG phải là ngày cuối tháng."); 
                                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                                }

                                                                count = count + 1;
                                                            }
                                                            else
                                                            {
                                                                //giu nguyen lich cach tinh cu *****                                                
                                                                string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                                string inputDay2 = LLC_ETD;
                                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                                //update len 2 gia tri len co so du lieu *****
                                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                                count = count + 1;
                                                                // In kết quả
                                                                //Response.Write($"Ngày {inputDay} trong tuần chứa {date_request:dd/MM/yyyy} là: {resultDay:dd/MM/yyyy}");
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        //*** truong hop nay co ****** doi xem co xay vao truong hop nay khong **** fix tiep trong tuong lai ******
                                                        //kiem tra ngay ATP co phai la tuan dau tien cua thang khong?? => neu la tuan dau lay luon lich trong tuan luon!   //Test 2 **** test file 2
                                                        //bool isFirstWeekOfMonth = date_request1.Day <= 7;
                                                        //if (isFirstWeekOfMonth == true)

                                                        //(int)day2 = (int)day1
                                                        DayOfWeek day11 = ConvertToDayOfWeek(FCL_ETD);
                                                        DayOfWeek day22 = ConvertToDayOfWeek(LLC_ETD);
                                                        if ((int)day11 < (int)day22)
                                                        {
                                                            string inputDay = FCL_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        else if ((int)day22 < (int)day11)
                                                        {
                                                            string inputDay = LLC_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = LLC_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            //update len 2 gia tri len co so du lieu *****
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        else if (Int32.Parse(stansit_time) > Int32.Parse(stansit_time2))
                                                        {
                                                            //lay theo FCL
                                                            string inputDay = FCL_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        else if (Int32.Parse(stansit_time) < Int32.Parse(stansit_time2))
                                                        {
                                                            //lay theo LLC
                                                            string inputDay = LLC_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = LLC_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        else
                                                        {
                                                            //lay theo FCL
                                                            string inputDay = FCL_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }

                                                    }
                                                }

                                            }
                                            else if (day1.HasValue && !day2.HasValue)
                                            {
                                                // truong hop 1 co gia tri, truong hop 2 khong co gia tri
                                                //giu nguyen lich cach tinh cu *****   
                                                string inputDay = FCL_Ex_factory;
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = FCL_ETD;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                count = count + 1;
                                            }
                                            else if (!day1.HasValue && day2.HasValue)
                                            {
                                                // truong hop 2 co gia tri, truong hop 1 khong co gia tri
                                                //giu nguyen lich cach tinh cu *****                                                
                                                string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = LLC_ETD;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                //update len 2 gia tri len co so du lieu *****
                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                count = count + 1;
                                            }
                                        }
                                        else
                                        {
                                            //lay theo tuan special note   ==> khong lay theo ngay ATPdate ==> quy tac van giong tren (khac ngay ATP date => chon ngay thu 2 trong tuan dacbiet)
                                            // Lấy năm & tháng từ ATPdate (hoặc gán thủ công nếu bạn biết tháng)
                                            //DateTime atpDate = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                            DateTime atpDate = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                                            int year = atpDate.Year;
                                            int month = atpDate.Month;

                                            // Tuần đặc biệt (SpecialETD_week) truyền vào từ DB
                                            int specialWeek = int.Parse(SpecialETD_week); // ví dụ: 3
                                            // Tìm ngày Thứ Hai của tuần thứ N trong tháng
                                            DateTime firstDayOfMonth = new DateTime(year, month, 1);
                                            int dayOffset = ((int)firstDayOfMonth.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                                            DateTime firstMonday = firstDayOfMonth.AddDays(-dayOffset <= 0 ? -dayOffset : 7 - dayOffset); // bắt đầu từ tuần chứa ngày đầu tháng

                                            DateTime mondayOfSpecialWeek = firstMonday.AddDays((specialWeek - 1) * 7);  //tim ra ngay 1 ngay cua tuan dac biet (thu 2 dau tuan)

                                            // so sanh lich trong shipment
                                            //DayOfWeek day1 = ConvertToDayOfWeek(FCL_Ex_factory); // "THU"; // giá trị truyền vào thu 5
                                            //DayOfWeek day1b = ConvertToDayOfWeek(FCL_ETD); // "MON"; // giá trị truyền vào thu 2
                                            //DayOfWeek day2 = ConvertToDayOfWeek(LLC_Ex_factory);  // "TUE"; // giá trị truyền vào thu 3
                                            //DayOfWeek day2b = ConvertToDayOfWeek(LLC_ETD);  // "SUN"; // giá trị truyền vào thu CN

                                            if (day1.HasValue && day2.HasValue)
                                            {
                                                if (chk_khactuan == true)
                                                {
                                                    if ((int)day1 < (int)day2)
                                                    {
                                                        //lay theo lich FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else if ((int)day2 < (int)day1)
                                                    {
                                                        //lay theo lich LCL
                                                        string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //(int)day2 = (int)day1 //*** truon hop nay tam thoi cu lay theo lich FCL
                                                        //lay theo lich FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }

                                                }
                                                else
                                                {
                                                    if ((int)day1 < (int)day2)
                                                    {
                                                        //Response.Write("Ngày đứng trước là: " + day1);
                                                        //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);                                                
                                                        //bool isDiffWeek = IsDifferentWeek(day2, day2b);  //lay day2b lam goc
                                                        bool isDiffWeek = IsDifferentWeek2(day2, day2b);  //lay day2b lam goc
                                                        if (isDiffWeek == true)
                                                        {
                                                            //khac tuan => lay lich tau nguoc lai ***** 
                                                            string inputDay = LLC_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = LLC_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                            //neu la tuan dau cua thang
                                                            //if (IsFirstWeekOfMonth(mondayOfSpecialWeek) == true)
                                                            //{                                                        
                                                            //}
                                                            //else
                                                            //{
                                                            //    // TH khong phai tuan dau cua thang                                                        
                                                            //}
                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD
                                                                                                                                          //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                          //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                            count = count + 1;
                                                        }
                                                        else
                                                        {
                                                            //giu nguyen lich cach tinh cu *****   
                                                            string inputDay = FCL_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                            DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD
                                                                                                                                          //update len 2 gia tri len co so du lieu *****
                                                                                                                                          //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                          //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }

                                                    }
                                                    else if ((int)day2 < (int)day1)
                                                    {
                                                        //Response.Write("Ngày đứng trước là: " + day2);
                                                        //string ATPdate1 = ATPdate; // e.g., "2025-07-02 00:00:00.000"
                                                        //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                                                        //1*so sánh tương quan ngày Ex-factory và ngày ETD xem có cùng tuần hay không? 
                                                        //neu khac tuan thi phai lay lich tau khac
                                                        //bool isDiffWeek = IsDifferentWeek(day1, day1b);  //lay day1b lam goc
                                                        bool isDiffWeek = IsDifferentWeek2(day1, day1b);  //lay day1b lam goc

                                                        if (isDiffWeek == true)
                                                        {
                                                            //khac tuan => lay lich tau nguoc lai *****   
                                                            string inputDay = FCL_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD
                                                                                                                                          //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                          //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        else
                                                        {
                                                            //giu nguyen lich cach tinh cu *****                                                
                                                            string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = LLC_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                            //update len 2 gia tri len co so du lieu *****
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                            // In kết quả
                                                            //Response.Write($"Ngày {inputDay} trong tuần chứa {date_request:dd/MM/yyyy} là: {resultDay:dd/MM/yyyy}");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //(int)day2 = (int)day1  ==> TH bang nhau // so sanh ngay ETD de lay lich tau
                                                        DayOfWeek day11 = ConvertToDayOfWeek(FCL_ETD);
                                                        DayOfWeek day22 = ConvertToDayOfWeek(LLC_ETD);
                                                        if ((int)day11 < (int)day22)
                                                        {
                                                            string inputDay = FCL_Ex_factory;
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = FCL_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD
                                                                                                                                          //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                          //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            count = count + 1;
                                                        }
                                                        if ((int)day22 < (int)day11)
                                                        {
                                                            string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                            string inputDay2 = LLC_ETD;
                                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                            DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                            //update len 2 gia tri len co so du lieu *****
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                            count = count + 1;
                                                        }
                                                        //*****de y xem con truong hop so sanh theo transit time theo truong hop nay khong????
                                                    }
                                                }

                                            }
                                            else if (day1.HasValue && !day2.HasValue)
                                            {
                                                string inputDay = FCL_Ex_factory;
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = FCL_ETD;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day  //ngay tuan dac biet  //test 2******
                                                DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                count = count + 1;
                                            }
                                            else if (!day1.HasValue && day2.HasValue)
                                            {
                                                string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = LLC_ETD;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay);   // tinh ra ngay Ex-factory day
                                                DateTime resultDay2 = GetSpecificDayInWeek(mondayOfSpecialWeek, targetDay2);  //tinh ra ngay ETD

                                                //update len 2 gia tri len co so du lieu *****
                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                count = count + 1;
                                            }

                                        }
                                    }
                                    else if (Special_ETA_Date != "" && SpecialETD_week != "0")
                                    {
                                        //truong hop 3
                                        //dựa vào ngày ship date tháng hiện tại là tháng nào  
                                        //dt_tinhlichtau.Rows[i]["ATPdate"].ToString() //  7/16/2025  => thang 8
                                        //tinh Special_ETA_Date tang them 1 thang so ngay la 14: => 8/14/2025  ***** dua vao so ngay la bao nhieu thi cong them so thang
                                        int soNgay = Int32.Parse(dt_mater_vessel.Rows[0]["Special_ETA_Date"].ToString());
                                        DateTime ngayGoc = DateTime.Parse(dt_tinhlichtau.Rows[i]["ATPdate"].ToString());
                                        //DateTime ngayCongSoNgay = ngayGoc.AddDays(soNgay); // ngày sau khi cộng 14 ngày
                                        DateTime ngayThay = new DateTime(ngayGoc.Year, ngayGoc.Month, soNgay);
                                        DateTime ketQua = ngayThay;

                                        //dua vao so ngay transit time la bao nhieu ? thi cong them so thang
                                        //co 2 transit time => chon transitme cua lich tau nao???
                                        //??? tam lay transit time cua lich tau 1
                                        if (Int32.Parse(stansit_time) <= 45)
                                        {
                                            ketQua = ngayThay.AddMonths(1); // cộng thêm 1 tháng vào ngày trên   
                                        }
                                        else
                                        {
                                            ketQua = ngayThay.AddMonths(2);
                                        }

                                        // Trả về ngày dạng "dd/MM/yyyy" hoặc bạn muốn
                                        //string ketQuaNgay = ketQua.ToString("dd/MM/yyyy");

                                        //So sánh ngày trong tuần (FCL & LCL) ngày nào trước thì chọn để lấy ra transit time de tru di
                                        DayOfWeek day11 = ConvertToDayOfWeek(FCL_ETD); // "FRI"; // giá trị truyền vào thu 5
                                        DayOfWeek day22 = ConvertToDayOfWeek(LLC_ETD);  // "MON"; // giá trị truyền vào thu 3

                                        if ((int)day1 == (int)day2)  //truong hop 2 ngay ex-factory bang nhau  ==> so sanh ngay ETD
                                        {
                                            if ((int)day11 < (int)day22)
                                            {
                                                //lay ngay nao nho < hon thi tinh toan****
                                                //so sanh Thứ ngày ETD trước thứ ngày Ex-factory thì lấy ngày ex-factory là tuần trước. còn nếu sau thì vẫn lấy trong tuần.

                                                //tinh toan nhu cu  ****
                                                //stansit_time = dt_mater_vessel.Rows[0]["FCL_ETA"].ToString();  //transit time
                                                DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));

                                                bool sameWeek = IsSameWeek(date_request1, ngayDatru1);
                                                if (sameWeek == true)
                                                {
                                                    //string ngaycantim1 = ngayDatru1.ToString("dd/MM/yyyy");  // giu nguyen cach tinh cu *****
                                                    string inputDay = FCL_ETD;
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay2 = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day

                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                    if (date_request1.Date < ngayDatru1.Date)
                                                    {
                                                        // lay theo tuan ATP request
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay2 = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day

                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        // giu nguyen cach tinh cu *****
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay2 = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day

                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                }

                                            }
                                            else if ((int)day22 < (int)day11)
                                            {
                                                //lay gia tri transit time  la "MON" < "WED"
                                                //lay ngay tang len 1 thang - transit time : 8/14/2025 - 28
                                                //stansit_time = dt_mater_vessel.Rows[0]["LLC_ETA"].ToString();  ////transit time

                                                //tinh toan nhu cu ******
                                                DateTime ngayDatru2 = ketQua.AddDays(-Int32.Parse(stansit_time2));  //ngay deadline muộn nhất ngày ETD
                                                //so sanh tuan muon nhat nay ETD voi tuan ATP xem cung tuan khong?

                                                bool sameWeek = IsSameWeek(date_request1, ngayDatru2);
                                                if (sameWeek == true)
                                                {
                                                    //cung tuan nhau  // giu nguyen cach tinh cu *****
                                                    //string ngaycantim2 = ngayDatru2.ToString("dd/MM/yyyy");

                                                    string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = LLC_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru2, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru2, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)                                                                                                                            
                                                                                                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                    if (date_request1.Date < ngayDatru2.Date)
                                                    {
                                                        // lay theo tuan ATP request
                                                        string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)                                                                                                                            

                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        // giu nguyen cach tinh cu *****
                                                        string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru2, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru2, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)                                                                                                                            

                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                }

                                            }
                                            else if (Int32.Parse(stansit_time) > Int32.Parse(stansit_time2))
                                            {
                                                //lay teho trantsit time1  //lay theo FCL
                                                DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));
                                                //string ngaycantim1 = ngayDatru1.ToString("dd/MM/yyyy");
                                                bool sameWeek = IsSameWeek(date_request1, ngayDatru1);
                                                if (sameWeek == true)
                                                {
                                                    //giu nguyen cach tinh cu ****
                                                    string inputDay = FCL_ETD;
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                    if (date_request1.Date < ngayDatru1.Date)
                                                    {
                                                        // lay theo tuan ATP date
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                       //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //giu nguyen cach tinh cu ****
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                }
                                            }
                                            else if (Int32.Parse(stansit_time) < Int32.Parse(stansit_time2))
                                            {
                                                //lay teho trantsit time2  //lay theo LLC
                                                DateTime ngayDatru2 = ketQua.AddDays(-Int32.Parse(stansit_time2));

                                                bool sameWeek = IsSameWeek(date_request1, ngayDatru2);
                                                if (sameWeek == true)
                                                {
                                                    //lay theo cach tinh cu ****
                                                    string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = LLC_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru2, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru2, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)
                                                                                                                                //DateTime ngayTru1Tuan2 = resultDay2.AddDays(-7); // Trừ 7 ngày
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                    if (date_request1.Date < ngayDatru2.Date)
                                                    {
                                                        //lay theo ngay ATP date
                                                        string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)
                                                                                                                                       //DateTime ngayTru1Tuan2 = resultDay2.AddDays(-7); // Trừ 7 ngày
                                                                                                                                       //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //lay theo cach tinh cu ****
                                                        string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru2, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru2, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)
                                                                                                                                    //DateTime ngayTru1Tuan2 = resultDay2.AddDays(-7); // Trừ 7 ngày
                                                                                                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                //lay teho trantsit time1  //lay theo FCL
                                                DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));

                                                bool sameWeek = IsSameWeek(date_request1, ngayDatru1);
                                                if (sameWeek == true)
                                                {
                                                    //lay theo cach tinh cu ****
                                                    string inputDay = FCL_ETD;
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                    if (date_request1.Date < ngayDatru1.Date)
                                                    {
                                                        //lay theo ngay ATP date
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                       //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //lay theo cach tinh cu ****
                                                        string inputDay = FCL_ETD;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_Ex_factory;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                        DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day
                                                                                                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                }

                                            }
                                        }
                                        else if ((int)day1 < (int)day2)
                                        {
                                            //tinh toan nhu cu  ****
                                            //stansit_time = dt_mater_vessel.Rows[0]["FCL_ETA"].ToString();  //transit time
                                            DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));

                                            bool sameWeek = IsSameWeek(date_request1, ngayDatru1);
                                            if (sameWeek == true)
                                            {
                                                //lay theo cach tinh cu ****
                                                string inputDay = FCL_ETD;// "THU"; // giá trị truyền vào thu 5
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = FCL_Ex_factory;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                count = count + 1;
                                            }
                                            else
                                            {
                                                //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                if (date_request1.Date < ngayDatru1.Date)
                                                {
                                                    //lay theo ngay ATP date
                                                    string inputDay = FCL_ETD;// "THU"; // giá trị truyền vào thu 5
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //lay theo cach tinh cu ****
                                                    string inputDay = FCL_ETD;// "THU"; // giá trị truyền vào thu 5
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                            }


                                        }
                                        else if ((int)day2 < (int)day1)
                                        {
                                            DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time2));

                                            bool sameWeek = IsSameWeek(date_request1, ngayDatru1);
                                            if (sameWeek == true)
                                            {
                                                //lay theo cach tinh cu ****
                                                string inputDay = LLC_ETD;// "THU"; // giá trị truyền vào thu 5
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = LLC_Ex_factory;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                count = count + 1;
                                            }
                                            else
                                            {
                                                //khac tuan nhau => so sanh xem tuan nao truoc thi lay
                                                if (date_request1.Date < ngayDatru1.Date)
                                                {
                                                    //lay theo ngay ATP date
                                                    string inputDay = LLC_ETD;// "THU"; // giá trị truyền vào thu 5
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = LLC_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //lay theo cach tinh cu ****
                                                    string inputDay = LLC_ETD;// "THU"; // giá trị truyền vào thu 5
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = LLC_Ex_factory;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    DateTime resultDay2 = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                            }

                                        }
                                    }
                                    else if (Special_exfactory_date != "" && SpecialETD_week != "0")
                                    {
                                        //truong hop 2  => cate dect khong co truong hop 2
                                    }
                                    else
                                    {
                                        //truong hop khong co ngay special note => quy ra tuần ATP -> Đối chiếu lịch tàu trong tuần đó 

                                        //so sanh truong hop day1 va day2 xem truong hop null khong?
                                        if (day1.HasValue && day2.HasValue)
                                        {
                                            if (chk_khactuan == true)
                                            {
                                                //truong hop 2 lich deu khac tuan
                                                if ((int)day1 < (int)day2)
                                                {
                                                    //lay theo lich FCL
                                                    string inputDay = FCL_Ex_factory;
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_ETD;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                    //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                    //===> test file 2 vi khac tuan phai lay truoc 1 tuan*** chu y test lai nguoc 1         //*****test2
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);
                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                                else if ((int)day2 < (int)day1)
                                                {
                                                    //lay theo lich LCL
                                                    string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = LLC_ETD;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                    //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                    //===> test file 2 vi khac tuan phai lay truoc 1 tuan*** chu y test lai nguoc 1                     //*****test2
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day 
                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                    //update len 2 gia tri len co so du lieu *****
                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                    count = count + 1;
                                                }
                                                else
                                                {
                                                    //(int)day2 = (int)day1 //*** truon hop nay tam thoi cu lay theo lich FCL
                                                    string inputDay = FCL_Ex_factory;
                                                    DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                    string inputDay2 = FCL_ETD;
                                                    DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                    //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                    //===> test file 2 vi khac tuan phai lay truoc 1 tuan*** chu y test lai nguoc 1                     //*****test2
                                                    DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);
                                                    DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                    //tinh ra ngay ETA =  Ngay ETD + transitime
                                                    DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                    dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                    count = count + 1;
                                                }
                                            }
                                            else
                                            {
                                                if ((int)day1 < (int)day2)
                                                {
                                                    //Response.Write("Ngày đứng trước là: " + day1);
                                                    //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                                    //bool isDiffWeek = IsDifferentWeek(day2, day2b);  //lay day2b lam goc
                                                    bool isDiffWeek = IsDifferentWeek2(day2, day2b);  //chu nhat la 7
                                                    if (isDiffWeek == true)
                                                    {
                                                        //khac tuan => lay lich tau nguoc lai ***** 
                                                        string inputDay = LLC_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        //neu la tuan dau cua thang
                                                        if (IsFirstWeekOfMonth(date_request1) == true)
                                                        {
                                                            //DateTime resultDay = GetSpecificDayInWeek_back(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        }
                                                        else
                                                        {
                                                            // TH khong phai tuan dau cua thang
                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        }
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //giu nguyen lich cach tinh cu *****   
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD                                                                                                                                
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }

                                                }
                                                else if ((int)day2 < (int)day1)
                                                {
                                                    //Response.Write("Ngày đứng trước là: " + day2);
                                                    //string ATPdate1 = ATPdate; // e.g., "2025-07-02 00:00:00.000"
                                                    //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                                    //DateTime date_request1 = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                                                    //1*so sánh tương quan ngày Ex-factory và ngày ETD xem có cùng tuần hay không? 
                                                    //neu khac tuan thi phai lay lich tau khac
                                                    //bool isDiffWeek = IsDifferentWeek(day1, day1b);   
                                                    bool isDiffWeek = IsDifferentWeek2(day1, day1b);
                                                    //Console.WriteLine(isDiffWeek ? "Khác tuần" : "Cùng tuần");
                                                    if (isDiffWeek == true)
                                                    {
                                                        //khac tuan => lay lich tau nguoc lai *****   
                                                        string inputDay = FCL_Ex_factory;// "THU"; // giá trị truyền vào thu 5
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        //neu la tuan dau cua thang
                                                        if (IsFirstWeekOfMonth(date_request1) == true)
                                                        {
                                                            //DateTime resultDay = GetSpecificDayInWeek_back(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        }
                                                        else
                                                        {
                                                            // TH khong phai tuan dau cua thang
                                                            DateTime resultDay = GetSpecificDayInPreviousWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        }

                                                        //DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        //DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //giu nguyen lich cach tinh cu *****                                                
                                                        string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                        count = count + 1;
                                                        // In kết quả
                                                        //Response.Write($"Ngày {inputDay} trong tuần chứa {date_request:dd/MM/yyyy} là: {resultDay:dd/MM/yyyy}");
                                                    }

                                                }
                                                else
                                                {
                                                    //(int)day2 = (int)day1
                                                    DayOfWeek day11 = ConvertToDayOfWeek(FCL_ETD);
                                                    DayOfWeek day22 = ConvertToDayOfWeek(LLC_ETD);

                                                    if ((int)day11 < (int)day22)
                                                    {
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                //update len 2 gia tri len co so du lieu *****
                                                                                                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else if ((int)day22 < (int)day11)
                                                    {
                                                        string inputDay = LLC_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else if (Int32.Parse(stansit_time) > Int32.Parse(stansit_time2))
                                                    {
                                                        //truong hop nay so sanh vao phan transit time =>  ben nao transit time nao dai hon thi lay
                                                        //lay theo FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                //update len 2 gia tri len co so du lieu *****
                                                                                                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else if (Int32.Parse(stansit_time) < Int32.Parse(stansit_time2))
                                                    {
                                                        //lay theo LLC
                                                        string inputDay = LLC_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = LLC_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                                        //update len 2 gia tri len co so du lieu *****
                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

                                                        //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        //lay theo FCL
                                                        string inputDay = FCL_Ex_factory;
                                                        DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                        string inputDay2 = FCL_ETD;
                                                        DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                                        DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                                        DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD
                                                                                                                                //update len 2 gia tri len co so du lieu *****
                                                                                                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                                                                                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                        DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                                        dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                        count = count + 1;
                                                    }

                                                }
                                            }

                                        }
                                        else if (day1.HasValue && !day2.HasValue)
                                        {
                                            // truong hop 1 co gia tri, truong hop 2 khong co gia tri
                                            //giu nguyen lich cach tinh cu *****   
                                            string inputDay = FCL_Ex_factory;
                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                            string inputDay2 = FCL_ETD;
                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);
                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time));
                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                            count = count + 1;
                                        }
                                        else if (!day1.HasValue && day2.HasValue)
                                        {
                                            // truong hop 2 co gia tri, truong hop 1 khong co gia tri
                                            //giu nguyen lich cach tinh cu *****                                                
                                            string inputDay = LLC_Ex_factory;// "TUE"; // giá trị truyền vào thu 3
                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                            string inputDay2 = LLC_ETD;
                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                            DateTime resultDay = GetSpecificDayInWeek(date_request1, targetDay);   // tinh ra ngay Ex-factory day
                                            DateTime resultDay2 = GetSpecificDayInWeek(date_request1, targetDay2);  //tinh ra ngay ETD

                                            //update len 2 gia tri len co so du lieu *****
                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);
                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                            DateTime dateETA = resultDay2.AddDays(Int32.Parse(stansit_time2));
                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                            count = count + 1;
                                        }
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                        else if (Category_ == "DP")
                        {

                        }
                        else if (Category_ == "MW")
                        {

                        }

                        if (count > 1)
                        {
                            lblConfirm.Text = "so ban ghi duoc update : " + count;
                            lblConfirm.Attributes.Add("style", "color:green");
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Du lieu update thanh cong!');", true);
                            //load lai du lieu
                            dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate", System.Data.CommandType.StoredProcedure);
                        }
                        else
                        {
                            lblConfirm.Text = "so ban ghi duoc update : " + count;
                            lblConfirm.Attributes.Add("style", "color:red");
                            //load lai du lieu
                            dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate", System.Data.CommandType.StoredProcedure);
                        }
                        //
                        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Da tinh xong ngay, kiem tra lai!');", true);
                        //load lai du lieu
                        //dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate", System.Data.CommandType.StoredProcedure);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }


                }

            }



        }

        public void Download_Click(object sender, EventArgs e)
        {

            DataTable dt_dowload = new DataTable();
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string _checkpartno = Request.Form["check_history_search"];

            string category = dr_filter_Cate.SelectedValue;
            string uploadno = dr_filter_namegroup.SelectedValue;

            string _modelname = model_search.Value.ToString();
            string _countryname = country_search.Value.ToString();

            string statushistory = "off";

            string status_ex_risky = "off";
            string status_TLL_sum = "off";

            if (_checkpartno == "on")
            {
                statushistory = "on";
            }

            //truong hop export detail  (normal)
            string relativePath = "Mau_Report_Detail.xlsx";
            string localPath = Server.MapPath(relativePath);

            // Đường dẫn để lưu file Excel mới
            string newFileName = "Report_Detail.xlsx"; // Tên file mới
            string newFilePath = Server.MapPath("Textfile/" + newFileName); // Đường dẫn đầy đủ

            // Gọi phương thức để xử lý file Excel và lưu file mới
            ProcessExcelFile1(localPath, newFilePath, _fromdate, _todate, category, statushistory);

            // Tải xuống file mới
            DownloadFile(newFilePath, newFileName);

        }

        //ham doi dinh dang ngay thang
        static void SetDate(ExcelWorksheet ws, int r, int c, object value)
        {
            if (value != DBNull.Value)
            {
                if (DateTime.TryParse(value.ToString(), out DateTime d))
                {
                    ws.Cells[r, c].Value = d;
                    ws.Cells[r, c].Style.Numberformat.Format = "dd/MM/yyyy";
                    return;
                }
            }
            ws.Cells[r, c].Value = "";
        }

        static void ProcessExcelFile1(string filePath, string newFilePath, string tungay, string denngay, string category, string status_ex)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            // Đảm bảo file tồn tại
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File không tồn tại", filePath);
            }
            // Tạo file mới để lưu kết quả
            FileInfo newFileInfo = new FileInfo(newFilePath);

            DataTable dt_all = new DataTable();
            DataTable dt_sum_qty_TLL = new DataTable();
            string uploadno = "==UploadNo==";
            string _modelname = "";
            string _countryname = "";

            //bo tinh tong di => dat cong thuc subtotal trong excel   ****ttinh sheet 2
            dt_sum_qty_TLL = DataConn.StoreFillDS("Select_Report_Sum", System.Data.CommandType.StoredProcedure, tungay, denngay, category, status_ex);
            //loc theo ngay
            if (category == "==Category==")
            {

                dt_all = DataConn.StoreFillDS("Select_Upload_VanningDate2_HS", System.Data.CommandType.StoredProcedure, tungay, denngay, status_ex, uploadno, _modelname, _countryname);
            }
            else
            {
                dt_all = DataConn.StoreFillDS("Select_Upload_VanningDate2_cate_HS", System.Data.CommandType.StoredProcedure, tungay, denngay, category, status_ex, uploadno, _modelname, _countryname);
            }

            using (var package = new ExcelPackage(fileInfo))
            {
                //var worksheet = package.Workbook.Worksheets["Sheet1"];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                //worksheet.Cells["D5"].Value = tungay;// "Thông tin mới";

                if (worksheet == null)
                {
                    throw new Exception("Không tìm thấy sheet 'Sheet1' trong file Excel.");
                }

                int row = 3;
                int i = 0;
                //DateTime currentDate = DateTime.Today;

                //tinh tong cac cot va zen vo o cell
                //worksheet.Cells[1, 8].Value = dt_sum_qty_TLL.Rows[0][0].ToString();
                //worksheet.Cells[1, 10].Value = dt_sum_qty_TLL.Rows[0][2].ToString();
                //worksheet.Cells[1, 11].Value = dt_sum_qty_TLL.Rows[0][3].ToString();


                //foreach (DataRow dataRow in dtexcel.Rows)
                foreach (DataRow dataRow in dt_all.Rows)
                {
                    i++;
                    worksheet.Cells[row, 1].Value = i; // dataRow["id"]; //
                    worksheet.Cells[row, 2].Value = dataRow["Cat"];
                    worksheet.Cells[row, 3].Value = dataRow["Shipmode"];
                    worksheet.Cells[row, 4].Value = dataRow["Consignee"];
                    worksheet.Cells[row, 5].Value = dataRow["Country"]; //
                    worksheet.Cells[row, 6].Value = dataRow["Destination"];
                    worksheet.Cells[row, 7].Value = dataRow["Model"];
                    worksheet.Cells[row, 8].Value = dataRow["Quantity"];
                    //worksheet.Cells[row, 10].Value = dataRow["ATPdate"];
                    if (dataRow["ATPdate"] != DBNull.Value)
                    {
                        DateTime atpDate;
                        if (DateTime.TryParse(dataRow["ATPdate"].ToString(), out atpDate))
                        {
                            worksheet.Cells[row, 9].Value = atpDate;
                            SetDate(worksheet, row, 9, dataRow["ATPdate"]);
                            //worksheet.Cells[row, 9].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet.Cells[row, 9].Value = "";
                        }
                    }
                    else
                    {
                        worksheet.Cells[row, 9].Value = "";
                    }
                    //worksheet.Cells[row, 10].Value = dataRow["Grossweight"];
                    if (dataRow["Grossweight"] != DBNull.Value)
                    {
                        double GWeight;
                        if (double.TryParse(dataRow["Grossweight"].ToString(), out GWeight))
                        {
                            // Làm tròn 3 chữ số thập phân
                            GWeight = Math.Round(GWeight, 3);

                            worksheet.Cells[row, 10].Value = GWeight;
                            worksheet.Cells[row, 10].Style.Numberformat.Format = "0.000"; // Giữ hiển thị 3 chữ số thập phân
                        }
                        else
                        {
                            worksheet.Cells[row, 10].Value = "";
                        }
                    }
                    else
                    {
                        worksheet.Cells[row, 10].Value = "";
                    }

                    if (dataRow["TTLVolume"] != DBNull.Value)
                    {
                        double ttlValue;
                        if (double.TryParse(dataRow["TTLVolume"].ToString(), out ttlValue))
                        {
                            // Làm tròn 3 chữ số thập phân
                            ttlValue = Math.Round(ttlValue, 3);

                            worksheet.Cells[row, 11].Value = ttlValue;
                            worksheet.Cells[row, 11].Style.Numberformat.Format = "0.000"; // Giữ hiển thị 3 chữ số thập phân
                        }
                        else
                        {
                            worksheet.Cells[row, 11].Value = "";
                        }
                    }
                    else
                    {
                        worksheet.Cells[row, 11].Value = "";
                    }
                    //worksheet.Cells[row, 12].Value = dataRow["Exfactorydate"];
                    if (dataRow["Exfactorydate"] != DBNull.Value)
                    {
                        DateTime exFactoryDate;
                        if (DateTime.TryParse(dataRow["Exfactorydate"].ToString(), out exFactoryDate))
                        {
                            worksheet.Cells[row, 12].Value = exFactoryDate;
                            SetDate(worksheet, row, 12, dataRow["Exfactorydate"]);
                            //worksheet.Cells[row, 12].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet.Cells[row, 12].Value = "";
                        }
                    }
                    else
                    {
                        worksheet.Cells[row, 12].Value = "";
                    }

                    //worksheet.Cells[row, 13].Value = dataRow["ETD"];
                    if (dataRow["ETD"] != DBNull.Value)
                    {
                        DateTime ETD;
                        if (DateTime.TryParse(dataRow["ETD"].ToString(), out ETD))
                        {
                            worksheet.Cells[row, 13].Value = ETD;
                            SetDate(worksheet, row, 13, dataRow["ETD"]);
                            //worksheet.Cells[row, 13].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet.Cells[row, 13].Value = "";
                        }
                    }
                    else
                    {
                        worksheet.Cells[row, 13].Value = "";
                    }

                    //worksheet.Cells[row, 14].Value = dataRow["ETA"];
                    if (dataRow["ETA"] != DBNull.Value)
                    {
                        DateTime eta;
                        if (DateTime.TryParse(dataRow["ETA"].ToString(), out eta))
                        {
                            worksheet.Cells[row, 14].Value = eta;
                            SetDate(worksheet, row, 14, dataRow["ETA"]);
                            //worksheet.Cells[row, 14].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet.Cells[row, 14].Value = "";
                        }
                    }
                    else
                    {
                        worksheet.Cells[row, 14].Value = "";
                    }

                    worksheet.Cells[row, 15].Value = dataRow["Cancombine"];
                    worksheet.Cells[row, 16].Value = dataRow["Risky"];

                    //worksheet.Cells[row, 18].Value = dataRow["CreateTime"];
                    if (dataRow["CreateTime"] != DBNull.Value)
                    {
                        DateTime createtime;
                        if (DateTime.TryParse(dataRow["CreateTime"].ToString(), out createtime))
                        {
                            worksheet.Cells[row, 17].Value = createtime;
                            SetDate(worksheet, row, 17, dataRow["CreateTime"]);
                            //worksheet.Cells[row, 17].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet.Cells[row, 17].Value = "";
                        }
                    }
                    else
                    {
                        worksheet.Cells[row, 17].Value = "";
                    }

                    //them cot remark
                    worksheet.Cells[row, 18].Value = dataRow["Remark"];

                    row++;
                }


                //2. //  xuat excel file sheet 2 //========================================

                //tao bang tam de zen vao excel file
                DataTable dt_new = new DataTable();
                dt_new.Columns.Add("id", typeof(Int32));
                dt_new.Columns.Add("Cat", typeof(String));
                dt_new.Columns.Add("Shipmode", typeof(String));
                dt_new.Columns.Add("Consignee", typeof(String));
                dt_new.Columns.Add("Country", typeof(String));
                dt_new.Columns.Add("Destination", typeof(String));
                dt_new.Columns.Add("Model", typeof(String));
                dt_new.Columns.Add("Quantity", typeof(Int32));
                dt_new.Columns.Add("ATPdate", typeof(String));
                dt_new.Columns.Add("TTLcont", typeof(float));
                dt_new.Columns.Add("Exfactorydate", typeof(String));
                dt_new.Columns.Add("ETD", typeof(String));
                dt_new.Columns.Add("ETA", typeof(String));
                dt_new.Columns.Add("Cancombine", typeof(String));
                dt_new.Columns.Add("Risky", typeof(String));


                DataTable dt_all2 = new DataTable();
                //DataTable dt_all_NG2 = new DataTable();
                DataTable dt_group2 = new DataTable();
                //DataTable dt_sum_qty_TLL2 = new DataTable();

                dt_group2 = DataConn.StoreFillDS("Select_Report_Risky_group", System.Data.CommandType.StoredProcedure, tungay, denngay, category, status_ex);
                dt_all2 = DataConn.StoreFillDS("Select_Report_Risky", System.Data.CommandType.StoredProcedure, tungay, denngay, category, status_ex);

                //dt_all_NG = DataConn.StoreFillDS("Select_Report_Risky_NG", System.Data.CommandType.StoredProcedure, tungay, denngay, category, status_ex_risky);

                //dt_sum_qty_TLL2 = DataConn.StoreFillDS("Select_Report_Sum", System.Data.CommandType.StoredProcedure, tungay, denngay, category, status_ex);

                int Sum_qty = 0;
                float Sum_TTL_Volum = 0;

                int sum_qty_tong2 = 0;
                float Sum_TTL_Volum_tong2 = 0;

                for (int k = 0; k < dt_group2.Rows.Count; k++)
                {
                    if (dt_group2.Rows[k]["Cancombine"].ToString() == "OK")
                    {
                        //lay all cate , tinh tong all cac ban ghi  ==> bo dieu kien: && dt_all2.Rows[j]["Exfactorydate"].ToString() == dt_group2.Rows[k]["Exfactorydate"].ToString()
                        for (int j = 0; j < dt_all2.Rows.Count; j++)
                        {
                            // Group theo consignee
                            if (dt_all2.Rows[j]["Consignee"].ToString() == dt_group2.Rows[k]["Consignee"].ToString() &&
                                dt_all2.Rows[j]["Shipmode"].ToString() == dt_group2.Rows[k]["Shipmode"].ToString()
                                && dt_all2.Rows[j]["Destination"].ToString() == dt_group2.Rows[k]["Destination"].ToString()
                                 && dt_all2.Rows[j]["Cancombine"].ToString() == dt_group2.Rows[k]["Cancombine"].ToString())
                            {
                                Sum_qty = Sum_qty + Int32.Parse(dt_all2.Rows[j]["Quantity"].ToString());
                                Sum_TTL_Volum = Sum_TTL_Volum + float.Parse(dt_all2.Rows[j]["TTLcont"].ToString());

                                dt_new.Rows.Add(j, dt_all2.Rows[j]["Cat"].ToString(), dt_all2.Rows[j]["Shipmode"].ToString(), dt_all2.Rows[j]["Consignee"].ToString(), dt_all2.Rows[j]["Country"].ToString(), dt_all2.Rows[j]["Destination"].ToString(), dt_all2.Rows[j]["Model"].ToString(), dt_all2.Rows[j]["Quantity"].ToString(), dt_all2.Rows[j]["ATPdate"].ToString(), dt_all2.Rows[j]["TTLcont"].ToString(), dt_all2.Rows[j]["Exfactorydate"].ToString(), dt_all2.Rows[j]["ETD"].ToString(), dt_all2.Rows[j]["ETA"].ToString(), dt_all2.Rows[j]["Cancombine"].ToString(), dt_all2.Rows[j]["Risky"].ToString());
                            }
                        }
                    }
                    else
                    {
                        //truong hop NG chi lay theo cate  ==> bo dieu kien:  && dt_all2.Rows[j]["Exfactorydate"].ToString() == dt_group2.Rows[k]["Exfactorydate"].ToString()
                        for (int j = 0; j < dt_all2.Rows.Count; j++)
                        {
                            if (dt_all2.Rows[j]["Consignee"].ToString() == dt_group2.Rows[k]["Consignee"].ToString() &&
                                dt_all2.Rows[j]["Shipmode"].ToString() == dt_group2.Rows[k]["Shipmode"].ToString()
                                && dt_all2.Rows[j]["Destination"].ToString() == dt_group2.Rows[k]["Destination"].ToString()
                                && dt_all2.Rows[j]["Cancombine"].ToString() == dt_group2.Rows[k]["Cancombine"].ToString()
                                && dt_all2.Rows[j]["Cat"].ToString() == dt_group2.Rows[k]["Cat"].ToString())                  //hang NG them dieu kien theo Cate chi loc theo cate
                            {
                                Sum_qty = Sum_qty + Int32.Parse(dt_all2.Rows[j]["Quantity"].ToString());
                                Sum_TTL_Volum = Sum_TTL_Volum + float.Parse(dt_all2.Rows[j]["TTLcont"].ToString());

                                dt_new.Rows.Add(j, dt_all2.Rows[j]["Cat"].ToString(), dt_all2.Rows[j]["Shipmode"].ToString(), dt_all2.Rows[j]["Consignee"].ToString(), dt_all2.Rows[j]["Country"].ToString(), dt_all2.Rows[j]["Destination"].ToString(), dt_all2.Rows[j]["Model"].ToString(), dt_all2.Rows[j]["Quantity"].ToString(), dt_all2.Rows[j]["ATPdate"].ToString(), dt_all2.Rows[j]["TTLcont"].ToString(), dt_all2.Rows[j]["Exfactorydate"].ToString(), dt_all2.Rows[j]["ETD"].ToString(), dt_all2.Rows[j]["ETA"].ToString(), dt_all2.Rows[j]["Cancombine"].ToString(), dt_all2.Rows[j]["Risky"].ToString());
                            }
                        }

                    }


                    //tinh tong bao cao risky theo group o day 
                    dt_new.Rows.Add(0, "TTL", "", "", "", "", "", Sum_qty, "", Sum_TTL_Volum, "", "", "", "", "");

                    sum_qty_tong2 = sum_qty_tong2 + Sum_qty;
                    Sum_TTL_Volum_tong2 = Sum_TTL_Volum_tong2 + Sum_TTL_Volum;

                    //reset tong ve 0
                    Sum_qty = 0;
                    Sum_TTL_Volum = 0;
                }

                //var worksheet2 = package.Workbook.Worksheets["Sheet2"];
                ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];
                int row2 = 3;
                int i2 = 0;

                // Ghi ngày vào các ô trong Excel (chú ý rằng chỉ số cột bắt đầu từ 1)
                //worksheet2.Cells[1, 9].Value = Convert.ToDouble(dt_sum_qty_TLL.Rows[0][0]);            //@sum_qty
                worksheet2.Cells[1, 9].Value = Convert.ToDouble(sum_qty_tong2);            //@sum_qty
                worksheet2.Cells[1, 9].Style.Numberformat.Format = "#,##0";
                //worksheet2.Cells[1, 11].Value = Convert.ToDouble(dt_sum_qty_TLL.Rows[0][1]);           //@sum_TLL
                worksheet2.Cells[1, 11].Value = Convert.ToDouble(Sum_TTL_Volum_tong2);           //@sum_TLL
                //worksheet2.Cells[1, 11].Style.Numberformat.Format = "#,##0";
                worksheet2.Cells[1, 11].Style.Numberformat.Format = "#,##0.0000";  // hiển thị 0.3698

                //foreach (DataRow dataRow in dtexcel.Rows)
                foreach (DataRow dataRow2 in dt_new.Rows)
                {
                    i2++;
                    worksheet2.Cells[row2, 2].Value = dataRow2["id"];
                    worksheet2.Cells[row2, 3].Value = dataRow2["Cat"];
                    worksheet2.Cells[row2, 4].Value = dataRow2["Shipmode"];
                    worksheet2.Cells[row2, 5].Value = dataRow2["Consignee"];
                    worksheet2.Cells[row2, 6].Value = dataRow2["Country"]; //
                    worksheet2.Cells[row2, 7].Value = dataRow2["Destination"];
                    worksheet2.Cells[row2, 8].Value = dataRow2["Model"];
                    worksheet2.Cells[row2, 9].Value = dataRow2["Quantity"];
                    //worksheet.Cells[row2, 10].Value = dataRow["ATPdate"];

                    if (dataRow2["ATPdate"] != DBNull.Value)
                    {
                        DateTime atpDate;
                        if (DateTime.TryParse(dataRow2["ATPdate"].ToString(), out atpDate))
                        {
                            worksheet2.Cells[row2, 10].Value = atpDate;
                            SetDate(worksheet2, row2, 10, dataRow2["ATPdate"]);
                            //worksheet2.Cells[row2, 10].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet2.Cells[row2, 10].Value = "";
                        }
                    }
                    else
                    {
                        worksheet2.Cells[row2, 10].Value = "";
                    }

                    //worksheet.Cells[row, 11].Value = dataRow["TTLcont"]; 
                    if (dataRow2["TTLcont"] != DBNull.Value)
                    {
                        double ttlContValue;
                        if (double.TryParse(dataRow2["TTLcont"].ToString(), out ttlContValue))
                        {
                            // Làm tròn 3 chữ số thập phân
                            ttlContValue = Math.Round(ttlContValue, 3);

                            worksheet2.Cells[row2, 11].Value = ttlContValue;
                            worksheet2.Cells[row2, 11].Style.Numberformat.Format = "0.000"; // Giữ hiển thị 3 chữ số thập phân
                        }
                        else
                        {
                            worksheet2.Cells[row2, 11].Value = "";
                        }
                    }
                    else
                    {
                        worksheet2.Cells[row2, 11].Value = "";
                    }

                    //worksheet.Cells[row, 12].Value = dataRow["Exfactorydate"];
                    if (dataRow2["Exfactorydate"] != DBNull.Value)
                    {
                        DateTime exFactoryDate;
                        if (DateTime.TryParse(dataRow2["Exfactorydate"].ToString(), out exFactoryDate))
                        {
                            worksheet2.Cells[row2, 12].Value = exFactoryDate;
                            SetDate(worksheet2, row2, 12, dataRow2["Exfactorydate"]);
                            //worksheet2.Cells[row2, 12].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet2.Cells[row2, 12].Value = "";
                        }
                    }
                    else
                    {
                        worksheet2.Cells[row2, 12].Value = "";
                    }

                    //worksheet.Cells[row, 13].Value = dataRow["ETD"];
                    if (dataRow2["ETD"] != DBNull.Value)
                    {
                        DateTime ETD;
                        if (DateTime.TryParse(dataRow2["ETD"].ToString(), out ETD))
                        {
                            worksheet2.Cells[row2, 13].Value = ETD;
                            SetDate(worksheet2, row2, 13, dataRow2["ETD"]);
                            //worksheet2.Cells[row2, 13].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet2.Cells[row2, 13].Value = "";
                        }
                    }
                    else
                    {
                        worksheet2.Cells[row2, 13].Value = "";
                    }

                    //worksheet.Cells[row, 14].Value = dataRow["ETA"];
                    if (dataRow2["ETA"] != DBNull.Value)
                    {
                        DateTime eta;
                        if (DateTime.TryParse(dataRow2["ETA"].ToString(), out eta))
                        {
                            worksheet2.Cells[row2, 14].Value = eta;
                            SetDate(worksheet2, row2, 14, dataRow2["ETA"]);
                            //worksheet2.Cells[row2, 14].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet2.Cells[row2, 14].Value = "";
                        }
                    }
                    else
                    {
                        worksheet2.Cells[row2, 14].Value = "";
                    }

                    worksheet2.Cells[row2, 15].Value = dataRow2["Cancombine"];
                    worksheet2.Cells[row2, 16].Value = dataRow2["Risky"];

                    if (Convert.ToInt32(dataRow2["id"]) == 0 && dataRow2["Cat"].ToString() == "TTL")
                    {
                        var range = worksheet2.Cells[row2, 2, row2, 16]; // từ cột 2 đến 14 (tùy số cột bạn có)
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue); // hoặc Color.LightBlue, LightGray...
                        range.Style.Font.Bold = true; // In đậm dòng tổng
                    }

                    row2++;
                }


                //================ 3. //  xuat excel file sheet 3  // TTL Summary tong //=====================
                DataTable dt_group3 = new DataTable();

                DataTable dt_new3 = new DataTable();
                dt_new3.Columns.Add("id", typeof(Int32));
                dt_new3.Columns.Add("Consignee", typeof(String));
                dt_new3.Columns.Add("Country", typeof(String));
                dt_new3.Columns.Add("Shipmode", typeof(String));
                dt_new3.Columns.Add("Destination", typeof(String));
                dt_new3.Columns.Add("Cat", typeof(String));
                dt_new3.Columns.Add("Quantity", typeof(Int32));
                dt_new3.Columns.Add("ATPdate", typeof(String));
                dt_new3.Columns.Add("Grossweight", typeof(String));
                dt_new3.Columns.Add("TTLVolume", typeof(float));
                dt_new3.Columns.Add("Exfactorydate", typeof(String));
                dt_new3.Columns.Add("ETD", typeof(String));
                dt_new3.Columns.Add("ETA", typeof(String));
                dt_new3.Columns.Add("Cancombine", typeof(String));

                // truong hop nay group bo Category
                DataTable dt_all_sumary = new DataTable();
                dt_group3 = DataConn.StoreFillDS("Select_Report_TTL_group", System.Data.CommandType.StoredProcedure, tungay, denngay, category, status_ex);
                dt_all_sumary = DataConn.StoreFillDS("Select_Report_Risky2", System.Data.CommandType.StoredProcedure, tungay, denngay, category, status_ex);

                int Sum_qty3 = 0;
                float Sum_TTL_GrossWeight = 0;
                float Sum_TTL_Volume = 0;

                int Sum_qty3_tong = 0;
                float Sum_TTL_GrossWeight_tong = 0;
                float Sum_TTL_Volume_tong = 0;


                //for (int k = 0; k < dt_group3.Rows.Count; k++)
                //{
                //    if (dt_group3.Rows[k]["Cancombine"].ToString() == "OK")
                //    {
                //        //lay all cate, tinh tong all cac ban ghi    --->Consignee va ETD date (khong phai exfactory date)
                //        for (int j = 0; j < dt_all_sumary.Rows.Count; j++)
                //        {
                //            if (dt_all_sumary.Rows[j]["Consignee"].ToString() == dt_group3.Rows[k]["Consignee"].ToString()
                //                && dt_all_sumary.Rows[j]["Shipmode"].ToString() == dt_group3.Rows[k]["Shipmode"].ToString()
                //                && dt_all_sumary.Rows[j]["Destination"].ToString() == dt_group3.Rows[k]["Destination"].ToString()
                //                && dt_all_sumary.Rows[j]["ETD"].ToString() == dt_group3.Rows[k]["ETD"].ToString()
                //                && dt_all_sumary.Rows[j]["Cancombine"].ToString() == dt_group3.Rows[k]["Cancombine"].ToString())
                //            {
                //                Sum_qty3 = Sum_qty3 + Int32.Parse(dt_all_sumary.Rows[j]["Quantity"].ToString());
                //                Sum_TTL_GrossWeight = Sum_TTL_GrossWeight + float.Parse(dt_all_sumary.Rows[j]["Grossweight"].ToString());
                //                Sum_TTL_Volume = Sum_TTL_Volume + float.Parse(dt_all_sumary.Rows[j]["TTLVolume"].ToString());

                //                dt_new3.Rows.Add(j, dt_all_sumary.Rows[j]["Consignee"].ToString(), dt_all_sumary.Rows[j]["Country"].ToString(), dt_all_sumary.Rows[j]["Shipmode"].ToString(),
                //                    dt_all_sumary.Rows[j]["Destination"].ToString(), dt_all_sumary.Rows[j]["Cat"].ToString(), dt_all_sumary.Rows[j]["Quantity"].ToString(),
                //                    dt_all_sumary.Rows[j]["ATPdate"].ToString(), dt_all_sumary.Rows[j]["Grossweight"].ToString(), dt_all_sumary.Rows[j]["TTLVolume"].ToString(),
                //                    dt_all_sumary.Rows[j]["Exfactorydate"].ToString(), dt_all_sumary.Rows[j]["ETD"].ToString(), dt_all_sumary.Rows[j]["ETA"].ToString(),
                //                    dt_all_sumary.Rows[j]["Cancombine"].ToString());
                //            }
                //        }
                //    }
                //    else
                //    {
                //        //truong hop NG chi lay theo cate  ==> truong hop nay group bo Category *** tam thoi cu cho cate vao de test voi user ******
                //        for (int j = 0; j < dt_all_sumary.Rows.Count; j++)
                //        {
                //            if (dt_all_sumary.Rows[j]["Consignee"].ToString() == dt_group3.Rows[k]["Consignee"].ToString()
                //                && dt_all_sumary.Rows[j]["Shipmode"].ToString() == dt_group3.Rows[k]["Shipmode"].ToString()
                //                && dt_all_sumary.Rows[j]["Destination"].ToString() == dt_group3.Rows[k]["Destination"].ToString()
                //                && dt_all_sumary.Rows[j]["ETD"].ToString() == dt_group3.Rows[k]["ETD"].ToString()
                //                && dt_all_sumary.Rows[j]["Cancombine"].ToString() == dt_group3.Rows[k]["Cancombine"].ToString()
                //                && dt_all_sumary.Rows[j]["Cat"].ToString() == dt_group3.Rows[k]["Cat"].ToString())                  //hang NG them dieu kien theo Cate chi loc theo cate
                //            {
                //                Sum_qty3 = Sum_qty3 + Int32.Parse(dt_all_sumary.Rows[j]["Quantity"].ToString());
                //                Sum_TTL_GrossWeight = Sum_TTL_GrossWeight + float.Parse(dt_all_sumary.Rows[j]["Grossweight"].ToString());
                //                Sum_TTL_Volume = Sum_TTL_Volume + float.Parse(dt_all_sumary.Rows[j]["TTLVolume"].ToString());

                //                dt_new3.Rows.Add(j, dt_all_sumary.Rows[j]["Consignee"].ToString(), dt_all_sumary.Rows[j]["Country"].ToString(), dt_all_sumary.Rows[j]["Shipmode"].ToString(),
                //                    dt_all_sumary.Rows[j]["Destination"].ToString(), dt_all_sumary.Rows[j]["Cat"].ToString(), dt_all_sumary.Rows[j]["Quantity"].ToString(),
                //                    dt_all_sumary.Rows[j]["ATPdate"].ToString(), dt_all_sumary.Rows[j]["Grossweight"].ToString(), dt_all_sumary.Rows[j]["TTLVolume"].ToString(),
                //                    dt_all_sumary.Rows[j]["Exfactorydate"].ToString(), dt_all_sumary.Rows[j]["ETD"].ToString(), dt_all_sumary.Rows[j]["ETA"].ToString(),
                //                    dt_all_sumary.Rows[j]["Cancombine"].ToString());
                //            }
                //        }

                //    }
                //    //tinh tong bao cao risky theo group o day 
                //    if (Sum_qty3 > 0)
                //    {
                //        dt_new3.Rows.Add(0, "TTL", "", "", "", "", Sum_qty3, "", Sum_TTL_GrossWeight, Sum_TTL_Volume, "", "", "", "");
                //    }
                //    //dt_new3.Rows.Add(0, "TTL", "", "", "", "", Sum_qty3, "", Sum_TTL_GrossWeight, Sum_TTL_Volume, "", "", "", "");

                //    Sum_qty3_tong = Sum_qty3_tong + Sum_qty3;
                //    Sum_TTL_GrossWeight_tong = Sum_TTL_GrossWeight_tong + Sum_TTL_GrossWeight;
                //    Sum_TTL_Volume_tong = Sum_TTL_Volume_tong + Sum_TTL_Volume;

                //    //reset tong ve 0
                //    Sum_qty3 = 0;
                //    Sum_TTL_GrossWeight = 0;
                //    Sum_TTL_Volume = 0;
                //}

                var source = dt_all_sumary.AsEnumerable();
                //var grouped = source.GroupBy(r => r.Field<DateTime>("ETD").Date);

                //var grouped = source.GroupBy(r => new
                //{
                //    ETD = r.Field<DateTime>("ETD").Date,   // group cùng ngày
                //    Consignee = r.Field<string>("Consignee"),
                //    Shipmode = r.Field<string>("Shipmode"),
                //    Destination = r.Field<string>("Destination")
                //});

                //tranh loi du lieu null
                var grouped = source.GroupBy(r => new
                {
                    ETD = (r.Field<DateTime?>("ETD") ?? DateTime.MinValue).Date,
                    Consignee = r.Field<string>("Consignee") ?? "",
                    Shipmode = r.Field<string>("Shipmode") ?? "",
                    Destination = r.Field<string>("Destination") ?? ""
                });

                DataTable dt_groupByETD = dt_new3.Clone(); // giữ cấu trúc cột

                foreach (var g in grouped)
                {
                    int sumQty = g.Sum(r => r.Field<int>("Quantity"));
                    float sumGross = g.Sum(r => float.Parse(r["Grossweight"].ToString()));
                    float sumVolume = g.Sum(r => float.Parse(r["TTLVolume"].ToString()));

                    var first = g.First(); // lấy thông tin đại diện

                    dt_groupByETD.Rows.Add(
                        0,
                        first["Consignee"],
                        first["Country"],
                        first["Shipmode"],
                        first["Destination"],
                        first["Cat"],
                        sumQty,
                        first["ATPdate"],
                        sumGross,
                        sumVolume,
                        first["Exfactorydate"],
                        g.Key.ETD,      // ETD đã group
                        first["ETA"],
                        first["Cancombine"]
                    );
                }

                ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                int row3 = 3;
                int i3 = 0;
                // Ghi ngày vào các ô trong Excel (chú ý rằng chỉ số cột bắt đầu từ 1)                
                //worksheet3.Cells[1, 8].Value = Convert.ToDouble(Sum_qty3_tong);                   //==> khong tinh tong vi da group tren theo vermoi
                worksheet3.Cells[1, 8].Style.Numberformat.Format = "#,##0";
                
                //worksheet3.Cells[1, 10].Value = Convert.ToDouble(Sum_TTL_GrossWeight_tong);  //@sum_grossweight       //==> khong tinh tong vi da group tren theo vermoi
                worksheet3.Cells[1, 10].Style.Numberformat.Format = "#,##0";
                
                //worksheet3.Cells[1, 11].Value = Convert.ToDouble(Sum_TTL_Volume_tong);  //@sum_volume             //==> khong tinh tong vi da group tren theo vermoi
                worksheet3.Cells[1, 11].Style.Numberformat.Format = "#,##0";

                //foreach (DataRow dataRow3 in dt_new3.Rows)
                foreach (DataRow dataRow3 in dt_groupByETD.Rows)
                {
                    i3++;
                    worksheet3.Cells[row3, 2].Value = dataRow3["id"];
                    worksheet3.Cells[row3, 3].Value = dataRow3["Consignee"];
                    worksheet3.Cells[row3, 4].Value = dataRow3["Country"];
                    worksheet3.Cells[row3, 5].Value = dataRow3["Shipmode"];
                    worksheet3.Cells[row3, 6].Value = dataRow3["Destination"];
                    worksheet3.Cells[row3, 7].Value = dataRow3["Cat"];
                    worksheet3.Cells[row3, 8].Value = dataRow3["Quantity"];
                    if (dataRow3["ATPdate"] != DBNull.Value)
                    {
                        DateTime atpDate;
                        if (DateTime.TryParse(dataRow3["ATPdate"].ToString(), out atpDate))
                        {
                            worksheet3.Cells[row3, 9].Value = atpDate;
                            SetDate(worksheet3, row3, 9, dataRow3["ATPdate"]);
                            //worksheet3.Cells[row3, 9].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet3.Cells[row3, 9].Value = "";
                        }
                    }
                    else
                    {
                        worksheet3.Cells[row3, 9].Value = "";
                    }
                    //worksheet3.Cells[row3, 10].Value = dataRow["Grossweight"];
                    if (dataRow3["Grossweight"] != DBNull.Value)
                    {
                        double GrossweightValue;
                        if (double.TryParse(dataRow3["Grossweight"].ToString(), out GrossweightValue))
                        {
                            // Làm tròn 3 chữ số thập phân
                            GrossweightValue = Math.Round(GrossweightValue, 3);

                            worksheet3.Cells[row3, 10].Value = GrossweightValue;
                            worksheet3.Cells[row3, 10].Style.Numberformat.Format = "0.000"; // Giữ hiển thị 3 chữ số thập phân
                        }
                        else
                        {
                            worksheet3.Cells[row3, 10].Value = "";
                        }
                    }
                    else
                    {
                        worksheet3.Cells[row3, 10].Value = "";
                    }

                    //worksheet3.Cells[row3, 11].Value = dataRow["TTLVolume"];
                    if (dataRow3["TTLVolume"] != DBNull.Value)
                    {
                        double TTLVolumeValue;
                        if (double.TryParse(dataRow3["TTLVolume"].ToString(), out TTLVolumeValue))
                        {
                            // Làm tròn 3 chữ số thập phân
                            TTLVolumeValue = Math.Round(TTLVolumeValue, 3);

                            worksheet3.Cells[row3, 11].Value = TTLVolumeValue;
                            worksheet3.Cells[row3, 11].Style.Numberformat.Format = "0.000"; // Giữ hiển thị 3 chữ số thập phân
                        }
                        else
                        {
                            worksheet3.Cells[row3, 11].Value = "";
                        }
                    }
                    else
                    {
                        worksheet3.Cells[row3, 11].Value = "";
                    }

                    if (dataRow3["Exfactorydate"] != DBNull.Value)
                    {
                        DateTime exFactoryDate;
                        if (DateTime.TryParse(dataRow3["Exfactorydate"].ToString(), out exFactoryDate))
                        {
                            worksheet3.Cells[row3, 12].Value = exFactoryDate;
                            SetDate(worksheet3, row3, 12, dataRow3["Exfactorydate"]);
                            //worksheet2.Cells[row2, 12].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet3.Cells[row3, 12].Value = "";
                        }
                    }
                    else
                    {
                        worksheet3.Cells[row3, 12].Value = "";
                    }

                    if (dataRow3["ETD"] != DBNull.Value)
                    {
                        DateTime ETD;
                        if (DateTime.TryParse(dataRow3["ETD"].ToString(), out ETD))
                        {
                            worksheet3.Cells[row3, 13].Value = ETD;
                            SetDate(worksheet3, row3, 13, dataRow3["ETD"]);
                            //worksheet3.Cells[row3, 13].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet3.Cells[row3, 13].Value = "";
                        }
                    }
                    else
                    {
                        worksheet3.Cells[row3, 13].Value = "";
                    }
                    if (dataRow3["ETA"] != DBNull.Value)
                    {
                        DateTime eta;
                        if (DateTime.TryParse(dataRow3["ETA"].ToString(), out eta))
                        {
                            worksheet3.Cells[row3, 14].Value = eta;
                            SetDate(worksheet3, row3, 14, dataRow3["ETA"]);
                            //worksheet3.Cells[row3, 14].Style.Numberformat.Format = "m/d/yyyy";
                            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
                        }
                        else
                        {
                            worksheet3.Cells[row3, 14].Value = "";
                        }
                    }
                    else
                    {
                        worksheet3.Cells[row3, 14].Value = "";
                    }
                    worksheet3.Cells[row3, 15].Value = dataRow3["Cancombine"];


                    //khong can boi mau vi => group het lam 1****  //&& dataRow3["Consignee"].ToString() == "TTL"
                    if (Convert.ToInt32(dataRow3["id"]) == 0 )
                    {
                        var range = worksheet3.Cells[row3, 2, row3, 15]; // từ cột 2 đến 14 (tùy số cột bạn có)
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue); // hoặc Color.LightBlue, LightGray...
                        //range.Style.Font.Bold = true; // In đậm dòng tổng
                    }

                    row3++;
                }

                // Lưu vào file mới
                package.SaveAs(newFileInfo);
            }

        }

        static void ProcessExcelFile3(string filePath, string newFilePath, string tungay, string denngay, string category, string status_ex) 
        {
            ////================ 3. //  xuat excel file sheet 3  // TTL Summary tong //=====================
            //DataTable dt_group3 = new DataTable();

            //DataTable dt_new3 = new DataTable();
            //dt_new3.Columns.Add("id", typeof(Int32));
            //dt_new3.Columns.Add("Consignee", typeof(String));
            //dt_new3.Columns.Add("Country", typeof(String));
            //dt_new3.Columns.Add("Shipmode", typeof(String));
            //dt_new3.Columns.Add("Destination", typeof(String));
            //dt_new3.Columns.Add("Cat", typeof(String));
            //dt_new3.Columns.Add("Quantity", typeof(Int32));
            //dt_new3.Columns.Add("ATPdate", typeof(String));
            //dt_new3.Columns.Add("Grossweight", typeof(String));
            //dt_new3.Columns.Add("TTLVolume", typeof(float));
            //dt_new3.Columns.Add("Exfactorydate", typeof(String));
            //dt_new3.Columns.Add("ETD", typeof(String));
            //dt_new3.Columns.Add("ETA", typeof(String));
            //dt_new3.Columns.Add("Cancombine", typeof(String));

            //// truong hop nay group bo Category
            //DataTable dt_all_sumary = new DataTable();
            //dt_group3 = DataConn.StoreFillDS("Select_Report_TTL_group", System.Data.CommandType.StoredProcedure, tungay, denngay, category, status_ex);
            //dt_all_sumary = DataConn.StoreFillDS("Select_Report_Risky2", System.Data.CommandType.StoredProcedure, tungay, denngay, category, status_ex);

            //int Sum_qty3 = 0;
            //float Sum_TTL_GrossWeight = 0;
            //float Sum_TTL_Volume = 0;

            //int Sum_qty3_tong = 0;
            //float Sum_TTL_GrossWeight_tong = 0;
            //float Sum_TTL_Volume_tong = 0;


            //for (int k = 0; k < dt_group3.Rows.Count; k++)
            //{
            //    if (dt_group3.Rows[k]["Cancombine"].ToString() == "OK")
            //    {
            //        //lay all cate, tinh tong all cac ban ghi    --->Consignee va ETD date (khong phai exfactory date)
            //        for (int j = 0; j < dt_all_sumary.Rows.Count; j++)
            //        {
            //            if (dt_all_sumary.Rows[j]["Consignee"].ToString() == dt_group3.Rows[k]["Consignee"].ToString()
            //                && dt_all_sumary.Rows[j]["Shipmode"].ToString() == dt_group3.Rows[k]["Shipmode"].ToString()
            //                && dt_all_sumary.Rows[j]["Destination"].ToString() == dt_group3.Rows[k]["Destination"].ToString()
            //                && dt_all_sumary.Rows[j]["ETD"].ToString() == dt_group3.Rows[k]["ETD"].ToString()
            //                && dt_all_sumary.Rows[j]["Cancombine"].ToString() == dt_group3.Rows[k]["Cancombine"].ToString())
            //            {
            //                Sum_qty3 = Sum_qty3 + Int32.Parse(dt_all_sumary.Rows[j]["Quantity"].ToString());
            //                Sum_TTL_GrossWeight = Sum_TTL_GrossWeight + float.Parse(dt_all_sumary.Rows[j]["Grossweight"].ToString());
            //                Sum_TTL_Volume = Sum_TTL_Volume + float.Parse(dt_all_sumary.Rows[j]["TTLVolume"].ToString());

            //                dt_new3.Rows.Add(j, dt_all_sumary.Rows[j]["Consignee"].ToString(), dt_all_sumary.Rows[j]["Country"].ToString(), dt_all_sumary.Rows[j]["Shipmode"].ToString(),
            //                    dt_all_sumary.Rows[j]["Destination"].ToString(), dt_all_sumary.Rows[j]["Cat"].ToString(), dt_all_sumary.Rows[j]["Quantity"].ToString(),
            //                    dt_all_sumary.Rows[j]["ATPdate"].ToString(), dt_all_sumary.Rows[j]["Grossweight"].ToString(), dt_all_sumary.Rows[j]["TTLVolume"].ToString(),
            //                    dt_all_sumary.Rows[j]["Exfactorydate"].ToString(), dt_all_sumary.Rows[j]["ETD"].ToString(), dt_all_sumary.Rows[j]["ETA"].ToString(),
            //                    dt_all_sumary.Rows[j]["Cancombine"].ToString());
            //            }
            //        }
            //    }
            //    else
            //    {
            //        //truong hop NG chi lay theo cate  ==> truong hop nay group bo Category *** tam thoi cu cho cate vao de test voi user ******
            //        for (int j = 0; j < dt_all_sumary.Rows.Count; j++)
            //        {
            //            if (dt_all_sumary.Rows[j]["Consignee"].ToString() == dt_group3.Rows[k]["Consignee"].ToString()
            //                && dt_all_sumary.Rows[j]["Shipmode"].ToString() == dt_group3.Rows[k]["Shipmode"].ToString()
            //                && dt_all_sumary.Rows[j]["Destination"].ToString() == dt_group3.Rows[k]["Destination"].ToString()
            //                && dt_all_sumary.Rows[j]["ETD"].ToString() == dt_group3.Rows[k]["ETD"].ToString()
            //                && dt_all_sumary.Rows[j]["Cancombine"].ToString() == dt_group3.Rows[k]["Cancombine"].ToString()
            //                && dt_all_sumary.Rows[j]["Cat"].ToString() == dt_group3.Rows[k]["Cat"].ToString())                  //hang NG them dieu kien theo Cate chi loc theo cate
            //            {
            //                Sum_qty3 = Sum_qty3 + Int32.Parse(dt_all_sumary.Rows[j]["Quantity"].ToString());
            //                Sum_TTL_GrossWeight = Sum_TTL_GrossWeight + float.Parse(dt_all_sumary.Rows[j]["Grossweight"].ToString());
            //                Sum_TTL_Volume = Sum_TTL_Volume + float.Parse(dt_all_sumary.Rows[j]["TTLVolume"].ToString());

            //                dt_new3.Rows.Add(j, dt_all_sumary.Rows[j]["Consignee"].ToString(), dt_all_sumary.Rows[j]["Country"].ToString(), dt_all_sumary.Rows[j]["Shipmode"].ToString(),
            //                    dt_all_sumary.Rows[j]["Destination"].ToString(), dt_all_sumary.Rows[j]["Cat"].ToString(), dt_all_sumary.Rows[j]["Quantity"].ToString(),
            //                    dt_all_sumary.Rows[j]["ATPdate"].ToString(), dt_all_sumary.Rows[j]["Grossweight"].ToString(), dt_all_sumary.Rows[j]["TTLVolume"].ToString(),
            //                    dt_all_sumary.Rows[j]["Exfactorydate"].ToString(), dt_all_sumary.Rows[j]["ETD"].ToString(), dt_all_sumary.Rows[j]["ETA"].ToString(),
            //                    dt_all_sumary.Rows[j]["Cancombine"].ToString());
            //            }
            //        }

            //    }
            //    //tinh tong bao cao risky theo group o day 
            //    if (Sum_qty3 > 0)
            //    {
            //        dt_new3.Rows.Add(0, "TTL", "", "", "", "", Sum_qty3, "", Sum_TTL_GrossWeight, Sum_TTL_Volume, "", "", "", "");
            //    }
            //    //dt_new3.Rows.Add(0, "TTL", "", "", "", "", Sum_qty3, "", Sum_TTL_GrossWeight, Sum_TTL_Volume, "", "", "", "");

            //    Sum_qty3_tong = Sum_qty3_tong + Sum_qty3;
            //    Sum_TTL_GrossWeight_tong = Sum_TTL_GrossWeight_tong + Sum_TTL_GrossWeight;
            //    Sum_TTL_Volume_tong = Sum_TTL_Volume_tong + Sum_TTL_Volume;

            //    //reset tong ve 0
            //    Sum_qty3 = 0;
            //    Sum_TTL_GrossWeight = 0;
            //    Sum_TTL_Volume = 0;
            //}

            //ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
            //int row3 = 3;
            //int i3 = 0;
            //// Ghi ngày vào các ô trong Excel (chú ý rằng chỉ số cột bắt đầu từ 1)
            ////worksheet3.Cells[1, 8].Value = Convert.ToDouble(dt_sum_qty_TLL.Rows[0][0]);
            //worksheet3.Cells[1, 8].Value = Convert.ToDouble(Sum_qty3_tong);
            //worksheet3.Cells[1, 8].Style.Numberformat.Format = "#,##0";

            ////worksheet3.Cells[1, 10].Value = Convert.ToDouble(Sum_qty3_tong);  //@sum_grossweight
            //worksheet3.Cells[1, 10].Value = Convert.ToDouble(Sum_TTL_GrossWeight_tong);  //@sum_grossweight
            //worksheet3.Cells[1, 10].Style.Numberformat.Format = "#,##0";

            ////worksheet3.Cells[1, 11].Value = Convert.ToDouble(dt_sum_qty_TLL.Rows[0][3]);  //@sum_volume
            //worksheet3.Cells[1, 11].Value = Convert.ToDouble(Sum_TTL_Volume_tong);  //@sum_volume
            //worksheet3.Cells[1, 11].Style.Numberformat.Format = "#,##0";

            //foreach (DataRow dataRow3 in dt_new3.Rows)
            //{
            //    i3++;
            //    worksheet3.Cells[row3, 2].Value = dataRow3["id"];
            //    worksheet3.Cells[row3, 3].Value = dataRow3["Consignee"];
            //    worksheet3.Cells[row3, 4].Value = dataRow3["Country"];
            //    worksheet3.Cells[row3, 5].Value = dataRow3["Shipmode"];
            //    worksheet3.Cells[row3, 6].Value = dataRow3["Destination"];
            //    worksheet3.Cells[row3, 7].Value = dataRow3["Cat"];
            //    worksheet3.Cells[row3, 8].Value = dataRow3["Quantity"];
            //    if (dataRow3["ATPdate"] != DBNull.Value)
            //    {
            //        DateTime atpDate;
            //        if (DateTime.TryParse(dataRow3["ATPdate"].ToString(), out atpDate))
            //        {
            //            worksheet3.Cells[row3, 9].Value = atpDate;
            //            SetDate(worksheet3, row3, 9, dataRow3["ATPdate"]);
            //            //worksheet3.Cells[row3, 9].Style.Numberformat.Format = "m/d/yyyy";
            //            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
            //        }
            //        else
            //        {
            //            worksheet3.Cells[row3, 9].Value = "";
            //        }
            //    }
            //    else
            //    {
            //        worksheet3.Cells[row3, 9].Value = "";
            //    }
            //    //worksheet3.Cells[row3, 10].Value = dataRow["Grossweight"];
            //    if (dataRow3["Grossweight"] != DBNull.Value)
            //    {
            //        double GrossweightValue;
            //        if (double.TryParse(dataRow3["Grossweight"].ToString(), out GrossweightValue))
            //        {
            //            // Làm tròn 3 chữ số thập phân
            //            GrossweightValue = Math.Round(GrossweightValue, 3);

            //            worksheet3.Cells[row3, 10].Value = GrossweightValue;
            //            worksheet3.Cells[row3, 10].Style.Numberformat.Format = "0.000"; // Giữ hiển thị 3 chữ số thập phân
            //        }
            //        else
            //        {
            //            worksheet3.Cells[row3, 10].Value = "";
            //        }
            //    }
            //    else
            //    {
            //        worksheet3.Cells[row3, 10].Value = "";
            //    }

            //    //worksheet3.Cells[row3, 11].Value = dataRow["TTLVolume"];
            //    if (dataRow3["TTLVolume"] != DBNull.Value)
            //    {
            //        double TTLVolumeValue;
            //        if (double.TryParse(dataRow3["TTLVolume"].ToString(), out TTLVolumeValue))
            //        {
            //            // Làm tròn 3 chữ số thập phân
            //            TTLVolumeValue = Math.Round(TTLVolumeValue, 3);

            //            worksheet3.Cells[row3, 11].Value = TTLVolumeValue;
            //            worksheet3.Cells[row3, 11].Style.Numberformat.Format = "0.000"; // Giữ hiển thị 3 chữ số thập phân
            //        }
            //        else
            //        {
            //            worksheet3.Cells[row3, 11].Value = "";
            //        }
            //    }
            //    else
            //    {
            //        worksheet3.Cells[row3, 11].Value = "";
            //    }

            //    if (dataRow3["Exfactorydate"] != DBNull.Value)
            //    {
            //        DateTime exFactoryDate;
            //        if (DateTime.TryParse(dataRow3["Exfactorydate"].ToString(), out exFactoryDate))
            //        {
            //            worksheet3.Cells[row3, 12].Value = exFactoryDate;
            //            SetDate(worksheet3, row3, 12, dataRow3["Exfactorydate"]);
            //            //worksheet2.Cells[row2, 12].Style.Numberformat.Format = "m/d/yyyy";
            //            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
            //        }
            //        else
            //        {
            //            worksheet3.Cells[row3, 12].Value = "";
            //        }
            //    }
            //    else
            //    {
            //        worksheet3.Cells[row3, 12].Value = "";
            //    }

            //    if (dataRow3["ETD"] != DBNull.Value)
            //    {
            //        DateTime ETD;
            //        if (DateTime.TryParse(dataRow3["ETD"].ToString(), out ETD))
            //        {
            //            worksheet3.Cells[row3, 13].Value = ETD;
            //            SetDate(worksheet3, row3, 13, dataRow3["ETD"]);
            //            //worksheet3.Cells[row3, 13].Style.Numberformat.Format = "m/d/yyyy";
            //            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
            //        }
            //        else
            //        {
            //            worksheet3.Cells[row3, 13].Value = "";
            //        }
            //    }
            //    else
            //    {
            //        worksheet3.Cells[row3, 13].Value = "";
            //    }
            //    if (dataRow3["ETA"] != DBNull.Value)
            //    {
            //        DateTime eta;
            //        if (DateTime.TryParse(dataRow3["ETA"].ToString(), out eta))
            //        {
            //            worksheet3.Cells[row3, 14].Value = eta;
            //            SetDate(worksheet3, row3, 14, dataRow3["ETA"]);
            //            //worksheet3.Cells[row3, 14].Style.Numberformat.Format = "m/d/yyyy";
            //            // hoặc "dd/MM/yyyy" nếu bạn muốn định dạng kiểu Việt Nam
            //        }
            //        else
            //        {
            //            worksheet3.Cells[row3, 14].Value = "";
            //        }
            //    }
            //    else
            //    {
            //        worksheet3.Cells[row3, 14].Value = "";
            //    }
            //    worksheet3.Cells[row3, 15].Value = dataRow3["Cancombine"];

            //    if (Convert.ToInt32(dataRow3["id"]) == 0 && dataRow3["Consignee"].ToString() == "TTL")
            //    {
            //        var range = worksheet3.Cells[row3, 2, row3, 15]; // từ cột 2 đến 14 (tùy số cột bạn có)
            //        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            //        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue); // hoặc Color.LightBlue, LightGray...
            //        range.Style.Font.Bold = true; // In đậm dòng tổng
            //    }

            //    row3++;
            //}
        }
        private void DownloadFile(string filePath, string fileName)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists)
            {
                // Đặt các header cho tải xuống
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());

                // Ghi nội dung file vào response
                Response.WriteFile(fileInfo.FullName);
                Response.End();
            }
            else
            {
                Response.Write("File không tồn tại.");
            }
        }

        //so sanh tuong quan giua 2 ngay trong tuan
        public static bool IsDifferentWeek(DayOfWeek? day1, DayOfWeek? day1b)
        {
            return (int)day1 >= (int)day1b;
        }
        //Chuyển Sunday (0) thành 7 tạm thời khi so sánh
        public static int NormalizeDayOfWeek(DayOfWeek? day)
        {
            return day == DayOfWeek.Sunday ? 7 : (int)day;
        }
        public static bool IsDifferentWeek2(DayOfWeek? day1, DayOfWeek? day2)
        {
            return NormalizeDayOfWeek((DayOfWeek?)(int)day1) > NormalizeDayOfWeek((DayOfWeek?)(int)day2);
        }
        //ham check 2 ngay co cung tuan khong
        public static bool IsSameWeek(DateTime date1, DateTime date2)
        {
            int diff1 = (7 + (int)date1.DayOfWeek - 1) % 7; // Thứ Hai = 0
            int diff2 = (7 + (int)date2.DayOfWeek - 1) % 7;

            DateTime startOfWeek1 = date1.AddDays(-diff1).Date;
            DateTime startOfWeek2 = date2.AddDays(-diff2).Date;

            return startOfWeek1 == startOfWeek2;
        }

        //Hàm lấy ra ngày mong muốn trong tuần
        private DateTime GetSpecificDayInWeek(DateTime anyDateInWeek, DayOfWeek targetDay)
        {
            int diff = (7 + (targetDay - DayOfWeek.Monday)) % 7;
            DateTime monday = anyDateInWeek.AddDays(-(7 + (anyDateInWeek.DayOfWeek - DayOfWeek.Monday)) % 7);
            return monday.AddDays(diff);
        }
        //ham lay nay lui sang thang truoc neu la tuan dau cua thang
        public static DateTime GetSpecificDayInWeek_back(DateTime referenceDate, DayOfWeek targetDay, bool allowPreviousMonth = true)
        {
            // Tìm ngày đầu tuần chứa referenceDate (tuần bắt đầu từ Monday)
            int diffToMonday = ((int)referenceDate.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime mondayOfWeek = referenceDate.AddDays(-diffToMonday);

            // Nếu tuần hiện tại là tuần đầu tháng và cần dùng ngày tháng trước
            if (allowPreviousMonth && mondayOfWeek.Month != referenceDate.Month)
            {
                // Lùi về tuần trước
                mondayOfWeek = mondayOfWeek.AddDays(-7);
            }

            // Tính ngày có targetDay trong tuần
            int daysOffset = ((int)targetDay - (int)DayOfWeek.Monday + 7) % 7;
            return mondayOfWeek.AddDays(daysOffset);
        }

        //ham lay ngay truoc 1 tuan => truong hop so sanh tuong tac giua 2 ngay ex-factory va ETD
        private DateTime GetSpecificDayInPreviousWeek(DateTime anyDateInWeek, DayOfWeek targetDay)
        {
            // Chuyển về tuần trước
            anyDateInWeek = anyDateInWeek.AddDays(-7);

            // Tính thứ Hai của tuần trước
            int diffToMonday = ((int)anyDateInWeek.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday = anyDateInWeek.AddDays(-diffToMonday);

            // Tính ngày targetDay trong tuần đó
            int diffToTarget = ((int)targetDay - (int)DayOfWeek.Monday + 7) % 7;
            return monday.AddDays(diffToTarget);
        }

        //ham lay tuan dau cua thang
        public static bool IsFirstWeekOfMonth(DateTime date)
        {
            // Lấy ngày đầu tiên của tháng
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            // Xác định thứ mấy là ngày đầu tháng
            int diffToMonday = ((int)firstDayOfMonth.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;

            // Lấy thứ Hai đầu tuần chứa ngày đầu tháng
            DateTime startOfFirstWeek = firstDayOfMonth.AddDays(-diffToMonday);

            // Lấy Chủ Nhật của tuần đó (nếu cần, không dùng trong so sánh)
            DateTime endOfFirstWeek = startOfFirstWeek.AddDays(6);

            // Nếu ngày truyền vào nằm trong tuần chứa ngày đầu tháng
            return date >= startOfFirstWeek && date <= endOfFirstWeek;
        }

        protected void ImportFromExcel(object sender, EventArgs e)
        {
            DataTable dtcheck = new DataTable();
            if (dr_filter_keyconvert.SelectedValue.ToString() != "==TemplateName==" && dr_filter_keyconvert.SelectedValue.ToString() != "")
            {
                //truong hop upload bang mau template //DPOversea
                int dongloi = 0;

                try
                {
                    DataTable dt_checkupload = new DataTable();
                    DataTable dt_new = new DataTable();
                    int countlap = 0;

                    dt_new.Columns.Add("ID", typeof(Int32));
                    dt_new.Columns.Add("Sheet", typeof(String));
                    dt_new.Columns.Add("Cat", typeof(String));
                    dt_new.Columns.Add("Shipmode", typeof(String));
                    dt_new.Columns.Add("Consignee", typeof(String));
                    dt_new.Columns.Add("Country", typeof(String));
                    dt_new.Columns.Add("Destination", typeof(String));
                    dt_new.Columns.Add("Model", typeof(String));
                    dt_new.Columns.Add("Quantity", typeof(int));
                    dt_new.Columns.Add("ATPdate", typeof(DateTime));
                    dt_new.Columns.Add("Volume", typeof(String));
                    dt_new.Columns.Add("Grossweight", typeof(String));
                    dt_new.Columns.Add("TTLVolume", typeof(float));
                    dt_new.Columns.Add("TTLcont", typeof(float));
                    dt_new.Columns.Add("Qtycont", typeof(int));
                    dt_new.Columns.Add("TTLcont2", typeof(float));
                    dt_new.Columns.Add("Exfactorydate", typeof(DateTime));
                    dt_new.Columns.Add("ETD", typeof(DateTime));
                    //dt_new.Columns.Add("ETA", typeof(int));
                    dt_new.Columns.Add("ETA", typeof(DateTime));
                    dt_new.Columns.Add("Cancombine", typeof(String));
                    dt_new.Columns.Add("Risky", typeof(String));

                    string temp = dr_filter_keyconvert.SelectedValue.ToString();  //DPOversea28112025
                    string Category_ = "DPOversea28112025";
                    if (temp != "==TemplateName==")
                    {
                        Category_ = temp.Substring(0, temp.Length - 8); //DPOversea
                    }

                    

                    DataTable dtExcelData = DataConn.StoreFillDS("Get_infor_mater_template", System.Data.CommandType.StoredProcedure, temp);

                    string Sheet = "";
                    if (Category_ == "DPOversea")
                    {
                        Sheet = "DP";
                    } 
                    //else if () { }
                    string Cat = "";
                    string Shipmode = "";
                    string Consignee = "";
                    string Country = "";
                    string Destination = "";
                    string Model = "";
                    int Quantity = 0;
                    DateTime ATPdate;
                    string Volume = "";
                    string Grossweight = "";
                    float TTLVolume = 0;
                    float TTLcont = 0;
                    int Qtycont = 0;
                    float TTLcont2 = 0;
                    DateTime? Exfactorydate = null;
                    DateTime? ETD = null;
                    DateTime? ETA = null;
                    //int ETA = 0;
                    string Cancombine = "";
                    string Risky = "";

                    int req_qty = 0;
                    int jit_qty = 0;
                    int delay1_qty = 0;
                    int delay2_qty = 0;
                    int delay3_qty = 0;
                    int delay4_qty = 0;
                    int delay5_qty = 0;

                    int pcs_cnt = 0;  //phuc vu viec tinh Groww weight
                    int ctn_vol = 0;  //***
                    float ctn_weight = 0;  //phuc vu viec tinh Groww weight

                    DateTime ngayATP;

                    //int dongloi = 0;

                    if (Category_ == "DPOversea")
                    {
                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {
                            countlap = 0;
                            dongloi = i;

                            Model = dtExcelData.Rows[i]["producemodel"].ToString().Trim();
                            Cat = "DPOversea";// dtExcelData.Rows[i]["Category"].ToString();     // gia tri no la
                            Shipmode = dtExcelData.Rows[i]["sa"].ToString().Trim();
                            Consignee = dtExcelData.Rows[i]["name"].ToString().Trim();
                            //fix loi co dong trong phi duoi file
                            if (Model == "" || Cat == "" || Shipmode == "" || Consignee == "")
                            {
                                break;
                            }
                            Quantity = Int32.Parse(dtExcelData.Rows[i]["jitqty"].ToString().Trim());

                            //kiem tra xem tren csdl co chua? chua co thi moi them

                            dt_getmodel = DataConn.StoreFillDS("Get_infor_mater_model", System.Data.CommandType.StoredProcedure, Model, Cat);
                            if (dt_getmodel.Rows[0][0].ToString() == "1")
                            {
                                Country = dt_getmodel.Rows[0][1].ToString();            //lay tu mater model
                                Destination = dt_getmodel.Rows[0][2].ToString();        //lay tu mater model
                                Volume = dt_getmodel.Rows[0][3].ToString();             //lay tu mater model

                                pcs_cnt = Int32.Parse(dt_getmodel.Rows[0][6].ToString());
                                ctn_vol = Int32.Parse(dt_getmodel.Rows[0][7].ToString());
                                ctn_weight = float.Parse(dt_getmodel.Rows[0][8].ToString());

                                //Grossweight = dt_getmodel.Rows[0][4].ToString();       //lay tu mater model
                                //cong thuc duoc lay tu file test 4  => thay doi cong thuc
                                //Qty*product weight + Qty/pcs_ctnt*ctn_vol
                                float soluong_GW = (Quantity * float.Parse(dt_getmodel.Rows[0][4].ToString())) + (Quantity / pcs_cnt * ctn_weight);

                                Grossweight = soluong_GW.ToString();                //tinh theo cong thu phan test 4

                                Cancombine = dt_getmodel.Rows[0][5].ToString();         //link tu mater model sang mater vessel
                            }
                            else
                            {
                                Country = "";            //lay tu mater model
                                Destination = "";        //lay tu mater model
                                Volume = "0";             //lay tu mater model
                                Grossweight = "0";       //lay tu mater model
                            }

                            TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ  
                                                                                 //TTLcont = RoundUpDiv(TTLVolume, 53);   //Roundup(L9/53,0)
                            TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)
                            Qtycont = 0;    //(Dect khong co cot nay)                             //lay tu mater model 
                            TTLcont2 = 0;   // (Dect khong co cot nay)   Roundup(H/N,0)

                            // gia tri se duoc tinh o buoc so 2
                            Exfactorydate = null;// dtExcelData.Rows[i][""].ToString().Trim();
                            ETD = null;
                            ETA = null;
                            //ETA = 0;
                            //Cancombine = "";// dtExcelData.Rows[i][""].ToString().Trim();
                            Risky = "";

                            req_qty = Int32.Parse(dtExcelData.Rows[i]["ReqQty"].ToString().Trim());
                            jit_qty = Int32.Parse(dtExcelData.Rows[i]["jitqty"].ToString().Trim());

                            delay1_qty = GetIntValueFromExcel(dtExcelData.Rows[i]["delay1qty"]);
                            delay2_qty = GetIntValueFromExcel(dtExcelData.Rows[i]["delay2qty"]);
                            delay3_qty = GetIntValueFromExcel(dtExcelData.Rows[i]["delay3qty"]);
                            delay4_qty = GetIntValueFromExcel(dtExcelData.Rows[i]["delay4qty"]);
                            delay5_qty = GetIntValueFromExcel(dtExcelData.Rows[i]["delay5qty"]);

                            if (jit_qty == req_qty)
                            {
                                // neu jit_qty=req_qty => lấy cột jit_qty (cột Q) => ATP date lấy theo cột ship_date (cột O)
                                Quantity = jit_qty;
                                if (DateTime.TryParse(dtExcelData.Rows[i]["shipdate"].ToString().Trim(), out ATPdate))
                                {
                                    ngayATP = ATPdate;
                                }
                                else
                                {
                                    // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                    Console.WriteLine($"Giá trị không hợp lệ!");
                                }
                                //ATPdate = dtExcelData.Rows[i]["ship_date"].ToString().Trim();
                                dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                            }
                            else
                            {
                                //tach thanh cac dong tuong ung voi cac cot delay
                                if (jit_qty < req_qty)
                                {
                                    Quantity = jit_qty;
                                    if (DateTime.TryParse(dtExcelData.Rows[i]["shipdate"].ToString().Trim(), out ATPdate))
                                    {
                                        ngayATP = ATPdate;
                                    }
                                    else
                                    {
                                        // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                        Console.WriteLine($"Giá trị không hợp lệ!");
                                    }
                                    TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                    TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)

                                    //ATPdate = dtExcelData.Rows[i]["ship_date"].ToString().Trim();
                                    dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                }
                                if (delay1_qty > 0)
                                {
                                    //add them 1 dong gia tri
                                    Quantity = delay1_qty;
                                    if (DateTime.TryParse(dtExcelData.Rows[i]["delay1date"].ToString().Trim(), out ATPdate))
                                    {
                                        ngayATP = ATPdate;
                                    }
                                    else
                                    {
                                        // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                        Console.WriteLine($"Giá trị không hợp lệ 1!");
                                    }
                                    TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                    TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)

                                    dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                }
                                if (delay2_qty > 0)
                                {
                                    //add them 1 dong gia tri
                                    Quantity = delay2_qty;
                                    if (DateTime.TryParse(dtExcelData.Rows[i]["delay2date"].ToString().Trim(), out ATPdate))
                                    {
                                        ngayATP = ATPdate;
                                    }
                                    else
                                    {
                                        // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                        Console.WriteLine($"Giá trị không hợp lệ 2!");
                                    }
                                    TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                    TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)
                                    dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                }
                                if (delay3_qty > 0)
                                {
                                    //add them 1 dong gia tri
                                    Quantity = delay3_qty;
                                    if (DateTime.TryParse(dtExcelData.Rows[i]["delay3date"].ToString().Trim(), out ATPdate))
                                    {
                                        ngayATP = ATPdate;
                                    }
                                    else
                                    {
                                        // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                        Console.WriteLine($"Giá trị không hợp lệ 3!");
                                    }
                                    TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                    TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)
                                    dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                }
                                if (delay4_qty > 0)
                                {
                                    //add them 1 dong gia tri
                                    Quantity = delay4_qty;
                                    if (DateTime.TryParse(dtExcelData.Rows[i]["delay4date"].ToString().Trim(), out ATPdate))
                                    {
                                        ngayATP = ATPdate;
                                    }
                                    else
                                    {
                                        // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                        Console.WriteLine($"Giá trị không hợp lệ 4!");
                                    }
                                    TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                    TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)
                                    dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                }
                                if (delay5_qty > 0)
                                {
                                    //add them 1 dong gia tri
                                    Quantity = delay5_qty;
                                    if (DateTime.TryParse(dtExcelData.Rows[i]["delay5date"].ToString().Trim(), out ATPdate))
                                    {
                                        ngayATP = ATPdate;
                                    }
                                    else
                                    {
                                        // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                        Console.WriteLine($"Giá trị không hợp lệ 5!");
                                    }
                                    TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                    TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)
                                    dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                }
                            }

                            //ATPdate = dtExcelData.Rows[i][""].ToString().Trim();

                            // Dừng vòng lặp khi các cột cần kiểm tra (cột 0, 2, 3) đều rỗng
                            if (dtExcelData.Rows[i][0].ToString() == "" && dtExcelData.Rows[i][1].ToString() == "" && dtExcelData.Rows[i][2].ToString() == "")
                            {
                                break;
                            }

                        }
                    }

                    if (dt_new.Rows.Count > 0)
                    {
                        //string sqlConnStr = "Data Source=./;Persist Security Info=False;" +
                        //                        "Initial Catalog=OQC;User Id=sa;Password='';" +
                        //                        "Connect Timeout=30;";

                        string sqlConnStr = "Data Source=10.92.184.22\\hienpc;Persist Security Info=False;" +
                        "Initial Catalog=LichTau;User Id=sa;Password=Hien304@;" +
                        "Connect Timeout=30;";

                        using (SqlConnection con = new SqlConnection(sqlConnStr))
                        {
                            con.Open();

                            // Initialize SqlBulkCopy.
                            using (SqlBulkCopy oSqlBulk = new SqlBulkCopy(con))
                            {
                                oSqlBulk.DestinationTableName = "tblVanningDate"; // Table name in database.
                                                                                  //oSqlBulk.WriteToServer(dtExcelData); // Write data from DataTable to database.
                                oSqlBulk.WriteToServer(dt_new);
                            }
                        }
                        if (countlap > 0)
                        {
                            lblConfirm.Text = "Ban ghi lap : " + countlap;
                            lblConfirm.Attributes.Add("style", "color:green");
                        }
                        else
                        {
                            lblConfirm.Text = "DATA IMPORTED SUCCESSFULLY.";
                            lblConfirm.Attributes.Add("style", "color:green");
                        }
                        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Thành công!');", true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('OK, Upload thành công!');", true);
                        dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate", System.Data.CommandType.StoredProcedure);

                    }
                    else 
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Data Null!'); ", true);
                    }

                    
                }
                catch (Exception ex)
                {
                    lblConfirm.Text = ex.Message + dongloi;
                    lblConfirm.Attributes.Add("style", "color:red");
                    //throw;
                }
                
            }
            else 
            {
                //truong hop upload bang file
                if (FileUpload.HasFile)
                {
                    if (FileUpload.PostedFile.ContentLength > 0)
                    {
                        // Save the uploaded file to the server.
                        FileUpload.SaveAs(Server.MapPath(".") + "\\" + FileUpload.FileName);

                        // Set connection string with the Excel file.
                        //string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" +
                        //                      Server.MapPath(".") + "\\" + FileUpload.FileName +
                        //                      "; Extended Properties=Excel 12.0;"

                        //new
                        string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" +
                          Server.MapPath(".") + "\\" + FileUpload.FileName +
                          "; Extended Properties='Excel 12.0; HDR=YES; IMEX=1;'"; // HDR=YES để xử lý header, IMEX=1 để xử lý cả dữ liệu chuỗi và số

                        OleDbConnection excelConn = null;
                        OleDbDataReader objBulkReader = null;

                        int dongloi = 0;

                        try
                        {
                            DataTable dt_checkupload = new DataTable();
                            DataTable dt_new = new DataTable();
                            int countlap = 0;

                            dt_new.Columns.Add("ID", typeof(Int32));
                            dt_new.Columns.Add("Sheet", typeof(String));
                            dt_new.Columns.Add("Cat", typeof(String));
                            dt_new.Columns.Add("Shipmode", typeof(String));
                            dt_new.Columns.Add("Consignee", typeof(String));
                            dt_new.Columns.Add("Country", typeof(String));
                            dt_new.Columns.Add("Destination", typeof(String));
                            dt_new.Columns.Add("Model", typeof(String));
                            dt_new.Columns.Add("Quantity", typeof(int));
                            dt_new.Columns.Add("ATPdate", typeof(DateTime));
                            dt_new.Columns.Add("Volume", typeof(String));
                            dt_new.Columns.Add("Grossweight", typeof(String));
                            dt_new.Columns.Add("TTLVolume", typeof(float));
                            dt_new.Columns.Add("TTLcont", typeof(float));
                            dt_new.Columns.Add("Qtycont", typeof(int));
                            dt_new.Columns.Add("TTLcont2", typeof(float));
                            dt_new.Columns.Add("Exfactorydate", typeof(DateTime));
                            dt_new.Columns.Add("ETD", typeof(DateTime));
                            //dt_new.Columns.Add("ETA", typeof(int));
                            dt_new.Columns.Add("ETA", typeof(DateTime));
                            dt_new.Columns.Add("Cancombine", typeof(String));
                            dt_new.Columns.Add("Risky", typeof(String));


                            // Open connection to Excel file.
                            excelConn = new OleDbConnection(excelConnStr);
                            excelConn.Open();

                            // Lấy danh sách các sheet trong Excel
                            DataTable sheets = excelConn.GetSchema("Tables");
                            // Lấy tên sheet đầu tiên (vì chỉ có một sheet)
                            string sheetName = sheets.Rows[0]["TABLE_NAME"].ToString();
                            Console.WriteLine("Tên sheet: " + sheetName);

                            // Xử lý tên sheet (nếu có ký tự đặc biệt)
                            string sanitizedSheetName = SanitizeSheetName(sheetName);
                            // Tạo câu truy vấn SQL với tên sheet đã xử lý
                            OleDbCommand objOleDB = new OleDbCommand($"SELECT * FROM [{sanitizedSheetName}$]", excelConn);
                            // Get data from Excel sheet.
                            //OleDbCommand objOleDB = new OleDbCommand("SELECT * FROM [Sheet1$]", excelConn);
                            //OleDbCommand objOleDB = new OleDbCommand("SELECT * FROM [{sanitizedSheetName}$]", excelConn);

                            objBulkReader = objOleDB.ExecuteReader();
                            // Check if there is data to process.
                            if (objBulkReader.HasRows)
                            {
                                DataTable dtExcelData = new DataTable();
                                dtExcelData.Load(objBulkReader); // Load data into DataTable.

                                string Category_ = "";
                                if (rblDP.Checked)
                                {
                                    Category_ = rblDP.Text;
                                }
                                else if (rblDECT.Checked)
                                {
                                    Category_ = rblDECT.Text;
                                }
                                else if (rblMW.Checked)
                                {
                                    Category_ = rblMW.Text;
                                }
                                else if (rblSound.Checked)
                                {
                                    Category_ = rblSound.Text;
                                }

                                string Sheet = sheetName.Replace("$", "");
                                string Cat = "";
                                string Shipmode = "";
                                string Consignee = "";
                                string Country = "";
                                string Destination = "";
                                string Model = "";
                                int Quantity = 0;
                                DateTime ATPdate;
                                string Volume = "";
                                string Grossweight = "";
                                float TTLVolume = 0;
                                float TTLcont = 0;
                                int Qtycont = 0;
                                float TTLcont2 = 0;
                                DateTime? Exfactorydate = null;
                                DateTime? ETD = null;
                                DateTime? ETA = null;
                                //int ETA = 0;
                                string Cancombine = "";
                                string Risky = "";

                                int req_qty = 0;
                                int jit_qty = 0;
                                int delay1_qty = 0;
                                int delay2_qty = 0;
                                int delay3_qty = 0;
                                int delay4_qty = 0;
                                int delay5_qty = 0;

                                int pcs_cnt = 0;  //phuc vu viec tinh Groww weight
                                int ctn_vol = 0;  //***
                                float ctn_weight = 0;  //phuc vu viec tinh Groww weight

                                DateTime ngayATP;

                                //int dongloi = 0;

                                if (Category_ == "DECT")
                                {
                                    for (int i = 0; i < dtExcelData.Rows.Count; i++)
                                    {
                                        countlap = 0;
                                        dongloi = i;

                                        Model = dtExcelData.Rows[i]["produce_model"].ToString().Trim();
                                        Cat = dtExcelData.Rows[i]["Category"].ToString();
                                        Shipmode = dtExcelData.Rows[i]["s/a"].ToString().Trim();
                                        Consignee = dtExcelData.Rows[i]["name"].ToString().Trim();
                                        //fix loi co dong trong phi duoi file
                                        //*** xem xet lai truong null van input
                                        if (Model == "" || Cat == "" || Shipmode == "" || Consignee == "")
                                        {
                                            break;
                                        }
                                        Quantity = Int32.Parse(dtExcelData.Rows[i]["jit_qty"].ToString().Trim());

                                        //kiem tra xem tren csdl co chua? chua co thi moi them

                                        //dt_getmodel = DataConn.StoreFillDS("Get_infor_mater_model", System.Data.CommandType.StoredProcedure, Model, Cat);
                                        //bat them ca dieu kien cosignee => 1 model va 1 cat co the di nhieu thi truong  ****12.09.2025
                                        dt_getmodel = DataConn.StoreFillDS("Get_infor_mater_model", System.Data.CommandType.StoredProcedure, Model, Cat, Consignee);
                                        if (dt_getmodel.Rows[0][0].ToString() == "1")
                                        {
                                            Country = dt_getmodel.Rows[0][1].ToString();            //lay tu mater model
                                            Destination = dt_getmodel.Rows[0][2].ToString();        //lay tu mater model
                                            Volume = dt_getmodel.Rows[0][3].ToString();             //lay tu mater model

                                            pcs_cnt = Int32.Parse(dt_getmodel.Rows[0][6].ToString());
                                            ctn_vol = Int32.Parse(dt_getmodel.Rows[0][7].ToString());
                                            ctn_weight = float.Parse(dt_getmodel.Rows[0][8].ToString());

                                            //Grossweight = dt_getmodel.Rows[0][4].ToString();       //lay tu mater model
                                            //cong thuc duoc lay tu file test 4  => thay doi cong thuc
                                            //Qty*product weight + Qty/pcs_ctnt*ctn_vol
                                            float soluong_GW = (Quantity * float.Parse(dt_getmodel.Rows[0][4].ToString())) + (Quantity / pcs_cnt * ctn_weight);

                                            Grossweight = soluong_GW.ToString();                //tinh theo cong thu phan test 4

                                            Cancombine = dt_getmodel.Rows[0][5].ToString();         //link tu mater model sang mater vessel
                                        }
                                        else
                                        {
                                            Country = "";            //lay tu mater model
                                            Destination = "";        //lay tu mater model
                                            Volume = "0";             //lay tu mater model
                                            Grossweight = "0";       //lay tu mater model
                                        }

                                        TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ  
                                                                                             //TTLcont = RoundUpDiv(TTLVolume, 53);   //Roundup(L9/53,0)
                                        TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)
                                        Qtycont = 0;    //(Dect khong co cot nay)                             //lay tu mater model 
                                        TTLcont2 = 0;   // (Dect khong co cot nay)   Roundup(H/N,0)

                                        // gia tri se duoc tinh o buoc so 2
                                        Exfactorydate = null;// dtExcelData.Rows[i][""].ToString().Trim();
                                        ETD = null;
                                        ETA = null;
                                        //ETA = 0;
                                        //Cancombine = "";// dtExcelData.Rows[i][""].ToString().Trim();
                                        Risky = "";

                                        req_qty = Int32.Parse(dtExcelData.Rows[i]["req_qty"].ToString().Trim());
                                        jit_qty = Int32.Parse(dtExcelData.Rows[i]["jit_qty"].ToString().Trim());

                                        delay1_qty = GetIntValueFromExcel(dtExcelData.Rows[i]["delay1_qty"]);
                                        delay2_qty = GetIntValueFromExcel(dtExcelData.Rows[i]["delay2_qty"]);
                                        delay3_qty = GetIntValueFromExcel(dtExcelData.Rows[i]["delay3_qty"]);
                                        delay4_qty = GetIntValueFromExcel(dtExcelData.Rows[i]["delay4_qty"]);
                                        delay5_qty = GetIntValueFromExcel(dtExcelData.Rows[i]["delay5_qty"]);

                                        if (jit_qty == req_qty)
                                        {
                                            // neu jit_qty=req_qty => lấy cột jit_qty (cột Q) => ATP date lấy theo cột ship_date (cột O)
                                            Quantity = jit_qty;
                                            if (DateTime.TryParse(dtExcelData.Rows[i]["ship_date"].ToString().Trim(), out ATPdate))
                                            {
                                                ngayATP = ATPdate;
                                            }
                                            else
                                            {
                                                // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                                Console.WriteLine($"Giá trị không hợp lệ!");
                                            }
                                            //ATPdate = dtExcelData.Rows[i]["ship_date"].ToString().Trim();
                                            dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                        }
                                        else
                                        {
                                            //tach thanh cac dong tuong ung voi cac cot delay
                                            if (jit_qty < req_qty)
                                            {
                                                Quantity = jit_qty;
                                                if (DateTime.TryParse(dtExcelData.Rows[i]["ship_date"].ToString().Trim(), out ATPdate))
                                                {
                                                    ngayATP = ATPdate;
                                                }
                                                else
                                                {
                                                    // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                                    Console.WriteLine($"Giá trị không hợp lệ!");
                                                }
                                                TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                                TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)

                                                //ATPdate = dtExcelData.Rows[i]["ship_date"].ToString().Trim();
                                                dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                            }
                                            if (delay1_qty > 0)
                                            {
                                                //add them 1 dong gia tri
                                                Quantity = delay1_qty;
                                                if (DateTime.TryParse(dtExcelData.Rows[i]["delay1_date"].ToString().Trim(), out ATPdate))
                                                {
                                                    ngayATP = ATPdate;
                                                }
                                                else
                                                {
                                                    // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                                    Console.WriteLine($"Giá trị không hợp lệ 1!");
                                                }
                                                TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                                TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)

                                                dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                            }
                                            if (delay2_qty > 0)
                                            {
                                                //add them 1 dong gia tri
                                                Quantity = delay2_qty;
                                                if (DateTime.TryParse(dtExcelData.Rows[i]["delay2_date"].ToString().Trim(), out ATPdate))
                                                {
                                                    ngayATP = ATPdate;
                                                }
                                                else
                                                {
                                                    // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                                    Console.WriteLine($"Giá trị không hợp lệ 2!");
                                                }
                                                TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                                TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)
                                                dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                            }
                                            if (delay3_qty > 0)
                                            {
                                                //add them 1 dong gia tri
                                                Quantity = delay3_qty;
                                                if (DateTime.TryParse(dtExcelData.Rows[i]["delay3_date"].ToString().Trim(), out ATPdate))
                                                {
                                                    ngayATP = ATPdate;
                                                }
                                                else
                                                {
                                                    // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                                    Console.WriteLine($"Giá trị không hợp lệ 3!");
                                                }
                                                TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                                TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)
                                                dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                            }
                                            if (delay4_qty > 0)
                                            {
                                                //add them 1 dong gia tri
                                                Quantity = delay4_qty;
                                                if (DateTime.TryParse(dtExcelData.Rows[i]["delay4_date"].ToString().Trim(), out ATPdate))
                                                {
                                                    ngayATP = ATPdate;
                                                }
                                                else
                                                {
                                                    // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                                    Console.WriteLine($"Giá trị không hợp lệ 4!");
                                                }
                                                TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                                TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)
                                                dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                            }
                                            if (delay5_qty > 0)
                                            {
                                                //add them 1 dong gia tri
                                                Quantity = delay5_qty;
                                                if (DateTime.TryParse(dtExcelData.Rows[i]["delay5_date"].ToString().Trim(), out ATPdate))
                                                {
                                                    ngayATP = ATPdate;
                                                }
                                                else
                                                {
                                                    // Ép kiểu thất bại, có thể xử lý lỗi ở đây
                                                    Console.WriteLine($"Giá trị không hợp lệ 5!");
                                                }
                                                TTLVolume = float.Parse(Volume) * Quantity;          //cot HxJ                                               
                                                TTLcont = (TTLVolume / 53);   //Roundup(L9/53,0)
                                                dt_new.Rows.Add(i, Sheet, Cat, Shipmode, Consignee, Country, Destination, Model, Quantity, ATPdate, Volume, Grossweight, TTLVolume, TTLcont, Qtycont, TTLcont2, Exfactorydate, ETD, ETA, Cancombine, Risky);
                                            }
                                        }

                                        //ATPdate = dtExcelData.Rows[i][""].ToString().Trim();

                                        // Dừng vòng lặp khi các cột cần kiểm tra (cột 0, 2, 3) đều rỗng
                                        if (dtExcelData.Rows[i][0].ToString() == "" && dtExcelData.Rows[i][1].ToString() == "" && dtExcelData.Rows[i][2].ToString() == "")
                                        {
                                            break;
                                        }

                                    }
                                }
                                else if (Category_ == "DP") { }
                                else if (Category_ == "MW") { }
                                else if (Category_ == "PJ") { }
                                else if (Category_ == "SB") { }
                                else if (Category_ == "CAM") { }
                                else
                                {
                                    //lblConfirm.Text = "Ban chua chon category!";
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon category!'); ", true);
                                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Ban chua chon category! </br> Vui long chon Cate','Lỗi'); ", true);

                                }
                                //string sqlConnStr = "Data Source=./;Persist Security Info=False;" +
                                //                        "Initial Catalog=OQC;User Id=sa;Password='';" +
                                //                        "Connect Timeout=30;";

                                //string sqlConnStr = "Data Source=10.92.186.30;Persist Security Info=False;" +
                                //    "Initial Catalog=PC_Inventory_Infra;User Id=sa;Password=Psnvdb2013;" +
                                //    "Connect Timeout=30;";

                                string sqlConnStr = "Data Source=10.92.184.22\\hienpc;Persist Security Info=False;" +
        "Initial Catalog=LichTau;User Id=sa;Password=Hien304@;" +
        "Connect Timeout=30;";


                                using (SqlConnection con = new SqlConnection(sqlConnStr))
                                {
                                    con.Open();

                                    // Initialize SqlBulkCopy.
                                    using (SqlBulkCopy oSqlBulk = new SqlBulkCopy(con))
                                    {
                                        oSqlBulk.DestinationTableName = "tblVanningDate"; // Table name in database.
                                                                                          //oSqlBulk.WriteToServer(dtExcelData); // Write data from DataTable to database.
                                        oSqlBulk.WriteToServer(dt_new);
                                    }
                                }
                                if (countlap > 0)
                                {
                                    lblConfirm.Text = "Ban ghi lap : " + countlap;
                                    lblConfirm.Attributes.Add("style", "color:green");
                                }
                                else
                                {
                                    lblConfirm.Text = "DATA IMPORTED SUCCESSFULLY.";
                                    lblConfirm.Attributes.Add("style", "color:green");
                                }
                                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Thành công!');", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('OK, Upload thành công!');", true);
                                dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate", System.Data.CommandType.StoredProcedure);
                            }
                        }
                        catch (Exception ex)
                        {
                            lblConfirm.Text = ex.Message + dongloi;
                            lblConfirm.Attributes.Add("style", "color:red");
                            //throw;
                        }
                        finally
                        {
                            // Close and dispose objects.
                            if (objBulkReader != null && !objBulkReader.IsClosed)
                            {
                                objBulkReader.Close();
                            }
                            if (excelConn != null && excelConn.State == ConnectionState.Open)
                            {
                                excelConn.Close();
                            }
                            // Delete the uploaded file (optional).
                            File.Delete(Server.MapPath(".") + "\\" + FileUpload.FileName);
                            // Reload grid or perform other necessary actions.
                            //dt_phanca = Db_connect.StoreFillDS("HR_List_phanca", System.Data.CommandType.StoredProcedure);
                        }
                    }
                }
            }            
        }
        public static string SanitizeSheetName(string sheetName)
        {
            // Loại bỏ các ký tự không hợp lệ cho tên sheet trong Excel
            // Các ký tự không hợp lệ bao gồm: :, \, /, ?, *, [, ], và dấu cách đầu hoặc cuối
            string pattern = @"[^a-zA-Z0-9\s]";  // Giữ lại chữ cái, số và dấu cách
            sheetName = Regex.Replace(sheetName, pattern, "");

            // Cắt tên sheet nếu quá dài (tối đa 31 ký tự)
            if (sheetName.Length > 31)
            {
                sheetName = sheetName.Substring(0, 31);
            }

            // Đảm bảo rằng tên sheet kết thúc với dấu $
            return sheetName;
        }

        public static int GetIntValueFromExcel(object value)
        {
            if (value == DBNull.Value || value == null)
                return 0;

            string strValue = value.ToString().Trim();
            if (string.IsNullOrEmpty(strValue))
                return 0;

            if (int.TryParse(strValue, out int result))
                return result;

            // Nếu không parse được (ví dụ: "abc"), trả về 0 hoặc xử lý tùy ý
            return 0;
        }

        public static int RoundUpDiv(float value, int divisor)
        {
            return (int)Math.Ceiling((double)value / divisor);
        }

        // Hàm tính tuần thứ mấy trong tháng
        public static int GetWeekOfMonth(DateTime date)
        {
            // Lấy ngày đầu tiên của tháng
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            // Lấy số ngày cần đệm từ đầu tuần đến ngày đầu tháng
            int offset = (int)date.DayOfWeek - (int)firstDayOfMonth.DayOfWeek;

            // Nếu ngày trong tuần của ngày hiện tại nhỏ hơn ngày đầu tháng (do tuần bắt đầu từ Chủ Nhật)
            if (offset < 0) offset += 7;

            // Tính tuần thứ mấy
            return ((date.Day + offset - 1) / 7) + 1;
        }

        public static int GetWeekOfMonth_New(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            int firstDayWeekDay = (int)firstDayOfMonth.DayOfWeek; // Chủ Nhật = 0, Thứ Bảy = 6
            int dayOfMonth = date.Day;

            int adjustedDay = dayOfMonth + firstDayWeekDay;

            int weekNumber = (int)Math.Ceiling(adjustedDay / 7.0);

            return weekNumber;
        }

        //Tạo ham chuyen chuoi thanh DayOfWeek
        public static DayOfWeek ConvertToDayOfWeek(string dayAbbreviation)
        {
            switch (dayAbbreviation.ToUpper())
            {
                case "SUN": return DayOfWeek.Sunday;
                case "MON": return DayOfWeek.Monday;
                case "TUE": return DayOfWeek.Tuesday;
                case "WED": return DayOfWeek.Wednesday;
                case "THU": return DayOfWeek.Thursday;
                case "FRI": return DayOfWeek.Friday;
                case "SAT": return DayOfWeek.Saturday;
                default: throw new ArgumentException("Invalid day abbreviation");
            }
        }

        public static bool IsSameWeek_theongay(DateTime date1, DateTime date2)
        {
            CultureInfo ci = CultureInfo.InvariantCulture;
            System.Globalization.Calendar calendar = ci.Calendar;

            CalendarWeekRule weekRule = CalendarWeekRule.FirstFourDayWeek;
            DayOfWeek firstDayOfWeek = DayOfWeek.Monday;

            int week1 = calendar.GetWeekOfYear(date1, weekRule, firstDayOfWeek);
            int week2 = calendar.GetWeekOfYear(date2, weekRule, firstDayOfWeek);

            return (week1 == week2 && date1.Year == date2.Year);
        }

        //public static DayOfWeek? ConvertToDayOfWeek(string dayAbbr)   //fix truong hop null 04.09.2025
        //{
        //    if (string.IsNullOrWhiteSpace(dayAbbr))
        //        return null;

        //    switch (dayAbbr.Trim().ToUpper())
        //    {
        //        case "SUN": return DayOfWeek.Sunday;
        //        case "MON": return DayOfWeek.Monday;
        //        case "TUE": return DayOfWeek.Tuesday;
        //        case "WED": return DayOfWeek.Wednesday;
        //        case "THU": return DayOfWeek.Thursday;
        //        case "FRI": return DayOfWeek.Friday;
        //        case "SAT": return DayOfWeek.Saturday;
        //        default: return null; // hoặc throw nếu muốn báo lỗi
        //    }
        //}

    }
}