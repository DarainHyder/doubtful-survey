<%@ Page Title="Available Surveys" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Lab09.Surveyor_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Available Surveys</h2>

    <asp:GridView ID="gvAvailableSurveys" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-striped">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Survey Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <a href='TakeSurvey.aspx?SurveyID=<%# Eval("SurveyID") %>' class="btn btn-primary btn-sm">Take Survey</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="alert alert-info">No new surveys available for you at this time.</div>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
