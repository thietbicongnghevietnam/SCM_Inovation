<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmConvertToolVessel.aspx.cs" Inherits="FreeLayout.frmConvertToolVessel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tool Convert Vessel</title>
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
                    <h3><b style="font-size: 30px;">Tool Convert Vessel</b></h3>
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
                                DataTextField="NameTemplate"
                                DataValueField="NameTemplate"
                                CssClass="custom-select custom-select-sm form-control form-control-sm" OnSelectedIndexChanged="dr_filter_Cate_SelectedIndexChanged" AutoPostBack="True" />
                            
                        </div>
                    </div>

                    <div class="col-md-1" style="float: left">
    <div class="form-group">
        <%-- <label for="Group">Filter Cate</label>--%>
        <asp:DropDownList ID="dr_filter_keyconvert" runat="server"
            AppendDataBoundItems="true"
            DataTextField="KeyConvert"
            DataValueField="KeyConvert"
            CssClass="custom-select custom-select-sm form-control form-control-sm" />
        <%--OnSelectedIndexChanged="dr_filter_Plan_SelectedIndexChanged" AutoPostBack="True"--%>
    </div>
</div>

                  <%--  <div style="float: left; padding-right: 10px;">
                        <input type="text" id="filterSanction" runat="server" placeholder="Input Sacntion" style="height: 34px;" />
                    </div>--%>

                   <%-- <div style="float: left; padding-right: 10px;">
                        <input type="text" id="filterIssueout" runat="server" placeholder="Input IusseOut" style="height: 34px;" />
                    </div>--%>

                    <span style="padding-left: 10px;"></span>
                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">

                        <i class="fa fa-fw fa-lg fa-search"></i>Filter</button>
                    <span style="padding-left: 5px;"></span>
                     <button class="btn btn-primary" type="button" runat="server" >  <%--onserverclick="export_craplist_Click"--%>

     <i class="fa fa-download"></i>Export shipment</button>

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
                            <input type="button" value="Import Templates" runat="server" onserverclick="ImportFromExcel" class="btn btn-primary" />

                            &nbsp;&nbsp;&nbsp;
                           
                               <%-- <asp:RadioButton ID="rblNG" runat="server" GroupName="rblOptions" Text="NG list" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rblDesktock" runat="server" GroupName="rblOptions" Text="Deadstock list" />--%>
                            <%--AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged"--%>
                           <%-- AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged"--%>
                        </p>
                    </div>
                </div>

            </div>
            <div class="col-sm-12">
                <b style="float: left; padding-top: 25px; margin-right: 5px;">Colums :</b>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">Cat</b><br />
                    <input type="text" id="txtCat" runat="server" placeholder="Cat" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">S/a</b><br />
                    <input type="text" id="txtsa" runat="server" placeholder="S/a" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">Name</b><br />
                    <input type="text" id="txtname" runat="server" placeholder="Name" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">Produce_model</b><br />
                    <input type="text" id="txtproductmodel" runat="server" placeholder="Produce_model" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">Rep_qty</b><br />
                    <input type="text" id="txtjit_qty" runat="server" placeholder="jit_qty" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    <b style="color:red">Ship_date</b><br />
                    <input type="text" id="txtShipDate" runat="server" placeholder="Ship_date" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    delay1_date<br />
                    <input type="text" id="txtdelay1_date" runat="server" placeholder="delay1_date" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    delay1_qty<br />
                    <input type="text" id="txtdelay1_qty" runat="server" placeholder="delay1_qty" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    delay2_date<br />
                    <input type="text" id="txtdelay2_date" runat="server" placeholder="delay2_date" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    delay2_qty<br />
                    <input type="text" id="txtdelay2_qty" runat="server" placeholder="delay2_qty" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    delay3_date<br />
                    <input type="text" id="txtdelay3_date" runat="server" placeholder="delay3_date" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    delay3_qty<br />
                    <input type="text" id="txtdelay3_qty" runat="server" placeholder="delay3_qty" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    delay4_date<br />
                    <input type="text" id="txtdelay4_date" runat="server" placeholder="delay4_date" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    delay4_qty<br />
                    <input type="text" id="txtdelay4_qty" runat="server" placeholder="delay4_qty" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    delay5_date<br />
                    <input type="text" id="txtdelay5_date" runat="server" placeholder="delay5_date" style="height: 34px; width: 100px;" />
                </div>
                <div style="float: left; padding-right: 5px;">
                    delay5_qty<br />
                    <input type="text" id="txtdelay5_qty" runat="server" placeholder="delay5_qty" style="height: 34px; width: 100px;" />
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
                    <th>Category</th>
                    <th>sa</th>
                    <th>name</th>
                    <th>producemodel</th>
                    <th>ReqQty</th>
                    <th>jitqty</th>
                    <th>shipdate</th>
                    
                    <th>delay1date</th>
                    <th>delay1qty</th>
                    <th>delay2date</th>
                    <th>delay2qty</th>
                    <th>delay3date</th>
                    <th>delay3qty</th>
                    <th>delay4date</th>
                    <th>delay4qty</th>
                    <th>delay5date</th>
                    <th>delay5qty</th>

                    <th>TemplateName</th>                  
                    <th>KeyConvert</th>
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
                <td><%=rows["Category"].ToString()%></td>
                <td><%=rows["sa"].ToString()%></td>
                <td><%=rows["name"].ToString()%></td>
                <td><%=rows["producemodel"].ToString()%></td>
                <td><%=rows["ReqQty"].ToString()%></td>
                <td><%=rows["jitqty"].ToString()%></td>
                <td><%=rows["shipdate"].ToString()%></td>
                
                <td><%=rows["delay1date"].ToString()%></td>
                <td><%=rows["delay1qty"].ToString()%></td>

                <td><%=rows["delay2date"].ToString()%></td>
                <td><%=rows["delay2qty"].ToString()%></td>
                <td><%=rows["delay3date"].ToString()%></td>
                <td><%=rows["delay3qty"].ToString()%></td>
                <td><%=rows["delay4date"].ToString()%></td>
                <td><%=rows["delay4qty"].ToString()%></td>
                <td><%=rows["delay5date"].ToString()%></td>
                <td><%=rows["delay5qty"].ToString()%></td>
                <td><%=rows["TemplateName"].ToString()%></td>
                <td><%=rows["KeyConvert"].ToString()%></td>
             
                <td>
                     <a href="#" title="eidt item" onclick="openEditModal3('<%= rows["Id"].ToString() %>','<%= rows["producemodel"].ToString() %>','<%= rows["ReqQty"].ToString() %>')"><i class="fas fa-edit"></i></a>
                    <a href="#" title="delete item" onclick="openEditModal5('<%= rows["Id"].ToString() %>','<%= rows["producemodel"].ToString() %>')"><i class="fas fa-trash"></i></a>
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
        // $('#IDedit').prop("readonly", true);
        // $('#idSanctionname').prop("readonly", true);
         //$('#txtid').prop("readonly", true);
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

     //function openEditModal3(Id, SanctionId, Qty) {
     //    $("#IDedit").val(Id);
     //    $("#idSanctionname").val(SanctionId);
     //   /* $("#idmaterial").val(Material);      */     
     //    $("#idqty").val(Qty);           
     //    $('#myModal3').modal('show');
     //}

     //function openEditModal5(id, SanctionId) {
     //    $('#txtid').val(id);
     //    $('#txtsanction').val(SanctionId);
     //    $('#myModal5').modal('show');
     //}

     //function openEditModal4(id, model) {
     //    $("#txtid_del").val(id);
     //    $("#txModel_del").val(model);

     //    $('#myModal4').modal('show');
     //}



 </script>

 <script src="/plugins/jquery/jquery-ui.js"></script>
 <script type="text/javascript">
    
 </script>

</body>
</html>
