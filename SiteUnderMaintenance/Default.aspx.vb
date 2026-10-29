
Partial Class _Default
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim str As String = ""

        Try
            Dim strReferer As String = ""
            Dim arr1(), arr2() As String
            Dim coll As NameValueCollection
            ' Load Header collection into NameValueCollection object.
            coll = Request.Headers

            ' Put the names of all keys into a string array.
            arr1 = coll.AllKeys
            For loop1 = 0 To arr1.GetUpperBound(0)
                arr2 = coll.GetValues(loop1)
                ' Get all values under this key. 
                For loop2 = 0 To arr2.GetUpperBound(0)
                    If arr1(loop1) = "Referer" Then
                        strReferer = Server.HtmlEncode(arr2(loop2))
                        'objSQL.CreateLog("Referer:" & strReferer)
                    End If
                Next loop2
            Next loop1

            Dim i As Integer = 0
            i += 1
        Catch ex As Exception

        End Try

        str = "a"
        Response.Redirect("default2.aspx")
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class
