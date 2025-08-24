<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPhthalate.aspx.cs" Inherits="FreeLayout.QCC.UploadPhthalate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Master Phthalate</title>
    <link rel="stylesheet" href="/plugins/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" href="/plugins/datatables-responsive/css/responsive.bootstrap4.min.css" />
    <link rel="stylesheet" href="/dist/css/adminlte.min.css" />

    <link rel="stylesheet" href="/plugins/toastr/toastr.css" />
    <%--<link rel="stylesheet" href="/plugins/toastr/toastr.min.css" />--%>
    <script src="/plugins/jquery/jquery.min.js"></script>
    <script src="/plugins/toastr/toastr.js"></script>


    <link rel="stylesheet" href="/plugins/fontawesome-free/css/jquery-ui.css" />

   <%-- <script>
        function pageRedirect() {
            window.location.replace("#");
        }
        setTimeout("pageRedirect()", 10000);
    </script>--%>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="card-header">
                <h1>Upload Master Phthalate</h1>
            </div>

            <div class="card card-primary">
                <div class="card-header">
                    <h3 class="card-title">Add New!</h3>
                </div>
                <p>
                    <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
                </p>

                <!-- import file excel -->
                <!-- ADD A FILE UPLOAD CONTROL AND A BUTTON TO EXECUTE. -->
                <div style="font: 14px Verdana; float: right">
                    <p style="margin-top: 20px; margin-left: 20px;">
                        Select a file to upload:
                        <asp:FileUpload ID="FileUpload" Width="450px" runat="server" />
                    </p>
                    <p style="margin-top: 20px; margin-left: 20px;">
                        <input type="button" onserverclick="ImportFromExcel" value="Import data to Excel" runat="server" class="btn btn-primary" />

                        <button type="button" class="btn btn-primary float-right" style="margin-right: 5px;" onserverclick="btnDownloadClick" runat="server">
                            <i class="fas fa-download"></i>Dowload template file
                        </button>
                    </p>
                    <p>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </p>

                </div>


            </div>

            <div class="row">
                <span style="float: left; margin-left:50px;"><b>Material Input :</b></span>
                <span style="float: left; margin-left: 5px; margin-right: 3px;">
                    <asp:TextBox ID="txt_mahang" type="text" Width="200" name="mahang" runat="server" CssClass="form-control"></asp:TextBox>
                </span>
                <button type="submit" class="btn btn-primary" style="margin-left:20px;" id="search_mahang" onserverclick="btnfindmater" runat="server">Find material</button>
            </div>


            <div class="card-body">
                <div id="example1_wrapper" class="dataTables_wrapper dt-bootstrap4">

                    <div class="row">
                        <div class="col-sm-12">

                            <table id="example1" class="table table-bordered">
                                <thead>
                                    <tr role="row">
                                        <th>ID</th>
                                        <th>Material</th>
                                        <th>Userid</th>
                                        <th>CreateDate</th> 
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%foreach (System.Data.DataRow rows in dt.Rows)
                                        {%>

                                    <tr role="row">
                                        <td><%=rows[0].ToString() %></td>
                                        <td><%=rows[1].ToString() %></td>
                                        <td><%=rows[2].ToString() %></td>
                                        <td><%=rows[4].ToString() %></td> 
                                        <td>                                           
                                            <a href="#" class="btn btn-info btn-sm" title="delete item" onclick="openEditModal2('<%= rows[0].ToString() %>','<%= rows[1].ToString() %>')"><i class="far fa-trash-alt"></i>Delete</a>
                                        </td>
                                    </tr>

                                    <% } %>
                                </tbody>

                                <tfoot>
                                    <tr>
                                        <th>ID</th>
                                        <th>Material</th>
                                        <th>Userid</th>
                                        <th>CreateDate</th>   
                                        <th>Action</th>
                                    </tr>
                                </tfoot>
                            </table>

                        </div>
                    </div>
                </div>
                <!-- /.card-body -->
            </div>
        </div>

        <div class="modal" id="myModal2">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag" style="float: left">Do you want to delete item? </h4>

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
                                        <asp:TextBox ID="txtid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Material</label>
                                        <asp:TextBox ID="txtmaterial" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">UserID</label>
                                        <span style="color: red; font-size: 11px; font-style: italic;">You must input!(*)</span>
                                        <asp:TextBox ID="txtuserid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Close</button>
                        <button type="button" runat="server" id="btndeleteitem" onserverclick="BtndeleteItem_p" class="btn btn-primary">
                            <i class="fas fa-download"></i>
                            Save
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

    <script type="text/javascript">       
        $(document).ready(function () {
            $('#txtid').prop("readonly", true);
            $('#txtmaterial').prop("readonly", true);
        });

        $(function () {
            $("#example1").DataTable({
                "responsive": true,
                "autoWidth": false,
                //"order": [[0, "desc"]],
                "order": [[0, "desc"]],
                "pageLength": 100
                //"ordering": true,
                //"paging": true,
                //"lengthChange": false,
                //"searching": false,
                //"info": true,                    
            });
        });

        function openEditModal2(txt_id, txt_mahang) {
            $("#txtid").val(txt_id);
            $("#txtmaterial").val(txt_mahang);

            $('#myModal2').modal('show');
        }

        


    </script>

</body>
</html>
