<%@ Page Title="Admin Dashboard" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Lab09.Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Administrator Dashboard</h2>

    <div class="row g-4">
        <div class="col-md-6">
            <div class="card h-100 shadow-sm border-primary">
                <div class="card-body text-center">
                    <h3 class="card-title">Manage Users</h3>
                    <p class="card-text text-muted">View all registered users and toggle their anonymity settings for surveys.</p>
                    <a href="ManageUsers.aspx" class="btn btn-primary px-4">Go to Users</a>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="card h-100 shadow-sm border-success">
                <div class="card-body text-center">
                    <h3 class="card-title">Survey Results</h3>
                    <p class="card-text text-muted">View detailed analytics, response counts, and respondent lists for all surveys.</p>
                    <a href="Results.aspx" class="btn btn-success px-4">View Results</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
