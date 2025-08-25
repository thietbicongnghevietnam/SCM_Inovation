using FreeLayout.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreeLayout
{
    public partial class frmMaterVessel : System.Web.UI.Page
    {
        public DataTable dt_plan = new DataTable();
        public DataTable dt_getmodel = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);
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
            string cate = cateid.Text;
            string Area = Areaid.Text;
            string Country = Countryid.Text;
            string DestCity = DestCityid.Text;
            string DestCityName = DestCityNameid.Text;
            string PIC = PICid.Text;
            string Consignee = Consigneeid.Text;
            string FCL_Ex_factory = FCL_Ex_factoryid.Text;
            string FCL_ETD = FCL_ETDid.Text;
            string FCL_ETA = FCL_ETAid.Text;
            string LLC_Ex_factory = LLC_Ex_factoryid.Text;
            string LLC_ETD = LLC_ETDid.Text;
            string LLC_ETA = LLC_ETAid.Text;
            string AIR_Ex_factory = AIR_Ex_factoryid.Text;
            string AIR_ETD = AIR_ETDid.Text;
            string AIR_ETA = AIR_ETAid.Text;
            string Special_exfactory_date = Special_exfactory_dateid.Text;
            string SpecialETD_week = SpecialETD_weekid.Text;
            string Special_ETA_Date = Special_ETA_Dateid.Text;
            string Can_combine = Can_combineid.Text;
            //string userid = Session["username"].ToString();

            DataTable dtinsert = new DataTable();
            dtinsert = DataConn.StoreFillDS("Insert_materVessel_SCM", System.Data.CommandType.StoredProcedure, cate, Area, Country, DestCity, DestCityName, PIC, Consignee, FCL_Ex_factory, FCL_ETD, FCL_ETA, LLC_Ex_factory, LLC_ETD, LLC_ETA, AIR_Ex_factory, AIR_ETD, AIR_ETA, Special_exfactory_date, SpecialETD_week, Special_ETA_Date, Can_combine);
            if (dtinsert.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);
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
            string cate = idcate.Text;
            string Area = idArea.Text;
            string Country = idCountry.Text;
            string DestCity = idDestCity.Text;
            string DestCityName = idDestCityName.Text;
            string PIC = idPIC.Text;
            string Consignee = idConsignee.Text;
            string FCL_Ex_factory = idFCL_Ex_factory.Text;
            string FCL_ETD = idFCL_ETD.Text;
            string FCL_ETA = idFCL_ETA.Text;
            string LLC_Ex_factory = idLLC_Ex_factory.Text;
            string LLC_ETD = idLLC_ETD.Text;
            string LLC_ETA = idLLC_ETA.Text;
            string AIR_Ex_factory = idAIR_Ex_factory.Text;
            string AIR_ETD = idAIR_ETD.Text;
            string AIR_ETA = idAIR_ETA.Text;
            string Special_exfactory_date = idSpecial_exfactory_date.Text;
            string SpecialETD_week = idSpecialETD_week.Text;
            string Special_ETA_Date = idSpecial_ETA_Date.Text;
            string Can_combine = idCan_combine.Text;
            //string userid = Session["username"].ToString();

            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS("Update_materVessel_SCM", System.Data.CommandType.StoredProcedure, id, cate, Area, Country, DestCity, DestCityName, PIC, Consignee, FCL_Ex_factory, FCL_ETD, FCL_ETA, LLC_Ex_factory, LLC_ETD, LLC_ETA, AIR_Ex_factory, AIR_ETD, AIR_ETA, Special_exfactory_date, SpecialETD_week, Special_ETA_Date, Can_combine);

            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);
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
            string model = txtCountry_del.Text;

            //string username = Session["username"].ToString();
            //string role_ = Session["role"].ToString();
            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS("Delete_materVessel_SCM", System.Data.CommandType.StoredProcedure, id); //username
            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);
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

            dt_plan = DataConn.StoreFillDS("Select_Mater_VesselSCM", System.Data.CommandType.StoredProcedure);
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