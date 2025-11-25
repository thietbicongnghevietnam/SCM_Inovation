<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUploadScraplist.aspx.cs" Inherits="FreeLayout.frmUploadScraplist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scrap List</title>
    <link rel="stylesheet" href="/plugins/fontawesome-free/css/all.min.css" />

    <link rel="stylesheet" href="/plugins/datatables-responsive/css/responsive.bootstrap4.min.css" />
    <link rel="stylesheet" href="/plugins/toastr/toastr.css" />
    <link rel="stylesheet" href="/dist/css/adminlte.min.css" />
    <link rel="stylesheet" href="/plugins/fontawesome-free/css/jquery-ui.css" />

    <link rel="stylesheet" href="/dist/Infra/bootstrap.css" />
    <link rel="stylesheet" href="/dist/Infra/dataTables.bootstrap4.min.css" />

    <script src="/plugins/jquery/jquery.min.js"></script>
    <script src="/plugins/toastr/toastr.js"></script>

    <script src="/dist/Infra/jquery-3.7.0.js"></script>
    <script src="/dist/Infra/jquery.dataTables.min.js"></script>
    <script src="/dist/Infra/dataTables.bootstrap4.min.js"></script>



    <script src="/Exportexcel/jquery.table2excel.min.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="card">
            <div class="card-header">
                <div class="col-sm-12">
                    <h3><b style="font-size: 30px;">Scrap List Management</b></h3>
                    <br />
                    <p style="color: blue;">
                        <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="col-sm-12">
                    <div style="float: left;">
                        From Date:
                             <%--<input type="text" id="datepicker" runat="server">--%>
                        <input type="date" id="Date1" name="date" runat="server">
                        To Date:                                    
                             <input type="date" id="ngaychiid" name="date" runat="server">
                    </div>
                    <div class="col-md-1" style="float: left">
                        <div class="form-group">
                            <%-- <label for="Group">Filter Cate</label>--%>
                            <asp:DropDownList ID="dr_filter_Cate" runat="server"
                                AppendDataBoundItems="true"
                                DataTextField="Description"
                                DataValueField="Description"
                                CssClass="custom-select custom-select-sm form-control form-control-sm" />
                            <%--OnSelectedIndexChanged="dr_filter_Plan_SelectedIndexChanged" AutoPostBack="True"--%>
                        </div>
                    </div>
                                        <div class="col-md-1" style="float: left">
    <div class="form-group">
        <%-- <label for="Group">Filter Cate</label>--%>
        <asp:DropDownList ID="dr_filter_Sanction" runat="server"
            AppendDataBoundItems="true"
            DataTextField="Sanction"
            DataValueField="Sanction"
            CssClass="custom-select custom-select-sm form-control form-control-sm" OnSelectedIndexChanged="dr_filter_Sanction_SelectedIndexChanged" AutoPostBack="True" />        
    </div>
</div>
                                                            <div class="col-md-1" style="float: left">
    <div class="form-group">
        <%-- <label for="Group">Filter Cate</label>--%>
        <asp:DropDownList ID="dr_filter_IssueOut" runat="server"
            AppendDataBoundItems="true"
            DataTextField="TypeName"
            DataValueField="TypeName"
            CssClass="custom-select custom-select-sm form-control form-control-sm" />
        <%--OnSelectedIndexChanged="dr_filter_Plan_SelectedIndexChanged" AutoPostBack="True"--%>
    </div>
</div>

                    <%--<div style="float: left; padding-right: 10px;">
                        <input type="text" id="filterSanction" runat="server" placeholder="Nhập Sacntion" style="height: 34px;" />
                    </div>--%>

                    <%--<div style="float: left; padding-right: 10px;">
                        <input type="text" id="filterIssueout" runat="server" placeholder="Nhập IusseOut" style="height: 34px;" />                        
                    </div>--%>

                    <span style="padding-left: 20px;"></span>
                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">

                        <i class="fa fa-fw fa-lg fa-search"></i>Lọc</button>

                   

                    <button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;" onserverclick="Export_IssueOut"><i class="fa fa-download"></i>&nbsp; Export Issue Out</button>&nbsp;&nbsp;&nbsp;
                    <button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;" onserverclick="Export_FA_PE"><i class="fa fa-download"></i>&nbsp; Export Disposition Property List</button>&nbsp;&nbsp;&nbsp;             
                    <%--<button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;"><i class="fa fa-download"></i>&nbsp; Export Scarp List</button>&nbsp;&nbsp;&nbsp; --%>            
          
                </div>

                <div class="col-sm-12">
                </div>

            </div>
        </div>


        <div>
            <table id="example" class="table table-striped table-bordered" style="width: 100%">
                <thead>
                    <tr>
                        <tr role="row">
                            <th>ID</th>
                            <th>SanctionId</th>
                            <th>Material</th>
                            <th>Qty</th>
                            <th>QtyActual</th>
                            <th>UnitPrice</th>
                            <th>Amount</th>
                            <th>CostCenter</th>
                            <th>Reason</th>
                            <th>Plant</th>
                            <th>Sloc</th>
                            <th>NameCost</th>
                            <th>Pallet</th>
                            <th>Barcode</th>
                            <th>IssueOut</th>
                            <th>CreatedDate</th>

                            <th>Action</th>
                        </tr>
                    </tr>
                </thead>
                <tbody>
                    <%int i = 0; %>
                    <%foreach (System.Data.DataRow rows in dt_plan.Rows)
                        {%>
                    <%i++;%>
                    <tr role="row">
                        <td><%=i %></td>
                        <td><%=rows["SanctionId"].ToString()%></td>
                        <td><%=rows["Material"].ToString()%></td>
                        <td><%=rows["Qty"].ToString()%></td>
                        <td><%=rows["QtyActual"].ToString()%></td>
                        <td><%=rows["UnitPrice"].ToString()%></td>
                        <td><%=rows["Amount"].ToString()%></td>
                        <td><%=rows["CostCenter"].ToString()%></td>
                        <td><%=rows["Reason"].ToString()%></td>
                        <td><%=rows["Plant"].ToString()%></td>
                        <td><%=rows["Sloc"].ToString()%></td>
                        <td><%=rows["NameCost"].ToString()%></td>
                        <td><%=rows["Pallet"].ToString()%></td>
                        <td><%=rows["Barcode"].ToString()%></td>
                        <td><%=rows["IssueOut"].ToString()%></td>
                        <td><%=rows["CreatedDate"].ToString()%></td>

                        <td></td>
                    </tr>
                    <%} %>
                </tbody>
                <tfoot>
                    <tr>
                        <th>ID</th>
                        <th>SanctionId</th>
                        <th>Material</th>
                        <th>Qty</th>
                        <th>QtyActual</th>
                        <th>UnitPrice</th>
                        <th>Amount</th>
                        <th>CostCenter</th>
                        <th>Reason</th>
                        <th>Plant</th>
                        <th>Sloc</th>
                        <th>NameCost</th>
                        <th>Pallet</th>
                        <th>Barcode</th>
                        <th>IssueOut</th>
                        <th>CreatedDate</th>

                        <th>Action</th>
                    </tr>
                </tfoot>
            </table>
        </div>



    </form>

    <script src="/plugins/jquery/jquery.min.js"></script>
    <script src="/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
    <script src="/plugins/datatables-responsive/js/dataTables.responsive.min.js"></script>
    <script src="/plugins/datatables-responsive/js/responsive.bootstrap4.min.js"></script>
    <script src="/dist/js/adminlte.min.js"></script>
    <script src="/dist/js/demo.js"></script>

    <script>

        //$(function () {
        //    $("#btnExport_normal").click(function () {
        //        $("#example1").table2excel({
        //            filename: "Report_inspection_normal"
        //        });
        //    })
        //});
    </script>

    <script type="text/javascript">  
        $(document).ready(function () {
            $('#IDedit').prop("readonly", true);
            $('#txtid_del').prop("readonly", true);
        });

        $(function () {
            $("#example").DataTable({
                //"responsive": true,
                "autoWidth": true,
                scrollX: true,
                //"order": [[7, "desc"]],
                "pageLength": 50
                //"ordering": true,
                //"paging": true,
                //"lengthChange": false,
                //"searching": false,
                //"info": true,                    
            });

        });

        //function openEditModal3(id, CAT, Consignee_Refer_ATP, Country, Dest, Model, Stuffing_type, Model_Vol, Pcs_ctn, CTN_part, CTN_vol, Gross_weight, Series, MaxQty_cont40H, Max_Qty_cont20F, DIM_of_Carton_L, DIM_of_Carton_W, DIM_of_Carton_H) {
        //    $("#IDedit").val(id);
        //    $("#idCAT").val(CAT);
        //    $("#idConsignee_Refer_ATP").val(Consignee_Refer_ATP);
        //    $("#idCountry").val(Country);
        //    $("#idDest").val(Dest);
        //    $("#idModel").val(Model);
        //    $("#idStuffing_type").val(Stuffing_type);
        //    $("#idModel_Vol").val(Model_Vol);
        //    $("#idPcs_ctn").val(Pcs_ctn);
        //    $("#idCTN_part").val(CTN_part);
        //    $("#idCTN_vol").val(CTN_vol);
        //    $("#idGross_weight").val(Gross_weight);
        //    $("#idSeries").val(Series);
        //    $("#idMaxQty_cont40H").val(MaxQty_cont40H);
        //    $("#idMax_Qty_cont20F").val(Max_Qty_cont20F);
        //    $("#idDIM_of_Carton_L").val(DIM_of_Carton_L);
        //    $("#idDIM_of_Carton_W").val(DIM_of_Carton_W);
        //    $("#idDIM_of_Carton_H").val(DIM_of_Carton_H);
        //    $('#myModal3').modal('show');
        //}

        //function openEditModal4(id, model) {
        //    $("#txtid_del").val(id);
        //    $("#txModel_del").val(model);

        //    $('#myModal4').modal('show');
        //}



    </script>

    <script src="/plugins/jquery/jquery-ui.js"></script>
    <script type="text/javascript">
        //$(function () {
        //    var onlyDate, today = new Date();
        //    var dateNewFormat = '';

        //    onlyDate = today.getDate();
        //    if (onlyDate.toString().length == 2) {

        //        dateNewFormat = onlyDate;
        //    }
        //    else {
        //        dateNewFormat = '0' + onlyDate;
        //    }

        //    dateNewFormat = dateNewFormat + '-';

        //    if (today.getMonth().length == 2) {

        //        dateNewFormat += (today.getMonth() + 1);
        //    }
        //    else {
        //        //dateNewFormat += '0' + (today.getMonth() + 1);
        //        dateNewFormat += (today.getMonth() + 1);
        //    }

        //    dateNewFormat = dateNewFormat + '-' + today.getFullYear();
        //    //dateNewFormat = today.getFullYear() + '-';

        //    //$('#datepicker').val(dateNewFormat);


        //    //$("#datepicker").datepicker({ dateFormat: 'dd-mm-yy' });

        //    $("#Date1").datepicker({ dateFormat: 'dd-mm-yy' });
        //    $("#ngaychiid").datepicker({ dateFormat: 'dd-mm-yy' });

        //});


    </script>


</body>
</html>
