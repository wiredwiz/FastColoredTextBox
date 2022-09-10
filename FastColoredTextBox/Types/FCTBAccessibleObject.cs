#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="FCTBAccessibleObject.cs">
// Copyright (c) Thaddeus Ryker 2022
// </copyright>
//
// BSD 3-Clause License
//
// Copyright (c) 2022,
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer.
//
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// 3. Neither the name of the copyright holder nor the names of its
//    contributors may be used to endorse or promote products derived from
//    this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using FastColoredTextBoxNS.Feature;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;

namespace FastColoredTextBoxNS.Types;

/// <summary>
/// Class that implements accessibility for a Fast Colored Text Box instance.
/// Implements the <see cref="AccessibleObject" />
/// </summary>
/// <seealso cref="AccessibleObject" />
// ReSharper disable once InconsistentNaming
public class FCTBAccessibleObject : AccessibleObject
{
   /// <summary>
   /// Initializes a new instance of the <see cref="FCTBAccessibleObject"/> class.
   /// </summary>
   /// <param name="textBox">The text box.</param>
   public FCTBAccessibleObject(FastColoredTextBox textBox)
   {
      TextBox = textBox;
      Name = string.Empty;
      Lines = new List<AccessibleObject>();
   }

   internal List<AccessibleObject> Lines;

   /// <summary>
   /// Gets or sets the text box.
   /// </summary>
   /// <value>The text box.</value>
   internal FastColoredTextBox TextBox { get; set; }

   /// <summary>
   /// Gets the location and size of the accessible object.
   /// </summary>
   /// <value>The bounds.</value>
   public override Rectangle Bounds => new Rectangle(TextBox.PointToScreen(TextBox.Location), TextBox.Size);

   /// <summary>
   /// Gets the role of this accessible object.
   /// </summary>
   /// <value>The role.</value>
   public override AccessibleRole Role => AccessibleRole.Text;

   /// <summary>
   /// Gets a string that describes the visual appearance of the specified object. Not all objects have a description.
   /// </summary>
   /// <value>The description.</value>
   public override string Description => "A text box";

   /// <summary>
   /// Gets or sets the object name.
   /// </summary>
   /// <value>The name.</value>
   public override string Name { get; set; }

   /// <summary>
   /// Gets a string that describes the default action of the object. Not all objects have a default action.
   /// </summary>
   public override string DefaultAction => "Edit";

   //protected string _Value = "foo text";

   ///// <summary>
   ///// Gets or sets the value of an accessible object.
   ///// </summary>
   //public override string Value
   //{
   //   get => _Value;
   //   set => _Value = value;
   //}

   /// <summary>
   /// Gets the state of this accessible object.
   /// </summary>
   /// <value>The state.</value>
   public override AccessibleStates State
   {
      get
      {
         AccessibleStates state = AccessibleStates.Focusable;// | AccessibleStates.Selectable;
         //if (TextBox.Focused)
         //    state |= AccessibleStates.Selected | AccessibleStates.Focused;
         return state;
      }
   }

   ///// <summary>
   ///// Gets a description of what the object does or how the object is used.
   ///// </summary>
   ///// <value>The help.</value>
   //public override string Help => "Edit";

   /// <summary>
   /// Gets the parent of an accessible object.
   /// </summary>
   /// <value>The parent.</value>
   public override AccessibleObject Parent => TextBox.Parent.AccessibilityObject;

   /// <summary>
   /// Retrieves the accessible child corresponding to the specified index.
   /// </summary>
   /// <param name="index">The zero-based index of the accessible child.</param>
   /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
   public override AccessibleObject GetChild(int index)
   {
      return Lines[index];
   }

   /// <summary>
   /// Retrieves the number of children belonging to an accessible object.
   /// </summary>
   /// <returns>The number of children belonging to an accessible object.</returns>
   public override int GetChildCount()
   {
      Lines = BuildLines(TextBox.TextSource.ToList());
      return Lines.Count;
   }

   /// <summary>
   /// Retrieves the object that has the keyboard focus.
   /// </summary>
   /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that specifies the currently focused child. This method returns the calling object if the object itself is focused. Returns <see langword="null" /> if no object has focus.</returns>
   public override AccessibleObject GetFocused()
   {
      return base.GetFocused();
   }

   /// <summary>
   /// Retrieves the currently selected child.
   /// </summary>
   /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the currently selected child. This method returns the calling object if the object itself is selected. Returns <see langword="null" /> if is no child is currently selected and the object itself does not have focus.</returns>
   public override AccessibleObject GetSelected()
   {
      return base.GetSelected();
   }

   /// <summary>
   /// Retrieves the child object at the specified screen coordinates.
   /// </summary>
   /// <param name="x">The horizontal screen coordinate.</param>
   /// <param name="y">The vertical screen coordinate.</param>
   /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the child object at the given screen coordinates. This method returns the calling object if the object itself is at the location specified. Returns <see langword="null" /> if no object is at the tested location.</returns>
   public override AccessibleObject HitTest(int x, int y)
   {
      return base.HitTest(x, y);
   }

   /// <summary>
   /// Navigates to another accessible object.
   /// </summary>
   /// <param name="navdir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
   /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents one of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</returns>
   public override AccessibleObject Navigate(AccessibleNavigation navdir)
   {
      switch (navdir)
      {
         case AccessibleNavigation.Down:
         case AccessibleNavigation.Right:
         case AccessibleNavigation.FirstChild:
            if (Lines.Count > 0)
               return Lines[0];
            else
               return base.Navigate(navdir);
            break;
         default:
            return base.Navigate(navdir);
      }
   }

   /// <summary>
   /// Modifies the selection or moves the keyboard focus of the accessible object.
   /// </summary>
   /// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
   public override void Select(AccessibleSelection flags)
   {
      base.Select(flags);
   }

   /// <summary>
   /// Raises the LiveRegionChanged UI automation event.
   /// </summary>
   /// <returns><see langword="true" /> if the operation succeeds; <see langword="False" /> otherwise.</returns>
   public override bool RaiseLiveRegionChanged()
   {
      return base.RaiseLiveRegionChanged();
   }

   protected List<AccessibleObject> BuildLines(List<Line> lines)
   {
      var textAreaRect = TextBox.TextAreaRect;

      int leftTextIndent = Math.Max(TextBox.LeftIndent, TextBox.LeftIndent + TextBox.Paddings.Left - TextBox.HorizontalScroll.Value);
      int textWidth = textAreaRect.Width;
      int firstChar = (Math.Max(0, TextBox.HorizontalScroll.Value - TextBox.Paddings.Left)) / TextBox.CharWidth;
      int lastChar = (TextBox.HorizontalScroll.Value + TextBox.ClientSize.Width) / TextBox.CharWidth;
      //
      var x = TextBox.LeftIndent + TextBox.Paddings.Left - TextBox.HorizontalScroll.Value;
      if (x < TextBox.LeftIndent)
         firstChar++;

      int startLine = TextBox.YtoLineIndex(TextBox.VerticalScroll.Value);
      int iLine;

      var results = new List<AccessibleObject>();

      //draw text
      for (iLine = startLine; iLine < lines.Count; iLine++)
      {
         Line line = lines[iLine];
         LineInfo lineInfo = TextBox.LineInfos[iLine];
         //
         if (lineInfo.startY > TextBox.VerticalScroll.Value + TextBox.ClientSize.Height)
            break;
         if (lineInfo.startY + lineInfo.WordWrapStringsCount * TextBox.CharHeight < TextBox.VerticalScroll.Value)
            continue;
         if (lineInfo.VisibleState == VisibleState.Hidden)
            continue;

         int y = lineInfo.startY - TextBox.VerticalScroll.Value;
         //

         var height = TextBox.CharHeight * lineInfo.WordWrapStringsCount;
         var endY = Math.Min(y + height, textAreaRect.Bottom);
         Rectangle bounds = new Rectangle(firstChar,
                                          y,
                                          lastChar - firstChar,
                                          endY - y);

         ////draw wordwrap strings of line
         //for (int iWordWrapLine = 0; iWordWrapLine < lineInfo.WordWrapStringsCount; iWordWrapLine++)
         //{
         //   y = lineInfo.startY + iWordWrapLine * TextBox.CharHeight - TextBox.VerticalScroll.Value;
         //   // break if too long line (important for extremely big lines)
         //   if (y > TextBox.VerticalScroll.Value + TextBox.ClientSize.Height)
         //      break;
         //   // continue if wordWrapLine isn't seen yet (important for extremely big lines)
         //   if (lineInfo.startY + iWordWrapLine * TextBox.CharHeight < TextBox.VerticalScroll.Value)
         //      continue;

         //   //indent
         //   var indent = iWordWrapLine == 0 ? 0 : lineInfo.wordWrapIndent * TextBox.CharWidth;
         //   //draw chars
         //   //DrawLineChars(e.Graphics, firstChar, lastChar, iLine, iWordWrapLine, x + indent, y);
         //}

         results.Add(new LineAccessibleObject(TextBox, line, bounds));
      }

      int endLine = iLine - 1;

      return results;
   }
}