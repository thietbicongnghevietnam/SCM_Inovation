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

    <style>
        .horizontal-radio-group {
            display: flex;
            flex-wrap: wrap; /* Tự xuống hàng nếu màn hình nhỏ */
            gap: 10px; /* Khoảng cách giữa các nút */
            align-items: center;
            margin-bottom: 25px;
        }

        .navbar-nav {
            display: flex;
            gap: 15px; /* khoảng cách giữa các ô */
        }

            .navbar-nav .nav-item .nav-link {
                position: relative;
                padding: 8px 12px;
                color: white;
                font-size: 18px;
                font-weight: 500;
                text-decoration: none;
                border: 2px solid transparent; /* chuẩn bị viền */
                border-radius: 6px;
                transition: color 0.3s ease, border-color 0.3s ease;
            }

                .navbar-nav .nav-item .nav-link:hover {
                    color: #FFD700; /* vàng nổi bật khi hover */
                    border-color: #FFD700; /* viền vàng khi hover */
                }

                /* Hiệu ứng gạch chân chuyển động */
                .navbar-nav .nav-item .nav-link::after {
                    content: "";
                    position: absolute;
                    left: 10%;
                    right: 10%;
                    bottom: 2px;
                    height: 2px;
                    background: #FFD700;
                    transform: scaleX(0);
                    transition: transform 0.3s ease;
                    border-radius: 1px;
                }

                .navbar-nav .nav-item .nav-link:hover::after {
                    transform: scaleX(1);
                }

            /* Icon bars giữ nguyên màu trắng */
            .navbar-nav .nav-item:first-child .nav-link {
                border: none;
                padding: 8px 10px;
            }
    </style>

</head>


<body>
    <form id="form1" runat="server">
        <div>
            <nav class="navbar navbar-expand navbar-dark bg-primary">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a class="nav-link" data-widget="pushmenu" href="#" role="button"><i class="fas fa-bars"></i></a>
                    </li>
                    <li class="nav-item d-none d-sm-inline-block">
                        <a href="/frmVanningDateSCM.aspx" class="nav-link">Home
                        </a>
                    </li>
                    <li class="nav-item d-none d-sm-inline-block">
                        <a href="/frmMaterVessel.aspx" target="_blank" class="nav-link">Master vessel
                        </a>
                    </li>
                    <li class="nav-item d-none d-sm-inline-block">
                        <a href="/frmMaterModel.aspx" target="_blank" class="nav-link">Master model
                        </a>
                    </li>
                </ul>
            </nav>
        </div>

        <div class="card">
            <div class="card-header">
                <div class="col-sm-12">
                    <h3><b style="font-size: 30px;">SHIPMENT PLAN BY DEMAND</b></h3>
                    <p style="color: blue;">
                        <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
                    </p>
                </div>

                <div class="col-sm-12">
                    <!-- Import file excel -->
                    <div style="font: 14px Verdana;">
                        <div style="display: flex; gap: 40px; margin-left: 5px;">
                            <!-- Nhóm nút 1 -->
                            <div style="display: flex; flex-direction: column; align-items: flex-start; width: 300px;">
                                <div class="horizontal-radio-group">
                                    <asp:RadioButton ID="rblDECT" runat="server" GroupName="rblOptions" Text="DECT" />
                                    <asp:RadioButton ID="rblDP" runat="server" GroupName="rblOptions" Text="DP" />
                                    <%--<asp:RadioButton ID="rblPJ" runat="server" GroupName="rblOptions" Text="PJ" />--%>
                                    <asp:RadioButton ID="rblMW" runat="server" GroupName="rblOptions" Text="MW" />
                                    <%--<asp:RadioButton ID="rblSound" runat="server" GroupName="rblOptions" Text="SB" />--%>
                                    <%--<asp:RadioButton ID="rblTV" runat="server" GroupName="rblOptions" Text="TV" />--%>
                                    <asp:RadioButton ID="rblCAM" runat="server" GroupName="rblOptions" Text="DSC" />
                                </div>
                                <button type="button" class="btn btn-primary" style="margin-bottom: 10px; width: 250px;" runat="server">
                                    <%--onserverclick="btnDownloadClick" --%>
                                    <i class="fas fa-download"></i>Download Template
                                </button>


                            </div>

                            <!-- Nhóm nút 2 -->
                            <div style="display: flex; flex-direction: column; align-items: flex-start; width: 300px;">
                                <p style="margin-top: 0px; margin-left: 20px; margin-bottom: 25px;">
                                    <asp:FileUpload ID="FileUpload" Width="450px" runat="server" />
                                </p>
                                <input type="button" value="Import Demand" runat="server" onserverclick="ImportFromExcel" class="btn btn-primary" style="width: 250px;" />
                            </div>

                            <!-- Nhóm nút 3 -->
                            <div style="display: flex; flex-direction: column; align-items: flex-start; width: 300px;">
                                <button class="btn btn-primary" type="button" runat="server" onserverclick="btnTinhLichTau" style="margin-bottom: 10px; width: 250px;">
                                    <i class="fa fa-calculator"></i>&nbsp; Define ETD Psnv
                                </button>
                                <button class="btn btn-primary" type="button" runat="server" onserverclick="btnSplitCont" style="width: 250px;">
                                    <i class="fas fa-compress"></i>&nbsp; Balance Cont weekly
                                </button>
                            </div>
                            <!-- Nhóm nút 4 -->
                            <div style="display: flex; flex-direction: column; align-items: flex-start; width: 300px;">
                                <button class="btn btn-primary" type="button" runat="server" onserverclick="btnRisK" style="margin-bottom: 10px; width: 250px;">
                                    <i class="fas fa-exclamation-triangle"></i>&nbsp; Findout Risky
                                </button>

                                <button class="btn btn-primary" type="button" runat="server" onserverclick="btnRemark" style="width: 250px;">
                                    <i class="fas fa-comment"></i>&nbsp; Explain reasons
                                </button>
                            </div>
                            <!-- Nhóm nút 5 -->
                            <div style="display: flex; flex-direction: column; align-items: flex-start; width: 300px;">
                                <button class="btn btn-primary" type="button" runat="server" onserverclick="btnSaveHistory" style="margin-bottom: 10px; width: 250px;">
                                    <i class="fas fa-save"></i>&nbsp; Save History
                                </button>

                                <button class="btn btn-primary" type="button" runat="server" onserverclick="Download_Click" style="width: 250px;">
                                    <i class="fa fa-download"></i>&nbsp;Final Export
                                </button>
                            </div>


                        </div>
                    </div>
                </div>

            </div>

            <div class="col-sm-12" style="display: flex; align-items: center; flex-wrap: wrap; gap: 20px;">
                <!-- Checkbox -->
                <div style="display: flex; align-items: center;">
                    <asp:CheckBox ID="check_history_search" runat="server" Text="" />
                    <label style="margin-left: 5px;">Show History</label>
                </div>

                <!-- From / To Date -->
                <div style="display: flex; align-items: center; gap: 5px;">
                    <label for="Date1" style="margin: 0;">From:</label>
                    <input type="date" id="Date1" name="date" runat="server" class="form-control" style="width: 150px;" />
                    <label for="ngaychiid" style="margin: 0;">To:</label>
                    <input type="date" id="ngaychiid" name="date" runat="server" class="form-control" style="width: 150px;" />
                </div>

                <!-- Model -->
                <div style="display: flex; align-items: center; gap: 5px;">
                    <label for="model_search" style="margin: 0;">Model:</label>
                    <input type="text" id="model_search" runat="server" class="form-control" style="width: 150px;" />
                </div>

                <!-- Country -->
                <div style="display: flex; align-items: center; gap: 5px;">
                    <label for="country_search" style="margin: 0;">Country:</label>
                    <input type="text" id="country_search" runat="server" class="form-control" style="width: 150px;" />
                </div>

                <div style="display: flex; flex-direction: row; align-items: center; gap: 10px;">
                    <div class="form-group" style="margin: 0;">
                        <asp:DropDownList ID="dr_filter_namegroup" runat="server"
                            AppendDataBoundItems="true"
                            DataTextField="NameGroup"
                            DataValueField="NameGroup"
                            CssClass="custom-select custom-select-sm form-control form-control-sm" />
                    </div>

                    <div class="form-group" style="margin: 0;">
                        <asp:DropDownList ID="dr_filter_Cate" runat="server"
                            AppendDataBoundItems="true"
                            DataTextField="Description"
                            DataValueField="Description"
                            CssClass="custom-select custom-select-sm form-control form-control-sm" />
                    </div>
                </div>

                <!-- Filter button -->
                <div>
                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">
                        <i class="fa fa-fw fa-lg fa-search"></i>Filter Data
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
                            <%--<th>Sheet</th>--%>
                            <th>Cat</th>
                            <th>Shipmode</th>
                            <th>Consignee</th>
                            <th>Country</th>
                            <th>Destination</th>
                            <th>Model</th>
                            <th>Quantity</th>
                            <%-- <th>ATPdate</th>--%>
                            <th>ATP jit date</th>

                            <%--<th>Volume</th>--%>
                            <th>TTL gross weight (KG)</th>
                            <th>TTL Volume (M3)</th>
                            <%-- <th>TTLcont</th>--%>
                            <%-- <th>Qtycont</th>--%>
                            <%--<th>TTLcont2</th>--%>
                            <th>ETD PSNV</th>
                            <th>ETD Port</th>
                            <th>ETA Port</th>
                            <th>Remark</th>
                            <%--<th>Cancombine</th>--%>
                            <%-- <th>Risky</th>--%>
                            <%-- <th>NameGroup</th>--%>
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
                        <%--<td><%=rows["Sheet"].ToString()%></td>--%>

                        <td><%=rows["Cat"].ToString()%></td>
                        <%if (rows["Shipmode"].ToString() == "S")
                            {
                        %>
                        <td>SEA</td>
                        <%} %>
                        <%else if (rows["Shipmode"].ToString() == "A")
                            { %>
                        <td>AIR</td>
                        <%} %>

                        <td><%=rows["Consignee"].ToString()%></td>
                        <td><%=rows["Country"].ToString()%></td>
                        <td><%=rows["Destination"].ToString()%></td>
                        <td><%=rows["Model"].ToString()%></td>
                        <%--<td><%=rows["Quantity"].ToString()%></td>--%>
                        <td><%= String.Format("{0:N0}", Convert.ToInt32(rows["Quantity"])) %></td>
                        <%-- <td><%=rows["ATPdate"].ToString()%></td>--%>
                        <td><%= Convert.ToDateTime(rows["ATPdate"]).ToString("dd/MM/yyyy") %></td>

                        <%--<td><%=rows["Volume"].ToString()%></td>--%>
                        <td>
                            <%--<%=rows["Grossweight"].ToString()%>--%>
                            <%= String.Format("{0:N2}", Convert.ToDouble(rows["Grossweight"])) %>
                        </td>
                        <%-- <td><%=rows["TTLVolume"].ToString()%></td>--%>
                        <td><%= String.Format("{0:N2}", Convert.ToDouble(rows["TTLVolume"])) %></td>
                        <%-- <td><%=rows["TTLcont"].ToString()%></td>--%>
                        <%-- <td><%=rows["Qtycont"].ToString()%></td>--%>
                        <%--<td><%=rows["TTLcont2"].ToString()%></td>--%>
                        <td>
                            <%--<%=rows["Exfactorydate"].ToString()%>--%>
                            <%= (rows["Exfactorydate"] != DBNull.Value && rows["Exfactorydate"] != null && !string.IsNullOrEmpty(rows["Exfactorydate"].ToString())) 
    ? Convert.ToDateTime(rows["Exfactorydate"]).ToString("dd/MM/yyyy") 
    : ""  %>
                        </td>
                        <td>
                            <%-- <%=rows["ETD"].ToString()%>--%>
                            <%= (rows["ETD"] != DBNull.Value && rows["ETD"] != null && !string.IsNullOrEmpty(rows["ETD"].ToString())) 
    ? Convert.ToDateTime(rows["ETD"]).ToString("dd/MM/yyyy") 
    : "" %>
                        </td>
                        <td>
                            <%--<%=rows["ETA"].ToString()%>--%>
                            <%= (rows["ETA"] != DBNull.Value && rows["ETA"] != null && !string.IsNullOrEmpty(rows["ETA"].ToString())) 
    ? Convert.ToDateTime(rows["ETA"]).ToString("dd/MM/yyyy") 
    : "" %>
                        </td>
                        <td><%=rows["Remark"].ToString()%></td>
                        <%--<td><%=rows["Cancombine"].ToString()%></td>--%>
                        <%--<td><%=rows["Risky"].ToString()%></td>--%>
                        <%-- <td style='<% if (rows["Risky"].ToString() == "Chú ý LCL") { %>background-color: yellow; color: red; <% } %>'>
                            <%=rows["Risky"].ToString()%>
                        </td>--%>
                        <%--<td><%=rows["NameGroup"].ToString()%></td>--%>
                        <td>
                            <!-- Cột trống này giữ nguyên -->
                            <a href="#" title="eidt item" onclick="openEditModal3('<%= rows["ID"].ToString() %>','<%= rows["Exfactorydate"].ToString() %>','<%= rows["ETD"].ToString() %>')"><i class="fas fa-edit"></i></a>
                            <a href="#" title="detail item" onclick="openEditModal4('<%= rows["ID"].ToString() %>',
                                '<%= rows["Exfactorydate"].ToString() %>',
                                '<%= rows["ETD"].ToString() %>',
                                '<%= rows["Consignee"].ToString() %>',
                                '<%= rows["Country"].ToString() %>',
                                '<%= rows["Destination"].ToString() %>',
                                '<%= rows["Model"].ToString() %>',
                                '<%= rows["Quantity"].ToString() %>',
                                '<%= rows["TTLcont"].ToString() %>',
                                '<%= rows["Qtycont"].ToString() %>',
                                '<%= rows["TTLcont2"].ToString() %>',
                                '<%= rows["Cancombine"].ToString() %>',
                                '<%= rows["Risky"].ToString() %>',
                                )"><i class="fas fa-info-circle"></i></a>
                        </td>


                    </tr>
                    <%} %>
                </tbody>
                <tfoot>
                    <tr>
                        <th>ID</th>
                        <%--<th>Sheet</th>--%>
                        <th>Cat</th>
                        <th>Shipmode</th>
                        <th>Consignee</th>
                        <th>Country</th>
                        <th>Destination</th>
                        <th>Model</th>
                        <th>Quantity</th>
                        <%-- <th>ATPdate</th>--%>
                        <th>ATP jit date</th>

                        <%--<th>Volume</th>--%>
                        <th>TTL gross weight (KG)</th>
                        <th>TTL Volume (M3)</th>
                        <%-- <th>TTLcont</th>--%>
                        <%-- <th>Qtycont</th>--%>
                        <%--<th>TTLcont2</th>--%>
                        <th>ETD PSNV</th>
                        <th>ETD Port</th>
                        <th>ETA Port</th>
                        <th>Remark</th>
                        <%--<th>Cancombine</th>--%>
                        <%-- <th>Risky</th>--%>
                        <%-- <th>NameGroup</th>--%>
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
                                <h4 class="modal-title" id="headerTag1" style="float: left">Update vessel Information</h4>
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
                                <label for="exFactoryDate">Ex-factory Date</label>
                                <asp:TextBox ID="IDexfactorydate" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exFactoryDate"><i style="color: green">Update Ex-factory Date</i></label>
                                <input type="date" id="exFactoryDate" class="form-control" name="exFactoryDate" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="exETD">ETD Date</label>
                                <asp:TextBox ID="IDETDdate" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="etdDate"><i style="color: green">Update ETD Date</i></label>
                                <input type="date" id="etdDate" class="form-control" name="etdDate" runat="server" />
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

        <div class="modal" id="myModal4">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag12" style="float: left">Detail Information</h4>
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
                            <%--<div class="col-md-12">
                        <label for="ID">ID</label>
                        <asp:TextBox ID="IDedit2" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>--%>
                            <%-- <div class="col-md-3"></div>--%>
                            <%-- <div class="col-md-3"></div>--%>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="exFactoryDate">Ex-factory Date</label>
                                <asp:TextBox ID="IDexfactorydate2" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="exFactoryDate">ETD Date</label>
                                <asp:TextBox ID="IDETDdate2" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="exETD">Consignee</label>
                                <asp:TextBox ID="idConsignee" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="etdDate">Country</label>
                                <asp:TextBox ID="idCountry" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="exETD">Destination</label>
                                <asp:TextBox ID="idDestination" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="etdDate">Model</label>
                                <asp:TextBox ID="idModel" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="exETD">Quantity</label>
                                <asp:TextBox ID="idQuantity" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="etdDate">TTLcont</label>
                                <asp:TextBox ID="idTTLcont" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="exETD">Qtycont</label>
                                <asp:TextBox ID="idQtycont" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="etdDate">TTLcont2</label>
                                <asp:TextBox ID="idTTLcont2" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="exETD">Cancombine</label>
                                <asp:TextBox ID="idCancombine" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="etdDate">Risky</label>
                                <asp:TextBox ID="idRisky" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <%-- Modal footer --%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Đóng</button>
                        <%-- <button type="button" runat="server" id="Button2"  class="btn btn-primary" onserverclick="Updatethongtin"> 
                    <i class="fas fa-download"></i>
                    Ghi lại
                </button>--%>
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
            $('#example thead th').slice(0, 9).css('background-color', '#FFDAB9');
            $('#example thead th').slice(9).css('background-color', '#B0E0E6');

            // Footer cũng tô màu tương tự
            $('#example tfoot th').slice(0, 9).css('background-color', '#FFDAB9');
            $('#example tfoot th').slice(9).css('background-color', '#B0E0E6');
        });

        $(function () {
            $("#example").DataTable({
                //"responsive": false,
                "autoWidth": true,
                //"order": [[7, "desc"]],
                "scrollX": true,
                "pageLength": 50
                //"ordering": true,
                //"paging": true,
                //"lengthChange": false,
                //"searching": false,
                //"info": true,                    
            });

        });

        function openEditModal3(id, exfactory_date, ETA_Date) {
            $("#IDedit").val(id);
            //$("#exFactoryDate").val(exfactory_date);
            //$("#etdDate").val(ETA_Date);
            // Ensure the date is in YYYY-MM-DD format
            $("#IDexfactorydate").val(exfactory_date);
            $("#IDETDdate").val(ETA_Date);

            $('#myModal3').modal('show');
        }

        function openEditModal4(id, exfactory_date, ETD_Date, Consignee, Country, Destination, Model, Quantity, TTLcont, Qtycont, TTLcont2, Cancombine, Risky) {
            /*$("#IDedit2").val(id);*/
            //$("#exFactoryDate").val(exfactory_date);
            //$("#etdDate").val(ETA_Date);
            // Ensure the date is in YYYY-MM-DD format
            $("#IDexfactorydate2").val(exfactory_date);
            $("#IDETDdate2").val(ETD_Date);
            $("#idConsignee").val(Consignee);
            $("#idCountry").val(Country);
            $("#idDestination").val(Destination);
            $("#idModel").val(Model);
            $("#idQuantity").val(Quantity);
            $("#idTTLcont").val(TTLcont);
            $("#idQtycont").val(Qtycont);
            $("#idTTLcont2").val(TTLcont2);
            $("#idCancombine").val(Cancombine);
            $("#idRisky").val(Risky);

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
