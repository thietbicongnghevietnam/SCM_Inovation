<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVanningDateSCM.aspx.cs" Inherits="FreeLayout.frmVanningDateSCM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vanning Date SCM</title>
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
        <div>
            <nav class="navbar navbar-expand navbar-dark bg-primary">
                <!-- Left navbar links -->
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a class="nav-link" data-widget="pushmenu" href="#" role="button"><i class="fas fa-bars"></i></a>
                    </li>
                    <li class="nav-item d-none d-sm-inline-block">
                        <a href="/frmVanningDateSCM.aspx" class="nav-link"><span style="font-size: 22px;">Home</span></a>
                    </li>
                    <li class="nav-item d-none d-sm-inline-block">
                        <%--<a href="/InventoryInfra.aspx" target="_blank" class="nav-link"><span style="font-size: 22px;">Master vessel schedule</span></a>--%>
                        <a href="/frmMaterVessel.aspx" target="_blank" class="nav-link"><span style="font-size: 22px;">Master vessel</span></a>
                    </li>
                    <li class="nav-item d-none d-sm-inline-block">
                        <a href="/frmMaterModel.aspx" target="_blank" class="nav-link"><span style="font-size: 22px;">Master model</span></a>
                    </li>
                </ul>

            </nav>
        </div>

        <div class="card">
            <div class="card-header">
                <div class="col-sm-12">
                    <h3><b style="font-size: 30px;">SCM Vanning Date</b></h3>
                    <br />
                    <p style="color: blue;">
                        <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="col-sm-12">
                    <div style="float: left;">
                        Từ ngày:
                                     <%--<input type="text" id="datepicker" runat="server">--%>
                        <input type="date" id="Date1" name="date" runat="server">
                        Đến ngày:                                    
                                     <input type="date" id="ngaychiid" name="date" runat="server">
                    </div>
                    <div class="col-md-1" style="float: left">
                        <div class="form-group">
                            <%-- <label for="Group">Filter Cate</label>--%>
                            <asp:DropDownList ID="dr_filter_Cate" runat="server"
                                AppendDataBoundItems="true"
                                DataTextField="Description"
                                DataValueField="Description"
                                CssClass="custom-select custom-select-sm form-control form-control-sm" OnSelectedIndexChanged="dr_filter_Plan_SelectedIndexChanged" AutoPostBack="True" />
                        </div>
                    </div>
                    <span style="padding-left: 20px;"></span>
                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">

                        <i class="fa fa-fw fa-lg fa-search"></i>Lọc</button>

                    <!-- import file excel -->
                    <!-- ADD A FILE UPLOAD CONTROL AND A BUTTON TO EXECUTE. -->
                    <div style="font: 14px Verdana; float: right">
                        <p style="margin-top: 0px; margin-left: 20px;">
                            Chọn file để upload:
        <asp:FileUpload ID="FileUpload" Width="450px" runat="server" />
                        </p>
                        <p style="margin-top: 0px; margin-left: 20px;">
                            <input type="button" value="Import Plan" runat="server" onserverclick="ImportFromExcel" class="btn btn-primary" />

                            &nbsp;&nbsp;&nbsp;<%--<input type="button" value="Import DECT" runat="server" onserverclick="ImportFromExcel1" class="btn btn-primary" />--%>

         &nbsp;&nbsp;&nbsp;
                            <button type="button" class="btn btn-primary float-right" style="margin-right: 5px;" runat="server">
                                <%--onserverclick="btnDownloadClick" --%>
                                <i class="fas fa-download"></i>Tải file mẫu upload
                            </button>
                        </p>
                        <p>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </p>
                    </div>

                    <button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;"><i class="fa fa-download"></i>&nbsp; Export</button>&nbsp;&nbsp;&nbsp;
                    <button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;" onserverclick="btnTinhLichTau"><i class="fa fa-calculator"></i>&nbsp; Calculate Date</button>
                    <%--onserverclick="btnExport_Click"--%>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                       

                </div>
                <br />
                <div class="col-sm-12">
                    <asp:RadioButton ID="rblDECT" runat="server" GroupName="rblOptions" Text="DECT" CssClass="horizontal-radio-buttons" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rblDP" runat="server" GroupName="rblOptions" Text="DP" CssClass="horizontal-radio-buttons" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rblPJ" runat="server" GroupName="rblOptions" Text="PJ" CssClass="horizontal-radio-buttons" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rblMW" runat="server" GroupName="rblOptions" Text="MW" CssClass="horizontal-radio-buttons" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rblSound" runat="server" GroupName="rblOptions" Text="SB" CssClass="horizontal-radio-buttons" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rblTV" runat="server" GroupName="rblOptions" Text="TV" CssClass="horizontal-radio-buttons" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rblCAM" runat="server" GroupName="rblOptions" Text="CAM" CssClass="horizontal-radio-buttons" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                </div>
            </div>
        </div>


        <div>
            <table id="example" class="table table-striped table-bordered" style="width: 100%">
                <thead>
                    <tr>
                        <tr role="row">
                            <th>ID</th>
                            <th>Sheet</th>
                            <th>Cat</th>
                            <th>Shipmode</th>
                            <th>Consignee</th>
                            <th>Country</th>
                            <th>Destination</th>
                            <th>Model</th>
                            <th>Quantity</th>
                            <th>ATPdate</th>
                            <th>Volume</th>
                            <th>Grossweight</th>
                            <th>TTLVolume</th>
                            <th>TTLcont</th>
                            <th>Qtycont</th>
                            <th>TTLcont2</th>
                            <th>Exfactorydate</th>
                            <th>ETD</th>
                            <th>ETA</th>
                            <th>Cancombine</th>
                            <th>Risky</th>
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
                        <td><%=rows["Sheet"].ToString()%></td>
                        <td><%=rows["Cat"].ToString()%></td>
                        <td><%=rows["Shipmode"].ToString()%></td>
                        <td><%=rows["Consignee"].ToString()%></td>
                        <td><%=rows["Country"].ToString()%></td>
                        <td><%=rows["Destination"].ToString()%></td>
                        <td><%=rows["Model"].ToString()%></td>
                        <td><%=rows["Quantity"].ToString()%></td>
                        <td><%=rows["ATPdate"].ToString()%></td>
                        <td><%=rows["Volume"].ToString()%></td>
                        <td><%=rows["Grossweight"].ToString()%></td>
                        <td><%=rows["TTLVolume"].ToString()%></td>
                        <td><%=rows["TTLcont"].ToString()%></td>
                        <td><%=rows["Qtycont"].ToString()%></td>
                        <td><%=rows["TTLcont2"].ToString()%></td>
                        <td><%=rows["Exfactorydate"].ToString()%></td>
                        <td><%=rows["ETD"].ToString()%></td>
                        <td><%=rows["ETA"].ToString()%></td>
                        <td><%=rows["Cancombine"].ToString()%></td>
                        <td><%=rows["Risky"].ToString()%></td>
                        <td>

                        </td>
                    </tr>
                    <%} %>
                </tbody>
                <tfoot>
                    <tr>
                        <th>ID</th>
                        <th>Sheet</th>
                        <th>Cat</th>
                        <th>Shipmode</th>
                        <th>Consignee</th>
                        <th>Country</th>
                        <th>Destination</th>
                        <th>Model</th>
                        <th>Quantity</th>
                        <th>ATPdate</th>
                        <th>Volume</th>
                        <th>Grossweight</th>
                        <th>TTLVolume</th>
                        <th>TTLcont</th>
                        <th>Qtycont</th>
                        <th>TTLcont2</th>
                        <th>Exfactorydate</th>
                        <th>ETD</th>
                        <th>ETA</th>
                        <th>Cancombine</th>
                        <th>Risky</th>
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
            $('#txtdevice').prop("readonly", true);
        });

        $(function () {
            $("#example").DataTable({
                "responsive": true,
                "autoWidth": true,
                //"order": [[7, "desc"]],
                "pageLength": 50
                //"ordering": true,
                //"paging": true,
                //"lengthChange": false,
                //"searching": false,
                //"info": true,                    
            });

        });





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
