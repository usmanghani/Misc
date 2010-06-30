Public Class frmLQVA
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
    Friend WithEvents grd As MSHierarchicalFlexGridLib.MSHFlexGrid
    Friend WithEvents lblsurah As System.Windows.Forms.Label
    Friend WithEvents cboSurah As System.Windows.Forms.ComboBox
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents rtxtTahleel As System.Windows.Forms.RichTextBox
    Friend WithEvents lblTahleel As System.Windows.Forms.Label
    Friend WithEvents tipGrd As System.Windows.Forms.ToolTip
    Friend WithEvents chkArabic As System.Windows.Forms.CheckBox
    Friend WithEvents chkUrdu As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnglish As System.Windows.Forms.CheckBox
    Friend WithEvents mnu As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuShow As System.Windows.Forms.MenuItem
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Friend WithEvents lstIndex As System.Windows.Forms.ListBox
    Friend WithEvents lnk As System.Windows.Forms.LinkLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmLQVA))
        Me.grd = New MSHierarchicalFlexGridLib.MSHFlexGrid()
        Me.lblsurah = New System.Windows.Forms.Label()
        Me.cboSurah = New System.Windows.Forms.ComboBox()
        Me.lbl = New System.Windows.Forms.Label()
        Me.rtxtTahleel = New System.Windows.Forms.RichTextBox()
        Me.mnu = New System.Windows.Forms.ContextMenu()
        Me.mnuShow = New System.Windows.Forms.MenuItem()
        Me.lblTahleel = New System.Windows.Forms.Label()
        Me.tipGrd = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkArabic = New System.Windows.Forms.CheckBox()
        Me.chkUrdu = New System.Windows.Forms.CheckBox()
        Me.chkEnglish = New System.Windows.Forms.CheckBox()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.lstIndex = New System.Windows.Forms.ListBox()
        Me.lnk = New System.Windows.Forms.LinkLabel()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl.SuspendLayout()
        Me.SuspendLayout()
        '
        'grd
        '
        Me.grd.DataSource = Nothing
        Me.grd.Location = New System.Drawing.Point(2, 56)
        Me.grd.Name = "grd"
        Me.grd.OcxState = CType(resources.GetObject("grd.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grd.Size = New System.Drawing.Size(676, 336)
        Me.grd.TabIndex = 3
        '
        'lblsurah
        '
        Me.lblsurah.AutoSize = True
        Me.lblsurah.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblsurah.ForeColor = System.Drawing.Color.Blue
        Me.lblsurah.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblsurah.Location = New System.Drawing.Point(608, 34)
        Me.lblsurah.Name = "lblsurah"
        Me.lblsurah.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblsurah.Size = New System.Drawing.Size(65, 15)
        Me.lblsurah.TabIndex = 4
        Me.lblsurah.Text = "سورة الفاتحة"
        Me.lblsurah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblsurah.Visible = False
        '
        'cboSurah
        '
        Me.cboSurah.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.cboSurah.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cboSurah.ForeColor = System.Drawing.Color.MediumBlue
        Me.cboSurah.ItemHeight = 13
        Me.cboSurah.Location = New System.Drawing.Point(240, 34)
        Me.cboSurah.MaxDropDownItems = 10
        Me.cboSurah.Name = "cboSurah"
        Me.cboSurah.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cboSurah.Size = New System.Drawing.Size(272, 21)
        Me.cboSurah.TabIndex = 8
        Me.cboSurah.Visible = False
        '
        'lbl
        '
        Me.lbl.BackColor = System.Drawing.Color.Navy
        Me.lbl.Location = New System.Drawing.Point(-8, 0)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(690, 18)
        Me.lbl.TabIndex = 9
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'rtxtTahleel
        '
        Me.rtxtTahleel.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.rtxtTahleel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtxtTahleel.ContextMenu = Me.mnu
        Me.rtxtTahleel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtTahleel.ForeColor = System.Drawing.Color.MediumBlue
        Me.rtxtTahleel.Location = New System.Drawing.Point(10, 419)
        Me.rtxtTahleel.Name = "rtxtTahleel"
        Me.rtxtTahleel.ReadOnly = True
        Me.rtxtTahleel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.rtxtTahleel.Size = New System.Drawing.Size(656, 85)
        Me.rtxtTahleel.TabIndex = 10
        Me.rtxtTahleel.Text = ""
        '
        'mnu
        '
        Me.mnu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuShow})
        '
        'mnuShow
        '
        Me.mnuShow.Index = 0
        Me.mnuShow.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.mnuShow.Text = "اس لفظ کے بارے ميں جانيے"
        '
        'lblTahleel
        '
        Me.lblTahleel.AutoSize = True
        Me.lblTahleel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTahleel.ForeColor = System.Drawing.Color.Blue
        Me.lblTahleel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTahleel.Location = New System.Drawing.Point(636, 396)
        Me.lblTahleel.Name = "lblTahleel"
        Me.lblTahleel.Size = New System.Drawing.Size(36, 19)
        Me.lblTahleel.TabIndex = 11
        Me.lblTahleel.Text = "تحليل"
        '
        'chkArabic
        '
        Me.chkArabic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkArabic.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkArabic.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkArabic.Location = New System.Drawing.Point(6, 26)
        Me.chkArabic.Name = "chkArabic"
        Me.chkArabic.Size = New System.Drawing.Size(54, 26)
        Me.chkArabic.TabIndex = 12
        Me.chkArabic.Text = "عربى"
        Me.chkArabic.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkUrdu
        '
        Me.chkUrdu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkUrdu.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkUrdu.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUrdu.Location = New System.Drawing.Point(68, 26)
        Me.chkUrdu.Name = "chkUrdu"
        Me.chkUrdu.Size = New System.Drawing.Size(48, 26)
        Me.chkUrdu.TabIndex = 13
        Me.chkUrdu.Text = "اردو"
        Me.chkUrdu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkEnglish
        '
        Me.chkEnglish.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkEnglish.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkEnglish.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnglish.Location = New System.Drawing.Point(132, 26)
        Me.chkEnglish.Name = "chkEnglish"
        Me.chkEnglish.Size = New System.Drawing.Size(72, 26)
        Me.chkEnglish.TabIndex = 14
        Me.chkEnglish.Text = "انگريزى"
        Me.chkEnglish.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl
        '
        Me.pnl.Controls.AddRange(New System.Windows.Forms.Control() {Me.lstIndex})
        Me.pnl.Location = New System.Drawing.Point(1, 56)
        Me.pnl.Name = "pnl"
        Me.pnl.Size = New System.Drawing.Size(675, 492)
        Me.pnl.TabIndex = 15
        '
        'lstIndex
        '
        Me.lstIndex.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.lstIndex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstIndex.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lstIndex.Location = New System.Drawing.Point(172, 9)
        Me.lstIndex.MultiColumn = True
        Me.lstIndex.Name = "lstIndex"
        Me.lstIndex.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lstIndex.Size = New System.Drawing.Size(492, 416)
        Me.lstIndex.TabIndex = 1
        '
        'lnk
        '
        Me.lnk.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnk.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnk.Location = New System.Drawing.Point(264, 510)
        Me.lnk.Name = "lnk"
        Me.lnk.Size = New System.Drawing.Size(168, 30)
        Me.lnk.TabIndex = 16
        Me.lnk.TabStop = True
        Me.lnk.Text = "قرآن کى فﮧرست دﻳکھﻳں"
        Me.lnk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lnk.Visible = False
        '
        'frmLQVA
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(201, Byte), CType(233, Byte), CType(254, Byte))
        Me.ClientSize = New System.Drawing.Size(682, 612)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lnk, Me.pnl, Me.chkEnglish, Me.chkUrdu, Me.chkArabic, Me.lblTahleel, Me.rtxtTahleel, Me.lbl, Me.lblsurah, Me.grd, Me.cboSurah})
        Me.ForeColor = System.Drawing.Color.DarkBlue
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLQVA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Learn Quran Via Arabic"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub grd_DblClick() Handles grd.DblClick

        Dim Rs As New ADODB.Recordset()
        Rs.Open("Select * From " & CType(cboSurah.SelectedItem, ItemData).ID & " where AN=" & grd.get_TextMatrix(grd.Row, 1), Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        If Not (Rs.EOF Or Rs.BOF) Then
            rtxtTahleel.Text = Rs.Fields.Item(5).Value & ""
        End If
        Rs.Close()

    End Sub

    Private Sub frmLQVA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '201, 233, 254

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
        CreateMyToolTip()
        grdIndexFormat()

    End Sub

    Private Sub cboSurah_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSurah.SelectedIndexChanged
        Call ShowData()
    End Sub

    Private Sub ShowData()

        '---------------- All checks are true ---------------------
        If chkArabic.Checked = True And chkUrdu.Checked = True And chkEnglish.Checked = True Then
            Call ArabicUrduEnglish()
            Exit Sub
        End If
        '----------------------- English Urdu -----------------------
        If chkEnglish.Checked = True And chkUrdu.Checked = True Then
            Call UrduEnglish()
            Exit Sub
        End If

        '-------------------- Arabic English ------------------------
        If chkArabic.Checked = True And chkEnglish.Checked = True Then
            Call ArabicEnglish()
            Exit Sub
        End If

        '------------------------- Arabic Urdu ---------------------
        If chkArabic.Checked = True And chkUrdu.Checked = True Then
            Call ArabicUrdu()
            Exit Sub
        End If

        '----------------------- Arabic -----------------------------
        If chkArabic.Checked = True Then
            Call Arabic()
            Exit Sub
        End If
        '----------------------- Urdu -----------------------------
        If chkUrdu.Checked = True Then
            Call Urdu()
            Exit Sub
        End If
        '----------------------- English -----------------------------
        If chkEnglish.Checked = True Then
            Call English()
            Exit Sub
        End If

    End Sub

    Private Sub CreateMyToolTip()
        ' Create the ToolTip and associate with the Form container.
        Dim toolTip1 As New ToolTip(Me.components)

        ' Set up the delays for the ToolTip.
        toolTip1.AutoPopDelay = 5000
        toolTip1.InitialDelay = 1000
        toolTip1.ReshowDelay = 500
        ' Force the ToolTip text to be displayed whether or not the form is active.
        toolTip1.ShowAlways = True

        ' Set up the ToolTip text for the Button and Checkbox.
        toolTip1.SetToolTip(Me.grd, "Double click on any ayat to view tahleel")
        toolTip1.SetToolTip(Me.cboSurah, "Surah")
    End Sub

    Private Sub chkState()
        If chkArabic.Checked = False And _
            chkUrdu.Checked = False And _
            chkEnglish.Checked = False Then
            chkArabic.Checked = True
        End If
    End Sub

    Private Sub chkArabic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkArabic.CheckedChanged
        Call chkState()
        Call ShowData()
    End Sub

    Private Sub ArabicUrduEnglish()
        rtxtTahleel.Text = ""
        grd.Clear()
        grd.Rows = 3
        Dim Rs As New ADODB.Recordset()
        grd.set_ColWidth(1, 0, 0)
        grd.set_ColWidth(0, 0, 9700)
        grd.set_ColAlignment(0, 7)
        grd.WordWrap = True
        grd.RowHeightMin = 400

        Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID, Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
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
                '--------------- Urdu -----------------------
                grd.Row = A + 1 'for focus a row
                grd.CellFontSize = 10
                grd.CellFontName = "Tahoma"
                grd.CellForeColor = Convert.ToUInt32(Color.DarkGreen)
                grd.WordWrap = True

                grd.set_TextMatrix(A + 1, 0, Rs.Fields.Item(3).Value & "")
                grd.set_TextMatrix(A + 1, 1, Rs.Fields.Item(0).Value & "")
                '--------------------------------------------
                '--------------- English --------------------
                grd.Row = A + 2 'for focus a row
                grd.CellFontName = "Verdana"
                grd.CellFontSize = 10
                grd.CellForeColor = Convert.ToUInt32(Color.Maroon) 'DarkGoldenrod
                grd.WordWrap = True

                grd.set_TextMatrix(A + 2, 0, Rs.Fields.Item(4).Value & "")
                grd.set_TextMatrix(A + 2, 1, Rs.Fields.Item(0).Value & "")

                If Len(Rs.Fields.Item(4).Value & "") + 300 >= grd.get_RowHeight(A + 2) Then
                    grd.set_RowHeight(A + 2, Len(Rs.Fields.Item(4).Value & "") + 800)
                    grd.Row = A + 2
                    grd.CellAlignment = 1
                End If
                '---------------------------------------------

                A = A + 3
                grd.Rows = grd.Rows + 3
                Rs.MoveNext()
            Next
            grd.Rows = grd.Rows - 3
            Rs.Close()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub ArabicUrdu()
        rtxtTahleel.Text = ""
        grd.Clear()
        grd.Rows = 2
        Dim Rs As New ADODB.Recordset()
        grd.set_ColWidth(1, 0, 0)
        grd.set_ColWidth(0, 0, 9700)
        grd.set_ColAlignment(0, 7)
        grd.WordWrap = True
        grd.RowHeightMin = 400

        Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID, Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
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
                '--------------- Urdu -----------------------
                grd.Row = A + 1 'for focus a row
                grd.CellFontSize = 10
                grd.CellFontName = "Tahoma"
                grd.CellForeColor = Convert.ToUInt32(Color.DarkGreen)
                grd.WordWrap = True

                grd.set_TextMatrix(A + 1, 0, Rs.Fields.Item(3).Value & "")
                grd.set_TextMatrix(A + 1, 1, Rs.Fields.Item(0).Value & "")
                '--------------------------------------------
                A = A + 2
                grd.Rows = grd.Rows + 2
                Rs.MoveNext()
            Next
            grd.Rows = grd.Rows - 2
            Rs.Close()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub ArabicEnglish()
        rtxtTahleel.Text = ""
        grd.Clear()
        grd.Rows = 2
        Dim Rs As New ADODB.Recordset()
        grd.set_ColWidth(1, 0, 0)
        grd.set_ColWidth(0, 0, 9700)
        grd.set_ColAlignment(0, 7)
        grd.WordWrap = True
        grd.RowHeightMin = 400

        Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID, Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
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
                '--------------- English --------------------
                grd.Row = A + 1 'for focus a row
                grd.CellFontName = "Verdana"
                grd.CellFontSize = 10
                grd.CellForeColor = Convert.ToUInt32(Color.Maroon) 'DarkGoldenrod
                grd.WordWrap = True

                grd.set_TextMatrix(A + 1, 0, Rs.Fields.Item(4).Value & "")
                grd.set_TextMatrix(A + 1, 1, Rs.Fields.Item(0).Value & "")

                If Len(Rs.Fields.Item(4).Value & "") + 300 >= grd.get_RowHeight(A + 1) Then
                    grd.set_RowHeight(A + 1, Len(Rs.Fields.Item(4).Value & "") + 800)
                    grd.Row = A + 1
                    grd.CellAlignment = 1
                End If
                '---------------------------------------------

                A = A + 2
                grd.Rows = grd.Rows + 2
                Rs.MoveNext()
            Next
            grd.Rows = grd.Rows - 2
            Rs.Close()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub UrduEnglish()
        rtxtTahleel.Text = ""
        grd.Clear()
        grd.Rows = 2
        Dim Rs As New ADODB.Recordset()
        grd.set_ColWidth(1, 0, 0)
        grd.set_ColWidth(0, 0, 9700)
        grd.set_ColAlignment(0, 7)
        grd.WordWrap = True
        grd.RowHeightMin = 400

        Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID, Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        Dim i As Integer
        Dim A As Integer
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If Not (Rs.EOF Or Rs.BOF) Then
            For i = 0 To Rs.RecordCount - 1
                '--------------- Urdu -----------------------
                grd.Row = A  'for focus a row
                grd.CellFontSize = 10
                grd.CellFontName = "Tahoma"
                grd.CellForeColor = Convert.ToUInt32(Color.DarkGreen)
                grd.WordWrap = True

                grd.set_TextMatrix(A, 0, Rs.Fields.Item(3).Value & "")
                grd.set_TextMatrix(A, 1, Rs.Fields.Item(0).Value & "")
                '--------------------------------------------
                '--------------- English --------------------
                grd.Row = A + 1 'for focus a row
                grd.CellFontName = "Verdana"
                grd.CellFontSize = 10
                grd.CellForeColor = Convert.ToUInt32(Color.Maroon) 'DarkGoldenrod
                grd.WordWrap = True

                grd.set_TextMatrix(A + 1, 0, Rs.Fields.Item(4).Value & "")
                grd.set_TextMatrix(A + 1, 1, Rs.Fields.Item(0).Value & "")

                If Len(Rs.Fields.Item(4).Value & "") + 300 >= grd.get_RowHeight(A + 1) Then
                    grd.set_RowHeight(A + 1, Len(Rs.Fields.Item(4).Value & "") + 800)
                    grd.Row = A + 1
                    grd.CellAlignment = 1
                End If
                '---------------------------------------------

                A = A + 2
                grd.Rows = grd.Rows + 2
                Rs.MoveNext()
            Next
            grd.Rows = grd.Rows - 2
            Rs.Close()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Arabic()
        rtxtTahleel.Text = ""
        grd.Clear()
        grd.Rows = 1
        Dim Rs As New ADODB.Recordset()
        grd.set_ColWidth(1, 0, 0)
        grd.set_ColWidth(0, 0, 9700)
        grd.set_ColAlignment(0, 7)
        grd.WordWrap = True
        grd.RowHeightMin = 400

        Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID, Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
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

    Private Sub Urdu()
        rtxtTahleel.Text = ""
        grd.Clear()
        grd.Rows = 1
        Dim Rs As New ADODB.Recordset()
        grd.set_ColWidth(1, 0, 0)
        grd.set_ColWidth(0, 0, 9700)
        grd.set_ColAlignment(0, 7)
        grd.WordWrap = True
        grd.RowHeightMin = 400

        Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID, Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        Dim i As Integer
        Dim A As Integer
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If Not (Rs.EOF Or Rs.BOF) Then
            For i = 0 To Rs.RecordCount - 1
                '--------------- Urdu -----------------------
                grd.Row = A  'for focus a row
                grd.CellFontSize = 10
                grd.CellFontName = "Tahoma"
                grd.CellForeColor = Convert.ToUInt32(Color.DarkGreen)
                grd.WordWrap = True

                grd.set_TextMatrix(A, 0, Rs.Fields.Item(3).Value & "")
                grd.set_TextMatrix(A, 1, Rs.Fields.Item(0).Value & "")
                '--------------------------------------------
                A = A + 1
                grd.Rows = grd.Rows + 1
                Rs.MoveNext()
            Next
            grd.Rows = grd.Rows - 1
            Rs.Close()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub English()
        rtxtTahleel.Text = ""
        grd.Clear()
        grd.Rows = 1
        Dim Rs As New ADODB.Recordset()
        grd.set_ColWidth(1, 0, 0)
        grd.set_ColWidth(0, 0, 9700)
        grd.set_ColAlignment(0, 7)
        grd.WordWrap = True
        grd.RowHeightMin = 400

        Rs.Open("Select * from " & CType(cboSurah.SelectedItem, ItemData).ID, Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        Dim i As Integer
        Dim A As Integer
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If Not (Rs.EOF Or Rs.BOF) Then
            For i = 0 To Rs.RecordCount - 1
                '--------------- English --------------------
                grd.Row = A  'for focus a row
                grd.CellFontName = "Verdana"
                grd.CellFontSize = 10
                grd.CellForeColor = Convert.ToUInt32(Color.Maroon) 'DarkGoldenrod
                grd.WordWrap = True
                grd.CellAlignment = 1

                grd.set_TextMatrix(A, 0, Rs.Fields.Item(4).Value & "")
                grd.set_TextMatrix(A, 1, Rs.Fields.Item(0).Value & "")

                If Len(Rs.Fields.Item(4).Value & "") + 300 >= grd.get_RowHeight(A) Then
                    grd.set_RowHeight(A, Len(Rs.Fields.Item(4).Value & "") + 800)
                    grd.Row = A
                    'grd.CellAlignment = 1
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

    Private Sub chkUrdu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUrdu.CheckedChanged
        Call chkState()
        Call ShowData()
    End Sub

    Private Sub chkEnglish_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnglish.CheckedChanged
        Call chkState()
        Call ShowData()
    End Sub

    Private Sub mnuShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuShow.Click
        'Dim frmGRM As New frmGrammar()
        Dim frmM As New frmMain()
        Dim FrmT As New frmTrems()

        If Len(rtxtTahleel.SelectedText) > 0 Then
            'frmGRM.Activate()
            'frmGRM.MdiParent = frmMain.ActiveForm
            'frmGRM.Show()
            'frmGRM.Width = frmMain.ActiveForm.Width - frmM.picLeft.Width - 13
            'frmGRM.Top = 0
            'frmGRM.Left = frmM.picLeft.Width
            'frmGRM.Height = frmMain.ActiveForm.Height - 32

            Dim Rs As New ADODB.Recordset()
            Rs.Open("Select * from TEHLEEL Where TERM='" & Trim(rtxtTahleel.SelectedText) & "'", Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
            If Not (Rs.EOF Or Rs.BOF) Then
                'MsgBox(Rs.Fields.Item(2).Value & "")
                FrmT.MdiParent = frmMain.ActiveForm
                FrmT.Show()
                FrmT.Width = frmMain.ActiveForm.Width - frmM.picLeft.Width - 13
                FrmT.Top = 0
                FrmT.Left = frmM.picLeft.Width
                FrmT.Height = frmMain.ActiveForm.Height - 32

                FrmT.lstTerms.SelectedIndex = Rs.Fields.Item(0).Value - 1 & ""

            End If
            Rs.Close()
        End If
    End Sub

    Private Sub mnu_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnu.Popup
        If Len(rtxtTahleel.SelectedText) > 0 Then
            mnuShow.Text = rtxtTahleel.SelectedText & " کے بارے ميں جانيے"
        Else
            mnuShow.Text = "کسى بھى لفظ کے بارے ميں جاننے کےليے اسے منتخب کريں"
        End If
    End Sub

    Private Sub grdIndexFormat()
        Dim Rs As New ADODB.Recordset()
        Rs.Open("Select * from SURAH", Cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        lstIndex.Items.Clear()
        Do While Not Rs.EOF = True
            lstData = New ItemData()
            lstData.ID = Rs.Fields.Item(0).Value
            lstData.Value = Rs.Fields.Item(1).Value
            lstIndex.Items.Add(lstData)
            Rs.MoveNext()
        Loop
        Rs.Close()

    End Sub

    Private Sub lstIndex_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstIndex.DoubleClick
        pnl.Visible = False
        lnk.Visible = True
        lblsurah.Visible = True
        cboSurah.SelectedIndex = lstIndex.SelectedIndex
        Try
            lblsurah.Text = (CType(lstIndex.Items.Item(lstIndex.SelectedIndex), ItemData).Value()) 'lstIndex.Items.Item(lstIndex.SelectedIndex)
        Finally
        End Try
    End Sub

    Private Sub lnk_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnk.LinkClicked
        pnl.Visible = True
        lnk.Visible = False
        lblsurah.Visible = False
    End Sub

End Class
