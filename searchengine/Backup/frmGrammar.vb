Public Class frmGrammar
    Inherits System.Windows.Forms.Form
    Dim lstData As ItemData
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents cboGrammar As System.Windows.Forms.ComboBox
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents txt As System.Windows.Forms.TextBox
    Friend WithEvents lstGrammar As System.Windows.Forms.ListBox
    Friend WithEvents lblDetail As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cboGrammar = New System.Windows.Forms.ComboBox()
        Me.lbl = New System.Windows.Forms.Label()
        Me.txt = New System.Windows.Forms.TextBox()
        Me.lstGrammar = New System.Windows.Forms.ListBox()
        Me.lblDetail = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cboGrammar
        '
        Me.cboGrammar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGrammar.Location = New System.Drawing.Point(334, 44)
        Me.cboGrammar.Name = "cboGrammar"
        Me.cboGrammar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboGrammar.Size = New System.Drawing.Size(216, 21)
        Me.cboGrammar.TabIndex = 0
        Me.cboGrammar.Visible = False
        '
        'lbl
        '
        Me.lbl.BackColor = System.Drawing.Color.Navy
        Me.lbl.Location = New System.Drawing.Point(-8, 0)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(690, 18)
        Me.lbl.TabIndex = 10
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txt
        '
        Me.txt.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.txt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt.Location = New System.Drawing.Point(8, 334)
        Me.txt.Multiline = True
        Me.txt.Name = "txt"
        Me.txt.ReadOnly = True
        Me.txt.Size = New System.Drawing.Size(660, 198)
        Me.txt.TabIndex = 13
        Me.txt.Text = ""
        Me.txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lstGrammar
        '
        Me.lstGrammar.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.lstGrammar.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstGrammar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lstGrammar.Location = New System.Drawing.Point(12, 30)
        Me.lstGrammar.MultiColumn = True
        Me.lstGrammar.Name = "lstGrammar"
        Me.lstGrammar.Size = New System.Drawing.Size(658, 260)
        Me.lstGrammar.TabIndex = 14
        '
        'lblDetail
        '
        Me.lblDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetail.Location = New System.Drawing.Point(532, 300)
        Me.lblDetail.Name = "lblDetail"
        Me.lblDetail.Size = New System.Drawing.Size(130, 30)
        Me.lblDetail.TabIndex = 15
        Me.lblDetail.Text = "تفصيل"
        Me.lblDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmGrammar
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.ClientSize = New System.Drawing.Size(682, 612)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblDetail, Me.lstGrammar, Me.txt, Me.lbl, Me.cboGrammar})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmGrammar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Learn Quran Via Arabic"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmGrammar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Rs As New ADODB.Recordset()
        Rs.Open("Select * from TEHLEEL", Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        cboGrammar.Items.Clear()
        Do While Not Rs.EOF = True
            lstData = New ItemData()
            lstData.ID = Rs.Fields.Item(0).Value
            lstData.Value = Rs.Fields.Item(1).Value
            cboGrammar.Items.Add(lstData)
            Rs.MoveNext()
        Loop
        Rs.Close()
        cboGrammar.SelectedIndex = 0

        grdFormat()
    End Sub

    Private Sub grdFormat()
        Dim Rs As New ADODB.Recordset()
        Rs.Open("Select * from GRAMMAR", Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        lstGrammar.Items.Clear()
        Do While Not Rs.EOF = True
            lstData = New ItemData()
            lstData.ID = Rs.Fields.Item(0).Value
            lstData.Value = Trim(Rs.Fields.Item(1).Value)
            lstGrammar.Items.Add(lstData)
            Rs.MoveNext()
        Loop
        Rs.Close()
    End Sub




    Private Sub lstGrammar_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstGrammar.SelectedIndexChanged
        Dim Rs As New ADODB.Recordset()
        Rs.Open("Select * from GRAMMAR Where SN=" & CType(lstGrammar.Items.Item(lstGrammar.SelectedIndex), ItemData).ID(), Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        If Not (Rs.EOF Or Rs.BOF) Then
            txt.Text = Rs.Fields.Item(2).Value & ""
        End If
        Rs.Close()
    End Sub
End Class
