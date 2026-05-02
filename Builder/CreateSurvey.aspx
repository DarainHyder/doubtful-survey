<%@ Page Title="Create Survey" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="CreateSurvey.aspx.vb" Inherits="Lab09.Builder_CreateSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div class="row justify-content-center">
        <div class="col-md-10">
            <div class="card shadow rounded mb-4">
                <div class="card-header bg-primary text-white">
                    <h3 class="card-title mb-0">Create New Survey</h3>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label font-weight-bold">Survey Title</label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Enter survey title"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Description</label>
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter survey description"></asp:TextBox>
                    </div>

                    <hr />

                    <asp:UpdatePanel ID="upQuestions" runat="server">
                        <ContentTemplate>
                            <h4>Questions</h4>
                            <asp:Repeater ID="rptQuestions" runat="server">
                                <ItemTemplate>
                                    <div class="card mb-2 border-info">
                                        <div class="card-body py-2">
                                            <strong>#<%# Container.ItemIndex + 1 %>:</strong> <%# Eval("QuestionText") %> 
                                            <span class="badge bg-info float-end"><%# Eval("QuestionType") %></span>
                                            <div class="mt-1 small text-muted">
                                                Options: <%# String.Join(", ", CType(Eval("Options"), String())) %>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <div class="card mt-4 border-primary">
                                <div class="card-body bg-light">
                                    <h5 class="card-title">Add Question</h5>
                                    <div class="mb-2">
                                        <label class="form-label">Question Text</label>
                                        <asp:TextBox ID="txtQText" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="mb-2">
                                        <label class="form-label">Question Type</label>
                                        <asp:DropDownList ID="ddlQType" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlQType_SelectedIndexChanged">
                                            <asp:ListItem Value="MultipleChoice">Multiple Choice</asp:ListItem>
                                            <asp:ListItem Value="TrueFalse">True/False</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <asp:Panel ID="pnlOptions" runat="server">
                                        <label class="form-label">Options</label>
                                        <div class="row g-2">
                                            <div class="col-6"><asp:TextBox ID="txtOpt1" runat="server" CssClass="form-control" placeholder="Option 1"></asp:TextBox></div>
                                            <div class="col-6"><asp:TextBox ID="txtOpt2" runat="server" CssClass="form-control" placeholder="Option 2"></asp:TextBox></div>
                                            <div class="col-6"><asp:TextBox ID="txtOpt3" runat="server" CssClass="form-control" placeholder="Option 3"></asp:TextBox></div>
                                            <div class="col-6"><asp:TextBox ID="txtOpt4" runat="server" CssClass="form-control" placeholder="Option 4"></asp:TextBox></div>
                                        </div>
                                    </asp:Panel>
                                    
                                    <asp:Panel ID="pnlTFOptions" runat="server" Visible="false">
                                        <div class="alert alert-secondary py-1 px-2">Options will be automatically set to "True" and "False".</div>
                                    </asp:Panel>

                                    <asp:Button ID="btnAddQ" runat="server" Text="Add This Question" CssClass="btn btn-primary mt-3" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="mt-4 text-center">
                        <asp:Button ID="btnSaveSurvey" runat="server" Text="Save Survey" CssClass="btn btn-success btn-lg px-5" />
                        <a href="Default.aspx" class="btn btn-secondary btn-lg ms-2">Cancel</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
