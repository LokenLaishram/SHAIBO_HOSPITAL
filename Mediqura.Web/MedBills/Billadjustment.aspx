<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="Billadjustment.aspx.cs" Inherits="Mediqura.Web.MedBills.Billadjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:TabContainer ID="tabbillcontainer" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabbill" runat="server" HeaderText="Bill Controller">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                    <ContentTemplate>
                        <div class="custab-panel" id="divbillcontroller">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Bill Category</span>
                                        <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_billcategory_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_billcategory">
                                            <asp:ListItem Value="1">Emergency Bill</asp:ListItem>
                                            <asp:ListItem Value="2">IP Bill</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_ipno" class="input-group-addon cusspan" runat="server" style="color: red">IP / Emerg. Number <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" AutoPostBack="true" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35"
                                            ID="txt_patientNumber" OnTextChanged="txt_patientNumber_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetPatientNumber" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_patientNumber"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Service Category</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_servicecategory" AutoPostBack="true" OnSelectedIndexChanged="ddl_servicecategory_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Bed</asp:ListItem>
                                            <asp:ListItem Value="2">Doctor Round</asp:ListItem>
                                            <asp:ListItem Value="3">Services</asp:ListItem>
                                            <asp:ListItem Value="4">Investigation</asp:ListItem>
                                            <asp:ListItem Value="5">OT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_name">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">ADT</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_admissiontime">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Adm. Doctor</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_admissiondoctor">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Patient Category</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_patientcategory">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div class="pbody">
                                        <div class="gridview-container-ML">
                                            <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="Gv_Ipbilldetails" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." OnRowDataBound="Gv_Ipbilldetails_RowDataBound" OnRowCommand="gvipservicerecord_RowCommand" AutoGenerateColumns="False"
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# (Container.DataItemIndex+1)+(Gv_Ipbilldetails.PageIndex)*Gv_Ipbilldetails.PageSize %>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Service Number
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_serviceID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ServiceID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_subheading" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("IsSubheading") %>'></asp:Label>
                                                                <asp:Label ID="lbl_servicecategory" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ServiceCategoryID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_servicenumber" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ServiceNumber") %>'></asp:Label>
                                                                <asp:Label ID="lblinvnumber" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("InvNumber") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Service Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_servicename" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ServiceName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Charge
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lbl_rate" MaxLength="10" Width="50PX" AutoPostBack="true" OnTextChanged="txt_ServiceCharge_TextChanged" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ServiceCharge", "{0:0#.##}") %>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="lbl_rate" ID="FilteredTextBoxExtender3"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Qty
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lbl_quantity" MaxLength="10" AutoPostBack="true" OnTextChanged="txt_qty_TextChanged" Width="25PX" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Quantity") %>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="lbl_quantity" ID="FilteredTextBoxExtender4"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Net Charge
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lbl_netcharge" ReadOnly="true" MaxLength="10" Width="50PX" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("NetServiceCharge", "{0:0#.##}") %>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="lbl_netcharge" ID="FilteredTextBoxExtender5"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Service Date  
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lbl_serviceDate" Width="130PX" runat="server" Text='<%# Eval("ServiceDate")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Service End Date  
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lbl_serviceEndDate" Width="130PX" runat="server" Text='<%# Eval("BedLastDate")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Doctor
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_doctor" runat="server" Text='<%# Eval("Doctor")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Added By
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_groupnumber" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("GroupNumber") %>'></asp:Label>
                                                                <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Remarks
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_remarks" Width="80PX" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lbl_print" Style="color: red; font-size: 12px" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                     Print
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <span class="cus-Delete-header">Delete</span>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                    OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                       <i class="fa fa-trash-o cus-delete-color"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4" style="height: 5px"></div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Total Bill</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalbill">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Total Deposited</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_deposited">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Payable Amount</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_payable">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-4"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnsave" runat="server" Text="Update" OnClick="btnsave_Click" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btnprint" OnClick="btnprint_Click" runat="server" Text="Print" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" Class="btn  btn-sm cusbtn" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
