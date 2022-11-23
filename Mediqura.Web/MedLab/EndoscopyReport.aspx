<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndoscopyReport.aspx.cs" Inherits="Mediqura.Web.MedLab.EndoscopyReport" %>
<html>
<head>
    <style>
        .shadow {
            background-color: #FFF;
            margin-left: 15%;
            margin-right: 15%;
            padding: 30px;
            -webkit-box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
            -moz-box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
            -ms-box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
            -o-box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
            box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
            -webkit-transition: all 0.25s ease-in-out;
            -moz-transition: all 0.25s ease-in-out;
            -ms-transition: all 0.25s ease-in-out;
            -o-transition: all 0.25s ease-in-out;
            transition: all 0.25s ease-in-out;
        }

        .verify-btn {
            position: fixed;
            float: right;
            right: 33px;
            bottom: 12px;
            background: #a2e4a7;
            border-color: #5e735e;
            padding-left: 30px;
            padding-right: 30px;
            padding-top: 5px;
            padding-bottom: 5px;
            font-size: 15px;
            color: #344439;
            font-weight: 600;
            cursor: pointer;
        }

        #msgDiv {
            position: fixed;
            top: 20px;
            right: 0;
            border: 1px solid #aaa;
            background: #aaa;
            color: #fff;
            padding: 5px;
        }

        body {
            background-color: #f7f7f7;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="shadow">
            <asp:Literal runat="server" ID="ltReport"></asp:Literal>
        </div>
         <asp:DropDownList ID="ddlverifyby" runat="server"  Width="200px"  >
         </asp:DropDownList>
                                                           
        <asp:Button CssClass="verify-btn" runat="server" ID="btn_verify" Text="Verify" OnClick="btn_verify_Click" />
        <div id="msgDiv" runat="server">
            <asp:Label ID="lblMessage" runat="server">Successfully updated!</asp:Label>
        </div>
    </form>

</body>
</html>
