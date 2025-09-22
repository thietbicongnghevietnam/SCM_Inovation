using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreeLayout.App_Code;

namespace FreeLayout
{
    public partial class frmMaterialName : System.Web.UI.Page
    {
        public DataTable dt_plan = new DataTable();
        public DataTable dt_getmodel = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt_plan = DataConn.StoreFillDS2("Select_Mater_MaterialName", System.Data.CommandType.StoredProcedure);
                //Date1.Value = DateTime.Now.ToString("dd-MM-yyyy");
                //ngaychiid.Value = DateTime.Now.ToString("dd-MM-yyyy");
                //dtcate = DataConnScrap.StoreFillDS("pro_get_categogy", System.Data.CommandType.StoredProcedure);
                //DataRow newRow1 = dtcate.NewRow();
                //newRow1["Description"] = "==Categogy==";
                //dtcate.Rows.InsertAt(newRow1, 0);
                //dr_filter_Cate.DataSource = dtcate;
                //dr_filter_Cate.DataBind();
            }
        }

        public void themhanghoa(object sender, EventArgs e)
        {
            //string id = IDedit.Text;
            string material = Mateialid.Text;
            string EnglishName = EnglishNameid.Text;
            string VietNameseName = VietNameseNameid.Text;

            ////string userid = Session["username"].ToString();

            DataTable dtinsert = new DataTable();
            dtinsert = DataConn.StoreFillDS2("Insert_mater_Materialname", System.Data.CommandType.StoredProcedure, material, EnglishName, VietNameseName);
            if (dtinsert.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS2("Select_Mater_MaterialName", System.Data.CommandType.StoredProcedure);
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
            string material = idMaterial.Text;
            string EnglishName = idEnglishName.Text;
            string VietNameseName = idVietNameseName.Text;

            //string userid = Session["username"].ToString();

            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS2("Update_mater_materialname", System.Data.CommandType.StoredProcedure, id, material, EnglishName, VietNameseName);

            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS2("Select_Mater_MaterialName_loc", System.Data.CommandType.StoredProcedure, material);
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
            string material = txMaterialName_del.Text;

            ////string username = Session["username"].ToString();
            ////string role_ = Session["role"].ToString();

            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS2("Delete_mater_MaterialName", System.Data.CommandType.StoredProcedure, id);  //username
            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS2("Select_Mater_MaterialName", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, kiểm tra lại thông tin!'); ", true);
            }


        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string filterMaterialid = filterMaterial.Value;

            //string category = dr_filter_Cate.SelectedValue;

            dt_plan = DataConn.StoreFillDS2("Select_Mater_MaterialName_loc", System.Data.CommandType.StoredProcedure, filterMaterialid);

        }

        protected void btnDownloadClick(Object sender, EventArgs e)
        {
            try
            {
                string fileName = "mau upload MaterialName.xlsx";
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
                        dt_new.Columns.Add("Id", typeof(Int32));
                        dt_new.Columns.Add("Material", typeof(String));
                        dt_new.Columns.Add("EnglishName", typeof(String));
                        dt_new.Columns.Add("VietNameseName", typeof(String));

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

                            string Material = "";
                            string EnglishName = "";
                            string VietNameseName = "";
                            for (int i = 0; i < dtExcelData.Rows.Count; i++) 
                            {
                                Material = dtExcelData.Rows[i]["Material"].ToString();
                                EnglishName = dtExcelData.Rows[i]["EnglishName"].ToString();
                                VietNameseName = dtExcelData.Rows[i]["VietNameseName"].ToString();
                                //check trung ban ghi
                                dt_getmodel = DataConn.StoreFillDS2("Check_trung_materialName", System.Data.CommandType.StoredProcedure, Material, EnglishName);
                                if (dt_getmodel.Rows[0][0].ToString() == "1")
                                {
                                    //da ton tai roi
                                    //nothing
                                    countlap = countlap + 1;
                                }
                                else
                                {
                                    //insert model moi
                                    dt_new.Rows.Add(i, Material, EnglishName, VietNameseName);
                                }

                                // Dừng vòng lặp khi các cột cần kiểm tra (cột 0, 2, 3) đều rỗng
                                if (dtExcelData.Rows[i][0].ToString() == "" && dtExcelData.Rows[i][1].ToString() == "" && dtExcelData.Rows[i][2].ToString() == "")
                                {
                                    break;
                                }
                            }

                            string sqlConnStr = "Data Source=10.92.186.30;Persist Security Info=False;" +
                                "Initial Catalog=ScrapSystem;User Id=sa;Password=Psnvdb2013;" +
                                "Connect Timeout=30;";

                            using (SqlConnection con = new SqlConnection(sqlConnStr))
                            {
                                con.Open();

                                // Initialize SqlBulkCopy.
                                using (SqlBulkCopy oSqlBulk = new SqlBulkCopy(con))
                                {
                                    oSqlBulk.DestinationTableName = "MaterialNames"; // Table name in database.                                                                                       
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
                            dt_plan = DataConn.StoreFillDS2("Select_Mater_MaterialName", System.Data.CommandType.StoredProcedure);

                        }

                    }
                    catch (Exception ex)
                    {

                        throw ex;
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


    }
}