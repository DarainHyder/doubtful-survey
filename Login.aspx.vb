' ===== Login.aspx.vb =====
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Configuration

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
        Dim conn As New SqlConnection(connString)
        Dim cmd As New SqlCommand()
        
        Try
            ' Using string concatenation as explicitly requested (security risk in production)
            cmd.CommandText = "SELECT UserID, Username, Role, IsAnonymous FROM Users WHERE Username='" & txtUsername.Text & "' AND Password='" & txtPassword.Text & "'"
            cmd.Connection = conn
            conn.Open()
            
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            
            If dr.Read() Then
                Session("UserID") = dr("UserID")
                Session("Role") = dr("Role")
                Session("IsAnonymous") = Convert.ToBoolean(dr("IsAnonymous"))
                
                FormsAuthentication.SetAuthCookie(txtUsername.Text, False)
                
                Dim role As String = dr("Role").ToString()
                If role = "SurveyBuilder" Then
                    Response.Redirect("~/Builder/Default.aspx")
                ElseIf role = "Surveyor" Then
                    Response.Redirect("~/Surveyor/Default.aspx")
                ElseIf role = "SurveyAdministrator" Then
                    Response.Redirect("~/Admin/Default.aspx")
                End If
            Else
                lblError.Text = "Invalid username or password."
            End If
            
        Catch ex As Exception
            lblError.Text = "Error: " & ex.Message
        Finally
            conn.Close()
        End Try
    End Sub
End Class
