#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="AccessibilityHelper.cs">
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

using FastColoredTextBoxNS.Types;
using System.Collections.Generic;
using System;

namespace FastColoredTextBoxNS.Accessibility;

/// <summary>
/// Class of accessibility helper methods.
/// </summary>
// ReSharper disable once HollowTypeName
public static class AccessibilityHelper
{
   /// <summary>
   /// Returns a list of visible text lines corresponding to text displayed in the editor.
   /// </summary>
   /// <param name="textBox">The <see cref="FastColoredTextBox"/>.</param>
   public static IList<IVisibleTextLine> GetVisibleLines(FastColoredTextBox textBox)
   {
      var visible = new List<IVisibleTextLine>();
      int firstChar = Math.Max(0, textBox.HorizontalScroll.Value - textBox.Paddings.Left) / textBox.CharWidth;
      int lastChar = (textBox.HorizontalScroll.Value + textBox.ClientSize.Width) / textBox.CharWidth;

      var x = textBox.LeftIndent + textBox.Paddings.Left - textBox.HorizontalScroll.Value;
      if (x < textBox.LeftIndent)
         firstChar++;
      int startLine = textBox.YtoLineIndex(textBox.VerticalScroll.Value);
      for (int i = startLine; i < textBox.Lines.Count; i++)
      {
         var subLines = ConstructVisibleLinesFromSingleLine(textBox, i, firstChar, lastChar);
         if (subLines.Count == 0)
            break;

         visible.AddRange(subLines);
      }

      return visible;
   }

   /// <summary>
   /// Constructs the visible lines that correspond to a single text line.
   /// </summary>
   /// <param name="textBox">The <see cref="FastColoredTextBox"/>.</param>
   /// <param name="lineIndex">The line index.</param>
   /// <param name="firstChar">The first visible character position.</param>
   /// <param name="lastChar">The last visible character position.</param>
   /// <returns>A <see cref="List{T}"/> of <see cref="IVisibleTextLine"/> instances.</returns>
   public static List<IVisibleTextLine> ConstructVisibleLinesFromSingleLine(FastColoredTextBox textBox, int lineIndex, int firstChar, int lastChar)
   {
      var visible = new List<IVisibleTextLine>();
      var info = textBox.LineInfos[lineIndex];

      var startY = info.startY;
      for (int iWordWrapLine = 0; iWordWrapLine < info.WordWrapStringsCount; iWordWrapLine++)
      {
         var y = info.startY + iWordWrapLine * textBox.CharHeight - textBox.VerticalScroll.Value;
         // break if too long line (important for extremely big lines)
         if (y > textBox.VerticalScroll.Value + textBox.ClientSize.Height)
            break;
         // continue if wordWrapLine isn't seen yet (important for extremely big lines)
         if (info.startY + iWordWrapLine * textBox.CharHeight < textBox.VerticalScroll.Value)
            continue;

         var yAdjust = iWordWrapLine * textBox.CharHeight;

         var text = GetVisibleLineText(textBox.TextSource[lineIndex], info, lineIndex, firstChar, lastChar);
         var visibleLine = new VisibleTextLine(textBox, text, firstChar, startY + yAdjust, info, lineIndex, iWordWrapLine);
         visible.Add(visibleLine);
      }

      return visible;
   }

   /// <summary>
   /// Gets the visible text from a line.
   /// </summary>
   /// <param name="line">The line to get the text from.</param>
   /// <param name="info">The line information.</param>
   /// <param name="wrapNo">The wrapped line number.</param>
   /// <param name="firstChar">The first visible character position.</param>
   /// <param name="lastChar">The last visible character position.</param>
   /// <returns>System.String.</returns>
   public static string GetVisibleLineText(Line line, LineInfo info, int wrapNo, int firstChar, int lastChar)
   {
      int from = info.GetWordWrapStringStartPosition(wrapNo);
      int to = info.GetWordWrapStringFinishPosition(wrapNo, line);
      lastChar = Math.Min(to - from, lastChar);


      var start = from + firstChar;
      var length = from + lastChar + 1 - start;
      return line.Text.Substring(start, length);
   }
}