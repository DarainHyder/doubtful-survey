<%@ Page Title="Manage Users" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="ManageUsers.aspx.vb" Inherits="Lab09.Admin_ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Manage Users</h2>

    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-striped" OnRowCommand="gvUsers_RowCommand">
        <Columns>
            <asp:BoundField DataField="UserID" HeaderText="ID" />
            <asp:BoundField DataField="Username" HeaderText="Username" />
            <asp:BoundField DataField="Role" HeaderText="Role" />
            <asp:TemplateField HeaderText="Anonymous Responses">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAnon" runat="server" Checked='<%# Eval("IsAnonymous") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button ID="btnToggle" runat="server" Text="Toggle Anonymity" CommandName="ToggleAnon" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-sm btn-warning" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
