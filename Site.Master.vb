' ===== Site.Master.vb =====
Imports System

Partial Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Role") IsNot Nothing Then
            Dim role As String = Session("Role").ToString()
            lnkLogout.Visible = True

            If role = "SurveyBuilder" Then
                phBuilderLinks.Visible = True
            ElseIf role = "Surveyor" Then
                phSurveyorLinks.Visible = True
            ElseIf role = "SurveyAdministrator" Then
                phAdminLinks.Visible = True
            End If
        Else
            lnkLogout.Visible = False
        End If
    End Sub
End Class
