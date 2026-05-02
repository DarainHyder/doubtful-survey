' ===== Builder/Default.aspx.vb =====
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Builder_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Role check
        If Session("Role") Is Nothing OrElse Session("Role").ToString() <> "SurveyBuilder" Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            BindSurveys()
        End If
    End Sub

    Private Sub BindSurveys()
        Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
        Dim conn As New SqlConnection(connString)
        Dim cmd As New SqlCommand()
        
        Try
            ' Using string concatenation as requested
            cmd.CommandText = "SELECT * FROM Surveys WHERE CreatedBy = " & Session("UserID") & " ORDER BY CreatedAt DESC"
            cmd.Connection = conn
            conn.Open()
            
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            
            gvSurveys.DataSource = dt
            gvSurveys.DataBind()
            
        Catch ex As Exception
            ' Handle error (maybe show in a label)
        Finally
            conn.Close()
        End Try
    End Sub
End Class
