' ===== Admin/Results.aspx.vb =====
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic

Partial Class Admin_Results
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Role") Is Nothing OrElse Session("Role").ToString() <> "SurveyAdministrator" Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            LoadSurveysList()
            If Request.QueryString("SurveyID") IsNot Nothing Then
                ddlSurveys.SelectedValue = Request.QueryString("SurveyID")
                ShowResults()
            End If
        End If
    End Sub

    Private Sub LoadSurveysList()
        Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("SELECT SurveyID, Title FROM Surveys", conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            ddlSurveys.DataSource = dt
            ddlSurveys.DataTextField = "Title"
            ddlSurveys.DataValueField = "SurveyID"
            ddlSurveys.DataBind()
            ddlSurveys.Items.Insert(0, New ListItem("-- Select a Survey --", "0"))
        End Using
    End Sub

    Protected Sub ddlSurveys_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ShowResults()
    End Sub

    Private Sub ShowResults()
        Dim selectedID As String = ddlSurveys.SelectedValue
        If selectedID = "0" Then
            pnlResults.Visible = False
            Return
        End If

        Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
        Using conn As New SqlConnection(connString)
            conn.Open()

            ' 1. Get total responses
            Dim cmdCount As New SqlCommand("SELECT COUNT(*) FROM SurveyResponses WHERE SurveyID = " & selectedID, conn)
            Dim totalResponses As Integer = Convert.ToInt32(cmdCount.ExecuteScalar())
            lblTotalResponses.Text = totalResponses.ToString()

            If totalResponses = 0 Then
                pnlResults.Visible = False
                pnlNoData.Visible = True
                Return
            End If

            pnlResults.Visible = True
            pnlNoData.Visible = False

            ' 2. Load Questions
            Dim cmdQ As New SqlCommand("SELECT QuestionID, QuestionText FROM Questions WHERE SurveyID = " & selectedID & " ORDER BY QuestionOrder", conn)
            Dim daQ As New SqlDataAdapter(cmdQ)
            Dim dtQ As New DataTable()
            daQ.Fill(dtQ)
            rptResults.DataSource = dtQ
            rptResults.DataBind()

            ' 3. Respondents (if not anonymous)
            ' Note: The requirement says if survey is NOT set to anonymous responses. 
            ' We check if the surveyor was anonymous when responding.
            Dim cmdRespondents As New SqlCommand("SELECT u.Username, sr.SubmittedAt FROM SurveyResponses sr JOIN Users u ON sr.UserID = u.UserID WHERE sr.SurveyID = " & selectedID & " AND sr.UserID IS NOT NULL", conn)
            Dim daR As New SqlDataAdapter(cmdRespondents)
            Dim dtR As New DataTable()
            daR.Fill(dtR)
            
            If dtR.Rows.Count > 0 Then
                pnlRespondents.Visible = True
                pnlAnonymousInfo.Visible = False
                gvRespondents.DataSource = dtR
                gvRespondents.DataBind()
            Else
                pnlRespondents.Visible = False
                pnlAnonymousInfo.Visible = True
            End If
        End Using
    End Sub

    Protected Sub rptResults_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hdnQ As HiddenField = CType(e.Item.FindControl("hdnQuestionID"), HiddenField)
            Dim rptOpts As Repeater = CType(e.Item.FindControl("rptOptions"), Repeater)
            Dim totalResponses As Integer = Integer.Parse(lblTotalResponses.Text)

            Dim connString As String = ConfigurationManager.ConnectionStrings("SurveyDB").ConnectionString
            Using conn As New SqlConnection(connString)
                ' Complex query to get option text and counts
                Dim sql As String = "SELECT ao.OptionText, COUNT(ra.AnswerID) AS SelectionCount " & _
                                  "FROM AnswerOptions ao LEFT JOIN ResponseAnswers ra ON ao.OptionID = ra.SelectedOptionID " & _
                                  "WHERE ao.QuestionID = " & hdnQ.Value & " " & _
                                  "GROUP BY ao.OptionText, ao.OptionOrder ORDER BY ao.OptionOrder"
                
                Dim da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                ' Add percentage column
                dt.Columns.Add("Percentage", GetType(Double))
                For Each row As DataRow In dt.Rows
                    Dim count As Integer = Convert.ToInt32(row("SelectionCount"))
                    row("Percentage") = If(totalResponses = 0, 0, (count / totalResponses) * 100)
                Next

                rptOpts.DataSource = dt
                rptOpts.DataBind()
            End Using
        End If
    End Sub
End Class
