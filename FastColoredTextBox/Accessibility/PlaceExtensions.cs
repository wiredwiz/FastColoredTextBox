#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="PlaceExtensions.cs">
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

namespace FastColoredTextBoxNS.Accessibility;

/// <summary>
/// Class with extension methods for <see cref="Place"/>.
/// </summary>
public static class PlaceExtensions
{
   /// <summary>
   /// Returns a new place advanced from this place forward within the specified TextBox.
   /// </summary>
   /// <param name="place">The place to advance.</param>
   /// <param name="tb">The TextBox.</param>
   /// <param name="forward">if set to <c>true</c> advances [forward]; otherwise advances backwards.</param>
   /// <returns>A new <see cref="Place"/> representing the advancement.</returns>
   // ReSharper disable once FlagArgument
   public static Place Advance(this Place place, FastColoredTextBox tb, bool forward = true)
   {
      var result = place;
      if (forward)
      {
         result.Offset(1, 0);
         if (result.iChar == tb.TextSource[result.iLine].Count)
         {
            if (result.iLine < tb.Lines.Count - 1)
            {
               result.iLine++;
               result.iChar = 0;
            }
            else if (result.iChar > tb.TextSource[result.iLine].Count - 1)
               result.iChar = tb.TextSource[result.iLine].Count - 1;
         }
      }
      else
      {
         result.Offset(-1, 0);
         if (result.iChar < 0)
         {
            if (result.iLine != 0)
            {
               result.iLine--;
               result.iChar = tb.TextSource[result.iLine].Count - 1;
            }
            else if (result.iChar < 0)
               result.iChar = 0;
         }
      }

      return result;
   }
}