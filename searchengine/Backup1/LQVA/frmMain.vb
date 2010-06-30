Public Class frmMain
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents picLeft As System.Windows.Forms.PictureBox
    Friend WithEvents btnLQVA As System.Windows.Forms.Button
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Friend WithEvents btnGrammar As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnTerm As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMain))
        Me.picLeft = New System.Windows.Forms.PictureBox
        Me.btnLQVA = New System.Windows.Forms.Button
        Me.btnAbout = New System.Windows.Forms.Button
        Me.btnGrammar = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnTerm = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'picLeft
        '
        Me.picLeft.BackColor = System.Drawing.Color.LightSkyBlue
        Me.picLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picLeft.Location = New System.Drawing.Point(2, 0)
        Me.picLeft.Name = "picLeft"
        Me.picLeft.Size = New System.Drawing.Size(120, 342)
        Me.picLeft.TabIndex = 1
        Me.picLeft.TabStop = False
        '
        'btnLQVA
        '
        Me.btnLQVA.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLQVA.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnLQVA.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLQVA.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnLQVA.Location = New System.Drawing.Point(4, 76)
        Me.btnLQVA.Name = "btnLQVA"
        Me.btnLQVA.Size = New System.Drawing.Size(116, 22)
        Me.btnLQVA.TabIndex = 2
        Me.btnLQVA.Text = "قرآنِ کريم"
        '
        'btnAbout
        '
        Me.btnAbout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAbout.Font = New System.Drawing.Font("Arial Unicode MS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbout.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnAbout.Location = New System.Drawing.Point(4, 48)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(116, 24)
        Me.btnAbout.TabIndex = 4
        Me.btnAbout.Text = "پروگرام کے بارے ميں"
        '
        'btnGrammar
        '
        Me.btnGrammar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGrammar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnGrammar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGrammar.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnGrammar.Location = New System.Drawing.Point(4, 104)
        Me.btnGrammar.Name = "btnGrammar"
        Me.btnGrammar.Size = New System.Drawing.Size(116, 32)
        Me.btnGrammar.TabIndex = 6
        Me.btnGrammar.Text = "قرآن اور عربى گرامر"
        '
        'btnSearch
        '
        Me.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnSearch.Location = New System.Drawing.Point(4, 208)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(116, 22)
        Me.btnSearch.TabIndex = 7
        Me.btnSearch.Text = "تلاش کريں"
        '
        'btnExit
        '
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnExit.Location = New System.Drawing.Point(4, 232)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(116, 22)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "باهر جانے کےلۓ"
        '
        'btnTerm
        '
        Me.btnTerm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTerm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnTerm.Font = New System.Drawing.Font("Arial Unicode MS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTerm.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnTerm.Location = New System.Drawing.Point(4, 144)
        Me.btnTerm.Name = "btnTerm"
        Me.btnTerm.Size = New System.Drawing.Size(116, 56)
        Me.btnTerm.TabIndex = 10
        Me.btnTerm.Text = "قرآن مﻳں مستعمل عربى گرامر کى تمام اصطلاحات"
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 15)
        Me.BackColor = System.Drawing.Color.LightSkyBlue
        Me.ClientSize = New System.Drawing.Size(476, 343)
        Me.Controls.Add(Me.btnTerm)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnGrammar)
        Me.Controls.Add(Me.btnAbout)
        Me.Controls.Add(Me.btnLQVA)
        Me.Controls.Add(Me.picLeft)
        Me.Font = New System.Drawing.Font("Arial Unicode MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "frmMain"
        Me.Text = "Learn Quran Via Arabic"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim frmLQVA1 As New frmLQVA()
    Dim frmAbout As New frmAbout()
    Dim frmG As New frmGrammar()
    Dim frmT As New frmTrems()
    Dim frmS As New frmSearch()

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call OpenConnection()
        picLeft.Height = Me.Height - 28
        Call About()
    End Sub

    Private Sub btnLQVA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLQVA.Click
        Call surah()
    End Sub

    Private Sub surah()
        frmLQVA1.Activate()
        frmLQVA1.MdiParent = Me
        frmLQVA1.Show()
        frmLQVA1.Width = Me.Width - Me.picLeft.Width - 13
        frmLQVA1.Top = 0
        frmLQVA1.Left = picLeft.Width
        frmLQVA1.Height = picLeft.Height - 4
        frmLQVA1.StartPosition = FormStartPosition.Manual
        frmLQVA1.chkArabic.Checked = True
    End Sub

    Private Sub frmMain_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        Dim i As Integer
        For i = 0 To Me.MdiChildren.Length - 1
            Me.MdiChildren(i).Width = Me.Width - Me.picLeft.Width - 13
            Me.MdiChildren(i).Left = Me.picLeft.Width
            Me.picLeft.Height = Me.Height - 28
            Me.MdiChildren(i).Height = Me.picLeft.Height - 4

        Next

    End Sub

    Private Sub About()
        frmAbout.Activate()
        frmAbout.MdiParent = Me
        frmAbout.Show()
        frmAbout.Width = Me.Width - Me.picLeft.Width - 13
        frmAbout.Top = 0
        frmAbout.Left = picLeft.Width
        frmAbout.Height = picLeft.Height - 4
        frmAbout.StartPosition = FormStartPosition.Manual

    End Sub

    Private Sub Grammar()
        frmG.Activate()
        frmG.MdiParent = Me
        frmG.Show()
        frmG.Width = Me.Width - Me.picLeft.Width - 13
        frmG.Top = 0
        frmG.Left = picLeft.Width
        frmG.Height = picLeft.Height - 4
        frmG.StartPosition = FormStartPosition.Manual

    End Sub

    Private Sub Terms()
        frmT.Activate()
        frmT.MdiParent = Me
        frmT.Show()
        frmT.Width = Me.Width - Me.picLeft.Width - 13
        frmT.Top = 0
        frmT.Left = picLeft.Width
        frmT.Height = picLeft.Height - 4
        frmT.StartPosition = FormStartPosition.Manual

    End Sub

    Private Sub Search()
        frmS.Activate()
        frmS.MdiParent = Me
        frmS.Show()
        frmS.Width = Me.Width - Me.picLeft.Width - 13
        frmS.Top = 0
        frmS.Left = picLeft.Width
        frmS.Height = picLeft.Height - 4
        frmS.StartPosition = FormStartPosition.Manual

    End Sub

    Private Sub btnAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbout.Click
        Call About()
    End Sub

    Private Sub frmMain_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If MessageBox.Show("کيا آپ پروگرام سے باهر جانا چاهتے هيں؟", "باهر جانے کےلۓ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.Yes Then
            If Cn.State = 1 Then
                Cn.Close()
                Cn = Nothing
            End If
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MessageBox.Show("کيا آپ پروگرام سے باهر جانا چاهتے هيں؟", "باهر جانے کےلۓ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub btnGrammar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrammar.Click
        Call Grammar()
    End Sub

    Private Sub btnTerm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTerm.Click
        Call Terms()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Call Search()
    End Sub
End Class
