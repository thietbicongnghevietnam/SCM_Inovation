using FreeLayout.App_Code;
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
    public partial class frmMaterModel : System.Web.UI.Page
    {
        public DataTable dt_plan = new DataTable();
        public DataTable dt_getmodel = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_ModelSCM", System.Data.CommandType.StoredProcedure);
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
            string CAT = CATid.Text;
            string Consignee_Refer_ATP = Consignee_Refer_ATPid.Text;
            string Country = Countryid.Text;
            string Dest = Destid.Text;
            string Model = Modelid.Text;
            string Stuffing_type = Stuffing_typeid.Text;
            string Model_Vol = Model_Volid.Text;
            string Pcs_ctn = Pcs_ctnid.Text;
            string CTN_part = CTN_partid.Text;
            string CTN_vol = CTN_volid.Text;
            string Gross_weight = Gross_weightid.Text;
            string Series = Seriesid.Text;
            string MaxQty_cont40H = MaxQty_cont40Hid.Text;
            string Max_Qty_cont20F = Max_Qty_cont20Fid.Text;
            string DIM_of_Carton_L = DIM_of_Carton_Lid.Text;
            string DIM_of_Carton_W = DIM_of_Carton_Wid.Text;
            string DIM_of_Carton_H = DIM_of_Carton_Hid.Text;

            string CTNweight = CTNweightid.Text;

            //string Category = cateid.Text;
            //string userid = Session["username"].ToString();

            DataTable dtinsert = new DataTable();
            dtinsert = DataConn.StoreFillDS("Insert_mater_ModelSCM", System.Data.CommandType.StoredProcedure, CAT, Consignee_Refer_ATP, Country, Dest, Model, Stuffing_type, Model_Vol, Pcs_ctn, CTN_part, CTN_vol, Gross_weight, Series, MaxQty_cont40H, Max_Qty_cont20F, DIM_of_Carton_L, DIM_of_Carton_W, DIM_of_Carton_H, CTNweight);
            if (dtinsert.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_ModelSCM", System.Data.CommandType.StoredProcedure);
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
            string CAT = idCAT.Text;
            string Consignee_Refer_ATP = idConsignee_Refer_ATP.Text;
            string Country = idCountry.Text;
            string Dest = idDest.Text;
            string Model = idModel.Text;
            string Stuffing_type = idStuffing_type.Text;
            string Model_Vol = idModel_Vol.Text;
            string Pcs_ctn = idPcs_ctn.Text;
            string CTN_part = idCTN_part.Text;
            string CTN_vol = idCTN_vol.Text;
            string Gross_weight = idGross_weight.Text;
            string Series = idSeries.Text;
            string MaxQty_cont40H = idMaxQty_cont40H.Text;
            string Max_Qty_cont20F = idMax_Qty_cont20F.Text;
            string DIM_of_Carton_L = idDIM_of_Carton_L.Text;
            string DIM_of_Carton_W = idDIM_of_Carton_W.Text;
            string DIM_of_Carton_H = idDIM_of_Carton_H.Text;

            string CTNweight = idCTNweight.Text;

            //string userid = Session["username"].ToString();

            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS("Update_mater_ModelSCM", System.Data.CommandType.StoredProcedure, id, CAT, Consignee_Refer_ATP, Country, Dest, Model, Stuffing_type, Model_Vol, Pcs_ctn, CTN_part, CTN_vol, Gross_weight, Series, MaxQty_cont40H, Max_Qty_cont20F, DIM_of_Carton_L, DIM_of_Carton_W, DIM_of_Carton_H, CTNweight);

            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_ModelSCM", System.Data.CommandType.StoredProcedure);
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
            string model = txModel_del.Text;

            //string username = Session["username"].ToString();
            //string role_ = Session["role"].ToString();

            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS("Delete_mater_ModelSCM", System.Data.CommandType.StoredProcedure, id);  //username
            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_ModelSCM", System.Data.CommandType.StoredProcedure);
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
                dt_plan = DataConn.StoreFillDS("Select_Mater_ModelSCM", System.Data.CommandType.StoredProcedure);
            }
            else 
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_ModelSCM_cate", System.Data.CommandType.StoredProcedure, category);
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
                        dt_new.Columns.Add("CAT", typeof(String));
                        dt_new.Columns.Add("Consignee_Refer_ATP", typeof(String));
                        dt_new.Columns.Add("Country", typeof(String));
                        dt_new.Columns.Add("Dest", typeof(String));
                        dt_new.Columns.Add("Model", typeof(String));
                        dt_new.Columns.Add("Stuffing_type", typeof(String));
                        dt_new.Columns.Add("Model_Vol", typeof(float));
                        dt_new.Columns.Add("Pcs_ctn", typeof(Int32));
                        dt_new.Columns.Add("CTN_part", typeof(String));
                        dt_new.Columns.Add("CTN_vol", typeof(Int32));
                        dt_new.Columns.Add("Gross_weight", typeof(float));
                        
                        dt_new.Columns.Add("CTNweight", typeof(float));

                        dt_new.Columns.Add("Series", typeof(String));
                        dt_new.Columns.Add("MaxQty_cont40H", typeof(Int32));
                        dt_new.Columns.Add("Max_Qty_cont20F", typeof(Int32));
                        dt_new.Columns.Add("DIM_of_Carton_L", typeof(String));
                        dt_new.Columns.Add("DIM_of_Carton_W", typeof(String));
                        dt_new.Columns.Add("DIM_of_Carton_H", typeof(String));

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

                            string CAT = "";
                            string Consignee_Refer_ATP = "";
                            string Country = "";
                            string Dest = "";
                            string Model = "";
                            string Stuffing_type = "";
                            float Model_Vol = 0;
                            int Pcs_ctn = 0;
                            string CTN_part = "";
                            int CTN_vol = 0;
                            float Gross_weight = 0;

                            float CTNweight = 0;
                            
                            string Series = "";
                            int MaxQty_cont40H = 0;
                            int Max_Qty_cont20F = 0;
                            string DIM_of_Carton_L = "";
                            string DIM_of_Carton_W = "";
                            string DIM_of_Carton_H = "";

                            for (int i = 0; i < dtExcelData.Rows.Count; i++) 
                            {
                                countlap = 0; 
                                CAT = dtExcelData.Rows[i]["CAT"].ToString();
                                Consignee_Refer_ATP = dtExcelData.Rows[i]["Consignee_Refer_ATP"].ToString();
                                Country = dtExcelData.Rows[i]["Country"].ToString();
                                Dest = dtExcelData.Rows[i]["Dest"].ToString();
                                Model = dtExcelData.Rows[i]["Model"].ToString();
                                Stuffing_type = dtExcelData.Rows[i]["Stuffing_type"].ToString();
                                //Model_Vol = float.Parse(dtExcelData.Rows[i]["Model_Vol"].ToString());
                                float.TryParse(dtExcelData.Rows[i]["Model_Vol"].ToString(), out Model_Vol);
                                Model_Vol = (float)Math.Round(Model_Vol, 3);
                                //Pcs_ctn = Int32.Parse(dtExcelData.Rows[i]["Pcs_ctn"].ToString());
                                int.TryParse(dtExcelData.Rows[i]["Pcs_ctn"].ToString(), out Pcs_ctn);
                                CTN_part = dtExcelData.Rows[i]["CTN_part"].ToString();
                                int.TryParse(dtExcelData.Rows[i]["CTN_vol"].ToString(), out CTN_vol);
                                //CTN_vol = Int32.Parse(dtExcelData.Rows[i]["CTN_vol"].ToString());
                                //Gross_weight = float.Parse(dtExcelData.Rows[i]["Gross_weight"].ToString());
                                float.TryParse(dtExcelData.Rows[i]["Gross_weight"].ToString(), out Gross_weight);

                                float.TryParse(dtExcelData.Rows[i]["CTNweight"].ToString(), out CTNweight);


                                Gross_weight = (float)Math.Round(Gross_weight, 3);
                                Series = dtExcelData.Rows[i]["Series"].ToString();

                                //truong hop null

                                //MaxQty_cont40H = Int32.Parse(dtExcelData.Rows[i]["MaxQty_cont40H"].ToString());
                                //Max_Qty_cont20F = Int32.Parse(dtExcelData.Rows[i]["Max_Qty_cont20F"].ToString());
                                int.TryParse(dtExcelData.Rows[i]["MaxQty_cont40H"].ToString(), out MaxQty_cont40H);
                                int.TryParse(dtExcelData.Rows[i]["Max_Qty_cont20F"].ToString(), out Max_Qty_cont20F);

                                DIM_of_Carton_L = dtExcelData.Rows[i]["DIM_of_Carton_L"].ToString();
                                DIM_of_Carton_W = dtExcelData.Rows[i]["DIM_of_Carton_W"].ToString();
                                DIM_of_Carton_H = dtExcelData.Rows[i]["DIM_of_Carton_H"].ToString();

                                dt_getmodel = DataConn.StoreFillDS("Get_infor_mater_model2", System.Data.CommandType.StoredProcedure, Model, CAT, Country, Dest);
                                if (dt_getmodel.Rows[0][0].ToString() == "1")
                                {
                                    //da ton tai roi
                                    //nothing
                                    countlap = countlap + 1;
                                }
                                else 
                                {
                                    //insert model moi
                                    dt_new.Rows.Add(i, CAT, Consignee_Refer_ATP, Country, Dest, Model, Stuffing_type, Model_Vol, Pcs_ctn, CTN_part, CTN_vol, Gross_weight, CTNweight, Series, MaxQty_cont40H, Max_Qty_cont20F, DIM_of_Carton_L, DIM_of_Carton_W, DIM_of_Carton_H);
                                }
                                // Dừng vòng lặp khi các cột cần kiểm tra (cột 0, 2, 3) đều rỗng
                                if (dtExcelData.Rows[i][0].ToString() == "" && dtExcelData.Rows[i][1].ToString() == "" && dtExcelData.Rows[i][2].ToString() == "")
                                {
                                    break;
                                }
                            }

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
                                    oSqlBulk.DestinationTableName = "tblMaterModelSCM"; // Table name in database.
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
                            dt_plan = DataConn.StoreFillDS("Select_Mater_ModelSCM", System.Data.CommandType.StoredProcedure);

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

        protected void btnDownloadClick(Object sender, EventArgs e)
        {
            try
            {
                string fileName = "mau upload mater model.xlsx";
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