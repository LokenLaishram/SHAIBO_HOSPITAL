<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="BedStatus.aspx.cs" inherits="Mediqura.Web.MedHouseKeeping.BedStatus" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerblockmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabblockmaster" runat="server" CssClass="Tab2" HeaderText="Bed Status">
                    <contenttemplate>
                        <div class="custab-panel" id="ipdadmissiondetaildiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_block" class="input-group-addon cusspan" runat="server">Block</span>
                                        <asp:DropDownList ID="ddl_block" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_floor" class="input-group-addon cusspan" runat="server">Floor</span>
                                        <asp:DropDownList ID="ddl_floor" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_floor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_ward" class="input-group-addon cusspan" runat="server">Ward</span>
                                        <asp:DropDownList ID="ddl_ward" runat="server"  class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                              <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Bed Status</span>
                                        <asp:DropDownList ID="ddl_bedstatus" runat="server"  class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            <asp:ListItem Value="4">Under maintenance</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
     <%--                           <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Working Status</span>
                                        <asp:DropDownList ID="ddl_workstatus" runat="server" class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnSelectedIndexChanged="ddl_workstatus_SelectedIndexChanged" >
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Completed"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="col-lg-4"></div>
                                <div class="col-sm-8">
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
                                        <div class="gridview-container">
                                            <div class="pbody">
                                                <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvBedStatus" runat="server" CssClass="table-hover grid_table result-table" OnRowDataBound="GvBedStatus_RowDataBound"
                                                        EmptyDataText="No record found..."  AutoGenerateColumns="False"  OnRowCommand="GvBedStatus_RowCommand"
                                                        Width="100%" HorizontalAlign="Center" >
                                                        <Columns>
                                                            <asp:TemplateField >
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
                                                                    Block
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   <asp:Label ID="lblID" Style="text-align: center !important;" runat="server" Visible="false"
                                                                        Text='<%# Eval("BedID") %>'></asp:Label>
                                                                    <asp:Label ID="lblblock" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("Block") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Floor
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfloor" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("Floor1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Ward
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblward" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("Ward") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Room
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblroom" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("Room") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    BedNo
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbedno" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("BedNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Bed Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_status" runat="server" Visible="false" Style="text-align: center !important;" Text='<%#Eval("bed_status")%>'></asp:Label>
                                                                   <asp:Label ID="lblbedstatus" runat="server" Style="text-align: center !important;" Text='<%#Eval("bedstatus")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Status
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblworkstatus" runat="server"  Visible="false" Style="text-align: center !important;" Text='<%#Eval("WorkingStatusID")%>'></asp:Label>
                                                                      <asp:Button ID="btnstatus" runat="server" CommandName="statusupdate" ItemStyle-CssClass="gvbutton"
                                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# Eval("WorkingStatusID")%>' OnClientClick="javascript: return confirm('Are you sure to Change status ?');"/>
                                                                                                                     
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
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                    <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                        </contenttemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
