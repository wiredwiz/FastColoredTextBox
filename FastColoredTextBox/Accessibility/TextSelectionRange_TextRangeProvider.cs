#region BSD 3-Clause License
// <copyright company="Edgerunner.org" file="TextSelectionRange_TextRangeProvider.cs">
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

using System.Windows.Automation.Provider;
using System.Windows.Automation.Text;

namespace FastColoredTextBoxNS.Types;

public partial class TextSelectionRange : ITextRangeProvider
{
   ITextRangeProvider ITextRangeProvider.Clone()
   {
      return this.Clone();
   }

   bool ITextRangeProvider.Compare(ITextRangeProvider range)
   {
      return range == this;
   }

   int ITextRangeProvider.CompareEndpoints(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange,
      TextPatternRangeEndpoint targetEndpoint)
   {
      throw new System.NotImplementedException();
   }

   void ITextRangeProvider.ExpandToEnclosingUnit(TextUnit unit)
   {
      throw new System.NotImplementedException();
   }

   ITextRangeProvider ITextRangeProvider.FindAttribute(int attribute, object value, bool backward)
   {
      throw new System.NotImplementedException();
   }

   ITextRangeProvider ITextRangeProvider.FindText(string text, bool backward, bool ignoreCase)
   {
      throw new System.NotImplementedException();
   }

   object ITextRangeProvider.GetAttributeValue(int attribute)
   {
      throw new System.NotImplementedException();
   }

   double[] ITextRangeProvider.GetBoundingRectangles()
   {
      throw new System.NotImplementedException();
   }

   IRawElementProviderSimple ITextRangeProvider.GetEnclosingElement()
   {
      throw new System.NotImplementedException();
   }

   string ITextRangeProvider.GetText(int maxLength)
   {
      throw new System.NotImplementedException();
   }

   int ITextRangeProvider.Move(TextUnit unit, int count)
   {
      throw new System.NotImplementedException();
   }

   int ITextRangeProvider.MoveEndpointByUnit(TextPatternRangeEndpoint endpoint, TextUnit unit, int count)
   {
      throw new System.NotImplementedException();
   }

   void ITextRangeProvider.MoveEndpointByRange(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange,
      TextPatternRangeEndpoint targetEndpoint)
   {
      throw new System.NotImplementedException();
   }

   /// <summary>
   /// Selects this instance.
   /// </summary>
   /// <exception cref="System.NotImplementedException"></exception>
   void ITextRangeProvider.Select()
   {
      throw new System.NotImplementedException();
   }

   /// <summary>
   /// Adds to selection.
   /// </summary>
   /// <exception cref="System.NotImplementedException"></exception>
   void ITextRangeProvider.AddToSelection()
   {
      throw new System.NotImplementedException();
   }

   /// <summary>
   /// Removes from selection.
   /// </summary>
   /// <exception cref="System.NotImplementedException"></exception>
   void ITextRangeProvider.RemoveFromSelection()
   {
      throw new System.NotImplementedException();
   }

   /// <summary>
   /// Scrolls the into view.
   /// </summary>
   /// <param name="alignToTop">if set to <c>true</c> [align to top].</param>
   /// <exception cref="System.NotImplementedException"></exception>
   void ITextRangeProvider.ScrollIntoView(bool alignToTop)
   {
      throw new System.NotImplementedException();
   }

   /// <summary>
   /// Gets the children.
   /// </summary>
   /// <returns>IRawElementProviderSimple[].</returns>
   /// <exception cref="System.NotImplementedException"></exception>
   IRawElementProviderSimple[] ITextRangeProvider.GetChildren()
   {
      throw new System.NotImplementedException();
   }
}