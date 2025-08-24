<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Layout1.aspx.cs" Inherits="FreeLayout.Layout1" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>--%>

<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <meta http-equiv="x-ua-compatible" content="ie=edge">
  <title>AdminLTE 3 | Top Navigation</title>
  <!-- Font Awesome Icons -->
  <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="dist/css/adminlte.min.css">
  <!-- Google Font: Source Sans Pro -->
  <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet">
</head>
<body class="hold-transition layout-top-nav">
<div class="wrapper">
 
  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1>General Form</h1>
          </div>
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">Home</a></li>
              <li class="breadcrumb-item active">General Form</li>
            </ol>
          </div>
        </div>
      </div><!-- /.container-fluid -->
    </section>
    <!-- Main content -->
    <section class="content">
      <div class="container-fluid">
        <div class="row">
          <!-- left column -->
          <div class="col-md-12">
            <!-- general form elements -->
            <div class="card card-primary">
              <div class="card-header">
                <h3 class="card-title">Quick Example</h3>
              </div>
              <!-- /.card-header -->
              <!-- form start -->
               <%-- <div class="card-body">
                           <div class="row"> 
                               <div class="row row-cols-5">
                                <%
                                    for (int i = 0; i < 60; i++)
                                    {
                                        %>
                                    <div class="col">
                                        <div class="card bg-primary">
                                                
                                            <div class="card-body text-center">
                                                <h6 class="card-text">PL<%=i+1 %></h6>
                                                 
                                            </div>
                                        </div>
                                    </div>
                                
                                <%
                                    }
                            %>
                           </div>
                           </div>
              </div>--%>
              
            </div>                    
          </div>
         
          <div class="col-md-6">
            <!-- general form elements disabled -->
            <div class="card card-warning">
              <div class="card-header">
                <h3 class="card-title">LEFT</h3>
              </div>
              <div class="card-body">
                    <div class="row">      <%--offset-md-1--%>
                     <%--   <div class="col-12">--%>
                            <div class="row row-cols-5">
                                <%
                                    for (int i = 61; i < 81; i++)
                                    {
                                        %>
                                        <div class="col">
                                            <div class="card bg-primary">
                                                    <%--<div class="card-header"><p class="text-center"><b>PL<%=i %></b></p></div>--%>
                                                <div class="card-body text-center">
                                                    <h6 class="card-text">PL<%=i %></h6>
                                                      <%-- <p class="card-text">AAAAA</p>--%>
                                                </div>
                                            </div>
                                        </div>
                                <%
                                    }
                            %>
                            </div>
                        <%--</div>--%>
                    </div>
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
            <!-- general form elements disabled -->
          </div>
          <!--/.col (left) -->


            <!-- right column -->
          <div class="col-md-6">
            <!-- general form elements disabled -->
            <div class="card card-pink">
              <div class="card-header">
                <h3 class="card-title">RIGHT</h3>
              </div>
              <div class="card-body">
                    <div class="row">      <%--offset-md-1--%>
                     <%--   <div class="col-12">--%>
                            <div class="row row-cols-5">
                                <%for (int i = 61; i < 81; i++)
                                    {%>
                                <div class="col">
                                    <div class="card bg-primary">
                                            <%--<div class="card-header"><p class="text-center"><b>PL<%=i %></b></p></div>--%>
                                        <div class="card-body text-center">
                                            <h6 class="card-text">PL<%=i %></h6>
                                              <%-- <p class="card-text">AAAAA</p>--%>
                                        </div>
                                    </div>
                                </div>
                                <%}%>
                            </div>
                        <%--</div>--%>
                    </div>
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
            <!-- general form elements disabled -->
          </div>
          <!--/.col (right) -->

          

            <!-- left column -->
          <div class="col-md-6">
            <!-- general form elements disabled -->
            <div class="card card-green">
              <div class="card-header">
                <h3 class="card-title">LEFT 2</h3>
              </div>
              <div class="card-body">
                    <div class="row">      <%--offset-md-1--%>
                     <%--   <div class="col-12">--%>
                            <div class="row row-cols-5">
                                <%
                                    for (int i = 61; i < 81; i++)
                                    {
                                        %>
                                <div class="col">
                                    <div class="card bg-primary">
                                            <%--<div class="card-header"><p class="text-center"><b>PL<%=i %></b></p></div>--%>
                                        <div class="card-body text-center">
                                            <h6 class="card-text">PL<%=i %></h6>
                                              <%-- <p class="card-text">AAAAA</p>--%>
                                        </div>
                                    </div>
                                </div>
                                <%
                                    }
                            %>
                            </div>
                        <%--</div>--%>
                    </div>
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
            <!-- general form elements disabled -->
          </div>
          <!--/.col (left) -->

<!-- left column -->
          <div class="col-md-6">
            <!-- general form elements disabled -->
            <div class="card card-green">
              <div class="card-header">
                <h3 class="card-title">RIGHT 2</h3>
              </div>
              <div class="card-body">
                    <div class="row">      <%--offset-md-1--%>
                     <%--   <div class="col-12">--%>
                            <div class="row row-cols-5">
                                <%
                                    for (int i = 61; i < 81; i++)
                                    {
                                        %>
                                <div class="col">
                                    <div class="card bg-primary">
                                            <%--<div class="card-header"><p class="text-center"><b>PL<%=i %></b></p></div>--%>
                                        <div class="card-body text-center">
                                            <h6 class="card-text">PL<%=i %></h6>
                                              <%-- <p class="card-text">AAAAA</p>--%>
                                        </div>
                                    </div>
                                </div>
                                <%
                                    }
                            %>
                            </div>
                        <%--</div>--%>
                    </div>
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
            <!-- general form elements disabled -->
          </div>
          <!--/.col (right) -->


        </div>
        <!-- /.row -->
      </div><!-- /.container-fluid -->
    </section>
    <!-- /.content -->
  </div>
  <!-- /.content-wrapper -->
  <footer class="main-footer">
    <div class="float-right d-none d-sm-block">
      <b>Version</b> 3.0.5
    </div>
    <strong>Copyright &copy; 2014-2019 <a href="http://adminlte.io">AdminLTE.io</a>.</strong> All rights
    reserved.
  </footer>
  <!-- Control Sidebar -->
  <aside class="control-sidebar control-sidebar-dark">
    <!-- Control sidebar content goes here -->
  </aside>
  <!-- /.control-sidebar -->
</div>
<!-- ./wrapper -->
<!-- jQuery -->
<script src="plugins/jquery/jquery.min.js"></script>
<!-- Bootstrap 4 -->
<script src="plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
<!-- bs-custom-file-input -->
<script src="plugins/bs-custom-file-input/bs-custom-file-input.min.js"></script>
<!-- AdminLTE App -->
<script src="dist/js/adminlte.min.js"></script>
<!-- AdminLTE for demo purposes -->
<script src="dist/js/demo.js"></script>
<script type="text/javascript">
$(document).ready(function () {
  bsCustomFileInput.init();
});
</script>
</body>
</html>
