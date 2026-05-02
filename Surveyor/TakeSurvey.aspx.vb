' ===== Surveyor/TakeSurvey.aspx.vb =====
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Surveyor_TakeSurvey
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Role") Is Nothing OrElse Session("Role").ToString() <> "Surveyor" Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            LoadSurvey()
        End If
    End Sub

    Private Sub LoadSurvey()
        Dim surveyID As String = Request.QueryString("SurveyID")
        If String.IsNullOrEmpty(surveyID) Then Response.Redirect("Default.aspx")

        Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
        Dim conn As New SqlConnection(connString)
        
        Try
            conn.Open()
            
            ' Load Survey Details
            Dim cmdSurvey As New SqlCommand("SELECT * FROM Surveys WHERE SurveyID = " & surveyID, conn)
            Dim drSurvey As SqlDataReader = cmdSurvey.ExecuteReader()
            If drSurvey.Read() Then
                litTitle.Text = drSurvey("Title").ToString()
                litDesc.Text = drSurvey("Description").ToString()
            End If
            drSurvey.Close()

            ' Load Questions
            Dim cmdQ As New SqlCommand("SELECT * FROM Questions WHERE SurveyID = " & surveyID & " ORDER BY QuestionOrder", conn)
            Dim da As New SqlDataAdapter(cmdQ)
            Dim dtQ As New DataTable()
            da.Fill(dtQ)
            
            rptSurveyQuestions.DataSource = dtQ
            rptSurveyQuestions.DataBind()
            
        Catch ex As Exception
            ' Handle error
        Finally
            conn.Close()
        End Try
    End Sub

    Protected Sub rptSurveyQuestions_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim questionID As String = drv("QuestionID").ToString()
            Dim rbl As RadioButtonList = CType(e.Item.FindControl("rblOptions"), RadioButtonList)

            Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
            Using conn As New SqlConnection(connString)
                Dim cmd As New SqlCommand("SELECT * FROM AnswerOptions WHERE QuestionID = " & questionID & " ORDER BY OptionOrder", conn)
                Dim da As New SqlDataAdapter(cmd)
                Dim dtOpts As New DataTable()
                da.Fill(dtOpts)
                rbl.DataSource = dtOpts
                rbl.DataBind()
            End Using
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Dim surveyID As String = Request.QueryString("SurveyID")
        Dim userID As String = If(Convert.ToBoolean(Session("IsAnonymous")), "NULL", Session("UserID").ToString())

        Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
        Dim conn As New SqlConnection(connString)
        
        Try
            conn.Open()
            
            ' 1. Create Response
            Dim cmdResp As New SqlCommand("INSERT INTO SurveyResponses (SurveyID, UserID) VALUES (" & surveyID & ", " & userID & ")", conn)
            cmdResp.ExecuteNonQuery()
            
            Dim cmdGetID As New SqlCommand("SELECT SCOPE_IDENTITY()", conn)
            Dim responseID As Integer = Convert.ToInt32(cmdGetID.ExecuteScalar())
            
            ' 2. Save Answers
            For Each item As RepeaterItem In rptSurveyQuestions.Items
                Dim hdnQ As HiddenField = CType(item.FindControl("hdnQuestionID"), HiddenField)
                Dim rbl As RadioButtonList = CType(item.FindControl("rblOptions"), RadioButtonList)
                
                If rbl.SelectedValue <> "" Then
                    Dim cmdAns As New SqlCommand("INSERT INTO ResponseAnswers (ResponseID, QuestionID, SelectedOptionID) VALUES (" & responseID & ", " & hdnQ.Value & ", " & rbl.SelectedValue & ")", conn)
                    cmdAns.ExecuteNonQuery()
                End If
            Next
            
            Response.Redirect("Default.aspx")
            
        Catch ex As Exception
            ' Handle error
        Finally
            conn.Close()
        End Try
    End Sub
End Class
