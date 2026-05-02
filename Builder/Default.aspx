<%@ Page Title="My Surveys" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Lab09.Builder_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>My Surveys</h2>
        <a href="CreateSurvey.aspx" class="btn btn-success">Create New Survey</a>
    </div>

    <asp:GridView ID="gvSurveys" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-striped">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="CreatedAt" HeaderText="Created Date" DataFormatString="{0:MMM dd, yyyy}" />
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <span class='<%# If(Eval("IsActive"), "badge bg-success", "badge bg-secondary") %>'>
                        <%# If(Eval("IsActive"), "Active", "Inactive") %>
                    </span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <a href='../Admin/Results.aspx?SurveyID=<%# Eval("SurveyID") %>' class="btn btn-sm btn-info text-white">View Results</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="alert alert-info">You haven't created any surveys yet.</div>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
