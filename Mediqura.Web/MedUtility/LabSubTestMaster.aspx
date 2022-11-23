<%@ Page Title="Lab Test Parameter" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LabSubTestMaster.aspx.cs" EnableEventValidation="false" Inherits="Mediqura.Web.MedUtility.LabSubTestMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerlabsubtestmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tablabsubtestpmaster" runat="server" CssClass="Tab2" HeaderText="Test Parameter Master">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server">
                            <div class="custab-panel" id="panellabsubtestmaster">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Group</span>
                                            <asp:DropDownList ID="ddl_labgroup" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_labgroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Sub-Group </span>
                                            <asp:DropDownList runat="server" ID="ddl_labsubgroup" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_labsubgroup_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Test <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" ID="txt_test" AutoPostBack="true" OnTextChanged="txt_test_TextChanged" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                ServiceMethod="GetServices" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_test"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Parameter </span>
                                            <asp:TextBox ID="txt_parameter" runat="server" placeholder="Searching ......." Font-Italic="true" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <asp:Button ID="btn_search" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btn_search_Click" />
                                            <asp:Button ID="btnadd" runat="server" Class="btn  btn-sm cusbtn" Text="Add New Row" OnClick="btnadd_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row cusrow pad-top ">
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="grid" style="float: left; width: 100%; height: 35vh; overflow: auto">
                                                            <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                                <ProgressTemplate>
                                                                    <div id="DIVloading" class="text-center loading" runat="server">
                                                                        <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <asp:GridView ID="GvLabSubTest" runat="server" CssClass="table-hover grid_table result-table"
                                                                EmptyDataText="No record found..." OnRowDataBound="GvLabSubTest_RowDataBound" OnRowCommand="gviplabsubtestlist_RowCommand" AutoGenerateColumns="False"
                                                                Width="100%" HorizontalAlign="Center">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Order
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ID" Visible="false" runat="server"
                                                                                Text='<%# Eval("ID") %>'></asp:Label>
                                                                            <asp:TextBox ID="txt_order" Style="text-align: left !important;" MaxLength="5" Width="30px" runat="server" Text='<%# Eval("OrderNo")%>'></asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender TargetControlID="txt_order" ID="FilteredTextBoxExtender4"
                                                                                runat="server" ValidChars="0123456789"
                                                                                Enabled="True">
                                                                            </asp:FilteredTextBoxExtender>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Parameter
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_subtest" Style="text-align: left !important;" runat="server" Width="150px" Text='<%# Eval("SubTestName")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Unit
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblunitID" Visible="false" runat="server"
                                                                                Text='<%# Eval("UnitID") %>'></asp:Label>
                                                                            <asp:DropDownList ID="ddl_unit" Width="65px" runat="server"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Sample
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sampletypeID" Visible="false" runat="server"
                                                                                Text='<%# Eval("SampleTypeID") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_groupdID" Visible="false" runat="server"
                                                                                Text='<%# Eval("LabGroupID") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_subgroupID" Visible="false" runat="server"
                                                                                Text='<%# Eval("LabSubGroupID") %>'></asp:Label>
                                                                            <asp:DropDownList ID="ddl_sample" Width="65px" runat="server"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Reagent
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_reagenttypeID" Visible="false" runat="server"
                                                                                Text='<%# Eval("ReagentTypeID") %>'></asp:Label>
                                                                            <asp:DropDownList ID="ddl_reagent" Width="65px" runat="server"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Method
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmethodID" Visible="false" runat="server"
                                                                                Text='<%# Eval("MethodID") %>'></asp:Label>
                                                                            <asp:DropDownList ID="ddl_method" Width="65px" runat="server"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Container
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcontainerID" Visible="false" runat="server"
                                                                                Text='<%# Eval("ContainerID") %>'></asp:Label>
                                                                            <asp:DropDownList ID="ddl_containerID" Width="65px" runat="server"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Age (F)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_age" MaxLength="4" Style="text-align: left !important;" Width="35px" Text='<%# Eval("AgeRangeFrom")%>' runat="server"></asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender TargetControlID="txt_age" ID="FilteredTextBoxExtender2"
                                                                                runat="server" ValidChars="0123456789DMYH"
                                                                                Enabled="True">
                                                                            </asp:FilteredTextBoxExtender>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Age (T)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_ageto" MaxLength="4" Style="text-align: left !important;" Width="35px" Text='<%# Eval("AgeRangeTo")%>' runat="server"></asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender TargetControlID="txt_ageto" ID="FilteredTextBoxExtender3"
                                                                                runat="server" ValidChars="0123456789DMYH"
                                                                                Enabled="True">
                                                                            </asp:FilteredTextBoxExtender>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            M-Nr  (F)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_normalrangemale" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeMaleFrom")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            M-Nr  (T)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_normalrangemaleto" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeMaleTo")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            F-Nr  (F)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_normalrangefemale" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeFeMaleFrom")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            F-Nr  (T)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_normalrangefemaleto" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeFeMaleTo")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <HeaderTemplate>
                                                                            Tm-Nr  (F)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_normalrangetransemalefrom" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeTransMaleFrom")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <HeaderTemplate>
                                                                            Tm-Nr  (T)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_normalrangetransemaleto" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeTransMaleTo")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <HeaderTemplate>
                                                                            Tf-Nr  (F)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_normalrangetransefemalefrom" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeTransFeMaleFrom")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <HeaderTemplate>
                                                                            Tf-Nr  (T)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_normalrangetransefemaleto" Style="text-align: left !important;" Width="50px" runat="server" Text='<%# Eval("NormalRangeTransFeMaleTo")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Range Wordings
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_Rangewording" Height="25px" TextMode="MultiLine" runat="server" Text='<%# Eval("RangeWording")%>' class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Type
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_rowtypeID" Visible="false" runat="server"
                                                                                Text='<%# Eval("RowTypeID") %>'></asp:Label>
                                                                            <asp:DropDownList ID="ddlrowtype" Width="70px" runat="server"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_defaultID" Visible="false" runat="server"
                                                                                Text='<%# Eval("defaultValue") %>'></asp:Label>
                                                                            <asp:CheckBox ID="chekbox" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                                OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                    <i class="fa fa-trash-o cus-delete-color"></i>   </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                                <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                            </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12">
                                    <div>
                                        <asp:TextBox ID="txt_Reamrks" placeholder="Comments......" Height="70px" TextMode="MultiLine" runat="server" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Report Template</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_template">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Machine Name<span
                                            style="color: red">* </span></span>
                                        <asp:DropDownList runat="server" ID="ddl_machine" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Save" OnClick="Bulk_Update" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-8">
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                    <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                    runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
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
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
