<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyMaterial.aspx.cs" Inherits="FreeLayout.QCC.DailyMaterial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List Daily Material</title>
    <link rel="stylesheet" href="/plugins/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" href="/plugins/datatables-responsive/css/responsive.bootstrap4.min.css" />
    <link rel="stylesheet" href="/dist/css/adminlte.min.css" />

    <link rel="stylesheet" href="/plugins/toastr/toastr.css" />
    <%--<link rel="stylesheet" href="/plugins/toastr/toastr.min.css" />--%>
    <script src="/plugins/jquery/jquery.min.js"></script>
    <script src="/plugins/toastr/toastr.js"></script>


    <link rel="stylesheet" href="/plugins/fontawesome-free/css/jquery-ui.css" />
    <%--/plugins/fontawesome-free/css/images--%>
</head>
<body>
    <form id="form1" runat="server">

        <div class="row">
            <div class="col-12">


                <div class="card">
                    <div class="card-header">
                        <h1>Material list to check</h1>
                        <%--class="card-title"--%>
                        <div class="col-sm-12">
                            Delivery Date:
                                    <input type="text" id="datepicker" runat="server">
                            <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">
                                <i class="fa fa-fw fa-lg fa-search"></i>Filter</button>


                            <button type="button" class="btn" style="margin-left: 50px; background-color: whitesmoke">
                                Waiting
                            </button>
                            <button type="button" class="btn" style="margin-left: 20px; background-color: #FFFF99">
                                Pending
                            </button>
                            <button type="button" class="btn" style="margin-left: 20px; background-color: #66FF33">
                                Finished
                            </button>

                            <button type="button" class="btn" style="margin-left: 20px; background-color: red">
                                Urgent
                            </button>

                        </div>

                    </div>
                    <!-- /.card-header -->
                    <div class="card-body">
                        <div id="example1_wrapper" class="dataTables_wrapper dt-bootstrap4">

                            <div class="row">
                                <div class="col-sm-12">

                                    <table id="example1" class="table table-bordered">
                                        <thead>
                                            <tr role="row">
                                                <%--<th class="sorting_asc" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Rendering engine: activate to sort column descending">Rendering engine</th>
                                            <th class="sorting" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Browser</th>
                                            <th class="sorting" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Platform(s)</th>
                                            <th class="sorting" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Engine version</th>
                                            <th class="sorting" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">CSS grade</th>--%>
                                               <%-- <th>ID</th>--%>
                                                <th>Action</th>
                                                <th>Material</th>
                                                <th>Location</th>                                                
                                                <th>Qty</th>
                                                <th>Invoice</th>
                                                <%--<th>[Dateinsert]</th>--%>
                                                <%--<th>Status</th>--%>
                                                <th>Delivery Date</th>
                                                <th>Note</th>
                                                <th>Vender</th>
                                                <th>Plan</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <%foreach (System.Data.DataRow rows in dt.Rows)
                                                {%>

                                            <%if (rows[13].ToString() == "pending")
                                                {%>

                                            <%if (rows[15].ToString() == "Urgent")
                                                {%>
                                            <tr role="row" style="background-color: #FFFF99">
                                                <%--<td><%=rows[0].ToString() %></td>--%>
                                                <td>
                                                    <%--<button type="button" class="btn btn-primary">Check</button>--%>
                                                    <a class="btn btn-info btn-sm" href="#">
                                                        <i class="fas fa-pencil-alt"></i>
                                                        Check
                                                    </a>

                                                    <a class="btn btn-primary btn-sm" href="#" onclick="openEditModal2('<%= rows[0].ToString() %>','<%= rows[1].ToString() %>')">
                                                        <i class="fas fa-save"></i>
                                                        Finish
                                                    </a>
                                                </td>
                                                <td><%=rows[1].ToString() %></td>
                                                <td><%=rows[2].ToString() %></td>
                                                <td><%=rows[5].ToString() %></td>                                               
                                                <td><%=rows[6].ToString() %></td>
                                                <%--<td><%=rows[11].ToString() %></td>--%>
                                                <td><%=rows[14].ToString() %></td>
                                                <td><%=rows[15].ToString() %><i class="far fa-bell"></i></td>
                                                <td><%=rows[4].ToString() %></td>
                                                <td><%=rows[3].ToString() %></td>
                                                
                                            </tr>
                                            <%} %>
                                            <%else
                                                { %>
                                                    <tr role="row" style="background-color: #FFFF99">
                                                        <%--<td><%=rows[0].ToString() %></td>--%>
                                                        <td>
                                                            <%--<button type="button" class="btn btn-primary">Check</button>--%>
                                                            <a class="btn btn-info btn-sm" href="#">
                                                                <i class="fas fa-pencil-alt"></i>
                                                                Check
                                                            </a>

                                                            <a class="btn btn-primary btn-sm" href="#" onclick="openEditModal2('<%= rows[0].ToString() %>','<%= rows[1].ToString() %>')">
                                                                <i class="fas fa-save"></i>
                                                                Finish
                                                            </a>
                                                        </td>
                                                        <td><%=rows[1].ToString() %></a></td>
                                                        <td><%=rows[2].ToString() %></td>
                                                        <td><%=rows[5].ToString() %></td>                                                        
                                                        <td><%=rows[6].ToString() %></td>
                                                        <%--<td><%=rows[11].ToString() %></td>--%>
                                                        <td><%=rows[14].ToString() %></td>
                                                        <td></td>
                                                        <td><%=rows[4].ToString() %></td>
                                                        <td><%=rows[3].ToString() %></td>
                                                        
                                                    </tr>
                                            <%} %>




                                            <% } %>
                                            <%else if (rows[13].ToString() == "finished")
                                                {%>

                                            <tr role="row" style="background-color: #66FF33">
                                                <%--<td><%=rows[0].ToString() %></td>--%>
                                                 <td>
                                                    <%--<button type="button" class="btn btn-primary">Check</button>--%>
                                                    <a class="btn btn-info btn-sm" href="#">
                                                        <i class="fas fa-pencil-alt"></i>
                                                        Check
                                                    </a>
                                                    <%--<a href="#" class="btn btn-success">Finish</a>--%>
                                                    <a class="btn btn-primary btn-sm" href="#">
                                                        <i class="fas fa-save"></i>
                                                        Finish
                                                    </a>
                                                </td>
                                                <td><a href="#"><%=rows[1].ToString() %></a></td>
                                                <td><%=rows[2].ToString() %></td>
                                                <td><%=rows[5].ToString() %></td>                                                
                                                <td><%=rows[6].ToString() %></td>
                                                <%-- <td><%=rows[11].ToString() %></td>--%>
                                                <td><%=rows[14].ToString() %></td>
                                                <td></td>
                                                <td><%=rows[4].ToString() %></td>
                                                <td><%=rows[3].ToString() %></td>
                                            </tr>

                                            <%}%>

                                            <%else
                                                {%>

                                            <%if (rows[15].ToString() == "Urgent")
                                                {%>
                                            <tr role="row" style="background-color: #FF0000; color: white;">
                                               <%-- <td><%=rows[0].ToString() %></td>--%>
                                                <td>
                                                    <%--<button type="button" id="<%= rows[0].ToString() %>" onclick="openEditModal('<%= rows[0].ToString() %>','<%= rows[1].ToString() %>','<%= rows[2].ToString() %>','<%= rows[5].ToString() %>','<%= rows[6].ToString() %>')" class="btn btn-primary">Check</button>--%>
                                                    <a class="btn btn-info btn-sm" href="#" id="<%= rows[0].ToString() %>" onclick="openEditModal('<%= rows[0].ToString() %>','<%= rows[1].ToString() %>','<%= rows[2].ToString() %>','<%= rows[5].ToString() %>','<%= rows[6].ToString() %>')">
                                                        <i class="fas fa-pencil-alt"></i>
                                                        Check
                                                    </a>
                                                    <%--<a href="#" class="btn btn-success">Finish</a>--%>
                                                    <a class="btn btn-primary btn-sm" href="#">
                                                        <i class="fas fa-save"></i>
                                                        Finish
                                                    </a>
                                                </td>
                                                <td><a href="/Main.aspx?material=<%=rows[1].ToString() %>" target="_blank"><%=rows[1].ToString() %></a></td>
                                                <td><%=rows[2].ToString() %></td>
                                                <td><%=rows[5].ToString() %></td>                                                
                                                <td><%=rows[6].ToString() %></td>
                                                <%--<td><%=rows[11].ToString() %></td>--%>
                                                <td><%=rows[14].ToString() %></td>
                                                <td><%=rows[15].ToString() %><i class="far fa-bell"></i></td>
                                                <td><%=rows[4].ToString() %></td>
                                                <td><%=rows[3].ToString() %></td>
                                            </tr>

                                            <% } %>
                                            <%else
                                                { %>
                                            <tr role="row">
                                               <%-- <td><%=rows[0].ToString() %></td>--%>
                                                 <td>
                                                    <%--<button type="button" id="<%= rows[0].ToString() %>" onclick="openEditModal('<%= rows[0].ToString() %>','<%= rows[1].ToString() %>','<%= rows[2].ToString() %>','<%= rows[5].ToString() %>','<%= rows[6].ToString() %>')" class="btn btn-primary">Check</button>--%>
                                                    <a class="btn btn-info btn-sm" href="#" id="<%= rows[0].ToString() %>" onclick="openEditModal('<%= rows[0].ToString() %>','<%= rows[1].ToString() %>','<%= rows[2].ToString() %>','<%= rows[5].ToString() %>','<%= rows[6].ToString() %>')">
                                                        <i class="fas fa-pencil-alt"></i>
                                                        Check
                                                    </a>
                                                    <%--<a href="#" class="btn btn-success">Finish</a>--%>
                                                    <a class="btn btn-primary btn-sm" href="#">
                                                        <i class="fas fa-save"></i>
                                                        Finish
                                                    </a>
                                                </td>
                                                <td><a href="/Main.aspx?material=<%=rows[1].ToString() %>" target="_blank"><%=rows[1].ToString() %></a></td>
                                                <td><%=rows[2].ToString() %></td>
                                                <td><%=rows[5].ToString() %></td>                                                
                                                <td><%=rows[6].ToString() %></td>
                                                <%--<td><%=rows[11].ToString() %></td>--%>
                                                <td><%=rows[14].ToString() %></td>
                                                <td></td>
                                                <td><%=rows[4].ToString() %></td>
                                                <td><%=rows[3].ToString() %></td>
                                            </tr>

                                            <%} %>



                                            <% } %>


                                            <% } %>
                                        </tbody>

                                        <tfoot>
                                            <tr>
                                                <%--<th>ID</th>--%>
                                                 <th>Action</th>
                                                <th>Material</th>
                                                <th>Location</th>                                                
                                                <th>Qty</th>
                                                <th>Invoice</th>
                                                <%-- <th>[Dateinsert]</th>--%>
                                                <%--<th>Status</th>--%>
                                                <th>Delivery Date</th>
                                                <th>Note</th>
                                                <th>Vender</th>
                                                <th>Plant</th>
                                            </tr>
                                        </tfoot>
                                    </table>

                                </div>
                            </div>
                        </div>
                        <!-- /.card-body -->
                    </div>
                    <!-- /.card -->
                </div>
                <!-- /.col -->
            </div>




            <div class="modal" id="myModal2">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="row">
                                <div>
                                    <h4 class="modal-title" id="headerTag" style="float: left">Select Ok / NG</h4>

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
                                            <asp:TextBox ID="ID2" CssClass="form-control" placeholder="ID" runat="server"></asp:TextBox>
                                            <%--onserverclick="btnAddClick"--%>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Material</label>
                                            <asp:TextBox ID="Material2" CssClass="form-control" placeholder="Material" runat="server"></asp:TextBox>
                                            <%-- end add Modal --%>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:RadioButton ID="RadioButton1" GroupName="Passport" Text="OK" runat="server" Checked="true" onclick="ShowHideDiv()" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:RadioButton ID="RadioButton2" GroupName="Passport" Text="NG" runat="server" onclick="ShowHideDiv()" />
                                            <div id="dvPassport" style="display: none">
                                                go<input type="text" id="txtPassportNumber" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<input runat="server" type="text" class="form-control" name="username" placeholder="Enter user name">--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Close</button>
                            <button type="button" runat="server" id="Button1" onserverclick="BtnFinish" class="btn btn-primary"><i class="fas fa-download"></i>Save</button>
                            <%--<input runat="server" type="text" class="form-control" name="fullname" placeholder="Enter full name">--%>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal" id="myModal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="row">
                                <div>
                                    <h4 class="modal-title" id="headerTag" style="float: left">Check Cargo</h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div>
                                    <label for="Group" id="_comment" runat="server">Note :</label>
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
                                            <asp:TextBox ID="idcheck" CssClass="form-control" placeholder="ID" runat="server"></asp:TextBox>
                                            <%--onserverclick="btnAddClick"--%>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Material</label>
                                            <asp:TextBox ID="material" CssClass="form-control" placeholder="Material" runat="server"></asp:TextBox>
                                            <%-- end add Modal --%>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Location</label>
                                            <asp:TextBox ID="location" CssClass="form-control" placeholder="location" runat="server"></asp:TextBox>
                                            <%-- The edit Modal --%>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Qty</label>
                                            <span style="color: red; font-size: 11px; font-style: italic;">You have to select!(*)</span>
                                            <asp:TextBox ID="Qty" CssClass="form-control" placeholder="Qty" runat="server"></asp:TextBox>
                                            <%-- Modal Header --%>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Invoice</label>
                                            <asp:TextBox ID="Invoice" CssClass="form-control" placeholder="Invoice" runat="server"></asp:TextBox>
                                            <%-- Modal body --%>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="Group">Type Check</label>
                                            <span style="color: red; font-size: 11px; font-style: italic;">You have to select!(*)</span>
                                            <asp:DropDownList ID="dr_TypeChek" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="Group" id="test" runat="server"></label>
                                            <%--<asp:Button id="openfile" class="btn btn-primary" runat="server" Text="Open" OnClick="bttnpdf_Click" />--%>

                                            <button type="button" id="openfile" class="btn btn-primary">Open</button>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<input runat="server" type="text" class="form-control" name="username" placeholder="Enter user name">--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Close</button>
                            <button type="button" runat="server" id="btnadd" onserverclick="BtnAddClick" class="btn btn-primary"><i class="fas fa-download"></i>Save</button>
                            <%--<input runat="server" type="text" class="form-control" name="fullname" placeholder="Enter full name">--%>
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


    <%--<script src="/plugins/toastr/toastr.min.js"></script>--%>


    <%--   <script src="/plugins/popper/popper.min.js"></script>        
    <script src="/plugins/bootstrap/bootstrap.js"></script>  
    <script src="../Scripts/bootbox.min.js"></script>--%>
    <%-- <script src="../Scripts/bootbox.locales.min.js"></script>
    <script src="../Scripts/toastr.js"></script>  --%>



    <script type="text/javascript">
        $(document).ready(function () {
            $("#openfile").css("display", "none");
        });

        function ShowHideDiv() {
            var _OK = document.getElementById("<%=RadioButton1.ClientID %>");
            var dvPassport = document.getElementById("dvPassport");
            //dvPassport.style.display = chkYes.checked ? "block" : "none";

        }

        $(function () {
            $("#example1").DataTable({
                "responsive": true,
                "autoWidth": false,
                "order": [[5, "desc"]],
                "pageLength": 50
                //"ordering": true,
                //"paging": true,
                //"lengthChange": false,
                //"searching": false,
                //"info": true,                    
            });
            //$('#example2').DataTable({
            //    "paging": true,
            //    "lengthChange": false,
            //    "searching": false,
            //    "ordering": true,
            //    "info": true,
            //    "autoWidth": false,
            //    "responsive": true,
            //});
        });

        function openEditModal(txt_id, txt_mahang, txt_vitri, txt_soluong, txt_invoice) {
            $("#idcheck").val(txt_id);
            $("#material").val(txt_mahang);
            $("#location").val(txt_vitri);
            $("#Qty").val(txt_soluong);
            $("#Invoice").val(txt_invoice);

            //$("#MainContent_Drop_Role").val(dr_role);
            //$('h4.modal-title').text('Edit User');
            $("#test").text("");
            $("#openfile").css("display", "none");
            $("#dr_TypeChek").val("==Select==");
            var data = { material: txt_mahang };
            $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "DailyMaterial.aspx/GetNormalCheck",
                    data: JSON.stringify(data),
                    dataType: "json",
                success: function (data) {
                    $("#_comment").text("Note: " + data.d);
                    $("#_comment").css("style", "font-weight:200;color:red");
                        //$("#MainContent_txt_makercode").val(data.d);
                        //$("#MainContent_txt_qty").select();
                    },
                    error: function () {
                        //$("#MainContent_txt_makercode").select();
                        //$("#MainContent_txt_makercode").val('Không tìm thấy maker code, kiểm tra lại thông tin nhựa và Part');
                        //$("#MainContent_txt_makercode").select();
                    }
                });


            $('#myModal').modal('show');
        }
        //openEditModal1
        function openEditModal2(txt_id, txt_mahang) {
            $("#ID2").val(txt_id);
            $("#Material2").val(txt_mahang);
            $('#myModal2').modal('show');
        }

        $("#openfile").click(function () {
            var path = $("#test").text();
            window.open(path);
            //window.open(path,"window2","");
            //"url of .xls","window2",""

        })

        $("#dr_TypeChek").change(function () {
            if (this.value === 'WIdocument') {
                //alert("vao day");
                var material = $("#material").val();
                var data = { material: material };
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "DailyMaterial.aspx/getdocumentWI",
                    data: JSON.stringify(data),
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Chua co document WI!') {
                            $("#test").text(data.d);
                            $("#openfile").css("display", "none");
                        } else {
                            $("#test").text(data.d);
                            $("#openfile").css("display", "block");
                        }

                    },
                    error: function () {

                    }
                });
            }
            else if (this.value === '==Select==') {
                $("#test").text('Ban phai cho loai check!');
                $("#openfile").css("display", "none");
            }
            else {
                //alert("vao day2");
                $("#test").text('Hang dua ve QCC Room');
                $("#openfile").css("display", "none");
                //openfile

            }
        });
    </script>
    <script src="/plugins/jquery/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            var onlyDate, today = new Date();
            var dateNewFormat = '';

            onlyDate = today.getDate();
            if (onlyDate.toString().length == 2) {

                dateNewFormat = onlyDate;
            }
            else {
                dateNewFormat = '0' + onlyDate;
            }

            dateNewFormat = dateNewFormat + '-';

            if (today.getMonth().length == 2) {

                dateNewFormat += (today.getMonth() + 1);
            }
            else {
                dateNewFormat += '0' + (today.getMonth() + 1);
            }

            dateNewFormat = dateNewFormat + '-' + today.getFullYear();
            //dateNewFormat = today.getFullYear() + '-';

            $('#datepicker').val(dateNewFormat);

            $("#datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
        });
    </script>
    <%--<script>
         var onlyDate, today = new Date();
         var dateNewFormat = '';

        onlyDate = today.getDate();
        if (onlyDate.toString().length == 2) {

            dateNewFormat = onlyDate;
        }
        else {
            dateNewFormat = '0' + onlyDate;
         }

         dateNewFormat = dateNewFormat + '-';
        
        if (today.getMonth().length == 2) {

            dateNewFormat += (today.getMonth() + 1);
        }
        else {
            dateNewFormat += '0' + (today.getMonth() + 1);
        }

         dateNewFormat = dateNewFormat + '-' +today.getFullYear();
         //dateNewFormat = today.getFullYear() + '-';

         $('#datepicker').val(dateNewFormat);                

      </script>--%>
</body>
</html>
