' ===== Builder/CreateSurvey.aspx.vb =====
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic

<Serializable>
Public Class QuestionViewModel
    Public Property QuestionText As String
    Public Property QuestionType As String
    Public Property Options As String()
End Class

Partial Class Builder_CreateSurvey
    Inherits System.Web.UI.Page

    Private Property QuestionsList As List(Of QuestionViewModel)
        Get
            If ViewState("QuestionsList") Is Nothing Then
                ViewState("QuestionsList") = New List(Of QuestionViewModel)()
            End If
            Return CType(ViewState("QuestionsList"), List(Of QuestionViewModel))
        End Get
        Set(ByVal value As List(Of QuestionViewModel))
            ViewState("QuestionsList") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Role") Is Nothing OrElse Session("Role").ToString() <> "SurveyBuilder" Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            BindQuestions()
        End If
    End Sub

    Protected Sub ddlQType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If ddlQType.SelectedValue = "TrueFalse" Then
            pnlOptions.Visible = False
            pnlTFOptions.Visible = True
        Else
            pnlOptions.Visible = True
            pnlTFOptions.Visible = False
        End If
    End Sub

    Protected Sub btnAddQ_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddQ.Click
        If String.IsNullOrEmpty(txtQText.Text) Then Return

        Dim q As New QuestionViewModel()
        q.QuestionText = txtQText.Text
        q.QuestionType = ddlQType.SelectedValue

        If q.QuestionType = "TrueFalse" Then
            q.Options = New String() {"True", "False"}
        Else
            Dim opts As New List(Of String)()
            If Not String.IsNullOrEmpty(txtOpt1.Text) Then opts.Add(txtOpt1.Text)
            If Not String.IsNullOrEmpty(txtOpt2.Text) Then opts.Add(txtOpt2.Text)
            If Not String.IsNullOrEmpty(txtOpt3.Text) Then opts.Add(txtOpt3.Text)
            if Not String.IsNullOrEmpty(txtOpt4.Text) Then opts.Add(txtOpt4.Text)
            q.Options = opts.ToArray()
        End If

        QuestionsList.Add(q)
        BindQuestions()

        ' Reset inputs
        txtQText.Text = ""
        txtOpt1.Text = ""
        txtOpt2.Text = ""
        txtOpt3.Text = ""
        txtOpt4.Text = ""
    End Sub

    Private Sub BindQuestions()
        rptQuestions.DataSource = QuestionsList
        rptQuestions.DataBind()
    End Sub

    Protected Sub btnSaveSurvey_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveSurvey.Click
        If String.IsNullOrEmpty(txtTitle.Text) OrElse QuestionsList.Count = 0 Then Return

        Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
        Dim conn As New SqlConnection(connString)
        Dim cmd As New SqlCommand()
        cmd.Connection = conn

        Try
            conn.Open()
            
            ' 1. Insert Survey
            cmd.CommandText = "INSERT INTO Surveys (Title, Description, CreatedBy) VALUES ('" & txtTitle.Text.Replace("'", "''") & "', '" & txtDesc.Text.Replace("'", "''") & "', " & Session("UserID") & ")"
            cmd.ExecuteNonQuery()
            
            ' 2. Get SurveyID
            cmd.CommandText = "SELECT SCOPE_IDENTITY()"
            Dim surveyID As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            
            ' 3. Insert Questions and Options
            Dim qOrder As Integer = 1
            For Each q As QuestionViewModel In QuestionsList
                cmd.CommandText = "INSERT INTO Questions (SurveyID, QuestionText, QuestionType, QuestionOrder) VALUES (" & surveyID & ", '" & q.QuestionText.Replace("'", "''") & "', '" & q.QuestionType & "', " & qOrder & ")"
                cmd.ExecuteNonQuery()
                
                cmd.CommandText = "SELECT SCOPE_IDENTITY()"
                Dim questionID As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                
                Dim optOrder As Integer = 1
                For Each opt As String In q.Options
                    cmd.CommandText = "INSERT INTO AnswerOptions (QuestionID, OptionText, OptionOrder) VALUES (" & questionID & ", '" & opt.Replace("'", "''") & "', " & optOrder & ")"
                    cmd.ExecuteNonQuery()
                    optOrder += 1
                Next
                qOrder += 1
            Next
            
            Response.Redirect("Default.aspx")
            
        Catch ex As Exception
            ' Handle error
        Finally
            conn.Close()
        End Try
    End Sub
End Class
