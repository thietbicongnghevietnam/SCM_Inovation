<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmHoliday.aspx.cs" Inherits="FreeLayout.frmHoliday" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mater Holiday</title>
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
        <div class="card">
     <div class="card-header">
         <div class="col-sm-12">
             <h3><b style="font-size: 30px;">Setting Mater Holiday Vessel</b></h3>
             <br />
             <p style="color: blue;">
                 <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
             </p>
         </div>
         <div class="col-sm-12">
             <div style="float: left; padding-right: 10px;">
                 Từ ngày:
                       <%--<input type="text" id="datepicker" runat="server">--%>
                 <input type="date" id="Date1" name="date" runat="server">
                 Đến ngày:                                    
                       <input type="date" id="ngaychiid" name="date" runat="server">
             </div>

             <div style="float: left; padding-right: 10px;">
                 <input type="text" id="filterMaterial" runat="server" placeholder="Nhập Name Sanction" style="height: 34px;" />
             </div>

             <div style="float: left;">
                 <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">
                     <i class="fa fa-fw fa-lg fa-search"></i>Lọc
                 </button>
             </div>

             <div style="float: left; padding-left: 10px;">
                 <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                     Add
                 </button>
             </div>

            <%-- <div style="clear: both;"></div>--%>

         

             <%--<button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;"><i class="fa fa-download"></i>&nbsp; Export</button>&nbsp;&nbsp;&nbsp;--%>             
      <%--onserverclick="btnExport_Click"--%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                       

         </div>

     </div>
 </div>


 <div>
     <table id="example" class="table table-striped table-bordered" style="width: 100%">
         <thead>
          
                 <tr role="row">
                     <th>ID</th>
                    <th>Cat</th>
                    <th>Date from</th>
                    <th>DateTo</th>
                    <th>Weekday</th>                 
                    <th>Action</th>
                 </tr>
           
         </thead>
         <tbody>
             <%int i = 0; %>
             <% foreach (System.Data.DataRow rows in dt_image.Rows)
                 {
                     // Gọi hàm để xác định màu nền
                     //string backgroundColor = "#EEEEEE";
                     //if (rows["Flag_export"].ToString() == "YES")
                     //{
                     //    backgroundColor = "#00FF00";    //mau AQ2
                     //}  
                     //else
                     //{
                     //    backgroundColor = "#EEEEEE";  //mau màu nền
                     //}

         %>
         <%i++;%>
             <tr role="row" >  <%--style="background-color: <%= backgroundColor %>"--%>
                 <td><%=i %></td>
                 <td><%=rows["Cat"].ToString()%></td>
                 <td><%=rows["Date"].ToString()%></td>
                 <td><%=rows["DateTo"].ToString()%></td>
                 <td><%=rows["Weekday"].ToString()%></td>                 
                 <td>
                     <a href="#" class="btn btn-info btn-sm" title="eidt item" onclick="openEditModal3('<%= rows["Id"].ToString() %>','<%=rows["Date"].ToString() %>','<%=rows["DateTo"].ToString() %>','<%=rows["Weekday"].ToString() %>')"><i class="fas fa-edit"></i>Edit</a>
                     <a href="#" style="background-color: #dc3545; color: white;" class="btn btn-info btn-sm" title="eidt item" onclick="openEditModal4('<%= rows["Id"].ToString() %>')"><i class="fas fa-trash"></i>Delete</a>

                 </td>
             </tr>
             <%} %>
         </tbody>
         <tfoot>
             <tr>
                  <th>ID</th>
                  <th>Cat</th>
                  <th>Date from</th>
                  <th>DateTo</th>
                  <th>Weekday</th>                 
                  <th>Action</th>
             </tr>
         </tfoot>
     </table>
 </div>


        <!-- Modal -->
<div class="modal" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Add holiday</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>

            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">Category</label>
                        <asp:TextBox ID="cateid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">Date from</label>
                        <asp:TextBox ID="datefromid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">

                    <div class="col-md-6">
                        <label for="ID">Daten to</label>
                        <asp:TextBox ID="datetoid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">Week</label>
                        <asp:TextBox ID="weekid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>

                <!-- Lặp lại thêm các dòng -->
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary" runat="server" onserverclick="themhanghoa">save</button>
            </div>
        </div>
    </div>
</div>

 <div class="modal" id="myModal4">
     <div class="modal-dialog">
         <div class="modal-content">
             <div class="modal-header">
                 <div class="row">
                     <div>
                         <h4 class="modal-title" id="headerTag11" style="float: left">Delete record?</h4>
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
                                <%-- <label for="exampleInputEmail1">Model</label>
                                 <asp:TextBox ID="txMaterialName_del" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>--%>
                             </div>
                         </div>
                     </div>
                     
                 </div>
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Close</button>
                 <button type="button" runat="server" id="Button2" onserverclick="Xoathongtin" class="btn btn-primary">
                     <i class="fas fa-download"></i>
                     Save
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
                         <h4 class="modal-title" id="headerTag1" style="float: left">Update infor Holiday</h4>
                         <%--<h6 class="modal-title" id="headerTag" style="float: left; color:red"><b><i>Chi tiết tồn kho!</i></b></h6>--%>

                         <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                             <span aria-hidden="true">&times;</span>
                         </button>
                     </div>

                 </div>
             </div>

             <div class="modal-body">
                 <div class="row">
                     <div class="col-md-6">
                         <label for="ID">ID</label>
                         <asp:TextBox ID="IDedit" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                     </div>
                     <div class="col-md-6">
                         <label for="ID">Date from</label>
                         <asp:TextBox ID="iddatefrom" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                     </div>

                 </div>
                 <div class="row">
                     <div class="col-md-6">
                         <label for="ID">Date To</label>
                         <asp:TextBox ID="iddateto" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                     </div>
                     <div class="col-md-6">
                         <label for="ID">Week</label>
                         <asp:TextBox ID="idweek" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                     </div>
                 </div>
                 <!-- Lặp lại thêm các dòng -->
             </div>

             <%-- Modal footer --%>
             <div class="modal-footer">
                 <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Close</button>
                 <button type="button" runat="server" id="Button1" onserverclick="Updatethongtin" class="btn btn-primary">
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

    function openEditModal3(id, Datefrom, Dateto,tuan) {
        $("#IDedit").val(id);
        $("#iddatefrom").val(Datefrom);
        $("#iddateto").val(Dateto);
        $("#idweek").val(tuan);

        $('#myModal3').modal('show');
    }

    function openEditModal4(id) {
        $("#txtid_del").val(id);
        //$("#txMaterialName_del").val(material);

        $('#myModal4').modal('show');
    }



</script>

<script src="/plugins/jquery/jquery-ui.js"></script>
<script type="text/javascript">    

</script>
</body>
</html>
