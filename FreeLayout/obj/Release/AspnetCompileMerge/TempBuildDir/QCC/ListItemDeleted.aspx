<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListItemDeleted.aspx.cs" Inherits="FreeLayout.QCC.ListItemDeleted" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List Item to be delete</title>
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
        <div>
            <div class="card card-primary">
                <div class="card-header">
                    <h5 class="card-title">List Item to be removed check!</h5>
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
                                        <th>PalletID</th>
                                        <th>Plant</th>
                                        <th>DeliveryDate</th>
                                        <th>DANo</th>
                                        <th>PONo</th>
                                        <th>Vender</th>
                                        <th>Qty</th>
                                        <th>Invoice</th>
                                        <th>UserID</th>
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
                                        <td><%=rows[4].ToString() %></td>
                                        <td><%=rows[5].ToString() %></td>
                                        <td><%=rows[6].ToString() %></td>
                                        <td><%=rows[7].ToString() %></td>
                                        <td><%=rows[8].ToString() %></td>
                                        <td><%=rows[9].ToString() %></td>
                                        <td><%=rows[10].ToString() %></td>
                                        <td>
                                            <%--<a class="btn btn-info btn-sm" href="#" id="<%= rows[0].ToString() %>" onclick="openEditModal('<%= rows[0].ToString() %>','<%= rows[1].ToString() %>','<%= rows[2].ToString() %>','<%= rows[3].ToString() %>')">
                                            <i class="fas fa-pencil-alt"></i>
                                            Edit
                                        </a>--%>
                                        </td>
                                    </tr>
                                    <% } %>
                                </tbody>

                                <tfoot>
                                    <tr>
                                        <th>ID</th>
                                        <th>Material</th>
                                        <th>PalletID</th>
                                        <th>Plant</th>
                                        <th>DeliveryDate</th>
                                        <th>DANo</th>
                                        <th>PONo</th>
                                        <th>Vender</th>
                                        <th>Qty</th>
                                        <th>Invoice</th>
                                        <th>UserID</th>
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


        //function openEditModal(txt_id, txt_mahang, txt_cate, txt_path) {
        //    $("#idcheck").val(txt_id);
        //    $("#material").val(txt_mahang);
        //    $("#cate").val(txt_cate);
        //    var data = { material: txt_mahang };
        //    $.ajax({
        //            type: "POST",
        //            contentType: "application/json; charset=utf-8",
        //            url: "UploadWI.aspx/getfulpath",
        //            data: JSON.stringify(data),
        //            dataType: "json",
        //            success: function (data) {
        //                $("#path").val(data.d);
        //                },
        //                error: function () {

        //                }
        //        });

        //    //$("#path").val(txt_path);

        //    $('#myModal').modal('show');
        //};

    </script>

</body>
</html>
