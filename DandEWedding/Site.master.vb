Imports System.Collections.Generic
Imports System.Security.Claims
Imports System.Security.Principal
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Net.Mail

Public Partial Class SiteMaster
    Inherits MasterPage
    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String

    Protected Sub Page_Init(sender As Object, e As EventArgs)
        ' The code below helps to protect against XSRF attacks
        Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
            ' Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With {
                .HttpOnly = True,
                .Value = _antiXsrfTokenValue
            }
            If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
                responseCookie.Secure = True
            End If
            Response.Cookies.[Set](responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            ' Set Anti-XSRF token
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)
        Else
            ' Validate the Anti-XSRF token
            If DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) Then
                Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs)

    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        Context.GetOwinContext().Authentication.SignOut()
    End Sub

    Private Sub sendBtn_Click(sender As Object, e As EventArgs) Handles sendBtn.Click
        If nameTxt.Text <> "" Then
            Dim name As String = nameTxt.Text
            Dim rsvp As Integer = rsvpRB.SelectedValue
            Dim diet As String = dietTxt.Text
            Dim song As String = songTxt.Text

            Dim emailStr As String = "<center><h2>Wedding RSVP Received</h2></center>"
            emailStr += "<br/>"
            emailStr += "<p><b>RSVP for:</b></p>"
            emailStr += "<p>" & name & "</p><br/>"

            If diet <> "" Then
                emailStr += "<p>Dietary Requirements: " & diet & "</p><br/>"
            End If

            If song <> "" Then
                emailStr += "<p>Song Request: " & song & "</p><br/>"
            End If

            If name2Txt.Text <> "" Then
                emailStr += "<br/>"
                Dim name2 As String = name2Txt.Text
                Dim diet2 As String = diet2Txt.Text
                Dim song2 As String = song2Txt.Text
                emailStr += "<p>" & name2 & "</p><br/>"

                If diet2 <> "" Then
                    emailStr += "<p>Dietary Requirements: " & diet2 & "</p><br/>"
                End If

                If song2 <> "" Then
                    emailStr += "<p>Song Request: " & song2 & "</p><br/>"
                End If
            End If

            If name3Txt.Text <> "" Then
                emailStr += "<br/>"
                Dim name3 As String = name3Txt.Text
                Dim diet3 As String = diet3Txt.Text
                Dim song3 As String = song3Txt.Text
                emailStr += "<p>" & name3 & "</p><br/>"

                If diet3 <> "" Then
                    emailStr += "<p>Dietary Requirements: " & diet3 & "</p><br/>"
                End If

                If song3 <> "" Then
                    emailStr += "<p>Song Request: " & song3 & "</p><br/>"
                End If
            End If

            If name4Txt.Text <> "" Then
                emailStr += "<br/>"
                Dim name4 As String = name4Txt.Text
                Dim diet4 As String = diet4Txt.Text
                Dim song4 As String = song4Txt.Text
                emailStr += "<p>" & name4 & "</p><br/>"

                If diet4 <> "" Then
                    emailStr += "<p>Dietary Requirements: " & diet4 & "</p><br/>"
                End If

                If song4 <> "" Then
                    emailStr += "<p>Song Request: " & song4 & "</p><br/>"
                End If
            End If

            If name5Txt.Text <> "" Then
                emailStr += "<br/>"
                Dim name5 As String = name5Txt.Text
                Dim diet5 As String = diet5Txt.Text
                Dim song5 As String = song5Txt.Text
                emailStr += "<p>" & name5 & "</p><br/>"

                If diet5 <> "" Then
                    emailStr += "<p>Dietary Requirements: " & diet5 & "</p><br/>"
                End If

                If song5 <> "" Then
                    emailStr += "<p>Song Request: " & song5 & "</p><br/>"
                End If
            End If

            If name6Txt.Text <> "" Then
                emailStr += "<br/>"
                Dim name6 As String = name6Txt.Text
                Dim diet6 As String = diet6Txt.Text
                Dim song6 As String = song6Txt.Text
                emailStr += "<p>" & name6 & "</p><br/>"

                If diet6 <> "" Then
                    emailStr += "<p>Dietary Requirements: " & diet6 & "</p><br/>"
                End If

                If song6 <> "" Then
                    emailStr += "<p>Song Request: " & song6 & "</p><br/>"
                End If
            End If

            If name7Txt.Text <> "" Then
                emailStr += "<br/>"
                Dim name7 As String = name7Txt.Text
                Dim diet7 As String = diet7Txt.Text
                Dim song7 As String = song7Txt.Text
                emailStr += "<p>" & name7 & "</p><br/>"

                If diet7 <> "" Then
                    emailStr += "<p>Dietary Requirements: " & diet7 & "</p><br/>"
                End If

                If song7 <> "" Then
                    emailStr += "<p>Song Request: " & song7 & "</p><br/>"
                End If
            End If

            emailStr += "<br/><br/>"

            If rsvp = 1 Then
                emailStr += "<p>RSVP: <b>Attending</b><p>"
            Else
                emailStr += "<p>RSVP: <b>Not Attending</b><p>"
            End If

            Dim client As New SmtpClient
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.EnableSsl = True
            client.Host = "smtp.gmail.com"
            client.Port = 587

            Dim credentials As System.Net.NetworkCredential = New System.Net.NetworkCredential("fineanddande2017@gmail.com", "13Broadview")
            client.UseDefaultCredentials = False
            client.Credentials = credentials

            Dim msg As MailMessage = New MailMessage()
            msg.From = New MailAddress("fineanddande2017@gmail.com")
            msg.To.Add(New MailAddress("fineanddande2017@gmail.com"))

            msg.Subject = "Wedding RSVP"
            msg.IsBodyHtml = True
            msg.Body = emailStr

            Try
                client.Send(msg)
                errorLbl.Text = "Your RSVP has been sent. Thank you!"
                errorLbl.ForeColor = Drawing.Color.Green
                errorLbl.Visible = True
            Catch ex As Exception
                errorLbl.Text = "Failed to send RSVP. Please try again later."
                errorLbl.ForeColor = Drawing.Color.Red
                errorLbl.Visible = True
            End Try
        Else
            errorLbl.Text = "Please enter the name(s) specified on your invitation."
            errorLbl.ForeColor = Drawing.Color.Red
            errorLbl.Visible = True
        End If
    End Sub
End Class
