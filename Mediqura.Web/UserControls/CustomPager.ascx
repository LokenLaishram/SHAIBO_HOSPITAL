<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomPager.ascx.cs" Inherits="Mediqura.Web.UserControls.CustomPager" %>
<div>
    <asp:Button ID="btnPrev" CssClass="button" runat="server" Text="Prev" OnCommand="Paging_Click" CommandName="Prev"
        CommandArgument="-1" CausesValidation="false" />
    <asp:Repeater ID="rptPage" runat="server" OnItemDataBound="rptPage_ItemdataBound">
        <ItemTemplate>
            <asp:LinkButton ID="lbtnPage" runat="server" OnCommand="Paging_Click" CommandName="PageNumber"
                CommandArgument="<%#Container.ItemIndex %>" CausesValidation="false"></asp:LinkButton>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Button ID="btnNext" CssClass="button" runat="server" Text="Next" OnCommand="Paging_Click" CommandName="Next"
        CommandArgument="1" CausesValidation="false" />
    <input type="hidden" id="origPageNumber" runat="server" value="0" />
    <input type="hidden" id="totalItems" class="totalItems" runat="server" />
    <input type="hidden" id="pageSize" class="pageSize" runat="server" />
</div>
