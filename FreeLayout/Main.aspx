<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="FreeLayout.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <title>Free Location | PSNV</title>
    <!-- Font Awesome Icons -->
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/adminlte.min.css" />
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet" />
    <style>
        .left {
            float: left;
        }

        .right {
            float: right;
        }

        .sans-serif {
            font-family: Helvetica, san-serif;
        }

        .p-table {
            display: grid;
            grid-gap: 2px;
            grid-template-columns: 1fr;
            grid-template-rows: repeat(10, 1fr);

            @media(min-width: 772px) {
                grid-template-columns: repeat(2, 1fr);
            }

            @media(min-width: 992px) {
                grid-template-columns: repeat(18, 65px);
            }
        }

        @-webkit-keyframes my {
            0% {
                /*color: #F8CD0A;*/
                color: red;
            }

            50% {
                /*color: #fff;*/
                color: white;
            }

            100% {
                color: #F8CD0A;
            }
        }

        @-moz-keyframes my {
            0% {
                /*color: #F8CD0A;*/
                color: red;
            }

            50% {
                /*color: #fff;*/
                color: white;
            }

            100% {
                color: #F8CD0A;
            }
        }

        @-o-keyframes my {
            0% {
                /*color: #F8CD0A;*/
                color: red;
            }

            50% {
                /*color: #fff;*/
                color: white;
            }

            100% {
                color: #F8CD0A;
            }
        }

        @keyframes my {
            0% {
                /*color: #F8CD0A;*/
                color: red;
            }

            50% {
                /*color: #fff;*/
                color: white;
            }

            100% {
                color: #F8CD0A;
            }
        }

        .unit {
            border: 2px solid black;
            border-radius: 0px 4px 4px 4px;
            cursor: cell;
            display: flex;
            flex-direction: column;
            padding: 0px;
            text-align: center;
            position: relative;
            min-height: 68px;
            width: 50px;
            height: 50px;
            float: left;
            margin-right: 50px;
        }

            .unit:hover {
                background-color: yellowgreen;
                /*color: lightyellow;//#008080*/
                color: #008080;
                z-index: 1;
                animation: scale 0.5s ease-in-out forwards;
            }

        .symbol {
            max-height: 120px;
            .type-number .name

        {
            font-size: 15px;
            margin-top: 10px;
            margin-bottom: 17px;
        }

        .author-year {
            font-size: 9px;
        }

        }

        .type-number {
            margin-bottom: 5px
        }

        .attributes {
            line-height: 1.2;
            font-size: 25px;
        }

        .number {
            font-size: 9px;
        }

        .letter {
            font-size: 32px;
            line-height: 0.9;
            margin-top: 3px;
        }

        .name {
            font-size: 6px;
            margin-bottom: 8px;
            margin-top: 0px;
        }

        .author-year {
            bottom: 0px;
            font-size: 3.5px;
            padding-bottom: 5px;
            position: relative;
            text-align: left;
            .author

        {
            position: absolute;
            bottom: 0px;
            line-height: 1;
        }

        .year {
            position: absolute;
            bottom: -1px;
            right: 0px;
        }

        }


        @keyframes scale {
            0% {
                transform: scale(1,1);
            }

            10% {
                transform: scale(1.1,.9);
                box-shadow: rgba(0, 0, 0, 0.3) 1px 2px 3px 1px;
            }

            30% {
                transform: scale(1.9,2.1);
                box-shadow: rgba(0, 0, 0, 0.1) 1px 1px 3px 1px;
            }

            50% {
                transform: scale(2,2)
            }

            57% {
                transform: scale(2,2);
                box-shadow: rgba(0, 0, 0, 0.1) 1px 2px 4px 1px;
            }

            64% {
                transform: scale(2,2)
            }

            100% {
                transform: scale(2,2);
                box-shadow: rgba(0, 0, 0, 0.2) 1px 2px 7px 1px;
            }
        }

        @keyframes animateBorderTwo {
            to {
                outline-color: #FA2A00;
                box-shadow: 0 0 0 10px #F2D694;
            }
        }
    </style>


    <style type="text/css">
        #overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: #000;
            filter: alpha(opacity=70);
            -moz-opacity: 0.7;
            -khtml-opacity: 0.7;
            opacity: 0.7;
            z-index: 100;
            display: none;
        }

        .content a {
            text-decoration: none;
        }

        .popup {
            width: 100%;
            margin: 0 auto;
            display: none;
            position: fixed;
            z-index: 101;
        }

        .popup_vendor {
            width: 100%;
            margin: 0 auto;
            display: none;
            position: fixed;
            z-index: 101;
        }

        .content {
            min-width: 600px;
            width: 600px;
            min-height: 150px;
            margin: 100px auto;
            background: #f3f3f3;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 5px #000;
        }

            .content p {
                clear: both;
                color: #555555;
                text-align: justify;
            }

                .content p a {
                    color: #d91900;
                    font-weight: bold;
                }

            .content .x {
                float: right;
                height: 35px;
                left: 22px;
                position: relative;
                top: -25px;
                width: 34px;
            }

                .content .x:hover {
                    cursor: pointer;
                }

        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        th, td {
            padding: 15px;
            text-align: left;
        }

        #t01 {
            width: 100%;
            background-color: #f1f1c1;
        }

        #t02 {
            width: 100%;
            background-color: #f1f1c1;
        }

        #table-scroll {
            height: 350px;
            overflow: auto;
            margin-top: 20px;
        }

        #table-scroll2 {
            height: 500px;
            overflow: auto;
            margin-top: 20px;
        }

        /*.hinh_vuong5 {
            width: 70px;
            height: 70px;
            background: #00CC33;
            float:left;
        }*/
        .hinh_vuong5 {
            width: 70px;
            height: 70px;
            background: #00CC33;
            float: left;
            border: solid 3px;
        }

        .hinh_vuong6 {
            width: 70px;
            height: 70px;
            background: #FF9900;
            float: left;
            border: solid 3px;
        }

        .hinh_vuong4 {
            width: 70px;
            height: 70px;
            background: #33FFFF;
            border: solid 3px;
        }

        .hinh_vuong3 {
            width: 70px;
            height: 70px;
            background: #FFFF00;
            border: solid 3px;
        }

        .hinh_vuong2 {
            width: 70px;
            height: 70px;
            background: #FFFAFA;
            border: solid 3px;
        }

        .hinh_vuong1 {
            width: 70px;
            height: 70px;
            float: left;
            color: #FA2A00;
            outline: 10px dashed #F2D694;
            box-shadow: 0 0 0 10px #FA2A00;
            animation: 2s animateBorderTwo ease infinite;
        }

        .box {
            display: flex;
        }

            .box .inner1 {
                width: 800px;
                height: 100px;
                line-height: 100px;
                font-size: 4em;
                font-family: sans-serif;
                font-weight: bold;
                white-space: nowrap;
                overflow: hidden;
            }

                .box .inner1:first-child {
                    /*background-color: indianred;*/
                    background-color: #00BB00;
                    color: darkred;
                    transform-origin: right;
                    transform: perspective(100px) rotateY(-15deg);
                }

                .box .inner1:last-child {
                    /*background-color: lightcoral;*/
                    background-color: #0000FF;
                    color: antiquewhite;
                    transform-origin: left;
                    transform: perspective(100px) rotateY(15deg);
                }

                .box .inner1 span {
                    position: absolute;
                    animation: marquee 5s linear infinite;
                }

                .box .inner1:first-child span {
                    animation-delay: 2.5s;
                    left: -100%;
                }

        @keyframes marquee {
            from {
                left: 100%;
            }

            to {
                left: -100%;
            }
        }
    </style>

    <script src="/plugins/jquery/jquery.min.js"></script>
    <%--   <script src="plugins/jquery/jquery.datetimepicker.js"></script>--%>

    <script type='text/javascript'>      
        $(function () {
            var overlay = $('<div id="overlay"></div>');
            $('.close').click(function () {
                $('.popup').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });
            $('.x').click(function () {
                $('.popup').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });
            $('.click').click(function () {
                overlay.show();
                overlay.appendTo(document.body);
                $('.popup').show();
                return false;
            });

        });

        function ShowPopUp(vitri) {
            var overlay = $('<div id="overlay"></div>');
            overlay.show();
            overlay.appendTo(document.body);
            $.ajax({
                type: "POST",
                url: "Main.aspx/GetMaHang",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ 'vitri': vitri }),
                dataType: "json",
                success: function (data) {
                    $('#t01 tbody tr').remove();

                    $('#tenvitri').text(vitri);

                    var objdata = $.parseJSON(data.d);
                    for (var i = 0; i < objdata['Table'].length - 1; i++) {
                        //debugger;
                        if (objdata['Table'].length == 2 && objdata['Table'][i][12] == "Urgent") {
                            $("#t01 tbody ").append("<tr style='outline:10px dashed #F2D694;box-shadow:0 0 0 10px #FA2A00;animation: 2s animateBorderTwo ease infinite;'><td>" + objdata['Table'][i][0] + "</td><td>" + objdata['Table'][i][1] + "</td><td>" + objdata['Table'][i][2] + "</td><td>" + objdata['Table'][i][3] + "</td><td>" + objdata['Table'][i][4] + "</td><td>" + objdata['Table'][i][5] + "</td><td>" + objdata['Table'][i][8] + "</td><td>" + objdata['Table'][i][9] + "</td><td>" + objdata['Table'][i][10] + "</td><td>" + objdata['Table'][i][11] + "</td></tr>");
                        } else {
                            if (objdata['Table'][i][12] == "Urgent") {
                                $("#t01 tbody ").append("<tr style='outline:10px dashed #F2D694;box-shadow:0 0 0 10px #FA2A00;animation: 2s animateBorderTwo ease infinite;'><td>" + objdata['Table'][i][0] + "</td><td>" + objdata['Table'][i][1] + "</td><td>" + objdata['Table'][i][2] + "</td><td>" + objdata['Table'][i][3] + "</td><td>" + objdata['Table'][i][4] + "</td><td>" + objdata['Table'][i][5] + "</td><td>" + objdata['Table'][i][8] + "</td><td>" + objdata['Table'][i][9] + "</td><td>" + objdata['Table'][i][10] + "</td><td>" + objdata['Table'][i][11] + "</td></tr>");
                            }
                            else if (objdata['Table'][i][4] == "finished" || objdata['Table'][i][5] == "Not Inspection") {
                                $("#t01 tbody ").append("<tr style='background-color: #00CC33;'><td>" + objdata['Table'][i][0] + "</td><td>" + objdata['Table'][i][1] + "</td><td>" + objdata['Table'][i][2] + "</td><td>" + objdata['Table'][i][3] + "</td><td>" + objdata['Table'][i][4] + "</td><td>" + objdata['Table'][i][5] + "</td><td>" + objdata['Table'][i][8] + "</td><td>" + objdata['Table'][i][9] + "</td><td>" + objdata['Table'][i][10] + "</td><td>" + objdata['Table'][i][11] + "</td></tr>");
                            }
                            else if (objdata['Table'][i][4] == "checked") {
                                $("#t01 tbody ").append("<tr style='background-color: #33FFFF;'><td>" + objdata['Table'][i][0] + "</td><td>" + objdata['Table'][i][1] + "</td><td>" + objdata['Table'][i][2] + "</td><td>" + objdata['Table'][i][3] + "</td><td>" + objdata['Table'][i][4] + "</td><td>" + objdata['Table'][i][5] + "</td><td>" + objdata['Table'][i][8] + "</td><td>" + objdata['Table'][i][9] + "</td><td>" + objdata['Table'][i][10] + "</td><td>" + objdata['Table'][i][11] + "</td></tr>");
                            }
                            else if (objdata['Table'][i][5] == "nocheck" && objdata['Table'][i][4] != "finished" && objdata['Table'][i][4] != "checked") {
                                $("#t01 tbody ").append("<tr style='background-color: #FF9900;'><td>" + objdata['Table'][i][0] + "</td><td>" + objdata['Table'][i][1] + "</td><td>" + objdata['Table'][i][2] + "</td><td>" + objdata['Table'][i][3] + "</td><td>" + objdata['Table'][i][4] + "</td><td>" + objdata['Table'][i][5] + "</td><td>" + objdata['Table'][i][8] + "</td><td>" + objdata['Table'][i][9] + "</td><td>" + objdata['Table'][i][10] + "</td><td>" + objdata['Table'][i][11] + "</td></tr>");
                            }
                            else {
                                $("#t01 tbody ").append("<tr><td>" + objdata['Table'][i][0] + "</td><td>" + objdata['Table'][i][1] + "</td><td>" + objdata['Table'][i][2] + "</td><td>" + objdata['Table'][i][3] + "</td><td>" + objdata['Table'][i][4] + "</td><td>" + objdata['Table'][i][5] + "</td><td>" + objdata['Table'][i][8] + "</td><td>" + objdata['Table'][i][9] + "</td><td>" + objdata['Table'][i][10] + "</td><td>" + objdata['Table'][i][11] + "</td></tr>");
                            }
                        }

                        //$("#t01 tbody ").append("<tr><td>" + objdata['Table'][i][0] + "</td><td>" + objdata['Table'][i][1] + "</td><td>" + objdata['Table'][i][2] + "</td><td>" + objdata['Table'][i][3] + "</td><td>" + objdata['Table'][i][4] + "</td><td>" + objdata['Table'][i][5] + "</td></tr>");

                    }
                    $('.popup').show();
                },
                complete: function () {
                },
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });
            return false;
        }

        $(function () {
            $("#datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
        });

    </script>
    <!--//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css-->
    <%-- <link rel="stylesheet" href="#">--%>
    <link rel="stylesheet" href="plugins/fontawesome-free/css/jquery-ui.css" />




    <style>
        .site {
            height: 100%;
        }

        .container1 {
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
            background: #1abc9c;
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


    <%-- <script>
        function pageRedirect() {
            window.location.replace("http:localhost:61772/Main_test.aspx");          
        }
        setTimeout("pageRedirect()", 60000);
        //setTimeout("pageRedirect()", 120000);
    </script>--%>


    <style>
        #status {
            width: 500px;
            height: 500px;
            position: fixed;
            left: 40%;
            /*left: 50%;*/
            z-index: 100;
            top: 40%;
            /*top: 50%;*/
            background-image: url(../Models/dot_loading2.gif);
            /*background-image: url(../Models/dots_loding.gif);*/
            /*background-image: url(../Models/Preloader_8.gif);*/
            background-repeat: no-repeat;
            background-position: center;
            margin: -100px 0 0 -100px;
            z-index: 1001
        }

        #loader {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            width: 100%;
            height: 100%;
            background-color: #FFF;
            z-index: 1000
        }
    </style>

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


        .modal {
            position: absolute;
            left: 50%;
            top: 50%;
            margin: -100px 0 0 -150px;
            display: none;
        }

        .txt {
            margin-left: 10%;
        }

        /*.loading {
            font-family: Arial;
            font-size: 10pt;
            /*border: 5px solid #EEEEEE;*/
        width: 300px;
        height: 150px;
        display: none;
        position: fixed;
        /*background-color: White;*/
        z-index: 999;
        }
        */
    </style>

    <%--<script src=""></script>--code.jquery.com/jquery-latest.js--%>
    <script src="dist/js/jquery-latest.js"></script>

    <script>
        //var loader = function () {
        //    setTimeout(function () {
        //        $('#loader').css({ 'opacity': 0, 'visibility': 'hidden' });
        //    }, 3000);
        //};
        //$(function () {
        //    loader();
        //});

        //<![CDATA[
        //$(window).bind("load", function () {
        //    setTimeout(function () {
        //        //$('#loader').css({ 'opacity': 0, 'visibility': 'hidden' });
        //        jQuery("#status").fadeOut();
        //        jQuery("#loader").fadeOut();
        //    }, 1000);

        //});
    //]]>
    </script>



</head>

<body class="sidebar-mini sidebar-collapse">

    <%--<div id='status'></div>
    <div id='loader'></div>--%>

    <%--<div class="loading" align="center">
        <img src="..\Models\Preloader_3.gif" />       
            Loading. Please wait.<br />
    </div>--%>

    <div class="wrapper">
        <nav class="navbar navbar-expand-sm navbar-dark bg-primary justify-content-center align-items-start">
            <div class="navbar-collapse collapse w-100 order-1 order-md-0 dual-collapse2">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item">
                        <a class="nav-link" data-widget="pushmenu" href="#" role="button"><i class="fas fa-bars"></i></a>
                    </li>

                    <li class="nav-item active">
                        <a href="Main.aspx" class="nav-link">Home </a>
                    </li>
                    <li class="nav-item">
                        <a href="../QCC/Normalinspection.aspx" target="_blank" class="nav-link">Normal inspection </a>
                    </li>
                    <li class="nav-item">
                        <a href="../QCC/RoshInspection.aspx" target="_blank" class="nav-link">RoHS inspection </a>
                    </li>
                    <li class="nav-item dropdown">
                        <a id="dropdownSubMenu1" href="#" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="nav-link dropdown-toggle">Upload Mater</a>
                        <ul aria-labelledby="dropdownSubMenu1" class="dropdown-menu border-0 shadow" style="left: 0px; right: inherit; background-color: #97FFFF">
                            <li><a href="../QCC/UploadReability.aspx" target="_blank" class="dropdown-item">Upload mater Reliability & WI</a></li>
                            <li><a href="../QCC/UploadPhthalate.aspx" target="_blank" class="dropdown-item">Upload mater Phthalate</a></li>
                            <li><a href="#" class="dropdown-item">Upload User</a></li>
                            <li class="dropdown-divider"></li>

                        </ul>
                    </li>
                    <li class="nav-item dropdown">
                        <a id="dropdownSubMenu1" href="#" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="nav-link dropdown-toggle">Report</a>
                        <ul aria-labelledby="dropdownSubMenu1" class="dropdown-menu border-0 shadow" style="left: 0px; right: inherit; background-color: #97FFFF">
                            <li><a href="../QCC/ReportMCS.aspx" target="_blank" class="dropdown-item">Report MCS </a></li>
                            <li><a href="../QCC/ReportDailyForm.aspx" target="_blank" class="dropdown-item">Report Inspection Normal Daily for QC</a></li>
                            <li><a href="../QCC/ReportNomarlForm.aspx" target="_blank" class="dropdown-item">Report Tranfer Form Normal</a></li>
                            <li><a href="../QCC/ReportRoshForm.aspx" target="_blank" class="dropdown-item">Report Tranfer Form Rosh</a></li>
                            <li class="dropdown-divider"></li>
                            <li><a href="../QCC/ListItemDeleted.aspx" target="_blank" class="dropdown-item">Report List item deleted</a></li>
                            <li><a href="../QCC/ReportQCC.aspx" target="_blank" class="dropdown-item">Send Mail Report QCC</a></li>
                            <li class="dropdown-divider"></li>
                        </ul>
                    </li>
                </ul>
            </div>

            <div class="navbar-collapse collapse w-100 order-3 dual-collapse2">
                <ul class="navbar-nav ml-auto">

                    <!-- Notifications Dropdown Menu -->
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false" style="color: greenyellow">
                            <i class="fa fa-user"></i>User <span class="caret"></span></a>
                        <ul class="dropdown-menu" role="menu" style="background-color: #97FFFF">
                            <li>
                                <a href="#"><i class="far fa-id-badge"></i>Profile </a>
                            </li>
                            <li>
                                <%--<a href="/QCC/LoginQCC.aspx" id="btn_logout" onserverclick="bttLogout_Click">--%>
                                <a href="#" id="btn_logout"><i class="fas fa-sign-out-alt"></i>Logout</a>
                            </li>
                        </ul>
                    </li>

                </ul>
            </div>
        </nav>


        <form id="form1" runat="server">


            <div class="site">
                <div class="container1">
                    <div class="page-content">
                        <%-- <p><strong>3D</strong> Page Flip1</p>--%>
                    </div>

                    <div class="page-content" id="new-content">
                        <%--<p><strong>3D</strong> Page Flip2</p>--%>

                        <div class="container-fluid" style="width: 100%; margin-top: 20px; margin-left: 60px;">

                            <div style="width: 15%; float: left; margin-right: 5px; margin-left: 5px;">
                                <span style="float: left; margin-top: 6px; font-size: 16px;"><b>Material Input:</b></span>
                                <span style="float: left; margin-left: 5px; margin-right: 3px;">
                                    <asp:TextBox ID="mahang" type="text" Width="150" name="mahang" runat="server" CssClass="form-control"></asp:TextBox>
                                </span>
                                <%--<button type="submit" class="btn btn-primary" id="search_qc">Find</button>--%>
                            </div>

                            <div style="width: 20%; float: left; margin-right: 5px; margin-top: 6px;">
                                <span style="font-size: 16px;"><b>Material Code : </b><span id="inner"></span></span>
                                <%--<br />
                    <span><b>Location : </b><span id="showvitri"></span></span>--%>
                            </div>
                            <div style="width: 15%; float: left; margin-right: 5px;">
                                <span style="float: left; margin-top: 6px; font-size: 16px;"><b>Vender:</b></span>
                                <span style="float: left; margin-left: 5px; margin-right: 3px;">
                                    <asp:TextBox ID="vendorname" type="text" Width="200" name="vendorname" runat="server" CssClass="form-control"></asp:TextBox>
                                </span>
                            </div>

                            <div style="width: 40%; float: left;">
                                <div class="box">
                                    <div class="inner1">
                                        <span>FREE LOCATION MCS AT RECEIVING AREA</span>
                                    </div>
                                    <div class="inner1">
                                        <span>QC INSPECTION AUTOMATION</span>
                                    </div>
                                </div>

                                <%--<button type="button" class="btn" style="margin-left: 50px; background-color: #FFFAFA">                        
                    Empty
                    </button>
                    <button type="button" class="btn" style="margin-left: 20px; background-color: #FFFF00">                        
                    Inspection
                    </button>
                    <button type="button" class="btn" style="margin-left: 20px; background-color: #33FFFF">
                        IQC checking
                    </button>
                    <button type="button" class="btn" style="margin-left: 20px; background-color: #00CC33">                        
                    Check ok/Non-inspection
                    </button>--%>

                                <%-- <p>
                    Date:
                    <input type="text" id="datepicker" runat="server">
                    <button class="btn btn-primary" type="button" runat="server" onserverclick="bttSearch_Click">
                        <i class="fa fa-fw fa-lg fa-search"></i>Search</button>
                </p>--%>
                            </div>



                            <br />
                            <br />
                            <br />



                            <div class="modal" id="myModal2">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <div class="row">
                                                <div>
                                                    <h4 class="modal-title" id="headerTag" style="float: left">List Pallet of Vender: <b id="vederselect"></b></h4>
                                                </div>
                                            </div>
                                        </div>

                                        <%-- Modal footer --%>
                                        <div class="modal-body">
                                            <div id="table-scroll2">
                                                <table id="t02">
                                                    <thead>
                                                        <tr>
                                                            <th>STT</th>
                                                            <th>PalletID</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>

                                            </div>
                                        </div>
                                        <%--<input runat="server" type="text" class="form-control" name="username" placeholder="Enter user name">--%>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fas fa-times"></i>Close</button>
                                            <%--<button type="button" runat="server" id="Button1" onserverclick="BtnFinish" class="btn btn-primary"><i class="fas fa-download"></i>Save</button>--%>
                                            <%--<input runat="server" type="text" class="form-control" name="fullname" placeholder="Enter full name">--%>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div>
                                <div class='popup'>
                                    <div class='content' style="width: 1000px;">
                                        <p>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="margin-right: 30px; margin-top: 30px;">X</button>
                                            <h2>List of Material at this location : <b><span id="tenvitri"></span></b></h2>

                                            <br />
                                            <div id="table-scroll">
                                                <table id="t01">
                                                    <thead>
                                                        <tr>
                                                            <th>ID</th>
                                                            <th>Material</th>
                                                            <th>QTY</th>
                                                            <th>Location</th>
                                                            <th>Status</th>
                                                            <th>Comment</th>
                                                            <th>Vender</th>
                                                            <th>Sloc</th>
                                                            <th>Vender Name</th>
                                                            <th>Received Time</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>

                                            </div>

                                            <br />
                                            <br />
                                            <a href='' class='close'>CLOSE</a>
                                        </p>
                                    </div>
                                </div>

                                <div class='popup_vendor'>
                                    <div class='content' style="width: 750px;">
                                        <p>
                                            <h2>List of PalletID : <b><span id="tenvendor"></span></b></h2>
                                            <br />


                                            <br />
                                            <br />
                                            <a href='' class='close'>CLOSE</a>
                                        </p>
                                    </div>
                                </div>

                                <%System.Data.DataView view3 = new System.Data.DataView(dt3);
                                    System.Data.DataTable distinctLoc3;
                                    distinctLoc3 = view3.ToTable(true, "vitri");

                                    System.Data.DataView view2 = new System.Data.DataView(dt2);
                                    System.Data.DataTable distinctLoc2;
                                    distinctLoc2 = view2.ToTable(true, "vitri");

                                    System.Data.DataView view = new System.Data.DataView(dt1);
                                    System.Data.DataTable distinctLoc;
                                    distinctLoc = view.ToTable(true, "vitri");

                                    System.Data.DataView view4 = new System.Data.DataView(dt4);
                                    System.Data.DataTable distinctLoc4;
                                    distinctLoc4 = view4.ToTable(true, "vitri");

                                    System.Data.DataView view5 = new System.Data.DataView(dt5);
                                    System.Data.DataTable distinctLoc5;
                                    distinctLoc5 = view5.ToTable(true, "vitri");

                                    int i = 1;
                                    string mahang = "";
                                    string ttcheck = "";
                                    string trakho_mcs = "";

                                    string vien_urgent = "";
                                %>
                                <%--<div class="row" style="width:100%">--%>

                                <div class="row" style="width: 100%" id="IDwebsocket">

                                    <div style="height: 650px;">
                                        <div style="width: 12%; float: right;">
                                            <%--<div>                                                
                                                <img src="..\Models\cong2.JPG" class="img-fluid img-thumbnail" style="width: 150px; height: 400px; margin-top: 0px; border: none;" alt="Cong vao">
                                            </div>
                                            <div>                                               
                                                <img src="..\Models\cong1new2.JPG" class="img-fluid img-thumbnail" style="width: 150px; height: 250px; margin-top: 0px; border: none;" alt="Cong vao">
                                            </div>--%>
                                            <div>
                                                <img src="..\Models\cong3a.JPG" class="img-fluid img-thumbnail" style="width: 150px; height: 650px; margin-top: 0px; border: none;" alt="Cong vao">
                                            </div>

                                        </div>
                                        <div style="width: 30%; float: right">
                                            <div class="col-md-12">
                                                <div class="card card-warning">
                                                    <div class="card-header">
                                                        <h3 class="card-title"><b style="margin-left: 100px;">Receiving area E-map 4</b></h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="row row-cols-5">
                                                                <%foreach (System.Data.DataRow rows in distinctLoc5.Rows)
                                                                    {%>
                                                                <%foreach (System.Data.DataRow row in dt5.Rows)
                                                                    {%>
                                                                <%if (row[1].ToString() == rows[0].ToString())
                                                                    {%>
                                                                <% mahang = mahang + row[0].ToString();  %>
                                                                <% ttcheck = ttcheck + row[4].ToString();  %>
                                                                <% trakho_mcs = row[5].ToString();  %>
                                                                <%}%>
                                                                <%}%>

                                                                <div class="col">
                                                                    <div id='<%=rows[0].ToString() +"_"+ mahang %>' class="unit card_<%=i %>" style="border: 1px solid black; width: 70px; height: 70px;" onclick="return ShowPopUp('<%=rows[0].ToString() %>')">
                                                                        <div>
                                                                            <%--http  concurrently.dev/2018/09/16/css-tao-chu-trong-suot-don-gian.html--%>
                                                                            <%-- tao chu trong suot--%>
                                                                            <%--<h6 style="font-size:6px; color: white;mix-blend-mode: multiply;"><%=ttcheck %></h6>--%>
                                                                            <h7><%=rows[0].ToString()%></h7>
                                                                            <%--<h6 style="font-size:6px; color: white;mix-blend-mode: multiply;" class="test_<%=i %>"><%=ttcheck %></h6>--%>
                                                                            <h6 style="font-size: 6px;" class="test_<%=i %>"><%=ttcheck %></h6>
                                                                            <span style="color: white"><%=trakho_mcs.Trim() %></span>


                                                                            <%foreach (System.Data.DataRow row_ur in dt_test2.Rows)
                                                                                {%>
                                                                            <%if (row_ur[0].ToString() == rows[0].ToString())
                                                                                {%>

                                                                            <h1 class="urg_<%=i %> animateBorderTwo" style="font-size: 6px;">Urgent</h1>

                                                                            <% }%>
                                                                            <%} %>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <%i++; %>
                                                                <% mahang = "";  %>
                                                                <% ttcheck = ""; %>
                                                                <%}%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>

                                        </div>
                                        <div style="width: 4%; float: right">
                                            <%--<img src="..\Models\handliftkeep.JPG" class="img-fluid img-thumbnail" style="width: 100px; height: 580px; margin-top: 0px;" alt="">--%>
                                            <img src="..\Models\xekeo2.JPG" class="img-fluid img-thumbnail" style="width: 100px; height: 580px; margin-top: 0px;" alt="">
                                        </div>
                                        <div style="width: 26%; float: right">
                                            <%--<img src="..\Models\Forkliftcharging.JPG" class="img-fluid img-thumbnail" style="width: 900px; height: 580px; margin-top: 0px; float: right; border: none;" alt="">--%>
                                            <img src="..\Models\xenang4.JPG" class="img-fluid img-thumbnail" style="width: 900px; height: 580px; margin-top: 0px; float: right; border: none;" alt="">
                                        </div>

                                        <div div style="width: 3%; float: right">
                                            <img src="..\Models\theway1.JPG" class="img-fluid img-thumbnail" style="margin-top: 0px; height: 790px; border: none" alt="the way1">
                                        </div>

                                        <%-- <span><b>Status indication: </b></span>--%>

                                        <div style="width: 25%; float: right">
                                            <div style="height: 20px; margin-bottom: 30px;">
                                                <b style="font-family: Tahoma; font-size: 24px; color: blue; margin-left: 15px;">Status indication :</b>
                                            </div>

                                            <div style="height: auto; margin-left: 20px; width: 400px; border: solid 1px white">
                                                <div>
                                                    <div style="float: left; width: 70px;" class="hinh_vuong1 animateBorderTwo"></div>
                                                    <div style="float: left; width: 100px; margin-left: 15px;"><b style="font-family: Arial; font-size: 22px; float: left">Urgent</b></div>
                                                    <div style="float: left; width: 70px; margin-left: 15px;" class="hinh_vuong3"></div>
                                                    <div style="float: left; width: 100px; margin-left: 15px;"><b style="font-family: Arial; font-size: 22px;">Wating Inspection</b></div>
                                                </div>
                                                <div style="margin-top: 100px;">
                                                    <div style="float: left; width: 70px;" class="hinh_vuong2"></div>
                                                    <div style="float: left; width: 100px; margin-left: 15px;"><b style="font-family: Arial; font-size: 22px;">Empty</b></div>
                                                    <div style="float: left; width: 70px; margin-left: 15px;" class="hinh_vuong4"></div>
                                                    <div style="float: left; width: 100px; margin-left: 15px;"><b style="font-family: Arial; font-size: 22px;">Checking</b></div>
                                                </div>
                                            </div>

                                            <div style="margin-top: 100px; margin-left: 20px;">
                                                <div style="float: left; width: 70px;" class="hinh_vuong6"></div>
                                                <div style="float: left; width: 100px; margin-left: 15px;"><b style="font-family: Arial; font-size: 22px;">Non-inspection</b></div>

                                                <div style="float: left; width: 70px; margin-left: 15px;" class="hinh_vuong5"></div>
                                                <div style="float: left; width: 100px; margin-left: 15px;"><b style="font-family: Arial; font-size: 22px;">Ok Checking</b></div>
                                                <%--<div style="float: left; width: 400px; margin-left: 15px;"><b style="font-family: Arial; font-size: 26px;">Ok Checking /Non-inspection</b></div>--%>
                                            </div>


                                            <div style="height: 10px;"></div>
                                            <div style="width: 500px; height: 230px; margin-left: 20px; margin-top: 20px;">
                                                <div style="height: 210px; margin-left: 0px; width: 500px; float: left; margin-bottom: 5px; margin-top: 20px;">
                                                    <%--<b style="font-family: Tahoma; font-size: 24px; color: blue;">Material urgent: Part Number-Location</b>--%>
                                                    <b style="font-family: Tahoma; font-size: 24px; color: blue;">PSNV - Material List New Come</b>
                                                    <asp:Panel ID="Panel1" runat="server" BackColor="#FFFFCC" BorderStyle="Inset" BorderWidth="3" Width="500px" GroupingText="">
                                                        <marquee direction="up" onmouseover="this.stop()" onmouseout="this.start()" scrolldelay="200" style="height: 160px; width: 500px;">  
                         <asp:Literal ID="lt1" runat="server"></asp:Literal>  
                    </marquee>
                                                    </asp:Panel>
                                                </div>

                                                <%-- <div style="height: 230px; margin-left: 15px; width: 200px; float: left; margin-bottom: 5px;">
                                                    <b style="font-family: Tahoma; font-size: 20px; color: blue;">Urgent Position</b>
                                                    <asp:Panel ID="Panel2" runat="server" BackColor="#CCFFCC" BorderStyle="Inset" BorderWidth="3" Width="200px" GroupingText="">
                                                        <marquee direction="up" onmouseover="this.stop()" onmouseout="this.start()" scrolldelay="200" style="height: 200px; width: 200px;">  
                         <asp:Literal ID="lt2" runat="server"></asp:Literal>  
                    </marquee>
                                                    </asp:Panel>
                                                </div>--%>
                                            </div>




                                            <%-- <img src="..\Models\theway4c.JPG" class="img-fluid img-thumbnail" style="width: 900px; height: 30px; border: none; float: right;" alt="the way1">--%>
                                            <img src="..\Models\Screen3.JPG" class="img-fluid img-thumbnail" style="width: 900px; height: 50px; border: none; float: right;">
                                        </div>






                                    </div>



                                    <div style="height: auto;">
                                        <div style="width: 25%; float: left">
                                            <div class="col-md-12">
                                                <!-- general form elements disabled -->
                                                <div class="card card-warning">
                                                    <div class="card-header">
                                                        <h3 class="card-title"><b style="margin-left: 50px;">Receiving area E-map 3</b></h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div style="width: 100%">

                                                                <div style="width: 30%; height: auto; float: left">

                                                                    <div class="row row-cols-1" style="width: 60%; float: right">
                                                                        <%foreach (System.Data.DataRow rows1 in distinctLoc4.Rows)
                                                                            {%>
                                                                        <%foreach (System.Data.DataRow row1 in dt4.Rows)
                                                                            {%>
                                                                        <%-- <%if (row1[4].ToString()=="waiting")
                                                            {%>                                                                                                                                                                                                                                      
                                                                <% ttcheck = "waiting"; %>  
                                                            <%}%>--%>

                                                                        <%if (row1[1].ToString() == rows1[0].ToString())
                                                                            {%>
                                                                        <% mahang = mahang + row1[0].ToString();  %>
                                                                        <% ttcheck = ttcheck + row1[4].ToString();  %>
                                                                        <% trakho_mcs = row1[5].ToString();  %>
                                                                        <%}%>
                                                                        <%}%>
                                                                        <div class="col">
                                                                            <div id='<%=rows1[0].ToString() +"_"+ mahang %>' class="unit card_<%=i %>" style="border: 1px solid black; width: 70px; height: 70px;" onclick="return ShowPopUp('<%=rows1[0].ToString() %>')">
                                                                                <%--<div class="card bg-white" style="border:1px solid black" onclick="return ShowPopUp('<%=rows1[0].ToString() %>')">--%>
                                                                                <%--mau sac ---https://getbootstrap.com/docs/4.0/utilities/colors/--%>
                                                                                <%--color: white;mix-blend-mode: multiply;--%>
                                                                                <div class="">
                                                                                    <h7><%=rows1[0].ToString() %></h7>
                                                                                    <h6 style="font-size: 6px;" class="test_<%=i %>"><%=ttcheck %></h6>
                                                                                    <span style="color: white"><%=trakho_mcs.Trim() %></span>

                                                                                    <%foreach (System.Data.DataRow row_ur in dt_test2.Rows)
                                                                                        {%>
                                                                                    <%if (row_ur[0].ToString() == rows1[0].ToString())
                                                                                        {%>

                                                                                    <h1 class="urg_<%=i %> animateBorderTwo" style="font-size: 6px;">Urgent</h1>

                                                                                    <% }%>
                                                                                    <%} %>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                        <%i++; %>
                                                                        <% mahang = "";  %>
                                                                        <% ttcheck = ""; %>
                                                                        <%}%>



                                                                        <div style="width: 15%; height: 100px; text-align: center; margin-top: 10px;">
                                                                            <%--Air conditioners--%>
                                                                            <img src="..\Models\AC.JPG" class="img-fluid img-thumbnail" style="margin-top: 0px; width: 90px; float: left;" alt="AC">
                                                                        </div>
                                                                        <div style="width: 15%; height: 100px; text-align: center; margin-top: 150px;">
                                                                            <img src="..\Models\SMT_Part.JPG" class="img-fluid img-thumbnail" style="margin-top: 0px; width: 90px; float: left;" alt="smt line">
                                                                        </div>

                                                                    </div>



                                                                </div>

                                                                <div class="row row-cols-3" style="width: 60%; float: right">
                                                                    <%foreach (System.Data.DataRow rows1 in distinctLoc3.Rows)
                                                                        {%>
                                                                    <%foreach (System.Data.DataRow row1 in dt3.Rows)
                                                                        {%>
                                                                    <%if (row1[1].ToString() == rows1[0].ToString())
                                                                        {%>
                                                                    <% mahang = mahang + row1[0].ToString();  %>
                                                                    <% ttcheck = ttcheck + row1[4].ToString();  %>
                                                                    <% trakho_mcs = row1[5].ToString();  %>
                                                                    <%}%>
                                                                    <%}%>
                                                                    <div class="col">

                                                                        <div id='<%=rows1[0].ToString() +"_"+ mahang %>' class="unit card_<%=i %>" style="border: 1px solid black; width: 70px; height: 70px;" onclick="return ShowPopUp('<%=rows1[0].ToString() %>')">
                                                                            <div class="">
                                                                                <h7><%=rows1[0].ToString() %></h7>
                                                                                <h6 style="font-size: 6px;" class="test_<%=i %>"><%=ttcheck %></h6>
                                                                                <span style="color: white"><%=trakho_mcs.Trim() %></span>

                                                                                <%foreach (System.Data.DataRow row_ur in dt_test2.Rows)
                                                                                    {%>
                                                                                <%if (row_ur[0].ToString() == rows1[0].ToString())
                                                                                    {%>

                                                                                <h1 class="urg_<%=i %> animateBorderTwo" style="font-size: 6px;">Urgent</h1>

                                                                                <% }%>
                                                                                <%} %>
                                                                            </div>

                                                                        </div>




                                                                        <%-- <div class="">
                                                                    <h7><%=rows1[0].ToString() %></h7>
                                                                    <h6 style="font-size: 6px;" class="test_<%=i %>"><%=ttcheck %></h6>
                                                                    <span style="color: white"><%=trakho_mcs.Trim() %></span>
                                                                </div>--%>
                                                                    </div>
                                                                    <%i++; %>
                                                                    <% mahang = "";  %>
                                                                    <% ttcheck = ""; %>
                                                                    <%}%>
                                                                </div>


                                                            </div>





                                                        </div>
                                                    </div>
                                                    <!-- /.card-body -->
                                                </div>

                                            </div>

                                            <img src="..\Models\theway3.JPG" class="img-fluid img-thumbnail" style="margin-top: 0px; margin-left: 150px; border: none" alt="way2">
                                            Way
                                        </div>

                                        <div div style="width: 3%; float: left">
                                            <img src="..\Models\theway1.JPG" class="img-fluid img-thumbnail" style="margin-top: 0px; height: 790px; border: none" alt="the way1">
                                        </div>

                                        <div style="width: 25%; float: left">
                                            <div class="col-md-12">
                                                <!-- general form elements disabled -->
                                                <div class="card card-warning">
                                                    <div class="card-header">
                                                        <h3 class="card-title"><b style="margin-left: 50px;">Receiving area E-map 2</b></h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="row row-cols-4">
                                                                <%foreach (System.Data.DataRow rows1 in distinctLoc2.Rows)
                                                                    {%>
                                                                <%foreach (System.Data.DataRow row1 in dt2.Rows)
                                                                    {%>
                                                                <%if (row1[1].ToString() == rows1[0].ToString())
                                                                    {%>
                                                                <% mahang = mahang + row1[0].ToString();  %>
                                                                <% ttcheck = ttcheck + row1[4].ToString();  %>
                                                                <% trakho_mcs = row1[5].ToString();  %>
                                                                <%}%>
                                                                <%}%>
                                                                <div class="col">
                                                                    <div id='<%=rows1[0].ToString() +"_"+ mahang %>' class="unit card_<%=i %>" style="border: 1px solid black; width: 70px; height: 70px;" onclick="return ShowPopUp('<%=rows1[0].ToString() %>')">
                                                                        <div class="">
                                                                            <h7><%=rows1[0].ToString() %></h7>
                                                                            <h6 style="font-size: 6px;" class="test_<%=i %>"><%=ttcheck %></h6>
                                                                            <span style="color: white"><%=trakho_mcs.Trim() %></span>

                                                                            <%foreach (System.Data.DataRow row_ur in dt_test2.Rows)
                                                                                {%>
                                                                            <%if (row_ur[0].ToString() == rows1[0].ToString())
                                                                                {%>

                                                                            <h1 class="urg_<%=i %> animateBorderTwo" style="font-size: 6px;">Urgent</h1>

                                                                            <% }%>
                                                                            <%} %>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%i++; %>
                                                                <% mahang = "";  %>
                                                                <% ttcheck = ""; %>
                                                                <%}%>
                                                            </div>
                                                            <%--</div>--%>
                                                        </div>
                                                    </div>
                                                    <!-- /.card-body -->
                                                </div>

                                            </div>

                                            <%-- <img src="..\Models\theway3.JPG" class="img-fluid img-thumbnail" style="margin-top: 0px; margin-left: 150px; border: none" alt="way2">
                                            Way--%>
                                        </div>
                                        <div style="width: 35%; float: left">
                                            <div class="col-md-12">
                                                <div class="card card-warning">
                                                    <div class="card-header">
                                                        <h3 class="card-title"><b style="margin-left: 100px;">Receiving area E-map 1</b></h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="row row-cols-6">
                                                                <%foreach (System.Data.DataRow rows in distinctLoc.Rows)
                                                                    {%>
                                                                <%foreach (System.Data.DataRow row in dt1.Rows)
                                                                    {%>

                                                                <%if (row[1].ToString() == rows[0].ToString())
                                                                    {%>
                                                                <% mahang = mahang + row[0].ToString();  %>
                                                                <% ttcheck = ttcheck + row[4].ToString();  %>
                                                                <% trakho_mcs = row[5].ToString();  %>

                                                                <%}%>
                                                                <%}%>




                                                                <div class="col">
                                                                    <div id='<%=rows[0].ToString() +"_"+ mahang %>' class="unit card_<%=i %>" style="border: 1px solid black; width: 70px; height: 70px;" onclick="return ShowPopUp('<%=rows[0].ToString() %>')">
                                                                        <div>
                                                                            <%--http  concurrently.dev/2018/09/16/css-tao-chu-trong-suot-don-gian.html--%>
                                                                            <%-- tao chu trong suot--%>
                                                                            <%--<h6 style="font-size:6px; color: white;mix-blend-mode: multiply;"><%=ttcheck %></h6>--%>

                                                                            <h7><%=rows[0].ToString()%></h7>
                                                                            <%--<h6 style="font-size:6px; color: white;mix-blend-mode: multiply;" class="test_<%=i %>"><%=ttcheck %></h6>--%>
                                                                            <h6 style="font-size: 6px;" class="test_<%=i %>"><%=ttcheck %></h6>
                                                                            <span style="color: white"><%=trakho_mcs.Trim() %> </span>

                                                                            <%foreach (System.Data.DataRow row_ur in dt_test2.Rows)
                                                                                {%>
                                                                            <%if (row_ur[0].ToString() == rows[0].ToString())
                                                                                {%>

                                                                            <h1 class="urg_<%=i %> animateBorderTwo" style="font-size: 6px;">Urgent</h1>

                                                                            <% }%>
                                                                            <%} %>
                                                                        </div>

                                                                    </div>
                                                                </div>




                                                                <%i++; %>
                                                                <% mahang = "";  %>
                                                                <% ttcheck = ""; %>
                                                                <%}%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <%--<img src="..\Models\theway3.JPG" class="img-fluid img-thumbnail" style="margin-top: 0px; margin-left: 150px; border: none" alt="way2">--%>
                                            <%--Way--%>
                                        </div>

                                        <div style="width: 8%; float: left">
                                            <%-- <img src="..\Models\cong1new.JPG" class="img-fluid img-thumbnail" style="width: 200px; height: 900px; margin-top: 0px; border: none;" alt="Cong vao">--%>
                                            <img src="..\Models\cong3b.JPG" class="img-fluid img-thumbnail" style="width: 200px; height: 900px; margin-top: 0px; border: none;" alt="Cong vao">
                                        </div>

                                    </div>






                                </div>




                                <script type="text/javascript"> 

                                    $(document).ready(function () {
                                        //set 2 phut load lai page
                                        setTimeout(function () {
                                            window.location.reload(1);
                                            <%--document.getElementById('<%=bttLoadTime.ClientID%>').click();--%>
                                        }, 60000);



                                        $(".unit").each(function (index, value) {
                                            //debugger;
                                            var i = index + 1;
                                            var h6 = this.innerText;

                                            var h1 = this.innerText;
                                            if (h1.search(/Urgent/) >= 0) {
                                                //$(".card_" + i).css("border-color", "red");
                                                //$(".card_" + i).css("border", "3px solid red");
                                                $(".card_" + i).css({ "color": "#FA2A00", "outline": "10px dashed #F2D694", "box-shadow": "0 0 0 10px #FA2A00", "animation": "2s animateBorderTwo ease infinite" });
                                                $(".urg_" + i).text("Urgent");
                                            }


                                            //var checktrakho = this.find('input[name$="return_mcs"]').val();

                                            //if (h6.search(/checked/) >= 0) {
                                            //    //da check                            
                                            //    $(".card_" + i).css("background-color", "#00CC33");
                                            //    $(".test_" + i).text("checked");

                                            //}
                                            if (h6.search(/empty/) >= 0) {
                                                //da check                            
                                                $(".card_" + i).css("background-color", "#FFFAFA");
                                                $(".test_" + i).text("empty");

                                            }
                                            else if (h6.search(/finished/) >= 0) {
                                                //da check                            
                                                $(".card_" + i).css("background-color", "#00CC33");
                                                $(".test_" + i).text("finished");
                                            }
                                            else if (h6.search(/checked/) >= 0) {
                                                //da check                            
                                                $(".card_" + i).css("background-color", "#33FFFF");
                                                $(".test_" + i).text("checked");
                                            }
                                            else if (h6.search(/waiting/) >= 0) {
                                                //cho check
                                                $(".card_" + i).css("background-color", "#FFFF00");
                                                $(".test_" + i).text("waiting");
                                            }
                                            else if (h6.search(/nocheck/) >= 0) {
                                                //nocheck
                                                $(".card_" + i).css("background-color", "#FF9900");
                                                $(".test_" + i).text("nothing");
                                            }
                                            else if (h6.search(/nothing/) >= 0) {
                                                //khong co hang
                                                $(".card_" + i).css("background-color", "#FFFAFA");
                                                $(".test_" + i).text("nothing");
                                            }
                                            else {
                                                //khong co hang
                                                $(".card_" + i).css("background-color", "#FFFAFA");
                                            }

                                            //var checktrakho = $('input[name$="return_mcs"]').val();

                                        });

                                        $("#mahang").keyup(function () {
                                            search_material();
                                        });

                                        $("#vendorname").keyup(function () {
                                            search_vendor();
                                        });

                                    });


                                    function ShowPopup() {
                                        $("#lardgeModal").modal("show");
                                    }

                                    //$("#mahang").keyup(function () {
                                    //    search_material();
                                    //});
                                    //$("#search_qc").click(function () {
                                    //    search_material();
                                    //})

                                    function search_vendor() {
                                        var str = $('#vendorname').val();
                                        if (str != '') {
                                            $.ajax({
                                                type: "POST",
                                                url: "Main.aspx/checkvendor",
                                                contentType: "application/json; charset=utf-8",
                                                data: JSON.stringify({ 'vendor': str }),
                                                dataType: "json",
                                                success: function (data) {
                                                    var objdata = $.parseJSON(data.d);
                                                    var _vitri = objdata['Table'][0][0];
                                                    if (_vitri === 0) {
                                                        //alert('NO DATA !!!');
                                                    } else {
                                                        //alert('OK !!!');
                                                        //$('.popup_vendor').show();
                                                        $('#t02 tbody tr').remove();
                                                        $('#vederselect').text(str);

                                                        var objdata = $.parseJSON(data.d);
                                                        var j = 0;
                                                        for (var i = 0; i < objdata['Table'].length - 1; i++) {
                                                            j = j + 1;
                                                            $("#t02 tbody ").append("<tr><td>" + j + "</td><td>" + objdata['Table'][i][0] + "</td></tr>");
                                                        }
                                                        $('#myModal2').modal('show');
                                                    }
                                                }
                                            });
                                        }
                                        else {
                                            //$(".unit").css({
                                            //    'backgroundColor': 'white', 'color': 'black',
                                            //    'font - size': '14px',
                                            //    'font - weight': 'bold',
                                            //    '- webkit - animation': 'my 0ms',
                                            //    '-moz - animation': 'my 0ms ',
                                            //    '-o - animation': 'my 0ms ',
                                            //    'animation': 'my 0ms '

                                            //});
                                            //window.location.href = "Good_Receive_MCS.aspx?documentNo=" + $.trim($("#MainContent_txt_doc").val());
                                            window.location.href = "Main.aspx";
                                        }

                                        //$("#inner").text(str);
                                    }


                                    function search_material() {
                                        var str = $('#mahang').val();
                                        if (str != '') {
                                            $.ajax({
                                                type: "POST",
                                                url: "Main.aspx/Checkmahang",
                                                contentType: "application/json; charset=utf-8",
                                                data: JSON.stringify({ 'mahang': str }),
                                                dataType: "json",
                                                success: function (data) {
                                                    var objdata = $.parseJSON(data.d);
                                                    var _vitri = objdata['Table'][0][0];
                                                    if (_vitri === 0) {
                                                        //alert('Material Not exists !!!');
                                                        $(".unit").css({
                                                            'backgroundColor': 'white', 'color': 'black',
                                                            'font - size': '14px',
                                                            'font - weight': 'bold',
                                                            '- webkit - animation': 'my 0ms',
                                                            '-moz - animation': 'my 0ms ',
                                                            '-o - animation': 'my 0ms ',
                                                            'animation': 'my 0ms '

                                                        });

                                                    } else {
                                                        debugger;
                                                        $(".unit").each(function () {
                                                            var i = $(this).attr("id");

                                                            if (i.split('_')[1].search(str) > -1) {
                                                                $(this).css({
                                                                    'backgroundColor': 'red', 'color': 'Green',
                                                                    'font - size': '14px',
                                                                    'font - weight': 'bold',
                                                                    '- webkit - animation': 'my 700ms infinite',
                                                                    '-moz - animation': 'my 700ms infinite',
                                                                    '-o - animation': 'my 700ms infinite',
                                                                    'animation': 'my 700ms infinite'
                                                                });
                                                            } else {
                                                                $(this).css({
                                                                    'backgroundColor': 'white', 'color': 'black',
                                                                    'font - size': '14px',
                                                                    'font - weight': 'bold',
                                                                    '- webkit - animation': 'my 0ms',
                                                                    '-moz - animation': 'my 0ms ',
                                                                    '-o - animation': 'my 0ms ',
                                                                    'animation': 'my 0ms '

                                                                });
                                                            }
                                                        });
                                                    }
                                                }
                                            });
                                        }
                                        else {
                                            //$(".unit").css({
                                            //    'backgroundColor': 'white', 'color': 'black',
                                            //    'font - size': '14px',
                                            //    'font - weight': 'bold',
                                            //    '- webkit - animation': 'my 0ms',
                                            //    '-moz - animation': 'my 0ms ',
                                            //    '-o - animation': 'my 0ms ',
                                            //    'animation': 'my 0ms '

                                            //});
                                            //window.location.href = "Good_Receive_MCS.aspx?documentNo=" + $.trim($("#MainContent_txt_doc").val());
                                            window.location.href = "Main.aspx";
                                        }
                                        $("#inner").text(str);
                                    }


                                    function search_material2() {
                                        var str = $('#mahang').val();
                                        if (str != '') {
                                            $.ajax({
                                                type: "POST",
                                                url: "Main.aspx/Checkmahang",
                                                contentType: "application/json; charset=utf-8",
                                                data: JSON.stringify({ 'mahang': str }),
                                                dataType: "json",
                                                success: function (data) {
                                                    var objdata = $.parseJSON(data.d);
                                                    var _vitri = objdata['Table'][0][0];
                                                    if (_vitri === 0) {
                                                        alert('Material Not exists !!!');
                                                        $(".unit").css({
                                                            'backgroundColor': 'white', 'color': 'black',

                                                            'font - size': '14px',
                                                            'font - weight': 'bold',
                                                            '- webkit - animation': 'my 0ms',
                                                            '-moz - animation': 'my 0ms ',
                                                            '-o - animation': 'my 0ms ',
                                                            'animation': 'my 0ms '

                                                        });

                                                    } else {
                                                        debugger;
                                                        $(".unit").each(function () {
                                                            var i = $(this).attr("id");

                                                            if (i.split('_')[1].search(str) > -1) {
                                                                $(this).css({
                                                                    'backgroundColor': 'blue',
                                                                    'color': '#e91e63',
                                                                    'font - size': '140%',
                                                                    'font - weight': 'bold',
                                                                    '- webkit - animation': 'my 500ms infinite',
                                                                    '-moz - animation': 'my 500ms infinite',
                                                                    '-o - animation': 'my 500ms infinite',
                                                                    'animation': 'my 500ms infinite'
                                                                });
                                                            } else {
                                                                $(this).css({
                                                                    'backgroundColor': 'white', 'color': 'black',
                                                                    'font - size': '14px',
                                                                    'font - weight': 'bold',
                                                                    '- webkit - animation': 'my 0ms',
                                                                    '-moz - animation': 'my 0ms ',
                                                                    '-o - animation': 'my 0ms ',
                                                                    'animation': 'my 0ms '

                                                                });
                                                            }
                                                        });
                                                    }
                                                }
                                            });
                                        }
                                        else {
                                            //$(".unit").css({
                                            //    'backgroundColor': 'white', 'color': 'black',
                                            //    'font - size': '14px',
                                            //    'font - weight': 'bold',
                                            //    '- webkit - animation': 'my 0ms',
                                            //    '-moz - animation': 'my 0ms ',
                                            //    '-o - animation': 'my 0ms ',
                                            //    'animation': 'my 0ms '

                                            //});
                                            //window.location.href = "Good_Receive_MCS.aspx?documentNo=" + $.trim($("#MainContent_txt_doc").val());
                                            window.location.href = "Main.aspx";
                                        }
                                        $("#inner").text(str);
                                    }



                                </script>
                            </div>



                        </div>
                    </div>
                </div>



                <%--<asp:Button ID="bttLoadTime" class="btn btn-success" Width="140px" Font-Size="16px" runat="server" OnClientClick="disableBtn(this.id, 'Loading Data...')"
                UseSubmitBehavior="false" Font-Bold="true" Text="" OnClick="bttLoadTime_Click"  />--%>
        </form>
    </div>

    <%--<script src=""></script> //cdnjs.cloudflare.com/ajax/libs/gsap/2.1.3/TweenMax.min.js--%>
    <script src="dist/js/TweenMax.min.js"></script>
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

    <%--<script src="Scripts/jquery.datetimepicker.js"></script>--%>


    <script type="text/javascript">
        $(document).ready(function () {
            bsCustomFileInput.init();
        });
    </script>

    <!--https://code.jquery.com/ui/1.12.1/jquery-ui.js -->
    <%--<script src="#"></script>--%>
    <script src="plugins/jquery/jquery-ui.js"></script>

    <script>
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
            dateNewFormat += '0' + (today.getMonth() + 1);
        }

        dateNewFormat = dateNewFormat + '-' + today.getFullYear();
        //dateNewFormat = today.getFullYear() + '-';

        $('#datepicker').val(dateNewFormat);

    </script>

    <script>
        //var siteW = $(window).width();
        //var siteH = $(window).height();

        //$("p").css({ lineHeight: siteH + 'px' }); // dirty dirty

        //TweenMax.set(".site", { perspective: 10000 });
        //TweenMax.set(".container1", { transformStyle: "preserve-3d", transformOrigin: '-0% 50%' });
        ////TweenMax.set("#new-content", { rotationY: 90, z: -siteW / 1, x: siteW / 1 });
        //TweenMax.set("#new-content", { rotationY: 90, z: -siteW / 2, x: siteW / 2 });

        //var tlFlip = new TimelineMax({ yoyo: true, repeat: -1, delay: 1.5, repeatDelay: 60 });

        //tlFlip
        //    .to('.site', .5, { scale: 0.8, ease: Power2.easeInOut }, "start")
        //    .to('.container1', .4, { rotationY: -90, z: -siteW, ease: Power2.easeInOut }, "start+=0.7")
        //    .to('.site', .5, { scale: 1, ease: Power2.easeInOut }, "start+=1.2");

    </script>

    <script type="text/javascript">
        //function disableBtn(btnID, newText) {

        //    Page_IsValid = null;
        //    if (typeof (Page_ClientValidate) == 'function') {
        //        Page_ClientValidate();
        //    }
        //    var btn = document.getElementById(btnID);
        //    var isValidationOk = Page_IsValid;

        //    if (isValidationOk !== null) {
        //        if (isValidationOk) {
        //            btn.disabled = true;
        //            btn.value = newText;
        //        } else {
        //            btn.disabled = false;
        //            btnFilter.disabled = false;
        //        }
        //    }
        //    else {
        //        setTimeout(function () {
        //            ShowProgress();
        //        }, 100);
        //        btn.disabled = true;
        //        btn.value = newText;

        //    }
        //}
        //function ShowProgress() {
        //    setTimeout(function () {
        //        var modal = $('<div />');
        //        modal.addClass("modal");
        //        $('body').append(modal);
        //        var loading = $(".loading");
        //        loading.show();
        //        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        //        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        //        loading.css({ top: top, left: left });
        //    }, 200);
        //}
    </script>

</body>
</html>
