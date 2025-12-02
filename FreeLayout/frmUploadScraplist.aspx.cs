using FreeLayout.App_Code;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreeLayout
{
    public partial class frmUploadScraplist : System.Web.UI.Page
    {
        public DataTable dt_plan = new DataTable();
        public DataTable dt_checkupload = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dt_update = new DataTable();
        public DataTable dtsanction = new DataTable();
        public DataTable dtIssueOut = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Date1.Value = DateTime.Now.ToString("yyyy-MM-dd");
                ngaychiid.Value = DateTime.Now.ToString("yyyy-MM-dd");

                dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList", System.Data.CommandType.StoredProcedure);
                //Date1.Value = DateTime.Now.ToString("dd-MM-yyyy");
                //ngaychiid.Value = DateTime.Now.ToString("dd-MM-yyyy");
                string _fromdate = Date1.Value;
                string _todate = ngaychiid.Value;

                //danh sach bo phan
                dtcate = DataConn.StoreFillDS2("pro_get_categogy", System.Data.CommandType.StoredProcedure);
                DataRow newRow1 = dtcate.NewRow();
                newRow1["Description"] = "==Section==";
                dtcate.Rows.InsertAt(newRow1, 0);
                dr_filter_Cate.DataSource = dtcate;
                dr_filter_Cate.DataBind();

                dtsanction = DataConn.StoreFillDS2("pro_get_section2", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
                DataRow newRow2 = dtsanction.NewRow();
                newRow2["Sanction"] = "==Sanction==";
                dtsanction.Rows.InsertAt(newRow2, 0);
                dr_filter_Sanction.DataSource = dtsanction;
                dr_filter_Sanction.DataBind();
                         
            }
        }

        protected void dr_filter_Sanction_SelectedIndexChanged(object sender, EventArgs e)
        {           
            string tensanction = dr_filter_Sanction.SelectedValue.ToString();
            string _fromdate = Date1.Value;
            string _todate = ngaychiid.Value;
            string bophan = dr_filter_Cate.SelectedValue;

            dtIssueOut = DataConn.StoreFillDS2("pro_get_section3", System.Data.CommandType.StoredProcedure, _fromdate, _todate, tensanction);

            // XÓA TRƯỚC KHI BIND
            dr_filter_IssueOut.Items.Clear();

            if (dtIssueOut.Rows.Count > 0)
            {
                DataRow newRow3 = dtIssueOut.NewRow();
                newRow3["TypeName"] = "==IssueOut==";
                dtIssueOut.Rows.InsertAt(newRow3, 0);
                dr_filter_IssueOut.DataSource = dtIssueOut;
                dr_filter_IssueOut.DataBind();
            }

            if (bophan == "==Section==")
            {
                dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion", System.Data.CommandType.StoredProcedure, tensanction, _fromdate, _todate);
            }
            else
            {
                dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion2", System.Data.CommandType.StoredProcedure, bophan, tensanction, _fromdate, _todate);
            }
            //dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion2", System.Data.CommandType.StoredProcedure, bophan, tensanction, _fromdate, _todate);           
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string bophan = dr_filter_Cate.SelectedValue;
            string sacnctionid = dr_filter_Sanction.SelectedValue;// filterSanction.Value;
            //loc theo ngay
            if (_fromdate == "" || _todate == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban nen chon ngay!!!'); ", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Ban nen chon ngay!');", true);
            }
            else
            {
                if (bophan == "==Section==")
                {
                    dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion", System.Data.CommandType.StoredProcedure, sacnctionid, _fromdate, _todate);
                }
                else
                {
                    dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion2", System.Data.CommandType.StoredProcedure, bophan, sacnctionid, _fromdate, _todate);
                }
            }
        }

        public void Export_IssueOut(object sender, EventArgs e)
        {

            DataTable dt_dowload = new DataTable();
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string _sanction = dr_filter_Sanction.SelectedValue;// filterSanction.Value.ToString();
            string _issueout = dr_filter_IssueOut.SelectedValue;// filterIssueout.Value.ToString();


            if (_issueout == "==IssueOut==" || _sanction == "==Sanction==")
            {
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Save data thanh cong!');", true);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Du lieu Sanction or Issue out null!'); ", true);
            }
            else 
            {
                //truong hop export detail  (normal)
                string relativePath = "Mau_IssueOutB.xlsx";
                string localPath = Server.MapPath(relativePath);

                // Đường dẫn để lưu file Excel mới
                string newFileName = "IssueOutB.xlsx"; // Tên file mới
                string newFilePath = Server.MapPath("Textfile/" + newFileName); // Đường dẫn đầy đủ

                // Gọi phương thức để xử lý file Excel và lưu file mới
                ProcessExcelFile(localPath, newFilePath, _fromdate, _todate, _sanction, _issueout);

                // Tải xuống file mới
                DownloadFile(newFilePath, newFileName);
            }            
        }

        public void Export_FA_PE(object sender, EventArgs e)
        {

            DataTable dt_dowload = new DataTable();
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string _sanction = dr_filter_Sanction.SelectedValue;// filterSanction.Value.ToString();
            string _issueout = dr_filter_IssueOut.SelectedValue;// filterIssueout.Value.ToString();


            if (_sanction == "==Sanction==")   //_issueout == "==IssueOut==" ||
            {
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Save data thanh cong!');", true);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Du lieu Sanction or Issue out null!'); ", true);
            }
            else
            {
                //truong hop export detail  (normal)
                string relativePath = "Mau_DispositionProperty.xlsx";
                string localPath = Server.MapPath(relativePath);

                // Đường dẫn để lưu file Excel mới
                string newFileName = "DispositionProperty.xlsx"; // Tên file mới
                string newFilePath = Server.MapPath("Textfile/" + newFileName); // Đường dẫn đầy đủ

                // Gọi phương thức để xử lý file Excel và lưu file mới
                ProcessExcelFile1(localPath, newFilePath, _fromdate, _todate, _sanction, _issueout);

                // Tải xuống file mới
                DownloadFile(newFilePath, newFileName);
            }
        }

        public void Confirm_Issue_Out(object sender, EventArgs e)
        {
            DataTable dt_dowload = new DataTable();
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string _sanction = dr_filter_Sanction.SelectedValue;// filterSanction.Value.ToString();
            string _issueout = dr_filter_IssueOut.SelectedValue;// filterIssueout.Value.ToString();
            string bophan = dr_filter_Cate.SelectedValue.ToString();


            if (_sanction == "==Sanction==" && bophan == "==Section==")   //_issueout == "==IssueOut==" ||
            {
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Save data thanh cong!');", true);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Du lieu Sanction or section null !'); ", true);
            }
            else 
            {
                try
                {

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }

        static void ProcessExcelFile1(string filePath, string newFilePath, string tungay, string denngay, string sanction, string issueout) 
        {
            FileInfo fileInfo = new FileInfo(filePath);

            // Đảm bảo file tồn tại
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File không tồn tại", filePath);
            }
            FileInfo newFileInfo = new FileInfo(newFilePath);
            try
            {
                DataTable dt_total = new DataTable();
                DataTable dt_all = new DataTable();
                DataTable dt_account = new DataTable();
                dt_all = DataConn.StoreFillDS2("Export_Mau_FA_PE", System.Data.CommandType.StoredProcedure, tungay, denngay, sanction, issueout);
                if (dt_all.Rows.Count > 0)
                {
                    DataTable dtexcel = new DataTable();

                    using (var package = new ExcelPackage(fileInfo))
                    {
                        //var worksheet = package.Workbook.Worksheets["Sheet1"];
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        //worksheet.Cells["D5"].Value = tungay;// "Thông tin mới";

                        if (worksheet == null)
                        {
                            throw new Exception("Không tìm thấy sheet 'Sheet1' trong file Excel.");
                        }

                        int row = 6;
                        int i = 0;
                        DateTime currentDate = DateTime.Today;
                        // zen du lieu vao file excel
                        //string[] parts = issueout.Split('.');   //"7.Claim scrap deadstock";

                        //worksheet.Cells[2, 1].Value = parts[1];     //ten issue out  = Claim scrap deadstock
                        worksheet.Cells[4, 20].Value = currentDate; //ngay xuat issue out
                        //worksheet.Cells[4, 4].Style.Numberformat.Format = "dd/MM/yyyy";

                        //worksheet.Cells[6, 4].Value = dt_all.Rows[0]["MVT"].ToString();  //Mv.type
                        //worksheet.Cells[6, 6].Value = "Out";  //Mv.type
                      
                        //foreach (DataRow dataRow in dtexcel.Rows)
                        foreach (DataRow dataRow in dt_all.Rows)
                        {
                            i++;
                            worksheet.Cells[row, 1].Value = i; // dataRow["id"]; //
                            worksheet.Cells[row, 2].Value = dataRow["ControlNo"];    //Control No
                            worksheet.Cells[row, 3].Value = dataRow["Category"];        //Category 
                            worksheet.Cells[row, 4].Value = "";  //pic
                            worksheet.Cells[row, 5].Value = ""; //import no
                            worksheet.Cells[row, 6].Value = "";    //name       ==> ten tieng anh lay trong mater name.// lay tu mater name  ***
                            worksheet.Cells[row, 7].Value = dataRow["Qty"];         //qty   
                            worksheet.Cells[row, 8].Value = "";   //Unit        ==> ten tieng anh lay trong mater name.// lay tu mater name  ***
                            worksheet.Cells[row, 9].Value = dataRow["Material"];      //Model /Partno

                            worksheet.Cells[row, 10].Value = dataRow["Vendor"];    //suplier
                            worksheet.Cells[row, 11].Value = dataRow["UnitPrice"];       //price
                            worksheet.Cells[row, 12].Value = dataRow["Amount"];         //total asset
                            worksheet.Cells[row, 12].Value = dataRow["BookValue"];         //book value
                            worksheet.Cells[row, 12].Value = dataRow["Currency"];         //currency
                            worksheet.Cells[row, 12].Value = dataRow["FaTool"];         //FA/Tool
                            worksheet.Cells[row, 12].Value = "";                        //INV No
                            worksheet.Cells[row, 12].Value = dataRow["SoTK"];         //CD No
                            worksheet.Cells[row, 12].Value = dataRow["NgayTK"];         //CD Date

                            worksheet.Cells[row, 12].Value = "";                     //CD's Item No
                            worksheet.Cells[row, 12].Value = dataRow["Reason"];         //Reson 
                            worksheet.Cells[row, 12].Value = dataRow["Pallet"];         //Palet No

                            row++;
                        }
                        //zen tong qty va Amount  => fix co dinh
                        //worksheet.Cells[53, 7].Value = dt_total.Rows[0][0].ToString();   //tong qty
                        //worksheet.Cells[53, 9].Value = dt_total.Rows[0][1].ToString();   //tong amountST
                        //worksheet.Cells[53, 11].Value = dt_total.Rows[0][2].ToString();   //tong amountAC

                        // Lưu vào file mới
                        package.SaveAs(newFileInfo);
                    }

                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon bo phan!'); ", true);                    
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        static void ProcessExcelFile(string filePath, string newFilePath, string tungay, string denngay, string sanction, string issueout)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            // Đảm bảo file tồn tại
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File không tồn tại", filePath);
            }

            // Tạo file mới để lưu kết quả
            FileInfo newFileInfo = new FileInfo(newFilePath);

            //tao bang tam de zen vao excel file
            //DataTable dt_new = new DataTable();
            //dt_new.Columns.Add("id", typeof(Int32));
            //dt_new.Columns.Add("Plan", typeof(String));
            //dt_new.Columns.Add("Sloc", typeof(String));
            //dt_new.Columns.Add("CostCenter", typeof(String));
            //dt_new.Columns.Add("Namecode", typeof(String));
            //dt_new.Columns.Add("Material", typeof(String));
            //dt_new.Columns.Add("Issueqty", typeof(String));
            //dt_new.Columns.Add("UnitpriceST", typeof(Int32));
            //dt_new.Columns.Add("AmountST", typeof(String));
            //dt_new.Columns.Add("UnitpriceAC", typeof(float));
            //dt_new.Columns.Add("AmountAC", typeof(String));
            //dt_new.Columns.Add("Remark", typeof(String));

            try
            {
                DataTable dt_total = new DataTable();
                DataTable dt_all = new DataTable();
                DataTable dt_account = new DataTable();
                dt_all = DataConn.StoreFillDS2("Export_IssueOut_Data", System.Data.CommandType.StoredProcedure, tungay, denngay, sanction, issueout);
                dt_total = DataConn.StoreFillDS2("Export_IssueOut_total", System.Data.CommandType.StoredProcedure, tungay, denngay, sanction, issueout);
                dt_account = DataConn.StoreFillDS2("Export_IssueOut_MVT", System.Data.CommandType.StoredProcedure, tungay, denngay, sanction, issueout);

                if (dt_all.Rows.Count > 0)
                {
                    DataTable dtexcel = new DataTable();

                    using (var package = new ExcelPackage(fileInfo))
                    {
                        //var worksheet = package.Workbook.Worksheets["Sheet1"];
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        //worksheet.Cells["D5"].Value = tungay;// "Thông tin mới";

                        if (worksheet == null)
                        {
                            throw new Exception("Không tìm thấy sheet 'Sheet1' trong file Excel.");
                        }

                        int row = 16;
                        int i = 0;
                        DateTime currentDate = DateTime.Today;
                        // zen du lieu vao file excel
                        string[] parts = issueout.Split('.');   //"7.Claim scrap deadstock";

                        worksheet.Cells[2, 1].Value = parts[1];     //ten issue out  = Claim scrap deadstock
                        worksheet.Cells[4, 4].Value = currentDate; //ngay xuat issue out
                        worksheet.Cells[4, 4].Style.Numberformat.Format = "dd/MM/yyyy";

                        worksheet.Cells[6, 4].Value = dt_all.Rows[0]["MVT"].ToString();  //Mv.type
                        worksheet.Cells[6, 6].Value = "Out";  //Mv.type

                        worksheet.Cells[7, 4].Value = dt_account.Rows[0][0].ToString();  //Account
                        worksheet.Cells[7, 6].Value = dt_account.Rows[0][1].ToString();  //Account name

                        worksheet.Cells[9, 4].Value = "vendor code";  //Vendor Code ??? link mater??  ???
                        worksheet.Cells[10, 4].Value = dt_all.Rows[0]["Vendor"].ToString();  //Vendor name



                        //foreach (DataRow dataRow in dtexcel.Rows)
                        foreach (DataRow dataRow in dt_all.Rows)
                        {
                            i++;
                            worksheet.Cells[row, 1].Value = i; // dataRow["id"]; //
                            worksheet.Cells[row, 2].Value = dataRow["Plant"];
                            worksheet.Cells[row, 3].Value = dataRow["Sloc"];
                            worksheet.Cells[row, 4].Value = dataRow["CostCenter"];
                            worksheet.Cells[row, 5].Value = dataRow["NameCost"]; //
                            worksheet.Cells[row, 6].Value = dataRow["Material"];
                            worksheet.Cells[row, 7].Value = dataRow["Qty"];
                            worksheet.Cells[row, 8].Value = dataRow["UnitPrice"];
                            worksheet.Cells[row, 9].Value = dataRow["Amount"];
                            worksheet.Cells[row, 10].Value = dataRow["UnitPriceAC"];
                            worksheet.Cells[row, 11].Value = dataRow["AmountAC"];
                            worksheet.Cells[row, 12].Value = dataRow["Reason"];                            

                            row++;
                        }


                        //zen tong qty va Amount  => fix co dinh
                        worksheet.Cells[53,7].Value = dt_total.Rows[0][0].ToString();   //tong qty
                        worksheet.Cells[53,9].Value = dt_total.Rows[0][1].ToString();   //tong amountST
                        worksheet.Cells[53,11].Value = dt_total.Rows[0][2].ToString();   //tong amountAC

                        // Lưu vào file mới
                        package.SaveAs(newFileInfo);
                    }

                }            
                else 
                {
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon bo phan!'); ", true);                    
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
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
        


        protected void ImportFromExcel_old(object sender, EventArgs e) 
        {
            //DataTable dtcheck = new DataTable();
            //string _fromdate = Request.Form[Date1.UniqueID];
            //string _todate = Request.Form[ngaychiid.UniqueID];

            //if (FileUpload.HasFile) 
            //{
            //    if (FileUpload.PostedFile.ContentLength > 0) 
            //    {
            //        // Save the uploaded file to the server.
            //        FileUpload.SaveAs(Server.MapPath(".") + "\\" + FileUpload.FileName);

            //        // Set connection string with the Excel file.
            //        //string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" +
            //        //                      Server.MapPath(".") + "\\" + FileUpload.FileName +
            //        //                      "; Extended Properties=Excel 12.0;"

            //        //new
            //        string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" +
            //          Server.MapPath(".") + "\\" + FileUpload.FileName +
            //          "; Extended Properties='Excel 12.0; HDR=YES; IMEX=1;'"; // HDR=YES để xử lý header, IMEX=1 để xử lý cả dữ liệu chuỗi và số

            //        OleDbConnection excelConn = null;
            //        OleDbDataReader objBulkReader = null;
            //        try
            //        {
            //            DataTable dt_checkupload = new DataTable();
            //            DataTable dt_new = new DataTable();
            //            int countlap = 0;

            //            dt_new.Columns.Add("ID", typeof(Int32));
            //            dt_new.Columns.Add("SanctionId", typeof(Int32));
            //            dt_new.Columns.Add("Material", typeof(String));
            //            dt_new.Columns.Add("Qty", typeof(float));
            //            dt_new.Columns.Add("QtyActual", typeof(float));
            //            dt_new.Columns.Add("UnitPrice", typeof(float));
            //            dt_new.Columns.Add("Amount", typeof(float));
            //            dt_new.Columns.Add("CostCenter", typeof(String));
            //            dt_new.Columns.Add("Reason", typeof(String));
            //            dt_new.Columns.Add("Plant", typeof(String));
            //            dt_new.Columns.Add("Sloc", typeof(String));
            //            dt_new.Columns.Add("NameCost", typeof(String));
            //            dt_new.Columns.Add("Pallet", typeof(String));
            //            dt_new.Columns.Add("Barcode", typeof(String));
            //            dt_new.Columns.Add("ScrapSloc", typeof(String));
            //            dt_new.Columns.Add("ControlNo", typeof(String));
            //            dt_new.Columns.Add("FaTool", typeof(String));
                        

            //            // Open connection to Excel file.
            //            excelConn = new OleDbConnection(excelConnStr);
            //            excelConn.Open();
            //            // Lấy danh sách các sheet trong Excel
            //            DataTable sheets = excelConn.GetSchema("Tables");
            //            // Lấy tên sheet đầu tiên (vì chỉ có một sheet)
            //            string sheetName = sheets.Rows[0]["TABLE_NAME"].ToString();
            //            Console.WriteLine("Tên sheet: " + sheetName);

            //            // Xử lý tên sheet (nếu có ký tự đặc biệt)
            //            string sanitizedSheetName = SanitizeSheetName(sheetName);
            //            // Tạo câu truy vấn SQL với tên sheet đã xử lý
            //            OleDbCommand objOleDB = new OleDbCommand($"SELECT * FROM [{sanitizedSheetName}$]", excelConn);

            //            objBulkReader = objOleDB.ExecuteReader();

            //            if (objBulkReader.HasRows)
            //            {
            //                DataTable dtExcelData = new DataTable();
            //                dtExcelData.Load(objBulkReader); // Load data into DataTable.
            //                string Sheet = sheetName.Replace("$", "");

            //                string bophan = "";
            //                if (dr_filter_Cate.Text != "==Section==") 
            //                {
            //                    bophan = dr_filter_Cate.Text;
            //                }

            //                int SanctionId = 0;

            //                string tensanction = "";

            //                string Material = "";
            //                float Qty = 0;
            //                float QtyActual = 0;
            //                float UnitPrice = 0;
            //                float Amount = 0;
            //                string CostCenter = "";
            //                string Reason = "";
            //                string Plant = "";
            //                string Sloc = "";                            
            //                string NameCost = "";
            //                string Pallet = "";                            
            //                string Barcode = "";
            //                string ScrapSloc = "";

            //                string ControlNo = "";
            //                string FaTool = "";

            //                string type_upload = "";

            //                //string test3 = dtExcelData.Rows[2][1].ToString();

            //                if (rblMCS.Checked == true)
            //                {
            //                    // format cua MCS
            //                    if (bophan == "")
            //                    {
            //                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon bo phan!'); ", true);
            //                    }
            //                    else
            //                    {
            //                        //lay ten sacntion
            //                        tensanction = dtExcelData.Rows[4][3].ToString();
            //                        if (tensanction == "")
            //                        {
            //                            //khong ton tai sanction 
            //                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Fomat input sai dinh dang!'); ", true);
            //                        }
            //                        else
            //                        {
            //                            //kiem tra sanction co trong danh sach chua de lay ra id sanction
            //                            DataTable dt_getidsacntion = new DataTable();
            //                            dt_getidsacntion = DataConn.StoreFillDS2("Get_idsanction", System.Data.CommandType.StoredProcedure, tensanction, bophan);

            //                            if (dt_getidsacntion.Rows[0][0].ToString() != "")
            //                            {
            //                                SanctionId = Int32.Parse(dt_getidsacntion.Rows[0][0].ToString());
            //                            }
            //                            //else 
            //                            //{
            //                            //    //nothing
            //                            //    //SanctionId = 0
            //                            //}

            //                            for (int i = 9; i < dtExcelData.Rows.Count; i++)
            //                            {
            //                                countlap = 0;
            //                                // check cac cot co du lieu va khong co du lieu
            //                                //mahang + Plant + issue sloc + scrap loc + st price
            //                                if (dtExcelData.Rows[i][1].ToString() != "" && dtExcelData.Rows[i][4].ToString() != "" && dtExcelData.Rows[i][5].ToString() != "" && dtExcelData.Rows[i][6].ToString() != "" && dtExcelData.Rows[i][7].ToString() != "")
            //                                {
            //                                    Material = dtExcelData.Rows[i][1].ToString();
            //                                    float.TryParse(dtExcelData.Rows[i][8].ToString(), out Qty);

            //                                    float.TryParse(dtExcelData.Rows[i][8].ToString(), out QtyActual);  //lay luon so actual tren nay => khong can up pallet list
            //                                                                                                       //QtyActual = 0;
            //                                    float.TryParse(dtExcelData.Rows[i][7].ToString(), out UnitPrice);
            //                                    float.TryParse(dtExcelData.Rows[i][10].ToString(), out Amount);
            //                                    CostCenter = "";
            //                                    Reason = dtExcelData.Rows[i][12].ToString();
            //                                    Plant = dtExcelData.Rows[i][4].ToString();
            //                                    Sloc = dtExcelData.Rows[i][5].ToString();
            //                                    NameCost = "";
            //                                    Pallet = dtExcelData.Rows[i][14].ToString();
            //                                    Barcode = tensanction + ";" + Pallet + ";" + bophan;

            //                                    ScrapSloc = dtExcelData.Rows[i][6].ToString();

            //                                    ControlNo = "";
            //                                    FaTool = "";

            //                                    //float.TryParse(dtExcelData.Rows[i]["Model_Vol"].ToString(), out Model_Vol);
            //                                    //int.TryParse(dtExcelData.Rows[i]["CTN_vol"].ToString(), out CTN_vol);

            //                                    type_upload = "MCS";

            //                                    dt_checkupload = DataConn.StoreFillDS2("Check_upload_scraplist", System.Data.CommandType.StoredProcedure, SanctionId, Material, Qty, Plant, Sloc, Pallet, ScrapSloc, type_upload, ControlNo, FaTool);
            //                                    if (dt_checkupload.Rows[0][0].ToString() == "1")
            //                                    {
            //                                        //da ton tai roi
            //                                        //nothing
            //                                        countlap = countlap + 1;
            //                                    }
            //                                    else
            //                                    {
            //                                        //insert model moi
            //                                        dt_new.Rows.Add(i, SanctionId, Material, Qty, QtyActual, UnitPrice, Amount, CostCenter, Reason, Plant, Sloc, NameCost, Pallet, Barcode, ScrapSloc, ControlNo, FaTool);
            //                                    }
            //                                }

            //                                //mahang + Plant + issue sloc + scrap loc + st price  ==> Tong scrap (10)
            //                                if (dtExcelData.Rows[i][1].ToString() == "" && dtExcelData.Rows[i][4].ToString() == "" && dtExcelData.Rows[i][5].ToString() == "" && dtExcelData.Rows[i][6].ToString() == "" && dtExcelData.Rows[i][7].ToString() == "" && dtExcelData.Rows[i][10].ToString() == "")
            //                                {
            //                                    break;
            //                                }
            //                            }
                                       
            //                        }
            //                    }
            //                }
            //                else if (rblOther.Checked == true)
            //                {
            //                    //format other  FA/Tool  fixed asset
            //                    if (bophan == "")
            //                    {
            //                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon bo phan!'); ", true);
            //                    }
            //                    else
            //                    {
            //                        //lay ten sacntion
            //                        tensanction = dtExcelData.Rows[3][20].ToString();
            //                        if (tensanction == "")
            //                        {
            //                            //khong ton tai sanction 
            //                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Fomat input sai dinh dang!'); ", true);
            //                        }
            //                        else
            //                        {
            //                            //kiem tra sanction co trong danh sach chua de lay ra id sanction
            //                            DataTable dt_getidsacntion = new DataTable();
            //                            dt_getidsacntion = DataConn.StoreFillDS2("Get_idsanction", System.Data.CommandType.StoredProcedure, tensanction, bophan);

            //                            if (dt_getidsacntion.Rows[0][0].ToString() != "")
            //                            {
            //                                SanctionId = Int32.Parse(dt_getidsacntion.Rows[0][0].ToString());
            //                            }

            //                            for (int i = 3; i < dtExcelData.Rows.Count; i++)
            //                            {
            //                                countlap = 0;
            //                                if (dtExcelData.Rows[i][8].ToString() != "" && dtExcelData.Rows[i][6].ToString() != "" && dtExcelData.Rows[i][7].ToString() != "" && dtExcelData.Rows[i][10].ToString() != "")
            //                                {
            //                                    Material = dtExcelData.Rows[i][8].ToString();
            //                                    float.TryParse(dtExcelData.Rows[i][6].ToString(), out Qty);
            //                                    float.TryParse(dtExcelData.Rows[i][6].ToString(), out QtyActual);

            //                                    float.TryParse(dtExcelData.Rows[i][10].ToString(), out UnitPrice);
            //                                    //float.TryParse(dtExcelData.Rows[i][10].ToString(), out Amount);
            //                                    Amount = 0;
            //                                    CostCenter = "";
            //                                    Reason = dtExcelData.Rows[i][18].ToString();
            //                                    Plant = "";
            //                                    Sloc = "";
            //                                    NameCost = "";
            //                                    Pallet = dtExcelData.Rows[i][19].ToString();
            //                                    Barcode = tensanction + ";" + Pallet + ";" + bophan;

            //                                    ScrapSloc = "";
            //                                    type_upload = "other";

            //                                    ControlNo = dtExcelData.Rows[i][1].ToString();
            //                                    FaTool = dtExcelData.Rows[i][13].ToString();

            //                                    dt_checkupload = DataConn.StoreFillDS2("Check_upload_scraplist", System.Data.CommandType.StoredProcedure, SanctionId, Material, Qty, Plant, Sloc, Pallet, ScrapSloc, type_upload, ControlNo, FaTool);
            //                                    if (dt_checkupload.Rows[0][0].ToString() == "1")
            //                                    {
            //                                        //da ton tai roi
            //                                        //nothing
            //                                        countlap = countlap + 1;
            //                                    }
            //                                    else
            //                                    {
            //                                        //insert model moi
            //                                        dt_new.Rows.Add(i, SanctionId, Material, Qty, QtyActual, UnitPrice, Amount, CostCenter, Reason, Plant, Sloc, NameCost, Pallet, Barcode, ScrapSloc, ControlNo, FaTool);
            //                                    }
            //                                }

            //                                //mahang + soluong + unit  + price
            //                                if (dtExcelData.Rows[i][8].ToString() == "" && dtExcelData.Rows[i][6].ToString() == "" && dtExcelData.Rows[i][7].ToString() == "" && dtExcelData.Rows[i][10].ToString() == "" )
            //                                {
            //                                    break;
            //                                }
            //                            }
            //                        }



            //                    }
            //                }
            //                else 
            //                {
            //                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon template upload!!'); ", true);
            //                }

            //                //upload buckcopy tai day
            //                string sqlConnStr = "Data Source=10.92.186.30;Persist Security Info=False;" +
            //                                "Initial Catalog=ScrapSystem;User Id=sa;Password=Psnvdb2013;" +
            //                                "Connect Timeout=30;";

            //                using (SqlConnection con = new SqlConnection(sqlConnStr))
            //                {
            //                    con.Open();

            //                    // Initialize SqlBulkCopy.
            //                    using (SqlBulkCopy oSqlBulk = new SqlBulkCopy(con))
            //                    {
            //                        oSqlBulk.DestinationTableName = "ScrapDetails"; // Table name in database.
            //                                                                        //oSqlBulk.WriteToServer(dtExcelData); // Write data from DataTable to database.
            //                        oSqlBulk.WriteToServer(dt_new);
            //                    }
            //                }
            //                if (countlap > 0)
            //                {
            //                    lblConfirm.Text = "Ban ghi lap : " + countlap;
            //                    lblConfirm.Attributes.Add("style", "color:green");
            //                }
            //                else
            //                {
            //                    lblConfirm.Text = "DATA IMPORTED SUCCESSFULLY.";
            //                    lblConfirm.Attributes.Add("style", "color:green");
            //                }

            //                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('OK, Upload thành công!');", true);
            //                dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion", System.Data.CommandType.StoredProcedure, tensanction, _fromdate, _todate);

            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            lblConfirm.Text = "Lỗi : " + ex.Message;
            //            lblConfirm.Attributes.Add("style", "color:red");
            //            //throw;
            //        }
            //        finally
            //        {
            //            // Close and dispose objects.
            //            if (objBulkReader != null && !objBulkReader.IsClosed)
            //            {
            //                objBulkReader.Close();
            //            }
            //            if (excelConn != null && excelConn.State == ConnectionState.Open)
            //            {
            //                excelConn.Close();
            //            }
            //            // Delete the uploaded file (optional).
            //            File.Delete(Server.MapPath(".") + "\\" + FileUpload.FileName);
            //            // Reload grid or perform other necessary actions.
            //            //dt_phanca = Db_connect.StoreFillDS("HR_List_phanca", System.Data.CommandType.StoredProcedure);
            //        }
            //    }
            //}
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

        protected void btnDownloadClick(object sender, EventArgs e)
        {
            try
            {
                string fileName = "scrap list hang huy.xlsx";
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



    }
}