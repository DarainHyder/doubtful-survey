' ===== Admin/Default.aspx.vb =====
Partial Class Admin_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Role") Is Nothing OrElse Session("Role").ToString() <> "SurveyAdministrator" Then
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
End Class
