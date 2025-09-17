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
    public partial class frmUploadScraplist : System.Web.UI.Page
    {
        public DataTable dt_plan = new DataTable();
        public DataTable dt_getmodel = new DataTable();
        public DataTable dtcate = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt_plan = DataConn.StoreFillDS2("Select_Mater_ScrapList", System.Data.CommandType.StoredProcedure);
                //Date1.Value = DateTime.Now.ToString("dd-MM-yyyy");
                //ngaychiid.Value = DateTime.Now.ToString("dd-MM-yyyy");


                //danh sach bo phan
                dtcate = DataConn.StoreFillDS2("pro_get_categogy", System.Data.CommandType.StoredProcedure);
                DataRow newRow1 = dtcate.NewRow();
                newRow1["Description"] = "==Categogy==";
                dtcate.Rows.InsertAt(newRow1, 0);
                dr_filter_Cate.DataSource = dtcate;
                dr_filter_Cate.DataBind();
            }
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];

            string bophan = dr_filter_Cate.SelectedValue;

            //if (bophan == "==Categogy==")
            //{
            //    dt_plan = DataConn.StoreFillDS("Select_Mater_ModelSCM", System.Data.CommandType.StoredProcedure);
            //}
            //else
            //{
            //    dt_plan = DataConn.StoreFillDS("Select_Mater_ModelSCM_cate", System.Data.CommandType.StoredProcedure, category);
            //}

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


        protected void ImportFromExcel(object sender, EventArgs e) 
        {
            
        }

        protected void btnDownloadClick(object sender, EventArgs e)
        {

        }



    }
}