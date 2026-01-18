using FreeLayout.App_Code;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Input;

namespace FreeLayout
{
    public partial class frmConvertToll : System.Web.UI.Page
    {
        public DataTable dt_plan = new DataTable();
        public DataTable dt_checkupload = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dtsanction = new DataTable();
        public DataTable dt_update = new DataTable();
        public DataTable dt_setting = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Date1.Value = DateTime.Now.ToString("yyyy-MM-dd");
                ngaychiid.Value = DateTime.Now.ToString("yyyy-MM-dd");
                string Typeconvert = "";
                if (rblNG.Checked == true)
                {
                    Typeconvert = "NGList";
                }
                else
                {
                    Typeconvert = "deskstock";
                }
                string _fromdate = Date1.Value;
                string _todate = ngaychiid.Value;
                string tensanction = "";
                //dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList", System.Data.CommandType.StoredProcedure);
                dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion3", System.Data.CommandType.StoredProcedure, tensanction, _fromdate, _todate, filterSanction.Value.ToString());

                dt_setting = DataConn.StoreFillDS2("Select_setting_tool", System.Data.CommandType.StoredProcedure, Typeconvert);
                if (dt_setting.Rows.Count > 0)
                {
                    if (rblNG.Checked == true)
                    {
                        //Typeconvert = "NGList";
                        txtplan.Value = dt_setting.Rows[0]["Plant"].ToString();
                        txtsloc.Value = dt_setting.Rows[0]["Sloc"].ToString();
                        txtCostcenter.Value = dt_setting.Rows[0]["CostCenter"].ToString();
                        txtnamecost.Value = dt_setting.Rows[0]["Namecost"].ToString();
                        txtmaterial.Value = dt_setting.Rows[0]["Material"].ToString();
                        txtQty.Value = dt_setting.Rows[0]["IssueQty"].ToString();
                        txtunitpriceST.Value = dt_setting.Rows[0]["UnitpriceST"].ToString();
                        txtamountST.Value = dt_setting.Rows[0]["AmountST"].ToString();
                        txtunitpriceAC.Value = dt_setting.Rows[0]["UnitpriceAC"].ToString();
                        txtamountAC.Value = dt_setting.Rows[0]["AmountAC"].ToString(); ;
                        txtremark.Value = dt_setting.Rows[0]["remark"].ToString();
                        txtvendorname.Value = dt_setting.Rows[0]["vendorname"].ToString();
                        txtissueoutsloc.Value = dt_setting.Rows[0]["issueoutsloc"].ToString();

                        txtrow.Value = dt_setting.Rows[0]["index_row"].ToString();
                    }
                    else
                    {
                        //Typeconvert = "deskstock";
                        //Typeconvert = "deskstock";
                        txtplan.Value = dt_setting.Rows[0]["Plant"].ToString();
                        txtsloc.Value = dt_setting.Rows[0]["Sloc"].ToString();
                        txtCostcenter.Value = dt_setting.Rows[0]["CostCenter"].ToString();
                        txtnamecost.Value = dt_setting.Rows[0]["Namecost"].ToString();
                        txtmaterial.Value = dt_setting.Rows[0]["Material"].ToString();
                        txtQty.Value = dt_setting.Rows[0]["IssueQty"].ToString();
                        txtunitpriceST.Value = dt_setting.Rows[0]["UnitpriceST"].ToString();
                        txtamountST.Value = dt_setting.Rows[0]["AmountST"].ToString();
                        txtunitpriceAC.Value = dt_setting.Rows[0]["UnitpriceAC"].ToString();
                        txtamountAC.Value = dt_setting.Rows[0]["AmountAC"].ToString(); ;
                        txtremark.Value = dt_setting.Rows[0]["remark"].ToString();
                        txtvendorname.Value = dt_setting.Rows[0]["vendorname"].ToString();
                        txtissueoutsloc.Value = dt_setting.Rows[0]["issueoutsloc"].ToString();

                        txtrow.Value = dt_setting.Rows[0]["index_row"].ToString();
                    }
                }

                //danh sach bo phan
                dtcate = DataConn.StoreFillDS2("pro_get_categogy", System.Data.CommandType.StoredProcedure);
                DataRow newRow1 = dtcate.NewRow();
                newRow1["Description"] = "==Section==";
                dtcate.Rows.InsertAt(newRow1, 0);
                dr_filter_Cate.DataSource = dtcate;
                dr_filter_Cate.DataBind();

                dtsanction = DataConn.StoreFillDS2("pro_get_section", System.Data.CommandType.StoredProcedure, _fromdate, _todate);
                DataRow newRow2 = dtsanction.NewRow();
                newRow2["SanctionId"] = "==Sanction==";
                dtsanction.Rows.InsertAt(newRow2, 0);
                dr_filter_Sanction.DataSource = dtsanction;
                dr_filter_Sanction.DataBind();



            }
        }

        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string Typeconvert = "";
            if (rblNG.Checked == true)
            {
                Typeconvert = "NGList";
            }
            else
            {
                Typeconvert = "deskstock";
            }
            dt_setting = DataConn.StoreFillDS2("Select_setting_tool", System.Data.CommandType.StoredProcedure, Typeconvert);
            if (dt_setting.Rows.Count > 0)
            {
                if (rblNG.Checked == true)
                {
                    //Typeconvert = "NGList";
                    txtplan.Value = dt_setting.Rows[0]["Plant"].ToString();
                    txtsloc.Value = dt_setting.Rows[0]["Sloc"].ToString();
                    txtCostcenter.Value = dt_setting.Rows[0]["CostCenter"].ToString();
                    txtnamecost.Value = dt_setting.Rows[0]["Namecost"].ToString();
                    txtmaterial.Value = dt_setting.Rows[0]["Material"].ToString();
                    txtQty.Value = dt_setting.Rows[0]["IssueQty"].ToString();
                    txtunitpriceST.Value = dt_setting.Rows[0]["UnitpriceST"].ToString();
                    txtamountST.Value = dt_setting.Rows[0]["AmountST"].ToString();
                    txtunitpriceAC.Value = dt_setting.Rows[0]["UnitpriceAC"].ToString();
                    txtamountAC.Value = dt_setting.Rows[0]["AmountAC"].ToString(); ;
                    txtremark.Value = dt_setting.Rows[0]["remark"].ToString();
                    txtvendorname.Value = dt_setting.Rows[0]["vendorname"].ToString();
                    txtissueoutsloc.Value = dt_setting.Rows[0]["issueoutsloc"].ToString();

                    txtrow.Value = dt_setting.Rows[0]["index_row"].ToString();
                }
                else
                {
                    //Typeconvert = "deskstock";
                    txtplan.Value = dt_setting.Rows[0]["Plant"].ToString();
                    txtsloc.Value = dt_setting.Rows[0]["Sloc"].ToString();
                    txtCostcenter.Value = dt_setting.Rows[0]["CostCenter"].ToString();
                    txtnamecost.Value = dt_setting.Rows[0]["Namecost"].ToString();
                    txtmaterial.Value = dt_setting.Rows[0]["Material"].ToString();
                    txtQty.Value = dt_setting.Rows[0]["IssueQty"].ToString();
                    txtunitpriceST.Value = dt_setting.Rows[0]["UnitpriceST"].ToString();
                    txtamountST.Value = dt_setting.Rows[0]["AmountST"].ToString();
                    txtunitpriceAC.Value = dt_setting.Rows[0]["UnitpriceAC"].ToString();
                    txtamountAC.Value = dt_setting.Rows[0]["AmountAC"].ToString(); ;
                    txtremark.Value = dt_setting.Rows[0]["remark"].ToString();
                    txtvendorname.Value = dt_setting.Rows[0]["vendorname"].ToString();
                    txtissueoutsloc.Value = dt_setting.Rows[0]["issueoutsloc"].ToString();

                    txtrow.Value = dt_setting.Rows[0]["index_row"].ToString();
                }
            }
        }

        protected void delete_item(object sender, EventArgs e)
        {
            string _fromdate = Date1.Value;
            string _todate = ngaychiid.Value;

            DataTable dt_update = new DataTable();
            string IDdel = txtid.Text.ToString();
            string sanctionname = txtsanction.Text.ToString();
            string userid = txtuser.Text.ToString();

            if (userid != "")
            {
                dt_update = DataConn.StoreFillDS2("Delete_idconverttool", System.Data.CommandType.StoredProcedure, IDdel, userid, sanctionname);
                if (dt_update.Rows[0][0].ToString() == "1")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Delete data sucessful!');", true);
                    dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion3", System.Data.CommandType.StoredProcedure, sanctionname, _fromdate, _todate, filterSanction.Value.ToString());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, check information again!'); ", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG,Bạn chưa nhập User admin để xóa!'); ", true);
            }

        }

        protected void Updatethongtin(object sender, EventArgs e)
        {
            string _fromdate = Date1.Value;
            string _todate = ngaychiid.Value;

            DataTable dt_update = new DataTable();
            string idconvert = IDedit.Text.ToString();
            string sanctionname = idSanctionname.Text.ToString();
            string qty_act = idqty.Text.ToString();
            string vendor = idvendor.Text.ToString();

            try
            {
                if (qty_act == "")
                {
                    //dt_plan = DataConn.StoreFillDS("Select_Upload_Plan", System.Data.CommandType.StoredProcedure);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon Qty!'); ", true);
                }
                else
                {
                    dt_update = DataConn.StoreFillDS2("update_Qty_convert_tool", System.Data.CommandType.StoredProcedure, idconvert, sanctionname, qty_act, vendor);
                    if (dt_update.Rows[0][0].ToString() == "1")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Du lieu update thanh cong!');", true);
                        dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion3", System.Data.CommandType.StoredProcedure, sanctionname, _fromdate, _todate, filterSanction.Value.ToString());
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
        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string bophan = dr_filter_Cate.SelectedValue;
            string sacnctionid = dr_filter_Sanction.SelectedValue; //filterSanction.Value;

            string tensanction2 = filterSanction.Value.ToString();
            //loc theo ngay
            if (_fromdate == "" || _todate == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban nen chon ngay!!!'); ", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Ban nen chon ngay!');", true);
            }
            else
            {
                dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion3", System.Data.CommandType.StoredProcedure, sacnctionid, _fromdate, _todate, tensanction2);

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

        protected void dr_filter_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _fromdate = Date1.Value;
            string _todate = ngaychiid.Value;
            string bophan = dr_filter_Cate.SelectedValue;
            //string tensanction = dr_filter_Sanction.SelectedValue.ToString();
            string sacnctionid = dr_filter_Sanction.SelectedValue;
            string tensanction2 = filterSanction.Value.ToString();

            DataTable dtIssueOut = DataConn.StoreFillDS2("pro_get_section5", System.Data.CommandType.StoredProcedure, _fromdate, _todate, bophan);

            // XÓA TRƯỚC KHI BIND
            dr_filter_Sanction.Items.Clear();

            if (dtIssueOut.Rows.Count > 0)
            {
                DataRow newRow3 = dtIssueOut.NewRow();
                newRow3["SanctionId"] = "==Sanction==";
                dtIssueOut.Rows.InsertAt(newRow3, 0);
                dr_filter_Sanction.DataSource = dtIssueOut;
                dr_filter_Sanction.DataBind();
            }

            //dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion3", System.Data.CommandType.StoredProcedure, sacnctionid, _fromdate, _todate, tensanction2);
        }

        protected void Dongbo_craplist_Click(object sender, EventArgs e)
        {
            try
            {
                string _fromdate = Request.Form[Date1.UniqueID];
                string _todate = Request.Form[ngaychiid.UniqueID];
                string bophan = dr_filter_Cate.SelectedValue;
                //string sacnctionid = dr_filter_Sanction.SelectedValue; //filterSanction.Value;

                string tensanction2 = filterSanction.Value.ToString();

                string tensanction = dr_filter_Sanction.SelectedValue;
                //lay sanction de update lai so luong voi scrap list
                //kiem tra sanction da cos trong scrap list chua?
                DataTable dt_check = DataConn.StoreFillDS2("Check_dongbo_sacntion_tool", System.Data.CommandType.StoredProcedure, tensanction);
                if (dt_check.Rows[0][0].ToString() == "1")
                {
                    int count_update = 0;
                    DataTable dt_select = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion4", System.Data.CommandType.StoredProcedure, tensanction);
                    for (int i = 0; i < dt_select.Rows.Count; i++)
                    {
                        string material = dt_select.Rows[i]["Material"].ToString();
                        string Qty = dt_select.Rows[i]["Qty"].ToString();
                        string plant = dt_select.Rows[i]["Plant"].ToString();
                        string Sloc = dt_select.Rows[i]["Sloc"].ToString();
                        string TypeName = dt_select.Rows[i]["TypeName"].ToString();
                        string Vendor = dt_select.Rows[i]["Vendor"].ToString();
                        DataTable dt_changesoluong = DataConn.StoreFillDS2("Dongbo_ScrapList_toolconvert", System.Data.CommandType.StoredProcedure, tensanction, material, Qty, plant, Sloc, TypeName, Vendor);
                        if (dt_changesoluong.Rows[0][0].ToString() == "1")
                        {
                            count_update = count_update + 1;
                        }
                    }
                    if (count_update > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Data đồng bộ thành công!');", true);
                        dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion3", System.Data.CommandType.StoredProcedure, tensanction, _fromdate, _todate, tensanction2);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Sanction chưa được upload sang scrap list!!!'); ", true);
                }                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void export_craplist_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string bophan = dr_filter_Cate.SelectedValue;
            string sacnctionid = dr_filter_Sanction.SelectedValue;
            //loc theo ngay
            if (_fromdate == "" || _todate == "" || bophan == "==Section==" || sacnctionid == "==Sanction==")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon du thong tin!!!'); ", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Ban nen chon ngay!');", true);
            }
            else
            {
                //DataTable dt_dowload = new DataTable();
                //dt_plan = DataConn.StoreFillDS2("Export_ScrapList_tool", System.Data.CommandType.StoredProcedure, bophan, sacnctionid, _fromdate, _todate);

                string relativePath = "mau sraplist.xlsx";
                string localPath = Server.MapPath(relativePath);

                // Đường dẫn để lưu file Excel mới
                string newFileName = "Export_Scraplist.xlsx"; // Tên file mới
                string newFilePath = Server.MapPath("Textfile/" + newFileName); // Đường dẫn đầy đủ

                // Gọi phương thức để xử lý file Excel và lưu file mới
                ProcessExcelFile(localPath, newFilePath, _fromdate, _todate, bophan, sacnctionid);

                // Tải xuống file mới
                DownloadFile(newFilePath, newFileName);

            }
        }

        static void ProcessExcelFile(string filePath, string newFilePath, string tungay, string denngay, string bophan, string sacnctionid)
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
            //DataTable dt_dowload = new DataTable();
            //where SanctionId=@sacnctionid and Vendor <> '' and Sloc<>''   chan dieu kien sloc va vendor <>''
            DataTable dtexcel = new DataTable();
            dtexcel = DataConn.StoreFillDS2("Export_ScrapList_tool", System.Data.CommandType.StoredProcedure, bophan, sacnctionid, tungay, denngay);

            using (var package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets["Form B"];
                //ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                //worksheet.Cells["D5"].Value = tungay;// "Thông tin mới";

                //if (worksheet == null)
                //{
                //    throw new Exception("Không tìm thấy sheet 'Sheet1' trong file Excel.");
                //}

                int row = 12;
                int i = 0;
                DateTime currentDate = DateTime.Today;
                string monthString = DateTime.Today.ToString("MM");

                worksheet.Cells[8, 3].Value = monthString;
                worksheet.Cells[9, 3].Value = bophan;

                foreach (DataRow dataRow in dtexcel.Rows)
                {
                    i++;
                    worksheet.Cells[row, 1].Value = i;
                    worksheet.Cells[row, 2].Value = dataRow["Plant"];
                    worksheet.Cells[row, 3].Value = dataRow["Sloc"];
                    worksheet.Cells[row, 4].Value = dataRow["CostCenter"];
                    worksheet.Cells[row, 5].Value = dataRow["NameCost"];

                    worksheet.Cells[row, 6].Value = dataRow["Material"]; //

                    worksheet.Cells[row, 7].Value = dataRow["Qty"];
                    worksheet.Cells[row, 8].Value = dataRow["UnitPrice"];
                    //worksheet.Cells[row, 9].Value = dataRow["Amount"];    //lay theo cong thuc trong excel

                    decimal ckunitPriceAC = dataRow["UnitPriceAC"] == DBNull.Value ? 0 : Convert.ToDecimal(dataRow["UnitPriceAC"]);
                    if (ckunitPriceAC == 0)
                    {
                        worksheet.Cells[row, 10].Value = null; 
                    }
                    else
                    {
                        worksheet.Cells[row, 10].Value = dataRow["UnitPriceAC"];
                    }

                    //worksheet.Cells[row, 10].Value = dataRow["UnitPriceAC"];
                    //worksheet.Cells[row, 11].Value = dataRow["AmountAC"];     //lay theo cong thuc trong excel

                    worksheet.Cells[row, 12].Value = dataRow["Remark"]; ;// dataRow["Reason"];

                    worksheet.Cells[row, 13].Value = dataRow["VendorName"];  //VendorName
                    worksheet.Cells[row, 14].Value = dataRow["ScrapSloc"];

                    worksheet.Cells[row, 15].Value = ""; //so palet
                    worksheet.Cells[row, 16].Value = dataRow["SanctionId"]; //so sanction

                    worksheet.Cells[row, 17].Value = dataRow["Reason"]; //reason 17
                    worksheet.Cells[row, 18].Value = bophan;   //bo phan 18
                    worksheet.Cells[row, 19].Value = dataRow["TypeName"];  //Type
                    worksheet.Cells[row, 20].Value = dataRow["MVT"];
                    worksheet.Cells[row, 21].Value = dataRow["MoveType"];

                    worksheet.Cells[row, 22].Value = dataRow["AccountCost"]; //GL code = AccountCost
                    worksheet.Cells[row, 34].Value = dataRow["Vendor"]; //[Vendor]

                    row++;
                }
                //Xóa validation của toàn workbook
                //foreach (var ws in package.Workbook.Worksheets)
                //{
                //    for (int v = ws.DataValidations.Count - 1; v >= 0; v--)
                //    {
                //        ws.DataValidations.Remove(ws.DataValidations[v]);
                //    }
                //}
                // xoa worksheet
                var validations = worksheet.DataValidations;
                for (int v = validations.Count - 1; v >= 0; v--)
                {
                    validations.Remove(validations[v]);
                }

                // Lưu vào file mới
                package.SaveAs(newFileInfo);
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

        protected void Save_setting_Click(object sender, EventArgs e)
        {
            //string _fromdate = Request.Form[Date1.UniqueID];
            //string _todate = Request.Form[ngaychiid.UniqueID];

            string Plant = txtplan.Value.ToString();
            string Sloc = txtsloc.Value.ToString();
            string CostCenter = txtCostcenter.Value.ToString();
            string Namecost = txtnamecost.Value.ToString();
            string Material = txtmaterial.Value.ToString();
            string IssueQty = "";//; txtQty.Value.ToString();

            string UnitpriceST = txtunitpriceST.Value.ToString();
            string AmountST = txtamountST.Value.ToString();

            string UnitpriceAC = txtunitpriceAC.Value.ToString();
            string AmountAC = txtamountAC.Value.ToString();

            string remark = txtremark.Value.ToString();
            string vendorname = txtvendorname.Value.ToString();
            string issueoutsloc = txtissueoutsloc.Value.ToString();

            string typecontent = txttype.Value.ToString();
            string MVT = txtMVT.Value.ToString();
            string typeMVT = txttypeMVT.Value.ToString();

            string index_row = txtrow.Value.ToString();

            string Typeconvert = "";

            if (rblNG.Checked == true)
            {
                Typeconvert = "NGList";
            }
            else
            {
                Typeconvert = "deskstock";
            }
            //hang trong file excel bat buoc phai nhap
            if (Plant == "" && Material == "" && IssueQty == "" && issueoutsloc == "" && index_row == "")  //&& Sloc =="" && CostCenter =="" && Namecost ==""
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Thieu thong tin!'); ", true);
            }
            else
            {
                dt_checkupload = DataConn.StoreFillDS2("Update_setting_toolconvert", System.Data.CommandType.StoredProcedure, Plant, Sloc, CostCenter, Namecost, Material, IssueQty, UnitpriceST, AmountST, UnitpriceAC, AmountAC, remark, vendorname, issueoutsloc, typecontent, MVT, typeMVT, Typeconvert, index_row);
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

        protected void ImportFromExcel(object sender, EventArgs e)
        {
            //string saction_name = filterSanction.Value.ToString();
            //if (saction_name == "")
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban phai nhap ten sanction!'); ", true);
            //}
            //else
            //{
            DataTable dtcheck = new DataTable();
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            if (FileUpload.HasFile)
            {
                if (FileUpload.PostedFile.ContentLength > 0)
                {
                    // Save the uploaded file to the server.
                    FileUpload.SaveAs(Server.MapPath(".") + "\\" + FileUpload.FileName);

                    //new
                    //string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" +
                    //  Server.MapPath(".") + "\\" + FileUpload.FileName +
                    //  "; Extended Properties='Excel 12.0; HDR=YES; IMEX=1;'"; // HDR=YES để xử lý header, IMEX=1 để xử lý cả dữ liệu chuỗi và số

                    string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" +
                      Server.MapPath(".") + "\\" + FileUpload.FileName +
                      "; Extended Properties='Excel 12.0; HDR=NO; IMEX=1;'";

                    OleDbConnection excelConn = null;
                    OleDbDataReader objBulkReader = null;
                    try
                    {
                        DataTable dt_checkupload = new DataTable();
                        DataTable dt_new = new DataTable();
                        int countlap = 0;

                        dt_new.Columns.Add("ID", typeof(Int32));
                        dt_new.Columns.Add("SanctionId", typeof(string));
                        dt_new.Columns.Add("Material", typeof(String));
                        dt_new.Columns.Add("Qty", typeof(float));
                        dt_new.Columns.Add("QtyActual", typeof(float));
                        dt_new.Columns.Add("UnitPrice", typeof(float));
                        dt_new.Columns.Add("Amount", typeof(float));
                        dt_new.Columns.Add("CostCenter", typeof(String));
                        dt_new.Columns.Add("Reason", typeof(String));
                        dt_new.Columns.Add("Plant", typeof(String));
                        dt_new.Columns.Add("Sloc", typeof(String));
                        dt_new.Columns.Add("NameCost", typeof(String));
                        dt_new.Columns.Add("Pallet", typeof(String));
                        dt_new.Columns.Add("Barcode", typeof(String));
                        dt_new.Columns.Add("ScrapSloc", typeof(String));
                        dt_new.Columns.Add("ControlNo", typeof(String));
                        dt_new.Columns.Add("FaTool", typeof(String));

                        //tool covert
                        dt_new.Columns.Add("TypeName", typeof(String));
                        dt_new.Columns.Add("MVT", typeof(String));
                        dt_new.Columns.Add("MoveType", typeof(String));

                        dt_new.Columns.Add("UnitPriceAC", typeof(float));
                        dt_new.Columns.Add("AmountAC", typeof(float));
                        dt_new.Columns.Add("Vendor", typeof(String));
                        //type_convert
                        dt_new.Columns.Add("type_convert", typeof(String));
                        dt_new.Columns.Add("Remark", typeof(String));

                        //them cot vendor name
                        dt_new.Columns.Add("VendorName", typeof(String));
                        

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

                            string bophan = "";
                            if (dr_filter_Cate.Text != "==Section==")
                            {
                                bophan = dr_filter_Cate.Text;
                            }

                            //saction se lay cot D trong file
                            string SanctionId = dtExcelData.Rows[0][3].ToString();// filterSanction.Value.ToString();
                            string SanctionId_shortage = dtExcelData.Rows[0][5].ToString(); //so cuoi cung cua sloc = 8
                            //string SanctionId2 = dtExcelData.Rows[0][3].ToString();

                            // Kiểm tra checkbox có được check không
                            bool check_trung = chksacntion_trung.Checked;
                            if (check_trung)
                            {
                                // trung sanction  //khong xoa //nothing
                            }
                            else 
                            {
                                //xoa sacntion di sau do upload lai
                                DataTable dt_xoa = DataConn.StoreFillDS2("tool_convert_xoa_sanction", System.Data.CommandType.StoredProcedure, SanctionId, SanctionId_shortage);
                            }

                            if (SanctionId == "")
                            {
                                //SanctionId == "" || SanctionId_shortage == ""
                                //bat buoc phai co ca 2 loaij
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban phai nhap ten sanction!'); ", true);
                            }
                            else
                            {
                                string tensanction = "";
                                string Material = "";
                                float Qty = 0;
                                float QtyActual = 0;
                                float UnitPrice = 0;
                                float Amount = 0;
                                string CostCenter = "";
                                string Reason = "";
                                string Plant = "";
                                string Sloc = "";
                                string NameCost = "";
                                string Pallet = "";
                                string Barcode = "";
                                string ScrapSloc = "";
                                string ControlNo = "";
                                string FaTool = "";
                                string type_convert = "";

                                //tool convert
                                string TypeName = "";
                                string MVT = "";
                                string MoveType = "";

                                float UnitPriceAC = 0;
                                float AmountAC = 0;
                                string Vendor = "";
                                string VendorName = "";
                                string Remark = "";


                                //fix cot theo tool convert
                                int col_plan = Int32.Parse(txtplan.Value.ToString());

                                //3 cot tinh theo rule mater
                                //int col_sloc = Int32.Parse(txtsloc.Value.ToString());
                                //int col_costcenter = Int32.Parse(txtCostcenter.Value.ToString());
                                //int col_namecost = Int32.Parse(txtnamecost.Value.ToString());

                                int col_material = Int32.Parse(txtmaterial.Value.ToString());
                                //int col_qty = Int32.Parse(txtQty.Value.ToString());
                                int col_qty = int.TryParse(txtQty.Value?.ToString() ?? "0", out int temp0) ? temp0 : 0;
                                int col_unitpriceST = Int32.Parse(txtunitpriceST.Value.ToString());
                                int col_amountST = Int32.Parse(txtamountST.Value.ToString());

                                //int col_unitpriceAC = Int32.Parse(txtunitpriceAC.Value.ToString());
                                //int col_amountAC = Int32.Parse(txtamountAC.Value.ToString());
                                int col_unitpriceAC = int.TryParse(txtunitpriceAC.Value?.ToString() ?? "0", out int temp1) ? temp1 : 0;
                                int col_amountAC = int.TryParse(txtamountAC.Value?.ToString() ?? "0", out int temp2) ? temp2 : 0;

                                int col_remark = Int32.Parse(txtremark.Value.ToString());
                                //int col_vendorname = Int32.Parse(txtvendorname.Value.ToString());
                                int col_vendorname = int.TryParse(txtvendorname?.Value?.ToString(), out int value) ? value : 0;  //**** co cot vendor nam

                                int col_scraploc = Int32.Parse(txtissueoutsloc.Value.ToString());
                                int row_index = Int32.Parse(txtrow.Value.ToString());
                                //string test3 = dtExcelData.Rows[2][1].ToString();

                                if (rblNG.Checked == true)
                                {
                                    // format file NG list                                    
                                    //kiem tra sanction co trong danh sach chua de lay ra id sanction
                                    DataTable dt_getmater_sloc = new DataTable();
                                    DataTable dt_getmater_MVT = new DataTable();
                                    string type_cost = "psnvcost";
                                    string check_sloc = "";

                                    string debitnote_no = "";
                                    
                                    //for (int i = 2; i < dtExcelData.Rows.Count; i++)
                                    for (int i = row_index; i < dtExcelData.Rows.Count; i++)
                                    {
                                        //countlap = 0;
                                        // check cac cot co du lieu va khong co du lieu
                                        //mahang + Plant + issue sloc + scrap loc + st price
                                        if (dtExcelData.Rows[i][1].ToString() != "" && dtExcelData.Rows[i][2].ToString() != "" && dtExcelData.Rows[i][3].ToString() != "" && dtExcelData.Rows[i][4].ToString() != "" && dtExcelData.Rows[i][5].ToString() != "")
                                        {
                                            Plant = dtExcelData.Rows[i][col_plan].ToString();
                                            ScrapSloc = dtExcelData.Rows[i][col_scraploc].ToString();

                                            check_sloc = ScrapSloc.Substring(3,1);
                                            if (check_sloc == "8")
                                            {
                                                tensanction = SanctionId_shortage;
                                            }
                                            else
                                            {
                                                tensanction = SanctionId;
                                            }

                                            Material = dtExcelData.Rows[i][col_material].ToString();

                                            //**** rule lay ra cot so luong theo PSNV cost or Vendor cost ****** 09.12.2025
                                            //float.TryParse(dtExcelData.Rows[i][col_qty].ToString(), out Qty);         //QtyActual = 0;
                                            //float.TryParse(dtExcelData.Rows[i][col_qty].ToString(), out QtyActual);  //lay luon so actual tren nay => khong can up pallet list

                                            if (dtExcelData.Rows[i][11].ToString() == "" && dtExcelData.Rows[i][12].ToString() == "") // cot Vendor cost bi null or rong
                                            {
                                                float.TryParse(dtExcelData.Rows[i][13].ToString(), out Qty);
                                                float.TryParse(dtExcelData.Rows[i][13].ToString(), out QtyActual);                                                
                                                float.TryParse(dtExcelData.Rows[i][14].ToString(), out Amount); //cot 14
                                            }
                                            else
                                            {
                                                float.TryParse(dtExcelData.Rows[i][11].ToString(), out Qty);
                                                float.TryParse(dtExcelData.Rows[i][11].ToString(), out QtyActual);
                                                //float.TryParse(dtExcelData.Rows[i][col_amountST].ToString(), out Amount); //cot 12
                                                float.TryParse(dtExcelData.Rows[i][12].ToString(), out Amount); //cot 12
                                            }

                                            float.TryParse(dtExcelData.Rows[i][col_unitpriceST].ToString(), out UnitPrice);


                                            //UnitPriceAC = 0;  //rule PUS gui user tu điền 
                                            float.TryParse(dtExcelData.Rows[i][32].ToString(), out UnitPriceAC);
                                            AmountAC = 0; // do ra excel lay theo cong thuc

                                            //Vendor = dtExcelData.Rows[i][17].ToString();  //vendor name
                                            Vendor = dtExcelData.Rows[i][col_vendorname].ToString();  //vendor name

                                            Reason = dtExcelData.Rows[i][col_remark].ToString();    //reason

                                            //rule 4 dua ra cuoc hop 07.01.2026
                                            debitnote_no = dtExcelData.Rows[i][33].ToString(); //cot Debit Note number cot 33  //Vendor cost gia tri cot 11 va 12
                                            if (debitnote_no != "" && dtExcelData.Rows[i][11].ToString() != "" && dtExcelData.Rows[i][12].ToString() != "")  
                                            {
                                                //debitnote_no && Vendor cost && Actual Price  ==> bat buoc phai co
                                                Remark = debitnote_no; //cot remark => so debitnot  //rule 4 cuoc hop ngay 07.01.2026
                                            }
                                            else 
                                            {
                                                //Remark = "Scrap PSNV cost"; //cot remark   => mac dinh
                                                Remark = dtExcelData.Rows[i][18].ToString(); //cot remark => giu nguyen
                                            }                                            

                                            VendorName = dtExcelData.Rows[i][25].ToString();  //cot vendor name

                                            Sloc = "";// dtExcelData.Rows[i][7].ToString();=> lay theo scraploc       //issue sloc   //1185
                                            CostCenter = "";  //lay mater MVT
                                            NameCost = "";  //lay mater sloc

                                            dt_getmater_sloc = DataConn.StoreFillDS2("Get_infor_sloc_pus", System.Data.CommandType.StoredProcedure, ScrapSloc, Plant);
                                            if (dt_getmater_sloc.Rows.Count > 0)
                                            {
                                                Sloc = dt_getmater_sloc.Rows[0]["ScrapSloc"].ToString();
                                                //NameCost = dt_getmater_sloc.Rows[0]["Plant2"].ToString();
                                            }

                                            //truong hop co ca vendor cost & PSNV cost
                                            if (dtExcelData.Rows[i][12].ToString() != "" && dtExcelData.Rows[i][14].ToString() != "")
                                            {
                                                Pallet = "";
                                                Barcode = "";
                                                ControlNo = "";
                                                FaTool = "";
                                                type_convert = "NGList";  //deskstock
                                                //insert thanh 2 dong tu file excel  => co ca 2 cot vendor cost & PSNV cost
                                                MVT = "";
                                                TypeName = "";
                                                MoveType = dtExcelData.Rows[i][2].ToString();  //ROH  or HALB  

                                                dt_getmater_MVT = DataConn.StoreFillDS2("Get_infor_MVT_pus", System.Data.CommandType.StoredProcedure, Sloc, MoveType, "vendorcost");
                                                //quy tac lay ra 3 truong MVT - TypeName
                                                if (dt_getmater_MVT.Rows.Count > 0)
                                                {
                                                    TypeName = dt_getmater_MVT.Rows[0][0].ToString();
                                                    MVT = dt_getmater_MVT.Rows[0][1].ToString();
                                                    CostCenter = dt_getmater_MVT.Rows[0][2].ToString();
                                                    NameCost = dt_getmater_MVT.Rows[0][3].ToString();
                                                }
                                                //insert model moi lan 1
                                                dt_new.Rows.Add(i, tensanction, Material, Qty, QtyActual, UnitPrice, Amount, CostCenter, Reason, Plant, Sloc, NameCost, Pallet, Barcode, ScrapSloc, ControlNo, FaTool, TypeName, MVT, MoveType, UnitPriceAC, AmountAC, Vendor, type_convert, Remark, VendorName);

                                                dt_getmater_MVT = DataConn.StoreFillDS2("Get_infor_MVT_pus", System.Data.CommandType.StoredProcedure, Sloc, MoveType, "psnvcost");
                                                //quy tac lay ra 3 truong MVT - TypeName
                                                if (dt_getmater_MVT.Rows.Count > 0)
                                                {
                                                    TypeName = dt_getmater_MVT.Rows[0][0].ToString();
                                                    MVT = dt_getmater_MVT.Rows[0][1].ToString();
                                                    CostCenter = dt_getmater_MVT.Rows[0][2].ToString();
                                                    NameCost = dt_getmater_MVT.Rows[0][3].ToString();
                                                }
                                                //insert model moi lan 2   => qty, aty actual va unitprice & amount
                                                //lay so luong cot PSNV code
                                                float.TryParse(dtExcelData.Rows[i][13].ToString(), out Qty);
                                                float.TryParse(dtExcelData.Rows[i][13].ToString(), out QtyActual);
                                                //float.TryParse(dtExcelData.Rows[i][col_unitpriceST].ToString(), out UnitPrice);   
                                                float.TryParse(dtExcelData.Rows[i][14].ToString(), out Amount);

                                                dt_new.Rows.Add(i, tensanction, Material, Qty, QtyActual, UnitPrice, Amount, CostCenter, Reason, Plant, Sloc, NameCost, Pallet, Barcode, ScrapSloc, ControlNo, FaTool, TypeName, MVT, MoveType, UnitPriceAC, AmountAC, Vendor, type_convert, Remark, VendorName);
                                                //dt_checkupload = DataConn.StoreFillDS2("Check_upload_scraplist_convert", System.Data.CommandType.StoredProcedure, SanctionId, Material, Qty, Plant, Sloc, Pallet, ScrapSloc, type_convert, ControlNo, FaTool);
                                                //if (dt_checkupload.Rows[0][0].ToString() == "1")
                                                //{
                                                //    //da ton tai roi
                                                //    //nothing
                                                //    countlap = countlap + 1;
                                                //}
                                                //else
                                                //{
                                                //    //insert model moi
                                                //    dt_new.Rows.Add(i, SanctionId, Material, Qty, QtyActual, UnitPrice, Amount, CostCenter, Reason, Plant, Sloc, NameCost, Pallet, Barcode, ScrapSloc, ControlNo, FaTool, TypeName, MVT, MoveType, UnitPriceAC, AmountAC, Vendor, type_convert, Remark, VendorName);
                                                //}                                             
                                            }
                                            else 
                                            {
                                                //truong hop chi co 1
                                                if (dtExcelData.Rows[i][12].ToString() != "")
                                                {
                                                    type_cost = "vendorcost";
                                                }
                                                else
                                                {
                                                    type_cost = "psnvcost";   //(dtExcelData.Rows[i][14].ToString() != ""
                                                }

                                                MVT = "";
                                                TypeName = "";
                                                MoveType = dtExcelData.Rows[i][2].ToString();  //ROH  or HALB  
                                                dt_getmater_MVT = DataConn.StoreFillDS2("Get_infor_MVT_pus", System.Data.CommandType.StoredProcedure, Sloc, MoveType, type_cost);
                                                //quy tac lay ra 3 truong MVT - TypeName
                                                if (dt_getmater_MVT.Rows.Count > 0)
                                                {
                                                    TypeName = dt_getmater_MVT.Rows[0][0].ToString();
                                                    MVT = dt_getmater_MVT.Rows[0][1].ToString();
                                                    CostCenter = dt_getmater_MVT.Rows[0][2].ToString();
                                                    NameCost = dt_getmater_MVT.Rows[0][3].ToString();
                                                }

                                                Pallet = "";
                                                Barcode = "";
                                                ControlNo = "";
                                                FaTool = "";

                                                type_convert = "NGList";  //deskstock

                                                //float.TryParse(dtExcelData.Rows[i][32].ToString(), out UnitPriceAC);

                                                //insert model moi ==> khong check trung ma xoa di up moi lai
                                                dt_new.Rows.Add(i, tensanction, Material, Qty, QtyActual, UnitPrice, Amount, CostCenter, Reason, Plant, Sloc, NameCost, Pallet, Barcode, ScrapSloc, ControlNo, FaTool, TypeName, MVT, MoveType, UnitPriceAC, AmountAC, Vendor, type_convert, Remark, VendorName);

                                                //dt_checkupload = DataConn.StoreFillDS2("Check_upload_scraplist_convert", System.Data.CommandType.StoredProcedure, SanctionId, Material, Qty, Plant, Sloc, Pallet, ScrapSloc, type_convert, ControlNo, FaTool);
                                                //if (dt_checkupload.Rows[0][0].ToString() == "1")
                                                //{
                                                //    //da ton tai roi
                                                //    //nothing
                                                //    countlap = countlap + 1;
                                                //}
                                                //else
                                                //{
                                                   
                                                //}
                                            }
                                            
                                        }

                                        //mahang + Plant + issue sloc + scrap loc + st price  ==> Tong scrap (10)
                                        if (dtExcelData.Rows[i][1].ToString() == "" && dtExcelData.Rows[i][2].ToString() == "" && dtExcelData.Rows[i][3].ToString() == "" && dtExcelData.Rows[i][4].ToString() == "" && dtExcelData.Rows[i][5].ToString() == "" && dtExcelData.Rows[i][6].ToString() == "")
                                        {
                                            break;
                                        }
                                    }
                                }
                                else if (rblDesktock.Checked == true)
                                {
                                    // format file NG list                                    
                                    //kiem tra sanction co trong danh sach chua de lay ra id sanction
                                    DataTable dt_getmater_sloc = new DataTable();
                                    DataTable dt_getmater_MVT = new DataTable();
                                    DataTable dt_getmater_vendor = new DataTable();
                                    string type_cost = "psnvcost";

                                    //for (int i = 2; i < dtExcelData.Rows.Count; i++)
                                    for (int i = row_index; i < dtExcelData.Rows.Count; i++)
                                    {
                                        //countlap = 0;
                                        // check cac cot co du lieu va khong co du lieu
                                        //mahang + Plant + issue sloc + scrap loc + st price
                                        if (dtExcelData.Rows[i][1].ToString() != "" && dtExcelData.Rows[i][2].ToString() != "" && dtExcelData.Rows[i][3].ToString() != "" && dtExcelData.Rows[i][4].ToString() != "" && dtExcelData.Rows[i][5].ToString() != "")
                                        {

                                            Plant = dtExcelData.Rows[i][col_plan].ToString();
                                            ScrapSloc = dtExcelData.Rows[i][col_scraploc].ToString();

                                            Material = dtExcelData.Rows[i][col_material].ToString();

                                            float.TryParse(dtExcelData.Rows[i][col_qty].ToString(), out Qty);         //QtyActual = 0;
                                            float.TryParse(dtExcelData.Rows[i][col_qty].ToString(), out QtyActual);  //lay luon so actual tren nay => khong can up pallet list

                                            float.TryParse(dtExcelData.Rows[i][col_unitpriceST].ToString(), out UnitPrice);
                                            float.TryParse(dtExcelData.Rows[i][col_amountST].ToString(), out Amount);

                                            float.TryParse(dtExcelData.Rows[i][col_unitpriceAC].ToString(), out UnitPriceAC);
                                            float.TryParse(dtExcelData.Rows[i][col_amountAC].ToString(), out AmountAC);


                                            //Vendor = dtExcelData.Rows[i][17].ToString();  //vendor name
                                            VendorName = dtExcelData.Rows[i][col_vendorname].ToString();  //vendor name   ??? lay du lieu cot vendor name
                                            dt_getmater_vendor = DataConn.StoreFillDS2("Get_infor_vendor_desktock", System.Data.CommandType.StoredProcedure, VendorName);
                                            if (dt_getmater_vendor.Rows.Count > 0)
                                            {
                                                Vendor = dt_getmater_vendor.Rows[0][0].ToString();
                                            }
                                            else
                                            {
                                                Vendor = ""; //khong co trong mater 
                                            }

                                            //Reason = dtExcelData.Rows[i][19].ToString();    //remark 
                                            Reason = dtExcelData.Rows[i][col_remark].ToString();    //remark 
                                            Remark = "";
                                            Sloc = "";// dtExcelData.Rows[i][7].ToString();=> lay theo scraploc       //issue sloc   //1185
                                            CostCenter = "";  //lay mater MVT
                                            NameCost = "";  //lay mater sloc

                                            //dt_getmater_sloc = DataConn.StoreFillDS2("Get_infor_sloc_pus", System.Data.CommandType.StoredProcedure, ScrapSloc, Plant);
                                            dt_getmater_sloc = DataConn.StoreFillDS2("Get_infor_sloc_pus_desktock", System.Data.CommandType.StoredProcedure, ScrapSloc, Plant);
                                            if (dt_getmater_sloc.Rows.Count > 0)
                                            {
                                                //1. lay theo plant, Cate, MTyp,  ==> Issue sloc lay ra Dau sloc (vi du VB01 => dau 5D)
                                                //2. du vao cot PSNV cost hay la JP cost de xac dinh MVT name
                                                //3. khi co dau ra la 5D => xac dinh sloc nao? theo cate, type rohs/halb va MVT.
                                                Sloc = dt_getmater_sloc.Rows[0]["SlocPus"].ToString();        //????   => lay ra sloc ???                                                   
                                               // NameCost = dt_getmater_sloc.Rows[0]["Plant2"].ToString();       //????   => lay ra Name cost ???
                                            }

                                            //hang desktock cot JP cost co du lieu => type_cost = "vendorcost";
                                            if (dtExcelData.Rows[i][40].ToString() != "" && dtExcelData.Rows[i][41].ToString() != "" && dtExcelData.Rows[i][40].ToString() != "-")
                                            {
                                                type_cost = "vendorcost";
                                            }
                                            else
                                            {
                                                type_cost = "psnvcost";   //(dtExcelData.Rows[i][14].ToString() != ""
                                            }

                                            MVT = "";
                                            TypeName = "";
                                            MoveType = dtExcelData.Rows[i][6].ToString();  //ROH  or halb     ???? hang desktock khong co MVT & TypeName & Movetype
                                            dt_getmater_MVT = DataConn.StoreFillDS2("Get_infor_MVT_pus", System.Data.CommandType.StoredProcedure, Sloc, MoveType, type_cost);
                                            //quy tac lay ra 3 truong MVT - TypeName
                                            if (dt_getmater_MVT.Rows.Count > 0)
                                            {
                                                TypeName = dt_getmater_MVT.Rows[0][0].ToString();
                                                MVT = dt_getmater_MVT.Rows[0][1].ToString();
                                                CostCenter = dt_getmater_MVT.Rows[0][2].ToString();
                                                NameCost = dt_getmater_MVT.Rows[0][3].ToString();
                                            }

                                            Pallet = "";
                                            Barcode = "";
                                            ControlNo = "";
                                            FaTool = "";

                                            type_convert = "deskstock";  //deskstock
                                            tensanction = SanctionId;  // mac dinh o cot 3 dong 1
                                            //insert model moi
                                            dt_new.Rows.Add(i, tensanction, Material, Qty, QtyActual, UnitPrice, Amount, CostCenter, Reason, Plant, Sloc, NameCost, Pallet, Barcode, ScrapSloc, ControlNo, FaTool, TypeName, MVT, MoveType, UnitPriceAC, AmountAC, Vendor, type_convert, Remark, VendorName);

                                            //dt_checkupload = DataConn.StoreFillDS2("Check_upload_scraplist_convert", System.Data.CommandType.StoredProcedure, SanctionId, Material, Qty, Plant, Sloc, Pallet, ScrapSloc, type_convert, ControlNo, FaTool);
                                            //if (dt_checkupload.Rows[0][0].ToString() == "1")
                                            //{
                                            //    //da ton tai roi
                                            //    //nothing
                                            //    countlap = countlap + 1;
                                            //}
                                            //else
                                            //{
                                                
                                            //}
                                        }

                                        //mahang + Plant + issue sloc + scrap loc + st price  ==> Tong scrap (10)
                                        if (dtExcelData.Rows[i][1].ToString() == "" && dtExcelData.Rows[i][2].ToString() == "" && dtExcelData.Rows[i][3].ToString() == "" && dtExcelData.Rows[i][4].ToString() == "" && dtExcelData.Rows[i][5].ToString() == "" && dtExcelData.Rows[i][6].ToString() == "")
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban chua chon template upload!!'); ", true);
                                }

                                //upload buckcopy tai day
                                //string sqlConnStr = "Data Source=10.92.186.30;Persist Security Info=False;" +
                                //                "Initial Catalog=ScrapSystem;User Id=sa;Password=Psnvdb2013;" +
                                //                "Connect Timeout=30;";

                                string sqlConnStr = DataConn.source2;

                                //string sqlConnStr = @"Data Source=DESKTOP-P69S4E5;
                                //    Initial Catalog = ScrapSystem;
                                //    Integrated Security = True;
                                //    Connect Timeout = 30;
                                //    TrustServerCertificate = True; ";

                                using (SqlConnection con = new SqlConnection(sqlConnStr))
                                {
                                    con.Open();

                                    // Initialize SqlBulkCopy.
                                    using (SqlBulkCopy oSqlBulk = new SqlBulkCopy(con))
                                    {
                                        oSqlBulk.DestinationTableName = "ScrapDetails_convert"; // bang covnert
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
                                dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList_sacntion3", System.Data.CommandType.StoredProcedure, tensanction, _fromdate, _todate, filterSanction.Value.ToString());   //bang convert


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
            else 
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban Chon file!'); ", true);
            }

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



    }
}