<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main_test.aspx.cs" Inherits="FreeLayout.Main_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report MCS</title>
    <link rel="stylesheet" href="/plugins/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" href="/plugins/datatables-responsive/css/responsive.bootstrap4.min.css" />
    <link rel="stylesheet" href="/dist/css/adminlte.min.css" />

    <link rel="stylesheet" href="/plugins/toastr/toastr.css" />
    <%--<link rel="stylesheet" href="/plugins/toastr/toastr.min.css" />--%>
    <script src="/plugins/jquery/jquery.min.js"></script>
    <script src="/plugins/toastr/toastr.js"></script>

    <link rel="stylesheet" href="/plugins/fontawesome-free/css/jquery-ui.css" />

    <style>
        /*html, body {
            height: 100%;
            background: white;  /*#333*/
        }

        */

        .site {
            width: auto;
            height: 100%;
        }

        .container {
            position: relative;
            z-index: 2;
            width: 100%;
            height: 100%;
            -webkit-transform-style: preserve-3d;
            -moz-transform-style: preserve-3d;
            -o-transform-style: preserve-3d;
            -ms-transform-style: preserve-3d;
            transform-style: preserve-3d;
        }

        .page-content {
            position: absolute;
            z-index: 2;
            width: 100%;
            height: 100%;
            background: white; /*#1abc9c;*/
        }

        #new-content {
            background: #f1c40f;
            z-index: 1;
        }


        .page-content p {
            position: absolute;
            width: 100%;
            text-align: center;
            font-family: Montserrat, sans-serif;
            font-size: 75px;
            text-transform: uppercase;
            color: #fff;
            font-weight: 400;
            margin: 0;
        }

        #new-content p {
            color: #010101;
        }

        .page-content p strong {
            font-weight: 700;
        }
    </style>

    <style>
        * { /* reset lại margin và padding cho các tag */
            margin: 0;
            padding: 0;
        }

        #loader {
            position: fixed;
            top: 0;
            left: 0;
            z-index: 99999;
            width: 100%;
            height: 100%;
            background-color: #fff;
            transition: all 0.3s ease-in-out;
            padding-top: 500px;
        }

        .circle {
            height: 300px;
            margin: 50px auto;
            position: relative;
            text-align: center;
            width: 300px;
            -webkit-animation: circle_dot 2.0s infinite linear;
            animation: circle_dot 2.0s infinite linear;
        }

        .circle1, .circle2 {
            height: 60%;
            display: inline-block;
            background-color: #ef5f38;
            border-radius: 100%;
            position: absolute;
            top: 0;
            width: 60%;
            -webkit-animation: circle_bounce 2.0s infinite ease-in-out;
            animation: circle_bounce 2.0s infinite ease-in-out;
        }

        .circle2 {
            bottom: 0;
            top: auto;
            -webkit-animation-delay: -1.0s;
            animation-delay: -1.0s;
        }

        @-webkit-keyframes circle_dot {
            100% {
                -webkit-transform: rotate(360deg)
            }
        }

        @keyframes circle_dot {
            100% {
                transform: rotate(360deg);
                -webkit-transform: rotate(360deg)
            }
        }

        @-webkit-keyframes circle_bounce {
            0%, 100% {
                -webkit-transform: scale(0.0)
            }

            50% {
                -webkit-transform: scale(1.0)
            }
        }

        @keyframes circle_bounce {
            0%, 100% {
                -webkit-transform: scale(0.0);
                transform: scale(0.0);
            }

            50% {
                -webkit-transform: scale(1.0);
                transform: scale(1.0);
            }
        }
    </style>


    <script>
        function pageRedirect() {
            window.location.replace("http://localhost:61772/Main.aspx");
        }
        //10.92.184.24:8079
        setTimeout("pageRedirect()", 10000);
    </script>



</head>
<body>
    <div id="loader">
        <div class="circle">
            <div class="circle1"></div>
            <div class="circle2"></div>
        </div>
    </div>



    <form id="form1" runat="server">
        <div>




            <div class="site">
                <div class="container1">
                    <div class="page-content">
                        <p><strong>3D</strong> Page Flip1</p>
                    </div>

                    <div class="page-content" id="new-content">
                        <%--<p><strong>3D</strong> Page Flip2</p>--%>
                        
                        <div class="row">
                            <div class="col-12">


                                <div class="card">
                                    <div class="card-header">
                                        <h1>Material new list to come PSNV</h1>
                                        <%--class="card-title"--%>
                                        <div class="col-sm-12">
                                            <b>Delivery Date :</b>
                                            <input type="text" id="datepicker" runat="server">
                                            <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_MCS_Click">
                                                <i class="fa fa-fw fa-lg fa-search"></i>Filter</button>

                                            <button type="button" class="btn" style="margin-left: 700px; background-color: #99FFCC">
                                                QC checked
                                            </button>
                                            <button type="button" class="btn" style="margin-left: 50px; background-color: #FFFFCC">
                                                Wating check
                                            </button>
                                            <button type="button" class="btn" style="margin-left: 50px; background-color: #FF9933">
                                                Storaged
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
                                                                <th>ID</th>
                                                                <th>Material</th>
                                                                <th>Location</th>
                                                                <th>Plan</th>
                                                                <th>Receiving Time MCS</th>
                                                                <th>DA</th>
                                                                <th>PO</th>
                                                                <th>Vender</th>
                                                                <th>Qty</th>
                                                                <th>Status</th>
                                                                <th>Remark</th>
                                                                <th>Infor</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <%foreach (System.Data.DataRow rows in dt.Rows)
                                                                {%>

                                                            <%if (rows[14].ToString() == "waiting")
                                                                {%>

                                                            <tr role="row" style="background-color: #FFFFCC">
                                                                <td><%=rows[0].ToString() %></td>
                                                                <td><%=rows[1].ToString() %></td>
                                                                <td><%=rows[2].ToString() %></td>
                                                                <td><%=rows[3].ToString() %></td>
                                                                <td><%=rows[4].ToString() %></td>
                                                                <td><%=rows[5].ToString() %></td>
                                                                <td><%=rows[6].ToString() %></td>
                                                                <td><%=rows[7].ToString() %></td>
                                                                <td><%=rows[8].ToString() %></td>
                                                                <td><%=rows[14].ToString() %></td>
                                                                <td><%=rows[16].ToString() %></td>
                                                                <td>
                                                                    <a href="#" onclick="openEditModal('<%= rows[0].ToString() %>',
                                                        '<%= rows[1].ToString() %>',
                                                        '<%= rows[2].ToString() %>',
                                                        '<%= rows[3].ToString() %>',
                                                        '<%= rows[4].ToString() %>',
                                                        '<%= rows[5].ToString() %>',
                                                        '<%= rows[6].ToString() %>',
                                                        '<%= rows[7].ToString() %>',
                                                        '<%= rows[12].ToString() %>',
                                                        '<%= rows[13].ToString() %>')"><i class="fas fa-pencil-alt"></i></a>
                                                                </td>
                                                            </tr>

                                                            <% } %>
                                                            <%else if ((rows[14].ToString() == "checked") || (rows[14].ToString() == "finished"))
                                                                {%>
                                                            <tr role="row" style="background-color: #99FFCC">
                                                                <td><%=rows[0].ToString() %></td>
                                                                <td><%=rows[1].ToString() %></td>
                                                                <td><%=rows[2].ToString() %></td>
                                                                <td><%=rows[3].ToString() %></td>
                                                                <td><%=rows[4].ToString() %></td>
                                                                <td><%=rows[5].ToString() %></td>
                                                                <td><%=rows[6].ToString() %></td>
                                                                <td><%=rows[7].ToString() %></td>
                                                                <td><%=rows[8].ToString() %></td>
                                                                <td><%=rows[14].ToString() %></td>
                                                                <td><%=rows[16].ToString() %></td>
                                                                <td>
                                                                    <a href="#" onclick="openEditModal('<%= rows[0].ToString() %>',
                                                        '<%= rows[1].ToString() %>',
                                                        '<%= rows[2].ToString() %>',
                                                        '<%= rows[3].ToString() %>',
                                                        '<%= rows[4].ToString() %>',
                                                        '<%= rows[5].ToString() %>',
                                                        '<%= rows[6].ToString() %>',
                                                        '<%= rows[7].ToString() %>',
                                                        '<%= rows[12].ToString() %>',
                                                        '<%= rows[13].ToString() %>')"><i class="fas fa-pencil-alt"></i></a>
                                                                </td>
                                                            </tr>
                                                            <%}%>
                                                            <%else
                                                                {%>

                                                            <tr role="row" style="background-color: #FF9933">
                                                                <td><%=rows[0].ToString() %></td>
                                                                <td><%=rows[1].ToString() %></td>
                                                                <td><%=rows[2].ToString() %></td>
                                                                <td><%=rows[3].ToString() %></td>
                                                                <td><%=rows[4].ToString() %></td>
                                                                <td><%=rows[5].ToString() %></td>
                                                                <td><%=rows[6].ToString() %></td>
                                                                <td><%=rows[7].ToString() %></td>
                                                                <td><%=rows[8].ToString() %></td>
                                                                <%--<td><%=rows[14].ToString() %></td>--%>
                                                                <td>Storage</td>
                                                                <td><%=rows[16].ToString() %></td>
                                                                <td>
                                                                    <a href="#" onclick="openEditModal('<%= rows[0].ToString() %>',
                                                        '<%= rows[1].ToString() %>',
                                                        '<%= rows[2].ToString() %>',
                                                        '<%= rows[3].ToString() %>',
                                                        '<%= rows[4].ToString() %>',
                                                        '<%= rows[5].ToString() %>',
                                                        '<%= rows[6].ToString() %>',
                                                        '<%= rows[7].ToString() %>',
                                                        '<%= rows[12].ToString() %>',
                                                        '<%= rows[13].ToString() %>')"><i class="fas fa-pencil-alt"></i></a>
                                                                </td>
                                                            </tr>


                                                            <% } %>

                                                            <%}%>
                                                        </tbody>

                                                        <tfoot>
                                                            <tr>
                                                                <th>ID</th>
                                                                <th>Material</th>
                                                                <th>Location</th>
                                                                <th>Plan</th>
                                                                <th>DeliveryDate</th>
                                                                <th>DA</th>
                                                                <th>PO</th>
                                                                <th>Vender</th>
                                                                <th>Qty</th>
                                                                <th>Status</th>
                                                                <th>Remark</th>
                                                                <th>Infor</th>
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

                        </div>
                       
                        <div class="modal" id="myModal">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <div class="row">
                                            <div>
                                                <h4 class="modal-title" id="headerTag" style="float: left">Infomation Mateial</h4>

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
                                                        <asp:TextBox ID="txtid" CssClass="form-control" placeholder="ID" runat="server"></asp:TextBox>
                                                        <%--onserverclick="btnAddClick"--%>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">Material</label>
                                                        <asp:TextBox ID="txtmaterial" CssClass="form-control" placeholder="Material" runat="server"></asp:TextBox>
                                                        <%-- end add Modal --%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">Location</label>
                                                        <asp:TextBox ID="txtlocation" CssClass="form-control" placeholder="Location" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">Plan</label>
                                                        <asp:TextBox ID="txtplan" CssClass="form-control" placeholder="Plan" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">DeliveryDate</label>
                                                        <asp:TextBox ID="txtDeliveryDate" CssClass="form-control" placeholder="DeliveryDate" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">DANo</label>
                                                        <asp:TextBox ID="txtDA" CssClass="form-control" placeholder="DANo" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">PONo</label>
                                                        <asp:TextBox ID="txtPO" CssClass="form-control" placeholder="PONo" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">Vender</label>
                                                        <asp:TextBox ID="txtVender" CssClass="form-control" placeholder="Vender" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">Sloc</label>
                                                        <asp:TextBox ID="txtsloc" CssClass="form-control" placeholder="Sloc" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">Location Storage</label>
                                                        <asp:TextBox ID="txtstorage" CssClass="form-control" placeholder="Location Storage" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                    <%--<input runat="server" type="text" class="form-control" name="username" placeholder="Enter user name">--%>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Close</button>
                                        <%--<button type="button" id="Button1" class="btn btn-primary"><i class="fas fa-download"></i>Save</button>--%>
                                        <%--<input runat="server" type="text" class="form-control" name="fullname" placeholder="Enter full name">--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>




        </div>

    </form>
    
    <%--<script src=""></script>  cdnjs.cloudflare.com/ajax/libs/gsap/2.1.3/TweenMax.min.js--%>
    <script src="dist/js/TweenMax.min.js"></script>

     <script src="/plugins/jquery/jquery.min.js"></script>
    <script src="/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
    <script src="/plugins/datatables-responsive/js/dataTables.responsive.min.js"></script>
    <script src="/plugins/datatables-responsive/js/responsive.bootstrap4.min.js"></script>
    <script src="/dist/js/adminlte.min.js"></script>
    <script src="/dist/js/demo.js"></script>


    <script src="/plugins/jquery/jquery-ui.js"></script>
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

        function openEditModal(_id, _mahang,_vitri,_plant,_deliverydate,_DA,_PO,_Vender,_sloc,_storage) {
            $("#txtid").val(_id);
            $("#txtmaterial").val(_mahang);
            $("#txtlocation").val(_vitri);
            $("#txtplan").val(_plant);
            $("#txtDeliveryDate").val(_deliverydate);
            $("#txtDA").val(_DA);
            $("#txtPO").val(_PO);
            $("#txtVender").val(_Vender);
            $("#txtsloc").val(_sloc);
            $("#txtstorage").val(_storage);

            $('#myModal').modal('show');
        }

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

    <script>
        var loader = function () {
            setTimeout(function () {
                $('#loader').css({ 'opacity': 0, 'visibility': 'hidden' });
            }, 1000);
        };
        $(function () {
            loader();
        });
    </script>

    <script>
        var siteW = $(window).width();
        var siteH = $(window).height();

        $("p").css({ lineHeight: siteH + 'px' }); // dirty dirty

        TweenMax.set(".site", { perspective: 10000 });
        TweenMax.set(".container1", { transformStyle: "preserve-3d", transformOrigin: '-0% 50%' });
        //TweenMax.set("#new-content", { rotationY: 90, z: -siteW / 1, x: siteW / 1 });
        TweenMax.set("#new-content", { rotationY: 90, z: -siteW / 2, x: siteW / 2 });

        var tlFlip = new TimelineMax({ yoyo: true, repeat: -1, delay: 1.5, repeatDelay: 30 });

        tlFlip
            .to('.site', .5, { scale: 0.8, ease: Power2.easeInOut }, "start")
            .to('.container1', .4, { rotationY: -90, z: -siteW, ease: Power2.easeInOut }, "start+=0.7")
            .to('.site', .5, { scale: 1, ease: Power2.easeInOut }, "start+=1.2");

    </script>
</body>
</html>
