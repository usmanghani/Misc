Imports Word = Microsoft.Office.Interop.Word

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim application As New Word.Application()
        Dim document As New Word.Document()
        Dim filename As Object = "c:\\test.doc"

        application.Visible = False

        document = application.Documents.Open(filename)

        TextBox1.Text = document.Content.ListParagraphs.Item(1)


    End Sub

End Class
