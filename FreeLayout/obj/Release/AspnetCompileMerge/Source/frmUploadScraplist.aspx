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
                                CssClass="custom-select custom-select-sm form-control form-control-sm" OnSelectedIndexChanged="dr_filter_section_SelectedIndexChanged" AutoPostBack="True" />                            
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



                    <%-- <button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;" onserverclick="Export_IssueOut"><i class="fa fa-download"></i>&nbsp; Export Issue Out</button>&nbsp;&nbsp;&nbsp;--%>
                    <button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;" onserverclick="Export_FA_PE"><i class="fa fa-download"></i>&nbsp; Export Disposition Property List</button>&nbsp;&nbsp;&nbsp;             
                    <button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;" onserverclick="export_craplist_Click"><i class="fa fa-download"></i>&nbsp; Export Scarp List</button>&nbsp;&nbsp;&nbsp;   
                    <button class="btn btn-success" type="button" runat="server" style="margin-left: 20px;" onserverclick="Confirm_Issue_Out"><i class="fa fa-check-circle"></i>&nbsp; Confirm E-IsssueOut</button>&nbsp;&nbsp;&nbsp;   
                    <button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;" onserverclick="Dongbo_craplist_Click"><i class="fa fa-sync"></i>&nbsp; Sys Data Covnert</button>
                </div>

                <div class="col-sm-12">
                </div>

            </div>
        </div>

        <asp:HiddenField ID="hdSanction" runat="server" />
<asp:HiddenField ID="hdUser" runat="server" />
<asp:Button ID="btnConfirmYes" runat="server" OnClick="btnConfirmYes_Click" Style="display:none;" />
<asp:Button ID="btnConfirmNo" runat="server" OnClick="btnConfirmNo_Click" Style="display:none;" />


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
                            <th>E-IssueOut</th>
                            <th>CreatedDate</th>

                            <th>Action</th>
                        </tr>
                    </tr>
                </thead>
                <tbody>
                    <%int i = 0; %>
                    <%if (dt_plan != null)
                        {
                            foreach (System.Data.DataRow rows in dt_plan.Rows)
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
                        <td>
                          <%--  <%=rows["FlagEpro"].ToString()%>--%>
                            <% if (rows["FlagEpro"].ToString() == "1") { %>
                                    <span class="yes-label">YES</span>
                                <% } else { %>
                                    <span class="no-label">NO</span>
                                <% } %>

                        </td>
                        <td><%=rows["CreatedDate"].ToString()%></td>

                        <td>
                            <a href="#" title="eidt item" onclick="openEditModal3('<%= rows["Id"].ToString() %>',
                                '<%= rows["Material"].ToString() %>',
                                '<%= rows["Qty"].ToString() %>',
                                '<%= rows["QtyActual"].ToString() %>',
                                '<%= rows["UnitPrice"].ToString() %>',
                                '<%= rows["Sloc"].ToString() %>',
                                '<%= rows["NameCost"].ToString() %>')"><i class="fas fa-edit"></i></a>
                            <a href="#" title="delete item" onclick="openEditModal5('<%= rows["Id"].ToString() %>','<%= rows["Material"].ToString() %>')"><i class="fas fa-trash"></i></a>
                        </td>

                    </tr>
                    <%}
                    }%>
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
                        <th>E-IssueOut</th>
                        <th>CreatedDate</th>

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
                                <h4 class="modal-title" id="headerTag1" style="float: left">Cập nhật thông Scrap List</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">

                        <div class="row">
                          
                            <div class="col-md-6">
                                <label for="ID">ID</label>
                                <asp:TextBox ID="IDedit" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exETD">NameCost</label>
                                <asp:TextBox ID="txtNameCost" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="exFactoryDate">Material</label>
                                <asp:TextBox ID="idMaterial" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exETD">Qty</label>
                                <asp:TextBox ID="idQty" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <%-- <div class="col-md-6">
                        <label for="exFactoryDate"><i style="color: green">Material</i></label>                                                 
                        <asp:TextBox ID="idmaterial" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>--%>
                        </div>
                         <div class="row">
                             <div class="col-md-6">
                                 <label for="exETD">UnitPrice</label>
                                 <asp:TextBox ID="txtUnitPrice" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                             </div>
                             <div class="col-md-6">
                                 <label for="etdDate"><i style="color: green">Sloc</i></label>
                                 <asp:TextBox ID="txtSloc" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                             </div>
                         </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="exETD">QtyActual</label>
                                <asp:TextBox ID="idQtyActual" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="etdDate"><i style="color: green">UserID</i></label>
                                <asp:TextBox ID="isUser" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
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
                                        <label for="exampleInputEmail1">Material</label>
                                        <asp:TextBox ID="txtMaterial" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">UserID</label>
                                        <asp:TextBox ID="txtuser" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
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

        function openEditModal3(Id, Material, Qty, QtyActual, UnitPrice, Sloc, NameCost) {
            $("#IDedit").val(Id);
            $("#idMaterial").val(Material);
            $("#idQty").val(Qty);
            $("#idQtyActual").val(QtyActual);

            $('#txtUnitPrice').val(UnitPrice);
            $('#txtSloc').val(Sloc);
            $('#txtNameCost').val(NameCost);

            $('#myModal3').modal('show');
        }

        function openEditModal5(id, material) {
            $('#txtid').val(id);
            $('#txtMaterial').val(material);
            $('#myModal5').modal('show');
        }



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
