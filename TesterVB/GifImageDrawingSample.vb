Imports System.ComponentModel
Imports System.Text.RegularExpressions

Namespace TesterVB
	Public Class GifImageDrawingSample
		Inherits Form

		Private ReadOnly style As GifImageStyle

		Private Shared ReadOnly RegexSpecSymbolsPattern As String = "[\^\$\[\]\(\)\.\\\*\+\|\?\{\}]"

		Private ReadOnly components As IContainer = Nothing

		Private fctb As FastColoredTextBox

		Public Sub New()
			Me.InitializeComponent()
			Me.style = New GifImageStyle(Me.fctb)
			Me.style.ImagesByText.Add(":bb", My.Resources.bye)
			Me.style.ImagesByText.Add(":D", My.Resources.lol)
			Me.style.ImagesByText.Add("8)", My.Resources.rolleyes)
			Me.style.ImagesByText.Add(":@", My.Resources.unsure)
			Me.style.ImagesByText.Add(":)", My.Resources.smile_16x16)
			Me.style.ImagesByText.Add(":(", My.Resources.sad_16x16)
			Me.style.StartAnimation()
			Me.fctb.Text = "This example draws smile image instead of text smile" & vbCrLf & ": + D  = :D" & vbCrLf & ": + @ = :@" & vbCrLf & "8 + ) = 8)" & vbCrLf & ": + bb = :bb" & vbCrLf & ": + ) = :)" & vbCrLf & ": + ( = :("
		End Sub

		Private Sub Fctb_TextChanged(sender As Object, e As TextChangedEventArgs)
			e.ChangedRange.ClearAllStyles()
			For Each key As String In Me.style.ImagesByText.Keys
				Dim pattern As String = Regex.Replace(key, GifImageDrawingSample.RegexSpecSymbolsPattern, "\$0")
				e.ChangedRange.SetStyle(Me.style, pattern)
			Next
		End Sub

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing AndAlso Me.components IsNot Nothing Then
				Me.components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		Private Sub InitializeComponent()
			Me.fctb = New FastColoredTextBox()
			MyBase.SuspendLayout()
			Me.fctb.AutoScrollMinSize = New Size(0, 22)
			Me.fctb.Cursor = Cursors.IBeam
			Me.fctb.Dock = DockStyle.Fill
			Me.fctb.Font = New Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204)
			Me.fctb.Location = New Point(0, 0)
			Me.fctb.Name = "fctb"
			Me.fctb.ShowLineNumbers = False
			Me.fctb.Size = New Size(314, 262)
			Me.fctb.TabIndex = 1
			Me.fctb.WordWrap = True
			AddHandler Me.fctb.TextChanged, New EventHandler(Of TextChangedEventArgs)(AddressOf Me.Fctb_TextChanged)
			MyBase.AutoScaleDimensions = New SizeF(6.0F, 13.0F)
			MyBase.AutoScaleMode = AutoScaleMode.Font
			MyBase.ClientSize = New Size(314, 262)
			MyBase.Controls.Add(Me.fctb)
			MyBase.Name = "GifImageDrawingSample"
			Me.Text = "GifImageDrawingSample"
			MyBase.ResumeLayout(False)
		End Sub
	End Class
End Namespace
