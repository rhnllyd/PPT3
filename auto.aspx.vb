Imports System.Net
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Net.Mail


Partial Class auto
    Inherits System.Web.UI.Page

    Protected Sub timestamp_TextChanged(sender As Object, e As EventArgs) Handles timestamp.TextChanged

        'jff  sfnsfsf  nfnsfl 
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim timenow As String = ""
        Dim kingstonforecast As String
        Dim mobayforecast As String
        Dim testdataread As String = "" 'temp  string remove after testing '
        timenow = System.DateTime.Now.ToShortTimeString
        
        timestamp.Text = timenow

        If (timenow.Equals("6:00 PM")) Then
            'getweathher  
            kingstonforecast = getweather("kingston")
            mobayforecast = getweather("montego bay")

            'call function to send email 
            testdataread = sendemails(kingstonforecast, "kingston")
            testdataread = sendemails(kingstonforecast, "mobay")


        End If





    End Sub

    'get weather function 
    Private Function getweather(ByVal location As String) As String

        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim weather As String
        Dim typeofday As String = "clear"


        Dim url As String = ""
        If (location.Equals("kingston")) Then
            url = "http://api.openweathermap.org/data/2.5/weather?q=Kingston,jm&APPID=569f89ee1f3380475e90b8a92ec064ce"
        Else
            url = "http://api.openweathermap.org/data/2.5/weather?q=Montego%20bay,jm&APPID=569f89ee1f3380475e90b8a92ec064ce"
        End If

        Try
            request = DirectCast(WebRequest.Create(url), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())
            weather = reader.ReadToEnd.ToString 'convert xml code to string 
        Catch ex As Exception
        End Try


        If weather.Contains("rain") OrElse weather.Contains("Rain") Then
            typeofday = "rainy"

        End If

        Return typeofday
    End Function

    Private Function sendemails(ByVal weatherforecast As String, ByVal location As String) As String
        Dim test As String = "did not read"
        Dim sqlConnection1 As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Empinfo.mdf;Integrated Security=True")
        Dim reader As SqlDataReader
        Dim cmd As New SqlCommand("getworkersbyloc", sqlConnection1)
        Dim message As String = ""

        cmd.CommandType = System.Data.CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@location", location)

        sqlConnection1.Open()
        reader = cmd.ExecuteReader()


        While (reader.Read())
            'email IT guys if rainy 
            If (weatherforecast.Equals("rainy") And reader("role").Equals("IT")) Then 'if its rainy and role is IT 
                message = " Its going to rain today stay in dont hit the street"
                sendmail(reader("email"), message)
            End If
            If (weatherforecast.Equals("rainy") And (reader("Role").ToString() <> "IT")) Then
                message = "Its going to be rainy you will only work 4 hours today and not the usual 8 hours"
                sendmail(reader("email"), message)
            End If

            If (weatherforecast.ToString <> "rainy") Then
                message = "Its sunny you are schduled for 8 hours of work tomorrow "
                sendmail(reader("email"), message)
            End If

        End While


        sqlConnection1.Close()
        Return test
    End Function



    Private Function sendmail(ByVal email As String, ByVal message1 As String) As String
        Dim SmtpServer As New SmtpClient
        Dim mail As New MailMessage()
        SmtpServer.UseDefaultCredentials = False
        SmtpServer.Credentials = New Net.NetworkCredential("mlnllydsmth@gmail.com", "Sign1@#$")
        SmtpServer.EnableSsl = True
        SmtpServer.Port = 587
        SmtpServer.Host = "smtp.gmail.com"
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network

        mail = New MailMessage
        mail.From = New MailAddress("mlnllydsmth@gmail.com")
        mail.To.Add(email)
        mail.Subject = "Work update"
        mail.Body = message1
        SmtpServer.Send(mail)

        Return 0
    End Function

End Class
