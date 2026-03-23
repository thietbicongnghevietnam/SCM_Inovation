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
    
    public partial class PrintpartcardQC : System.Web.UI.Page
    {
        DataConn cnn = new DataConn();
        public DataTable dt_scrap = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string data = Request.QueryString["data"];
            dt_scrap = DataConn.StoreFillDS2("Get_List_Material_scraplist", System.Data.CommandType.StoredProcedure, data);
        }


    }
}