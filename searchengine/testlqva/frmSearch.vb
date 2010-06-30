
Public Class frmSearch
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
    Friend WithEvents cboSurah As System.Windows.Forms.ComboBox
    Friend WithEvents lblsurah As System.Windows.Forms.Label
    Friend WithEvents rSingle As System.Windows.Forms.RadioButton
    Friend WithEvents rRange As System.Windows.Forms.RadioButton
    Friend WithEvents txtSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFrom As System.Windows.Forms.TextBox
    Friend WithEvents grd As MSHierarchicalFlexGridLib.MSHFlexGrid
    Friend WithEvents lnk As System.Windows.Forms.LinkLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSearch))
        Me.lbl = New System.Windows.Forms.Label()
        Me.cboSurah = New System.Windows.Forms.ComboBox()
        Me.lblsurah = New System.Windows.Forms.Label()
        Me.rSingle = New System.Windows.Forms.RadioButton()
        Me.rRange = New System.Windows.Forms.RadioButton()
        Me.txtSingle = New System.Windows.Forms.TextBox()
        Me.txtTo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFrom = New System.Windows.Forms.TextBox()
        Me.grd = New MSHierarchicalFlexGridLib.MSHFlexGrid()
        Me.lnk = New System.Windows.Forms.LinkLabel()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl
        '
        Me.lbl.BackColor = System.Drawing.Color.Navy
        Me.lbl.Location = New System.Drawing.Point(-4, 0)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(690, 18)
        Me.lbl.TabIndex = 11
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboSurah
        '
        Me.cboSurah.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.cboSurah.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSurah.ForeColor = System.Drawing.Color.MediumBlue
        Me.cboSurah.ItemHeight = 13
        Me.cboSurah.Location = New System.Drawing.Point(305, 32)
        Me.cboSurah.MaxDropDownItems = 10
        Me.cboSurah.Name = "cboSurah"
        Me.cboSurah.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboSurah.Size = New System.Drawing.Size(267, 21)
        Me.cboSurah.TabIndex = 12
        '
        'lblsurah
        '
        Me.lblsurah.AutoSize = True
        Me.lblsurah.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblsurah.ForeColor = System.Drawing.Color.Black
        Me.lblsurah.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblsurah.Location = New System.Drawing.Point(581, 36)
        Me.lblsurah.Name = "lblsurah"
        Me.lblsurah.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblsurah.Size = New System.Drawing.Size(30, 15)
        Me.lblsurah.TabIndex = 13
        Me.lblsurah.Text = "سورة"
        Me.lblsurah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rSingle
        '
        Me.rSingle.Checked = True
        Me.rSingle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.rSingle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rSingle.Location = New System.Drawing.Point(578, 68)
        Me.rSingle.Name = "rSingle"
        Me.rSingle.Size = New System.Drawing.Size(80, 26)
        Me.rSingle.TabIndex = 14
        Me.rSingle.TabStop = True
        Me.rSingle.Text = "ايک آيت کيلۓ"
        Me.rSingle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rRange
        '
        Me.rRange.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.rRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rRange.Location = New System.Drawing.Point(578, 106)
        Me.rRange.Name = "rRange"
        Me.rRange.Size = New System.Drawing.Size(40, 26)
        Me.rRange.TabIndex = 15
        Me.rRange.Text = "آيت"
        Me.rRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSingle
        '
        Me.txtSingle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSingle.Location = New System.Drawing.Point(306, 71)
        Me.txtSingle.Name = "txtSingle"
        Me.txtSingle.Size = New System.Drawing.Size(266, 20)
        Me.txtSingle.TabIndex = 16
        Me.txtSingle.Text = ""
        Me.txtSingle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTo
        '
        Me.txtTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTo.Location = New System.Drawing.Point(458, 110)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(114, 20)
        Me.txtTo.TabIndex = 17
        Me.txtTo.Text = ""
        Me.txtTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(430, 114)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(20, 15)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "سے"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFrom
        '
        Me.txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFrom.Location = New System.Drawing.Point(306, 111)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(114, 20)
        Me.txtFrom.TabIndex = 19
        Me.txtFrom.Text = ""
        Me.txtFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'grd
        '
        Me.grd.DataSource = Nothing
        Me.grd.Location = New System.Drawing.Point(3, 138)
        Me.grd.Name = "grd"
        Me.grd.OcxState = CType(resources.GetObject("grd.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grd.Size = New System.Drawing.Size(671, 392)
        Me.grd.TabIndex = 20
        '
        'lnk
        '
        Me.lnk.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnk.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnk.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnk.Location = New System.Drawing.Point(196, 110)
        Me.lnk.Name = "lnk"
        Me.lnk.Size = New System.Drawing.Size(70, 20)
        Me.lnk.TabIndex = 21
        Me.lnk.TabStop = True
        Me.lnk.Text = "تلاش کريں"
        '
        'frmSearch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.ClientSize = New System.Drawing.Size(682, 612)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lnk, Me.grd, Me.txtFrom, Me.Label1, Me.lblsurah, Me.txtTo, Me.txtSingle, Me.rRange, Me.rSingle, Me.cboSurah, Me.lbl})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Learn Quran Via Arabic"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Rs As New ADODB.Recordset()
        Rs.Open("Select * from SURAH", Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        cboSurah.Items.Clear()
        Do While Not Rs.EOF = True
            lstData = New ItemData()
            lstData.ID = Rs.Fields.Item(0).Value
            lstData.Value = Rs.Fields.Item(1).Value
            cboSurah.Items.Add(lstData)
            Rs.MoveNext()
        Loop
        Rs.Close()
        cboSurah.SelectedIndex = 0
    End Sub

    Private Sub lnk_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnk.LinkClicked
        'rtxtTahleel.Text = ""
        grd.Clear()
        grd.Rows = 1
        Dim Rs As New ADODB.Recordset()
        grd.set_ColWidth(1, 0, 0)
        grd.set_ColWidth(0, 0, 9700)
        grd.set_ColAlignment(0, 7)
        grd.WordWrap = True
        grd.RowHeightMin = 400

        'Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID, Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)

        'If Rs.RecordCount < Trim(txtSingle.Text) And Rs.RecordCount > Trim(txtSingle.Text) Then
        '    MsgBox("ok")
        '    Rs.Close()
        '    Exit Sub
        'End If
        'Rs.Close()


        If rSingle.Checked = True Then
            If Trim(txtSingle.Text) = "" Then MsgBox("آيت نمبر لکھيں۔") : Exit Sub
            Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID, Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)

            If Rs.RecordCount < Trim(txtSingle.Text) And Rs.RecordCount > Trim(txtSingle.Text) Then
                MsgBox("ok")
                Rs.Close()
                Exit Sub
            End If
            Rs.Close()

            Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID & " Where AN=" & Trim(txtSingle.Text), Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        Else
            If Trim(txtTo.Text) = "" Or Trim(txtFrom.Text) = "" Then MsgBox("آيت نمبر لکھيں۔") : Exit Sub
            Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID & " Where AN BETWEEN " & Trim(txtTo.Text) & "And " & Trim(txtFrom.Text), Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        End If
        Dim i As Integer
        Dim A As Integer
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If Not (Rs.EOF Or Rs.BOF) Then
            For i = 0 To Rs.RecordCount - 1
                ' ----------- Arabic Text ------------------
                grd.Row = A 'for focus a row
                grd.CellFontName = "Microsoft Sans Serif"
                grd.WordWrap = True

                grd.set_TextMatrix(A, 0, Rs.Fields.Item(2).Value & "")
                grd.set_TextMatrix(A, 1, Rs.Fields.Item(0).Value & "")

                If Len(Rs.Fields.Item(2).Value) + 200 >= grd.get_RowHeight(A) Then
                    grd.set_RowHeight(A, Len(Rs.Fields.Item(2).Value) + 1000)
                End If
                '---------------------------------------------
                A = A + 1
                grd.Rows = grd.Rows + 1
                Rs.MoveNext()
            Next
            grd.Rows = grd.Rows - 1
            Rs.Close()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub txtSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSingle.TextChanged
        rSingle.Checked = True
    End Sub

    Private Sub txtTo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTo.TextChanged
        rRange.Checked = True
    End Sub
End Class
