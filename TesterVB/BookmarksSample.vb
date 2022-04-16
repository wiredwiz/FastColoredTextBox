﻿
Imports FastColoredTextBoxNS.Feature

Public Class BookmarksSample

	Private Sub BtGo_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGo.DropDownOpening
		btGo.DropDownItems.Clear()
		For Each bookmark In fctb.Bookmarks
			Dim item = btGo.DropDownItems.Add(bookmark.Name)
			item.Tag = bookmark
		Next
	End Sub

	Private Sub BtRemoveBookmark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRemoveBookmark.Click
		fctb.Bookmarks.Remove(fctb.Selection.Start.iLine)
	End Sub

	Private Sub BtAddBookmark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btAddBookmark.Click
		fctb.Bookmarks.Add(fctb.Selection.Start.iLine)
	End Sub

	Private Sub BtGo_DropDownItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles btGo.DropDownItemClicked
		TryCast(e.ClickedItem.Tag, Bookmark).DoVisible()
	End Sub
End Class