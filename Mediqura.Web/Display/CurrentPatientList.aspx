<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrentPatientList.aspx.cs" Inherits="Mediqura.Web.Display.CurrentPatientList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../DashboardCss/bootstrap.min.css" rel="stylesheet" />
    <script src="../DashboardCss/jquery.min.js"></script>
    <script src="../DashboardCss/bootstrap.min.js"></script>
    
<script type="text/JavaScript">
    setTimeout(function () {
        location.reload();
    }, 200000);
    setInterval(function () {

        //time to scroll to bottom
        $("html, body").animate({ scrollTop: $(document).height() }, 1000);

        //scroll to top
        setTimeout(function () {
            $('html, body').animate({ scrollTop: 0 }, 100000);
        }, 100);//call every 2000 miliseconds

    }, 100);
</script>
 
</head>
<body style="font-size: 20px;
font-weight: bold;">
    <img style="position:fixed;padding: 20px;
right: 0;
bottom: 50px;
opacity: 0.5;" src="../Images/medi.svg" />
    <form id="form1" runat="server">
        <div>

                             <asp:GridView ID="gvadmissionlist" runat="server" CssClass="table table-bordered" 
                                        Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false" OnRowDataBound="gvadmissionlist_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Sl.No
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSlNo" Style="text-align: left !important;" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("PatName") %>'></asp:Label>
                                                     <asp:Label ID="lblSubHeading" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("IsSubHeading") %>'></asp:Label>
                                                    <asp:Label ID="lblIsRelease" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("isReleased") %>'></asp:Label>
                                                    <asp:Label ID="lblPatientActive" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("PatientActive") %>'></asp:Label>
                                                    <asp:Label ID="lblWard" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("wardName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Address
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("patAddress") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Age
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblage" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("Agecount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Room
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblroom" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("Room") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Bed No.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbed" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("BedNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                    </asp:GridView>
        </div>
    </form>
</body>
</html>
