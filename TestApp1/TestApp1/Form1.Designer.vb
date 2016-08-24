<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestApp1
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
        Me.ButtonLoadTrips = New System.Windows.Forms.Button()
        Me.ListBoxSchools = New System.Windows.Forms.ListBox()
        Me.TextBoxError = New System.Windows.Forms.TextBox()
        Me.ListBoxTrips = New System.Windows.Forms.ListBox()
        Me.ButtonLoadStops = New System.Windows.Forms.Button()
        Me.ListBoxStops = New System.Windows.Forms.ListBox()
        Me.ButtonLoadView = New System.Windows.Forms.Button()
        Me.PictureBoxLeft = New System.Windows.Forms.PictureBox()
        Me.PictureBoxFront = New System.Windows.Forms.PictureBox()
        Me.PictureBoxRight = New System.Windows.Forms.PictureBox()
        Me.TextBoxDetails = New System.Windows.Forms.TextBox()
        CType(Me.PictureBoxLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxFront, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonLoadTrips
        '
        Me.ButtonLoadTrips.Location = New System.Drawing.Point(12, 48)
        Me.ButtonLoadTrips.Name = "ButtonLoadTrips"
        Me.ButtonLoadTrips.Size = New System.Drawing.Size(75, 23)
        Me.ButtonLoadTrips.TabIndex = 0
        Me.ButtonLoadTrips.Text = "Load Trips"
        Me.ButtonLoadTrips.UseVisualStyleBackColor = True
        '
        'ListBoxSchools
        '
        Me.ListBoxSchools.FormattingEnabled = True
        Me.ListBoxSchools.Location = New System.Drawing.Point(8, 12)
        Me.ListBoxSchools.Name = "ListBoxSchools"
        Me.ListBoxSchools.ScrollAlwaysVisible = True
        Me.ListBoxSchools.Size = New System.Drawing.Size(491, 30)
        Me.ListBoxSchools.TabIndex = 4
        '
        'TextBoxError
        '
        Me.TextBoxError.Location = New System.Drawing.Point(8, 541)
        Me.TextBoxError.Name = "TextBoxError"
        Me.TextBoxError.Size = New System.Drawing.Size(916, 20)
        Me.TextBoxError.TabIndex = 5
        '
        'ListBoxTrips
        '
        Me.ListBoxTrips.FormattingEnabled = True
        Me.ListBoxTrips.Location = New System.Drawing.Point(8, 77)
        Me.ListBoxTrips.Name = "ListBoxTrips"
        Me.ListBoxTrips.ScrollAlwaysVisible = True
        Me.ListBoxTrips.Size = New System.Drawing.Size(491, 69)
        Me.ListBoxTrips.TabIndex = 6
        '
        'ButtonLoadStops
        '
        Me.ButtonLoadStops.Location = New System.Drawing.Point(12, 152)
        Me.ButtonLoadStops.Name = "ButtonLoadStops"
        Me.ButtonLoadStops.Size = New System.Drawing.Size(75, 23)
        Me.ButtonLoadStops.TabIndex = 7
        Me.ButtonLoadStops.Text = "Load Stops"
        Me.ButtonLoadStops.UseVisualStyleBackColor = True
        '
        'ListBoxStops
        '
        Me.ListBoxStops.FormattingEnabled = True
        Me.ListBoxStops.Location = New System.Drawing.Point(8, 181)
        Me.ListBoxStops.Name = "ListBoxStops"
        Me.ListBoxStops.ScrollAlwaysVisible = True
        Me.ListBoxStops.Size = New System.Drawing.Size(491, 95)
        Me.ListBoxStops.TabIndex = 8
        '
        'ButtonLoadView
        '
        Me.ButtonLoadView.Location = New System.Drawing.Point(13, 283)
        Me.ButtonLoadView.Name = "ButtonLoadView"
        Me.ButtonLoadView.Size = New System.Drawing.Size(75, 23)
        Me.ButtonLoadView.TabIndex = 9
        Me.ButtonLoadView.Text = "Load View"
        Me.ButtonLoadView.UseVisualStyleBackColor = True
        '
        'PictureBoxLeft
        '
        Me.PictureBoxLeft.Location = New System.Drawing.Point(12, 322)
        Me.PictureBoxLeft.Name = "PictureBoxLeft"
        Me.PictureBoxLeft.Size = New System.Drawing.Size(300, 200)
        Me.PictureBoxLeft.TabIndex = 10
        Me.PictureBoxLeft.TabStop = False
        '
        'PictureBoxFront
        '
        Me.PictureBoxFront.Location = New System.Drawing.Point(318, 322)
        Me.PictureBoxFront.Name = "PictureBoxFront"
        Me.PictureBoxFront.Size = New System.Drawing.Size(300, 200)
        Me.PictureBoxFront.TabIndex = 11
        Me.PictureBoxFront.TabStop = False
        '
        'PictureBoxRight
        '
        Me.PictureBoxRight.Location = New System.Drawing.Point(624, 322)
        Me.PictureBoxRight.Name = "PictureBoxRight"
        Me.PictureBoxRight.Size = New System.Drawing.Size(300, 200)
        Me.PictureBoxRight.TabIndex = 12
        Me.PictureBoxRight.TabStop = False
        '
        'TextBoxDetails
        '
        Me.TextBoxDetails.Location = New System.Drawing.Point(515, 13)
        Me.TextBoxDetails.Multiline = True
        Me.TextBoxDetails.Name = "TextBoxDetails"
        Me.TextBoxDetails.Size = New System.Drawing.Size(409, 263)
        Me.TextBoxDetails.TabIndex = 13
        '
        'TestApp1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(945, 582)
        Me.Controls.Add(Me.TextBoxDetails)
        Me.Controls.Add(Me.PictureBoxRight)
        Me.Controls.Add(Me.PictureBoxFront)
        Me.Controls.Add(Me.PictureBoxLeft)
        Me.Controls.Add(Me.ButtonLoadView)
        Me.Controls.Add(Me.ListBoxStops)
        Me.Controls.Add(Me.ButtonLoadStops)
        Me.Controls.Add(Me.ListBoxTrips)
        Me.Controls.Add(Me.TextBoxError)
        Me.Controls.Add(Me.ListBoxSchools)
        Me.Controls.Add(Me.ButtonLoadTrips)
        Me.Name = "TestApp1"
        Me.Text = "TestApp1"
        CType(Me.PictureBoxLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxFront, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonLoadTrips As System.Windows.Forms.Button
    Friend WithEvents ListBoxSchools As System.Windows.Forms.ListBox
    Friend WithEvents TextBoxError As System.Windows.Forms.TextBox
    Friend WithEvents ListBoxTrips As System.Windows.Forms.ListBox
    Friend WithEvents ButtonLoadStops As System.Windows.Forms.Button
    Friend WithEvents ListBoxStops As System.Windows.Forms.ListBox
    Friend WithEvents ButtonLoadView As System.Windows.Forms.Button
    Friend WithEvents PictureBoxLeft As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxFront As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxRight As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxDetails As System.Windows.Forms.TextBox

End Class
