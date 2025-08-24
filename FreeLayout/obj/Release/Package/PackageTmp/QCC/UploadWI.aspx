<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadWI.aspx.cs" Inherits="FreeLayout.QCC.UploadWI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload WI QCC</title>
    <link rel="stylesheet" href="/plugins/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" href="/plugins/datatables-responsive/css/responsive.bootstrap4.min.css" />
    <link rel="stylesheet" href="/dist/css/adminlte.min.css" />

    <link rel="stylesheet" href="/plugins/toastr/toastr.css" />
    <%--<link rel="stylesheet" href="/plugins/toastr/toastr.min.css" />--%>
    <script src="/plugins/jquery/jquery.min.js"></script>
    <script src="/plugins/toastr/toastr.js"></script>


    <link rel="stylesheet" href="/plugins/fontawesome-free/css/jquery-ui.css" />

</head>
<body>
    <form id="form1" runat="server">

        <%--<div class="card-header">--%>
        <%--<h1>Upload WI</h1>--%>
        <%--class="card-title"--%>
        <%--  </div>--%>

        <%--<div class="card card-primary">--%>
        <%--<div class="card-header">
                    <h3 class="card-title">Upload Document WI!</h3>
                </div>--%>
        <!-- /.card-header -->
        <!-- form start -->

        <%--<div class="card-body">
                    <div class="form-group">--%>
        <%--<label for="exampleInputEmail1">Material</label>
                        <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                        <input type="text" name="material" class="form-control" id="material" runat="server" placeholder="Enter material">--%>
        <%--</div>--%>

        <%-- <div class="form-group">--%>
        <%--<label for="exampleInputFile">File input</label>
                        <span style="color: red; font-size: 11px; font-style: italic;">(*)</span>
                        <div class="input-group">
                            <div class="custom-file">
                               
                                <input type="file" name="FileUpload" class="custom-file-input" />
                                <label class="custom-file-label" for="exampleInputFile">Choose file</label>
                            </div>
                           
                        </div>--%>
        <%-- </div>

                </div>--%>
        <!-- /.card-body -->


        <%-- <div class="card-footer">--%>

        <%--<asp:Button ID="Button1" Text="Upload" runat="server" OnClick="Upload" />--%>
        <%--</div>--%>

        <%-- <br />--%>
        <%-- <asp:Label ID="lblMessage" Text="File uploaded successfully." runat="server" ForeColor="Green" Visible="false" />--%>

        <%--</div>--%>


        <div class="card card-primary">
            <div class="card-header">
                <h3 class="card-title">Upload Excel file!</h3>
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

        <div class="card-body">
            <div id="example1_wrapper" class="dataTables_wrapper dt-bootstrap4">

                <div class="row">
                    <div class="col-sm-12">

                        <table id="example1" class="table table-bordered">
                            <thead>
                                <tr role="row">

                                    <th>ID</th>
                                    <th>Material</th>
                                    <th>Cate</th>
                                    <th>Path</th>
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
                                    <td><%=rows[3].ToString() %></td>
                                    <td>
                                        <a class="btn btn-info btn-sm" href="#" id="<%= rows[0].ToString() %>" onclick="openEditModal('<%= rows[0].ToString() %>','<%= rows[1].ToString() %>','<%= rows[2].ToString() %>','<%= rows[3].ToString() %>')">
                                            <i class="fas fa-pencil-alt"></i>
                                            Edit
                                        </a>
                                    </td>
                                </tr>
                                <% } %>
                            </tbody>

                            <tfoot>
                                <tr>
                                    <th>ID</th>
                                    <th>Material</th>
                                    <th>Cate</th>
                                    <th>Path</th>
                                    <th>Action</th>
                                </tr>
                            </tfoot>
                        </table>

                    </div>
                </div>
            </div>
            <!-- /.card-body -->
        </div>

        <div class="modal" id="myModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag" style="float: left">Edit WI document!</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                    </div>


                    <div class="modal-body">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ID">ID</label>
                                        <span style="color: green; font-size: 11px; font-style: italic;">(Read only)</span>
                                        <asp:TextBox ID="idcheck" CssClass="form-control" placeholder="ID" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Material</label>
                                        <asp:TextBox ID="material" CssClass="form-control" placeholder="Material" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Cate</label>
                                        <asp:TextBox ID="cate" CssClass="form-control" placeholder="Category" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Path</label>
                                        <span style="color: red; font-size: 11px; font-style: italic;">Do You want to edit? (*)</span>
                                        <asp:TextBox ID="path" CssClass="form-control" placeholder="path document" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <%--<span style="color: red; font-size: 11px; font-style: italic;">(*)</span>--%>
                                <div class="input-group">
                                    <div class="custom-file">
                                        <%--<input type="file" name="FileUpload" id="myFile" class="custom-file-input"  />
                                        <label class="custom-file-label" for="exampleInputFile">Choose file</label>--%>
                                    </div>

                                </div>


                            </div>

                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Close</button>
                        <button type="button" runat="server" id="btnadd" onserverclick="UpdateClick" class="btn btn-primary">
                            <i class="fas fa-download"></i>Save</button>
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

        $(function () {
            $("#example1").DataTable({
                "responsive": true,
                "autoWidth": false,
                "order": [[0, "desc"]],
                "pageLength": 50
                //"ordering": true,
                //"paging": true,
                //"lengthChange": false,
                //"searching": false,
                //"info": true,                    
            });
        });


        function openEditModal(txt_id, txt_mahang, txt_cate, txt_path) {
            $("#idcheck").val(txt_id);
            $("#material").val(txt_mahang);
            $("#cate").val(txt_cate);
            var data = { material: txt_mahang };
            $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "UploadWI.aspx/getfulpath",
                    data: JSON.stringify(data),
                    dataType: "json",
                    success: function (data) {
                        $("#path").val(data.d);
                        },
                        error: function () {
                           
                        }
                });

            //$("#path").val(txt_path);

            $('#myModal').modal('show');
        };

        // $('input[type=file]').change(function () {
        //     //console.log(this.files[0].mozFullPath);
        //     var path = (window.URL || window.webkitURL).createObjectURL(this.files[0].mozFullPath);
        //     alert(path);
              
        //});

        //function test() {

           
            //var fil = document.getElementById("myFile");   
            //var aa = $('#myFile').val();
            ////var path = (window.URL || window.webkitURL).createObjectURL(fil);
            ////console.log('path', path);
            ////alert(fil.value);
            //alert(path);


        //}

    </script>


</body>
</html>
