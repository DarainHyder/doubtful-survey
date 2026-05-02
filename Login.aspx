<%@ Page Title="Login" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-4">
            <div class="card shadow rounded mt-5">
                <div class="card-body">
                    <h3 class="card-title text-center mb-4">Login</h3>
                    <div class="mb-3">
                        <label class="form-label">Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger mb-3 d-block"></asp:Label>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" />
                    
                    <hr />
                    <div class="mt-3 small text-muted">
                        <p class="mb-1"><strong>Test Credentials:</strong></p>
                        <ul class="list-unstyled mb-0">
                            <li><span class="badge bg-secondary">Builder</span> builder1 / Builder123</li>
                            <li><span class="badge bg-secondary">Surveyor</span> surveyor1 / Surveyor123</li>
                            <li><span class="badge bg-secondary">Admin</span> admin1 / Admin123</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
