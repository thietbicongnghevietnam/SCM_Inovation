<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginQCC.aspx.cs" Inherits="FreeLayout.QCC.LoginQCC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LOGIN QCC</title>

      <!-- Google Font: Source Sans Pro -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
  <!-- Font Awesome -->
    <link rel="stylesheet" href="/plugins/fontawesome-free/css/all.min.css" /> 
  <!-- icheck bootstrap -->
  <link rel="stylesheet" href="/plugins/icheck-bootstrap/icheck-bootstrap.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="/dist/css/adminlte.min.css" />

    <link rel="stylesheet" href="/plugins/toastr/toastr.css" />
    <script src="/plugins/jquery/jquery.min.js"></script>
    <script src="/plugins/toastr/toastr.js"></script>

</head>
    <body class="hold-transition login-page">


<%--        <form id="form1" runat="server">
        <div>
        </div>
    </form>--%>


<div class="login-box">
  
  <!-- /.login-logo -->
  <div class="card">
    <div class="card-body login-card-body">
      <p class="login-box-msg">Sign in to start your inspection</p>

      <form action="#" method="post" runat="server">
        <div class="input-group mb-3">
          <input name="username" id="inputUser" type="text" placeholder="Enter username" class="form-control"/>

          <div class="input-group-append">
            <div class="input-group-text">
              <span class="fas fa-user"></span>
            </div>
          </div>
        </div>
        <div class="input-group mb-3">
          <input type="password" class="form-control" placeholder="Password" name="password" id="inputPassword"/>            
          <div class="input-group-append">
            <div class="input-group-text">
              <span class="fas fa-lock"></span>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-8">
            <div class="icheck-primary">
              <input type="checkbox" id="rememberPasswordCheck">
              <label for="remember">
                Remember Me
              </label>
            </div>
          </div>
          <!-- /.col -->
          <div class="col-4">
            <button type="submit" class="btn btn-primary btn-block" OnServerclick="BtnLoginClick" runat="server">Login In</button>
          </div>
          <!-- /.col -->
        </div>
      </form>

    
    </div>
    <!-- /.login-card-body -->
  </div>
</div>
<!-- /.login-box -->

<!-- jQuery -->
<script src="/plugins/jquery/jquery.min.js"></script>
<!-- Bootstrap 4 -->
<script src="/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
<!-- AdminLTE App -->
<script src="/dist/js/adminlte.min.js"></script>

        <script type="text/javascript">
         $(document).ready(function () {             
             $("#inputUser").select();
         });
         $("#inputUser").on('keyup', function (e) {           
            if (e.key === 'Enter' || e.keyCode === 13) {
                $("#inputPassword").select();
            }
        });
         //$("#inputPassword").on('keyup', function (e) {             
         //    if (e.key === 'Enter' || e.keyCode === 13) {                
         //        $("#ContentPlaceHolder1_btn_login").click();
         //    }
         //});
     </script>



</body>


</html>
