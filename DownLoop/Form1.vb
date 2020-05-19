Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Text

Public Class Form1



    <DllImport("wininet.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function InternetGetCookieEx(ByVal pchURL As String, ByVal pchCookieName As String, ByVal pchCookieData As StringBuilder, ByRef pcchCookieData As UInteger, ByVal dwFlags As Integer, ByVal lpReserved As IntPtr) As Boolean
    End Function

    Const INTERNET_COOKIE_HTTPONLY As Integer = &H2000

    Public Shared Function GetGlobalCookies(ByVal uri As String) As String

        Dim datasize As UInteger = 1024
        Dim cookieData As StringBuilder = New StringBuilder(CInt(datasize))

        If InternetGetCookieEx(uri, Nothing, cookieData, datasize, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero) AndAlso cookieData.Length > 0 Then
            Return cookieData.ToString().Replace(";"c, ","c)
        Else
            Return ""
        End If
    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        oWB.Navigate("www.google.com")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        oWB.Navigate("www.simulaimport.com.br/Teste/Arquivo.zip")
    End Sub

    Private Sub oWB_Navigating(sender As Object, e As WebBrowserNavigatingEventArgs) Handles oWB.Navigating



        'If e.Url.Segments(e.Url.Segments.Length - 1).EndsWith(".zip") Then
        '
        '    e.Cancel = True
        '    read = DownloadFile(e.Url.ToString, "c:\temp\arq2.zip")
        '
        'End If


        If e.Url.Segments(e.Url.Segments.Length - 1).EndsWith(".zip") Then
            e.Cancel = True

            Dim arquivo1 As String = ""
            Dim oWC As New WebClient

            'adicionado, caso tenha autennticação...
            If Not oWB.Document Is Nothing Then
                oWC.Headers.Add(HttpRequestHeader.Cookie, GetGlobalCookies(e.Url.AbsoluteUri))
            End If

            oWC.DownloadFile(e.Url, "c:\temp\arq.zip")

        End If
        '
        'If e.Url.Segments(e.Url.Segments.Length - 1).EndsWith(".pdf") Then
        '    e.Cancel = True
        '    Dim arquivo1 As String = ""
        '    Dim oWC As New WebClient
        '
        '    'adicionado, caso tenha autennticação...
        '    If Not oWB.Document Is Nothing Then
        '        oWC.Headers.Add(HttpRequestHeader.Cookie, oWB.Document.Cookie)
        '    End If
        '
        '    oWC.DownloadFile(e.Url, "c:\temp\arq.pdf")
        'End If

    End Sub

    Public Function DownloadFile(ByVal remoteFilename As String, ByVal localFilename As String) As Integer

        'Dim bytesProcessed As Integer = 0
        'Dim remoteStream As Stream = Nothing
        'Dim localStream As Stream = Nothing
        Dim response As WebResponse = Nothing

        Try


            Dim oReq As HttpWebRequest
            Dim oResp As HttpWebResponse
            Dim oStream As MemoryStream
            Dim sr As StreamReader

            oReq = WebRequest.Create(remoteFilename)
            oReq.CookieContainer.SetCookies(oWB.Document.Url, GetGlobalCookies(oWB.Document.Url.AbsoluteUri))

            oResp = oReq.GetResponse()

            oStream = response.GetResponseStream()

            sr = New StreamReader(oStream)

            Dim content As String
            content = sr.ReadToEnd()


            'Dim request As WebRequest = WebRequest.Create(

            '
            '
            '
            '
            '     If request IsNot Nothing Then
            '         response = request.GetResponse()
            '
            '         If response IsNot Nothing Then
            '             remoteStream = response.GetResponseStream()
            '             localStream = File.Create(localFilename)
            '             Dim buffer As Byte() = New Byte(1023) {}
            '             Dim bytesRead As Integer
            '
            '             Do
            '                 bytesRead = remoteStream.Read(buffer, 0, buffer.Length)
            '                 localStream.Write(buffer, 0, bytesRead)
            '                 bytesProcessed += bytesRead
            '             Loop While bytesRead > 0
            '         End If
            '     End If


            Return 0

        Catch e As Exception
            Console.WriteLine(e.Message)
            Return 0
        Finally
            'If response IsNot Nothing Then response.Close()
            'If remoteStream IsNot Nothing Then remoteStream.Close()
            'If localStream IsNot Nothing Then localStream.Close()
        End Try

    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        oWB.Navigate("www.simulaimport.com.br/Teste/Arquivo.pdf")

    End Sub
End Class
