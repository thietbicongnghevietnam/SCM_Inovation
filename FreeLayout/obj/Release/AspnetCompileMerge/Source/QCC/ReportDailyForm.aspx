<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportDailyForm.aspx.cs" Inherits="FreeLayout.QCC.ReportDailyForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Daily For QC</title>
    <link rel="stylesheet" href="/plugins/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" href="/plugins/datatables-responsive/css/responsive.bootstrap4.min.css" />
    <link rel="stylesheet" href="/dist/css/adminlte.min.css" />

    <link rel="stylesheet" href="/plugins/toastr/toastr.css" />
    <%--<link rel="stylesheet" href="/plugins/toastr/toastr.min.css" />--%>
    <script src="/plugins/jquery/jquery.min.js"></script>
    <script src="/plugins/toastr/toastr.js"></script>

    <link rel="stylesheet" href="/plugins/fontawesome-free/css/jquery-ui.css" />

    <script src="/Exportexcel/jquery.table2excel.min.js"></script>
</head>



<body>
    <form id="form1" runat="server">
        <div>
            <div class="card-header">
                <h1>Report Daily Inspection QC</h1>
                <br />
                <div class="col-sm-12">
                    Date From :
                    <input type="text" id="datepicker4" runat="server">
                    Date To :
                    <input type="text" id="datepicker5" runat="server">

                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click_4">
                        <i class="fa fa-fw fa-lg fa-search"></i>Filter</button>

                    <button id="btnExport" class="btn btn-primary btn-lg" style="margin-right: 20px; float: right;">Export table data to excel</button>
                </div>
            </div>



            <div class="card-body">
                <div id="example1_wrapper" class="dataTables_wrapper dt-bootstrap4">

                    <div class="row">
                        <div class="col-sm-12">

                            <table id="example1" class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Vendorcode</th>
                                        <th>No</th>
                                        <th>PartNo</th>
                                        <th>PartName</th>
                                        <th>Supplier</th>
                                        <th>Maker</th>
                                        <th>InvoiceNo</th>
                                        <th>Lotdate</th>
                                        <th>N</th>
                                        <th>N'</th>
                                        <th>R</th>
                                        <th>R'</th>
                                        <th>RejectDescription</th>
                                        <th>LotJudgment</th>
                                        <th>Inspector</th>
                                        <th>Remark</th>
                                        <th>Comment</th>
                                        <th>Levelofinspection</th>
                                        <th>Typeofcomponents</th>
                                        <th>Quantityofcomponentsroll</th>
                                        <th>Sloc</th>
                                        <th>Datereport</th>
                                        <th>r+r'</th>
                                        <%--<th>S.M</th>--%>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%int i = 0; %>
                                    <%foreach (System.Data.DataRow rows in dt.Rows)
                                        {%>
                                    <%i++;%>
                                    <tr role="row">
                                        <td><%=rows["Vender"].ToString() %></td>
                                        <td><%=i %></td>
                                        <td><%=rows["mahang"].ToString() %></td>
                                        <td><%=rows["PartName"].ToString() %></td>
                                        <td><%=rows["Supplier"].ToString() %></td>
                                        <td><%=rows["Maker"].ToString() %></td>

                                        <%if (rows["ckinvoice"].ToString() == "2")
                                            {%>
                                        <td><%=rows["DANo"].ToString() %></td>
                                        <% } %>
                                        <%else
                                            {%>
                                        <td><%=rows["Invoice"].ToString() %></td>
                                        <% } %>
                                        <%-- <td><%=rows["Invoice"].ToString() %></td>--%>

                                        <td><%=rows["Lotdate"].ToString() %></td>
                                        <td><%=rows["QtyLot"].ToString() %></td>
                                        <td><%=rows["QtySample"].ToString() %></td>
                                        <td><%=rows["R"].ToString() %></td>
                                        <td><%=rows["R2"].ToString() %></td>
                                        <td><%=rows["Reject_Description"].ToString() %></td>
                                        <td><%=rows["Lot_Judgment"].ToString() %></td>
                                        <td><%=rows["Inspector"].ToString() %></td>
                                        <td><%=rows["Remark"].ToString() %></td>
                                        <td><%=rows["comment"].ToString() %></td>
                                        <td><%=rows["Levelchecking"].ToString() %></td>
                                        <td><%=rows["typeofcomponent"].ToString() %></td>
                                        <td><%=rows["qtyofcomponent"].ToString() %></td>
                                        <td><%=rows["sloc"].ToString() %></td>
                                        <td><%=rows["DateReport"].ToString() %></td>
                                        <td><%=rows["R_R"].ToString() %></td>
                                        <%--<td><%=rows["SM"].ToString() %></td>--%>
                                    </tr>
                                    <%} %>
                                </tbody>

                                <%--<tfoot>
                                    <tr>
                                        <th>Vendorcode</th>
                                        <th>No</th>
                                        <th>PartNo</th>
                                        <th>PartName</th>
                                        <th>Supplier</th>
                                        <th>Maker</th>
                                        <th>InvoiceNo</th>
                                        <th>LotDate</th>
                                        <th>N</th>
                                        <th>N'</th>
                                        <th>R</th>
                                        <th>R'</th>
                                        <th>RejectDescription</th>
                                        <th>LotJudgment</th>
                                        <th>Inspector</th>
                                        <th>Remark</th>
                                        <th>Comment</th>
                                        <th>Levelofinspection</th>
                                        <th>Typeofcomponents</th>
                                        <th>Quantityofcomponentsroll</th>
                                        <th>Sloc</th>
                                        <th>Datereport</th>
                                        <th>r+r'</th>
                                        <th>S.M</th>
                                    </tr>
                                </tfoot>--%>
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
    <script src="/dist/js/adminlte.min.js"></script>
    <script src="/dist/js/demo.js"></script>

    <script src="/Exportexcel/jquery.table2excel.min.js"></script>

    <script>
        $(function () {
            $("#btnExport").click(function () {
                $("#example1").table2excel({
                    filename: "ReportDailyQC"
                });
            })
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            //$("#example1").DataTable({
            //    dom: 'Bfrtip',
            //    buttons: [
            //        'copyHtml5',
            //        'excelHtml5',
            //        'csvHtml5',
            //        'pdfHtml5',
            //        'print'
            //    ],
            //     "pageLength": 200                              
            //});
        });

        $(function () {
            $("#example1").DataTable({
                "responsive": true,
                "autoWidth": false,
                //"order": [[0, "desc"]],
                "order": [[1, "asc"]],
                "pageLength": 500
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
                //dateNewFormat += '0' + (today.getMonth() + 1);
                dateNewFormat += (today.getMonth() + 1);
            }

            dateNewFormat = dateNewFormat + '-' + today.getFullYear();
            //dateNewFormat = today.getFullYear() + '-';

            //$('#datepicker').val(dateNewFormat);

            $("#datepicker4").datepicker({ dateFormat: 'dd-mm-yy' });

            $("#datepicker5").datepicker({ dateFormat: 'dd-mm-yy' });
        });
    </script>

</body>
</html>
