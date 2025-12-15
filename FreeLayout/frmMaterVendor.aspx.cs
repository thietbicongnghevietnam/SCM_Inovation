using FreeLayout.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Interop;

namespace FreeLayout
{
    public partial class frmVendor : System.Web.UI.Page
    {
        public DataTable dt_vendor = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            dt_vendor = DataConn.StoreFillDS2("Select_mater_vendor", System.Data.CommandType.StoredProcedure);
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string filterVendorid = filterVendor.Value;

            dt_vendor = DataConn.StoreFillDS2("Select_mater_vendor", System.Data.CommandType.StoredProcedure);

            //string category = dr_filter_Cate.SelectedValue;
            //if (_fromdate == "" || _todate == "")
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Date is null!'); ", true);
            //}
            //else
            //{
            //    //dt_vendor = DataConn.StoreFillDS2("Select_holiday_vessel", System.Data.CommandType.StoredProcedure, _fromdate, _todate);

            //}


        }

        public void themhanghoa(object sender, EventArgs e)
        {
            //VendorCode, VendorName, Vendor_Address, PIC_Vendor, Country_Origin
            string VendorCode = VendorCodeid.Text;
            string VendorName = VendorNameid.Text;
            string Vendor_Address = Vendor_Addressid.Text;
            string PIC_Vendor = PIC_Vendorid.Text;
            string Country_Origin = Country_Originid.Text;

            ////string userid = Session["username"].ToString();

            DataTable dtinsert = new DataTable();
            dtinsert = DataConn.StoreFillDS2("Insert_mater_vendor", System.Data.CommandType.StoredProcedure, VendorCode, VendorName, Vendor_Address, PIC_Vendor, Country_Origin);
            if (dtinsert.Rows[0][0].ToString() == "1")
            {
                dt_vendor = DataConn.StoreFillDS2("Select_mater_vendor", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, check again!'); ", true);
            }

        }

        public void Updatethongtin(object sender, EventArgs e)
        {
            string VendorCode = idVendorCode.Text;
            string VendorName = idVendorName.Text;
            string Vendor_Address = idVendor_Address.Text;
            string PIC_Vendor = idPIC_Vendor.Text;
            string Country_Origin = idCountry_Origin.Text;

            ////string userid = Session["username"].ToString();

            if (VendorCode != "" && VendorName != "")
            {
                DataTable dtupdate = new DataTable();
                dtupdate = DataConn.StoreFillDS2("Update_Mater_vendor", System.Data.CommandType.StoredProcedure, VendorCode, VendorName, Vendor_Address, PIC_Vendor, Country_Origin);

                if (dtupdate.Rows[0][0].ToString() == "1")
                {
                    dt_vendor = DataConn.StoreFillDS2("Select_mater_vendor", System.Data.CommandType.StoredProcedure);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
                }
            }
            else 
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, data null!'); ", true);
            }            
        }

        public void Xoathongtin(object sender, EventArgs e)
        {
            string id = txtVendorCode.Text;
            //string material = txMaterialName_del.Text;
            //////string username = Session["username"].ToString();
            //////string role_ = Session["role"].ToString();

            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS2("Delete_Mater_vendor", System.Data.CommandType.StoredProcedure, id);  //username
            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_vendor = DataConn.StoreFillDS2("Select_mater_vendor", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
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