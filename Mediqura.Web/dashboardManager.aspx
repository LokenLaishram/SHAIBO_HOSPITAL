<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboardManager.aspx.cs" Inherits="Mediqura.Web.dashboardManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="DashboardCss/bootstrap.min.css" rel="stylesheet" />
    <link href="Styles/responsive.bootstrap.min.css" rel="stylesheet" />
     <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="Styles/medistyles.css" rel="stylesheet" />
    <link href="Styles/admin.css" rel="stylesheet" />

    <link href="Styles/jquery.contextMenu.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="col-sm-12">
                   <asp:Literal ID="BedDataliterals" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </form>
</body>

<script src='<%= this.ResolveClientUrl("Scripts/contextJquery.min.js") %>'></script>
<script src='<%= this.ResolveClientUrl("Scripts/jquery.contextMenu.js") %>'></script>
<script src='<%= this.ResolveClientUrl("Scripts/jquery.ui.position.min.js") %>'></script>
<script src='<%= this.ResolveClientUrl("DashboardCss/bootstrap.min.js") %>'></script>
<script src='<%= this.ResolveClientUrl("/Styles/service.js") %>'></script>
<script type="text/javascript">
        $(function () {
            $.contextMenu({
                selector: '#right-click',
                callback: function (key, options) {
                    var m = "clicked: " + key;
                    window.console && console.log(m) || alert(m);
                },
                items: {
                    "Doctor": { name: "Doctor Round" },
                    "sep1": "---------",
                    "Other": { name: "Other Services" },
                    "sep2": "---------",
                    "OtherWithDoctor": { name: "Other Services with doctor" },
                    "sep3": "---------",
                    "INV": { name: "Lab/Radio service" }
                 
                }
            });

        });
    </script>
</html>
