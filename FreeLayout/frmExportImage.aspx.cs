using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreeLayout.App_Code;

namespace FreeLayout
{
    public partial class frmExportImage : System.Web.UI.Page
    {
        public DataTable dt_image = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt_image = DataConn.StoreFillDS2("Select_Export_ScarpImage", System.Data.CommandType.StoredProcedure);
                //Date1.Value = DateTime.Now.ToString("dd-MM-yyyy");
                //ngaychiid.Value = DateTime.Now.ToString("dd-MM-yyyy");
                //dtcate = DataConnScrap.StoreFillDS("pro_get_categogy", System.Data.CommandType.StoredProcedure);
                //DataRow newRow1 = dtcate.NewRow();
                //newRow1["Description"] = "==Categogy==";
                //dtcate.Rows.InsertAt(newRow1, 0);
                //dr_filter_Cate.DataSource = dtcate;
                //dr_filter_Cate.DataBind();
                gvPalletImage.EnableViewState = false;
            }
        }

        //public void themhanghoa(object sender, EventArgs e)
        //{
        //    //string id = IDedit.Text;
        //    string material = Mateialid.Text;
        //    string EnglishName = EnglishNameid.Text;
        //    string VietNameseName = VietNameseNameid.Text;

        //    ////string userid = Session["username"].ToString();

        //    DataTable dtinsert = new DataTable();
        //    dtinsert = DataConn.StoreFillDS2("Insert_mater_Materialname", System.Data.CommandType.StoredProcedure, material, EnglishName, VietNameseName);
        //    if (dtinsert.Rows[0][0].ToString() == "1")
        //    {
        //        dt_plan = DataConn.StoreFillDS2("Select_Mater_MaterialName", System.Data.CommandType.StoredProcedure);
        //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, kiểm tra lại thông tin!'); ", true);
        //    }

        //}

        public void Updatethongtin(object sender, EventArgs e)
        {
            //string id = IDedit.Text;
            //string material = idMaterial.Text;
            //string EnglishName = idEnglishName.Text;
            //string VietNameseName = idVietNameseName.Text;

            ////string userid = Session["username"].ToString();

            //DataTable dtupdate = new DataTable();
            //dtupdate = DataConn.StoreFillDS2("Update_mater_materialname", System.Data.CommandType.StoredProcedure, id, material, EnglishName, VietNameseName);

            //if (dtupdate.Rows[0][0].ToString() == "1")
            //{
            //    dt_plan = DataConn.StoreFillDS2("Select_Mater_MaterialName_loc", System.Data.CommandType.StoredProcedure, material);
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, kiểm tra lại thông tin!'); ", true);
            //}
        }

        public void Xoathongtin(object sender, EventArgs e)
        {
            string id = txtid_del.Text;
            //string material = txMaterialName_del.Text;
            string userid = txtuser_del.Text.ToString();

            if (userid == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Bạn chưa nhập user để xóa!'); ", true);
            }
            else 
            {
                DataTable dtupdate = new DataTable();
                dtupdate = DataConn.StoreFillDS2("Delete_ScarpImage_Log", System.Data.CommandType.StoredProcedure, id, userid);  //username
                if (dtupdate.Rows[0][0].ToString() == "1")
                {
                    dt_image = DataConn.StoreFillDS2("Select_Export_ScarpImage", System.Data.CommandType.StoredProcedure);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
                }
            }
            
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string filterMaterialid = filterMaterial.Value;
            string filterPalletid = filterPalletNo.Value;

            //string category = dr_filter_Cate.SelectedValue;
            if (_fromdate == "" || _todate == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Date is null!'); ", true);
            }
            else
            {
                dt_image = DataConn.StoreFillDS2("Select_Export_ScarpImage_loc", System.Data.CommandType.StoredProcedure, _fromdate, _todate, filterMaterialid, filterPalletid);
            }
                

        }

        protected void btnCheckImage_Click(object sender, EventArgs e)
        {
            LoadPalletImage();

            // BẮT BUỘC gọi JS mở modal
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "ShowModal",
                "$('#exampleModal').modal('show');",
                true
            );

        }

        protected void gvPalletImage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int hasA = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "HasA"));
                int hasB = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "HasB"));

                Literal ltNG = (Literal)e.Row.FindControl("ltNG");

                if (hasA == 0 || hasB == 0)
                {
                    ltNG.Text = "<i class='fas fa-times-circle text-danger' title='NG'></i>";
                }
                else
                {
                    ltNG.Text = "<i class='fas fa-check-circle text-success' title='OK'></i>";
                }
            }
        }



        private void LoadPalletImage()
        {
            string sanctionID = filterMaterial.Value;

            DataTable dt_check_image = DataConn.StoreFillDS2("sp_CheckImagePallet", System.Data.CommandType.StoredProcedure, sanctionID);

            gvPalletImage.DataSource = dt_check_image;
            gvPalletImage.DataBind();

            //string connStr = ConfigurationManager.ConnectionStrings["YourConn"].ConnectionString;

            //using (SqlConnection conn = new SqlConnection(connStr))
            //using (SqlCommand cmd = new SqlCommand("sp_CheckImagePallet", conn))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@SanctionId", 9);

            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);

            //    gvPalletImage.DataSource = dt;
            //    gvPalletImage.DataBind();
            //}
        }


        //protected void Check_image_Click(object sender, EventArgs e) 
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}


        //protected void ImportFromExcel(object sender, EventArgs e) 
        //{

        //}

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