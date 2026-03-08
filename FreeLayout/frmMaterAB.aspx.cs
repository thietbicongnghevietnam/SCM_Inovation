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
    public partial class frmMaterAB : System.Web.UI.Page
    {
        public DataTable dt_image = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            dt_image = DataConn.StoreFillDS2("Select_form_AB", System.Data.CommandType.StoredProcedure);
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            //string _fromdate = Request.Form[Date1.UniqueID];
            //string _todate = Request.Form[ngaychiid.UniqueID];
            string filtertypename = filterMaterial.Value;

            dt_image = DataConn.StoreFillDS2("Select_form_AB_loc", System.Data.CommandType.StoredProcedure, filtertypename);

            //string category = dr_filter_Cate.SelectedValue;
            //if (_fromdate == "" || _todate == "")
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Date is null!'); ", true);
            //}
            //else
            //{
            //    //dt_image = DataConn.StoreFillDS("Select_holiday_vessel", System.Data.CommandType.StoredProcedure, _fromdate, _todate);

            //}


        }

        public void themhanghoa(object sender, EventArgs e)
        {
           
            string NameTemplate = NameTemplateid.Text;
            string TypeID = TypeIDid.Text;
            string TypeName = TypeNameid.Text;
            string Description = Descriptionid.Text;
            string AccountCost = AccountCostid.Text;

            ////string userid = Session["username"].ToString();
            //chuc nang ke toan
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Contact ACC department!'); ", true);

            //DataTable dtinsert = new DataTable();
            //dtinsert = DataConn.StoreFillDS2("Insert_Form_AB", System.Data.CommandType.StoredProcedure, NameTemplate, TypeID, TypeName, AccountCost, Description);
            //if (dtinsert.Rows[0][0].ToString() == "1")
            //{
            //    dt_image = DataConn.StoreFillDS2("Select_form_AB", System.Data.CommandType.StoredProcedure);
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, check again!'); ", true);
            //}
        }

        public void Updatethongtin(object sender, EventArgs e)
        {
            string id = IDedit.Text;
            string NameTemplate = idNameTemplate.Text;
            string TypeID = idTypeID.Text;
            string TypeName = idTypeName.Text;
            string AccountCost = idAccountCost.Text;
            string Description = idDescription.Text;

            ////string userid = Session["username"].ToString();
            
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Contact ACC department!'); ", true);

            //if (NameTemplate != "" && TypeID != "" && TypeName != "")
            //{
            //    DataTable dtupdate = new DataTable();
            //    dtupdate = DataConn.StoreFillDS2("Update_Form_AB", System.Data.CommandType.StoredProcedure, id, NameTemplate, TypeID, TypeName, AccountCost, Description);

            //    if (dtupdate.Rows[0][0].ToString() == "1")
            //    {
            //        dt_image = DataConn.StoreFillDS2("Select_form_AB", System.Data.CommandType.StoredProcedure);
            //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            //    }
            //    else
            //    {
            //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
            //    }
            //}
            //else 
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, data null!'); ", true);
            //}            
        }

        public void Xoathongtin(object sender, EventArgs e)
        {
            string id = txtid_del.Text;
            //string material = txMaterialName_del.Text;
            //////string username = Session["username"].ToString();
            //////string role_ = Session["role"].ToString();
            //chuc nang ke toan
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Contact ACC department!'); ", true);

            //DataTable dtupdate = new DataTable();
            //dtupdate = DataConn.StoreFillDS2("Delete_from_AB", System.Data.CommandType.StoredProcedure, id);  //username
            //if (dtupdate.Rows[0][0].ToString() == "1")
            //{
            //    dt_image = DataConn.StoreFillDS2("Select_form_AB", System.Data.CommandType.StoredProcedure);
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
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