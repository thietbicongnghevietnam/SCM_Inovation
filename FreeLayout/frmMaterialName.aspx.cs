using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        

        protected void ImportFromExcel(object sender, EventArgs e) 
        {
        
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