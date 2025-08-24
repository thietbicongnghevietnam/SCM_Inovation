<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryInfra.aspx.cs" Inherits="FreeLayout.InventoryInfra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>Borrow Equipment</title>    
    <link rel="stylesheet" href="/plugins/fontawesome-free/css/all.min.css" />

    <link rel="stylesheet" href="/plugins/datatables-responsive/css/responsive.bootstrap4.min.css" />
     <link rel="stylesheet" href="/plugins/toastr/toastr.css" />
    <link rel="stylesheet" href="/dist/css/adminlte.min.css" />
        <link rel="stylesheet" href="/plugins/fontawesome-free/css/jquery-ui.css" />

  
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.4.2/css/buttons.bootstrap4.min.css" />
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
                <h1>Report Inventory Infra</h1>
                <%--class="card-title"--%>
                <div class="row">
                   <b style="font-family:Arial; font-size:16px; padding-top:10px; padding-left:5px;"> Date Time: </b>
                                &nbsp;&nbsp;&nbsp;&nbsp;    <input type="text" id="datepicker" runat="server">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <input type="checkbox" id="check_partno_search" style="width: 20px; height: 20px; margin-top:15px;" name="check_partno_search">
                    <b style="font-family:Arial; font-size:16px; padding-top:10px; padding-left:15px;">ManagementID:</b>     &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    <input type="text" id="partno_search" runat="server">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
               <b style="font-family:Arial; font-size:16px; padding-top:10px; padding-left:15px;">Type device:</b>
                    <div class="col-md-2" style="float: left; padding-top:10px;"> 
                        
                        <asp:DropDownList ID="dr_filter_cate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filter_cate_Change"
                            AppendDataBoundItems="true"
                            DataTextField="DeviceTypeID"
                            DataValueField="DeviceTypeID"
                            CssClass="custom-select custom-select-sm form-control form-control-sm"> 
                        </asp:DropDownList>
                    
                </div>

                    <b style="font-family:Arial; font-size:16px; padding-top:10px; padding-left:15px;">Type ProcessID:</b>
                    <div class="col-md-2" style="float: left; padding-top:10px;"> 
                        
                        <asp:DropDownList ID="dr_filter_process" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filter_process_Change"
                            AppendDataBoundItems="true"
                            DataTextField="ProcessID"
                            DataValueField="ProcessID"
                            CssClass="custom-select custom-select-sm form-control form-control-sm"> 
                        </asp:DropDownList>
                    
                </div>

                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click" >
                        <i class="fa fa-fw fa-lg fa-search"></i>Filter</button>

                  <button class="btn btn-primary" type="button" runat="server" style="margin-left:20px;" onserverclick="Download_Click" ><i class="fa fa-download" ></i>Export All</button>
                   
                    <button id="btnExport" class="btn btn-primary btn-lg" style="margin-right: 20px; float: right; margin-left:15px;">Export excel filter</button>

            </div>

                <div class="row" style="padding-top:10px;">
                   &nbsp;&nbsp;&nbsp;<b>Month:</b>&nbsp;&nbsp;&nbsp;
                    <asp:dropdownlist runat="server" id="drMonth" >
                         <asp:listitem text="01" value="1"></asp:listitem>
                         <asp:listitem text="02" value="2"></asp:listitem>
                         <asp:listitem text="03" value="3"></asp:listitem>
                         <asp:listitem text="04" value="4"></asp:listitem>
                         <asp:listitem text="05" value="5"></asp:listitem>
                        <asp:listitem text="06" value="6"></asp:listitem>
                        <asp:listitem text="07" value="7"></asp:listitem>
                        <asp:listitem text="08" value="8"></asp:listitem>
                        <asp:listitem text="09" value="9"></asp:listitem>
                        <asp:listitem text="10" value="10"></asp:listitem>
                        <asp:listitem text="11" value="11"></asp:listitem>
                        <asp:listitem text="12" value="12"></asp:listitem>
                    </asp:dropdownlist>                                                   
                    <b style="padding-left:10px; padding-right:10px;">Year:</b> <input type="text" id="txtYear" runat="server" style="float:left">
                    <b style="padding-left:10px; padding-right:10px;">Serial:</b> <input type="text" id="txtserial" runat="server" style="float:left">
                    <b style="padding-left:10px; padding-right:10px;">Floor:</b> <input type="text" id="txtFloor" runat="server" style="float:left">
                    <b style="padding-left:10px; padding-right:10px;">Area:</b> <input type="text" id="txtarea" runat="server" style="float:left">
                    <b style="padding-left:10px; padding-right:10px;">Table:</b> <input type="text" id="txttable" runat="server" style="float:left">
                    
                
        </div>

           
           <br />
            <div>
            <table id="example" class="table table-striped table-bordered" style="width:100%">
        <thead>
            <tr>
                 <tr role="row">
                                        <th>ID</th>
                                        <th>ProcessID</th>
                                        <th>DeviceTypeID</th>
                                        <th>ManagementID</th> 
                                        <th>Model</th> 
                                        <th>Serial</th> 
                                        <th>OS</th>
                                        <th>SectionCode</th>
                                        <th>SectionPIC</th>
                                        <th>SectionManagerID</th>
                                        <th>Floor</th>
                                        <th>Area</th>
                                        <th>Table</th>
                                        <th>ApprovedID</th>
                                        <th>CreatedDate</th>
                                        <th>CreatedBy</th>
                                    </tr>
            </tr>
        </thead>
        <tbody>
                                   <%int i = 0; %>
                                    <%foreach (System.Data.DataRow rows in dt.Rows)
                                        {%>
                                     <%i++;%>
                                    <tr role="row">                                        
                                        <td><%=i %></td>
                                        <td><%=rows["ProcessID"].ToString()%></td>
                                        <td><%=rows["DeviceTypeID"].ToString()%></td>
                                        <td><%=rows["ManagementID"].ToString()%></td>
                                        <td><%=rows["Model"].ToString()%></td>
                                        <td><%=rows["Serial"].ToString()%></td> 
                                        <td><%=rows["OS"].ToString()%></td>
                                        <td><%=rows["SectionCode"].ToString()%></td>
                                        <td><%=rows["SectionPIC"].ToString()%></td>
                                        <td><%=rows["SectionManagerID"].ToString()%></td>
                                        <td><%=rows["Floor"].ToString()%></td>
                                        <td><%=rows["Area"].ToString()%></td>
                                        <td><%=rows["Table"].ToString()%></td>
                                        <td><%=rows["ApprovedID"].ToString()%></td>
                                        <td><%=rows["CreatedDate"].ToString()%></td>
                                        <td><%=rows["CreatedBy"].ToString()%></td>
                                    </tr>

                                    <%} %>
                                   
                                </tbody>
        <tfoot>
            <tr>
                 <th>ID</th>
                                        <th>ProcessID</th>
                                        <th>DeviceTypeID</th>
                                        <th>ManagementID</th> 
                                        <th>Model</th> 
                                        <th>Serial</th> 
                                        <th>OS</th>
                                        <th>SectionCode</th>
                                        <th>SectionPIC</th>
                                        <th>SectionManagerID</th>
                                        <th>Floor</th>
                                        <th>Area</th>
                                        <th>Table</th>
                                        <th>ApprovedID</th>
                                        <th>CreatedDate</th>
                                        <th>CreatedBy</th>
            </tr>
        </tfoot>
    </table>
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
                $("#example").table2excel({
                    filename: "ReportInventory"
                });
            })
        });
    </script>

    <script type="text/javascript">  
    $(document).ready(function () {            
            //$('#txtdevice').prop("readonly", true);
          

  
  
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

            $('#datepicker').val(dateNewFormat);

            $("#datepicker").datepicker({ dateFormat: 'dd-mm-yy' });



        });


    </script>


</body>
</html>
