<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LabLogin.aspx.cs" Inherits="Mediqura.Web.LabLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="mediqura lab login page">
    <meta name="author" content="MOBIMP">
    <title>Mediqura Lab</title>

    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/style.css" rel="stylesheet" />


</head>
<body class=" login2background">


    <div class="container-fluid">
        <!-- CONTAINER STARTS -->

        <div class="col-md-12">

            <div class="col-md-3">
                <!-- CLIENT LOGO STARTS HERE -->
                <img src="Images/CompanyLogo.jpg" width="140" height="131" style="margin-top: 20%; margin-left: 9%" />
                <h3 style="color: #fff; margin-top: 1px;">SHAIBO HOSPITAL</h3>
                <p style="color: #fff;">
                    Khurai Sajor Leikai, Near Chaithabi Leirak
                </p>
                <br />
            </div>
            <!-- CLIENT LOGO ENDS HERE -->

            <div class="col-md-9">
                <!-- COL-MD-9 STARTS -->

                <!-- BLANK COLUMN -->
                <div class="col-md-8">
                </div>
                <!-- BLANK COLUMN ENDS -->

                <!-- LOGIN FORM STARTS HERE -->
                <form id="Form1" class="login" runat="server">
                    <div class=" col-md-4 loginbox" style="z-index: 22; border-radius: 8px;">
                        <div class=" row">
                            <%--<div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                                <img src="assets/img/log.jpg" alt="Logo" class="logo">
                            </div>--%>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6  " style="margin-left: 12% !important">
                                <span class="singtext">Login </span>
                            </div>

                        </div>
                        <div class=" row loginbox_content ">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-user" style="color: #fff;"></span>
                                </span>
                                <asp:TextBox runat="server" class="form-control" MaxLength="30" required="" ID="txtusername" oncopy="return false" onpaste="return false" autocomplete="off" placeholder="User Name"></asp:TextBox>

                            </div>
                            <br>
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-lock" style="color: #fff;"></span>
                                </span>
                                <asp:TextBox class="form-control" runat="server" MaxLength="30" required="" ID="txtpassword" oncopy="return false" onpaste="return false" autocomplete="off" TextMode="Password" placeholder="Password"></asp:TextBox>

                            </div>
                        </div>
                        <div class="row " style="margin-top: 1em;">
                            <div class="col-lg-8 col-md-8  col-sm-8 col-xs-7 forgotpassword ">
                                <a href="#" style="color: #fff;">Forgot Username / Password?</a>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4  col-xs-5 ">
                                <asp:Button runat="server" ID="btnlogin" Text="Login" class=" btn btn-default submit-btn" OnClick="btnlogin_Click" />

                            </div>
                            <div class="col-md-12">
                                <div style="color: rgb(255, 255, 255); text-align: -webkit-center; background-color: #eb4336; font-size: .9em;">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row ">
                            <hr>
                            <div class="col-md-12 forgotpassword ">
                                <a href="#" style="color: #fff;">For any Support Call</a>

                            </div>
                            <div class="col-md-12 forgotpassword">
                                <span style="font-weight: bold; font-size: 17px; color: #fff;">+91 7005144148</span>
                            </div>
                        </div>

                    </div>
                </form>
                <!-- LOGIN FORM ENDS HERE -->

            </div>
            <!-- COL-MD-9 ENDS -->

        </div>

    </div>
    <!-- CONTAINER ENDS-->

    <div class="container-fluid">
        <!-- CONTAINER STARTS -->
        <div class="row">


            <div class="col-md-12">
                <!-- GRAPHICS STARTS -->

                <div class="col-md-9" style="margin-top: -17em;">

                    <img class="img-responsive" src="assets/img/graphic.svg" />
                </div>
            </div>
            <!-- GRAPHICS ENDS -->
        </div>

    </div>
    <!-- CONTAINER ENDS -->



    <!-- FOOTER STARTS -->
    <div class="navbar navbar-fixed-bottom">
        <div class="container-fluid">

            <div class="col-md-12">

                <div class="col-md-5">
                    <p class="navbar-text" style="color: #FFFFFF; font-size: 12px;">
                        © 2022, All Rights Reserved, SHAIBO HOSPITAL
                
            
                    </p>
                </div>

                <div class="col-md-3" style="margin-top: 16px;">
                    <a style="color: #FFFFFF" href="#">Powered By - Mobimp Services Pvt Ltd</a>
                </div>

                <div class="col-md-4">
                    <img src="assets/img/1.png" width="120" style="float: right;">
                </div>
            </div>
        </div>

    </div>
    <!-- GRAPHICS ENDS -->
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="assets/assets/js/jquery.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="assets/assets/js/ie10-viewport-bug-workaround.js"></script>
</body>
</html>
