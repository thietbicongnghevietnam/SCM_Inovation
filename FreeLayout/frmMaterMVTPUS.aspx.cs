using FreeLayout.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreeLayout
{
    public partial class frmMaterMVTPUS : System.Web.UI.Page
    {
        public DataTable dt_image = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            dt_image = DataConn.StoreFillDS2("Select_mater_MVT_Pus", System.Data.CommandType.StoredProcedure);
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            //string _fromdate = Request.Form[Date1.UniqueID];
            //string _todate = Request.Form[ngaychiid.UniqueID];
            //string filterMaterialid = filterMaterial.Value;

            dt_image = DataConn.StoreFillDS2("Select_mater_MVT_Pus", System.Data.CommandType.StoredProcedure);

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

            string Plant = Plantid.Text;
            string Sloc = Slocid.Text;
            string Description = Descriptionid.Text;
            string Costcenter = Costcenterid.Text;
            string NameCode = NameCodeid.Text;
            string TypeRoshHalb = TypeRoshHalbid.Text;
            string Typevendor = Typevendorid.Text;
            string MVTvendor = MVTvendorid.Text;
            string TypePSNV = TypePSNVid.Text;
            string MVTPSNV = MVTPSNVid.Text;
            ////string userid = Session["username"].ToString();

            DataTable dtinsert = new DataTable();
            dtinsert = DataConn.StoreFillDS2("Insert_mater_MVT_pus", System.Data.CommandType.StoredProcedure, Plant, Sloc, Description, Costcenter, NameCode, TypeRoshHalb, Typevendor, MVTvendor, TypePSNV, MVTPSNV);
            if (dtinsert.Rows[0][0].ToString() == "1")
            {
                dt_image = DataConn.StoreFillDS2("Select_mater_MVT_Pus", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, data exit!'); ", true);
            }

        }

        public void Xoathongtin(object sender, EventArgs e)
        {
            string id = txtid_del.Text;
            //string material = txMaterialName_del.Text;
            //////string username = Session["username"].ToString();
            //////string role_ = Session["role"].ToString();

            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS2("Delete_mater_MVT_pus", System.Data.CommandType.StoredProcedure, id);  //username
            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_image = DataConn.StoreFillDS2("Select_mater_MVT_Pus", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
            }
        }

        public void Updatethongtin(object sender, EventArgs e)
        {
            string id = IDedit.Text;

            string Plant = idPlant.Text;
            string Sloc = idSloc.Text;
            string Description = idDescription.Text;
            string Costcenter = idCostcenter.Text;
            string NameCode = idNameCode.Text;
            string TypeRoshHalb = idTypeRoshHalb.Text; ///idTypeRoshHalb
            string Typevendor = idTypevendor.Text;
            string MVTvendor = idMVTvendor.Text;
            string TypePSNV = idTypePSNV.Text;
            string MVTPSNV = idMVTPSNV.Text;

            ////string userid = Session["username"].ToString();

            if (Sloc != "" && Costcenter != "" && Plant !="" && NameCode !="")
            {
                DataTable dtupdate = new DataTable();
                dtupdate = DataConn.StoreFillDS2("Update_mater_MVT_pus", System.Data.CommandType.StoredProcedure, id, Plant, Sloc, Description, Costcenter, NameCode, TypeRoshHalb, Typevendor, MVTvendor, TypePSNV, MVTPSNV);

                if (dtupdate.Rows[0][0].ToString() == "1")
                {
                    dt_image = DataConn.StoreFillDS2("Select_mater_MVT_Pus", System.Data.CommandType.StoredProcedure);
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

    }
}