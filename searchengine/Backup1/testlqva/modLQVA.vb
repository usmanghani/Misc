Module modLQVA
    Public Cn As New ADODB.Connection()
    'Public frmLQVA As frmLQVA

    Public Sub OpenConnection()
        Cn.Provider = "Microsoft.Jet.OLEDB.4.0"
        Cn.Open(Application.StartupPath + "\LQVA.mdb")
    End Sub

End Module
