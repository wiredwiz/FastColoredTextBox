#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="FastColoredTextBox_Selections.cs">
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

using FastColoredTextBoxNS.Types;

// ReSharper disable once CheckNamespace
namespace FastColoredTextBoxNS;

public partial class FastColoredTextBox
{
   /// <summary>
   /// Gets a selection of one character from the current position.
   /// </summary>
   /// <param name="place">The place to select from.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the character from the current position.</returns>
   public virtual TextSelectionRange GetCharacterSelection(Place place)
   {
      var lineLength = this[place.iLine].Count;
      if (place.iChar >= lineLength)
         place.iChar = lineLength - 1;

      if (place.iChar < 0)
         place.iChar = 0;

      var start = place;
      var end = start;
      if (place.iChar == lineLength - 1)
         end = new Place(0, FindNextVisibleLine(place.iLine));
      else
         end.Offset(1, 0);

      return new TextSelectionRange(this, start, end);
   }

   /// <summary>
   /// Gets a selection of one word from the current position.
   /// </summary>
   /// <param name="place">The place to select from.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the word from the current position.</returns>
   public virtual TextSelectionRange GetWordSelection(Place place)
   {
      int fromX = place.iChar;
      int toX = place.iChar;

      for (int i = place.iChar; i < lines[place.iLine].Count; i++)
      {
         char c = lines[place.iLine][i].C;
         if (char.IsLetterOrDigit(c) || c == '_')
            toX = i + 1;
         else
            break;
      }

      for (int i = place.iChar - 1; i >= 0; i--)
      {
         char c = lines[place.iLine][i].C;
         if (char.IsLetterOrDigit(c) || c == '_')
            fromX = i;
         else
            break;
      }

      return new TextSelectionRange(this, toX, place.iLine, fromX, place.iLine);
   }

   /// <summary>
   /// Gets a selection of one consistent formatted segment from the current position.
   /// </summary>
   /// <param name="place">The place to select from.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the formatted segment from the current position.</returns>
   public virtual TextSelectionRange GetFormatSelection(Place place)
   {
      return GetLineSelection(place);
   }

   /// <summary>
   /// Gets a selection of one line from the current position.
   /// </summary>
   /// <param name="place">The place to select from.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the line from the current position.</returns>
   public virtual TextSelectionRange GetLineSelection(Place place)
   {
      return new TextSelectionRange(this, new Place(0, place.iLine), new Place(this[place.iLine].Count, place.iLine));
   }

   /// <summary>
   /// Gets a selection of one paragraph from the current position.
   /// </summary>
   /// <param name="place">The place to select from.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the paragraph from the current position.</returns>
   public virtual TextSelectionRange GetParagraphSelection(Place place)
   {
      return GetDocumentSelection(place);
   }

   /// <summary>
   /// Gets a selection of one page from the current position.
   /// </summary>
   /// <param name="place">The place to select from.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the page from the current position.</returns>
   public virtual TextSelectionRange GetPageSelection(Place place)
   {
      return GetDocumentSelection(place);
   }

   /// <summary>
   /// Gets a selection of the entire document from the current position.
   /// </summary>
   /// <param name="place">The place to select from.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the the entire document.</returns>
   /// <remarks>place is superfluous in the base implementation.</remarks>
   public virtual TextSelectionRange GetDocumentSelection(Place place)
   {
      var start = new Place(0, 0);
      var lastLine = Lines.Count;
      var end = new Place(this[lastLine].Count, lastLine);
      return new TextSelectionRange(this, start, end);
   }

   /// <summary>
   /// Gets the next character selection from the specified place.
   /// </summary>
   /// <param name="place">The starting place.</param>
   /// <param name="moveIncrement">The move increment.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the next character selection.</returns>
   public virtual TextSelectionRange GetNextCharacterSelection(Place place, ref int moveIncrement)
   {
      var start = place;
      place.Offset(moveIncrement, 0);
      return new TextSelectionRange(this, start, place);
   }

   /// <summary>
   /// Gets the next word selection from the specified place.
   /// </summary>
   /// <param name="place">The starting place.</param>
   /// <param name="moveIncrement">The move increment.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the next word selection.</returns>
   public virtual TextSelectionRange GetNextWordSelection(Place place, ref int moveIncrement)
   {
      return null;
   }

   /// <summary>
   /// Gets the next formatted segment selection from the specified place.
   /// </summary>
   /// <param name="place">The starting place.</param>
   /// <param name="moveIncrement">The move increment.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the next formatted segment selection.</returns>
   public virtual TextSelectionRange GetNextFormatSelection(Place place, ref int moveIncrement)
   {
      return GetNextLineSelection(place, ref moveIncrement);
   }

   /// <summary>
   /// Gets the next line selection from the specified place.
   /// </summary>
   /// <param name="place">The starting place.</param>
   /// <param name="moveIncrement">The move increment.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the next line selection.</returns>
   public virtual TextSelectionRange GetNextLineSelection(Place place, ref int moveIncrement)
   {
      place.Offset(0, moveIncrement);

      if (place.iLine < 0)
      {
         moveIncrement = place.iLine;
         place.iLine = 0;
      }

      if (place.iLine >= Lines.Count)
      {
         moveIncrement = place.iLine - (Lines.Count - 1);
         place.iLine = Lines.Count - 1;
      }

      return GetLineSelection(place);
   }

   /// <summary>
   /// Gets the next paragraph selection from the specified place.
   /// </summary>
   /// <param name="place">The starting place.</param>
   /// <param name="moveIncrement">The move increment.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the next paragraph selection.</returns>
   public virtual TextSelectionRange GetNextParagraphSelection(Place place, ref int moveIncrement)
   {
      moveIncrement = 0;
      return GetDocumentSelection(place);
   }

   /// <summary>
   /// Gets the next page selection from the specified place.
   /// </summary>
   /// <param name="place">The starting place.</param>
   /// <param name="moveIncrement">The move increment.</param>
   /// <returns>A new <see cref="TextSelectionRange"/> containing the next page selection.</returns>
   public virtual TextSelectionRange GetNextPageSelection(Place place, ref int moveIncrement)
   {
      moveIncrement = 0;
      return GetDocumentSelection(place);
   }
}