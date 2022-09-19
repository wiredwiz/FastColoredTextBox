#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="IVisibleTextLine.cs">
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
using System.Drawing;

namespace FastColoredTextBoxNS.Accessibility;

/// <summary>
/// Interface representing a visible line of text on screen.
/// </summary>
public interface IVisibleTextLine
{
   /// <summary>
   /// Gets the visible text.
   /// </summary>
   /// <value>The text.</value>
   string Text { get; }

   /// <summary>
   /// Gets the starting X coordinate for where the line is drawn from.
   /// </summary>
   /// <value>The start x.</value>
   int StartX { get; }

   /// <summary>
   /// Gets the starting Y coordinate for where the line is drawn from.
   /// </summary>
   /// <value>The starting Y coordinate.</value>
   int StartY { get; }

   /// <summary>
   /// Gets the associated <see cref="LineInfo"/> for the visible line instance.
   /// </summary>
   /// <value>The <see cref="LineInfo"/> for this instance.</value>
   LineInfo Info { get;}

   /// <summary>
   /// Gets the document line number from the source document that the visible line belongs to.
   /// </summary>
   /// <value>The source document line number.</value>
   int SourceLineNo { get; }

   /// <summary>
   /// Gets the wrapped line number in the source document for this visible line.
   /// </summary>
   /// <value>The line wrapped number.</value>
   /// <remarks>This is used in conjunction with the SourceLineNo property to determine where the text comes from.</remarks>
   int LineWrappedNo { get; }

   /// <summary>
   /// Gets the visible bounds that surround the visible line.
   /// </summary>
   /// <returns>A new bounding <see cref="Rectangle"/>.</returns>
   Rectangle GetBounds();
}