<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMaterModel.aspx.cs" Inherits="FreeLayout.frmMaterModel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mater Model</title>
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
                    <h3><b style="font-size: 30px;">Master model</b></h3>
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

                    <span style="padding-left: 20px;"></span>
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                        Thêm mới
                    </button>

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
             <%--onserverclick="btnExport_Click"--%>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                       

                </div>

            </div>
        </div>


        <div>
            <table id="example" class="table table-striped table-bordered" style="width: 100%">
                <thead>
                    <tr>
                        <tr role="row">
                            <th>ID</th>
                            <th>CAT</th>
                            <th>Consignee_Refer_ATP</th>
                            <th>Country</th>
                            <th>Dest</th>
                            <th>Model</th>
                            <th>Stuffing_type</th>
                            <th>Model_Vol</th>
                            <th>Pcs_ctn</th>
                            <th>CTN_part</th>
                            <th>CTN_vol</th>
                            <th>Gross_weight</th>
                            <th>Series</th>
                            <th>MaxQty_cont40H</th>
                            <th>Max_Qty_cont20F</th>
                            <th>DIM_of_Carton_L</th>
                            <th>DIM_of_Carton_W</th>
                            <th>DIM_of_Carton_H</th>

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
                        <td><%=rows["CAT"].ToString()%></td>
                        <td><%=rows["Consignee_Refer_ATP"].ToString()%></td>
                        <td><%=rows["Country"].ToString()%></td>
                        <td><%=rows["Dest"].ToString()%></td>
                        <td><%=rows["Model"].ToString()%></td>
                        <td><%=rows["Stuffing_type"].ToString()%></td>
                        <td><%=rows["Model_Vol"].ToString()%></td>
                        <td><%=rows["Pcs_ctn"].ToString()%></td>
                        <td><%=rows["CTN_part"].ToString()%></td>
                        <td><%=rows["CTN_vol"].ToString()%></td>
                        <td><%=rows["Gross_weight"].ToString()%></td>
                        <td><%=rows["Series"].ToString()%></td>
                        <td><%=rows["MaxQty_cont40H"].ToString()%></td>
                        <td><%=rows["Max_Qty_cont20F"].ToString()%></td>
                        <td><%=rows["DIM_of_Carton_L"].ToString()%></td>
                        <td><%=rows["DIM_of_Carton_W"].ToString()%></td>
                        <td><%=rows["DIM_of_Carton_H"].ToString()%></td>
                        <td>
                            <a href="#" class="btn btn-info btn-sm" title="eidt item" onclick="openEditModal3('<%= rows["ID"].ToString() %>','<%=rows["CAT"].ToString() %>')"><i class="fas fa-edit"></i>Edit</a>
                            <a href="#" style="background-color: #dc3545; color: white;" class="btn btn-info btn-sm" title="eidt item" onclick="openEditModal4('<%= rows["ID"].ToString() %>','<%=rows["CAT"].ToString()%>')"><i class="fas fa-trash"></i>Delete</a>

                        </td>
                    </tr>
                    <%} %>
                </tbody>
                <tfoot>
                    <tr>
                        <th>ID</th>
                        <th>CAT</th>
                        <th>Consignee_Refer_ATP</th>
                        <th>Country</th>
                        <th>Dest</th>
                        <th>Model</th>
                        <th>Stuffing_type</th>
                        <th>Model_Vol</th>
                        <th>Pcs_ctn</th>
                        <th>CTN_part</th>
                        <th>CTN_vol</th>
                        <th>Gross_weight</th>
                        <th>Series</th>
                        <th>MaxQty_cont40H</th>
                        <th>Max_Qty_cont20F</th>
                        <th>DIM_of_Carton_L</th>
                        <th>DIM_of_Carton_W</th>
                        <th>DIM_of_Carton_H</th>

                        <th>Action</th>
                    </tr>
                </tfoot>
            </table>
        </div>

        <div class="modal" id="myModal4">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag11" style="float: left">Delete Model ID?</h4>
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
                                        <label for="ID">ID</label>
                                        <span style="color: green; font-size: 11px; font-style: italic;">(Read only)</span>
                                        <asp:TextBox ID="txtid_del" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Model</label>
                                        <asp:TextBox ID="txModel_del" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <%--<label for="ID">Production Date</label>
                                <asp:TextBox ID="txtngay_del" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <%--<label for="exampleInputEmail1">Line</label>
                                <asp:TextBox ID="txtline_del" CssClass="form-control" placeholder="0" runat="server"></asp:TextBox>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Đóng</button>
                        <button type="button" runat="server" id="Button2" onserverclick="Xoathongtin" class="btn btn-primary">
                            <i class="fas fa-download"></i>
                            Ghi lại
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal -->
        <div class="modal" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Thêm mới Mater Model</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">CAT</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="CATid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Consignee_Refer_ATP</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="Consignee_Refer_ATPid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Country</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="Countryid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Dest</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="Destid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">Model</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="Modelid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Stuffing_type</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="Stuffing_typeid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Model_Vol</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="Model_Volid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Pcs_ctn</label>
                                <asp:TextBox ID="Pcs_ctnid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">CTN_part</label>
                                <asp:TextBox ID="CTN_partid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">CTN_vol</label>
                                <asp:TextBox ID="CTN_volid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Gross_weight</label>
                                <asp:TextBox ID="Gross_weightid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Series</label>
                                <asp:TextBox ID="Seriesid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">MaxQty_cont40H</label>
                                <asp:TextBox ID="MaxQty_cont40Hid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Max_Qty_cont20F</label>
                                <asp:TextBox ID="Max_Qty_cont20Fid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">DIM_of_Carton_L</label>
                                <asp:TextBox ID="DIM_of_Carton_Lid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">DIM_of_Carton_W</label>
                                <asp:TextBox ID="DIM_of_Carton_Wid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">DIM_of_Carton_H</label>
                                <asp:TextBox ID="DIM_of_Carton_Hid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-3">
                            </div>
                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                        <button type="button" class="btn btn-primary" runat="server" onserverclick="themhanghoa">Ghi lại</button>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal" id="myModal3">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag1" style="float: left">Cập nhật thông tin mater Model</h4>
                                <%--<h6 class="modal-title" id="headerTag" style="float: left; color:red"><b><i>Chi tiết tồn kho!</i></b></h6>--%>

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">CAT</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idCAT" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Consignee_Refer_ATP</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idConsignee_Refer_ATP" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Country</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idCountry" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Dest</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idDest" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">Model</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idModel" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Stuffing_type</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idStuffing_type" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Model_Vol</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idModel_Vol" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Pcs_ctn</label>
                                <asp:TextBox ID="idPcs_ctn" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">CTN_part</label>
                                <asp:TextBox ID="idCTN_part" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">CTN_vol</label>
                                <asp:TextBox ID="idCTN_vol" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Gross_weight</label>
                                <asp:TextBox ID="idGross_weight" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Series</label>
                                <asp:TextBox ID="idSeries" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">MaxQty_cont40H</label>
                                <asp:TextBox ID="idMaxQty_cont40H" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Max_Qty_cont20F</label>
                                <asp:TextBox ID="idMax_Qty_cont20F" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">DIM_of_Carton_L</label>
                                <asp:TextBox ID="idDIM_of_Carton_L" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">DIM_of_Carton_W</label>
                                <asp:TextBox ID="idDIM_of_Carton_W" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">DIM_of_Carton_H</label>
                                <asp:TextBox ID="idDIM_of_Carton_H" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">ID</label>
                                <asp:TextBox ID="IDedit" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-3">
                            </div>
                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <%-- Modal footer --%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Đóng</button>
                        <button type="button" runat="server" id="Button1" onserverclick="Updatethongtin" class="btn btn-primary">
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

        function openEditModal3(id, model, ngaysanxuat, linename, giosx) {
            //$("#txtid2").val(id);
            //$("#txtmodel2").val(model);
            //$("#txtngaysx2").val(ngaysanxuat);
            //$("#txtline2").val(linename); //txtgiosx_
            //$("#txtgiosx_").val(giosx);

            $('#myModal3').modal('show');
        }

        function openEditModal4(id, model, ngaysanxuat, linename) {
            //$("#txtid_del").val(id);
            //$("#txtmodel_del").val(model);
            //$("#txtngay_del").val(ngaysanxuat);
            //$("#txtline_del").val(linename);

            $('#myModal4').modal('show');
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
