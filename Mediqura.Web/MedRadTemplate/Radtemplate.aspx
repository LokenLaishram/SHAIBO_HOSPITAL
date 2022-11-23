<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="Radtemplate.aspx.cs" Inherits="Mediqura.Web.MedRadTemplate.Radtemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxRichEdit.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRichEdit" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
</script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainer" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabbedassign" runat="server" CssClass="Tab2" HeaderText="Test List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div3">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Group</span>
                                        <asp:DropDownList runat="server" ID="ddl_group" AutoPostBack="true" OnSelectedIndexChanged="ddl_labgroup_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Sub-Group </span>
                                        <asp:DropDownList OnSelectedIndexChanged="ddl_labsubgroup_SelectedIndexChanged" AutoPostBack="true" runat="server" ID="ddl_labsubgroup" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Test Name</span>
                                        <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txt_testname_TextChanged" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_testname"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetLabServices" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_testname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnSearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" />
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
                                            <div class="grid" style="float: left; width: 100%; height: 55vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="GvLabService" runat="server" CssClass="table-hover grid_table result-table" OnRowDataBound="GvLabService_RowDataBound" OnRowCommand="gv_labtestlist_RowCommand" PageSize="10"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# (Container.DataItemIndex+1)+(GvLabService.PageIndex)*GvLabService.PageSize %>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Test Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                <asp:LinkButton ID="lbl_test" Font-Underline="true" CommandName="Search" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" runat="server" Style="text-align: left !important;" Text='<%# Eval("TestName") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Male
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_maleID" Visible="false" runat="server" Text='<%# Eval("MaleID")%>'></asp:Label>
                                                                <asp:CheckBox ID="chk_male" Enabled="false" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Female
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_femaleID" Visible="false" runat="server" Text='<%# Eval("FemaleID")%>'></asp:Label>
                                                                <asp:CheckBox ID="chk_female" Enabled="false" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Both
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_bothID" Visible="false" runat="server" Text='<%# Eval("BotheID")%>'></asp:Label>
                                                                <asp:CheckBox ID="chek_both" Enabled="false" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Rad Template">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div89">
                            <div class="row fixeddiv" id="div2" runat="server" style="padding-bottom: 2px; margin-bottom: 10px;">
                                <asp:Label ID="lbl_message2" runat="server"></asp:Label>
                            </div>
                            <div style="height: 100%; width: 100%">
                                <div id="pdfViewer" style="height: 100%; width: 100%;"></div>
                            </div>
                            <div class="row">
                                <div class="col-sm-10">
                                    <dx:ASPxRichEdit ID="Richteditor" runat="server" WorkDirectory="~\App_Data\WorkDirectory"></dx:ASPxRichEdit>
                                </div>
                                <div class="col-sm-2">
                                    <div class="row">
                                        <div class="col-sm-4"></div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ID="ddl_gender" AutoPostBack="true" OnSelectedIndexChanged="ddl_gender_SelectedIndexChanged1" class="form-control input-sm col-sm custextbox" ForeColor="Red">
                                                <asp:ListItem Value="0">--Gender--</asp:ListItem>
                                                <asp:ListItem Value="1"> Male </asp:ListItem>
                                                <asp:ListItem Value="2"> Female </asp:ListItem>
                                                <asp:ListItem Value="3"> Both </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lbl_testID" Visible="false" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-4"></div>
                                        <div class="col-sm-8" style="margin-top: 4px;">
                                            <asp:DropDownList runat="server" ID="ddl_templatetype" AutoPostBack="true" ForeColor="Red" OnSelectedIndexChanged="ddl_gender_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0">--Template--</asp:ListItem>
                                                <asp:ListItem Value="1"> Template - I </asp:ListItem>
                                                <asp:ListItem Value="2"> Template - II </asp:ListItem>
                                                <asp:ListItem Value="3"> Template - III </asp:ListItem>
                                                <asp:ListItem Value="4"> Template - IV </asp:ListItem>
                                                <asp:ListItem Value="5"> Template - V </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 4px">
                                        <div class="col-sm-5"></div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btn_save" runat="server" CssClass="btn btn-sm scusbtn" OnClick="btn_save_Click" Text="Save" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 4px">
                                        <div class="col-sm-5"></div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btn_print" runat="server" CssClass="btn btn-sm scusbtn" OnClick="btnprint_Click" Text="View" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 4px">
                                        <div class="col-sm-5"></div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btn_reset" CssClass="btn btn-sm scusbtn " runat="server" OnClick="btn_reset_Click" Text="Reset" />
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
