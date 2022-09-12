#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="FastColoredTextBoxAccessibleObject.cs">
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
using FastColoredTextBoxNS.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace FastColoredTextBoxNS;

public partial class FastColoredTextBox
{
   /// <summary>
   /// Class that implements accessibility for a Fast Colored Text Box instance.
   /// Implements the <see cref="AccessibleObject" />
   /// </summary>
   /// <seealso cref="AccessibleObject" />
// ReSharper disable once InconsistentNaming
   public class FastColoredTextBoxAccessibleObject : AccessibleObject
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="FastColoredTextBoxAccessibleObject"/> class.
      /// </summary>
      /// <param name="textBox">The text box.</param>
      public FastColoredTextBoxAccessibleObject(FastColoredTextBox textBox, AccessibleObject parent)
      {
         //Parent = parent;
         TextBox = textBox;
         Name = string.Empty;
         Lines = new List<LineAccessibleObject>();
         BuildAccessibleLines(textBox);
      }

      internal List<LineAccessibleObject> Lines;

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

      //protected string _Value = null;

      ///// <summary>
      ///// Gets or sets the value of an accessible object.
      ///// </summary>
      //public override string Value
      //{
      //    get => _Value;
      //    set => _Value = value;
      //}

      /// <summary>
      /// Gets the state of this accessible object.
      /// </summary>
      /// <value>The state.</value>
      public override AccessibleStates State
      {
         get
         {
            AccessibleStates state = AccessibleStates.Focusable | AccessibleStates.Selectable;
            if (TextBox.Focused)
               state |= AccessibleStates.Selected | AccessibleStates.Focused;
            return state;
         }
      }

      /////// <summary>
      /////// Gets a description of what the object does or how the object is used.
      /////// </summary>
      /////// <value>The help.</value>
      ////public override string Help => "Edit";

      ///// <summary>
      ///// Gets the parent of an accessible object.
      ///// </summary>
      ///// <value>The parent.</value>
      //public override AccessibleObject Parent { get; }

      /// <summary>
      /// Retrieves the accessible child corresponding to the specified index.
      /// </summary>
      /// <param name="index">The zero-based index of the accessible child.</param>
      /// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
      public override AccessibleObject GetChild(int index)
      {
         if (index >= 0 && index < Lines.Count)
            return Lines[index];

         return null;
      }

      /// <summary>
      /// Retrieves the number of children belonging to an accessible object.
      /// </summary>
      /// <returns>The number of children belonging to an accessible object.</returns>
      public override int GetChildCount()
      {
         return Lines.Count;
      }

      public override AccessibleObject GetSelected()
      {
         if (Lines.Count != 0)
            return Lines[0];

         return base.GetSelected();
      }

      public override AccessibleObject GetFocused()
      {
         if (Lines.Count != 0)
            return Lines[0];

         return base.GetFocused();
      }

      public override void Select(AccessibleSelection flags)
      {
         var childId = 0;

         // Determine which selection action should occur, based on the
         // AccessibleSelection value.
         if ((flags & AccessibleSelection.TakeSelection) != 0)
         {
            for (var i = 0; i < Lines.Count; i++)
               Lines[i].Selected = i == childId;

            // AccessibleSelection.AddSelection means that the CurveLegend will be selected.
            if ((flags & AccessibleSelection.AddSelection) != 0)
               Lines[childId].Selected = true;

            // AccessibleSelection.AddSelection means that the CurveLegend will be unselected.
            if ((flags & AccessibleSelection.RemoveSelection) != 0)
               Lines[childId].Selected = false;
         }
      }

      /// <summary>
      /// Builds the accessible objects for all the visible lines in the text box.
      /// </summary>
      /// <param name="textBox">The text box.</param>
      protected virtual void BuildAccessibleLines(FastColoredTextBox textBox)
      {
         Lines = new List<LineAccessibleObject>();
         int firstChar = Math.Max(0, TextBox.HorizontalScroll.Value - TextBox.Paddings.Left) / TextBox.CharWidth;
         int lastChar = (TextBox.HorizontalScroll.Value + TextBox.ClientSize.Width) / TextBox.CharWidth;
         //
         var x = TextBox.LeftIndent + TextBox.Paddings.Left - TextBox.HorizontalScroll.Value;
         if (x < TextBox.LeftIndent)
            firstChar++;
         int startLine = textBox.YtoLineIndex(textBox.VerticalScroll.Value);
         var cHeight = textBox.CharHeight;
         var numLines = (int)Math.Ceiling(textBox.ClientRectangle.Height / (double)cHeight);
         for (int i = startLine; i < textBox.Lines.Count; i++)
         {
            LineInfo lineInfo = TextBox.LineInfos[i];
            if (!BuildAccessibleLines(textBox, lineInfo, i, numLines, firstChar, lastChar))
               break;
         }
      }

      private bool BuildAccessibleLines(FastColoredTextBox textBox, LineInfo info, int i, int maxLines, int firstChar,
         int lastChar)
      {
         Line line = textBox.TextSource[i];
         for (int j = 0; j < info.WordWrapStringsCount; j++)
         {
            if (Lines.Count == maxLines)
               return false;

            var bounds = CalculateBounds(textBox, info, j + 1, firstChar, lastChar);

            var text = GetLineText(line, info, j, firstChar, lastChar);
            var accessible = new LineAccessibleObject(textBox, i, Lines.Count, text, bounds);
            Lines.Add(accessible);
         }

         return true;
      }

      private Rectangle CalculateBounds(FastColoredTextBox textBox, LineInfo lineInfo, int wrapNo, int firstChar,
         int lastChar)
      {
         // TODO: Better handle out of range lines later, for now we assume folding is disabled for screen reading
         //if (lineInfo.startY > TextBox.VerticalScroll.Value + TextBox.ClientSize.Height)
         //   break;
         //if (lineInfo.startY + lineInfo.WordWrapStringsCount * TextBox.CharHeight < TextBox.VerticalScroll.Value)
         //   continue;
         //if (lineInfo.VisibleState == VisibleState.Hidden)
         //   continue;

         int y = lineInfo.startY - TextBox.VerticalScroll.Value;
         //

         var height = textBox.CharHeight; // * lineInfo.WordWrapStringsCount;
         var endY = Math.Min(y + height, textBox.TextAreaRect.Bottom);

         return new Rectangle(firstChar,
            y,
            lastChar - firstChar,
            endY - y);
      }

      string GetLineText(Line line, LineInfo info, int wrapNo, int firstChar, int lastChar)
      {
         int from = info.GetWordWrapStringStartPosition(wrapNo);
         int to = info.GetWordWrapStringFinishPosition(wrapNo, line);
         lastChar = Math.Min(to - from, lastChar);


         var start = from + firstChar;
         var length = from + lastChar + 1 - start;
         return line.Text.Substring(start, length);
      }
   }
}