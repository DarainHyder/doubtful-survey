<%@ Page Title="Take Survey" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="TakeSurvey.aspx.vb" Inherits="Lab09.Surveyor_TakeSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card shadow rounded">
                <div class="card-body">
                    <h2 class="card-title"><asp:Literal ID="litTitle" runat="server"></asp:Literal></h2>
                    <p class="text-muted"><asp:Literal ID="litDesc" runat="server"></asp:Literal></p>
                    <hr />

                    <asp:Repeater ID="rptSurveyQuestions" runat="server" OnItemDataBound="rptSurveyQuestions_ItemDataBound">
                        <ItemTemplate>
                            <div class="mb-4">
                                <h5><%# Container.ItemIndex + 1 %>. <%# Eval("QuestionText") %></h5>
                                <asp:HiddenField ID="hdnQuestionID" runat="server" Value='<%# Eval("QuestionID") %>' />
                                <asp:RadioButtonList ID="rblOptions" runat="server" CssClass="ms-3 mt-2" DataTextField="OptionText" DataValueField="OptionID">
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="rblOptions" ErrorMessage="Please select an option" CssClass="text-danger small ms-3" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="text-center mt-4">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit Survey" CssClass="btn btn-primary btn-lg" />
                        <a href="Default.aspx" class="btn btn-secondary btn-lg ms-2">Back</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
