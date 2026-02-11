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
    public partial class frmMaterSlocPUS : System.Web.UI.Page
    {
        public DataTable dt_image = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            dt_image = DataConn.StoreFillDS2("Select_mater_Sloc_Pus", System.Data.CommandType.StoredProcedure);
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            //string _fromdate = Request.Form[Date1.UniqueID];
            //string _todate = Request.Form[ngaychiid.UniqueID];
            //string filterMaterialid = filterMaterial.Value;

            dt_image = DataConn.StoreFillDS2("Select_mater_Sloc_Pus", System.Data.CommandType.StoredProcedure);

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
            string Plant2 = Plant2id.Text;
            string SlocPus = SlocPusid.Text;
            string Category = Categoryid.Text;
            string Description = Descriptionid.Text;
            string ScrapSloc = ScrapSlocid.Text;
            string MVT = MVTid.Text;

            ////string userid = Session["username"].ToString();

            DataTable dtinsert = new DataTable();
            dtinsert = DataConn.StoreFillDS2("Insert_mater_sloc_pus", System.Data.CommandType.StoredProcedure, Plant, Plant2, SlocPus, Category, Description, ScrapSloc,MVT);
            if (dtinsert.Rows[0][0].ToString() == "1")
            {
                dt_image = DataConn.StoreFillDS2("Select_mater_Sloc_Pus", System.Data.CommandType.StoredProcedure);
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
            dtupdate = DataConn.StoreFillDS2("Delete_mater_sloc_pus", System.Data.CommandType.StoredProcedure, id);  //username
            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_image = DataConn.StoreFillDS2("Select_mater_Sloc_Pus", System.Data.CommandType.StoredProcedure);
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
            string Plant2 = idPlant2.Text;
            string SlocPus = idSlocPus.Text;
            string Category = idCategory.Text;
            string Description = idDescription.Text;
            string ScrapSloc = idScrapSloc.Text;
            string MVT = idMVT.Text;

            ////string userid = Session["username"].ToString();

            if (SlocPus != "" && ScrapSloc != "")
            {
                DataTable dtupdate = new DataTable();
                dtupdate = DataConn.StoreFillDS2("Update_mater_sloc_pus", System.Data.CommandType.StoredProcedure, id, Plant, Plant2, SlocPus, Category, Description, ScrapSloc, MVT);

                if (dtupdate.Rows[0][0].ToString() == "1")
                {
                    dt_image = DataConn.StoreFillDS2("Select_mater_Sloc_Pus", System.Data.CommandType.StoredProcedure);
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