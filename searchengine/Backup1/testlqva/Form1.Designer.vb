<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.AxMSHFlexGrid1 = New AxMSHierarchicalFlexGridLib.AxMSHFlexGrid
        CType(Me.AxMSHFlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxMSHFlexGrid1
        '
        Me.AxMSHFlexGrid1.DataSource = Nothing
        Me.AxMSHFlexGrid1.Location = New System.Drawing.Point(140, 56)
        Me.AxMSHFlexGrid1.Name = "AxMSHFlexGrid1"
        Me.AxMSHFlexGrid1.OcxState = CType(resources.GetObject("AxMSHFlexGrid1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMSHFlexGrid1.Size = New System.Drawing.Size(328, 233)
        Me.AxMSHFlexGrid1.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 335)
        Me.Controls.Add(Me.AxMSHFlexGrid1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.AxMSHFlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AxMSHFlexGrid1 As AxMSHierarchicalFlexGridLib.AxMSHFlexGrid

End Class
