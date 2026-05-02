' ===== Admin/ManageUsers.aspx.vb =====
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Admin_ManageUsers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Role") Is Nothing OrElse Session("Role").ToString() <> "SurveyAdministrator" Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            BindUsers()
        End If
    End Sub

    Private Sub BindUsers()
        Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
        Dim conn As New SqlConnection(connString)
        
        Try
            Dim cmd As New SqlCommand("SELECT UserID, Username, Role, IsAnonymous FROM Users", conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            gvUsers.DataSource = dt
            gvUsers.DataBind()
        Catch ex As Exception
            ' Handle error
        End Try
    End Sub

    Protected Sub gvUsers_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "ToggleAnon" Then
            Dim userID As String = e.CommandArgument.ToString()
            Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
            
            Using conn As New SqlConnection(connString)
                ' Using string concatenation as requested
                Dim cmd As New SqlCommand("UPDATE Users SET IsAnonymous = CASE WHEN IsAnonymous = 1 THEN 0 ELSE 1 END WHERE UserID = " & userID, conn)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
            
            BindUsers()
        End If
    End Sub
End Class
