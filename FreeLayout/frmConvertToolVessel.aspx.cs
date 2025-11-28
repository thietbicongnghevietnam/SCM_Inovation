using FreeLayout.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.Media3D;

namespace FreeLayout
{
    public partial class frmConvertToolVessel : System.Web.UI.Page
    {
        public DataTable dt_plan = new DataTable();
        public DataTable dt_checkupload = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dtkeyconvert = new DataTable();
        public DataTable dt_update = new DataTable();
        public DataTable dt_setting = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Date1.Value = DateTime.Now.ToString("yyyy-MM-dd");
                ngaychiid.Value = DateTime.Now.ToString("yyyy-MM-dd");

                string _fromdate = Date1.Value;
                string _todate = ngaychiid.Value;
                               
                dt_plan = DataConn.StoreFillDS("Select_TempConvertVessel", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
                //dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion3", System.Data.CommandType.StoredProcedure, tensanction, _fromdate, _todate);
                string Typeconvert = dr_filter_Cate.SelectedValue.ToString();
                dt_setting = DataConn.StoreFillDS("Select_setting_toolvessle", System.Data.CommandType.StoredProcedure, Typeconvert);
                if (dt_setting.Rows.Count > 0)
                {
                    if (Typeconvert == "DPOversea")
                    {
                        //Typeconvert = "NGList";
                        txtCat.Value = dt_setting.Rows[0]["Cat"].ToString();
                        txtsa.Value = dt_setting.Rows[0]["sa"].ToString();
                        txtname.Value = dt_setting.Rows[0]["name"].ToString();
                        txtproductmodel.Value = dt_setting.Rows[0]["produce_model"].ToString();
                        txtjit_qty.Value = dt_setting.Rows[0]["jit_qty"].ToString();
                        txtShipDate.Value = dt_setting.Rows[0]["ship_date"].ToString();
                        txtdelay1_date.Value = dt_setting.Rows[0]["delay1_date"].ToString();
                        txtdelay1_qty.Value = dt_setting.Rows[0]["delay1_qty"].ToString();
                        txtdelay2_date.Value = dt_setting.Rows[0]["delay2_date"].ToString();
                        txtdelay2_qty.Value = dt_setting.Rows[0]["delay2_qty"].ToString();
                        txtdelay3_date.Value = dt_setting.Rows[0]["delay3_date"].ToString();
                        txtdelay3_qty.Value = dt_setting.Rows[0]["delay3_qty"].ToString();
                        txtdelay4_date.Value = dt_setting.Rows[0]["delay4_date"].ToString();
                        txtdelay4_qty.Value = dt_setting.Rows[0]["delay4_qty"].ToString();
                        txtdelay5_date.Value = dt_setting.Rows[0]["delay5_date"].ToString();
                        txtdelay5_qty.Value = dt_setting.Rows[0]["delay5_qty"].ToString();
                        txtrow.Value = dt_setting.Rows[0]["rowid"].ToString();
                    }
                    else
                    {
                        txtCat.Value = dt_setting.Rows[0]["Cat"].ToString();
                        txtsa.Value = dt_setting.Rows[0]["sa"].ToString();
                        txtname.Value = dt_setting.Rows[0]["name"].ToString();
                        txtproductmodel.Value = dt_setting.Rows[0]["produce_model"].ToString();
                        txtjit_qty.Value = dt_setting.Rows[0]["jit_qty"].ToString();
                        txtShipDate.Value = dt_setting.Rows[0]["ship_date"].ToString();
                        txtdelay1_date.Value = dt_setting.Rows[0]["delay1_date"].ToString();
                        txtdelay1_qty.Value = dt_setting.Rows[0]["delay1_qty"].ToString();
                        txtdelay2_date.Value = dt_setting.Rows[0]["delay2_date"].ToString();
                        txtdelay2_qty.Value = dt_setting.Rows[0]["delay2_qty"].ToString();
                        txtdelay3_date.Value = dt_setting.Rows[0]["delay3_date"].ToString();
                        txtdelay3_qty.Value = dt_setting.Rows[0]["delay3_qty"].ToString();
                        txtdelay4_date.Value = dt_setting.Rows[0]["delay4_date"].ToString();
                        txtdelay4_qty.Value = dt_setting.Rows[0]["delay4_qty"].ToString();
                        txtdelay5_date.Value = dt_setting.Rows[0]["delay5_date"].ToString();
                        txtdelay5_qty.Value = dt_setting.Rows[0]["delay5_qty"].ToString();
                        txtrow.Value = dt_setting.Rows[0]["rowid"].ToString();
                    }
                }


                ////danh sach template
                dtcate = DataConn.StoreFillDS("pro_get_categogy_convert", System.Data.CommandType.StoredProcedure);
                DataRow newRow1 = dtcate.NewRow();
                newRow1["NameTemplate"] = "=Template=";
                dtcate.Rows.InsertAt(newRow1, 0);
                dr_filter_Cate.DataSource = dtcate;
                dr_filter_Cate.DataBind();

                ////keyname convert
                dtkeyconvert = DataConn.StoreFillDS("pro_get_categogy_keyconvert", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
                if (dtkeyconvert.Rows.Count > 0)
                {
                    DataRow newRow2 = dtkeyconvert.NewRow();
                    newRow2["KeyConvert"] = "=IDName=";
                    dtkeyconvert.Rows.InsertAt(newRow2, 0);
                    dr_filter_keyconvert.DataSource = dtkeyconvert;
                    dr_filter_keyconvert.DataBind();
                }                
            }
        }


        protected void dr_filter_Cate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Typeconvert = dr_filter_Cate.SelectedValue.ToString();
            dt_setting = DataConn.StoreFillDS("Select_setting_toolvessle", System.Data.CommandType.StoredProcedure, Typeconvert);
            if (dt_setting.Rows.Count > 0)
            {
                if (Typeconvert == "DPOversea")
                {
                    //Typeconvert = "NGList";
                    txtCat.Value = dt_setting.Rows[0]["Cat"].ToString();
                    txtsa.Value = dt_setting.Rows[0]["sa"].ToString();
                    txtname.Value = dt_setting.Rows[0]["name"].ToString();
                    txtproductmodel.Value = dt_setting.Rows[0]["produce_model"].ToString();
                    txtjit_qty.Value = dt_setting.Rows[0]["jit_qty"].ToString();
                    txtShipDate.Value = dt_setting.Rows[0]["ship_date"].ToString();
                    txtdelay1_date.Value = dt_setting.Rows[0]["delay1_date"].ToString();
                    txtdelay1_qty.Value = dt_setting.Rows[0]["delay1_qty"].ToString();
                    txtdelay2_date.Value = dt_setting.Rows[0]["delay2_date"].ToString();
                    txtdelay2_qty.Value = dt_setting.Rows[0]["delay2_qty"].ToString();
                    txtdelay3_date.Value = dt_setting.Rows[0]["delay3_date"].ToString();
                    txtdelay3_qty.Value = dt_setting.Rows[0]["delay3_qty"].ToString();
                    txtdelay4_date.Value = dt_setting.Rows[0]["delay4_date"].ToString();
                    txtdelay4_qty.Value = dt_setting.Rows[0]["delay4_qty"].ToString();
                    txtdelay5_date.Value = dt_setting.Rows[0]["delay5_date"].ToString();
                    txtdelay5_qty.Value = dt_setting.Rows[0]["delay5_qty"].ToString();
                    txtrow.Value = dt_setting.Rows[0]["rowid"].ToString();
                }
                else
                {
                    txtCat.Value = dt_setting.Rows[0]["Cat"].ToString();
                    txtsa.Value = dt_setting.Rows[0]["sa"].ToString();
                    txtname.Value = dt_setting.Rows[0]["name"].ToString();
                    txtproductmodel.Value = dt_setting.Rows[0]["produce_model"].ToString();
                    txtjit_qty.Value = dt_setting.Rows[0]["jit_qty"].ToString();
                    txtShipDate.Value = dt_setting.Rows[0]["ship_date"].ToString();
                    txtdelay1_date.Value = dt_setting.Rows[0]["delay1_date"].ToString();
                    txtdelay1_qty.Value = dt_setting.Rows[0]["delay1_qty"].ToString();
                    txtdelay2_date.Value = dt_setting.Rows[0]["delay2_date"].ToString();
                    txtdelay2_qty.Value = dt_setting.Rows[0]["delay2_qty"].ToString();
                    txtdelay3_date.Value = dt_setting.Rows[0]["delay3_date"].ToString();
                    txtdelay3_qty.Value = dt_setting.Rows[0]["delay3_qty"].ToString();
                    txtdelay4_date.Value = dt_setting.Rows[0]["delay4_date"].ToString();
                    txtdelay4_qty.Value = dt_setting.Rows[0]["delay4_qty"].ToString();
                    txtdelay5_date.Value = dt_setting.Rows[0]["delay5_date"].ToString();
                    txtdelay5_qty.Value = dt_setting.Rows[0]["delay5_qty"].ToString();
                    txtrow.Value = dt_setting.Rows[0]["rowid"].ToString();
                }
            }
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string template = dr_filter_Cate.SelectedValue.ToString();
            string keyconvert = dr_filter_keyconvert.SelectedValue.ToString();

            ////loc theo ngay
            if (_fromdate == "" || _todate == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban nen chon ngay!!!'); ", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Ban nen chon ngay!');", true);
            }
            else
            {
                dt_plan = DataConn.StoreFillDS("Select_TempConvertVessel_keyconvert", System.Data.CommandType.StoredProcedure, _fromdate, _todate, keyconvert, template);

                // XÓA TRƯỚC KHI BIND
                //dr_filter_Sanction.Items.Clear();

                //dtsanction = DataConn.StoreFillDS2("pro_get_section", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
                //DataRow newRow2 = dtsanction.NewRow();
                //newRow2["SanctionId"] = "==Sanction==";
                //dtsanction.Rows.InsertAt(newRow2, 0);
                //dr_filter_Sanction.DataSource = dtsanction;
                //dr_filter_Sanction.DataBind();

                //if (bophan == "==Section==")
                //{
                //    //dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion", System.Data.CommandType.StoredProcedure, sacnctionid, _fromdate, _todate);
                //    dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion3", System.Data.CommandType.StoredProcedure, sacnctionid, _fromdate, _todate);
                //}
                //else
                //{
                //    dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion2", System.Data.CommandType.StoredProcedure, bophan, sacnctionid, _fromdate, _todate);
                //}

            }
        }

        protected void Save_setting_Click(object sender, EventArgs e)
        {
            //string _fromdate = Request.Form[Date1.UniqueID];
            //string _todate = Request.Form[ngaychiid.UniqueID];

            string cate = txtCat.Value.ToString();
            string sa = txtsa.Value.ToString();
            string name = txtname.Value.ToString();
            string productmodel = txtproductmodel.Value.ToString();
            string jitqty = txtjit_qty.Value.ToString();
            string shipdate = txtShipDate.Value.ToString();

            string delaydate1 = txtdelay1_date.Value.ToString();
            string delayqty1 = txtdelay1_qty.Value.ToString();

            string delaydate2 = txtdelay2_date.Value.ToString();
            string delayqty2 = txtdelay2_qty.Value.ToString();

            string delaydate3 = txtdelay3_date.Value.ToString();
            string delayqty3 = txtdelay3_qty.Value.ToString();

            string delaydate4 = txtdelay4_date.Value.ToString();
            string delayqty4 = txtdelay4_qty.Value.ToString();

            string delaydate5 = txtdelay5_date.Value.ToString();
            string delayqty5 = txtdelay5_qty.Value.ToString();

            string index_row = txtrow.Value.ToString();

            string Typeconvert = "";

            if (dr_filter_Cate.SelectedValue == "=Template=")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, do not chosen template!'); ", true);
            }
            else
            {
                Typeconvert = dr_filter_Cate.SelectedValue.ToString();
                //hang trong file excel bat buoc phai nhap
                if (cate == "" && sa == "" && name == "" && productmodel == "" && jitqty == "" && shipdate == "" && index_row == "")  
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, lack of infomation!'); ", true);
                }
                else
                {
                    dt_checkupload = DataConn.StoreFillDS("Update_setting_toolconvert", System.Data.CommandType.StoredProcedure, cate, sa, name, productmodel, jitqty, shipdate, delaydate1, delayqty1, delaydate2, delayqty2, delaydate3, delayqty3, delaydate4, delayqty4, delaydate5, delayqty5, Typeconvert, index_row);
                    if (dt_checkupload.Rows[0][0].ToString() == "1")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('OK, Update thành công!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('OK, Insert thành công!');", true);
                    }
                }
            }            
        }

        protected void ImportFromExcel(object sender, EventArgs e)
        {
            string template_name = dr_filter_Cate.SelectedValue.ToString();
            if (template_name == "=Template=")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, you do not select template_name!'); ", true);
            }
            else
            {
                DataTable dtcheck = new DataTable();
                string _fromdate = Request.Form[Date1.UniqueID];
                string _todate = Request.Form[ngaychiid.UniqueID];

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
                            dt_new.Columns.Add("Category", typeof(string));
                            dt_new.Columns.Add("sa", typeof(string));
                            dt_new.Columns.Add("name", typeof(string));
                            dt_new.Columns.Add("producemodel", typeof(string));
                            dt_new.Columns.Add("ReqQty", typeof(int));
                            dt_new.Columns.Add("jitqty", typeof(int));
                            dt_new.Columns.Add("shipdate", typeof(DateTime));

                            dt_new.Columns.Add("delay1date", typeof(DateTime));
                            dt_new.Columns.Add("delay1qty", typeof(int));

                            dt_new.Columns.Add("delay2date", typeof(DateTime));
                            dt_new.Columns.Add("delay2qty", typeof(int));

                            dt_new.Columns.Add("delay3date", typeof(DateTime));
                            dt_new.Columns.Add("delay3qty", typeof(int));

                            dt_new.Columns.Add("delay4date", typeof(DateTime));
                            dt_new.Columns.Add("delay4qty", typeof(int));

                            dt_new.Columns.Add("delay5date", typeof(DateTime));
                            dt_new.Columns.Add("delay5qty", typeof(int));

                            dt_new.Columns.Add("TemplateName", typeof(string));
                            dt_new.Columns.Add("KeyConvert", typeof(string));

                           

                            // Open connection to Excel file.
                            excelConn = new OleDbConnection(excelConnStr);
                            excelConn.Open();
                            // Lấy danh sách các sheet trong Excel
                            DataTable sheets = excelConn.GetSchema("Tables");
                            // Lấy tên sheet đầu tiên (vì chỉ có một sheet)
                            string sheetName = sheets.Rows[0]["TABLE_NAME"].ToString();
                            sheetName = sheetName.Trim('\'');               // Nếu có dấu nháy đơn bao ngoài ---> LOẠI BỎ
                            Console.WriteLine("Tên sheet: " + sheetName);

                            // Xử lý tên sheet (nếu có ký tự đặc biệt)
                            //string sanitizedSheetName = SanitizeSheetName(sheetName);

                            // Tạo câu truy vấn SQL với tên sheet đã xử lý
                            //OleDbCommand objOleDB = new OleDbCommand($"SELECT * FROM [{sanitizedSheetName}$]", excelConn);
                            OleDbCommand objOleDB = new OleDbCommand($"SELECT * FROM [{sheetName}]", excelConn);

                            objBulkReader = objOleDB.ExecuteReader();

                            if (objBulkReader.HasRows)
                            {
                                DataTable dtExcelData = new DataTable();
                                dtExcelData.Load(objBulkReader); // Load data into DataTable.
                                string Sheet = sheetName.Replace("$", "");
                                                               
                                string type_convert = template_name;
                                //string TemplateName = "";

                                string Cat = "";
                                string sa = "";
                                string name = "";
                                string produce_model = "";
                                int jit_qty = 0;
                                DateTime ship_date;
                                DateTime? delay1_date = null;
                                int delay1_qty = 0;
                                DateTime? delay2_date = null;
                                int delay2_qty = 0;
                                DateTime? delay3_date = null;
                                int delay3_qty = 0;
                                DateTime? delay4_date = null;
                                int delay4_qty = 0;
                                DateTime? delay5_date = null;
                                int delay5_qty = 0;

                                string KeyConvert = "";


                                //fix cot theo tool convert
                                int col_cate = Int32.Parse(txtCat.Value.ToString());
                                int col_sa = Int32.Parse(txtsa.Value.ToString());
                                int col_name = Int32.Parse(txtname.Value.ToString());
                                int col_productmodel = Int32.Parse(txtproductmodel.Value.ToString());

                                int col_jitqty = Int32.Parse(txtjit_qty.Value.ToString());
                                int col_ship_date = Int32.Parse(txtShipDate.Value.ToString());

                                //int col_delay1_date = Int32.Parse(txtdelay1_date.Value.ToString());
                                //int col_delay1_qty = Int32.Parse(txtdelay1_qty.Value.ToString());
                                int col_delay1_date = int.TryParse(txtdelay1_date.Value?.ToString() ?? "0", out int temp1) ? temp1 : 0;
                                int col_delay1_qty = int.TryParse(txtdelay1_qty.Value?.ToString() ?? "0", out int temp2) ? temp2 : 0;

                                //int col_delay2_date = Int32.Parse(txtdelay2_date.Value.ToString());
                                //int col_delay2_qty = Int32.Parse(txtdelay2_qty.Value.ToString());
                                int col_delay2_date = int.TryParse(txtdelay2_date.Value?.ToString() ?? "0", out int tempb) ? tempb : 0;
                                int col_delay2_qty = int.TryParse(txtdelay2_qty.Value?.ToString() ?? "0", out int tempb2) ? tempb2 : 0;

                                //int col_delay3_date = Int32.Parse(txtdelay3_date.Value.ToString());
                                //int col_delay3_qty = Int32.Parse(txtdelay3_qty.Value.ToString());
                                int col_delay3_date = int.TryParse(txtdelay3_date.Value?.ToString() ?? "0", out int tempa) ? tempa : 0;
                                int col_delay3_qty = int.TryParse(txtdelay3_qty.Value?.ToString() ?? "0", out int tempa2) ? tempa2 : 0;


                                //int col_delay4_date = Int32.Parse(txtdelay4_date.Value.ToString());
                                //int col_delay4_qty = Int32.Parse(txtdelay4_qty.Value.ToString());
                                int col_delay4_date = int.TryParse(txtdelay4_date.Value?.ToString() ?? "0", out int tempc) ? tempc : 0;
                                int col_delay4_qty = int.TryParse(txtdelay4_qty.Value?.ToString() ?? "0", out int tempac) ? tempac : 0;

                                //int col_delay5_date = Int32.Parse(txtdelay5_date.Value.ToString());
                                //int col_delay5_qty = Int32.Parse(txtdelay5_qty.Value.ToString());
                                int col_delay5_date = int.TryParse(txtdelay5_date.Value?.ToString() ?? "0", out int tempd) ? tempd : 0;
                                int col_delay5_qty = int.TryParse(txtdelay5_qty.Value?.ToString() ?? "0", out int tempad) ? tempad : 0;                                                                

                                int row_index = Int32.Parse(txtrow.Value.ToString());

                                //string test3 = dtExcelData.Rows[2][1].ToString();

                                if (type_convert == "")
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG,type_convert is null!'); ", true);
                                }
                                else
                                {
                                    if (type_convert == "DPOversea")
                                    {
                                        KeyConvert = type_convert + DateTime.Now.ToString("ddMMyyyy");   //ten key de khi tinh tu bang tem den bang chinh

                                        //for (int i = 2; i < dtExcelData.Rows.Count; i++)
                                        for (int i = row_index; i < dtExcelData.Rows.Count; i++)
                                        {
                                            //countlap = 0;
                                            // check cac cot co du lieu va khong co du lieu
                                            //mahang + Plant + issue sloc + scrap loc + st price
                                            if (dtExcelData.Rows[i][1].ToString() != "" && dtExcelData.Rows[i][2].ToString() != "" && dtExcelData.Rows[i][3].ToString() != "" && dtExcelData.Rows[i][4].ToString() != "" && dtExcelData.Rows[i][5].ToString() != "")
                                            {
                                                Cat = dtExcelData.Rows[i][col_cate].ToString();
                                                sa = dtExcelData.Rows[i][col_sa].ToString();
                                                name = dtExcelData.Rows[i][col_name].ToString();
                                                produce_model = dtExcelData.Rows[i][col_productmodel].ToString();
                                                jit_qty = Int32.Parse(dtExcelData.Rows[i][col_jitqty].ToString());
                                                ship_date = DateTime.Parse(dtExcelData.Rows[i][col_ship_date].ToString());
                                                
                                                DateTime temp;

                                                if (DateTime.TryParse(dtExcelData.Rows[i][col_delay1_date].ToString(), out temp))
                                                    delay1_date = temp;
                                                else
                                                    delay1_date = null;

                                                if (DateTime.TryParse(dtExcelData.Rows[i][col_delay2_date].ToString(), out temp))
                                                    delay2_date = temp;
                                                else
                                                    delay2_date = null;

                                                if (DateTime.TryParse(dtExcelData.Rows[i][col_delay3_date].ToString(), out temp))
                                                    delay3_date = temp;
                                                else
                                                    delay3_date = null;

                                                if (DateTime.TryParse(dtExcelData.Rows[i][col_delay4_date].ToString(), out temp))
                                                    delay4_date = temp;
                                                else
                                                    delay4_date = null;

                                                if (DateTime.TryParse(dtExcelData.Rows[i][col_delay5_date].ToString(), out temp))
                                                    delay5_date = temp;
                                                else
                                                    delay5_date = null;

                                                delay1_qty = string.IsNullOrWhiteSpace(dtExcelData.Rows[i][col_delay1_qty].ToString()) ? 0 :
                                                             Int32.Parse(dtExcelData.Rows[i][col_delay1_qty].ToString());

                                                delay2_qty = string.IsNullOrWhiteSpace(dtExcelData.Rows[i][col_delay2_qty].ToString()) ? 0 :
                                                             Int32.Parse(dtExcelData.Rows[i][col_delay2_qty].ToString());

                                                delay3_qty = string.IsNullOrWhiteSpace(dtExcelData.Rows[i][col_delay3_qty].ToString()) ? 0 :
                                                             Int32.Parse(dtExcelData.Rows[i][col_delay3_qty].ToString());

                                                delay4_qty = string.IsNullOrWhiteSpace(dtExcelData.Rows[i][col_delay4_qty].ToString()) ? 0 :
                                                             Int32.Parse(dtExcelData.Rows[i][col_delay4_qty].ToString());

                                                delay5_qty = string.IsNullOrWhiteSpace(dtExcelData.Rows[i][col_delay5_qty].ToString()) ? 0 :
                                                             Int32.Parse(dtExcelData.Rows[i][col_delay5_qty].ToString());



                                                dt_checkupload = DataConn.StoreFillDS("Check_upload_Vessel_convert", System.Data.CommandType.StoredProcedure, type_convert, produce_model, ship_date, jit_qty, Cat, _fromdate,_todate);
                                                if (dt_checkupload.Rows[0][0].ToString() == "1")
                                                {
                                                    //da ton tai roi
                                                    //nothing
                                                    countlap = countlap + 1;
                                                }
                                                else
                                                {
                                                    //insert model moi   //jit_qty = repqty
                                                    dt_new.Rows.Add(i, Cat, sa, name, produce_model, jit_qty, jit_qty, ship_date, delay1_date, delay1_qty, delay2_date, delay2_qty, delay3_date, delay3_qty, delay4_date, delay4_qty, delay5_date, delay5_qty, type_convert, KeyConvert);
                                                }
                                            }

                                            //mahang + Plant + issue sloc + scrap loc + st price  ==> Tong scrap (10)
                                            if (dtExcelData.Rows[i][1].ToString() == "" && dtExcelData.Rows[i][2].ToString() == "" && dtExcelData.Rows[i][3].ToString() == "" && dtExcelData.Rows[i][4].ToString() == "" && dtExcelData.Rows[i][5].ToString() == "" && dtExcelData.Rows[i][6].ToString() == "")
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else if (type_convert == "other")
                                    {
                                        //template chung
                                    }
                                    else
                                    {
                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon template upload!!'); ", true);
                                    }

                                    string sqlConnStr = "Data Source=10.92.184.22\\hienpc;Persist Security Info=False;" +
                                                    "Initial Catalog=LichTau;User Id=sa;Password=Hien304@;" +
                                                    "Connect Timeout=30;";

                                    //upload buckcopy tai day
                                    //string sqlConnStr = "Data Source=10.92.186.30;Persist Security Info=False;" +
                                    //                "Initial Catalog=ScrapSystem;User Id=sa;Password=Psnvdb2013;" +
                                    //                "Connect Timeout=30;";

                                    //              string sqlConnStr = @"Data Source=LT-DE2302026;
                                    //Initial Catalog=ScrapSystem;
                                    //Integrated Security=True;
                                    //Connect Timeout=30;
                                    //TrustServerCertificate=True;";

                                    using (SqlConnection con = new SqlConnection(sqlConnStr))
                                    {
                                        con.Open();

                                        // Initialize SqlBulkCopy.
                                        using (SqlBulkCopy oSqlBulk = new SqlBulkCopy(con))
                                        {
                                            oSqlBulk.DestinationTableName = "tblTempConvertVessel"; // bang covnert
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
                                    //dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion3", System.Data.CommandType.StoredProcedure, tensanction, _fromdate, _todate);   //bang convert
                                    dt_plan = DataConn.StoreFillDS("Select_TempConvertVessel_keyconvert", System.Data.CommandType.StoredProcedure, _fromdate, _todate, KeyConvert, template_name);
                                }
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
        }


    }
}