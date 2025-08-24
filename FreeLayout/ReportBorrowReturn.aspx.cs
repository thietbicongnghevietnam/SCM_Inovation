using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FreeLayout.App_Code;

namespace FreeLayout
{
    public partial class ReportBorrowReturn : System.Web.UI.Page
    {
        DataConn cnn = new DataConn();
        public DataTable dt_report = new DataTable();
        //public DataTable dt1 = new DataTable();
        public DataTable dtserial = new DataTable();

        public string itemid = "";
        public string serialno = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //dt_report = DataConn.StoreFillDS("history_borrow_return_ISD", System.Data.CommandType.StoredProcedure);
                if (Request.QueryString["itemid"] is null)
                {
                    dt_report = DataConn.StoreFillDS("history_borrow_return_ISD", System.Data.CommandType.StoredProcedure);
                    itemid = "";
                    serialno = "";
                }
                else
                {
                    string _itemid = Request.QueryString["itemid"];
                    itemid = _itemid.Replace("'", "");
                    dtserial = DataConn.StoreFillDS("get_serialno", System.Data.CommandType.StoredProcedure, itemid);
                    if (dtserial.Rows.Count > 0)
                    {
                        serialno = dtserial.Rows[0][0].ToString();
                    }
                    else
                    {
                        serialno = "";
                    }

                    dt_report = DataConn.StoreFillDS("history_borrow_return_ISD2", System.Data.CommandType.StoredProcedure,itemid);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "search_material2();", true);
                }
            }

            

        }




    }
}