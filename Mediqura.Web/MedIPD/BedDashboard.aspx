<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="BedDashboard.aspx.cs" Inherits="Mediqura.Web.MedIPD.BedDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:TabContainer ID="tabcontainerlabgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabbedassign" runat="server" CssClass="Tab2" HeaderText="Bed Dashboard">
            <ContentTemplate>
                <asp:Panel ID="panel2" runat="server">
                    <div class="custab-panel" id="panelassignbed">
                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                            <ContentTemplate>
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                         <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Bed Status</span>
                                        <asp:DropDownList ID="ddl_bed_status" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Vacant</asp:ListItem>
                                            <asp:ListItem Value="2">Occupied</asp:ListItem>
                                             <asp:ListItem Value="3">Discharge process</asp:ListItem>
                                            <asp:ListItem Value="4">Under maintenance </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Block</span>
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_block" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddl_block" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Floor</span>
                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_floor" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_floor_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddl_floor" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Ward</span>
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_ward" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_ward_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddl_ward" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Room</span>
                                            <asp:TextBox runat="server" AutoPostBack="true" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_room"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                ServiceMethod="Getautoroomnos" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_room"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_room" ValidChars=" -0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"></asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnrearch_Click" />
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
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; height: 50vh; overflow: auto">
                                                <asp:Literal ID="BedDataliterals" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-sm-12" style="margin-top: 20px;">
                                    <div class="col-sm-3">
                                        <div class="info-box info-box-custom">
                                            <span class="info-box-icon bg-yellow info-box-custom custom-badge"><asp:Literal ID="litDischarge" runat="server">0</asp:Literal></span>
                                            <div class="info-box-content">
                                                <span class="info-box-text text-center">Discharge in process</span>

                                            </div>

                                        </div>

                                    </div>
                                    <div class="col-sm-3">
                                        <div class="info-box info-box-custom">
                                            <span class="info-box-icon bg-red info-box-custom custom-badge"><asp:Literal ID="litOccupied" runat="server">0</asp:Literal></span>
                                            <div class="info-box-content">
                                                <span class="info-box-text text-center">Bed occupied</span>

                                            </div>

                                        </div>

                                    </div>
                                    <div class="col-sm-3">
                                        <div class="info-box info-box-custom">
                                            <span class="info-box-icon bg-green info-box-custom custom-badge"><asp:Literal ID="litVacant" runat="server">0</asp:Literal></span>
                                            <div class="info-box-content">
                                                <span class="info-box-text text-center">Bed vacant</span>

                                            </div>

                                        </div>

                                    </div>
                                     <div class="col-sm-3">
                                        <div class="info-box info-box-custom">
                                            <span class="info-box-icon bg-blue info-box-custom custom-badge"><asp:Literal ID="litMantenence" runat="server">0</asp:Literal></span>
                                            <div class="info-box-content">
                                                <span class="info-box-text text-center">Under maintenance</span>

                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>


                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
