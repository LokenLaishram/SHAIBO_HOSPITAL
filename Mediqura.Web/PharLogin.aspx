<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PharLogin.aspx.cs" Inherits="Mediqura.Web.PharLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="mediqura pharmacy login page" />
    <meta name="author" content="MOBIMP" />
    <title>Mediqura Pharma</title>
    <!-- Bootstrap core CSS -->
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="Styles/PharStyleSheet.css" rel="stylesheet" />
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->

</head>
<body class=" login2background" runat="server">


    <div class="container-fluid">
        <!-- CONTAINER STARTS -->

        <div class="col-md-12">

            <div class="col-md-3">
                <!-- CLIENT LOGO STARTS HERE -->

                <img src="assets/img/log.png" width="120" style="margin-top: 15%;">
                <h3 style="color: #fff; margin-top: -6px;">Raj Medicity Pharmacy</h3>
                <p style="color: #fff;">
                    North AOC, Imphal-West,<br>
                    Manipur - 795001
                </p>
            </div>
            <!-- CLIENT LOGO ENDS HERE -->

            <div class="col-md-9">
                <!-- COL-MD-9 STARTS -->

                <!-- BLANK COLUMN -->
                <div class="col-md-8">
                </div>
                <!-- BLANK COLUMN ENDS -->

                <!-- LOGIN FORM STARTS HERE -->
                <!-- LOGIN FORM STARTS HERE -->
                <form id="Form1" class="login" runat="server">
                    <div class=" col-md-4 loginbox">
                        <div class=" row">
                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                                <img src="Images/Pharmalogo.png" alt="Logo" class="logo">
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6  ">
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
                        </div>
                         <div class="col-md-12">
                                <div style="color: rgb(255, 255, 255); text-align: -webkit-center; background-color: #eb4336; font-size: .9em;">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                        <div class="row ">

                            <hr>
                            <div class="col-md-12 forgotpassword ">
                                <a href="#" style="color: #fff;">For any Support Call</a>

                            </div>
                            <div class="col-md-12 forgotpassword">
                                <span style="font-weight: bold; font-size: 18px; color: #fff;"></span>
                            </div>

                        </div>

                    </div>

                    <!-- LOGIN FORM ENDS HERE -->
                </form>
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

                <div class="col-md-9" style="margin-top: -16em;">

                    <img class="img-responsive" src="Images/graphic.svg">
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

                <div class="col-md-6">
                    <p class="navbar-text" style="color: #fff; font-size: 12px;">
                        © 2018, All Rights Reserved, Raj Medicity 
                
            
                    </p>
                </div>

                <div class="col-md-3" style="margin-top: 16px;">
                    <a href="#">Powered By - Mobimp Services Pvt Ltd</a>
                </div>

                <div class="col-md-3">
                    <img src="Images/1.png" width="120" style="float: right;">
                </div>
            </div>
        </div>

    </div>
    <!-- GRAPHICS ENDS -->
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->

    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="Scripts/ie10-viewport-bug-workaround.js"></script>
</body>
</html>
