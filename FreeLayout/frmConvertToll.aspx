<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmConvertToll.aspx.cs" Inherits="FreeLayout.frmConvertToll" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tool Convert</title>
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

        <style>
    .yes-label {
        background: #28a745;
        color: white;
        padding: 3px 8px;
        border-radius: 4px;
    }
    .no-label {
        background: #6c757d;
        color: white;
        padding: 3px 8px;
        border-radius: 4px;
    }
</style>
</head>

<body>

    <form id="form1" runat="server">
        <div class="card">
            <div class="card-header">
                <div class="col-sm-12">
                    <h3><b style="font-size: 30px;">Tool Convert Scrap</b></h3>
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
                                CssClass="custom-select custom-select-sm form-control form-control-sm" OnSelectedIndexChanged="dr_filter_section_SelectedIndexChanged" AutoPostBack="True" />                            
                        </div>
                    </div>

                    <div class="col-md-1" style="float: left">
    <div class="form-group">
        <%-- <label for="Group">Filter Cate</label>--%>
        <asp:DropDownList ID="dr_filter_Sanction" runat="server"
            AppendDataBoundItems="true"
            DataTextField="SanctionId"
            DataValueField="SanctionId"
            CssClass="custom-select custom-select-sm form-control form-control-sm" />
        <%--OnSelectedIndexChanged="dr_filter_Plan_SelectedIndexChanged" AutoPostBack="True"--%>
    </div>
</div>

                    <div style="float: left; padding-right: 10px;">
                        <input type="text" id="filterSanction" runat="server" placeholder="Input Sacntion" style="height: 34px;" />
                    </div>

                   <%-- <div style="float: left; padding-right: 10px;">
                        <input type="text" id="filterIssueout" runat="server" placeholder="Input IusseOut" style="height: 34px;" />
                    </div>--%>

                    <span style="padding-left: 10px;"></span>
                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">

                        <i class="fa fa-fw fa-lg fa-search"></i>Filter</button>
                    <span style="padding-left: 5px;"></span>
                     <button class="btn btn-primary" type="button" runat="server" onserverclick="export_craplist_Click">

     <i class="fa fa-download"></i>Export Scraplist</button>&nbsp;&nbsp;&nbsp;

                                    <%--<button class="btn btn-primary" type="button" runat="server" onserverclick="Dongbo_craplist_Click">
<i class="fa fa-sync"></i>Sys Data Scraplist</button>--%>

                    <%--<span style="padding-left: 20px;"></span>
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                        Thêm mới
                    </button>--%>

                    <!-- import file excel -->
                    <!-- ADD A FILE UPLOAD CONTROL AND A BUTTON TO EXECUTE. -->
                    <div style="font: 14px Verdana; float: right">
                        <p style="margin-top: 0px; margin-left: 20px;">
                            Chose file to upload:
                            <asp:FileUpload ID="FileUpload" Width="450px" runat="server" />
                        </p>
                        <p style="margin-top: 0px; margin-left: 20px;">
                            <input type="button" value="Import ScrapList" runat="server" onserverclick="ImportFromExcel" class="btn btn-primary" />

                            &nbsp;&nbsp;&nbsp;
                           
                                <asp:RadioButton ID="rblNG" runat="server" GroupName="rblOptions" Text="NG list" Checked="true" AutoPostBack="true"
    OnCheckedChanged="RadioButton_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rblDesktock" runat="server" GroupName="rblOptions" Text="Deadstock list" AutoPostBack="true"
    OnCheckedChanged="RadioButton_CheckedChanged" />
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <!-- ========== CHECKBOX MỚI ========== -->
        <asp:CheckBox ID="chksacntion_trung" runat="server" Text="exitsanction" />
                        </p>
                    </div>
                </div>

            </div>
            <div class="col-sm-12">
                <b style="float: left; padding-top: 25px; margin-right: 5px;">Colums :</b>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">plan</b><br />
                    <input type="text" id="txtplan" runat="server" placeholder="Plan" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    Sloc<br />
                    <input type="text" id="txtsloc" runat="server" placeholder="Sloc" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    CostCenter<br />
                    <input type="text" id="txtCostcenter" runat="server" placeholder="CostCenter" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    NameCost<br />
                    <input type="text" id="txtnamecost" runat="server" placeholder="NameCost" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">material</b><br />
                    <input type="text" id="txtmaterial" runat="server" placeholder="material" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    IssueQty<br />
                    <input type="text" id="txtQty" runat="server" placeholder="IssueQty" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">unitpriceST</b><br />
                    <input type="text" id="txtunitpriceST" runat="server" placeholder="unitpriceST" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">amountST</b><br />
                    <input type="text" id="txtamountST" runat="server" placeholder="amountST" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    unitpriceAC<br />
                    <input type="text" id="txtunitpriceAC" runat="server" placeholder="unitpriceAC" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    amountAC<br />
                    <input type="text" id="txtamountAC" runat="server" placeholder="amountAC" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    remark<br />
                    <input type="text" id="txtremark" runat="server" placeholder="remark" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    vendorcode<br />
                    <input type="text" id="txtvendorname" runat="server" placeholder="vendorname" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">issueoutsloc</b><br />
                    <input type="text" id="txtissueoutsloc" runat="server" placeholder="issueoutsloc" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    Type<br />
                    <input type="text" id="txttype" runat="server" placeholder="Type" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    MVT<br />
                    <input type="text" id="txtMVT" runat="server" placeholder="MVT" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    typeMVT<br />
                    <input type="text" id="txttypeMVT" runat="server" placeholder="typeMVT" style="height: 34px; width: 100px;" />
                </div>

                 <b style="float: left; padding-top: 25px;">Row: </b>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">Row</b> <br />
                    <input type="text" id="txtrow" runat="server" placeholder="" style="height: 34px; width: 50px;" />
                </div>

                <div style="float: left; padding-right: 5px; padding-top: 22px;">
                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Save_setting_Click">
                        <i class="fa fa-save fa-fw fa-lg"></i>Save
                    </button>
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
                            <%--<th>QtyActual</th>--%>
                            <th>UnitPrice</th>
                            <th>Amount</th>
                            <th>CostCenter</th>
                            
                            <th>Plant</th>
                            <th>Sloc</th>
                            <th>NameCost</th>
                            <th>VendorCode</th>
                            <th>MoveType</th>
                            <th>TypeName</th>
                            <th>MVT</th>
                            <th>Reason</th>
                            <th>Qty_old</th>
                            <th>Change_Qty</th>
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
                       <%-- <td><%=rows["QtyActual"].ToString()%></td>--%>
                        <td><%=rows["UnitPrice"].ToString()%></td>
                        <td><%=rows["Amount"].ToString()%></td>
                        <td><%=rows["CostCenter"].ToString()%></td>
                        
                        <td><%=rows["Plant"].ToString()%></td>
                        <td><%=rows["Sloc"].ToString()%></td>
                        <td><%=rows["NameCost"].ToString()%></td>
                        <td><%=rows["Vendor"].ToString()%></td>
                        <td><%=rows["MoveType"].ToString()%></td>
                       <%-- <td><%=rows["IssueOut"].ToString()%></td>--%>
                        <td><%=rows["TypeName"].ToString()%></td>
                        <td><%=rows["MVT"].ToString()%></td>
                        <td><%=rows["Reason"].ToString()%></td>
                        <td>
                            
                             <% if (rows["Flag_change"].ToString() == "1") { %>
                                 <%=rows["Qty_old"].ToString()%>
                             <% } else { %>
                                
                             <% } %>
                        </td>
                         <td>
                             <% if (rows["Flag_change"].ToString() == "1") { %>
                                     <span class="yes-label">YES</span>
                                 <% } else { %>
                                     <span class="no-label">NO</span>
                                 <% } %>

                         </td>
                        <td>
                             <a href="#" title="eidt item" onclick="openEditModal3('<%= rows["Id"].ToString() %>','<%= rows["SanctionId"].ToString() %>','<%= rows["Qty"].ToString() %>','<%= rows["Vendor"].ToString() %>')"><i class="fas fa-edit"></i></a>
                            <a href="#" title="delete item" onclick="openEditModal5('<%= rows["Id"].ToString() %>','<%= rows["SanctionId"].ToString() %>')"><i class="fas fa-trash"></i></a>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
                <tfoot>
                    <tr>
                        <th>ID</th>
                        <th>SanctionId</th>
                        <th>Material</th>
                        <th>Qty</th>
                       <%-- <th>QtyActual</th>--%>
                        <th>UnitPrice</th>
                        <th>Amount</th>
                        <th>CostCenter</th>
                        
                        <th>Plant</th>
                        <th>Sloc</th>
                        <th>NameCost</th>
                        <%--<th>Pallet</th>--%>
                        <th>MoveType</th>
                        <th>TypeName</th>
                        <th>MVT</th>
                        <th>Reason</th>
                        <th>Action</th>
                    </tr>
                </tfoot>
            </table>
        </div>


        <div class="modal" id="myModal3">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div>
                        <h4 class="modal-title" id="headerTag1" style="float: left">Cập nhật thông tin covert tool</h4>
                        <%--<h6 class="modal-title" id="headerTag" style="float: left; color:red"><b><i>Chi tiết tồn kho!</i></b></h6>--%>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                </div>
            </div>

            <div class="modal-body">

                <div class="row">
                    <%--<div class="col-md-3">--%>
                    <%--<label for="exampleInputEmail1">Can_combine</label>
                <asp:TextBox ID="idCan_combine" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>--%>
                    <%--</div>--%>
                    <div class="col-md-12">
                        <label for="ID">ID</label>
                        <asp:TextBox ID="IDedit" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <%-- <div class="col-md-3"></div>--%>
                    <%-- <div class="col-md-3"></div>--%>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label for="exFactoryDate">Sanction Name</label>
                        <asp:TextBox ID="idSanctionname" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="exETD">Qty</label>
                        <asp:TextBox ID="idqty" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                   <%-- <div class="col-md-6">
                        <label for="exFactoryDate"><i style="color: green">Material</i></label>                                                 
                        <asp:TextBox ID="idmaterial" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>--%>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label for="exETD">Vendor</label>
                        <asp:TextBox ID="idvendor" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <%--<label for="etdDate"><i style="color: green">asdad</i></label>--%>
                       <%-- <input type="date" id="etdDate" class="form-control" name="etdDate" runat="server" />--%>
                    </div>
                </div>

                <!-- Lặp lại thêm các dòng -->
            </div>

            <%-- Modal footer --%>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Đóng</button>
                <button type="button" runat="server" id="Button1" class="btn btn-primary" onserverclick="Updatethongtin">
                    <i class="fas fa-download"></i>
                    Ghi lại
                </button>
            </div>
        </div>
    </div>
</div>

                <div class="modal" id="myModal5">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div>
                        <h4 class="modal-title" id="headerTag" style="float: left">Delete Item?</h4>
                        <%--<h6 class="modal-title" id="headerTag" style="float: left; color:red"><b><i>Chi tiết tồn kho!</i></b></h6>--%>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                </div>
            </div>

            <%-- Modal footer --%>
            <div class="modal-body">
                <div class="container-fluid">
                    <div class="row">                                
                        <div class="col-md-6">
                            <div class="form-group">
                               <label for="exampleInputEmail1">ID</label>                                        
                                <asp:TextBox ID="txtid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>                                        
                            </div>
                        </div>
                         <div class="col-md-6">
                            <div class="form-group">
                                    <label for="exampleInputEmail1">UserID</label>                                        
                                <asp:TextBox ID="txtuser" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>                                  
                            </div>
                        </div>
                    </div> 
                    <div class="row">                                
                    <div class="col-md-6">
                        <div class="form-group">
                           <label for="exampleInputEmail1">Sanction name</label>                                        
                            <asp:TextBox ID="txtsanction" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>                                        
                        </div>
                    </div>
                     <div class="col-md-6">
                        <div class="form-group">
                                                         
                        </div>
                    </div>
                </div>    
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Đóng</button>
                <button type="button" runat="server" id="btnOrder" class="btn btn-primary" onserverclick="delete_item"> 
                    <i class="fas fa-download"></i>
                    Ghi lại
                </button>
            </div>
        </div>
    </div>
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
            $('#idSanctionname').prop("readonly", true);
            $('#txtid').prop("readonly", true);
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

        function openEditModal3(Id, SanctionId, Qty,vendor) {
            $("#IDedit").val(Id);
            $("#idSanctionname").val(SanctionId);
           /* $("#idmaterial").val(Material);      */     
            $("#idqty").val(Qty);           
            $("#idvendor").val(vendor);           
            $('#myModal3').modal('show');
        }

        function openEditModal5(id, SanctionId) {
            $('#txtid').val(id);
            $('#txtsanction').val(SanctionId);
            $('#myModal5').modal('show');
        }

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
