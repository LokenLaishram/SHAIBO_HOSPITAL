<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="Patientenquiry.aspx.cs" Inherits="Mediqura.Web.MedIPD.Patientenquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontaineritemgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabitemgroupmaster" runat="server" CssClass="Tab2" HeaderText="Patient Enquiry">
                    <ContentTemplate>
                        <div class="custab-panel" id="paneldepartmentmaster">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div6" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group">
                                        <span id="lbl_ipno" class="input-group-addon cusspan" runat="server" style="color: red">IPNo <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35" AutoPostBack="true" OnTextChanged="txt_autoipno_TextChanged"
                                            ID="txt_autoipno"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_autoipno" ID="FilteredTextBoxExtender1"
                                            runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                                            FilterMode="ValidChars"
                                            ValidChars="-,:|#/" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_autoipno"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">From <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_from"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_from" />
                                        <%--    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_mfgdate" />
                                        --%>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">To <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_To"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_To" />
                                        <%--  <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_mfgdate" />
                                        --%>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg3" runat="server">
                                            <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">

                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-L">
                                                <div class="grid" style="float: left; width: 100%; height: 50vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvIpdPatientEnq" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    IP No.
                                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIPDIpNo" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("IPNO") %>'></asp:Label>

                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Patient Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIPDPatientName" runat="server" Text='<%# Eval("PatientName") %>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Address
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIPDAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Contact No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIPDMobile" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Admission Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIpdAdmDate" runat="server" Text='<%# Eval("AssignedDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Bed Details
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIPDBed" runat="server" Text='<%# Eval("BedDetails") %>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>



                                                        </Columns>

                                                        <HeaderStyle BackColor="#D8EBF5" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4"></div>
                                    <div class="col-md-8">
                                        <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                        <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                        runat="server">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="PDF"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnexport" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

