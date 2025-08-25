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
            //string Category = cateid.Text;
            //string userid = Session["username"].ToString();

            //DataTable dtinsert = new DataTable();
            //dtinsert = DataConn.StoreFillDS("Insert_materAQ2", System.Data.CommandType.StoredProcedure, ModelNo, Category, userid);
            //if (dtinsert.Rows[0][0].ToString() == "1")
            //{
            //    dt = DataConn.StoreFillDS("Select_mater_AQ2", System.Data.CommandType.StoredProcedure);
            //    // dt_leadtime = DataConn.StoreFillDS("Select_mater_listime_cate", System.Data.CommandType.StoredProcedure, cateid_);
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, kiểm tra lại thông tin!'); ", true);
            //}

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
            
            //string userid = Session["username"].ToString();

            //DataTable dtupdate = new DataTable();
            //dtupdate = DataConn.StoreFillDS("Update_kehoachsanxuat", System.Data.CommandType.StoredProcedure, id, model, ngaysx, line, giosanxuat);
            ////bo trang thai tach kip12b va ca3  => tinh gio san xuat tren stored
            ////dtupdate = DataConn.StoreFillDS("Update_kehoachsanxuat", System.Data.CommandType.StoredProcedure, id, model, ngaysx, line, giosanxuat, trangthaitach);
            //if (dtupdate.Rows[0][0].ToString() == "1")
            //{
            //    //dt_hanghoa = DataConn.StoreFillDS("NH_danhmuchanghoa", System.Data.CommandType.StoredProcedure);
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
            string model = txModel_del.Text;

            //string username = Session["username"].ToString();
            //string role_ = Session["role"].ToString();

            //if (role_ == "Admin")
            //{
            //    DataTable dtupdate = new DataTable();
            //    dtupdate = DataConn.StoreFillDS("Xoaid_kehoachsanxuat", System.Data.CommandType.StoredProcedure, id, username);
            //    if (dtupdate.Rows[0][0].ToString() == "1")
            //    {
            //        //dt_hanghoa = DataConn.StoreFillDS("NH_danhmuchanghoa", System.Data.CommandType.StoredProcedure);
            //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            //    }
            //    else
            //    {
            //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, kiểm tra lại thông tin!'); ", true);
            //    }
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