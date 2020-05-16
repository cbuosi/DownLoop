Imports System.Net

Public Class Form1


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        oWB.Navigate("www.google.com")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        oWB.Navigate("www.simulaimport.com.br/Teste/Arquivo.zip")
    End Sub

    Private Sub oWB_Navigating(sender As Object, e As WebBrowserNavigatingEventArgs) Handles oWB.Navigating

        If e.Url.Segments(e.Url.Segments.Length - 1).EndsWith(".zip") Then
            e.Cancel = True
            Dim arquivo1 As String = ""
            Dim oWC As New WebClient

            'adicionado, caso tenha autennticação...
            If Not oWB.Document Is Nothing Then
                oWC.Headers.Add(HttpRequestHeader.Cookie, oWB.Document.Cookie)
            End If


            oWC.DownloadFile(e.Url, "c:\temp\arq.zip")
        End If

        If e.Url.Segments(e.Url.Segments.Length - 1).EndsWith(".pdf") Then
            e.Cancel = True
            Dim arquivo1 As String = ""
            Dim oWC As New WebClient

            'adicionado, caso tenha autennticação...
            If Not oWB.Document Is Nothing Then
                oWC.Headers.Add(HttpRequestHeader.Cookie, oWB.Document.Cookie)
            End If

            oWC.DownloadFile(e.Url, "c:\temp\arq.pdf")
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        oWB.Navigate("www.simulaimport.com.br/Teste/Arquivo.pdf")

    End Sub
End Class
