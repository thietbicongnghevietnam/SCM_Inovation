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

namespace FreeLayout
{
    public partial class frmVanningDateSCM : System.Web.UI.Page
    {
        public DataTable dt_plan = new DataTable();
        public DataTable dt_getmodel = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate", System.Data.CommandType.StoredProcedure);
                //Date1.Value = DateTime.Now.ToString("dd-MM-yyyy");
                //ngaychiid.Value = DateTime.Now.ToString("dd-MM-yyyy");
                dtcate = DataConn.StoreFillDS("pro_get_categogy", System.Data.CommandType.StoredProcedure);
                DataRow newRow1 = dtcate.NewRow();
                newRow1["Description"] = "==Categogy==";
                dtcate.Rows.InsertAt(newRow1, 0);
                dr_filter_Cate.DataSource = dtcate;
                dr_filter_Cate.DataBind();
                
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  // Dùng cho mục đích phi thương mại
                
            }

        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string category = dr_filter_Cate.SelectedValue;
            //loc theo ngay
            if (_fromdate == "" || _fromdate == "")
            {
                //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban nen chon ngay!!!'); ", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Ban nen chon ngay!');", true);
            }
            else
            {
                if (category == "==Categogy==")
                {
                    //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
                    dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
                }
                else
                {
                    dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2_cate", System.Data.CommandType.StoredProcedure, _fromdate, _todate, category);
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
            //if (category == "==Categogy==")
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
                            dt_get_infor = DataConn.StoreFillDS("get_infor_cal_risk", System.Data.CommandType.StoredProcedure, Destination, Exfactorydate, cancombine, ID_lichtau);
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
                                int weekOfMonth_rq = GetWeekOfMonth(date_request);

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
                                    
                                    DateTime date_request1 = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                                    if (SpecialETD_week != "")
                                    {
                                        //truong hop 1
                                        //tinh ra tuan cua special note //neu bang thi lay theo truoc do?
                                        if (weekOfMonth_rq <= int.Parse(SpecialETD_week))
                                        {
                                            //so sanh truong hop day1 va day2 xem truong hop null khong?
                                            if (day1.HasValue && day2.HasValue)
                                            {
                                                if ((int)day1 < (int)day2)
                                                {
                                                    //Response.Write("Ngày đứng trước là: " + day1);
                                                    //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                                    bool isDiffWeek = IsDifferentWeek(day2, day2b);  //lay day2b lam goc
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
                                                    //DateTime date_request1 = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                                                    //1*so sánh tương quan ngày Ex-factory và ngày ETD xem có cùng tuần hay không? 
                                                    //neu khac tuan thi phai lay lich tau khac
                                                    bool isDiffWeek = IsDifferentWeek(day1, day1b);  //lay day1b lam goc
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
                                                                                                                        //update len 2 gia tri len co so du lieu *****
                                                                                                                        //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

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
                                                if ((int)day1 < (int)day2)
                                                {
                                                    //Response.Write("Ngày đứng trước là: " + day1);
                                                    //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);                                                
                                                    bool isDiffWeek = IsDifferentWeek(day2, day2b);  //lay day2b lam goc
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
                                                    bool isDiffWeek = IsDifferentWeek(day1, day1b);  //lay day1b lam goc
                                                                                                     //Console.WriteLine(isDiffWeek ? "Khác tuần" : "Cùng tuần");
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
                                                }
                                            }
                                            else if (day1.HasValue && !day2.HasValue)
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
                                    else if (Special_ETA_Date != "")
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

                                                //string ngaycantim1 = ngayDatru1.ToString("dd/MM/yyyy");
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
                                            else if ((int)day22 < (int)day11)
                                            {
                                                //lay gia tri transit time  la "MON" < "WED"
                                                //lay ngay tang len 1 thang - transit time : 8/14/2025 - 28
                                                //stansit_time = dt_mater_vessel.Rows[0]["LLC_ETA"].ToString();  ////transit time

                                                //tinh toan nhu cu ******
                                                DateTime ngayDatru2 = ketQua.AddDays(-Int32.Parse(stansit_time2));
                                                //string ngaycantim2 = ngayDatru2.ToString("dd/MM/yyyy");
                                                string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = LLC_Ex_factory;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay = GetSpecificDayInWeek(ngayDatru2, targetDay);   //tinh ra ngay ETD
                                                DateTime resultDay2 = GetSpecificDayInPreviousWeek(ngayDatru2, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)
                                                                                                                             //DateTime ngayTru1Tuan2 = resultDay2.AddDays(-7); // Trừ 7 ngày

                                                //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time2));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                count = count + 1;
                                            }
                                            else if (Int32.Parse(stansit_time) > Int32.Parse(stansit_time2))
                                            {
                                                //lay teho trantsit time1  //lay theo FCL
                                                DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));
                                                //string ngaycantim1 = ngayDatru1.ToString("dd/MM/yyyy");
                                                string inputDay = FCL_ETD;
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = FCL_Ex_factory;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                DateTime resultDay2 = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                count = count + 1;
                                            }
                                            else if (Int32.Parse(stansit_time) < Int32.Parse(stansit_time2))
                                            {
                                                //lay teho trantsit time2  //lay theo LLC
                                                DateTime ngayDatru2 = ketQua.AddDays(-Int32.Parse(stansit_time2));                                               
                                                string inputDay = LLC_ETD;// "MON"; // giá trị truyền vào thu2
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = LLC_Ex_factory;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay = GetSpecificDayInWeek(ngayDatru2, targetDay);   //tinh ra ngay ETD
                                                DateTime resultDay2 = GetSpecificDayInPreviousWeek(ngayDatru2, targetDay2);  // tinh ra ngay Ex-factory day  (***truoc 1 tuan tau chay***)
                                                //DateTime ngayTru1Tuan2 = resultDay2.AddDays(-7); // Trừ 7 ngày
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time2));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                                count = count + 1;
                                            }
                                            else 
                                            {
                                                //lay teho trantsit time1  //lay theo FCL
                                                DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));
                                                //string ngaycantim1 = ngayDatru1.ToString("dd/MM/yyyy");
                                                string inputDay = FCL_ETD;
                                                DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                                string inputDay2 = FCL_Ex_factory;
                                                DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                                DateTime resultDay = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                                DateTime resultDay2 = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day
                                                //tinh ra ngay ETA =  Ngay ETD + transitime
                                                DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time));
                                                dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);
                                                count = count + 1;
                                            }
                                        }
                                        else if ((int)day1 < (int)day2)
                                        {
                                            //tinh toan nhu cu  ****
                                            //stansit_time = dt_mater_vessel.Rows[0]["FCL_ETA"].ToString();  //transit time
                                            DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time));

                                            //string ngaycantim1 = ngayDatru1.ToString("dd/MM/yyyy");
                                            string inputDay = FCL_ETD;// "THU"; // giá trị truyền vào thu 5
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
                                        else if ((int)day2 < (int)day1)
                                        {
                                            DateTime ngayDatru1 = ketQua.AddDays(-Int32.Parse(stansit_time2));

                                            //string ngaycantim1 = ngayDatru1.ToString("dd/MM/yyyy");
                                            string inputDay = LLC_ETD;// "THU"; // giá trị truyền vào thu 5
                                            DayOfWeek targetDay = ConvertToDayOfWeek(inputDay);
                                            string inputDay2 = LLC_Ex_factory;
                                            DayOfWeek targetDay2 = ConvertToDayOfWeek(inputDay2);

                                            DateTime resultDay = GetSpecificDayInWeek(ngayDatru1, targetDay);   //tinh ra ngay ETD
                                            DateTime resultDay2 = GetSpecificDayInPreviousWeek(ngayDatru1, targetDay2);  // tinh ra ngay Ex-factory day                                                                                                                        

                                            //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay2, resultDay, ID_lichtau);
                                            //tinh ra ngay ETA =  Ngay ETD + transitime
                                            DateTime dateETA = resultDay.AddDays(Int32.Parse(stansit_time2));
                                            dt_update = DataConn.StoreFillDS("Update_lichtau2", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau, dateETA);

                                            count = count + 1;
                                        }
                                    }
                                    else if (Special_exfactory_date != "")
                                    {
                                        //truong hop 2  => cate dect khong co truong hop 2
                                    }
                                    else 
                                    {
                                        //truong hop khong co ngay special note => quy ra tuần ATP -> Đối chiếu lịch tàu trong tuần đó 

                                        //so sanh truong hop day1 va day2 xem truong hop null khong?
                                        if (day1.HasValue && day2.HasValue)
                                        {
                                            if ((int)day1 < (int)day2)
                                            {
                                                //Response.Write("Ngày đứng trước là: " + day1);
                                                //DateTime date_request1 = DateTime.ParseExact(ATPdate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                                bool isDiffWeek = IsDifferentWeek(day1, day1b);  //lay day2b lam goc
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
                                                //DateTime date_request1 = DateTime.ParseExact(ATPdate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                                                //1*so sánh tương quan ngày Ex-factory và ngày ETD xem có cùng tuần hay không? 
                                                //neu khac tuan thi phai lay lich tau khac
                                                bool isDiffWeek = IsDifferentWeek(day2, day2b);  
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
                                                                                                                    //update len 2 gia tri len co so du lieu *****
                                                                                                                    //dt_update = DataConn.StoreFillDS("Update_lichtau", System.Data.CommandType.StoredProcedure, resultDay, resultDay2, ID_lichtau);

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
                        else if (Category_ == "MW") 
                        {
                        
                        }
                        else if (Category_ == "DP")
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

            if (Category_ == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon category!'); ", true);
            }

           
           

            //dowload excel
            dt_dowload = DataConn.StoreFillDS("Select_Upload_VanningDate2_cate", System.Data.CommandType.StoredProcedure, _fromdate, _todate, Category_);

            //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=Baocao_Vessel.xls");
            //Response.Charset = "";
            //Response.ContentType = "application/ms-excel";

            ////System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            ////response.Clear();
            ////response.Buffer = true;
            ////response.Charset = "";
            ////response.ContentType = "text/csv";
            ////response.AddHeader("Content-Disposition", "attachment;filename=myfilename.csv");

            //if (dt_dowload != null)
            //{
            //    foreach (DataColumn dc in dt_dowload.Columns)
            //    {
            //        Response.Write(dc.ColumnName + "\t");

            //    }
            //    Response.Write(System.Environment.NewLine);
            //    foreach (DataRow dr in dt_dowload.Rows)
            //    {
            //        for (int i = 0; i < dt_dowload.Columns.Count; i++)
            //        {
            //            Response.Write(dr[i].ToString() + "\t");
            //        }
            //        Response.Write("\n");
            //    }
            //}
            //Response.End();  //must this sentence           

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

                // Ghi header
                for (int col = 0; col < dt_dowload.Columns.Count; col++)
                {
                    ws.Cells[1, col + 1].Value = dt_dowload.Columns[col].ColumnName;
                }

                // Ghi dữ liệu
                //for (int row = 0; row < dt_dowload.Rows.Count; row++)
                //{
                //    for (int col = 0; col < dt_dowload.Columns.Count; col++)
                //    {
                //        ws.Cells[row + 2, col + 1].Value = dt_dowload.Rows[row][col];
                //    }
                //}
                for (int row = 0; row < dt_dowload.Rows.Count; row++)
                {
                    for (int col = 0; col < dt_dowload.Columns.Count; col++)
                    {
                        var cell = ws.Cells[row + 2, col + 1];
                        var value = dt_dowload.Rows[row][col];

                        if (value is DateTime)
                        {
                            cell.Value = ((DateTime)value);
                            //cell.Style.Numberformat.Format = "dd/MM/yyyy";  // hoặc "yyyy-MM-dd"
                            cell.Style.Numberformat.Format = "MM/dd/yyyy";  // hoặc "yyyy-MM-dd"
                        }
                        else
                        {
                            cell.Value = value;
                        }
                    }
                }

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Baocao_Vessel.xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }

        }

        //so sanh tuong quan giua 2 ngay trong tuan
        public static bool IsDifferentWeek(DayOfWeek? day1, DayOfWeek? day1b)
        {
            return (int)day1 >= (int)day1b;
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

                            string Sheet = sheetName.Replace("$","");
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

                            DateTime ngayATP;


                            if (Category_ == "DECT")
                            {
                                for (int i = 0; i < dtExcelData.Rows.Count; i++)
                                {
                                    countlap = 0;

                                    Model = dtExcelData.Rows[i]["produce_model"].ToString().Trim();
                                    Cat = dtExcelData.Rows[i]["Category"].ToString();
                                    Shipmode = dtExcelData.Rows[i]["s/a"].ToString().Trim();
                                    Consignee = dtExcelData.Rows[i]["name"].ToString().Trim();

                                    //kiem tra xem tren csdl co chua? chua co thi moi them

                                    dt_getmodel = DataConn.StoreFillDS("Get_infor_mater_model", System.Data.CommandType.StoredProcedure, Model, Cat);
                                    if (dt_getmodel.Rows[0][0].ToString() == "1")
                                    {
                                        Country = dt_getmodel.Rows[0][1].ToString();            //lay tu mater model
                                        Destination = dt_getmodel.Rows[0][2].ToString();        //lay tu mater model
                                        Volume = dt_getmodel.Rows[0][3].ToString();             //lay tu mater model
                                        Grossweight = dt_getmodel.Rows[0][4].ToString();       //lay tu mater model
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
                                    TTLcont = RoundUpDiv(TTLVolume, 53);   //Roundup(L9/53,0)
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

                            string sqlConnStr = "Data Source=10.92.186.30;Persist Security Info=False;" +
                                "Initial Catalog=PC_Inventory_Infra;User Id=sa;Password=Psnvdb2013;" +
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
                        lblConfirm.Text = ex.Message;
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