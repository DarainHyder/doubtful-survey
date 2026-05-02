<%@ Page Title="Survey Results" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Results.aspx.vb" Inherits="Lab09.Admin_Results" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Survey Results Analysis</h2>

    <div class="card mb-4 shadow-sm">
        <div class="card-body">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <label class="form-label">Select Survey to View Results</label>
                    <asp:DropDownList ID="ddlSurveys" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlSurveys_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-6 text-md-end mt-3 mt-md-0">
                    <h4 class="mb-0">Total Responses: <asp:Label ID="lblTotalResponses" runat="server" Text="0" CssClass="badge bg-primary"></asp:Label></h4>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlResults" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-8">
                <h4 class="mb-3">Question breakdown</h4>
                <asp:Repeater ID="rptResults" runat="server" OnItemDataBound="rptResults_ItemDataBound">
                    <ItemTemplate>
                        <div class="card mb-3 shadow-sm">
                            <div class="card-body">
                                <h5 class="card-title text-primary"><%# Eval("QuestionText") %></h5>
                                <asp:HiddenField ID="hdnQuestionID" runat="server" Value='<%# Eval("QuestionID") %>' />
                                
                                <asp:Repeater ID="rptOptions" runat="server">
                                    <ItemTemplate>
                                        <div class="mb-2">
                                            <div class="d-flex justify-content-between small mb-1">
                                                <span><%# Eval("OptionText") %></span>
                                                <span><%# Eval("SelectionCount") %> votes (<%# Eval("Percentage", "{0:0.#}") %>%)</span>
                                            </div>
                                            <div class="progress" style="height: 10px;">
                                                <div class="progress-bar bg-success" role="progressbar" style='width: <%# Eval("Percentage") %>%;'></div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="col-md-4">
                <asp:Panel ID="pnlRespondents" runat="server" Visible="false">
                    <h4 class="mb-3">Respondents</h4>
                    <asp:GridView ID="gvRespondents" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-bordered bg-white shadow-sm">
                        <Columns>
                            <asp:BoundField DataField="Username" HeaderText="User" />
                            <asp:BoundField DataField="SubmittedAt" HeaderText="Date" DataFormatString="{0:MM/dd HH:mm}" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="text-muted small">No identified respondents.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="pnlAnonymousInfo" runat="server" Visible="false">
                    <div class="alert alert-info">Identities are hidden for this survey's responses.</div>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlNoData" runat="server" Visible="false" CssClass="alert alert-warning">
        No responses recorded for this survey yet.
    </asp:Panel>
</asp:Content>
