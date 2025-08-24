<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentBorrowHistory.aspx.cs" Inherits="FreeLayout.EquipmentBorrowHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>History Tranfer Borrow Equipment</title>
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
            <div class="card">
            <div class="card-header">
                <h1>History Borrow and return Management equipment</h1>
                <br />
                <%--class="card-title"--%>
                <div class="col-sm-12">
                    Date Time:
                                    <input type="text" id="datepicker" runat="server">

                    <input type="checkbox" id="check_partno_search" style="width: 20px; height: 20px;" name="check_partno_search">
                    Item:                                    
                                    <input type="text" id="partno_search" runat="server">

                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click" >
                        <i class="fa fa-fw fa-lg fa-search"></i>Filter</button>

                    

                    <%--<button class="btn btn-primary" type="button" runat="server" style="margin-left:20px;"  >
                        Inventory Equipment
                    </button>--%>
                  
                          
                    <button class="btn btn-primary" type="button" runat="server" style="margin-left:20px;" onserverclick="Download_Click" ><i class="fa fa-download"></i>Export</button>

                    <%--<button class="btn btn-success" type="button" runat="server" style="margin-left:50px;" ><i class="fa fa-download"></i><a href="ReportBorrowReturn.aspx" target="_blank" style="color:blue">Report Item</a></button>--%>
                    <button class="btn btn-success" type="button" runat="server" style="margin-left:50px;" onserverclick="Download_Click2" ><i class="fa fa-download">Report Item</i></button>

                    <input type="text" id="itemid" runat="server">
                   

                </div>


                <div class="col-md-3" style="float: left">
                    <div class="form-group">
                        <label for="Group">Choose Type</label>
                        <asp:DropDownList ID="dr_filter_cate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filter_cate_Change"
                            AppendDataBoundItems="true"
                            DataTextField="DESCRIPTION"
                            DataValueField="DESCRIPTION"
                            CssClass="custom-select custom-select-sm form-control form-control-sm"> 
                        </asp:DropDownList>
                    </div>
                </div>

               
               
            </div>
        </div>


             <div>
            <table id="example" class="table table-striped table-bordered" style="width:100%">
        <thead>
            <tr>
                 <tr role="row">
                                        <th>NO</th>
                                        <th>NameDevice</th>
                                        <th>TypeDevice</th>
                                        <th>Reason</th>
                                        <th>UserIDMuon</th> 
                                        <th>UserIDTra</th> 
                                         <th>Department</th> 
                                        <th>type_action</th>
                                        <th>CreateUser</th>
                                        <th>Createtime</th>
                                        <th>CreateUser_update</th>
                                        <th>Createtime_update</th>
                                        <th>User_order</th>
                                        <th>Time_order</th>                                        
                                        <th>Action</th>
                                    </tr>
            </tr>
        </thead>
        <tbody>
                                <%int i = 0; %>
                                    <%foreach (System.Data.DataRow rows in dt_history.Rows)
                                        {%>
                                     <%i++;%>
                                    <tr role="row">                                        
                                        <td><%=i %></td>
                                        <td><%=rows["NameDevice"].ToString() %></td>
                                        <td><%=rows["DESCRIPTION"].ToString() %></td>
                                        <td><%=rows["Reason"].ToString() %></td>
                                        <td><%=rows["UserIDMuon"].ToString() %></td>
                                        <td><%=rows["UserIDTra"].ToString() %></td>
                                         <td><%=rows["Department"].ToString() %></td> 
                                        <td><%=rows["type_action"].ToString() %></td>
                                        <td><%=rows["CreateUser"].ToString() %></td>
                                        <td><%=rows["Createtime"].ToString() %></td>
                                        <td><%=rows["CreateUser_update"].ToString() %></td>
                                        <td><%=rows["Createtime_update"].ToString() %></td>
                                        <td><%=rows["User_order"].ToString() %></td>  
                                        <td><%=rows["Time_order"].ToString() %></td> 
                                        <td></td> 
                                        
                                    </tr>
                                    <%} %>
                                     
                                </tbody>
        <tfoot>
            <tr>
                 <th>NO</th>
                <th>NameDevice</th>
                <th>TypeDevice</th>
                <th>Reason</th>
                <th>UserIDMuon</th> 
                <th>UserIDTra</th> 
                <th>Department</th> 
                <th>type_action</th>
                <th>CreateUser</th>
                <th>Createtime</th>
                <th>CreateUser_update</th>
                <th>Createtime_update</th>
                <th>User_order</th>
                <th>Time_order</th>                                        
                <th>Action</th>
            </tr>
        </tfoot>

    </table>
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

            $("#datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
 $("#txtngaymuon").datepicker({ dateFormat: 'dd-mm-yy' });
 $("#txtngaytra").datepicker({ dateFormat: 'dd-mm-yy' });
        });


    </script>

</body>
</html>
