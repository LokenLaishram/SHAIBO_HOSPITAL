<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="PasswordChange.aspx.cs" Inherits="Mediqura.Web.PasswordChange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <link rel="stylesheet" type="text/css" href="style.css" />		
	<script src="script.js"></script>		
      <script type="text/javascript">
     
          function disableCntrls(e){                
              if(e.keyCode==17 || e.keyCode==93){                   
                  alert('Copy Paste not allowed.');
                  return false;                    
              }
          }
 
          function disContextMenu(e)
          {
              $(e).bind("contextmenu",function(e)
              {
                  return false;
              });
          }

          /*
	jQuery document ready.
*/
          $(document).ready(function () {
              /*
                  assigning keyup event to password field
                  so everytime user type code will execute
              */

              $('#txt_newPswd').keyup(function () {
                  $('#result').html(checkStrength($('#txt_newPswd').val()))
              })

              /*
                  checkStrength is function which will do the 
                  main password strength checking for us
              */

              function checkStrength(txt_newPswd) {
                  //initial strength
                  var strength = 0

                  //if the password length is less than 6, return message.
                  if (txt_newPswd.length < 5) {
                      $('#result').removeClass()
                      $('#result').addClass('short')
                      return 'Too short'
                  }

                  //length is ok, lets continue.

                  //if length is 8 characters or more, increase strength value
                  if (txt_newPswd.length > 7) strength += 1

                  //if password contains both lower and uppercase characters, increase strength value
                  if (txt_newPswd.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) strength += 1

                  //if it has numbers and characters, increase strength value
                  if (txt_newPswd.match(/([a-zA-Z])/) && password.match(/([0-9])/)) strength += 1

                  //if it has one special character, increase strength value
                  if (txt_newPswd.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1

                  //if it has two special characters, increase strength value
                  if (txt_newPswd.match(/(.*[!,%,&,@,#,$,^,*,?,_,~].*[!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1

                  //now we have calculated strength value, we can return messages

                  //if value is less than 2
                  if (strength < 2) {
                      $('#result').removeClass()
                      $('#result').addClass('weak')
                      return 'Weak'
                  }
                  else if (strength == 2) {
                      $('#result').removeClass()
                      $('#result').addClass('good')
                      return 'Good'
                  }
                  else {
                      $('#result').removeClass()
                      $('#result').addClass('strong')
                      return 'Strong'
                  }
              }
          });

          
      </script>

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerlabgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tablabgroupmaster" runat="server" CssClass="Tab2" HeaderText="Change Password">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server">
                            <div class="custab-panel" id="panellsamplemaster">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Username</span>

                                            <asp:TextBox runat="server" required="" ID="txtusername" autocomplete="off" placeholder="User Name"
                                                Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="True" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Old Password</span>
                                           
                                            <asp:TextBox runat="server" required="" ID="txt_Oldpassword" autocomplete="off" TextMode="Password" placeholder="Old Password"
                                                Class="form-control input-sm col-sm custextbox" Style="z-index: 3"  AutoPostBack="True" ></asp:TextBox>
                                               
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">New Password</span>
                                            <asp:TextBox runat="server" required="" ID="txt_newPswd" autocomplete="off" TextMode="Password" placeholder="New Password"
                                                Class="form-control input-sm col-sm custextbox" Style="z-index: 3" 
                                                onkeydown="return disableCntrls(event);" onmousedown="disContextMenu(this);" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Confirmed Password</span>
                                            <asp:TextBox runat="server" required="" ID="txt_confirmed" autocomplete="off" TextMode="Password" placeholder="Confirmed Password"
                                                Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button runat="server" Style="background-color: #31e9bb;" ID="btnresetPwd" Text="Save Password" class="btn  loginbtn btn-sm" OnClick="btnresetPwd_Click" />
                                            <asp:Button runat="server" Style="background-color: #31e9bb;" ID="btn_reset" Text="Reset" class="btn  loginbtn btn-sm" OnClick="btn_reset_Click"/>
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
