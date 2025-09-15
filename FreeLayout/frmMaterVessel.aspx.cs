using FreeLayout.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreeLayout
{
    public partial class frmMaterVessel : System.Web.UI.Page
    {
        public DataTable dt_plan = new DataTable();
        public DataTable dt_getmodel = new DataTable();
        public DataTable dt_getvessel = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);
                //Date1.Value = DateTime.Now.ToString("dd-MM-yyyy");
                //ngaychiid.Value = DateTime.Now.ToString("dd-MM-yyyy");
                dtcate = DataConn.StoreFillDS("pro_get_categogy", System.Data.CommandType.StoredProcedure);
                DataRow newRow1 = dtcate.NewRow();
                newRow1["Description"] = "==Categogy==";
                dtcate.Rows.InsertAt(newRow1, 0);
                dr_filter_Cate.DataSource = dtcate;
                dr_filter_Cate.DataBind();
            }
        }

        public void themhanghoa(object sender, EventArgs e)
        {
            string cate = cateid.Text;
            string Area = Areaid.Text;
            string Country = Countryid.Text;
            string DestCity = DestCityid.Text;
            string DestCityName = DestCityNameid.Text;
            string PIC = PICid.Text;
            string Consignee = Consigneeid.Text;
            string FCL_Ex_factory = FCL_Ex_factoryid.Text;
            string FCL_ETD = FCL_ETDid.Text;
            string FCL_ETA = FCL_ETAid.Text;
            string LLC_Ex_factory = LLC_Ex_factoryid.Text;
            string LLC_ETD = LLC_ETDid.Text;
            string LLC_ETA = LLC_ETAid.Text;
            string AIR_Ex_factory = AIR_Ex_factoryid.Text;
            string AIR_ETD = AIR_ETDid.Text;
            string AIR_ETA = AIR_ETAid.Text;
            string Special_exfactory_date = Special_exfactory_dateid.Text;
            string SpecialETD_week = SpecialETD_weekid.Text;
            string Special_ETA_Date = Special_ETA_Dateid.Text;
            string Can_combine = Can_combineid.Text;
            //string userid = Session["username"].ToString();

            DataTable dtinsert = new DataTable();
            dtinsert = DataConn.StoreFillDS("Insert_materVessel_SCM", System.Data.CommandType.StoredProcedure, cate, Area, Country, DestCity, DestCityName, PIC, Consignee, FCL_Ex_factory, FCL_ETD, FCL_ETA, LLC_Ex_factory, LLC_ETD, LLC_ETA, AIR_Ex_factory, AIR_ETD, AIR_ETA, Special_exfactory_date, SpecialETD_week, Special_ETA_Date, Can_combine);
            if (dtinsert.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, kiểm tra lại thông tin!'); ", true);
            }

        }

        public void Updatethongtin(object sender, EventArgs e)
        {
            string id = IDedit.Text;
            string cate = idcate.Text;
            string Area = idArea.Text;
            string Country = idCountry.Text;
            string DestCity = idDestCity.Text;
            string DestCityName = idDestCityName.Text;
            string PIC = idPIC.Text;
            string Consignee = idConsignee.Text;
            string FCL_Ex_factory = idFCL_Ex_factory.Text;
            string FCL_ETD = idFCL_ETD.Text;
            string FCL_ETA = idFCL_ETA.Text;
            string LLC_Ex_factory = idLLC_Ex_factory.Text;
            string LLC_ETD = idLLC_ETD.Text;
            string LLC_ETA = idLLC_ETA.Text;
            string AIR_Ex_factory = idAIR_Ex_factory.Text;
            string AIR_ETD = idAIR_ETD.Text;
            string AIR_ETA = idAIR_ETA.Text;
            string Special_exfactory_date = idSpecial_exfactory_date.Text;
            string SpecialETD_week = idSpecialETD_week.Text;
            string Special_ETA_Date = idSpecial_ETA_Date.Text;
            string Can_combine = idCan_combine.Text;
            //string userid = Session["username"].ToString();

            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS("Update_materVessel_SCM", System.Data.CommandType.StoredProcedure, id, cate, Area, Country, DestCity, DestCityName, PIC, Consignee, FCL_Ex_factory, FCL_ETD, FCL_ETA, LLC_Ex_factory, LLC_ETD, LLC_ETA, AIR_Ex_factory, AIR_ETD, AIR_ETA, Special_exfactory_date, SpecialETD_week, Special_ETA_Date, Can_combine);

            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, kiểm tra lại thông tin!'); ", true);
            }
        }

        public void Xoathongtin(object sender, EventArgs e)
        {
            string id = txtid_del.Text;
            string model = txtCountry_del.Text;

            //string username = Session["username"].ToString();
            //string role_ = Session["role"].ToString();
            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS("Delete_materVessel_SCM", System.Data.CommandType.StoredProcedure, id); //username
            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, kiểm tra lại thông tin!'); ", true);
            }

            //if (role_ == "Admin")
            //{

            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban khong co quyen xoa!'); ", true);
            //}
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string category = dr_filter_Cate.SelectedValue;

            if (category == "==Categogy==")
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);
            }
            else
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM_cate", System.Data.CommandType.StoredProcedure, category);
            }

            //loc theo ngay
            //if (_fromdate == "" || _fromdate == "")
            //{               
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban nen chon ngay!!!'); ", true);
            //    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Ban nen chon ngay!');", true);
            //}
            //else
            //{
            //    if (category == "==Categogy==")
            //    {
            //        //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
            //        dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
            //    }
            //    else
            //    {
            //        dt_plan = DataConn.StoreFillDS("Select_Upload_VanningDate2_cate", System.Data.CommandType.StoredProcedure, _fromdate, _todate, category);
            //    }

            //}
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
                        dt_new.Columns.Add("cate", typeof(String));
                        dt_new.Columns.Add("Area", typeof(String));
                        dt_new.Columns.Add("Country", typeof(String));
                        dt_new.Columns.Add("DestCity", typeof(String));
                        dt_new.Columns.Add("DestCityName", typeof(String));
                        dt_new.Columns.Add("PIC", typeof(String));
                        dt_new.Columns.Add("Consignee", typeof(String));

                        dt_new.Columns.Add("FCL_Ex_factory", typeof(String));
                        dt_new.Columns.Add("FCL_ETD", typeof(String));
                        dt_new.Columns.Add("FCL_ETA", typeof(Int32));

                        dt_new.Columns.Add("LLC_Ex_factory", typeof(String));
                        dt_new.Columns.Add("LLC_ETD", typeof(String));
                        dt_new.Columns.Add("LLC_ETA", typeof(Int32));

                        dt_new.Columns.Add("AIR_Ex_factory", typeof(String));
                        dt_new.Columns.Add("AIR_ETD", typeof(String));
                        dt_new.Columns.Add("AIR_ETA", typeof(Int32));

                        dt_new.Columns.Add("Special_exfactory_date", typeof(Int32));
                        dt_new.Columns.Add("SpecialETD_week", typeof(Int32));
                        dt_new.Columns.Add("Special_ETA_Date", typeof(Int32));

                        dt_new.Columns.Add("Can_combine", typeof(String));

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

                        objBulkReader = objOleDB.ExecuteReader();

                        if (objBulkReader.HasRows)
                        {
                            DataTable dtExcelData = new DataTable();
                            dtExcelData.Load(objBulkReader); // Load data into DataTable.
                            string Sheet = sheetName.Replace("$", "");

                            string cate = "";
                            string Area = "";
                            string Country = "";
                            string DestCity = "";
                            string DestCityName = "";
                            string PIC = "";
                            string Consignee = "";

                            string FCL_Ex_factory = "";
                            string FCL_ETD = "";
                            int FCL_ETA = 0;

                            string LLC_Ex_factory = "";
                            string LLC_ETD = "";
                            int LLC_ETA = 0;

                            string AIR_Ex_factory = "";
                            string AIR_ETD = "";
                            int AIR_ETA = 0;

                            int Special_exfactory_date = 0;
                            int SpecialETD_week = 0;
                            int Special_ETA_Date = 0;
                            string Can_combine = "";

                            for (int i = 0; i < dtExcelData.Rows.Count; i++) 
                            {
                                countlap = 0;
                                cate = dtExcelData.Rows[i]["cate"].ToString();
                                Area = dtExcelData.Rows[i]["Area"].ToString();
                                Country = dtExcelData.Rows[i]["Country"].ToString();
                                DestCity = dtExcelData.Rows[i]["DestCity"].ToString();
                                DestCityName = dtExcelData.Rows[i]["DestCityName"].ToString();
                                PIC = dtExcelData.Rows[i]["PIC"].ToString();
                                Consignee = dtExcelData.Rows[i]["Consignee"].ToString();

                                FCL_Ex_factory = dtExcelData.Rows[i]["FCL_Ex_factory"].ToString();
                                FCL_ETD = dtExcelData.Rows[i]["FCL_ETD"].ToString();
                                //FCL_ETA = dtExcelData.Rows[i]["FCL_ETA"].ToString();
                                int.TryParse(dtExcelData.Rows[i]["FCL_ETA"].ToString(), out FCL_ETA);

                                LLC_Ex_factory = dtExcelData.Rows[i]["LLC_Ex_factory"].ToString();
                                LLC_ETD = dtExcelData.Rows[i]["LLC_ETD"].ToString();
                                //LLC_ETA = dtExcelData.Rows[i]["LLC_ETA"].ToString();
                                int.TryParse(dtExcelData.Rows[i]["LLC_ETA"].ToString(), out LLC_ETA);

                                AIR_Ex_factory = dtExcelData.Rows[i]["AIR_Ex_factory"].ToString();
                                AIR_ETD = dtExcelData.Rows[i]["AIR_ETD"].ToString();
                                //AIR_ETA = dtExcelData.Rows[i]["AIR_ETA"].ToString();
                                int.TryParse(dtExcelData.Rows[i]["AIR_ETA"].ToString(), out AIR_ETA);

                                //Special_exfactory_date = dtExcelData.Rows[i]["Special_exfactory_date"].ToString();
                                int.TryParse(dtExcelData.Rows[i]["Special_exfactory_date"].ToString(), out Special_exfactory_date);
                                //SpecialETD_week = dtExcelData.Rows[i]["SpecialETD_week"].ToString();
                                int.TryParse(dtExcelData.Rows[i]["SpecialETD_week"].ToString(), out SpecialETD_week);
                                //Special_ETA_Date = dtExcelData.Rows[i]["Special_ETA_Date"].ToString();
                                int.TryParse(dtExcelData.Rows[i]["Special_ETA_Date"].ToString(), out Special_ETA_Date);
                                Can_combine = dtExcelData.Rows[i]["Can_combine"].ToString();

                                dt_getvessel = DataConn.StoreFillDS("Get_infor_mater_vessel", System.Data.CommandType.StoredProcedure, cate, Country, DestCity, Consignee);
                                if (dt_getvessel.Rows[0][0].ToString() == "1")
                                {
                                    //da ton tai roi
                                    //nothing
                                    countlap = countlap + 1;
                                }
                                else
                                {
                                    //insert model moi
                                    dt_new.Rows.Add(i, cate, Area, Country, DestCity, DestCityName, PIC, Consignee, FCL_Ex_factory, FCL_ETD, FCL_ETA, LLC_Ex_factory, LLC_ETD, LLC_ETA, AIR_Ex_factory, AIR_ETD, AIR_ETA, Special_exfactory_date, SpecialETD_week, Special_ETA_Date, Can_combine);
                                }

                                // Dừng vòng lặp khi các cột cần kiểm tra (cột 0, 2, 3) đều rỗng
                                if (dtExcelData.Rows[i][0].ToString() == "" && dtExcelData.Rows[i][1].ToString() == "" && dtExcelData.Rows[i][2].ToString() == "")
                                {
                                    break;
                                }
                            }

                            string sqlConnStr = "Data Source=10.92.186.30;Persist Security Info=False;" +
                                "Initial Catalog=PC_Inventory_Infra;User Id=sa;Password=Psnvdb2013;" +
                                "Connect Timeout=30;";

                            using (SqlConnection con = new SqlConnection(sqlConnStr))
                            {
                                con.Open();

                                // Initialize SqlBulkCopy.
                                using (SqlBulkCopy oSqlBulk = new SqlBulkCopy(con))
                                {
                                    oSqlBulk.DestinationTableName = "tblMaster_vessel"; // Table name in database.
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

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('OK, Upload thành công!');", true);
                            dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);

                        }

                    }
                    catch (Exception ex)
                    {
                        lblConfirm.Text = "Lỗi : " + ex.Message;
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

        protected void btnDownloadClick(Object sender, EventArgs e)
        {
            try
            {
                string fileName = "Mau upload vessel.xlsx";
                string fileExtension = ".xlsx";

                // Set Response.ContentType
                Response.ContentType = GetContentType(fileExtension);

                // Append header
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

                // Write the file to the Response
                Response.TransmitFile(Server.MapPath("~/Textfile/" + fileName));
                //Response.TransmitFile(Server.MapPath("~/Uploads/" + fileName));
                Response.End();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetContentType(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return string.Empty;

            string contentType = string.Empty;
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                    contentType = "text/HTML";
                    break;
                case ".csv":
                case ".txt":
                    contentType = "text/plain";
                    break;

                case ".doc":
                case ".rtf":
                case ".docx":
                    contentType = "Application/msword";
                    break;

                case ".xls":
                case ".xlsx":
                    contentType = "Application/x-msexcel";
                    break;

                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;

                case ".gif":
                    contentType = "image/GIF";
                    break;

                case ".pdf":
                    contentType = "application/pdf";
                    break;
            }
            return contentType;
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



    }
}