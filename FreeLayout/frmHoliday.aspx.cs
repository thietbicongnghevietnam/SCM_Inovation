using FreeLayout.App_Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreeLayout
{
    public partial class frmHoliday : System.Web.UI.Page
    {
        public DataTable dt_image = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            dt_image = DataConn.StoreFillDS("Select_holiday_vessel", System.Data.CommandType.StoredProcedure);
            //LoadCategory();
            if (!IsPostBack)
            {
                LoadCategory();  //phai cho vao postbac thi moi nhân gia tri khi them moi
            }
        }

        private void LoadCategory()
        {
            //string connStr = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(DataConn.source))
            //using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT CategoryID, Description FROM tblCATEGORY";  // đổi tên cột cho đúng
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cateid1.DataSource = dt;
                cateid1.DataTextField = "Description";   // cột hiển thị
                cateid1.DataValueField = "CategoryID";    // cột lấy value
                cateid1.DataBind();

                cateid1.Items.Insert(0, new ListItem("-- Select Category --", ""));
            }
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            //string filterMaterialid = filterMaterial.Value;

            dt_image = DataConn.StoreFillDS("Select_holiday_vessel", System.Data.CommandType.StoredProcedure);
        }

        public void themhanghoa(object sender, EventArgs e)
        {
            //string cateid_ = cateid.Text;
            //string cateid_2 = cateid1.Text;  // lấy Id
            string cateid_ = cateid1.SelectedItem.Text;  // lấy Name nếu cần
            string datefromid_ = datefromid2.Value;
            string datetoid_ = datetoid2.Value;
            string weekid_ = weekid.Text;

            ////string userid = Session["username"].ToString();
            if (cateid_ != "-- Select Category --")
            {
                DataTable dtinsert = new DataTable();
                dtinsert = DataConn.StoreFillDS("Insert_holiday_vessel", System.Data.CommandType.StoredProcedure, cateid_, datefromid_, datetoid_, weekid_);
                if (dtinsert.Rows[0][0].ToString() == "1")
                {
                    dt_image = DataConn.StoreFillDS("Select_holiday_vessel", System.Data.CommandType.StoredProcedure);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, check again!'); ", true);
                }
            }
            else 
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Data is null!'); ", true);
            }
        }

        public void Updatethongtin(object sender, EventArgs e)
        {
            string id = IDedit.Text;
            string datefrom = iddatefrom1.Value;
            string dateto = iddateto1.Value;
            string week = idweek.Text;

            ////string userid = Session["username"].ToString();

            if (datefrom != "" && dateto != "")
            {
                DataTable dtupdate = new DataTable();
                dtupdate = DataConn.StoreFillDS("Update_holiday_vessel", System.Data.CommandType.StoredProcedure, id, datefrom, dateto, week);

                if (dtupdate.Rows[0][0].ToString() == "1")
                {
                    dt_image = DataConn.StoreFillDS("Select_holiday_vessel", System.Data.CommandType.StoredProcedure);
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
            string id = txtid_del.Text;
            //string material = txMaterialName_del.Text;
            //////string username = Session["username"].ToString();
            //////string role_ = Session["role"].ToString();

            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS("Delete_holiday_vessel", System.Data.CommandType.StoredProcedure, id);  //username
            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_image = DataConn.StoreFillDS("Select_holiday_vessel", System.Data.CommandType.StoredProcedure);
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