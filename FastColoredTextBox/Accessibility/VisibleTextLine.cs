#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="VisibleTextLine.cs">
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

using System.Drawing;
using FastColoredTextBoxNS.Types;

namespace FastColoredTextBoxNS.Accessibility;

/// <summary>
/// Class that represents a visible text line on the screen.
/// Implements the <see cref="FastColoredTextBoxNS.Accessibility.IVisibleTextLine" />
/// </summary>
/// <seealso cref="FastColoredTextBoxNS.Accessibility.IVisibleTextLine" />
public class VisibleTextLine : IVisibleTextLine
{
   /// <summary>
   /// Initializes a new instance of the <see cref="VisibleTextLine"/> class.
   /// </summary>
   /// <param name="textBox">The text box.</param>
   /// <param name="text">The text.</param>
   /// <param name="startX">The starting X position of the line</param>
   /// <param name="startY">The starting Y position of the line.</param>
   /// <param name="info">The information.</param>
   /// <param name="sourceLineNo">The source line no.</param>
   /// <param name="lineWrappedNo">The line wrapped no.</param>
   public VisibleTextLine(FastColoredTextBox textBox,
                          string text,
                          int startX,
                          int startY,
                          LineInfo info,
                          int sourceLineNo,
                          int lineWrappedNo)
   {
      TextBox = textBox;
      Text = text;
      StartX = startX;
      StartY = startY;
      Info = info;
      SourceLineNo = sourceLineNo;
      LineWrappedNo = lineWrappedNo;
   }

   /// <summary>
   /// Gets or sets the <see cref="FastColoredTextBox"/> instance that the visible text derives from.
   /// </summary>
   /// <value>The parent <see cref="FastColoredTextBox"/>.</value>
   protected FastColoredTextBox TextBox { get; set; }

   /// <inheritdoc />
   public string Text { get; set; }

   /// <inheritdoc />
   public int StartX { get; }

   /// <inheritdoc />
   public int StartY { get; }

   /// <inheritdoc />
   public LineInfo Info { get; }

   /// <inheritdoc />
   public int SourceLineNo { get; }

   /// <inheritdoc />
   public int LineWrappedNo { get; }

   /// <inheritdoc />
   public Rectangle GetBounds()
   {
      var width = Text.Length * TextBox.CharWidth;
      return new Rectangle(TextBox.Left, StartY, width == 0 ? 3 : width, TextBox.CharHeight);
   }
}