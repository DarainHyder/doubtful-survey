' ===== Surveyor/Default.aspx.vb =====
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Surveyor_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Role") Is Nothing OrElse Session("Role").ToString() <> "Surveyor" Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            BindAvailableSurveys()
        End If
    End Sub

    Private Sub BindAvailableSurveys()
        Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
        Dim conn As New SqlConnection(connString)
        Dim cmd As New SqlCommand()
        
        Try
            ' Using string concatenation as requested. 
            ' Show only active surveys that this user hasn't responded to yet.
            cmd.CommandText = "SELECT * FROM Surveys WHERE IsActive = 1 AND SurveyID NOT IN (SELECT SurveyID FROM SurveyResponses WHERE UserID = " & Session("UserID") & ")"
            cmd.Connection = conn
            conn.Open()
            
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            
            gvAvailableSurveys.DataSource = dt
            gvAvailableSurveys.DataBind()
            
        Catch ex As Exception
            ' Handle error
        Finally
            conn.Close()
        End Try
    End Sub
End Class
