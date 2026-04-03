using System;
using System.Collections.Generic;
using System.Configuration;
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

        //code new
        protected void ImportFromExcel(object sender, EventArgs e)
        {
            if (!FileUpload.HasFile || FileUpload.PostedFile.ContentLength == 0)
                return;

            string excelPath = Server.MapPath("~/") + FileUpload.FileName;
            FileUpload.SaveAs(excelPath);

            string excelConnStr =
                @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath +
                @";Extended Properties=""Excel 12.0;HDR=YES;IMEX=1"";";

            OleDbConnection excelConn = null;
            OleDbDataReader reader = null;

            try
            {
                // ===== READ EXCEL =====
                excelConn = new OleDbConnection(excelConnStr);
                excelConn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", excelConn);
                reader = cmd.ExecuteReader();

                DataTable dtExcel = new DataTable();
                dtExcel.Load(reader);

                // ===== DATATABLE BULK =====
                DataTable dtBulk = new DataTable();
                dtBulk.Columns.Add("Material", typeof(string));
                dtBulk.Columns.Add("EnglishName", typeof(string));
                dtBulk.Columns.Add("VietNameseName", typeof(string));
                dtBulk.Columns.Add("CreatedDate", typeof(DateTime));
                dtBulk.Columns.Add("CreatedId", typeof(string));
                dtBulk.Columns.Add("UpdatedDate", typeof(DateTime));
                dtBulk.Columns.Add("UpdatedId", typeof(string));
                dtBulk.Columns.Add("Unit", typeof(string));
                dtBulk.Columns.Add("UnitEcus", typeof(string));
                dtBulk.Columns.Add("Flag", typeof(int));

                //string user = Session["UserName"].ToString();
                string user = "2012757";

                //kiem tra tach truong hop update de
                bool check_mater_new = chkmater_new.Checked;
                if (check_mater_new)
                {
                    // Đổ dữ liệu từ Excel vào dtBulk (giống đoạn else)
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        if (string.IsNullOrWhiteSpace(dtExcel.Rows[i]["Material"]?.ToString()))
                            break;

                        DataRow dr = dtBulk.NewRow();
                        dr["Material"] = dtExcel.Rows[i]["Material"]?.ToString() ?? "";
                        dr["EnglishName"] = dtExcel.Rows[i]["EnglishName"]?.ToString() ?? "";
                        dr["VietNameseName"] = dtExcel.Rows[i]["VietNameseName"]?.ToString() ?? "";
                        dr["CreatedDate"] = DateTime.Now;
                        dr["CreatedId"] = user;
                        dr["UpdatedDate"] = DateTime.Now;
                        dr["UpdatedId"] = user;
                        dr["Unit"] = DBNull.Value;
                        dr["UnitEcus"] = DBNull.Value;
                        dr["Flag"] = 0;

                        dtBulk.Rows.Add(dr);
                    }

                    using (SqlConnection conn = new SqlConnection(DataConn.source2))
                    {
                        conn.Open();

                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {
                                // 1️ Tạo bảng tạm
                                using (SqlCommand cmdCreateTemp = new SqlCommand(@"
                    CREATE TABLE #TempMaterial (
                        Material NVARCHAR(50),
                        EnglishName NVARCHAR(255),
                        VietNameseName NVARCHAR(255),
                        CreatedDate DATETIME,
                        CreatedId NVARCHAR(50),
                        UpdatedDate DATETIME,
                        UpdatedId NVARCHAR(50),
                        Unit NVARCHAR(50),
                        UnitEcus NVARCHAR(50),
                        Flag INT
                    )", conn, tran))
                                {
                                    cmdCreateTemp.ExecuteNonQuery();
                                }

                                // 2️ Bulk vào bảng tạm
                                using (SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
                                {
                                    bulk.DestinationTableName = "#TempMaterial";

                                    bulk.ColumnMappings.Add("Material", "Material");
                                    bulk.ColumnMappings.Add("EnglishName", "EnglishName");
                                    bulk.ColumnMappings.Add("VietNameseName", "VietNameseName");
                                    bulk.ColumnMappings.Add("CreatedDate", "CreatedDate");
                                    bulk.ColumnMappings.Add("CreatedId", "CreatedId");
                                    bulk.ColumnMappings.Add("UpdatedDate", "UpdatedDate");
                                    bulk.ColumnMappings.Add("UpdatedId", "UpdatedId");
                                    bulk.ColumnMappings.Add("Unit", "Unit");
                                    bulk.ColumnMappings.Add("UnitEcus", "UnitEcus");
                                    bulk.ColumnMappings.Add("Flag", "Flag");

                                    bulk.WriteToServer(dtBulk);
                                }

                                // 3️ MERGE: update nếu tồn tại, insert nếu chưa có
                                using (SqlCommand cmdMerge = new SqlCommand(@"
                    MERGE dbo.MaterialNames AS target
                    USING #TempMaterial AS source
                    ON target.Material = source.Material

                    WHEN MATCHED THEN
                        UPDATE SET
                            target.EnglishName = source.EnglishName,
                            target.VietNameseName = source.VietNameseName,
                            target.UpdatedDate = source.UpdatedDate,
                            target.UpdatedId = source.UpdatedId,
                            target.Unit = source.Unit,
                            target.UnitEcus = source.UnitEcus,
                            target.Flag = source.Flag

                    WHEN NOT MATCHED THEN
                        INSERT (Material, EnglishName, VietNameseName, CreatedDate, CreatedId, UpdatedDate, UpdatedId, Unit, UnitEcus, Flag)
                        VALUES (source.Material, source.EnglishName, source.VietNameseName, source.CreatedDate, source.CreatedId, source.UpdatedDate, source.UpdatedId, source.Unit, source.UnitEcus, source.Flag);
                ", conn, tran))
                                {
                                    cmdMerge.ExecuteNonQuery();
                                }

                                tran.Commit();
                            }
                            catch
                            {
                                tran.Rollback();
                                throw;
                            }
                        }
                    }
                }
                else 
                {
                    //insert moi hoan toan
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        // dừng khi dòng rỗng
                        if (string.IsNullOrWhiteSpace(dtExcel.Rows[i]["Material"]?.ToString()))
                            break;

                        DataRow dr = dtBulk.NewRow();
                        dr["Material"] = dtExcel.Rows[i]["Material"]?.ToString() ?? "";
                        dr["EnglishName"] = dtExcel.Rows[i]["EnglishName"]?.ToString() ?? "";
                        dr["VietNameseName"] = dtExcel.Rows[i]["VietNameseName"]?.ToString() ?? "";
                        dr["CreatedDate"] = DateTime.Now;
                        dr["CreatedId"] = user;
                        dr["UpdatedDate"] = DBNull.Value;
                        dr["UpdatedId"] = DBNull.Value;
                        dr["Unit"] = DBNull.Value;
                        dr["UnitEcus"] = DBNull.Value;
                        dr["Flag"] = 0;

                        dtBulk.Rows.Add(dr);
                    }

                    // ===== SQL BULK + TRANSACTION =====
                    //string ketnoi = "Data Source=192.168.128.1;Initial Catalog=ScrapSystem;User ID=sa;Password=Psnvdb2013";                                   
                    //using (SqlConnection conn = new SqlConnection(ketnoi))
                    using (SqlConnection conn = new SqlConnection(DataConn.source2))
                    {
                        conn.Open();

                        // BẮT BUỘC: khai báo transaction ở đây
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {
                                //1 XÓA DỮ LIỆU CŨ
                                using (SqlCommand cmdTruncate = new SqlCommand(
                                    "TRUNCATE TABLE dbo.MaterialNames", conn, tran))
                                {
                                    cmdTruncate.ExecuteNonQuery();
                                }

                                // 2️ BULK INSERT
                                using (SqlBulkCopy bulk =
                                    new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
                                {
                                    bulk.DestinationTableName = "dbo.MaterialNames";

                                    bulk.ColumnMappings.Add("Material", "Material");
                                    bulk.ColumnMappings.Add("EnglishName", "EnglishName");
                                    bulk.ColumnMappings.Add("VietNameseName", "VietNameseName");
                                    bulk.ColumnMappings.Add("CreatedDate", "CreatedDate");
                                    bulk.ColumnMappings.Add("CreatedId", "CreatedId");
                                    bulk.ColumnMappings.Add("UpdatedDate", "UpdatedDate");
                                    bulk.ColumnMappings.Add("UpdatedId", "UpdatedId");
                                    bulk.ColumnMappings.Add("Unit", "Unit");
                                    bulk.ColumnMappings.Add("UnitEcus", "UnitEcus");
                                    bulk.ColumnMappings.Add("Flag", "Flag");

                                    bulk.BatchSize = 5000;
                                    bulk.BulkCopyTimeout = 0;

                                    bulk.WriteToServer(dtBulk);
                                }

                                // ✅ OK → commit
                                tran.Commit();
                            }
                            catch
                            {
                                // ❌ lỗi → rollback
                                tran.Rollback();
                                throw;
                            }
                        }
                    }
                }
                // thông báo thành công
                lblConfirm.Text = "IMPORT THÀNH CÔNG: " + dtBulk.Rows.Count + " dòng";
                lblConfirm.Attributes.Add("style", "color:green");
                dt_plan = DataConn.StoreFillDS2("Select_Mater_MaterialName", System.Data.CommandType.StoredProcedure);


            }
            catch (Exception ex)
            {
                lblConfirm.Text = ex.Message;
                lblConfirm.Attributes.Add("style", "color:red");
            }
            finally
            {
                if (reader != null) reader.Close();
                if (excelConn != null) excelConn.Close();
                if (File.Exists(excelPath)) File.Delete(excelPath);
            }
        }


        //code old
        protected void ImportFromExcel123(object sender, EventArgs e)
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

                            //string sqlConnStr = "Data Source=10.92.186.30;Persist Security Info=False;" +
                            //    "Initial Catalog=ScrapSystem;User Id=sa;Password=Psnvdb2013;" +
                            //    "Connect Timeout=30;";

                            string sqlConnStr = DataConn.source2;

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