<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMaterVessel.aspx.cs" Inherits="FreeLayout.frmMaterVessel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mater Vessel</title>
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
                    <h3><b style="font-size: 30px;">Master Vessel</b></h3>
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
                            <th>cate</th>
                            <th>Area</th>
                            <th>Country</th>
                            <th>DestCity</th>
                            <th>DestCityName</th>
                            <th>PIC</th>
                            <th>Consignee</th>
                            <th>FCL_Ex_factory</th>
                            <th>FCL_ETD</th>
                            <th>FCL_ETA</th>
                            <th>LLC_Ex_factory</th>
                            <th>LLC_ETD</th>
                            <th>LLC_ETA</th>
                            <th>AIR_Ex_factory</th>
                            <th>AIR_ETD</th>
                            <th>AIR_ETA</th>
                            <th>Special_exfactory_date</th>
                            <th>SpecialETD_week</th>
                            <th>Special_ETA_Date</th>
                            <th>Can_combine</th>

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
                        <td><%=rows["cate"].ToString()%></td>
                        <td><%=rows["Area"].ToString()%></td>
                        <td><%=rows["Country"].ToString()%></td>
                        <td><%=rows["DestCity"].ToString()%></td>
                        <td><%=rows["DestCityName"].ToString()%></td>
                        <td><%=rows["PIC"].ToString()%></td>
                        <td><%=rows["Consignee"].ToString()%></td>
                        <td><%=rows["FCL_Ex_factory"].ToString()%></td>
                        <td><%=rows["FCL_ETD"].ToString()%></td>
                        <td><%=rows["FCL_ETA"].ToString()%></td>

                        <td><%=rows["LLC_Ex_factory"].ToString()%></td>
                        <td><%=rows["LLC_ETD"].ToString()%></td>
                        <td><%=rows["LLC_ETA"].ToString()%></td>

                        <td><%=rows["AIR_Ex_factory"].ToString()%></td>
                        <td><%=rows["AIR_ETD"].ToString()%></td>
                        <td><%=rows["AIR_ETA"].ToString()%></td>

                        <td><%=rows["Special_exfactory_date"].ToString()%></td>
                        <td><%=rows["SpecialETD_week"].ToString()%></td>
                        <td><%=rows["Special_ETA_Date"].ToString()%></td>
                        <td><%=rows["Can_combine"].ToString()%></td>

                        <td>
                            <a href="#" class="btn btn-info btn-sm" title="eidt item" onclick="openEditModal3('<%= rows["ID"].ToString() %>','<%=rows["cate"].ToString() %>','<%=rows["Area"].ToString() %>',
'<%=rows["Country"].ToString() %>',
'<%=rows["DestCity"].ToString() %>',
'<%=rows["DestCityName"].ToString() %>',
'<%=rows["PIC"].ToString() %>',
'<%=rows["Consignee"].ToString() %>',
'<%=rows["FCL_Ex_factory"].ToString() %>',
'<%=rows["FCL_ETD"].ToString() %>',
'<%=rows["FCL_ETA"].ToString() %>',
'<%=rows["LLC_Ex_factory"].ToString() %>',
'<%=rows["LLC_ETD"].ToString() %>',
'<%=rows["LLC_ETA"].ToString() %>',
'<%=rows["AIR_Ex_factory"].ToString() %>',
'<%=rows["AIR_ETD"].ToString() %>',
'<%=rows["AIR_ETA"].ToString() %>',
'<%=rows["Special_exfactory_date"].ToString() %>',
'<%=rows["SpecialETD_week"].ToString() %>',
'<%=rows["Special_ETA_Date"].ToString() %>',
'<%=rows["Can_combine"].ToString() %>')"><i class="fas fa-edit"></i>Edit</a>
                            <a href="#" style="background-color: #dc3545; color: white;" class="btn btn-info btn-sm" title="eidt item" onclick="openEditModal4('<%= rows["ID"].ToString() %>','<%=rows["Country"].ToString()%>')"><i class="fas fa-trash"></i>Delete</a>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
                <tfoot>
                    <tr>
                        <th>ID</th>
                        <th>cate</th>
                        <th>Area</th>
                        <th>Country</th>
                        <th>DestCity</th>
                        <th>DestCityName</th>
                        <th>PIC</th>
                        <th>Consignee</th>
                        <th>FCL_Ex_factory</th>
                        <th>FCL_ETD</th>
                        <th>FCL_ETA</th>
                        <th>LLC_Ex_factory</th>
                        <th>LLC_ETD</th>
                        <th>LLC_ETA</th>
                        <th>AIR_Ex_factory</th>
                        <th>AIR_ETD</th>
                        <th>AIR_ETA</th>
                        <th>Special_exfactory_date</th>
                        <th>SpecialETD_week</th>
                        <th>Special_ETA_Date</th>
                        <th>Can_combine</th>

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
                                <h4 class="modal-title" id="headerTag11" style="float: left">Delete Vessel ID?</h4>                               
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
                                        <label for="exampleInputEmail1">Country</label>
                                        <asp:TextBox ID="txtCountry_del" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
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

        <div class="modal" id="myModal3">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag1" style="float: left">Cập nhật thông tin mater vessel</h4>
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
                                <label for="ID">cate</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idcate" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Area</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idArea" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Country</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idCountry" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">DestCity</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idDestCity" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">DestCityName</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idDestCityName" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">PIC</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idPIC" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Consignee</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="idConsignee" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">FCL_Ex_factory</label>
                                <asp:TextBox ID="idFCL_Ex_factory" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">FCL_ETD</label>
                                <asp:TextBox ID="idFCL_ETD" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">FCL_ETA</label>
                                <asp:TextBox ID="idFCL_ETA" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">LLC_Ex_factory</label>
                                <asp:TextBox ID="idLLC_Ex_factory" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">LLC_ETD</label>
                                <asp:TextBox ID="idLLC_ETD" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">LLC_ETA</label>
                                <asp:TextBox ID="idLLC_ETA" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">AIR_Ex_factory</label>
                                <asp:TextBox ID="idAIR_Ex_factory" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">AIR_ETD</label>
                                <asp:TextBox ID="idAIR_ETD" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">AIR_ETA</label>
                                <asp:TextBox ID="idAIR_ETA" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">Special_exfactory_date</label>
                                <asp:TextBox ID="idSpecial_exfactory_date" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">SpecialETD_week</label>
                                <asp:TextBox ID="idSpecialETD_week" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Special_ETA_Date</label>
                                <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Special_ETA_Date</label>
                                <asp:TextBox ID="idSpecial_ETA_Date" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Can_combine</label>
                                <asp:TextBox ID="idCan_combine" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">ID</label>
                                <asp:TextBox ID="IDedit" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
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

        <!-- Modal -->
        <div class="modal" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Thêm mới Mater Vessel</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="row">
                            <div class="col-md-3">
                                <label for="ID">cate</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="cateid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">Area</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="Areaid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="ID">Country</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="Countryid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="exampleInputEmail1">DestCity</label>
                                <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                                <asp:TextBox ID="DestCityid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <label for="ID">DestCityName</label>
                            <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                            <asp:TextBox ID="DestCityNameid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="exampleInputEmail1">PIC</label>
                            <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                            <asp:TextBox ID="PICid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="ID">Consignee</label>
                            <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                            <asp:TextBox ID="Consigneeid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="exampleInputEmail1">FCL_Ex_factory</label>

                            <asp:TextBox ID="FCL_Ex_factoryid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label for="ID">FCL_ETD</label>

                            <asp:TextBox ID="FCL_ETDid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="exampleInputEmail1">FCL_ETA</label>

                            <asp:TextBox ID="FCL_ETAid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="ID">LLC_Ex_factory</label>

                            <asp:TextBox ID="LLC_Ex_factoryid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="exampleInputEmail1">LLC_ETD</label>

                            <asp:TextBox ID="LLC_ETDid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label for="ID">LLC_ETA</label>

                            <asp:TextBox ID="LLC_ETAid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="exampleInputEmail1">AIR_Ex_factory</label>

                            <asp:TextBox ID="AIR_Ex_factoryid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="ID">AIR_ETD</label>

                            <asp:TextBox ID="AIR_ETDid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-md-3">
                            <label for="exampleInputEmail1">AIR_ETA</label>

                            <asp:TextBox ID="AIR_ETAid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>

                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-3">
                            <label for="ID">Special_exfactory_date</label>

                            <asp:TextBox ID="Special_exfactory_dateid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="exampleInputEmail1">SpecialETD_week</label>

                            <asp:TextBox ID="SpecialETD_weekid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="ID">Special_ETA_Date</label>

                            <asp:TextBox ID="Special_ETA_Dateid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="exampleInputEmail1">Can_combine</label>

                            <asp:TextBox ID="Can_combineid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>

                        </div>
                    </div>

                    <!-- Lặp lại thêm các dòng -->



                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                        <button type="button" class="btn btn-primary" runat="server" onserverclick="themhanghoa">Ghi lại</button>
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

        function openEditModal3(id, cate, Area, Country, DestCity, DestCityName, PIC, Consignee, FCL_Ex_factory, FCL_ETD, FCL_ETA, LLC_Ex_factory, LLC_ETD, LLC_ETA, AIR_Ex_factory, AIR_ETD, AIR_ETA, Special_exfactory_date, SpecialETD_week, Special_ETA_Date, Can_combine) {
            $("#IDedit").val(id);
            $("#idcate").val(cate);
            $("#idArea").val(Area);
            $("#idCountry").val(Country);
            $("#idDestCity").val(DestCity);
            $("#idDestCityName").val(DestCityName);
            $("#idPIC").val(PIC);
            $("#idConsignee").val(Consignee);
            $("#idFCL_Ex_factory").val(FCL_Ex_factory);
            $("#idFCL_ETD").val(FCL_ETD);
            $("#idFCL_ETA").val(FCL_ETA);
            $("#idLLC_Ex_factory").val(LLC_Ex_factory);
            $("#idLLC_ETD").val(LLC_ETD);
            $("#idLLC_ETA").val(LLC_ETA);
            $("#idAIR_Ex_factory").val(AIR_Ex_factory);
            $("#idAIR_ETD").val(AIR_ETD);
            $("#idAIR_ETA").val(AIR_ETA);
            $("#idSpecial_exfactory_date").val(Special_exfactory_date);
            $("#idSpecialETD_week").val(SpecialETD_week);
            $("#idSpecial_ETA_Date").val(Special_ETA_Date);
            $("#idCan_combine").val(Can_combine);
            
            $('#myModal3').modal('show');
        }

        function openEditModal4(id, Country) {
            $("#txtid_del").val(id);
            $("#txtCountry_del").val(Country);
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
