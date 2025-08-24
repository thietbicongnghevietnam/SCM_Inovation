<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main_test2.aspx.cs" Inherits="FreeLayout.Main_test2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            width: 100%;
        }


        h1 {
            font-family: 'Raleway';
            font-weight: bold;
            text-align: center;
            font-size: 50px;
            color: #fff;
            margin-top: 120px;
        }

        /* PAGES
-------------------------------------------------*/
        #main-page {
            height: 25px;
            width: 375px;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
            background-color: #27ae60;
            display: none;
        }

        #next-page {
            height: 25px;
            width: 375px;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
            background-color: #e74c3c;
            display: none;
        }

        .maincontent, .nextcontent {
            padding-top: 40px;
            display: none;
        }

        a.back {
            font-family: 'Lato';
            font-size: 20px;
            color: #dfdfdf;
            text-decoration: none;
            text-align: center;
            width: 20%;
            margin: 25px auto 30px auto;
            display: block;
        }

        a.mainlink, a.nextlink {
            font-family: 'Lato';
            color: #fff;
            border: 3px solid #fff;
            padding: 15px 10px;
            display: block;
            text-align: center;
            margin: 25px auto;
            width: 13%;
            text-decoration: none;
            cursor: pointer;
            font-size: 20px;
            font-weight: 500;
        }

            a.mainlink:hover, a.nextlink:hover {
                background: #fff;
                color: #575757;
            }
    </style>

    <%-- <script>
        function pageRedirect() {
            window.location.replace("#");
        }
        setTimeout("pageRedirect()", 5000);
    </script>--%>

     <script>
        function pageRedirect() {
            window.location.replace("http://localhost:61772/Main.aspx");
        }
        setTimeout("pageRedirect()", 30000);
    </script>

</head>

<body>
    <form id="form1" runat="server">




        <div>

            <div id="main-page">
                <div class="maincontent">
                    <h1>Full Page Transition</h1>
                    <a class="mainlink">TRY IT NOW &rarr;</a>

                </div>
            </div>

            <div id="next-page">
                <div class="nextcontent">
                    <h1>Great! You're in the 2nd Page!</h1>
                    <a class="nextlink">&larr; GO BACK</a>
                </div>
            </div>

        </div>
    </form>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script>

        $(document).ready(function () {

            


            $.fn.animateRotate = function (angle, duration, easing, complete) {
                var args = $.speed(duration, easing, complete);
                var step = args.step;
                return this.each(function (i, e) {
                    args.complete = $.proxy(args.complete, e);
                    args.step = function (now) {
                        $.style(e, 'transform', 'rotate(' + now + 'deg)');
                        if (step) return step.apply(e, arguments);
                    };

                    $({ deg: 0 }).animate({ deg: angle }, args);
                });
            };

            $("#main-page").css("background-color", "#e74c3c");
            $("#main-page").css("height", "100vh");
            $("#main-page").css("width", "100%");
            $("#main-page").fadeIn();
            $(".maincontent").fadeIn();        

            
            $(".mainlink").on("click", function () {
                $(".maincontent").fadeOut();
                $("#main-page").animate({
                    width: "25px",
                    height: "375px"
                }, function () {
                    $(this).animateRotate(90);
                });

                setTimeout(function () {
                    $("#main-page").fadeOut();
                }, 1500);

                setTimeout(function () {
                    $("#next-page").animateRotate(0, 0);
                    $("#next-page").css("height", "25px");
                    $("#next-page").css("width", "375px");
                    $("#next-page").fadeIn();
                    $("#next-page").animate({
                        backgroundColor: "#27ae60"
                    }, function () {
                        $(this).animate({
                            height: "100vh"
                        }, function () {
                            $(this).animate({
                                width: "100%"
                            }, function () {
                                $(".nextcontent").fadeIn(300);
                            });
                        });
                    });
                }, 800);
              
            });

            $(".nextlink").on("click", function () {
                $(".nextcontent").fadeOut();
                $("#next-page").animate({
                    width: "25px",
                    height: "375px"
                }, function () {
                    $(this).animateRotate(-90);
                });

                setTimeout(function () {
                    $("#next-page").fadeOut();
                }, 1500);

                setTimeout(function () {
                    $("#main-page").animateRotate(0, 0);
                    $("#main-page").css("height", "25px");
                    $("#main-page").css("width", "375px");
                    $("#main-page").fadeIn();
                    $("#main-page").animate({
                        height: "100vh"
                    }, function () {
                        $(this).animate({
                            width: "100%"
                        }, function () {
                            $(".maincontent").fadeIn(300);
                        });
                    });
                }, 1400);

                


            });

        });
    </script>


</body>
</html>
