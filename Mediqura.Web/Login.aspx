<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Mediqura.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title>Mediqura</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="css/login.css" rel="stylesheet" />
    <style>
        body {
            padding-top: 70px;
            /* Required padding for .navbar-fixed-top. Remove if using .navbar-static-top. Change if height of navigation changes. */
        }

        .hospitalinfo {
            color: #FFFFFF;
            position: absolute;
            margin-left: 162px;
        }
    </style>
</head>

<body>

    <!-- Navigation -->
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
            </div>
        </div>
        <!-- /.container -->
    </nav>

    <!-- Page Content -->
    <div class="container" style="margin-top: 53px;">
        <div class="row">
            <img src="assets/img/log.jpg" style="width: 100px; position: absolute; margin-left: 3em;" />
            <%--<div class="hospitalinfo">
                 <h1 style="font-family: 'Cabin', sans-serif; font-size: 40px;">Nancy Lab</h1>
                <h4 style="font-family: 'Cabin', sans-serif; margin-top: -8px; font-size: 15px;"> Manipur - 795001</h4>
          </div>--%>
        </div>
        <div class="row">
            <div class="text-center" style="width: 294px; margin-right: 70px; float: right;">
                <div class="row logincontain">
                    <div class="col-md-12">
                        <div class="wrap">
                            <p class="form-title">
                                <img src="images/logo.png">
                            </p>
                            <form class="login" runat="server">
                                <asp:TextBox runat="server"  MaxLength="30" required="" ID="txtusername" oncopy="return false" onpaste="return false" autocomplete="off" placeholder="User Name"></asp:TextBox>
                                <asp:TextBox runat="server" MaxLength="30" required="" ID="txtpassword" oncopy="return false" onpaste="return false" autocomplete="off" TextMode="Password" placeholder="Password"></asp:TextBox>
                                <asp:Button runat="server" Style="background-color: #31e9bb;" ID="btnlogin" Text="Login" class="btn  loginbtn btn-sm" OnClick="btnlogin_Click" />
                                <div class="remember-forgot">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div style="color: rgb(255, 255, 255); text-align: -webkit-center; background-color: #eb4336; font-size: .9em;">
                                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-12 forgot-pass-content">
                                            <a class="forgot-pass">Forgot Password</a>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
<footer>
	<p><a href="http://www.mobimp.com"><img style="width: 120px" src="images/Mobimp.png"/></a></p>
</footer>

</html>
