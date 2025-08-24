<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentBorrow2.aspx.cs" Inherits="FreeLayout.EquipmentBorrow2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Borrow Equipment</title>    
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
        <a href="#" class="nav-link"><span style=" font-size: 22px;"> Home</span></a>
      </li>
      <li class="nav-item d-none d-sm-inline-block">
        <%--<a href="http://localhost:61772/InventoryInfra.aspx" target="_blank" class="nav-link">Report Inventory</a--%>
          <a href="/InventoryInfra.aspx" target="_blank" class="nav-link"><span style="font-size: 22px;">Report Inventory</span></a>
      </li>
    </ul>

  </nav>

       

               <div class="card">
            <div class="card-header">
               <%-- <h1>Borrow and return Management equipment</h1>--%>
                <%--class="card-title"--%>
                <div class="col-sm-12">
                    Date Time:
                                    <input type="text" id="datepicker" runat="server">

                    <input type="checkbox" id="check_partno_search" style="width: 20px; height: 20px;" name="check_partno_search">
                    Item:                                    
                                    <input type="text" id="partno_search" runat="server">

                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click" >
                        <i class="fa fa-fw fa-lg fa-search"></i>Filter</button>

                    <button type="button" class="btn" style="margin-left: 50px; background-color: #00FF00;border: 1px solid" runat="server" onserverclick="Filter_Pending_Click">
                        Borrowed
                    </button>
                    <button type="button" class="btn" style="margin-left: 50px; background-color: #FFFF33;border: 1px solid" runat="server" onserverclick="Filter_Order_Click">
                       Pending Order 
                    </button>
                    <button type="button" class="btn" style="margin-left: 20px; background-color: #FFFFFF; border: 1px solid" runat="server" onserverclick="Filter_Finish_Click" >
                        Can borrow
                    </button>

                    <%--<button class="btn btn-primary" type="button" runat="server" style="margin-left:20px;"  >
                        Inventory Equipment
                    </button>--%>
                  
                          
                    <button class="btn btn-primary" type="button" runat="server" style="margin-left:20px;" onserverclick="Download_Click"><i class="fa fa-download"></i>Export</button>


                    <a href="/EquipmentBorrowHistory.aspx" target="_blank">
                        <button class="btn btn-primary" type="button" runat="server" style="margin-left:20px;" ><i class="fab fa-expeditedssl"></i>  History</button>
                    </a>

                    
                    <span style="float:left; margin-right: 10px;">Type divice:</span>
                    <div style="width:200px; float:left; margin-right:20px;" class="form-group">
                        
                        <asp:DropDownList ID="dr_filter_cate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filter_cate_Change"
                            AppendDataBoundItems="true"
                            DataTextField="DESCRIPTION"
                            DataValueField="DESCRIPTION"
                            CssClass="custom-select custom-select-sm form-control form-control-sm"> 
                        </asp:DropDownList>
                    </div>
               

                </div>


               
                 <div class="col-md-6" style="float: left; padding-top:10px;">
                      <h3><b style="font-size: 30px;">Borrow and return Management equipment</b></h3>
                     </div>

               

               <%-- <div class="col-md-3" style="float: left">
                    <div class="form-group">
                        <label for="Group">Filter both Normal and Rosh</label>
                        <asp:DropDownList ID="dr_filter_both" runat="server"
                            AppendDataBoundItems="true"
                            DataTextField="NameFilter"
                            DataValueField="NameFilter"
                            CssClass="custom-select custom-select-sm form-control form-control-sm">
                        </asp:DropDownList>
                    </div>
                </div>--%>

                <%-- <div class="col-md-3" style="float: left; display: block; margin-top:30px;" id="">
                    <b style="float:left;">Filter : </b>                     
                    <asp:TextBox ID="filter_type" CssClass="form-control" runat="server" style="width: 200px; float:left; margin-left:5px;"></asp:TextBox>
                </div>--%>
               
               
            </div>
        </div>


        <div>
            <table id="example" class="table table-striped table-bordered" style="width:100%">
        <thead>
            <tr>
                 <tr role="row">
                                        <th>ID</th>
                                        <th>NameDevice</th>
                                        <th>SerialNo</th>
                                        <th>Position</th> 
                                        <th>FlagBorrow</th> 
                                         <th>FlagOrder</th> 
                                        <th>reason</th>
                                        <th>timeborrow</th>
                                        <th>timereturn</th>
                                        <th>CreateUser</th>
                                        <th>UserBorrow</th>
                                        <th>userreturn</th>
                                        <th>TimeOrder</th>
                                        <th>UserOrder</th>
                                        <th>Action</th>
                                    </tr>
            </tr>
        </thead>
        <tbody>
                                    <%int i = 0; %>
                                    <%foreach (System.Data.DataRow rows in dt.Rows)
                                        {%>
                                     <%i++;%>
                                    <%if (rows["FlagBorrow"].ToString() == "1" && rows["FlagOrder"].ToString() == "0")
                                        {%>
                                    <tr role="row" style="background-color: #00FF00">                                        
                                        <td><%=i %></td>
                                        <td><%=rows["NameDevice"].ToString() %></td>
                                        <td><%=rows["SerialNo"].ToString() %></td>
                                        <td><%=rows["Position"].ToString() %></td>
                                        <td><%=rows["FlagBorrow"].ToString() %></td>
                                         <td><%=rows["FlagOrder"].ToString() %></td> 
                                        <td><%=rows["reason"].ToString() %></td>
                                        <td><%=rows["timeborrow"].ToString() %></td>
                                        <td><%=rows["timereturn"].ToString() %></td>
                                        <td><%=rows["CreateUser"].ToString() %></td>
                                        <td>
                                            <%=rows["UserIDMuon"].ToString() %>
                                            <%if (rows["UserIDMuon"].ToString() != "") { %>
                                             <a href="#" title="history" onclick="openEditModal5('<%=rows["UserIDMuon"].ToString() %>')">
                                                    <i class="fas fa-search"></i>
                                             </a>
                                            <%} %>
                                            
                                        </td>
                                        <td>
                                            <%=rows["userreturn"].ToString() %>
                                            <%if (rows["userreturn"].ToString() != "") { %>
                                             <a href="#" title="history" onclick="openEditModal5('<%=rows["userreturn"].ToString() %>')">
                                                    <i class="fas fa-search"></i>
                                             </a>
                                            <%} %>
                                        </td>  
                                        <td><%=rows["Timeorder"].ToString() %></td> 
                                        <td><%=rows["Userorder"].ToString() %></td> 
                                        <td>                                             
                                            <%-- <a href="#" class="btn btn-info btn-sm" title="delete item" onclick="openEditModal2('<%= rows["NameDevice"].ToString() %>')"><i class="fas fa-pencil-alt"></i>Order</a>--%>
                                         </td>
                                    </tr>
                                    <%} %>
                                    <%else if(rows["FlagOrder"].ToString() == "1" && rows["FlagBorrow"].ToString() == "0")
                                        {%>
                                     <tr role="row"  style="background-color: #FFFF33">                                        
                                        <td><%=i %></td>
                                        <td><%=rows["NameDevice"].ToString() %></td>
                                        <td><%=rows["SerialNo"].ToString() %></td>
                                        <td><%=rows["Position"].ToString() %></td>
                                        <td><%=rows["FlagBorrow"].ToString() %></td> 
                                         <td><%=rows["FlagOrder"].ToString() %></td> 
                                        <td><%=rows["reason"].ToString() %></td>
                                        <td><%=rows["timeborrow"].ToString() %></td>
                                        <td><%=rows["timereturn"].ToString() %></td>
                                        <td><%=rows["CreateUser"].ToString() %></td>
                                         <td><%=rows["UserIDMuon"].ToString() %></td>
                                        <td><%=rows["userreturn"].ToString() %></td>  
                                        <td><%=rows["Timeorder"].ToString() %></td> 
                                        <td>
                                            <%=rows["Userorder"].ToString() %>
                                             <a href="#" title="history" onclick="openEditModal5('<%=rows["Userorder"].ToString() %>')">
                                                    <i class="fas fa-search"></i>
                                             </a>
                                        </td> 
                                         <td>                                             
                                              <%--<a href="#" class="btn btn-info btn-sm" title="delete item" onclick="openEditModal2('<%= rows["NameDevice"].ToString() %>')"><i class="fas fa-pencil-alt"></i>Order</a>--%>
                                         </td>
                                    <%} %>
                                    <%else
                                        { %>
                                     <tr role="row"  style="background-color: #FFFFFF">                                        
                                        <td><%=i %></td>
                                        <td><%=rows["NameDevice"].ToString() %></td>
                                        <td><%=rows["SerialNo"].ToString() %></td>
                                        <td><%=rows["Position"].ToString() %></td>
                                        <td><%=rows["FlagBorrow"].ToString() %></td> 
                                         <td><%=rows["FlagOrder"].ToString() %></td> 
                                        <td><%=rows["reason"].ToString() %></td>
                                        <td><%=rows["timeborrow"].ToString() %></td>
                                        <td><%=rows["timereturn"].ToString() %></td>
                                        <td><%=rows["CreateUser"].ToString() %></td>
                                         <td>
                                             <%=rows["UserIDMuon"].ToString() %>
                                             <%if (rows["UserIDMuon"].ToString() != "") { %>
                                             <a href="#" title="history" onclick="openEditModal5('<%=rows["UserIDMuon"].ToString() %>')">
                                                    <i class="fas fa-search"></i>
                                             </a>
                                            <%} %>
                                            <%-- <a href="#" title="history" onclick="openEditModal5('<%=rows["UserIDMuon"].ToString() %>')">
                                                    <i class="fas fa-search"></i>
                                             </a>--%>
                                         </td>
                                        <td>
                                            <%=rows["userreturn"].ToString() %>
                                             <%if (rows["userreturn"].ToString() != "") { %>
                                             <a href="#" title="history" onclick="openEditModal5('<%=rows["userreturn"].ToString() %>')">
                                                    <i class="fas fa-search"></i>
                                             </a>
                                            <%} %>
                                           <%-- <a href="#" title="history" onclick="openEditModal5('<%=rows["userreturn"].ToString() %>')">
                                                    <i class="fas fa-search"></i>
                                             </a>--%>
                                        </td>
                                        <td><%=rows["Timeorder"].ToString() %></td> 
                                        <td><%=rows["Userorder"].ToString() %></td> 
                                         <td>
                                             <%--<a href="#" class="btn btn-info btn-sm" title="order item" onclick="openEditModal2('<%= rows["NameDevice"].ToString() %>' %>')">Order</a>--%>
                                              <a href="#" class="btn btn-info btn-sm" title="delete item" onclick="openEditModal2('<%= rows["NameDevice"].ToString() %>')"><i class="fas fa-pencil-alt"></i>Order</a>
                                         </td>
                                    </tr>
                                        <%} %>

                                    <% } %>
                                </tbody>
        <tfoot>
            <tr>
                  <th>ID</th>
                                        <th>NameDevice</th>
                                        <th>SerialNo</th>
                                        <th>Position</th> 
                                        <th>FlagBorrow</th> 
                                         <th>FlagOrder</th> 
                                        <th>reason</th>
                                        <th>timeborrow</th>
                                        <th>timereturn</th>
                                        <th>CreateUser</th>
                                        <th>UserBorrow</th>
                                        <th>userreturn</th>
                                        <th>TimeOrder</th>
                                        <th>UserOrder</th>
                                        <th>Action</th>
            </tr>
        </tfoot>
    </table>
        </div>



             <div class="modal" id="myModal2">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag" style="float: left">Do you want order item?</h4>
                                <h6 class="modal-title" id="headerTag" style="float: left; color:red"><b><i>You need contact IT PIC to confirm!</i></b></h6>

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
                                        <label for="ID">Item</label>
                                        <span style="color: green; font-size: 11px; font-style: italic;">(Read only)</span>
                                        <asp:TextBox ID="txtdevice" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                       <label for="exampleInputEmail1">User Order</label>
                                        <span style="color: red; font-size: 11px; font-style: italic;">You must input!(*)</span>
                                        <asp:TextBox ID="txtuserid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div> 

                             <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">                                          
                                        <label id="lbl_machine" for="dr_machine">Reason</label>
                                        <asp:DropDownList ID="dr_lydo" runat="server"
                                            AppendDataBoundItems="true"
                                            DataTextField="Title"
                                            DataValueField="Title"
                                            CssClass="custom-select custom-select-sm form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group" >
                                       <label for="ID">Department</label>
                                        <asp:DropDownList ID="dr_phongban" runat="server"
                                            AppendDataBoundItems="true"
                                            DataTextField="Maphongban"
                                            DataValueField="Maphongban"
                                            CssClass="custom-select custom-select-sm form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                             <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="QtyNG">Date borrow</label>
                                        <%--<asp:TextBox ID="txtngaymuon" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>--%>
                                        <input type="text" id="txtngaymuon" runat="server">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Date Return</label>                                        
                                        <%--<asp:TextBox ID="txtngaytra" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>--%>
                                        <input type="text" id="txtngaytra" runat="server">
                                    </div>
                                </div>
                            </div>
                            
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Close</button>
                        <button type="button" runat="server" id="btnOrder" onserverclick="BtnOrderItem" class="btn btn-primary">
                            <i class="fas fa-download"></i>
                            Save
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="myModal5">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag" style="float: left">Information of User Borrower </h4>
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
                                        <label for="ID">UserID</label>                                        
                                        <asp:TextBox ID="txtnguoidung" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Full name</label>
                                        <asp:TextBox ID="txtfullname" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Department</label>
                                        <asp:TextBox ID="txtdepartment" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">DateofBirth</label>
                                        <asp:TextBox ID="txtngaysinh" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Phone number</label>
                                        <asp:TextBox ID="txtphone" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                
                            </div>

                           

                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Close</button>
                        <%--<button type="button" runat="server" id="Button3" onserverclick="test" class="btn btn-primary">
                            <i class="fas fa-download"></i>
                            Save
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
            $('#txtdevice').prop("readonly", true);
        });
  
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

        function openEditModal2(txt_namedeive) {           
            $("#txtdevice").val(txt_namedeive);

            $('#myModal2').modal('show');
        }

    function openEditModal5(txtuserid) {
            $("#txtnguoidung").val(txtuserid);
            var data = { userid: txtuserid };
            $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "EquimentBorrow.aspx/getusermuon",
                    data: JSON.stringify(data),
                    dataType: "json",
                    success: function (data) {
                        var str_ = data.d;
                        var catchuoi = str_.split(",");
                        if (catchuoi[0] == "OK") {
                            //auto display level checking
                            $('#txtfullname').val(catchuoi[1]);
                            $('#txtdepartment').val(catchuoi[2]);
                            $('#txtngaysinh').val(catchuoi[3]);
                            $('#txtphone').val(catchuoi[4]);
                            $('#myModal5').modal('show');
                        } 
                        else 
                        {
                            $('#txtfullname').val('chua duoc update');
                            $('#txtdepartment').val('');
                            $('#txtngaysinh').val('');
                            $('#txtphone').val('');
                            $('#myModal5').modal('show');
                        }
                    },
                    error: function () {
                    }
                });

            //$('#myModal5').modal('show');

        }

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
