<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Mediqura.Web.MedUtility.ChangePassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
         <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerlabgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tablabgroupmaster" runat="server" CssClass="Tab2" HeaderText="Change Password">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server">
                            <div class="custab-panel" id="panellsamplemaster">
                              
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                           <span id="Span1" class="input-group-addon cusspan" runat="server">Username</span>
                                            <asp:TextBox runat="server" required="" ID="txtusername" autocomplete="off" placeholder="User Name"
                                                Class="form-control input-sm col-sm custextbox" Style="z-index: 3"></asp:TextBox>
                                          </div>
                                    </div>
                                 </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Old Password</span>
                                            <asp:TextBox runat="server" required="" ID="txt_Oldpassword" autocomplete="off" TextMode="Password" placeholder="Old Password"
                                                Class="form-control input-sm col-sm custextbox" Style="z-index: 3"></asp:TextBox>
                                          </div>
                                    </div>
                                 </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">New Password</span>
                                            <asp:TextBox runat="server" required="" ID="txt_newPswd" autocomplete="off" TextMode="Password" placeholder="New Password"
                                                Class="form-control input-sm col-sm custextbox" Style="z-index: 3"></asp:TextBox>
                                          </div>
                                    </div>
                                 </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                           <span id="Span5" class="input-group-addon cusspan" runat="server">Confirmed Password</span>
                                            <asp:TextBox runat="server" required="" ID="txt_confirmed" autocomplete="off" TextMode="Password" placeholder="Confirmed Password"
                                                Class="form-control input-sm col-sm custextbox" Style="z-index: 3"></asp:TextBox>
                                          </div>
                                    </div>
                                 </div>
                                 <div class="row">
                                
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                             <asp:Button runat="server" Style="background-color: #31e9bb;" ID="btnresetPwd" Text="Change Password" class="btn  loginbtn btn-sm"/>
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
