#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="FastColoredTextBox_Helpers.cs">
// Copyright (c)  2022
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

// ReSharper disable once CheckNamespace
using FastColoredTextBoxNS.Types;
using System.Collections.Generic;
using System.Drawing;
using System;

// ReSharper disable once CheckNamespace
namespace FastColoredTextBoxNS;

/// <summary>
/// Partial class containing helper methods for fetching specific text blocks.
/// This code will get cleaned up and used with the TextProvider and TextRangeProvider logic
/// 
/// Implements the <see cref="System.Windows.Forms.UserControl" />
/// Implements the <see cref="System.ComponentModel.ISupportInitialize" />
/// Implements the <see cref="System.Windows.Automation.Provider.ITextProvider" />
/// Implements the <see cref="System.Windows.Automation.Provider.IRawElementProviderSimple" />
/// </summary>
/// <seealso cref="System.Windows.Forms.UserControl" />
/// <seealso cref="System.ComponentModel.ISupportInitialize" />
/// <seealso cref="System.Windows.Automation.Provider.ITextProvider" />
/// <seealso cref="System.Windows.Automation.Provider.IRawElementProviderSimple" />
public partial class FastColoredTextBox
{
      /// <summary>
      /// Builds the accessible objects for all the visible lines in the text box.
      /// </summary>
      /// <param name="textBox">The text box.</param>
      protected virtual void BuildAccessibleLines(FastColoredTextBox textBox)
      {
         //Lines = new List<LineAccessibleObject>();
         int firstChar = Math.Max(0, textBox.HorizontalScroll.Value - textBox.Paddings.Left) / textBox.CharWidth;
         int lastChar = (textBox.HorizontalScroll.Value + textBox.ClientSize.Width) / textBox.CharWidth;
         //
         var x = textBox.LeftIndent + textBox.Paddings.Left - textBox.HorizontalScroll.Value;
         if (x < textBox.LeftIndent)
            firstChar++;
         int startLine = textBox.YtoLineIndex(textBox.VerticalScroll.Value);
         var cHeight = textBox.CharHeight;
         var numLines = (int)Math.Ceiling(textBox.ClientRectangle.Height / (double)cHeight);
         for (int i = startLine; i < textBox.Lines.Count; i++)
         {
            LineInfo lineInfo = textBox.LineInfos[i];
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
            //var accessible = new LineAccessibleObject(textBox, i, Lines.Count, text, bounds);
            //Lines.Add(accessible);
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

         int y = lineInfo.startY - textBox.VerticalScroll.Value;

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