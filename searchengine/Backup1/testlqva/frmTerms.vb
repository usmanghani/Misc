Public Class frmTrems
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
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents lblDetail As System.Windows.Forms.Label
    Friend WithEvents lstTerms As System.Windows.Forms.ListBox
    Friend WithEvents txt As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lbl = New System.Windows.Forms.Label()
        Me.lblDetail = New System.Windows.Forms.Label()
        Me.lstTerms = New System.Windows.Forms.ListBox()
        Me.txt = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lbl
        '
        Me.lbl.BackColor = System.Drawing.Color.Navy
        Me.lbl.Location = New System.Drawing.Point(-8, 4)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(690, 18)
        Me.lbl.TabIndex = 10
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDetail
        '
        Me.lblDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetail.Location = New System.Drawing.Point(530, 311)
        Me.lblDetail.Name = "lblDetail"
        Me.lblDetail.Size = New System.Drawing.Size(130, 30)
        Me.lblDetail.TabIndex = 18
        Me.lblDetail.Text = "تفصيل"
        Me.lblDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lstTerms
        '
        Me.lstTerms.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.lstTerms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstTerms.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lstTerms.Location = New System.Drawing.Point(10, 41)
        Me.lstTerms.MultiColumn = True
        Me.lstTerms.Name = "lstTerms"
        Me.lstTerms.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lstTerms.Size = New System.Drawing.Size(658, 260)
        Me.lstTerms.TabIndex = 17
        '
        'txt
        '
        Me.txt.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.txt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt.Location = New System.Drawing.Point(6, 345)
        Me.txt.Multiline = True
        Me.txt.Name = "txt"
        Me.txt.ReadOnly = True
        Me.txt.Size = New System.Drawing.Size(660, 198)
        Me.txt.TabIndex = 16
        Me.txt.Text = ""
        Me.txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmTrems
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.ClientSize = New System.Drawing.Size(674, 585)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblDetail, Me.lstTerms, Me.txt, Me.lbl})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmTrems"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Learn Quran Via Arabic"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmTrems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Rs As New ADODB.Recordset()
        Rs.Open("Select * from TEHLEEL", Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        lstTerms.Items.Clear()
        Do While Not Rs.EOF = True
            lstData = New ItemData()
            lstData.ID = Rs.Fields.Item(0).Value
            lstData.Value = Trim(Rs.Fields.Item(1).Value)
            lstTerms.Items.Add(lstData)
            Rs.MoveNext()
        Loop
        Rs.Close()
    End Sub

    Private Sub lstTerms_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTerms.SelectedIndexChanged
        Dim Rs As New ADODB.Recordset()
        Rs.Open("Select * from TEHLEEL Where SN=" & CType(lstTerms.Items.Item(lstTerms.SelectedIndex), ItemData).ID(), Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        If Not (Rs.EOF Or Rs.BOF) Then
            txt.Text = Rs.Fields.Item(2).Value & ""
        End If
        Rs.Close()

    End Sub
End Class
